using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebService
{
    public class Progress : Hub
    {
        public static void ReportProgress(string progress)
        {
            try
            {
                var chat = GlobalHost.ConnectionManager.GetHubContext("Progress");
                if (chat != null)
                {
                    chat.Clients.All.reportProgress(progress);
                }
            }
            catch { }
        }
    }
}