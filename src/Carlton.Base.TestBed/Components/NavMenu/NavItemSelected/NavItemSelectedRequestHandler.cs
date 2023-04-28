namespace Carlton.Base.TestBed;

public sealed class NavItemSelectedRequestHandler : TestBedCommandRequestHandler<NavItemSelectedCommand>
{
    public NavItemSelectedRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}
