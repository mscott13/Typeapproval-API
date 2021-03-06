﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService.Database;
using WebService.Models;
using WebService.Commons;
using System;
using System.Threading.Tasks;
namespace WebService.Controllers
{
    public class NotifyController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage ForTypeApprovals([FromBody]Message message)
        {
            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            db.NotifySpecific(message.message, message.target_user, Constants.notifyTypeApproval);
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        public HttpResponseMessage ClientGeneral([FromBody]Message message)
        {
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        public HttpResponseMessage All([FromBody]Message message)
        {

            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

        [HttpPost]
        public HttpResponseMessage SendEmail()
        {
            Utilities.Email.Send("info@sma.gov.jm", "Testing Subject", "Using postmark is quite nice");
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }
    }
}