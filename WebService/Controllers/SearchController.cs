using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using WebService.Database;
using WebService.Models;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SearchController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage TypeApprovalDetails([FromUri]String Dealer, [FromUri]String Model)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            if (Dealer == null)
            {
                Dealer = "";
            }
            if (Model == null) 
            {
                Model = "";
            }

            List<TypeApprovalDetails> data = db.GetTypeApprovalInfo(Dealer, Model);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        public HttpResponseMessage Manufacturers([FromUri]String q)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<string> data = db.GetManufacturers(q);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        public HttpResponseMessage Models([FromUri]String q)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<string> data = db.GetModels(q);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}