using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Domain.Entities;

public class LibraryAnnouncement : BaseAnnouncement
{
    public string WorkingHours { get; set; }

    public LibraryAnnouncement(string title, string content) : base(title, content)
    {
        Type = AnnouncementType.Kutuphane;
    }
}
