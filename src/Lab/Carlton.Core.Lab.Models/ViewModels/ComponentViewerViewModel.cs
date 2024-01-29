using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;

namespace Carlton.Core.Lab.Models.ViewModels;

public sealed record ComponentViewerViewModel
{
    [Required]
    [JsonConverter(typeof(JsonTypeConverter))]
    public required Type ComponentType { get; init; }

    [Required]
    public required ComponentParameters ComponentParameters { get; init; }
};