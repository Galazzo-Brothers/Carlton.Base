namespace Carlton.Base.TestBedFramework;

public record NavItemSelected(int SelectedGroupID, int SelectedItemID) : ComponentEventBase<NavMenuViewModel>;