namespace Carlton.Base.TestBed;

public sealed class SelectedComponentBreadCrumbsViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<SelectedComponentBreadCrumbsViewModel>, SelectedComponentBreadCrumbsViewModel>
{
    public SelectedComponentBreadCrumbsViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public Task<SelectedComponentBreadCrumbsViewModel> Handle(ViewModelRequest<SelectedComponentBreadCrumbsViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new SelectedComponentBreadCrumbsViewModel(State.SelectedComponentState.Type.GetDisplayName(), State.SelectedComponentState.DisplayName));
    }
}

