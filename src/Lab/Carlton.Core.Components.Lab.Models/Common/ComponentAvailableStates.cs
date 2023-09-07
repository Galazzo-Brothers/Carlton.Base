namespace Carlton.Core.Components.Lab.Models.Common;

public record ComponentAvailableStates(Type ComponentType, bool IsExpanded, IEnumerable<ComponentState> ComponentStates);
