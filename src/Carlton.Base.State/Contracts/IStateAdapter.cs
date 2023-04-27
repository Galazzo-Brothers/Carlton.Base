namespace Carlton.Base.State;

public interface IStateAdapter<TViewModel>
{
    public TViewModel GetViewModel();
    public void SaveViewModel(TViewModel viewModel);
    public bool IsEmpty { get; }
}
