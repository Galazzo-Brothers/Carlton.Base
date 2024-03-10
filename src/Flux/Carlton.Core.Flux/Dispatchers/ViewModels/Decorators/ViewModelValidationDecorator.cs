namespace Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;

public class ViewModelValidationDecorator<TState>(
	IViewModelQueryDispatcher<TState> _decorated)
	: IViewModelQueryDispatcher<TState>
{
	public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		//Retrieve ViewModel
		var vmResult = await _decorated.Dispatch(sender, context, cancellationToken);

		//Validate ViewModel and return result
		return vmResult.Match
		(
			vm => ValidateViewModelResult(vm, context),
			err => err.ToResult<TViewModel, FluxError>()
		);
	}

	private static Result<TViewModel, FluxError> ValidateViewModelResult<TViewModel>(TViewModel vm, ViewModelQueryContext<TViewModel> context)
	{
		//Validate ViewModel
		var isValid = vm.TryValidate(out var validationErrors);

		//Continue with valid ViewModel
		if (isValid)
		{
			context.MarkAsValid();
			return vm;
		}

		//Return Error
		context.MarkAsInvalid(validationErrors);
		return ValidationError(validationErrors).ToResult<TViewModel, FluxError>();

	}
}
