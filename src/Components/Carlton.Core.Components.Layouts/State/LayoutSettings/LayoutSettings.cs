namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

public class LayoutSettings : ILayoutSettings
{
    public IDictionary<string, object> Settings { get; init; } = new Dictionary<string, object>();
}
