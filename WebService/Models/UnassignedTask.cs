using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class UnassignedTask
    {
        public UnassignedTask() { }
        public UnassignedTask(string application_id, string created_date, DateTime created_date_raw, string submitted_by, string username)
        {
            this.application_id = application_id;
            this.created_date = created_date;
            this.submitted_by = submitted_by;
            this.username = username;
            this.created_date_raw = created_date_raw;
        }

        public string application_id { get; set; }
        public string created_date { get; set; }
        public DateTime created_date_raw { get; set; }
        public string submitted_by { get; set; }
        public string username { get; set; }
    }
}