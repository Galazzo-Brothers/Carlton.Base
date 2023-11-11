namespace Carlton.Core.Components.Flux.Decorators.Queries;

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
            Log.ViewModelValidationStarted(_logger, typeof(TViewModel).GetDisplayName());
            var validator = _provider.GetService<IValidator<TViewModel>>();
            var vm = await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
            validator.ValidateAndThrow(vm);
            Log.ViewModelValidationCompleted(_logger, typeof(TViewModel).GetDisplayName());
            return vm;
        }
        catch (ValidationException ex)
        {
            Log.ViewModelValidationError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.ValidationError(query, ex);
        }
    }
}
