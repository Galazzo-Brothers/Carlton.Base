using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.State;

internal class FluxDebugViewModelQueryLoggingScopesMiddleware<TState>(
	IViewModelQueryDispatcher<TState> _decorated,
	ILogger<FluxDebugViewModelQueryLoggingScopesMiddleware<TState>> _logger) : ViewModelQueryDispatcherMiddlewareBase<TState>(_decorated)
{
	public override Task<TViewModel> Dispatch<TViewModel>(object sender, CancellationToken cancellationToken, Func<Task<TViewModel>> next)
	{
		using (_logger.BeginScope("FluxDebug:{@FluxDebug}", true))
			return next();
	}
}

internal class FluxDebugMutationCommandLoggingScopesMiddleware<TState>(
	IMutationCommandDispatcher<TState> _decorated,
	ILogger<FluxDebugMutationCommandLoggingScopesMiddleware<TState>> _logger) : MutationCommandDispatcherMiddlewareBase<TState>(_decorated)
{
	public override Task<MutationCommandResult> Dispatch<TCommand>(object sender, CancellationToken cancellationToken, Func<Task<MutationCommandResult>> next)
	{
		using (_logger.BeginScope("FluxDebug:{@FluxDebug}", true))
			return next();
	}
}
