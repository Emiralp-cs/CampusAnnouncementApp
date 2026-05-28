using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Domain.Entities;

public class FoodMenuAnnouncement : BaseAnnouncement
{
    public string MenuItems { get; set; }

    public FoodMenuAnnouncement(string title, string content) : base(title, content)
    {
        Type = AnnouncementType.Yemekhane;
    }
}
