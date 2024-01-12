using Carlton.Core.Flux.Exceptions;
using MapsterMapper;

namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelQueryHandler<TState> : IViewModelQueryHandler<TState>
{
    private readonly TState _state;
    private readonly IMapper _mapper;
    private readonly ILogger<ViewModelQueryHandler<TState>> _logger;

    public ViewModelQueryHandler(TState state, IMapper mapper, ILogger<ViewModelQueryHandler<TState>> logger)
        => (_state, _mapper, _logger) = (state, mapper, logger);

    public Task<TViewModel> Handle<TViewModel>(ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        try
        {
            var viewmodel = _mapper.Map<TViewModel>(_state);
            context.MarkAsSucceeded(viewmodel);
            return Task.FromResult(viewmodel);
        }
        catch (CompileException ex)
        {
            context.MarkAsErrored(ex);
            _logger.ViewModelMappingError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.MappingError(context, ex);
        }
    }
}


