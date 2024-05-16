using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Application.Services;

public class NotificationService:INotificationService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;
    private const string NotificationKey = "Notifications";

    public NotificationService(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Redis");
        _redis = ConnectionMultiplexer.Connect(connectionString);
        _database = _redis.GetDatabase();
    }

    public void SendNotification(Notification notification)
    {
        notification.Id = Guid.NewGuid().ToString(); 
        notification.CreatedAt = DateTime.Now;
        notification.IsRead = false;

        string json = JsonConvert.SerializeObject(notification);
        _database.ListRightPush(NotificationKey, json);
    }

    public IEnumerable<Notification> GetAllNotifications()
    {
        var notifications = new List<Notification>();

        var jsonNotifications = _database.ListRange(NotificationKey);
        foreach (var json in jsonNotifications)
        {
            notifications.Add(JsonConvert.DeserializeObject<Notification>(json));
        }

        return notifications;
    }

    public Notification GetNotificationById(string id)
    {
        long index = BitConverter.ToInt64(Guid.Parse(id).ToByteArray(), 0);

        var json = _database.ListGetByIndex(NotificationKey, index);
        if (!json.IsNull)
        {
            return JsonConvert.DeserializeObject<Notification>(json);
        }
        return null;
    }

    public void MarkAsRead(string id)
    {
        long index = BitConverter.ToInt64(Guid.Parse(id).ToByteArray(), 0);

        var notification = GetNotificationById(id);
        if (notification != null)
        {
            notification.IsRead = true;
            string json = JsonConvert.SerializeObject(notification);
            _database.ListSetByIndex(NotificationKey, index, json);
        }
    }
}
