namespace Carlton.Core.Components.Layouts.State.Viewport;

public record ViewportModel
{
    public const double MobileMaxWidth = 767.98;
    public double Height { get; init; }
    public double Width { get; init; }
    public bool IsMoible { get => Width <= MobileMaxWidth; }
}

