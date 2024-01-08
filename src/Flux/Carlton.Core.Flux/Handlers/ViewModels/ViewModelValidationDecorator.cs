using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelValidationDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;
    private readonly ILogger<ViewModelValidationDecorator<TState>> _logger;

    public ViewModelValidationDecorator(IViewModelQueryDispatcher<TState> decorated, IServiceProvider provider, ILogger<ViewModelValidationDecorator<TState>> logger)
        => (_decorated, _provider, _logger) = (decorated, provider, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        try
        {
            var validator = _provider.GetService<IValidator<TViewModel>>();
            var vm = await _decorated.Dispatch(sender, context, cancellationToken);
            validator.ValidateAndThrow(vm);
            context.MarkAsValidated();
            _logger.ViewModelValidationCompleted(context.ViewModelType);
            return vm;
        }
        catch (ValidationException ex)
        {
            context.MarkAsErrored();
            _logger.ViewModelValidationError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.ValidationError(context, ex);
        }
    }
}
