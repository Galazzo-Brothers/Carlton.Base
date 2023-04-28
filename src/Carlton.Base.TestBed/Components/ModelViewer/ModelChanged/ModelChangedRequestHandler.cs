namespace Carlton.Base.TestBed;

public sealed class ModelChangedRequestHandler : TestBedCommandRequestHandler<ModelChangedCommand>
{
    public ModelChangedRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}
