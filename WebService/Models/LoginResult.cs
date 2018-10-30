using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class LoginResult
    {
        public LoginResult(string status, string access_key, int user_type, string name)
        {
            this.status = status;
            this.access_key = access_key;
            this.user_type = user_type;
            this.name = name;
        }

        public string status { get; set; }
        public string access_key { get; set; }
        public int user_type { get; set; }
        public string name { get; set; }
    }
}