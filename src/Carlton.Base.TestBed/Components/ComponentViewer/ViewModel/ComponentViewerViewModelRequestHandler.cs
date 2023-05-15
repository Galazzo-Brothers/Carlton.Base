
namespace Carlton.Base.TestBed;

public class ComponentViewerViewModelRequestHandler : TestBedViewModelRequestHandler<ComponentViewerViewModel>
{
    public ComponentViewerViewModelRequestHandler(IViewModelStateFacade state, ILogger<TestBedViewModelRequestHandler<ComponentViewerViewModel>> logger) : base(state, logger)
    {
    }
}

