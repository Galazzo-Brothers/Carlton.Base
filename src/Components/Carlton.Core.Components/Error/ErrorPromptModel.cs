namespace Carlton.Core.Components.Error;

public record ErrorPromptModel(string Header, string Message, string IconClass, Action Recover);