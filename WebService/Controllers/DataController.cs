using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebService.Models;
using WebService.Database;


namespace WebService.Controllers
{
    public class DataController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage ClientCompanyList([FromBody]dynamic obj)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            int clientId = obj.clientId;
            string access_key = obj.access_key;
            return Request.CreateResponse(HttpStatusCode.OK, db.getClientDetails(clientId));
        }
    }
}