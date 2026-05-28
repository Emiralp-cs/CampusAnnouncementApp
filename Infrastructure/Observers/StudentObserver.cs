using CampusAnnouncementApp.Application.Factories;
using CampusAnnouncementApp.Domain.Entities;
using CampusAnnouncementApp.Domain.Interfaces;

namespace CampusAnnouncementApp.Infrastructure.Observers;

public class StudentObserver : IObserver
{
    private readonly User _user;

    public StudentObserver(User user)
    {
        _user = user;
    }

    public void Update(Announcement announcement)
    {
        Logger.GetInstance().Log($"Öğrenci [{_user.Name}] yeni duyuruyu aldı: [{announcement.Title}]");

        foreach (var notificationType in _user.GetPreferenceList())
        {
            var notification = NotificationFactory.Create(notificationType, announcement.Content, _user);
            notification.Send();
        }
    }
}
