using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class OngoingTask
    {
        public OngoingTask(string application_id, string created_date, string assigned_to, string date_assigned, string status)
        {
            this.application_id = application_id;
            this.created_date = created_date;
            this.assigned_to = assigned_to;
            this.date_assigned = date_assigned;
            this.status = status;
        }

        public string application_id { get; set; }
        public string created_date { get; set; }
        public string assigned_to { get; set; }
        public string date_assigned { get; set; }
        public string status { get; set; }
    }
}