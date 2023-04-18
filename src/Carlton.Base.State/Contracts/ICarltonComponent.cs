namespace Carlton.Base.State;

public interface ICarltonComponent<TViewModel>
{
    TViewModel ViewModel { get; set; }
    EventCallback<IComponentEvent<TViewModel>> OnComponentEvent { get; init; }
}
