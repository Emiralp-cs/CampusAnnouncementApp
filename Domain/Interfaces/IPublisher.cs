using CampusAnnouncementApp.Domain.Entities;

namespace CampusAnnouncementApp.Domain.Interfaces;

public interface IPublisher
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void Notify(Announcement announcement);
}
