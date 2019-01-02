using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class UnassignedTask
    {
        public UnassignedTask() { }
        public UnassignedTask(string application_id, string created_date, string submitted_by)
        {
            this.application_id = application_id;
            this.created_date = created_date;
            this.submitted_by = submitted_by;
        }

        public string application_id { get; set; }
        public string created_date { get; set; }
        public string submitted_by { get; set; }
    }
}