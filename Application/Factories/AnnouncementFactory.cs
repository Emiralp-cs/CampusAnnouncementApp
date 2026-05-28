using CampusAnnouncementApp.Domain.Entities;
using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Application.Factories;

public static class AnnouncementFactory
{
    public static Announcement Create(AnnouncementType type, string title, string content)
    {
        return type switch
        {
            AnnouncementType.Sinav or
            AnnouncementType.Etkinlik or
            AnnouncementType.Yemekhane or
            AnnouncementType.Kutuphane => new Announcement
            {
                Title = title,
                Content = content,
                Type = type,
                CreatedAt = DateTime.Now
            },
            _ => throw new ArgumentException($"Geçersiz duyuru tipi: {type}")
        };
    }
}
