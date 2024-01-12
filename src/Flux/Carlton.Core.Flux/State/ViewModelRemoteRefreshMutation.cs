using MapsterMapper;
namespace Carlton.Core.Flux.State;

public class ViewModelRemoteRefreshMutation<TState, TViewModel>(IMapper mapper) : IFluxStateMutation<TState>
{
    private readonly IMapper _mapper = mapper;

    public string StateEvent => "StateRefreshedFromRemoteServer";

    public object MutationCommand { get; init; }

    public ViewModelRemoteRefreshMutation(IMapper mapper, object command)
        : this(mapper)
        => MutationCommand = command;

    public TState Mutate(TState state)
        => _mapper.Map(MutationCommand, state);

    public TState Mutate(TState state, object command)
    {
        throw new NotImplementedException();
    }
}

public record ViewModelRemoteRefreshCommand<TViewModel>(TViewModel ViewModel);

public class ViewModelRemoteRefreshCommandValidator<TViewModel> : AbstractValidator<ViewModelRemoteRefreshCommand<TViewModel>>
{
    public ViewModelRemoteRefreshCommandValidator(IValidator<TViewModel> validator)
    {
        RuleFor(_ => _.ViewModel).NotNull().SetValidator(validator);
    }
}
