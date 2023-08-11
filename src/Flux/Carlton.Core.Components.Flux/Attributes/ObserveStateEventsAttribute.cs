namespace Carlton.Core.Components.Flux.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventsAttribute : Attribute
{
    public string StateEvent { get; }

    public ObserveStateEventsAttribute(string stateEvent)
        => StateEvent = stateEvent;
}
