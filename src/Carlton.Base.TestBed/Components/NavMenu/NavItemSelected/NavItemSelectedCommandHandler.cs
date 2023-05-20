namespace Carlton.Base.TestBed;

public sealed class NavItemSelectedCommandHandler : CommandHandler<SelectMenuItemCommand>
{
    public NavItemSelectedCommandHandler(IStateProcessor processor) : base(processor)
    {
    }
}
