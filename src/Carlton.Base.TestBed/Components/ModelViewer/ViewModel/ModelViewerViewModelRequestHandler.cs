namespace Carlton.Base.TestBed;
public sealed class ModelViewerViewModelRequestHandler : TestBedRequestHandlerViewModelBase<ModelViewerViewModelRequest, ModelViewerViewModel>
{
    public ModelViewerViewModelRequestHandler(TestBedState state) : base(state)
    {
    }

    public override Task<ModelViewerViewModel> Handle(ModelViewerViewModelRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new ModelViewerViewModel(State.SelectedComponentParameters));
    }
}