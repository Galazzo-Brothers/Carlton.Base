using Carlton.Core.Components.Flux.Attributes;

namespace Carlton.Core.Components.Lab;

[StateEvents]
public enum LabStateEvents
{
    MenuItemSelected,
    ParametersUpdated,
    EventRecorded,
    EventsCleared
}
