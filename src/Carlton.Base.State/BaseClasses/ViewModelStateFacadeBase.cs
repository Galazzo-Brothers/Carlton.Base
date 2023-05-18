namespace Carlton.Base.State;

public class ViewModelStateFacadeBase<TState> : IViewModelStateFacade
{
    private readonly TState _state;
    private readonly ILogger<ViewModelStateFacadeBase<TState>> _logger;

    public ViewModelStateFacadeBase(TState state, ILogger<ViewModelStateFacadeBase<TState>> logger)
        => (_state, _logger) = (state, logger);

    public TViewModel GetViewModel<TViewModel>()
    {
        try
        {
            var viewModelName = typeof(TViewModel).GetDisplayName();
            Log.ViewModelRequestRetrievingViewModelStarted(_logger, viewModelName);
            var result = _state.Adapt<TViewModel>();
            Log.ViewModelRequestRetrievingViewModelCompleted(_logger, viewModelName, result);
            return result;
        }
        catch(Exception ex)
        {
            Log.ViewModelRequestRetrievingViewModelError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw;
        }
    }
}
