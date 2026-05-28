using CampusAnnouncementApp.Domain.Interfaces;

namespace CampusAnnouncementApp.Infrastructure.Notifications;

public class PushNotification : INotification
{
    public string Message { get; set; } = string.Empty;
    public string DeviceId { get; set; } = string.Empty;

    public PushNotification(string message, string deviceId)
    {
        Message = message;
        DeviceId = deviceId;
    }

    public void Send()
    {
        Console.WriteLine($"🔔 Mobil bildirim gönderildi [{DeviceId}]: {Message}");
    }
}
