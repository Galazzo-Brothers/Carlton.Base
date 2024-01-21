
namespace Carlton.Core.Components.Layouts;

public record class LayoutManagerCascadingState
{
    public required bool IsFullScreen { get; init; }
    public IDictionary<string, object> LayoutSettings { get; init; }
}
