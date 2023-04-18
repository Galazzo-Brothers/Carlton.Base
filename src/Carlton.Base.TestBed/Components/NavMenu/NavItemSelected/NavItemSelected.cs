namespace Carlton.Base.TestBed;

public record NavItemSelected(int SelectedGroupID, int SelectedItemID) : ComponentEventBase<NavMenuViewModel>;