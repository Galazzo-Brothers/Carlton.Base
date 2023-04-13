namespace Carlton.Base.State;

public interface IComponentEventRequest<TComponentEvent, TViewModel> : IRequest<Unit>
    where TComponentEvent : IComponentEvent<TViewModel>
{
    object Sender { get; }
    TComponentEvent ComponentEvent { get; }
}


