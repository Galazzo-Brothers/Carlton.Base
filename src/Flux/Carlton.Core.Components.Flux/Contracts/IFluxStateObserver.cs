namespace Carlton.Core.Flux.Contracts;

public interface IFluxStateObserver<TState>
{
    public event Func<string, Task> StateChanged;
}

