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
        [HttpGet]
        public HttpResponseMessage ClientCompanyList([FromUri]string q)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            return Request.CreateResponse(HttpStatusCode.OK, db.getClientDetails(q));
        }

        [HttpGet]
        public HttpResponseMessage CheckName([FromUri]string q)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            return Request.CreateResponse(HttpStatusCode.OK, db.CheckUserExist(q));
        }

        [HttpPost]
        public HttpResponseMessage NewApplication([FromBody] string model)
        {
            return null;
        }
    }
}