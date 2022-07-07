using Business.Notifications;

namespace Business.Interfaces
{
    public interface INotifier
    {
        bool HaveNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
