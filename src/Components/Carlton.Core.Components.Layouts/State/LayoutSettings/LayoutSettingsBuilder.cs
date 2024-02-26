namespace Carlton.Core.Components.Layouts.State.LayoutSettings;

/// <summary>
/// Represents a builder for constructing layout settings.
/// </summary>
public sealed class LayoutSettingsBuilder
{
    private readonly Dictionary<string, object> _settings = [];

    /// <summary>
    /// Adds a setting with the specified key and value to the builder.
    /// </summary>
    /// <param name="key">The key of the setting.</param>
    /// <param name="value">The value of the setting.</param>
    /// <returns>The layout settings builder.</returns>
    public LayoutSettingsBuilder AddSetting(string key, object value)
    {
        _settings[key] = value;
        return this;
    }

    /// <summary>
    /// Builds and returns the layout settings based on the settings added to the builder.
    /// </summary>
    /// <returns>The layout settings.</returns>
    public LayoutSettings Build()
    {
        return new LayoutSettings
        {
            Settings = _settings
        };
    }
}
