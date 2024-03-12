namespace Carlton.Core.Flux.Internals.Dispatchers.ViewModels;

internal sealed class ViewModelQueryDispatcher<TState>(IServiceProvider serviceProvider) : IViewModelQueryDispatcher<TState>
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		var handler = _serviceProvider.GetRequiredService<IViewModelQueryHandler<TState>>();
		return await handler.Handle(context, cancellationToken);
	}
}


internal sealed class ViewModelQueryHandler<TState>(IFluxState<TState> _state, IViewModelMapper<TState> _mapper) : IViewModelQueryHandler<TState>
{
	public Task<Result<TViewModel, FluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		var viewModel = ResultExtensions.TryResult<TViewModel, FluxError, Exception>(
			() => _mapper.Map<TViewModel>(_state.CurrentState), //Success Path 
			ex => new MappingError(context.FluxOperationTypeName, ex)); //Exception Path 
		return Task.FromResult(viewModel);
	}
}


public abstract class ViewModelQueryDispatcherMiddlewareBase<TState>(IViewModelQueryDispatcher<TState> _decorated) : IViewModelQueryDispatcher<TState>
{
	public abstract Task<TViewModel> Dispatch<TViewModel>(object sender, CancellationToken cancellationToken, Func<Task<TViewModel>> next);

	async Task<Result<TViewModel, FluxError>> IViewModelQueryDispatcher<TState>.Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		async Task<TViewModel> next() => (await _decorated.Dispatch(sender, context, cancellationToken))
			.GetViewModelResultOrThrow(context);
		return await Dispatch(sender, cancellationToken, next);
	}
}


