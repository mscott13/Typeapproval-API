using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using WebService.Database;

namespace WebService.Commons
{
    public class UserActivity
    {
        public static void Record(Models.UserActivity userActivity)
        {
            switch (userActivity.type)
            {
                case Constants.ACTIVITY_ACCOUNT_TYPE:
                    userActivity.priority = 0;
                    break;
                case Constants.ACTIVITY_APPROVAL_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.ACTIVITY_CANCELLATION_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.ACTIVITY_NEW_APPLICATION_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.ACTIVITY_SUBMISSION_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.ACTIVITY_UPDATE:
                    userActivity.priority = 1;
                    break;
            }

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            db.SaveActivity(userActivity);
        }
    }
}