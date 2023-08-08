namespace Carlton.Core.Components.Flux;
public interface IStateStore<TStateEvents>
    where TStateEvents : Enum
{
    event Func<object, TStateEvents, Task> StateChanged;
}

public interface IStateStoreEventDispatcher<TStateEvents>
{
    Task InvokeStateChanged(object sender, TStateEvents evt);
}


