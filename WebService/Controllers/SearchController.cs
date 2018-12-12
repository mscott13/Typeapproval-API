using System.Net.Http;
using System.Web.Http;
using System.Net;
using WebService.Database;
using WebService.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebService.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage TypeApprovalDetails([FromUri]string Dealer="", [FromUri]string Model="")
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
        public HttpResponseMessage Manufacturers([FromUri]string q)
        {
            if(q == null)
            {
                q = "";
            }

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<string> data = db.GetManufacturersByName(q);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        public HttpResponseMessage Models([FromUri]string q)
        {
            if (q == null)
            {
                q = "";
            }

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<string> data = db.GetModels(q);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        public HttpResponseMessage AllCategories([FromUri]string q)
        {
            if (q == null)
            {
                q = "";
            }

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            return Request.CreateResponse(HttpStatusCode.OK, db.GetMultiSearchApplicationResults(q));
        }
    }
}