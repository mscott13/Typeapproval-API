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
                case Constants.ACCOUNT_TYPE:
                    userActivity.priority = 0;
                    break;
                case Constants.APPROVAL_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.CANCELLATION_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.NEW_APPLICATION_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.SUBMISSION_TYPE:
                    userActivity.priority = 1;
                    break;
                case Constants.UPDATE:
                    userActivity.priority = 1;
                    break;
            }

            SLW_DatabaseInfo db = new SLW_DatabaseInfo();
            db.SaveActivity(userActivity);
        }
    }
}