namespace Carlton.Core.Lab.Components.ParametersViewer;

/// <summary>
/// Represents the arguments for the event that occurs when parameters are changed.
/// </summary>
/// <param name="UpdatedParameters">The parameters that were updated.</param>
public record OnParametersChangedArgs(object UpdatedParameters);
