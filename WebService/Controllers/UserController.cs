using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;
using System.Net;
using WebService.Database;
using WebService.Models;

namespace WebService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Register([FromBody]Models.NewUser user)
        {
            return null;
        }

        [HttpPost]
        public HttpResponseMessage Login([FromBody]Models.Login login)
        {
            Utilities.PasswordManager mgr = new Utilities.PasswordManager();
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();

            if (db.CheckUserExist(login.username))
            {
                UserCredentials credentials = db.GetUserCredentials(login.username);
                bool passed = mgr.VerifyCredentials(login.password, credentials.hash);

                if (passed)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new Models.LoginResult("credentials verified", mgr.GenerateNewAccessKey(login.username)));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, new Models.LoginResult("incorrect credentials", ""));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new LoginResult("invalid user", ""));
            }
        }

        [HttpPost]
        public HttpResponseMessage Delete([FromBody]String access)
        {
            int user_type_requirement = 2;
            Utilities.PasswordManager mgr = new Utilities.PasswordManager();
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();

            if (db.GetUserType(access) == user_type_requirement)
            {
                //delete user
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "unauthorized");
            }
            return null;
        }

        [HttpGet]
        public HttpResponseMessage test([FromUri]string s)
        {
            Utilities.PasswordManager mgr = new Utilities.PasswordManager();
            return Request.CreateResponse(HttpStatusCode.OK, mgr.GenerateNewAccessKey("mscott"));
        }
    }
}