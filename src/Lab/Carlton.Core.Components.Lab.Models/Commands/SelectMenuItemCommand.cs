namespace Carlton.Core.Components.Lab.Models;

public sealed record SelectMenuItemCommand(object Sender, ComponentState ComponentState) : MutationCommand(Sender);
