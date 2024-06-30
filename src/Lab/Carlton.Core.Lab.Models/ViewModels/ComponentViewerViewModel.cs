using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;
namespace Carlton.Core.Lab.Models.ViewModels;

/// <summary>
/// Represents the view model for the component viewer.
/// </summary>
public sealed record ComponentViewerViewModel
{
	/// <summary>
	/// Gets or initializes the type of the component.
	/// </summary>
	[Required]
	[JsonConverter(typeof(JsonTypeConverter))]
	public required Type ComponentType { get; init; }

	/// <summary>
	/// Gets or initializes the parameters of the component.
	/// </summary>
	[Required]
	public required object ComponentParameters { get; init; }
};