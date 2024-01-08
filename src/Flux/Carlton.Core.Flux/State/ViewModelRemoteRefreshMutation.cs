using MapsterMapper;
namespace Carlton.Core.Flux.State;

public class ViewModelRemoteRefreshMutation<TState, TViewModel>(IMapper mapper) : FluxStateMutationBase<TState, ViewModelRemoteRefreshCommand<TViewModel>>
{
    private readonly IMapper _mapper = mapper;

    public override string StateEvent => "StateRefreshedFromRemoteServer";

    public override TState Mutate(TState state, ViewModelRemoteRefreshCommand<TViewModel> command)
    {
        return _mapper.Map(command.ViewModel, state);
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
