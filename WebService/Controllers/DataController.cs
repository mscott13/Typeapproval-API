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
            return Request.CreateResponse(HttpStatusCode.OK, db.GetClientDetails(q));
        }

        [HttpGet]
        public HttpResponseMessage CheckName([FromUri]string q)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            return Request.CreateResponse(HttpStatusCode.OK, db.CheckUserExist(q));
        }

        [HttpPost]
        public HttpResponseMessage ApplicantInfo([FromBody]dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            AssignedCompany company = db.GetAssignedCompany(db.GetKeyDetail((string)data.access_key).username);

            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "empty");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.GetClientDetail(company.clientId));
            }
            
        }
    }
}