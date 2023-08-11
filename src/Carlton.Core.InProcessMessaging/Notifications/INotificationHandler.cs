namespace Carlton.Core.InProcessMessaging.Notifications;

public interface INotificationHandler<TNotification>
{
    public Task Handle(TNotification notification, CancellationToken cancellationToken);
}
