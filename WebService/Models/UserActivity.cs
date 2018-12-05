using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class UserActivity
    {
        public UserActivity(string username, string type, string description, string extras)
        {
            this.username = username;
            this.type = type;
            this.description = description;
            this.extras = extras;
        }

        public UserActivity(string username, string type, string description, string extras, int priority)
        {
            this.username = username;
            this.type = type;
            this.description = description;
            this.extras = extras;
            this.priority = priority;
        }

        public UserActivity(string username, string type, string description, string extras, int priority, string date)
        {
            this.username = username;
            this.type = type;
            this.description = description;
            this.extras = extras;
            this.priority = priority;
            this.date = date;
        }

        public string username { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string extras { get; set; }
        public int priority { get; set; }
        public string date { get; set; }
    }
}