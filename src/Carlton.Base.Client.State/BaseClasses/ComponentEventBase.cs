namespace Carlton.Base.State;

public record ComponentEventBase<TViewModel> : IComponentEvent<TViewModel>
{
    public ICarltonComponent<TViewModel> Sender { get; private set; }

    public bool IsCompleted { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime CompletedDateTime { get; private set; }

    public ComponentEventBase(ICarltonComponent<TViewModel> sender)
    {
        CreatedDateTime = DateTime.Now;
        Sender = sender;
    }

    public void MarkEventCompleted()
    {
        IsCompleted = true;
        CompletedDateTime = DateTime.Now;
    }
}
