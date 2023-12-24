namespace Carlton.Core.Lab.Models.ViewModels;

public sealed record NavMenuViewModel(IEnumerable<ComponentAvailableStates> MenuItems, int SelectedComponentIndex, int SelectedStateIndex);


