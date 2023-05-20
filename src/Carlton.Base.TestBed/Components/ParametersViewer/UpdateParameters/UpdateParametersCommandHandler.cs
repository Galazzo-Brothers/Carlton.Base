namespace Carlton.Base.TestBed;

public sealed class UpdateParametersCommandHandler : CommandHandler<UpdateParametersCommand>
{
    public UpdateParametersCommandHandler(IStateProcessor processor) : base(processor)
    {
    }
}
