using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class RecentDocuments
    {
        public RecentDocuments(string application_id, string date, string status, string last_update)
        {
            this.application_id = application_id;
            this.date = date;
            this.status = status;
            this.last_update = last_update;
        }

        public string application_id { get; set; }
        public string date { get; set; }
        public string status { get; set; }
        public string last_update { get; set; }
    }
}