using Carlton.Core.Flux.Debug.Extensions;
namespace Carlton.Core.Flux.Debug.Pages;

internal sealed class LoadLogMessagesMutation : IFluxStateMutation<FluxDebugState, LoadLogMessagesCommand>
{
	public string StateEvent => FluxDebugStateEvents.LoadLogMessages.ToString();

	public FluxDebugState Mutate(FluxDebugState state, LoadLogMessagesCommand command)
	{
		var id = 0;
		return state with
		{
			LogMessages = command.LogMessages.Select(log => new FluxDebugLogMessage
			{
				Id = id++,
				Message = log.Message,
				LogLevel = log.LogLevel,
				EventId = log.EventId,
				Exception = log.Exception?.ToExceptionSummary(),
				Timestamp = log.Timestamp,
				Scopes = log.Scopes
			}).ToList()
		};
	}
}
