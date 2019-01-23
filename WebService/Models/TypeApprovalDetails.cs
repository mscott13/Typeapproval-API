using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



namespace WebService.Models
{
    public class TypeApprovalDetails
    {
        public string approval_id { get; set; }
        public string clientCompany { get; set; }
        public string Dealer { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Address2 { get; set; }
        public string Remarks { get; set; }
        public DateTime issueDate { get; set; }
        public int keyTypeApprovalID { get; set; }
        public DataTable TableInfo { get; set; }
    }
}       