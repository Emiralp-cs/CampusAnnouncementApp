using CampusAnnouncementApp.Domain.Interfaces;

namespace CampusAnnouncementApp.Infrastructure.Notifications;

public class SmsNotification : INotification
{
    public string Message { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public SmsNotification(string message, string phoneNumber)
    {
        Message = message;
        PhoneNumber = phoneNumber;
    }

    public void Send()
    {
        Console.WriteLine($"📱 SMS gönderildi [{PhoneNumber}]: {Message}");
    }
}
