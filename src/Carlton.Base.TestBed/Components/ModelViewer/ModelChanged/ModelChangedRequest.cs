namespace Carlton.Base.TestBedFramework;

public sealed class ModelChangedRequest : ComponentEventRequestBase<ModelChanged, ModelViewerViewModel>
{
    public ModelChangedRequest(ICarltonComponent<ModelViewerViewModel> sender, ModelChanged evt) : base(sender, evt)
    {
    }
}

