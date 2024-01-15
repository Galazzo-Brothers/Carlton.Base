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
        var vm = _mapper.Map<TViewModel>(_state);
        context.MarkAsSucceeded(vm);
        return Task.FromResult(vm);
    }
}


