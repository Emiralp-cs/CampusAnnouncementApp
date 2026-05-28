using CampusAnnouncementApp.Domain.Entities;
using CampusAnnouncementApp.Domain.Enums;
using CampusAnnouncementApp.Domain.Interfaces;
using CampusAnnouncementApp.Infrastructure.Notifications;

namespace CampusAnnouncementApp.Application.Factories;

public static class NotificationFactory
{
    public static INotification Create(NotificationType type, string message, User user)
    {
        return type switch
        {
            NotificationType.Email         => new EmailNotification(message, user.Email),
            NotificationType.SMS           => new SmsNotification(message, user.Email),
            NotificationType.MobilBildirim => new PushNotification(message, user.Name),
            _ => throw new ArgumentException($"Geçersiz bildirim tipi: {type}")
        };
    }
}
