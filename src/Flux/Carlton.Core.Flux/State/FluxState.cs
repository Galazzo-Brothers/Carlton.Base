using MapsterMapper;
namespace Carlton.Core.Flux.State;

public class FluxState<TState> : IMutableFluxState<TState>
{
    private readonly Queue<IFluxStateMutation<TState>> _recordedMutations = new();
    private readonly IMutationResolver<TState> _mutationResolver;
    private readonly IMapper _mapper;
    private readonly ILogger<FluxState<TState>> _logger;

    public event Func<string, Task> StateChanged;
    public IReadOnlyList<string> RecordedEventStore { get => _recordedMutations.Select(_ => _.StateEvent).ToList().AsReadOnly(); }
    public TState InitialState { get; init; }
    public TState State { get; private set; }
    private TState RollbackState { get; set; }

    public FluxState(TState state, IMutationResolver<TState> mutationResolver,
        IMapper mapper, ILogger<FluxState<TState>> logger) =>
            (State, _mutationResolver, _mapper, _logger) = (state, mutationResolver, mapper, logger);

    public async Task MutateState<TCommand>(TCommand input)
    {
        var displayName = typeof(TCommand).GetDisplayName();
        try
        {
            //Find the correct mutations and save a rollback state
            var mutation = _mutationResolver.Resolve(input.GetType());
            _mapper.Map(State, RollbackState);

            //Run the non-destructive mutation to generate a new state from the old, 
            //and replace the old state with the new
            var mutatedState = mutation.Mutate(State, input);
            _mapper.Map(mutatedState, State);

            //Update EventStore 
            _recordedMutations.Enqueue(mutation);

            //Raise Event
            await InvokeStateChanged(mutation.StateEvent);
            _logger.MutationApplyCompleted(displayName);
        }
        catch(Exception ex)
        {
            _logger.MutationApplyError(ex, displayName);
            _mapper.Map(RollbackState, State);
            throw;
        }
    }

    private async Task InvokeStateChanged(string evt)
    {
        if(StateChanged != null)
        {
            var tasks = new List<Task>();
            foreach(var handler in StateChanged.GetInvocationList())
            {
                tasks.Add(Task.Run(async () =>
                {  
                    var castedDelegate = (Func<string, Task>)handler;
                    await castedDelegate(evt);
                }));
            }

            await Task.WhenAll(tasks);
        }
    }
}
