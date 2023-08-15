namespace Carlton.Core.Components.Flux.Decorators.Queries;

public class ViewModelValidationDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;
    private readonly ILogger<ViewModelValidationDecorator<TState>> _logger;

    public ViewModelValidationDecorator(IViewModelQueryDispatcher<TState> decorated, IServiceProvider provider, ILogger<ViewModelValidationDecorator<TState>> logger)
        => (_decorated, _provider, _logger) = (decorated, provider, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken)
    {
        Log.ViewModelValidationStarted(_logger, typeof(TViewModel).GetDisplayName());
        var validator = _provider.GetService<IValidator<TViewModel>>();
        var vm = await _decorated.Dispatch<TViewModel>(query, cancellationToken);
        validator.ValidateAndThrow(vm);
        Log.ViewModelValidationCompleted(_logger, typeof(TViewModel).GetDisplayName());
        return vm;
    }
}
