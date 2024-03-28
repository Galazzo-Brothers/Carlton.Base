namespace Carlton.Core.LayoutServices.Panel;

/// <summary>
/// Represents the event arguments for panel visibility state change event.
/// </summary>
/// <param name="IsVisible">Gets a value indicating whether the layout panel is visible.</param>
public sealed record PanelVisibilityChangedEventArgs(bool IsVisible);
