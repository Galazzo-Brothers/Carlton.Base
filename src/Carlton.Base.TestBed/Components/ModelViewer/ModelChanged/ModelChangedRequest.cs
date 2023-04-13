namespace Carlton.Base.TestBedFramework;

public class ModelChangedRequest : ComponentEventRequestBase<ModelChanged, ModelViewerViewModel>
{
    public ModelChangedRequest(object sender, ModelChanged evt) : base(sender, evt)
    {
    }
}

