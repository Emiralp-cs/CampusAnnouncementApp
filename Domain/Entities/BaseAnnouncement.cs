using CampusAnnouncementApp.Domain.Enums;
using CampusAnnouncementApp.Domain.Interfaces;

namespace CampusAnnouncementApp.Domain.Entities;

public abstract class BaseAnnouncement : IAnnouncement
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public AnnouncementType Type { get; set; }

    protected BaseAnnouncement(string title, string content)
    {
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now;
    }
}
