using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebService.Utilities
{
    public static class Generator
    {
        public static string guid()
        {
            return Guid.NewGuid().ToString();
        }

        public static string application_id()
        {
            Database.SLW_DatabaseInfo db = new Database.SLW_DatabaseInfo();
            return db.GenerateAppID();
        }

        public static string GeneratePassword()
        {
           return Membership.GeneratePassword(8, 3);
        }
    }
}