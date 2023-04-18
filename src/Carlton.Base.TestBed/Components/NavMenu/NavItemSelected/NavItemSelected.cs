namespace Carlton.Base.TestBedFramework;

public record NavItemSelected(int SelectedGroupID, int SelectedItemID) : IComponentEvent<NavMenuViewModel>;