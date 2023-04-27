
namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventsAttribute<TStateEvents> : Attribute
 where TStateEvents : Enum
{
    public IEnumerable<TStateEvents> StateEvents { get; }

    public ObserveStateEventsAttribute(params TStateEvents[] stateEvents)
        => StateEvents = stateEvents;
}
