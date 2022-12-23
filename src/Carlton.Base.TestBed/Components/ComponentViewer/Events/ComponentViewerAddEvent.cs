namespace Carlton.Base.TestBedFramework;

public record ComponentViewerAddEvent(object evt);

public class ComponentViewerAddEventRequest : ComponentEventRequestBase<ComponentViewerAddEvent>
{
    public ComponentViewerAddEventRequest(object sender, ComponentViewerAddEvent evt) : base(sender, evt)
    {
    }
}

public class ComponentViewerAddEventRequestHandler : TestBedEventRequestHandlerBase<ComponentViewerAddEventRequest>
{
    public ComponentViewerAddEventRequestHandler(TestBedState state) : base(state) { }

    public async override Task<Unit> Handle(ComponentViewerAddEventRequest request, CancellationToken cancellationToken)
    {
        await State.AddTestComponentEvents(request.Sender, request.ComponentEvent.evt);
        return Unit.Value;
    }
}
