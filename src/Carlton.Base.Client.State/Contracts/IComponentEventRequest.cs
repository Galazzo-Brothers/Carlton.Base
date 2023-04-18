namespace Carlton.Base.State;

public interface IComponentEventRequest<TComponentEvent, TViewModel> : IComponentEventRequest
    where TComponentEvent : IComponentEvent<TViewModel>
{
    public new ICarltonComponent<TViewModel> Sender { get; }
    public TComponentEvent ComponentEvent { get; }
}

public interface IComponentEventRequest : IRequest<Unit>
{
    public object Sender { get; }
    public bool IsCompleted { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime CompletedDateTime { get; }
    public void MarkEventCompleted();
}


