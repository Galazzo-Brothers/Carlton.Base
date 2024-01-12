namespace Carlton.Core.Flux.State;
using Carlton.Core.Utilities.Events.Extensions;

public class FluxStateObservable<TState> : IFluxStateObserver<TState>, IFluxStateObservable<TState>
{
    public event Func<FluxStateChangedEventArgs, Task> StateChanged;

    public async Task OnStateChanged(FluxStateChangedEventArgs args)
    {
        if (StateChanged == null)
            return;

        await StateChanged.GetInvocationList().RaiseAsyncDelegates(args);
    }
}


