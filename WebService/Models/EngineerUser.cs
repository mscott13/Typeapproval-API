using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class EngineerUser
    {
        public EngineerUser(string username, string name, string email)
        {
            this.username = username;
            this.name = name;
            this.email = email;
        }

        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }
}