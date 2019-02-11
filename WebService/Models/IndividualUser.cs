using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class IndividualUser
    {
        public IndividualUser()
        {
            username = "";
            first_name = "";
            password = "";
            user_role = -1;
            email = "";
        }

        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int user_role { get; set; }
        public string user_type { get; set; }
        public string access_key { get; set; }
        public bool send_credentials { get; set; }
    }
}