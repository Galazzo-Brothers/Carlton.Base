namespace Carlton.Base.State;

public class CarltonDataWrapperContext<TViewModel>
{
    public TViewModel ViewModel { get; set; }
    public Func<object, ICommand, Task> OnComponentEvent { get; init; }

    public CarltonDataWrapperContext(TViewModel vm, Func<object, ICommand, Task> onComponentEvent)
    {
        ViewModel = vm;
        OnComponentEvent = onComponentEvent;
    }
}
