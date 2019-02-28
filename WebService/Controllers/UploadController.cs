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
                Email.SendEmailAdmins("New Type Application Application", "Type approval application "+form.application_id+" for Device with Model number "+form.product_identification+" from "+form.applicant_name+" has been submitted for processing.");

                UserDetails userDetail = db.GetUserDetails(form.username);
                Email.Send(userDetail.email, "Submitted Type Approval Application", "Type Approval application "+form.application_id+" for Device with Model number "+form.product_identification+" has been received by our internal team and processing will commence once payment has been received.");

                Commons.UserActivity.Record(new UserActivity(form.username, Commons.Constants.ACTIVITY_SUBMISSION_TYPE, form.application_id, form.status));
                foreach (MultipartFileData file in provider.FileData)
                {
                    var type = file.Headers.ContentType.MediaType;
                    if (type == "application/pdf")
                    {
                        var purpose = file.Headers.ContentDisposition.Name.Replace("\"", "");
                        string path = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                        string name = Path.GetFileName(path);
                        switch (purpose)
                        {

                            case "tech_spec":
                                rename = root + @"\" + form.username + @"\" + application_id + @"\" + "technical_specifications" + @"\" + Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                                if (!Directory.Exists(root + @"\" + form.username + @"\" + application_id + @"\" + "technical_specifications"))
                                {
                                    Directory.CreateDirectory(root + @"\" + form.username + @"\" + application_id + @"\" + "technical_specifications");
                                }
                                break;
                            case "test_report":
                                rename = root + @"\" + form.username + @"\" + application_id + @"\" + "test_report" + @"\" + Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                                if (!Directory.Exists(root + @"\" + form.username + @"\" + application_id + @"\" + "test_report"))
                                {
                                    Directory.CreateDirectory(root + @"\" + form.username + @"\" + application_id + @"\" + "test_report");
                                }
                                break;
                            case "accreditation":
                                rename = root + @"\" + form.username + @"\" + application_id + @"\" + "accreditation" + @"\" + Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                                if (!Directory.Exists(root + @"\" + form.username + @"\" + application_id + @"\" + "accreditation"))
                                {
                                    Directory.CreateDirectory(root + @"\" + form.username + @"\" + application_id + @"\" + "accreditation");
                                }
                                break;
                            case "letter_auth":
                                rename = root + @"\" + form.username + @"\" + application_id + @"\" + "letter of authorization" + @"\" + Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                                if (!Directory.Exists(root + @"\" + form.username + @"\" + application_id + @"\" + "letter of authorization"))
                                {
                                    Directory.CreateDirectory(root + @"\" + form.username + @"\" + application_id + @"\" + "letter of authorization");
                                }
                                break;
                            case "user_man":
                                rename = root + @"\" + form.username + @"\" + application_id + @"\" + "user manual" + @"\" + Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                                if (!Directory.Exists(root + @"\" + form.username + @"\" + application_id + @"\" + "user manual"))
                                {
                                    Directory.CreateDirectory(root + @"\" + form.username + @"\" + application_id + @"\" + "user manual");
                                }
                                break;
                        }

                        if (!File.Exists(rename))
                        {
                            File.Move(file.LocalFileName, rename);

                            switch (purpose)
                            {
                                case "tech_spec":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.TECHNICAL_SPECIFICATION_FILE);
                                    break;
                                case "test_report":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.TEST_REPORT_FILE);
                                    break;
                                case "accreditation":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.ACCREDITATION_FILE);
                                    break;
                                case "letter_auth":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.LETTER_AUTHORIZATION_FILE);
                                    break;
                                case "user_man":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.USER_MANUAL_FILE);
                                    break;
                            }
                        }
                        else
                        {
                            File.Delete(rename);
                            File.Move(file.LocalFileName, rename);

                            switch (purpose)
                            {
                                case "tech_spec":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.TECHNICAL_SPECIFICATION_FILE);
                                    break;
                                case "test_report":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.TEST_REPORT_FILE);
                                    break;
                                case "accreditation":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.ACCREDITATION_FILE);
                                    break;
                                case "letter_auth":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.LETTER_AUTHORIZATION_FILE);
                                    break;
                                case "user_man":
                                    db.AddFileReference(Generator.guid(), Path.GetFileName(file.Headers.ContentDisposition.FileName.Replace("\"", "")), DateTime.Now, rename, application_id, form.name_of_test, form.country, form.username, Commons.Constants.USER_MANUAL_FILE);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        foreach (MultipartFileData files in provider.FileData)
                        {
                            File.Delete(files.LocalFileName);
                        }
                        
                        return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "invalid_file");
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