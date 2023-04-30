namespace Carlton.Base.TestBed;

public sealed class NavItemSelectedCommandHandler : TestBedCommandRequestHandler<SelectMenuItemCommand>
{
    public NavItemSelectedCommandHandler(ICommandProcessor processor) : base(processor)
    {
    }
}
