using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Database;
using WebService.Hubs;
using WebService.Models;

namespace WebService.Controllers
{

    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetUsersList([FromBody] string access_key)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail key_detail = db.GetKeyDetail(access_key);

            if (IsKeyValid(key_detail))
            {
                List<UserDetails> userDetails = db.GetAllUsersDetails();
                return Request.CreateResponse(HttpStatusCode.OK, userDetails);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid_key");
            }
        }

        [HttpPost]
        public HttpResponseMessage Register([FromBody]Models.NewUser user)
        {
            if (user != null)
            {
                //can be used to call client functions
                var connection = GlobalHost.ConnectionManager.GetHubContext<CrossDomainHub>();

                Utilities.PasswordManager mgr = new Utilities.PasswordManager();
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();
                bool valid_user_type = false;

                List<UserType> user_types = db.GetUserTypes();
                for (int i = 0; i < user_types.Count; i++)
                {
                    if (user_types[i].user_type == user.user_type)
                    {
                        valid_user_type = true;
                    }
                }

                if (valid_user_type)
                {
                    if (!db.CheckUserExist(user.username))
                    {
                        string hash = mgr.GetHash(user.password);
                        db.NewUser(user.username, user.first_name, user.last_name, DateTime.Now, user.user_type, DateTime.Now, (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue, hash, false, user.email, user.company, user.clientId);
                        db.SaveActivity(new UserActivity(user.username, Commons.Constants.ACTIVITY_CREATE_ACCOUNT, "", "", 0));
                        return Request.CreateResponse(HttpStatusCode.OK, "user created");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "user exists");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid user type");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "bad request");
            }
        }

        [HttpPost]
        public HttpResponseMessage RegisterV2([FromBody]Models.NewUser user)
        {
            if (user != null)
            {
                SLW_DatabaseInfo db = new SLW_DatabaseInfo();
                KeyDetail detail = db.GetKeyDetail(user.access_key);

                if (detail.data_present)
                {
                    //can be used to call client functions
                    var connection = GlobalHost.ConnectionManager.GetHubContext<CrossDomainHub>();

                    Utilities.PasswordManager mgr = new Utilities.PasswordManager();
                    bool valid_user_type = false;

                    List<UserType> user_types = db.GetUserTypes();
                    for (int i = 0; i < user_types.Count; i++)
                    {
                        if (user_types[i].user_type == user.user_type)
                        {
                            valid_user_type = true;
                        }
                    }

                    if (valid_user_type)
                    {
                        if (!db.CheckUserExist(user.username))
                        {
                            string hash = mgr.GetHash(user.password);
                            db.NewUser(user.username, user.first_name, user.last_name, DateTime.Now, user.user_type, DateTime.Now, (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue, hash, false, user.email, user.company, user.clientId);
                            db.SaveActivity(new UserActivity(user.username, Commons.Constants.ACTIVITY_CREATE_ACCOUNT, "", "", 0));
                            return Request.CreateResponse(HttpStatusCode.OK, db.GetUserDetails(user.username));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, "user exists");
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid user type");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "bad request");
            }
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody]Models.Login login)
        {
            Utilities.PasswordManager mgr = new Utilities.PasswordManager();
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();

            if (login != null)
            {

                if (db.CheckUserExist(login.username))
                {
                    UserCredentials credentials = db.GetUserCredentials(login.username);
                    bool passed = mgr.VerifyCredentials(login.password, credentials.hash);

                    if (passed)
                    {
                        string access_key = mgr.GenerateNewAccessKey(login.username);
                        db.SetNewAccessKey(login.username, access_key);
                        db.SaveActivity(new UserActivity(login.username, Commons.Constants.ACTIVITY_LOGIN, "login successful", "", 1));
                        return Request.CreateResponse(HttpStatusCode.OK, new Models.LoginResult("credentials verified", access_key, credentials.user_type, credentials.name, login.username));
                    }
                    else
                    {
                        db.SaveActivity(new UserActivity(login.username, Commons.Constants.ACTIVITY_LOGIN, "incorrect credentials. login failed", "", 1));
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, new Models.LoginResult("incorrect credentials", "", -1, "", login.username));
                    }
                }
                else
                {
                    db.SaveActivity(new UserActivity(login.username, Commons.Constants.ACTIVITY_LOGIN, "invalid user", "", 1));
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new LoginResult("invalid user", "", -1, "", login.username));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new LoginResult("bad request", "", -1, "", login.username));
            }
        }

        [HttpPost]
        public HttpResponseMessage ChangePassword([FromBody] dynamic data)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail key_detail = db.GetKeyDetail((string)data.access_key);

            if (IsKeyValid(key_detail))
            {
                Utilities.PasswordManager psw_manager = new Utilities.PasswordManager();
                if (psw_manager.ChangePassword((string)data.username, (string)data.old_psw, (string)data.new_psw))
                {
                    db.SaveActivity(new UserActivity((string)data.username, Commons.Constants.ACTIVITY_CHANGE_PASSWORD, "", "", 0));
                    return Request.CreateResponse(HttpStatusCode.OK, "password_updated");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "password_incorrect");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid key");
            }
        }

        [HttpPost]
        public HttpResponseMessage Delete([FromBody]Delete delete)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            KeyDetail key_detail = db.GetKeyDetail(delete.key);

            if (IsKeyValid(key_detail))
            {
                int user_type_requirement = 9;
                Utilities.PasswordManager mgr = new Utilities.PasswordManager();

                if (db.GetUserType(delete.key) == user_type_requirement)
                {
                    if (db.CheckUserExist(delete.user))
                    {
                        db.DeleteUser(delete.user);
                        db.SaveActivity(new UserActivity(delete.user, Commons.Constants.ACTIVITY_DELETE_ACCOUNT, delete.user, "", 0));
                        return Request.CreateResponse(HttpStatusCode.OK, "user_deleted");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid_user");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "unauthorized");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "access_key_invalid");
            }
        }

        private bool IsKeyValid(KeyDetail key_detail)
        {
            var key_expiry = key_detail.last_detected_activity.AddMinutes(key_detail.max_inactivity_length);
            if (key_expiry >= DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}