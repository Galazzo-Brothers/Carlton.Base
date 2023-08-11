namespace Carlton.Core.Components.Flux.Contracts;

public interface IFluxState<TState> : IFluxStateObserver<TState>
{
    public IReadOnlyList<string> RecordedEventStore { get; }
    public TState State { get; }
}





