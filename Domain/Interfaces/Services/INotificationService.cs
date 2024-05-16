using Domain.Entities;

namespace Domain.Interfaces.Services;
public interface INotificationService
{
    void SendNotification(Notification notification);
    IEnumerable<Notification> GetAllNotifications();
    Notification GetNotificationById(string id);
    void MarkAsRead(string id);
}
