using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Other
{
    public static class Constants
    {
        //@"Data Source=SMA-DBSRV\ASMSDEV;Initial Catalog=SLW_Database;Integrated Security=True";
        //@"Data Source=DESKTOP-6DGAJN8\SQLEXPRESS;Initial Catalog=SLW_Database;Integrated Security=True"
        //Data Source=DESKTOP-P8K7C55;Initial Catalog=SLW_Database;Integrated Security=True
        public const string databaseConnection = @"Data Source=DESKTOP-6DGAJN8\SQLEXPRESS;Initial Catalog=SLW_Database;Integrated Security=True";
        public const string notifyTypeApproval = "TYPE_APPROVAL";
        public const string notifyGeneral = "GENERAL";
    }
}

