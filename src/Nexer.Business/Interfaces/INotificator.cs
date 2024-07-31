
using Nexer.Business.Notifications;

namespace Nexer.Business.Interfaces
{
    public interface INotificator
    {
        bool HaveNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
