namespace Carlton.Base.State;

public interface ICarltonComponent<TViewModel>
{
    TViewModel ViewModel { get; set; }
    EventCallback OnComponentEvent { get; init; }
}
