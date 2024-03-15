using System.Text.Json.Serialization;
using Carlton.Core.Utilities.JsonConverters;
namespace Carlton.Core.Lab.State;

public record LabState
{
	protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

	public IReadOnlyList<ComponentConfigurations> ComponentConfigurations { get; init; }
	public int SelectedComponentIndex { get; init; }
	public int SelectedComponentStateIndex { get; init; }
	public ComponentState SelectedComponentState { get => ComponentConfigurations.ElementAt(SelectedComponentIndex).ComponentStates.ElementAt(SelectedComponentStateIndex); }
	[JsonConverter(typeof(JsonTypeConverter))]
	public Type SelectedComponentType { get { return ComponentConfigurations.ElementAt(SelectedComponentIndex).ComponentType; } }
	public string SelectedComponentMarkup { get; init; }
	public object SelectedComponentParameters { get; init; }
	public IEnumerable<ComponentRecordedEvent> ComponentEvents
	{
		get { return _componentEvents; }
		init { _componentEvents = value.ToList(); }
	}

	public LabState(IEnumerable<ComponentConfigurations> componentConfigurations)
	{
		ComponentConfigurations = componentConfigurations.ToList();
		SelectedComponentIndex = 0; //Default to the first item
		SelectedComponentStateIndex = 0; //Default to the first item
		SelectedComponentParameters = SelectedComponentState.ComponentParameters;
	}
}