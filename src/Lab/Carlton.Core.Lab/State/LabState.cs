using System.Text.Json.Serialization;
using Carlton.Core.Utilities.JsonConverters;
namespace Carlton.Core.Lab.State;

/// <summary>
/// Represents the the centralized, normalized state of a component lab.
/// </summary>
public record LabState
{
	protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

	/// <summary>
	/// Gets the configurations of all components in the laboratory.
	/// </summary>
	public IReadOnlyList<ComponentConfigurations> ComponentConfigurations { get; init; }

	/// <summary>
	/// Gets or sets the index of the selected component.
	/// </summary>
	public int SelectedComponentIndex { get; init; }

	/// <summary>
	/// Gets or sets the index of the state of the selected component.
	/// </summary>
	public int SelectedComponentStateIndex { get; init; }

	/// <summary>
	/// Gets the state of the selected component.
	/// </summary>
	public ComponentState SelectedComponentState { get => ComponentConfigurations.ElementAt(SelectedComponentIndex).ComponentStates.ElementAt(SelectedComponentStateIndex); }

	/// <summary>
	/// Gets or sets the type of the selected component.
	/// </summary>
	[JsonConverter(typeof(JsonTypeConverter))]
	public Type SelectedComponentType { get { return ComponentConfigurations.ElementAt(SelectedComponentIndex).ComponentType; } }

	/// <summary>
	/// Gets or sets the parameters of the selected component.
	/// </summary>
	public object SelectedComponentParameters { get; init; }

	/// <summary>
	/// Gets or sets the recorded events of all components.
	/// </summary>
	public IEnumerable<ComponentRecordedEvent> ComponentEvents
	{
		get { return _componentEvents; }
		init { _componentEvents = value.ToList(); }
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="LabState"/> class.
	/// </summary>
	/// <param name="componentConfigurations">The configurations of all components in the laboratory.</param>
	internal LabState(IEnumerable<ComponentConfigurations> componentConfigurations)
	{
		ComponentConfigurations = componentConfigurations.ToList();
		SelectedComponentIndex = 0; //Default to the first item
		SelectedComponentStateIndex = 0; //Default to the first item
		SelectedComponentParameters = SelectedComponentState.ComponentParameters;
	}
}