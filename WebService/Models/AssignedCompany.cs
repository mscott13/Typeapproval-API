using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class AssignedCompany
    {
        public string company { get; set; }
        public int clientId { get; set; }
        public string source { get; set; }
    }
}