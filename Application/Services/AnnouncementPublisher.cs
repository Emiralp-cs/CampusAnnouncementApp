using CampusAnnouncementApp.Domain.Entities;
using CampusAnnouncementApp.Domain.Interfaces;
using CampusAnnouncementApp.Infrastructure;

namespace CampusAnnouncementApp.Application.Services;

public class AnnouncementPublisher : IPublisher
{
    private readonly List<IObserver> _observers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify(Announcement announcement)
    {
        Logger.GetInstance().Log($"Duyuru yayınlandı: [{announcement.Title}]");

        foreach (var observer in _observers)
            observer.Update(announcement);
    }
}
