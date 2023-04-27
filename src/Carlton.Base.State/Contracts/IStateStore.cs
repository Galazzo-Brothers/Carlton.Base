namespace Carlton.Base.State;

public interface IStateStore<TStateEvents>
    where TStateEvents : Enum
{
    event Func<object, TStateEvents, Task> StateChanged;
}
