namespace Carlton.Base.State;

public class CarltonDataWrapperContext<TViewModel>
{
    public TViewModel ViewModel { get; set; }
    public Func<ICarltonComponent<TViewModel>, IComponentEvent<TViewModel>, Task> OnComponentEvent { get; init; }

    public CarltonDataWrapperContext(TViewModel vm, Func<ICarltonComponent<TViewModel>, IComponentEvent<TViewModel>, Task> onComponentEvent)
    {
        ViewModel = vm;
        OnComponentEvent = onComponentEvent;
    }
}
