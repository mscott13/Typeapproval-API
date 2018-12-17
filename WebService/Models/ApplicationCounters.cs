using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class ApplicationCounters
    {
        public ApplicationCounters()
        {
            licensed_applications = 0;
            pending_applications = 0;
            incomplete_applications = 0;
        }

        public int licensed_applications { get; set; }
        public int pending_applications { get; set; }
        public int incomplete_applications { get; set; }
    }
}