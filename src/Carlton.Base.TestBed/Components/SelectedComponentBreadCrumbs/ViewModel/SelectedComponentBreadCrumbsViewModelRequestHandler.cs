namespace Carlton.Base.TestBed;

public sealed class SelectedComponentBreadCrumbsViewModelRequestHandler : TestBedRequestHandlerViewModelBase<SelectedComponentBreadCrumbsViewModelRequest, SelectedComponentBreadCrumbsViewModel>
{
    public SelectedComponentBreadCrumbsViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public override Task<SelectedComponentBreadCrumbsViewModel> Handle(SelectedComponentBreadCrumbsViewModelRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SelectedComponentBreadCrumbsViewModel(State.SelectedComponentState.Type.GetDisplayName(), State.SelectedComponentState.DisplayName));
    }
}

