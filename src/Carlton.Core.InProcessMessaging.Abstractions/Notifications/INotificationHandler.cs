namespace Carlton.Core.InProcessMessaging.Notifications;

public interface INotificationHandler<TNotification>
    where TNotification : INotification 
{
    public Task Handle(INotification notifications, CancellationToken cancellationToken);
}
