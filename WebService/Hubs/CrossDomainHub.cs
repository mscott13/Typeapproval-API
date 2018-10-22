using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace WebService.Hubs
{
    public class CrossDomainHub : Hub
    {
        private readonly NotificationTicker _notificationTicker;

        public CrossDomainHub ():
            this(NotificationTicker.Instance)
        {

        }

        public CrossDomainHub(NotificationTicker notificationTicker)
        {
            _notificationTicker = notificationTicker;
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return _notificationTicker.GetNotifications();
        }

        public void Initialize()
        {
            _notificationTicker.InitializeNotifications();
        }

        public void StartTimer()
        {
            Clients.All.showTime(DateTime.UtcNow.ToLongTimeString());
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}