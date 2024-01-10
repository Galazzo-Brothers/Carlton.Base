namespace Carlton.Core.Flux.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventsAttribute(string stateEvent) : Attribute
{
    public string StateEvent { get; } = stateEvent;
}
