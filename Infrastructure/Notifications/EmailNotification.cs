using CampusAnnouncementApp.Domain.Interfaces;

namespace CampusAnnouncementApp.Infrastructure.Notifications;

public class EmailNotification : INotification
{
    public string Message { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public EmailNotification(string message, string email)
    {
        Message = message;
        Email = email;
    }

    public void Send()
    {
        Console.WriteLine($"📧 E-posta gönderildi [{Email}]: {Message}");
    }
}
