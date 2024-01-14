using Carlton.Core.Flux.Exceptions;
using MapsterMapper;

namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelQueryHandler<TState> : IViewModelQueryHandler<TState>
{
    private readonly TState _state;
    private readonly IMapper _mapper;

    public ViewModelQueryHandler(TState state, IMapper mapper)
        => (_state, _mapper) = (state, mapper);

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
            throw ViewModelFluxException<TState, TViewModel>.MappingError(context, ex); //Mapping Errors
        }
    }
}


