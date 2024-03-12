namespace Carlton.Core.Flux.Internals.Dispatchers.Mutations.Decorators;

internal class MutationLocalStorageDecorator<TState>(
	IMutationCommandDispatcher<TState> decorated,
	IFluxState<TState> fluxState,
	IBrowserStorageService<TState> browserStorage)
	: IMutationCommandDispatcher<TState>
{
	private readonly IMutationCommandDispatcher<TState> _decorated = decorated;
	private readonly IFluxState<TState> _fluxState = fluxState;
	private readonly IBrowserStorageService<TState> _browserStorage = browserStorage;

	public async Task<Result<MutationCommandResult, FluxError>> Dispatch<TCommand>(object sender, MutationCommandContext<TCommand> context, CancellationToken cancellationToken)
	{
		try
		{
			//Finish the dispatch
			var result = await _decorated.Dispatch(sender, context, cancellationToken);

			//return if there was an error processing mutation command
			if (!result.IsSuccess)
				return result;

			//Save to local storage
			await _browserStorage.SaveState(_fluxState.CurrentState);

			//Complete the LocalStorage Interception
			context.MarkAsCommittedToLocalStorage();

			return result;
		}
		catch (JsonException ex)
		{
			//Error Serializing JSON
			return LocalStorageError(ex);
		}
		catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
		{
			//Error Serializing JSON
			return LocalStorageError(ex);
		}
		catch (Exception ex)
		{
			//Unhandled local storage error
			return LocalStorageError(ex);
		}
	}
}

