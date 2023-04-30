namespace Carlton.Base.TestBed;

public sealed class NavItemSelectedCommandRequestHandler : TestBedCommandRequestHandler<SelectMenuItemCommand>
{
    public NavItemSelectedCommandRequestHandler(ICommandProcessor processor) : base(processor)
    {
    }
}
