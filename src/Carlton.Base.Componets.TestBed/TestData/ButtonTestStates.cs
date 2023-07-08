using System.Reflection.Metadata;

namespace Carlton.Base.Components.TestBed;

internal static class ButtonTestStates
{
    public static object DefaultState
    {
        get => new
        {
            Icon = "delete",
            PositionBottom = 75,
            PositionRight = 50
        };
    }
}
