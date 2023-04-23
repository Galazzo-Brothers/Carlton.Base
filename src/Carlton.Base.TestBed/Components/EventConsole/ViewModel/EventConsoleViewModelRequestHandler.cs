namespace Carlton.Base.TestBed;

public sealed class EventConsoleViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<EventConsoleViewModel>, EventConsoleViewModel>
{
    public EventConsoleViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public Task<EventConsoleViewModel> Handle(ViewModelRequest<EventConsoleViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new EventConsoleViewModel(State.ComponentEvents));
    }
}