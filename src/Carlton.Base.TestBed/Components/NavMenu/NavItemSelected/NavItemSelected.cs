namespace Carlton.Base.TestBedFramework;

public record NavItemSelected : ComponentEventBase<NavMenuViewModel>
{
    public int SelectedGroupID { get; }
    public int SelectedItemID { get; }

    public NavItemSelected(CarltonComponentBase<NavMenuViewModel> sender, int selectedGroupID, int selectedItemID)
        : base(sender) => (SelectedGroupID, SelectedItemID) = (selectedGroupID, selectedItemID);
}