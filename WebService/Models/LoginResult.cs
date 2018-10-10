using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class LoginResult
    {
        public LoginResult(string status, string access_key)
        {
            this.status = status;
            this.access_key = access_key;
        }

        public string status { get; set; }
        public string access_key { get; set; }
    }
}