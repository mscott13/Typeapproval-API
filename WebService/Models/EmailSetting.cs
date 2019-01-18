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
        public string password { get; set; }
        public int port { get; set; }
        public bool use_ssl { get; set; }
        public string host { get; set; }
    }
}