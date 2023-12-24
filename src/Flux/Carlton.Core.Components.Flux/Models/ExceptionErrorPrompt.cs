namespace Carlton.Core.Flux.Models;

public record ExceptionErrorPrompt(string Header, string Message, string IconClass, Action Recover);