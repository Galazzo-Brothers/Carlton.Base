namespace Carlton.Base.TestBed;

public sealed class UpdateParametersCommandHandler : TestBedCommandRequestHandler<UpdateParametersCommand>
{
    public UpdateParametersCommandHandler(IStateProcessor processor, ILogger<TestBedCommandRequestHandler<UpdateParametersCommand>> logger) : base(processor, logger)
    {
    }
}
