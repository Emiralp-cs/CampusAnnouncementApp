namespace CampusAnnouncementApp.Domain.Interfaces;

public interface INotification
{
    string Message { get; set; }
    void Send();
}
