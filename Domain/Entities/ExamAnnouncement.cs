using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Domain.Entities;

public class ExamAnnouncement : BaseAnnouncement
{
    public string CourseName { get; set; }

    public ExamAnnouncement(string title, string content) : base(title, content)
    {
        Type = AnnouncementType.Sinav;
    }
}
