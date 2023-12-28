namespace Carlton.Core.Components.Toasts;

public record ToastViewModel
{
    public string Title { get; init; }
    public string Message { get; init; }
    public ToastTypes ToastType { get; init; }
    public bool FadeOutEnabled { get; init; }
    public int ToastIndex { get; init; }
    public Action OnDismiss { get; init; }
}
