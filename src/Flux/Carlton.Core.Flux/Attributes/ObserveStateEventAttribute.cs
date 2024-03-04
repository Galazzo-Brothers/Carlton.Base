namespace Carlton.Core.Flux.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventAttribute(string stateEvent) : Attribute
{
    public string StateEvent { get; } = stateEvent;
}
