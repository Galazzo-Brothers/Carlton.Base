
namespace Carlton.Base.TestBed;

public class TestBedCommandRequestHandler<TCommand> : CommandRequestHandlerBase<TCommand>
    where TCommand : ICommand
{
    public TestBedCommandRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}
