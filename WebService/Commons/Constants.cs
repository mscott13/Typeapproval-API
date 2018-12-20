using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Commons
{
    public static class Constants
    {
        //@"Data Source=SMA-DBSRV\ASMSDEV;Initial Catalog=SLW_Database;Integrated Security=True";
        //@"Data Source=DESKTOP-6DGAJN8\SQLEXPRESS;Initial Catalog=SLW_Database;Integrated Security=True"
        //@"Data Source=DESKTOP-E9VTQUL\SQLEXPRESS;Initial Catalog=SLW_Database;Integrated Security=True"

        public const string databaseConnection = @"Data Source=DESKTOP-E9VTQUL\SQLEXPRESS;Initial Catalog=SLW_Database;Integrated Security=True";
        public const string notifyTypeApproval = "TYPE_APPROVAL";
        public const string notifyGeneral = "GENERAL";

        //user activity types
        public const string ACCOUNT_TYPE = "Account";
        public const string APPROVAL_TYPE = "Approval";
        public const string CANCELLATION_TYPE = "Cancellation";
        public const string NEW_APPLICATION_TYPE = "New Application";
        public const string SUBMISSION_TYPE = "Submission";
        public const string UPDATE = "Update";

        //application categories
        public const string TYPE_APPROVAL = "TYPE_APPROVAL";
        public const string MARINE = "MARINE";
        public const string MICROWAVE = "MICROWAVE";
        public const string PRIVATE_RADIO = "PRIVATE_RADIO";

        //application status types
        public const string INCOMPLETE_TYPE = "INCOMPLETE";
        public const string SUBMITTED_TYPE = "SUBMITTED";
        public const string PENDING_TYPE = "PENDING";
        public const string INVOICED_TYPE = "INVOICED";
        public const string LICENSED_TYPE = "LICENSED";
        public const string REJECTED = "REJECTED";

    }
}

