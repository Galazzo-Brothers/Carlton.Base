﻿namespace Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;

public class ViewModelValidationDecorator<TState>(
	IViewModelQueryDispatcher<TState> _decorated)
	: IViewModelQueryDispatcher<TState>
{
	public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
	{
		var vmResult = await _decorated.Dispatch(sender, context, cancellationToken);

		return vmResult.Match
		(
			vm => ValidateViewModelResult(vm, context),
			err => err.ToResult<TViewModel, FluxError>()
		);
	}

	private Result<TViewModel, FluxError> ValidateViewModelResult<TViewModel>(TViewModel vm, ViewModelQueryContext<TViewModel> context)
	{
		//Validate ViewModel
		var isValid = vm.TryValidate(out var validationErrors);
		context.MarkAsValidated(validationErrors);

		//Continue with valid ViewModel
		if (isValid)
			return vm;

		//Return Error
		return ValidationError(validationErrors).ToResult<TViewModel, FluxError>();
	}
}
