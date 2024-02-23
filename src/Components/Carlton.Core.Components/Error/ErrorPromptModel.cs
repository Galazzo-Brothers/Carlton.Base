namespace Carlton.Core.Components.Error;

/// <summary>
/// Represents a model for an error prompt, including header, message, icon class, and recovery action.
/// </summary>
public sealed record ErrorPromptModel
{
    /// <summary>
    /// Gets or sets the header of the error prompt.
    /// </summary>
    public string Header { get; init; }

    /// <summary>
    /// Gets or sets the message of the error prompt.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Gets or sets the CSS class for the error icon.
    /// </summary>
    public string IconClass { get; init; }

    /// <summary>
    /// Gets or sets the action to be invoked when recovering from the error.
    /// </summary>
    public Action Recover { get; init; }
}