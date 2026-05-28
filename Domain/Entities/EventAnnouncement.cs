using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Domain.Entities;

public class EventAnnouncement : BaseAnnouncement
{
    public string Location { get; set; }

    public EventAnnouncement(string title, string content) : base(title, content)
    {
        Type = AnnouncementType.Etkinlik;
    }
}
