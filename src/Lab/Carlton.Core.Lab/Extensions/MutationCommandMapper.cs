using Carlton.Core.Components.DynamicComponents;
using Carlton.Core.Components.Navigation;
using Riok.Mapperly.Abstractions;

namespace Carlton.Core.Lab.Extensions;

[Mapper]
internal partial class MutationCommandMapper
{
    internal static partial TCommand Map<TCommand>(object args);

    [MapProperty(nameof(ExpandedStateChangedEventArgs.GroupIndexID), nameof(SelectMenuExpandedCommand.SelectedComponentIndex))]
    [MapProperty(nameof(ExpandedStateChangedEventArgs.IsExpanded), nameof(SelectMenuExpandedCommand.IsExpanded))]
    internal static partial SelectMenuExpandedCommand ToCommand(ExpandedStateChangedEventArgs args);

    [MapProperty(nameof(SelectItemChangedEventArgs<ComponentState>.GroupIndexID), nameof(SelectMenuItemCommand.ComponentIndex))]
    [MapProperty(nameof(SelectItemChangedEventArgs<ComponentState>.ItemIndexID), nameof(SelectMenuItemCommand.ComponentStateIndex))]
    [MapProperty(nameof(SelectItemChangedEventArgs<ComponentState>.Item), nameof(SelectMenuItemCommand.SelectedComponentState))]
    internal static partial SelectMenuItemCommand ToCommand(SelectItemChangedEventArgs<ComponentState> args);

    [MapProperty(nameof(CapturedEventRaisedArgs.EventName), nameof(RecordEventCommand.RecordedEventName))]
    [MapProperty(nameof(CapturedEventRaisedArgs.EventArgs), nameof(RecordEventCommand.EventArgs))]
    internal static partial RecordEventCommand ToCommand(CapturedEventRaisedArgs args);

    internal static ClearEventsCommand ToClearEventsCommand(object args) => new();
    internal static UpdateParametersCommand ToUpdateParametersCommand(ComponentParameters args) => new() { Parameters = args };
}


