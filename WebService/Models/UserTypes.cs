using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class UserType
    {
        public UserType(int user_type, string description)
        {
            this.user_type = user_type;
            this.description = description;
        }

        public int user_type { get; set; }
        public string description { get; set; }
    }
}