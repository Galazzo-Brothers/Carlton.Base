namespace Carlton.Base.TestBed;

public sealed class ComponentViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<ComponentViewerViewModel>, ComponentViewerViewModel>
{
    public ComponentViewerViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public Task<ComponentViewerViewModel> Handle(ViewModelRequest<ComponentViewerViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ComponentViewerViewModel
        (
            State.SelectedComponentType,
            State.SelectedComponentParameters
        ));
    }
}

