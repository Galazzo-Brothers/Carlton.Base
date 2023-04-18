
namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventsAttribute<TStateEvents> : Attribute
 where TStateEvents : Enum
{
    public IEnumerable<TStateEvents> StateEventes { get; }

    public ObserveStateEventsAttribute(params TStateEvents[] stateEvents)
        => StateEventes = stateEvents;
}
