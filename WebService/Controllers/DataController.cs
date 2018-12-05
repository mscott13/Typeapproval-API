using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebService.Models;
using WebService.Database;
using WebService.Utilities;

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

        [HttpPost]
        public HttpResponseMessage CreateApplication([FromBody] Form data)
        {
            if (data.application_id != null && data.application_id != "")
            {
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();
                db.SaveApplication(data);

                Commons.UserActivity.Record(new UserActivity(data.username, Commons.Constants.UPDATE, data.application_id, data.status));
                return Request.CreateResponse(HttpStatusCode.OK, "updated");
            }
            else
            {
                string application_id = Generator.guid();
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();

                data.application_id = application_id;
                db.SaveApplication(data);

                Commons.UserActivity.Record(new UserActivity(data.username, Commons.Constants.NEW_APPLICATION_TYPE, data.application_id, data.status));
                return Request.CreateResponse(HttpStatusCode.OK, application_id);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetUserActivities([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<UserActivity> userActivities = new List<UserActivity>();

            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                userActivities = db.GetUserActivities(detail.username);
                return Request.CreateResponse(HttpStatusCode.OK, userActivities);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetRecentDocuments([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<RecentDocuments> recentDocuments = new List<RecentDocuments>();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                recentDocuments = db.GetRecentDocuments(detail.username);
                return Request.CreateResponse(HttpStatusCode.OK, recentDocuments);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }
    }
}