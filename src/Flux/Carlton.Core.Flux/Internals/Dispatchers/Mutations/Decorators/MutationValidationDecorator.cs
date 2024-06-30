namespace Carlton.Core.Flux.Internals.Dispatchers.Mutations.Decorators;

internal sealed class MutationValidationDecorator<TState>(IMutationCommandDispatcher<TState> _decorated)
	: IMutationCommandDispatcher<TState>
{
	public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		//Validate command
		var isValid = context.MutationCommand.TryValidate(out var validationErrors);
		context.MarkAsInvalid(validationErrors);

		//Continue with valid command
		if (isValid)
		{
			context.MarkAsValid();
			return await _decorated.Dispatch(sender, context, cancellationToken);
		}

		//Return ValidationError
		context.MarkAsInvalid(validationErrors);
		return ValidationError(validationErrors);
	}
}
