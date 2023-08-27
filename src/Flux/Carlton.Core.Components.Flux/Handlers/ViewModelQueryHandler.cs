using MapsterMapper;

namespace Carlton.Core.Components.Flux.Handlers;

public class ViewModelQueryHandler<TState, TViewModel> : IViewModelQueryHandler<TState, TViewModel>
{
    private readonly IFluxState<TState> _fluxState;
    private readonly IMapper _mapper;
    private readonly ILogger<ViewModelQueryHandler<TState, TViewModel>> _logger;

    public ViewModelQueryHandler(IFluxState<TState> state, IMapper mapper, ILogger<ViewModelQueryHandler<TState, TViewModel>> logger)
        => (_fluxState, _mapper, _logger) = (state, mapper, logger);

    public Task<TViewModel> Handle(ViewModelQuery query, CancellationToken cancellationToken)
    {
        Log.ViewModelMappingStarted(_logger, typeof(TViewModel).GetDisplayName());
        var result = _mapper.Map<TViewModel>(_fluxState.State);
        Log.ViewModelMappingCompleted(_logger, typeof(TViewModel).GetDisplayName());
        return Task.FromResult(result);
    }
}


