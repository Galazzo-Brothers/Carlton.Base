namespace Carlton.Core.Flux.Contracts;

public interface IFluxState<TState> : IFluxStateObserver<TState>
{
    public IReadOnlyList<string> RecordedEventStore { get; }
    public TState InitialState { get; }
    public TState State { get; }
}





