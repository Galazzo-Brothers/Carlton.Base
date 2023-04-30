namespace Carlton.Base.TestBed;

public sealed class UpdateParametersCommandRequestHandler : TestBedCommandRequestHandler<UpdateParametersCommand>
{
    public UpdateParametersCommandRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}
