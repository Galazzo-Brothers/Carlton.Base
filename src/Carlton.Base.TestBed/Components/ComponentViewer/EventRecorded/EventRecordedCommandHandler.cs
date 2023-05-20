namespace Carlton.Base.TestBed;

public sealed class EventRecordedCommandHandler : CommandHandler<RecordEventCommand>
{
    public EventRecordedCommandHandler(IStateProcessor processor) : base(processor)
    {
    }
}