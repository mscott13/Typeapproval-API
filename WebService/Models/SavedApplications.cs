using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class SavedApplications
    {
        public SavedApplications(string application_id, string created_date, string last_updated)
        {
            this.application_id = application_id;
            this.created_date = created_date;
            this.last_updated = last_updated;
        }

        public string application_id { get; set; }
        public string created_date { get; set; }
        public string last_updated { get; set; }
    }
}