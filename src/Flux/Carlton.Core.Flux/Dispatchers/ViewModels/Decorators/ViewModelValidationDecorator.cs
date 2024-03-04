namespace Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;

public class ViewModelValidationDecorator<TState>(IViewModelQueryDispatcher<TState> _decorated) : IViewModelQueryDispatcher<TState>
{
    public Task<Result<TViewModel, ViewModelFluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        var vm = _decorated.Dispatch(sender, context, cancellationToken);
        var isValid = vm.TryValidate(out var validationErrors);
        context.MarkAsValidated(validationErrors);

        //Log Here
        var validationString = string.Join(Environment.NewLine, validationErrors.Select(result => result));
        if (!isValid)
            return Task.FromResult((Result<TViewModel, ViewModelFluxError>)new Errors.ViewModelQueryErrors.ValidationError(typeof(TViewModel), validationErrors));

        return vm;
    }
}
