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
                string application_id = Generator.application_id();
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
            string mode = (string)data.mode;

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                Form form = db.GetApplication(application_id);
                if (mode == "preview")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, form);
                }
                else if (mode == "edit")
                {
                    string status = (string)data.status;
                    if (status == form.status)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, form);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "unauthorized");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "unauthroized");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetApplicationStatus([FromBody] dynamic data)
        {
            string access_key = (string)data.access_key;
            string application_id = (string)data.application_id;

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.GetApplicationStatus(application_id));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
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

        [HttpPost]
        public HttpResponseMessage GetDashboardFeed([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            Dashboard dashboard = new Dashboard();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                dashboard = db.GetDashboardData(detail.username);
                db.CheckForApplicationUpdates(detail.username);
                return Request.CreateResponse(HttpStatusCode.OK, dashboard);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetCertificate([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            Dashboard dashboard = new Dashboard();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                Certificate certificate = db.GetCertificate((string)data.application_id);
                return Request.CreateResponse(HttpStatusCode.OK, certificate);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetEngineers([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                List<EngineerUser> engineers = db.GetEngineerUsers();
                return Request.CreateResponse(HttpStatusCode.OK, engineers);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetOngoingTasks([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                db.CheckForApplicationUpdatesAllUsers();
                List<OngoingTask> ongoingTasks = db.GetOngoingTasks();
                return Request.CreateResponse(HttpStatusCode.OK, ongoingTasks);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetUnassignedTask([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail(access_key);

            if (detail.data_present)
            {
                db.CheckForApplicationUpdatesAllUsers();
                List<UnassignedTask> unassignedTasks = db.GetUnassignedTasks();
                return Request.CreateResponse(HttpStatusCode.OK, unassignedTasks);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage NewUnassignedTask([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                OngoingTask ongoing = db.GetSingleOngoingTask((string)data.application_id);
                db.DeleteOngoingTask((string)data.application_id);
                db.NewUnassignedTask((string)data.application_id, (string)data.submitted_by, ongoing.created_date_raw);

                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.SUBMITTED_TYPE);
                UnassignedTask unassigned = db.GetSingleUnassignedTask((string)data.application_id);
                return Request.CreateResponse(HttpStatusCode.OK, unassigned);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage MoveToUnassigned([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                OngoingTask ongoing = db.GetSingleOngoingTask((string)data.application_id);
                db.DeleteOngoingTask((string)data.application_id);
                db.NewUnassignedTask(ongoing.application_id, ongoing.submitted_by_username, ongoing.created_date_raw);
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.SUBMITTED_TYPE);
                return Request.CreateResponse(HttpStatusCode.OK, new UnassignedTask(ongoing.application_id, ongoing.created_date, ongoing.created_date_raw, ongoing.submitted_by, ongoing.submitted_by_username));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage NewOngoingTask([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                UnassignedTask unassigned = db.GetSingleUnassignedTask((string)data.application_id);
                db.DeleteUnassignedTask((string)data.application_id);

                db.NewOngoingTask((string)data.application_id, (string)data.assigned_to, unassigned.username, (string)data.status, unassigned.created_date_raw);
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.PENDING_TYPE);
                OngoingTask ongoing = db.GetSingleOngoingTask((string)data.application_id);
                ongoing.assigned_to = db.GetUserDetails(ongoing.assigned_to).fullname;
                return Request.CreateResponse(HttpStatusCode.OK, ongoing);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }


        [HttpPost]
        public HttpResponseMessage DeleteUnassignedTask([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                db.DeleteUnassignedTask((string)data.application_id);
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.REJECTED);
                FileManager.DeleteFiles((string)data.application_id);
                return Request.CreateResponse(HttpStatusCode.OK, "deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteOngoingTask([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                db.DeleteOngoingTask((string)data.application_id);
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.REJECTED);
                FileManager.DeleteFiles((string)data.application_id);
                return Request.CreateResponse(HttpStatusCode.OK, "deleted");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage ReassignTask([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                db.ReassignTask((string)data.application_id, (string)data.assign_to);
                OngoingTask ongoing = db.GetSingleOngoingTask((string)data.application_id);
                ongoing.assigned_to = db.GetUserDetails(ongoing.assigned_to).fullname;
                return Request.CreateResponse(HttpStatusCode.OK, ongoing);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetStaffAssignedTasks([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                db.CheckForApplicationUpdatesAllUsers();
                List<AssignedTask> assignedTasks = db.GetStaffAssignedTasks((string)data.username);
                for(int i=0; i<assignedTasks.Count; i++)
                {
                    assignedTasks[i].applicationFiles = db.GetApplicationFiles(assignedTasks[i].application_id);
                }
                return Request.CreateResponse(HttpStatusCode.OK, assignedTasks);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetApplicationFileCategories([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.GetApplicationFileCategories((string)data.application_id));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetClientUsers([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.GetClientUsers());
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetApplicationIDsForClient([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.GetApplicationIDsForUser((string)data.username));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage SendEmail([FromBody] string message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}