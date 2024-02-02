namespace Carlton.Core.Flux.Debug.Extensions;

internal static class TraceLogCollectionExtensions
{
    internal static int GetIndex(this IEnumerable<TraceLogMessageGroup> groups, TraceLogMessage log)
    {
        var index = 0;
        foreach (var group in groups)
        {
            if (group.ParentEntry == log)
                return index;

            index++;

            foreach (var child in group.ChildEntries)
            {
                if (child == log)
                    return index;

                index++;
            }
        }

        return -1;
    }

    internal static TraceLogMessage? GetElementAtIndex(this IEnumerable<TraceLogMessageGroup> groups, int targetIndex)
    {
        var flattenedEntries = groups.SelectMany(_ => _.FlattenedEntries);
        
        if(targetIndex < flattenedEntries.Count())
            return flattenedEntries.ElementAt(targetIndex);

        return default;
    }

}
