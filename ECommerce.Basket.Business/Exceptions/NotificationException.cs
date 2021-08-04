using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Basket.Business.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(string notification, string subject = "Uyarı") : base(notification)
        {
            Notification = notification;
            Subject = subject;
        }
        public string Notification { get; set; }
        public string Subject { get; private set; }
    }
}
