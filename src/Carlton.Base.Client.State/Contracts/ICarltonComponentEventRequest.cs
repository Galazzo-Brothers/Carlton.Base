namespace Carlton.Base.State;

public interface ICarltonComponentEventRequest<TComponentEvent> : IRequest<Unit>
{
    object Sender { get; }
    TComponentEvent ComponentEvent { get; }
}
