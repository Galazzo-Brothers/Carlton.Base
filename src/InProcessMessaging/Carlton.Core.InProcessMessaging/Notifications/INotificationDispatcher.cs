namespace Carlton.Core.InProcessMessaging.Notifications;

public interface INotificationDispatcher
{
    public Task Dispatch<TNotification>(TNotification notification, CancellationToken cancellationToken);
}