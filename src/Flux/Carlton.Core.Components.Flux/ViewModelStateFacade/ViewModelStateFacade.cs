using Mapster;
namespace Carlton.Core.Components.Flux;

public class ViewModelStateFacade<TState> : IViewModelStateFacade
{
    private readonly TState _state;
    private readonly ILogger<ViewModelStateFacade<TState>> _logger;

    public ViewModelStateFacade(TState state, ILogger<ViewModelStateFacade<TState>> logger)
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
        catch (Exception ex)
        {
            Log.ViewModelRequestRetrievingViewModelError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw;
        }
    }
}
