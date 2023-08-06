namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventsAttribute<TStateEvents> : Attribute
 where TStateEvents : Enum
{
    public TStateEvents StateEvent { get; }

    public ObserveStateEventsAttribute(TStateEvents stateEvent)
        => StateEvent = stateEvent;
}
