using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Models.Common;

public record TraceLogMessage
{
	public required DateTime Timestamp { get; init; }
	public required FluxActions FluxAction { get; init; }
	public required EventId EventId { get; init; }
	public required string TypeDisplayName { get; init; }
	public required bool RequestSucceeded { get; init; }
	public required object RequestContext { get; init; }
}

public record TraceLogMessageGroup
{
	public required TraceLogMessage ParentEntry { get; set; }
	public IEnumerable<TraceLogMessage> ChildEntries { get; set; } = new List<TraceLogMessage>();

	public IEnumerable<TraceLogMessage> FlattenedEntries
	{
		get
		{
			// Include the parent entry first, followed by child entries
			yield return ParentEntry;
			foreach (var childEntry in ChildEntries)
			{
				yield return childEntry;
			}
		}
	}
}

public enum FluxActions
{
	ViewModelQuery,
	MutationCommand
}
