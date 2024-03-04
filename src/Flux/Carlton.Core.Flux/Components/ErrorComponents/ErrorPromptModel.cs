namespace Carlton.Core.Flux.Components.ErrorComponents;

public record ErrorPromptModel(string Header, string Message, string IconClass, Action Recover);
