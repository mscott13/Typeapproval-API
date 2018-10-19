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

namespace WebService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UploadController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Multiple()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            string rename = "";
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (MultipartFileData file in provider.FileData)
                {
                    rename = root + @"\" + file.Headers.ContentDisposition.FileName.Replace("\"", "");
                    File.Move(file.LocalFileName, rename);
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