namespace Carlton.Core.Components.Lab.Models.Commands;

public sealed record SelectMenuItemCommand(int ComponentIndex, int ComponentStateIndex, ComponentState SelectedComponentState) : MutationCommand;
