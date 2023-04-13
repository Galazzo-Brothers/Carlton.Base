namespace Carlton.Base.State;

public interface IComponentEvent<TViewModel>
{
    public ICarltonComponent<TViewModel> Sender { get; }
    public bool IsCompleted { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime CompletedDateTime { get; }
    public void MarkEventCompleted();
}

