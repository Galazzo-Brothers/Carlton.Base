namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents a group of trace log messages.
/// </summary>
public sealed record TraceLogMessageGroup
{
	/// <summary>
	/// Gets or sets the parent log message entry.
	/// </summary>
	public required TraceLogMessageDescriptor ParentEntry { get; set; }

	/// <summary>
	/// Gets or sets the child log message entries.
	/// </summary>
	public IEnumerable<TraceLogMessageDescriptor> ChildEntries { get; set; } = new List<TraceLogMessageDescriptor>();

	/// <summary>
	/// Gets the flattened sequence of log message entries, including the parent entry followed by child entries.
	/// </summary>
	public IEnumerable<TraceLogMessageDescriptor> FlattenedEntries()
	{

		// Include the parent entry first, followed by child entries
		yield return ParentEntry;
		foreach (var childEntry in ChildEntries)
		{
			yield return childEntry;
		}
	}
}