using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using WebService.Database;
using WebService.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using WebService.Hubs;
using Newtonsoft.Json;
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

                string json = keyValuePairs["json_form"].ToString();
                Form form = JsonConvert.DeserializeObject<Form>(json);
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();

                if (form.application_id == null || form.application_id == "")
                {
                    application_id = Generator.guid();
                    form.application_id = application_id;
                }
                else
                {
                    application_id = form.application_id;
                }

                form.category = Commons.Constants.TYPE_APPROVAL;
                form.status = Commons.Constants.SUBMITTED_TYPE;
                db.SaveApplication(form);

                Commons.UserActivity.Record(new UserActivity(form.username, Commons.Constants.SUBMISSION_TYPE, form.application_id, form.status));

                foreach (MultipartFileData file in provider.FileData)
                {
                    rename = root + @"\"+form.username+@"\" + file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    if (!Directory.Exists(root + @"\" + form.username))
                    {
                        Directory.CreateDirectory(root + @"\" + form.username);
                    }

                    if (!File.Exists(rename))
                    {
                        File.Move(file.LocalFileName, rename);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK, application_id);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Single()
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
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

                string json = keyValuePairs["application_id"].ToString();
                dynamic obj = JsonConvert.DeserializeObject<dynamic>(json);

                if ((string)obj.application_id == null || (string)obj.application_id == "")
                {
                    application_id = Generator.guid();
                    obj.application_id = application_id;
                }
                else
                {
                    application_id = (string)obj.application_id;
                }

                foreach (MultipartFileData file in provider.FileData)
                {
                    rename = root + @"\" + (string)obj.username + @"\" + file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    if (!Directory.Exists(root + @"\" + (string)obj.username))
                    {
                        Directory.CreateDirectory(root + @"\" + (string)obj.username);
                    }

                    if (!File.Exists(rename))
                    {
                        File.Move(file.LocalFileName, rename);

                        var filename = file.Headers.ContentDisposition.FileName.Replace("\"", "");
                        db.AddFileReference(filename, DateTime.Now, rename, application_id, "", "", (string)obj.username);
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