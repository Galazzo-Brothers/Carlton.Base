namespace Carlton.Core.Flux.Dispatchers.ViewModels;

public class ViewModelQueryDispatcher<TState>(IServiceProvider serviceProvider) : IViewModelQueryDispatcher<TState>
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		var handler = _serviceProvider.GetRequiredService<IViewModelQueryHandler<TState>>();
		return await handler.Handle(context, cancellationToken);
	}
}


public class ViewModelQueryHandler<TState>(IFluxState<TState> _state, IViewModelMapper<TState> _mapper) : IViewModelQueryHandler<TState>
{
	public Task<Result<TViewModel, FluxError>> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		var vm = ResultExtensions.TryResult<TViewModel, FluxError, Exception>(
			() => _mapper.Map<TViewModel>(_state.CurrentState),
			ex => new MappingError(context.FluxOperationTypeName, ex));
		return Task.FromResult(vm);
	}
}
