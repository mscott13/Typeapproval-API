using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class UserCredentials
    {
        public UserCredentials(string hash, bool password_reset_required, int user_type)
        {
            this.hash = hash;
            this.password_reset_required = password_reset_required;
            this.user_type = user_type;
            user_found = true;
        }

        public UserCredentials()
        {
            hash = "";
            password_reset_required = false;
            user_type = -1;
            user_found = false;
        }

        public string hash { get; set; }
        public bool password_reset_required { get; set; }
        public int user_type { get; set; }
        public bool user_found { get; }
    }
}