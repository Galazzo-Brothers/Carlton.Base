namespace Carlton.Base.TestBedFramework;

public sealed class NavItemSelectedRequest : ComponentEventRequestBase<NavItemSelected, NavMenuViewModel>
{
    public NavItemSelectedRequest(ICarltonComponent<NavMenuViewModel> sender, NavItemSelected componentEvent)
        : base(sender, componentEvent)
    {
    }
}


