namespace Carlton.Base.TestBed;
public sealed class ModelViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<ModelViewerViewModel>, ModelViewerViewModel>
{
    public ModelViewerViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public Task<ModelViewerViewModel> Handle(ViewModelRequest<ModelViewerViewModel> request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ModelViewerViewModel(State.SelectedComponentParameters));
    }
}