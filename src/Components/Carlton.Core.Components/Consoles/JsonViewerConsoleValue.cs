namespace Carlton.Core.Components.Consoles;

/// <summary>
/// Represents a value for the JSON viewer console, indicating whether the value is valid and the associated value.
/// </summary>
/// <param name="IsValid">Indicates whether the JSON value is valid.</param>
/// <param name="Value">The actual value being represented in the JSON viewer console.</param>
public record JsonViewerConsoleValue(bool IsValid, object Value);
