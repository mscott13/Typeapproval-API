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
using WebService.Other;

namespace WebService.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Multiple()
        {
          
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/TA_APPLICATIONS");
            string rename = "";

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    rename = root + @"\" + file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    if (!File.Exists(rename))
                    {
                        File.Move(file.LocalFileName, rename);
                    }
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}