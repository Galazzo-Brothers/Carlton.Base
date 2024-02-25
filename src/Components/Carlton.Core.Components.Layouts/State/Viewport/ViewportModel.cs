namespace Carlton.Core.Components.Layouts.State.Viewport;

public sealed record ViewportModel(double Height, double Width)
{
    public const double MobileMaxWidth = 767.98;
    public bool IsMobile { get => Width <= MobileMaxWidth; }
}

