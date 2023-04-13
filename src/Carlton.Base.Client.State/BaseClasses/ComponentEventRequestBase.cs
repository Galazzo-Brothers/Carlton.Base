namespace Carlton.Base.State;

public class ComponentEventRequestBase<TComponentEvent, TViewModel> : IComponentEventRequest<TComponentEvent, TViewModel>
    where TComponentEvent : IComponentEvent<TViewModel>
{
    public object Sender { get; init; }
    public TComponentEvent ComponentEvent { get; init; }

    public ComponentEventRequestBase(object sender, TComponentEvent componentEvent)
        => (Sender, ComponentEvent) = (sender, componentEvent);
}




