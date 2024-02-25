namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

public sealed class LayoutSettingsBuilder
{
    private readonly Dictionary<string, object> _settings = [];

    public LayoutSettingsBuilder AddSetting(string key, object value)
    {
        _settings[key] = value;
        return this;
    }

    public LayoutSettings Build()
    {
        return new LayoutSettings
        {
            Settings = _settings
        };
    }
}
