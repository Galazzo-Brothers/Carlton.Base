using System.Text.Json.Serialization;
namespace Carlton.Core.Components.Toggles;

/// <summary>
/// Enumeration representing the options of the toggle select.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ToggleSelectOption
{
	FirstOption = 1,
	SecondOption = 2
}