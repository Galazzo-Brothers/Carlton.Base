namespace Carlton.Core.Flux.Debug.Extensions;

internal interface IFluxStateWrapper
{
	public object State { get; }
}

internal class FluxStateWrapper<T> : IFluxStateWrapper
{
	private readonly IFluxState<T> _fluxState;
	public FluxStateWrapper(IFluxState<T> fluxState)
		=> _fluxState = fluxState;

	public object? State => _fluxState.CurrentState;
}