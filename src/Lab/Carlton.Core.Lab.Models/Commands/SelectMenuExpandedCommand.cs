namespace Carlton.Core.Lab.Models.Commands;

public sealed record SelectMenuExpandedCommand(int SelectedComponentIndex, bool IsExpanded) : MutationCommand;
