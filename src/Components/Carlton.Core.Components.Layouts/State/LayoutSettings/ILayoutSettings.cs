namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

public interface ILayoutSettings
{
    public IReadOnlyDictionary<string, object> Settings { get; }
}
