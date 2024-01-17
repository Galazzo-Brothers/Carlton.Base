using Carlton.Core.Flux.Extensions;
namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelValidationDecorator<TState>(IViewModelQueryDispatcher<TState> _decorated) : IViewModelQueryDispatcher<TState>
{
    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var vm = await _decorated.Dispatch(sender, context, cancellationToken);
        var isValid = vm.TryValidate(out var validationErrors);
        context.MarkAsValidated(validationErrors);
        
        if (!isValid)
            throw new ValidationException(string.Join(Environment.NewLine, validationErrors.Select(result => result)));

        return vm;
    }
}
