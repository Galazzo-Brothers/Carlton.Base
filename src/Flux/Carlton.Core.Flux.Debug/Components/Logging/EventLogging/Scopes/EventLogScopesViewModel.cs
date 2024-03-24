namespace Carlton.Core.Flux.Debug.Components.Logging.EventLogging.Scopes;

public record EventLogScopesViewModel
{
    public required LogMessage SelectedLogMessage { get; init; }
};

