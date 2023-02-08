namespace Carlton.Base.State;

public interface ICarltonStateStore
{
    event Func<object, string, Task> StateChanged;
}
