using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebService.Database;
using WebService.Models;
using WebService.Utilities;

namespace WebService.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Multiple()
        {

            string application_id = "";
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/applications");
            string rename = "";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>();

                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var data in provider.FormData.GetValues(key))
                    {
                        keyValuePairs.Add(key, data);
                    }
                }

                string json = keyValuePairs["json"].ToString();

                Form form = JsonConvert.DeserializeObject<Form>(json);
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();

                if (form.application_id == null || form.application_id == "")
                {
                    application_id = Generator.application_id();
                    form.application_id = application_id;
                }
                else
                {
                    application_id = form.application_id;
                }

                form.category = Commons.Constants.TYPE_APPROVAL;
                form.status = Commons.Constants.SUBMITTED_TYPE;
                db.SaveApplication(form);
                db.NewUnassignedTask(form.application_id, form.username, DateTime.Now);

                Commons.UserActivity.Record(new UserActivity(form.username, Commons.Constants.SUBMISSION_TYPE, form.application_id, form.status));

                foreach (MultipartFileData file in provider.FileData)
                {
                    rename = root + @"\" + form.username + @"\" + application_id + @"\" + file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    if (!Directory.Exists(root + @"\" + form.username + @"\" + application_id))
                    {
                        Directory.CreateDirectory(root + @"\" + form.username + @"\" + application_id);
                    }

                    if (!File.Exists(rename))
                    {
                        File.Move(file.LocalFileName, rename);
                        var purpose = file.Headers.ContentDisposition.Name.Replace("\"", "");
                        switch (purpose)
                        {
                            case "tech_spec":
                                db.AddFileReference(Generator.guid(),  file.Headers.ContentDisposition.FileName.Replace("\"", ""), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.TECHNICAL_SPECIFICATION_FILE);
                                break;
                            case "test_report":
                                db.AddFileReference(Generator.guid(), file.Headers.ContentDisposition.FileName.Replace("\"", ""), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.TEST_REPORT_FILE);
                                break;
                            case "accreditation":
                                db.AddFileReference(Generator.guid(), file.Headers.ContentDisposition.FileName.Replace("\"", ""), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.ACCREDITATION_FILE);
                                break;
                        }
                    }
                    else
                    {
                        File.Delete(file.LocalFileName);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, application_id);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}