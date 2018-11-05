using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebService.Models;
using WebService.Database;
using System.Net;

namespace WebService.Controllers
{
    public class ApplicationController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage New([FromBody] Form form )
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail keyDetail = db.GetKeyDetail(form.access_key);

            string applicationId = string.Format("{0}_{1:N}", "ta", Guid.NewGuid());
            form.applicationId = applicationId;

            //db.SaveApplication(form);
            return Request.CreateResponse(HttpStatusCode.OK, "saved: "+applicationId); ;
        }

        [HttpPost]
        public HttpResponseMessage Update([FromBody] Form form)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail keyDetail = db.GetKeyDetail(form.access_key);

            //db.SaveApplication(form);
            return Request.CreateResponse(HttpStatusCode.OK, "updated"); ;
        }
    }
}