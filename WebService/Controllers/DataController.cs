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

        [HttpGet]
        public HttpResponseMessage ManufacturersDetail([FromUri]string q)
        {
            if (q == null)
            {
                q = "";
            }

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            List<Manufacturer> data = db.GetManufacturers(q);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [HttpPost]
        public HttpResponseMessage CreateApplication([FromBody] Form data)
        {
            if (data.application_id != null && data.application_id != "")
            {
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();
                data.category = Commons.Constants.TYPE_APPROVAL;
                data.status = Commons.Constants.INCOMPLETE_TYPE;
                db.SaveApplication(data);

                Commons.UserActivity.Record(new UserActivity(data.username, Commons.Constants.UPDATE, data.application_id, data.status));
                return Request.CreateResponse(HttpStatusCode.OK, "updated");
            }
            else
            {
                string application_id = Generator.guid();
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();

                data.application_id = application_id;
                data.category = Commons.Constants.TYPE_APPROVAL;
                data.status = Commons.Constants.INCOMPLETE_TYPE;
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

        [HttpPost]
        public HttpResponseMessage GetApplication([FromBody] dynamic data)
        {
            string access_key = (string)data.access_key;
            string application_id = (string)data.application_id;

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                Form form = db.GetApplication(application_id);
                return Request.CreateResponse(HttpStatusCode.OK, form);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetSavedApplications([FromBody] dynamic data)
        {
            string access_key = (string)data;
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                List<SavedApplications> savedApplications = db.GetSavedApplications(detail.username);
                return Request.CreateResponse(HttpStatusCode.OK, savedApplications);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }
    }
}