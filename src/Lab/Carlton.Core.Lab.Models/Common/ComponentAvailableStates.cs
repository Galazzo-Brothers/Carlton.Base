using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;

namespace Carlton.Core.Lab.Models.Common;


public record ComponentAvailableStates
{
    [Required]
    [JsonConverter(typeof(JsonTypeConverter))]
    public required Type ComponentType { get; init; }

    [Required]
    public required IEnumerable<ComponentState> ComponentStates { get; init; }

    public bool IsExpanded { get; init; }
}

