using System.ComponentModel.DataAnnotations;

namespace Carlton.Core.Flux.Debug.Models.Commands;

public record ChangeSelectedLogMessageCommand
{
    [Required]
    public required LogMessage SelectedLogMessage { get; init; }
};
