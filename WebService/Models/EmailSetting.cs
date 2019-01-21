using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class EmailSetting
    {
        public string email { get; set; }
        public DateTime last_accessed { get; set; }
        public string company_name { get; set; }
    }
}