namespace Carlton.Core.Components.Dropdowns;

public record ValueChangeArgs<T>(int SelectedIndex, string SelectedKey, T SelectedValue);
