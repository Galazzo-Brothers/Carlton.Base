namespace Carlton.Core.Foundation.Web.ViewState;

public interface IViewStateService<T>
{
    public T? ViewState { get; }
    public void UpdateViewState(T viewState);
}


public class ViewStateService<T>(T? viewState) : IViewStateService<T>
{
    public T? ViewState { get; private set; } = viewState;

    public void UpdateViewState(T viewState)
        => ViewState = viewState;

    public ViewStateService()
        : this(default)
    {
    }
}
