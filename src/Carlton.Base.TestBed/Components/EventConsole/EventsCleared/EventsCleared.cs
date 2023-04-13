namespace Carlton.Base.TestBedFramework;

public record EventsCleared : ComponentEventBase<EventConsoleViewModel>
{
    public EventsCleared(ICarltonComponent<EventConsoleViewModel> sender) : base(sender)
    {
    }
}
