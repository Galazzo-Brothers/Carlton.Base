using Carlton.Core.Components.Toasts;

namespace Carlton.Core.Components.Layouts.Toasts;

public class ToastViewModel
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; } 
    public required ToastTypes ToastType { get; init; }
    public required bool FadeOutEnabled { get; init; }
    public bool IsDismissed { get; private set; }

    public void MarkAsDismissed()
        => IsDismissed = true;
}
