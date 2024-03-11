namespace Carlton.Core.Flux.Components.ErrorComponents;

public record ErrorPromptModel
{
	public required string Header { get; init; }
	public required string Message { get; init; }
	public required string IconClass { get; init; }
	public required Action Recover { get; init; }
}
