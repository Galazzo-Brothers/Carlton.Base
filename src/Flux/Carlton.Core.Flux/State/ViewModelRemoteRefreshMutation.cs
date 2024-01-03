using MapsterMapper;

namespace Carlton.Core.Flux.State;

public class ViewModelRemoteRefreshMutation<TState>(IMapper mapper) : FluxStateMutationBase<TState, ViewModelRemoteRefreshCommand>
{
    private readonly IMapper _mapper = mapper;

    public override string StateEvent => "StateRefreshedFromRemoteServer";

    public override TState Mutate(TState state, ViewModelRemoteRefreshCommand command)
    {
        return _mapper.Map(command.ViewModel, state);
    }
}

public record ViewModelRemoteRefreshCommand(object ViewModel) : MutationCommand;