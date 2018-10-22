using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Message
    {
        public string access_key { get; set; }
        public string target_user { get; set; }
        public string message { get; set; }
    }
}