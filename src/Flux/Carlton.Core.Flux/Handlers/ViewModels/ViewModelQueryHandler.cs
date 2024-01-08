using Carlton.Core.Flux.Exceptions;
using MapsterMapper;

namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelQueryHandler<TState> : IViewModelQueryHandler<TState>
{
    private readonly IFluxState<TState> _fluxState;
    private readonly IMapper _mapper;
    private readonly ILogger<ViewModelQueryHandler<TState>> _logger;

    public ViewModelQueryHandler(IFluxState<TState> state, IMapper mapper, ILogger<ViewModelQueryHandler<TState>> logger)
        => (_fluxState, _mapper, _logger) = (state, mapper, logger);

    public Task<TViewModel> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        try
        {
            var result = _mapper.Map<TViewModel>(_fluxState.State);
            return Task.FromResult(result);
        }
        catch (CompileException ex)
        {
            context.MarkAsErrored();
            _logger.ViewModelMappingError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.MappingError(context, ex);
        }
    }
}


