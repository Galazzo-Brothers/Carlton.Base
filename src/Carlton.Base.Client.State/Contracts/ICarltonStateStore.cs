namespace Carlton.Base.State;

public interface ICarltonStateStore<TStateEvents>
    where TStateEvents : Enum
{
    event Func<object, TStateEvents, Task> StateChanged;
}
