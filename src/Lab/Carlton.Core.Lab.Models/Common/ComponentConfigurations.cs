using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;
namespace Carlton.Core.Lab.Models.Common;

/// <summary>
/// Represents a test lab configurations for a component.
/// </summary>
public record ComponentConfigurations
{
	/// <summary>
	/// Gets or initializes the type of the component.
	/// </summary>
	[Required]
	[JsonConverter(typeof(JsonTypeConverter))]
	public required Type ComponentType { get; init; }

	/// <summary>
	/// Gets or initializes the available lab states of the component.
	/// </summary>
	[Required]
	public required IEnumerable<ComponentState> ComponentStates { get; init; } = new List<ComponentState>();
}

