namespace Carlton.Base.State;

public interface IDataComponent<TViewModel>
{
    TViewModel ViewModel { get; set; }
    Func<Task<TViewModel>> GetViewModel { get; init; }
    EventCallback<ICommand> OnComponentEvent { get; init; }
}
