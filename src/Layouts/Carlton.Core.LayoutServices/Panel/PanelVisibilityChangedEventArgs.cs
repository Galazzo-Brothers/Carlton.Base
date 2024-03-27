namespace Carlton.Core.LayoutServices.Panel;

/// <summary>
/// Represents the event arguments for panel visibility state change event.
/// </summary>
public sealed record PanelVisibilityChangedEventArgs
(
	/// <summary>
	/// Gets a value indicating whether the layout panel is visible.
	/// </summary>
	bool IsVisible
);
