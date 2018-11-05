using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebService.Hubs
{
    public class CrossDomainHub : Hub
    {
        private readonly NotificationTicker _notificationTicker;

        public CrossDomainHub ():
            this(NotificationTicker.Instance)
        {}

        public CrossDomainHub(NotificationTicker notificationTicker)
        { 
            _notificationTicker = notificationTicker;
        }

        public IEnumerable<Notification> GetAllNotifications()
        {
            return _notificationTicker.GetNotifications();
        }

        public void Initialize(string username)
        {
            SignalRUsers signalRUsers = new SignalRUsers();
            signalRUsers.srClientId = Context.ConnectionId;
            signalRUsers.username = username;

            _notificationTicker.InitializeNotifications(signalRUsers);
        }

        public void StartTimer()
        {
            Clients.All.showTime(DateTime.UtcNow.ToLongTimeString());
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }
             
        public override Task OnDisconnected(bool stopCalled)
        {
            _notificationTicker.BroadcastCurrentClients();
            string name = Context.User.Identity.Name;
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            return base.OnReconnected();
        }
    }
}