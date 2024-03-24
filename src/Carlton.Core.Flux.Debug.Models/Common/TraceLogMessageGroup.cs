namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a group of trace log messages.
/// </summary>
public sealed record TraceLogMessageGroup
{
	/// <summary>
	/// Gets or sets the parent log message entry.
	/// </summary>
	public required TraceLogMessage ParentEntry { get; set; }

	/// <summary>
	/// Gets or sets the child log message entries.
	/// </summary>
	public IEnumerable<TraceLogMessage> ChildEntries { get; set; } = new List<TraceLogMessage>();

	/// <summary>
	/// Gets the flattened sequence of log message entries, including the parent entry followed by child entries.
	/// </summary>
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