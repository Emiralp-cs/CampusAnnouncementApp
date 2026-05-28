using CampusAnnouncementApp.Domain.Enums;

namespace CampusAnnouncementApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public string NotificationPreferences { get; set; } = string.Empty;

    public List<NotificationType> GetPreferenceList()
    {
        if (string.IsNullOrWhiteSpace(NotificationPreferences))
            return new List<NotificationType>();

        return NotificationPreferences
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(p => Enum.Parse<NotificationType>(p.Trim()))
            .ToList();
    }
}
