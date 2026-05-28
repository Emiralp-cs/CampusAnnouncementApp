using CampusAnnouncementApp.Domain.Entities;

namespace CampusAnnouncementApp.Domain.Interfaces;

public interface IObserver
{
    void Update(Announcement announcement);
}
