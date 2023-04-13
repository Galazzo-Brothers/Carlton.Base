namespace Carlton.Base.TestBedFramework;

public class NavItemSelectedRequestFactory : IComponentEventRequestFactory<NavItemSelected, NavMenuViewModel>
{
    public IComponentEventRequest<NavItemSelected, NavMenuViewModel> CreateEventRequest(ICarltonComponent<NavMenuViewModel> sender, NavItemSelected evt)
    {
        return new NavItemSelectedRequest(sender, evt);
    }
}

