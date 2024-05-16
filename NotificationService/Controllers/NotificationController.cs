using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost]
    public IActionResult SendNotification(Notification notification)
    {
        _notificationService.SendNotification(notification);
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAllNotifications()
    {
        var notifications = _notificationService.GetAllNotifications();
        return Ok(notifications);
    }

    [HttpGet("{id}")]
    public IActionResult GetNotificationById(string id)
    {
        var notification = _notificationService.GetNotificationById(id);
        if (notification == null)
        {
            return NotFound();
        }
        return Ok(notification);
    }

    [HttpPut("{id}/mark-as-read")]
    public IActionResult MarkNotificationAsRead(string id)
    {
        _notificationService.MarkAsRead(id);
        return Ok();
    }
}
