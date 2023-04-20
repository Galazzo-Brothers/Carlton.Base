namespace Carlton.Base.TestBed;

public sealed class SelectedComponentDisplayViewModelRequestHandler : TestBedRequestHandlerViewModelBase<SelectedComponentDisplayViewModelRequest, SelectedComponentDisplayViewModel>
{
    public SelectedComponentDisplayViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public override Task<SelectedComponentDisplayViewModel> Handle(SelectedComponentDisplayViewModelRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SelectedComponentDisplayViewModel(State.SelectedComponentState.Type.GetDisplayName(), State.SelectedComponentState.DisplayName));
    }
}

