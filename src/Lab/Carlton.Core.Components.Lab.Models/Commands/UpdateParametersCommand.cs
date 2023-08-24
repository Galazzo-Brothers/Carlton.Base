namespace Carlton.Core.Components.Lab.Models;

public record UpdateParametersCommand(object Sender, object ComponentParameters) : MutationCommand(Sender);