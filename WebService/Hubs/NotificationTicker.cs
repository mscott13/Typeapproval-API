using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace WebService.Hubs
{
    public class NotificationTicker
    {
        // Singleton instance
        private readonly static Lazy<NotificationTicker> _instance = new Lazy<NotificationTicker>(
            () => new NotificationTicker(GlobalHost.ConnectionManager.GetHubContext<CrossDomainHub>().Clients));

        private Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMilliseconds(500);
        private readonly ConcurrentDictionary<string, Notification> _notifications = new ConcurrentDictionary<string, Notification>();

        private NotificationTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            LoadNotifications();
        }

        public void InitializeNotifications()
        {
            _timer = new Timer(UpdateNotifications, null, _updateInterval, _updateInterval);
            BroadcastState("started");
        }

        public static NotificationTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _notifications.Values;
        }

        private void LoadNotifications()
        {
            _notifications.Clear();
            var notifications = new List<Notification>
            {
                new Notification {count = 1},
                new Notification {count = 2}
            };

            notifications.ForEach(notification => _notifications.TryAdd("notif" + DateTime.Now.ToShortDateString(), notification));
        }

        private void UpdateNotifications(object state)
        {
            if (true)
            {
                foreach (var notification in _notifications.Values)
                {
                    BroadcastNotifications(notification);
                }
            }
        }

        private void BroadcastState(string state)
        {
            Clients.All.state(state);
        }

        private void BroadcastNotifications(Notification notification)
        {
            Clients.All.notify(notification);
        }
    }
}