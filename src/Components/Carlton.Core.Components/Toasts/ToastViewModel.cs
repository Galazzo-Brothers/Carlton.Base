namespace Carlton.Core.Components.Toasts;

public class ToastViewModel(int ToastIndex, string Title, string Message, ToastTypes ToastType)
{
    public string Title { get; init; } = Title;
    public string Message { get; init; } = Message;
    public ToastTypes ToastType { get; init; } = ToastType;
    public bool FadeOutEnabled { get; init; } = true;
    public int ToastIndex { get; init; } = ToastIndex;

    public bool Toasted { get; private set; } = false;

    public void MarkAsToasted()
     => Toasted = true;
}
