using Carlton.Core.Components.DynamicComponents;
using Carlton.Core.Components.Navigation;
using Riok.Mapperly.Abstractions;

namespace Carlton.Core.Lab.Extensions;

[Mapper]
internal partial class MutationCommandMapper
{
    internal static partial TCommand Map<TCommand>(object args);

    [MapProperty(nameof(ItemExpansionChangeEventArgs.GroupIndexID), nameof(SelectMenuExpandedCommand.SelectedComponentIndex))]
    [MapProperty(nameof(ItemExpansionChangeEventArgs.IsExpanded), nameof(SelectMenuExpandedCommand.IsExpanded))]
    internal static partial SelectMenuExpandedCommand ToCommand(ItemExpansionChangeEventArgs args);

    [MapProperty(nameof(ItemSelectedEventArgs<ComponentState>.GroupIndexID), nameof(SelectMenuItemCommand.ComponentIndex))]
    [MapProperty(nameof(ItemSelectedEventArgs<ComponentState>.ItemIndexID), nameof(SelectMenuItemCommand.ComponentStateIndex))]
    [MapProperty(nameof(ItemSelectedEventArgs<ComponentState>.Item), nameof(SelectMenuItemCommand.SelectedComponentState))]
    internal static partial SelectMenuItemCommand ToCommand(ItemSelectedEventArgs<ComponentState> args);

    [MapProperty(nameof(CapturedComponentEventArgs.EventName), nameof(RecordEventCommand.RecordedEventName))]
    [MapProperty(nameof(CapturedComponentEventArgs.EventArgs), nameof(RecordEventCommand.EventArgs))]
    internal static partial RecordEventCommand ToCommand(CapturedComponentEventArgs args);

    internal static ClearEventsCommand ToClearEventsCommand(object args) => new();
    internal static UpdateParametersCommand ToUpdateParametersCommand(ComponentParameters args) => new() { Parameters = args };
}


