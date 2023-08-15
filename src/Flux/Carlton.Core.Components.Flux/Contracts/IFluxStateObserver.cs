namespace Carlton.Core.Components.Flux.Contracts;

public interface IFluxStateObserver<TState>
{
    public event Func<string, Task> StateChanged;
}

