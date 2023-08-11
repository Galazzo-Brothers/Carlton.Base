using System.Collections;

namespace Carlton.Core.Components.Flux.State;

public class StateMutationEvents<TState> : IEnumerable<string>
{
    public IEnumerable<string> Events { get; }

    public StateMutationEvents(IEnumerable<string> events)
    {
        Events = events;
    }

    //public static StateMutationEvents<TState> Create<TEvents>()
    //    where TEvents : Enum
    //{
    //    var parsedEvents = Enum.GetValues(typeof(TEvents))
    //                           .Cast<TEvents>()
    //                           .Select(e => e.ToString());

    //    return new StateMutationEvents<TState>(parsedEvents);
    //}

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var evt in Events)
            yield return evt;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
