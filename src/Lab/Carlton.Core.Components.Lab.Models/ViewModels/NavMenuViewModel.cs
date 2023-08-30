namespace Carlton.Core.Components.Lab.Models;

public sealed record NavMenuViewModel(IEnumerable<ComponentState> MenuItems, int SelectedIndex = 1);


