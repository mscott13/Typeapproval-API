using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class NewUser
    {
        public NewUser()
        {
            username = "";
            first_name = "";
            password = "";
            user_type = -1;
            email = "";
            company = "";
        }

        public string username   { get; set; }
        public string first_name { get; set; }
        public string last_name  { get; set; }
        public string password   { get; set; }
        public int    user_type  { get; set; }
        public string email      { get; set; }
        public string company    { get; set; }
        public int clientId      { get; set; }
    }
}