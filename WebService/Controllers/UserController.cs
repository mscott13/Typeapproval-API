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
        public HttpResponseMessage Register([FromBody]Models.NewUser user)
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
                    db.NewUser(user.username, user.first_name, user.last_name, DateTime.Now, user.user_type, DateTime.Now, (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue, hash, false, user.email, user.company);
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
                        return Request.CreateResponse(HttpStatusCode.OK, new Models.LoginResult("credentials verified", access_key, credentials.user_type, credentials.name));
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, new Models.LoginResult("incorrect credentials", "", -1, ""));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new LoginResult("invalid user", "", -1, ""));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new LoginResult("bad request", "", -1, ""));
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
                        return Request.CreateResponse(HttpStatusCode.OK, "user deleted");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, "invalid user");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "unauthorized");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "access key invalid");
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