namespace Carlton.Base.State;

public interface IViewModelStateAdapter<TViewModel>
{
    public bool ShouldRefreshState();
    public void SaveViewModel(TViewModel vm);
    public TViewModel GetViewModel();
}