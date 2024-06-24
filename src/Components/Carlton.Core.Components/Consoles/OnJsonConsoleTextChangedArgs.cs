namespace Carlton.Core.Components.Consoles;

/// <summary>
/// Represents the arguments for the event that occurs when the JSON text in the console changes.
/// </summary>
/// <param name="UpdatedJson">The updated JSON object.</param>
public record OnJsonConsoleTextChangedArgs(object UpdatedJson);
