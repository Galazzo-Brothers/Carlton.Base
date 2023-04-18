namespace Carlton.Base.State;

public abstract class ComponentEventRequestBase<TComponentEvent, TViewModel> : IComponentEventRequest<TComponentEvent, TViewModel>
    where TComponentEvent : IComponentEvent<TViewModel>
{
    public ICarltonComponent<TViewModel> Sender { get; init; }
    public TComponentEvent ComponentEvent { get; init; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedDateTime { get; }

    public DateTime CompletedDateTime { get; private set; }

    object IComponentEventRequest.Sender => Sender;

    protected ComponentEventRequestBase(ICarltonComponent<TViewModel> sender, TComponentEvent componentEvent)
        => (Sender, ComponentEvent, CreatedDateTime) = (sender, componentEvent, DateTime.Now);


    public void MarkEventCompleted()
    {
        IsCompleted = true;
        CompletedDateTime = DateTime.Now;
    }
}




