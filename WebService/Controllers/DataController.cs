﻿using System;
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
            List<ClientCompany> clientCompanies = db.GetClientDetails(q);
            List<ClientCompany> localClientCompanies = db.GetLocalClientCompanies(q);
            clientCompanies.AddRange(localClientCompanies);

            List<ClientCompany> combinedCompanies = clientCompanies.OrderBy(i => i.name).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, combinedCompanies);
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
            ClientCompany clientCompany = null;
            KeyDetail keyDetail = db.GetKeyDetail((string)data.access_key);

            if (keyDetail.data_present)
            {
                if (keyDetail.user_type == "company")
                {
                    AssignedCompany company = db.GetAssignedCompany(keyDetail.username);
                    if (company == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "empty");
                    }
                    else
                    {
                        if (company.source == Commons.Constants.LOCAL_SOURCE)
                        {
                            clientCompany = db.GetLocalClientCompany(company.clientId);
                        }
                        else
                        {
                            clientCompany = db.GetClientDetail(company.clientId);
                        }

                        return Request.CreateResponse(HttpStatusCode.OK, clientCompany);
                    }
                }
                else
                {
                    clientCompany = db.GetIndividualDetail(keyDetail.username);
                    return Request.CreateResponse(HttpStatusCode.OK, clientCompany);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "unauthorized");
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
            List<Grantee> data = db.GetManufacturers(q);
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

                db.SaveActivity(new UserActivity(data.username, Commons.Constants.ACTIVITY_UPDATE, data.application_id, "", 1));
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

                db.SaveActivity(new UserActivity(data.username, Commons.Constants.ACTIVITY_NEW_APPLICATION_TYPE, data.application_id, "", 1));
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
                userActivities = db.GetUserActivities("*preview");
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
            db.CheckForApplicationUpdatesAllUsers();
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
                db.CheckForApplicationUpdates(detail.username);
                dashboard = db.GetDashboardData(detail.username);
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
                Certificate certificate = db.GetPersonalSMACertificate((string)data.application_id);
                return Request.CreateResponse(HttpStatusCode.OK, certificate);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage GetSmaCertificate([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            Certificate certificate = db.GetSMACertificate((string)data.approval_id);
            return Request.CreateResponse(HttpStatusCode.OK, certificate);
        }

        [HttpPost]
        public HttpResponseMessage GetEngineers([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            db.CheckForApplicationUpdatesAllUsers();
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

                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.SUBMITTED_TYPE, DateTime.Now);
                UnassignedTask unassigned = db.GetSingleUnassignedTask((string)data.application_id);
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_NEW_UNASSIGNED, (string)data.application_id, "", 1));
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
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.SUBMITTED_TYPE, DateTime.Now);
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_MOVE_UNASSAIGNED, (string)data.application_id, "", 1));
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

                UserDetails userDetailsAdmin = db.GetUserDetails(detail.username);
                db.NewOngoingTask((string)data.application_id, (string)data.assigned_to, unassigned.username, (string)data.status, unassigned.created_date_raw, userDetailsAdmin.fullname, userDetailsAdmin.username);
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.PENDING_TYPE, DateTime.Now);
                OngoingTask ongoing = db.GetSingleOngoingTask((string)data.application_id);
                UserDetails userDetails = db.GetUserDetails(ongoing.assigned_to);
                ongoing.assigned_to = userDetails.fullname;
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_NEW_ONGOING, (string)data.application_id, (string)data.assigned_to, 1));
                Form form = db.GetApplication((string)data.application_id);

                Email.Send(userDetails.email, "New Type Approval Application", "Type approval application " + form.application_id + " for Device with Model number " + form.product_identification + " from " + ongoing.submitted_by + " is assigned to you for processing.");
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
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.REJECTED, DateTime.Now);
                FileManager.DeleteFiles((string)data.application_id);
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_REJECT_UNASSIGNED, (string)data.application_id, "", 1));
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
                db.UpdateApplicationStatus((string)data.application_id, Commons.Constants.REJECTED, DateTime.Now);
                FileManager.DeleteFiles((string)data.application_id);
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_REJECT_UNASSIGNED, (string)data.application_id, "", 1));
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
                OngoingTask prev_ongoing = db.GetSingleOngoingTask((string)data.application_id);
                db.ReassignTask((string)data.application_id, (string)data.assign_to);
                OngoingTask ongoing = db.GetSingleOngoingTask((string)data.application_id);
                UserDetails prevUserDetails = db.GetUserDetails(prev_ongoing.assigned_to);
                UserDetails userDetails = db.GetUserDetails(ongoing.assigned_to);
                
                Form form = db.GetApplication((string)data.application_id);
                ongoing.assigned_to = userDetails.fullname;
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_REASSIGN_TASK, (string)data.application_id, (string)data.assign_to, 1));

                Email.Send(userDetails.email, "New Type Approval Application", "Type approval application "+form.application_id+" for Device with Model number "+form.product_identification+" from "+ ongoing.submitted_by + " is assigned to you for processing.");
                Email.Send(prevUserDetails.email, "Assigned Type Approval Application", "Application "+form.application_id+" that had been assigned to you has been reassigned.");

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

        [HttpPost]
        public HttpResponseMessage NewEmailSetting([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail detail = db.GetKeyDetail((string)data.access_key);

            if (detail.data_present)
            {
                //do additional checks here to confirm if this is a sys admin
                db.NewEmailSetting((string)data.email, (string)data.company_name);
                db.SaveActivity(new UserActivity(detail.username, Commons.Constants.ACTIVITY_SET_EMAIL, (string)data.email, "", 1));

                Email.Send((string)data.email, "Email Verification", "Email is now configured...");
                return Request.CreateResponse(HttpStatusCode.OK, "email_saved");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }   
        }

        [HttpPost]
        public HttpResponseMessage NewCompany([FromBody] ClientCompany data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            try
            {
                int client_id = db.NewLocalClientCompany(data);
                return Request.CreateResponse(HttpStatusCode.OK, client_id);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
            }
        }

        [HttpPost]
        public HttpResponseMessage NewGrantee([FromBody] Grantee data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            try
            {
                Grantee grantee = db.NewLocalGrantee(data);
                return Request.CreateResponse(HttpStatusCode.OK, grantee);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
            }
        }
    }
}