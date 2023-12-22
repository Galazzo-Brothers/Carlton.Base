using Carlton.Core.Components.Lab.Models.Common;
using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;
namespace Carlton.Core.Components.Lab;

public record LabState 
{
    protected readonly IList<ComponentRecordedEvent> _componentEvents = new List<ComponentRecordedEvent>();

    public IReadOnlyList<ComponentAvailableStates> ComponentStates { get; init; }
    public int SelectedComponentIndex { get; init; }
    public int SelectedComponentStateIndex { get; init; }
    public ComponentState SelectedComponentState { get => ComponentStates.ElementAt(SelectedComponentIndex).ComponentStates.ElementAt(SelectedComponentStateIndex); }
    [JsonConverter(typeof(JsonTypeConverter))]
    public Type SelectedComponentType { get { return ComponentStates.ElementAt(SelectedComponentIndex).ComponentType; } }
    public string SelectedComponentMarkup { get; init; }
    public ComponentParameters SelectedComponentParameters { get; init; }
    public IEnumerable<ComponentRecordedEvent> ComponentEvents 
    {
        get { return _componentEvents; }
        init { _componentEvents = value.ToList(); }
    }

    public LabState(IEnumerable<ComponentAvailableStates> componentStates)
    {
        ComponentStates = componentStates.ToList();
        SelectedComponentIndex = 0; //Default to the first item
        SelectedComponentStateIndex = 0; //Default to the first item
        SelectedComponentParameters = SelectedComponentState.ComponentParameters;
    }
}