namespace Carlton.Base.TestBed;

public class TestBedCommandRequestHandler<TCommand> : CommandRequestHandlerBase<TCommand>
    where TCommand : ICommand
{
    public TestBedCommandRequestHandler(IStateProcessor processor, 
        ILogger<TestBedCommandRequestHandler<TCommand>> logger) : base(processor, logger)
    {
    }
}
