using System.Collections.Generic;

namespace Domain.Infraestructure.Notification
{
    public interface INotification
    {
        bool HaveNotifications();
        void NotificationAdd(string message);
        List<string> ListAll();
    }
}
