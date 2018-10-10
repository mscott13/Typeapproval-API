using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Cors;

namespace WebService.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Register()
        {
            return null;
        }

        [HttpPost]
        public HttpResponseMessage Login()
        {
            return null;
        }
    }
}