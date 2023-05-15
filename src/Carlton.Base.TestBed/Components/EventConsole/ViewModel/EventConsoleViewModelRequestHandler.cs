namespace Carlton.Base.TestBed;

public class EventConsoleViewModelRequestHandler : TestBedViewModelRequestHandler<EventConsoleViewModel>
{
    public EventConsoleViewModelRequestHandler(IViewModelStateFacade state, ILogger<TestBedViewModelRequestHandler<EventConsoleViewModel>> logger) : base(state, logger)
    {
    }
}
