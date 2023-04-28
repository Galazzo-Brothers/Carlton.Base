namespace Carlton.Base.State;

public class ViewModelStateFacadeBase<TState> : IViewModelStateFacade
{
    private readonly TState _state;

    public ViewModelStateFacadeBase(TState state)
        => _state = state;

    public TViewModel GetViewModel<TViewModel>()
    {
        return _state.Adapt<TViewModel>();
    }
}
