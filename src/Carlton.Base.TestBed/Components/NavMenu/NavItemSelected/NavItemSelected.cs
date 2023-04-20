namespace Carlton.Base.TestBed;

public sealed record NavItemSelected(int SelectedGroupID, int SelectedItemID) : ComponentEventBase<NavMenuViewModel>;