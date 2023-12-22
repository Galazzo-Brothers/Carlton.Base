namespace Carlton.Core.Components.Flux.Handlers.ViewModels;

public class ViewModelValidationDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;
    private readonly ILogger<ViewModelValidationDecorator<TState>> _logger;

    public ViewModelValidationDecorator(IViewModelQueryDispatcher<TState> decorated, IServiceProvider provider, ILogger<ViewModelValidationDecorator<TState>> logger)
        => (_decorated, _provider, _logger) = (decorated, provider, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var validator = _provider.GetService<IValidator<TViewModel>>();
            var vm = await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
            _logger.ViewModelValidationStarted(typeof(TViewModel).GetDisplayName());
            validator.ValidateAndThrow(vm);
            _logger.ViewModelValidationCompleted(typeof(TViewModel).GetDisplayName());
            return vm;
        }
        catch (ValidationException ex)
        {
            _logger.ViewModelValidationError(ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.ValidationError(query, ex);
        }
    }
}
