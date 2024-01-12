namespace Carlton.Core.Flux.Contracts;

public record FluxStateChangedEventArgs(string StateEvent, BaseRequestContext Context);

public interface IFluxStateObserver<TState>
{
    public event Func<FluxStateChangedEventArgs, Task> StateChanged;
}

public interface IFluxStateObservable<TState>
{
    public Task OnStateChanged(FluxStateChangedEventArgs args);
}



