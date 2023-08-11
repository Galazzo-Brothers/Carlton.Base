namespace Carlton.Core.InProcessMessaging.Notifications;

public class NotificationDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public NotificationDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task Dispatch<TNotification>(TNotification notification, CancellationToken cancellationToken)
    {
        var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();

        foreach (var handler in handlers)
        {
            if (handler == null)
                continue;

            await handler.Handle(notification, cancellationToken);
        }
    }
}

