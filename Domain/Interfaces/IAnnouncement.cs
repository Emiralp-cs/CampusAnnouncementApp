using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Domain.Interfaces;

public interface IAnnouncement
{
    string Title { get; set; }
    string Content { get; set; }
    DateTime CreatedAt { get; set; }
    AnnouncementType Type { get; set; }
}
