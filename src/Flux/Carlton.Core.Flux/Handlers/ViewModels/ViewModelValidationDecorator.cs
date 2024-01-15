using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelValidationDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly IServiceProvider _provider;

    public ViewModelValidationDecorator(IViewModelQueryDispatcher<TState> decorated, IServiceProvider provider)
        => (_decorated, _provider) = (decorated, provider);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var validator = _provider.GetService<IValidator<TViewModel>>();
        var vm = await _decorated.Dispatch(sender, context, cancellationToken);
        validator.ValidateAndThrow(vm);
        context.MarkAsValidated();
        return vm;
    }
}
