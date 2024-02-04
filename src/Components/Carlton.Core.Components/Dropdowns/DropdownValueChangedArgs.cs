namespace Carlton.Core.Components.Dropdowns;

public record DropdownValueChangedArgs<T>(int SelectedIndex, string SelectedKey, T SelectedValue);
