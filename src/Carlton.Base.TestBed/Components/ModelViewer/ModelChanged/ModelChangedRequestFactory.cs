namespace Carlton.Base.TestBedFramework;

public class ModelChangedRequestFactory : IComponentEventRequestFactory<ModelChanged, ModelViewerViewModel>
{
    public IComponentEventRequest<ModelChanged, ModelViewerViewModel> CreateEventRequest(ICarltonComponent<ModelViewerViewModel> sender, ModelChanged evt)
    {
        return new ModelChangedRequest(sender, evt);
    }
}