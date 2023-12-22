using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;

namespace Carlton.Core.Components.Lab.Models.Common;

public record ComponentAvailableStates(
    [property: JsonConverter(typeof(JsonTypeConverter))] Type ComponentType,
    bool IsExpanded, IEnumerable<ComponentState> ComponentStates);
