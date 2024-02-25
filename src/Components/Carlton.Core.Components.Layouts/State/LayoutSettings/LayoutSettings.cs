namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

public sealed class LayoutSettings : ILayoutSettings
{
    public IReadOnlyDictionary<string, object> Settings { get; init; } = new Dictionary<string, object>();
}
