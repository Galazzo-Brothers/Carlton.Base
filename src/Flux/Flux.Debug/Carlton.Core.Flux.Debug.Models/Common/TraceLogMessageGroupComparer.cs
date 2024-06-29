using System.Collections;
namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Provides custom comparison logic for <see cref="TraceLogMessageGroup"/> objects.
/// </summary>
public class TraceLogMessageGroupComparer : IEqualityComparer<TraceLogMessageGroup>
{
	/// <summary>
	/// Determines whether two <see cref="TraceLogMessageGroup"/> objects are equal.
	/// </summary>
	/// <param name="groupOne">The first <see cref="TraceLogMessageGroup"/> to compare.</param>
	/// <param name="groupTwo">The second <see cref="TraceLogMessageGroup"/> to compare.</param>
	/// <returns><see langword="true"/> if the specified <see cref="TraceLogMessageGroup"/> objects are equal; otherwise, <see langword="false"/>.</returns>
	public bool Equals(TraceLogMessageGroup groupOne, TraceLogMessageGroup groupTwo)
	{
		if (groupTwo == null)
			return false;

		// Compare parent entry
		if (!groupOne.ParentEntry.Equals(groupTwo.ParentEntry))
			return false;

		// Compare child entries
		return StructuralComparisons.StructuralEqualityComparer.Equals(groupOne.ChildEntries.ToArray(), groupTwo.ChildEntries.ToArray());
	}

	/// <summary>
	/// Returns a hash code for the specified <see cref="TraceLogMessageGroup"/> object.
	/// </summary>
	/// <param name="obj">The <see cref="TraceLogMessageGroup"/> for which a hash code is to be returned.</param>
	/// <returns>A hash code for the specified <see cref="TraceLogMessageGroup"/> object.</returns>
	public int GetHashCode(TraceLogMessageGroup obj)
	{
		// Implement hash code generation based on object's properties
		// This method is optional but recommended for hash-based collections
		return HashCode.Combine(obj.ParentEntry, obj.ChildEntries);
	}
}