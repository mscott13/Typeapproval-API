using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using WebService.Database;
using WebService.Models;
using System.Collections.Generic;

namespace WebService.Controllers
{
    public class SearchController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetTypeApprovalDetails([FromUri]String Dealer, [FromUri]String Model)
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
            return Request.CreateResponse(HttpStatusCode.Accepted, data);
        }
    }
}