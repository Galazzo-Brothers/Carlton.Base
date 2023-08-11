namespace Carlton.Core.InProcessMessaging.Notifications;

public interface INotificationDispatcher
{
    public Task Dispatch(INotification notification, CancellationToken cancellationToken);
}