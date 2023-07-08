namespace Carlton.Base.State;

public interface IViewModelStateFacade
{
    public TViewModel GetViewModel<TViewModel>();
}
