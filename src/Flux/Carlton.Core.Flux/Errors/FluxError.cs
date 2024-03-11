namespace Carlton.Core.Flux.Errors;

public abstract record FluxError(string Message, int EventId);
