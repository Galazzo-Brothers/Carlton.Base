namespace Carlton.Base.TestBedFramework;

public class NavItemSelectedRequest : ComponentEventRequestBase<NavItemSelected, NavMenuViewModel>
{
    public NavItemSelectedRequest(object sender, NavItemSelected componentEvent)
        : base(sender, componentEvent)
    {
    }
}


