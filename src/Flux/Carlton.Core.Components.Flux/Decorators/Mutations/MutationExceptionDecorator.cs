using Carlton.Core.Components.Flux.Attributes;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Decorators.Commands;

public class MutationExceptionDecorator<TState> : IMutationCommandDispatcher<TState>
{
    private readonly IMutationCommandDispatcher<TState> _decorated;
    private readonly ILogger<MutationExceptionDecorator<TState>> _logger;

    public MutationExceptionDecorator(IMutationCommandDispatcher<TState> decorated, ILogger<MutationExceptionDecorator<TState>> logger)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<Unit> Dispatch<TCommand>(object sender, TCommand command, CancellationToken cancellationToken)
        where TCommand : MutationCommand
    {
        var displayName = typeof(TCommand).GetDisplayName();
       
        try
        {
            using (_logger.BeginScope(Log.MutationScopeMessage, displayName, command.CommandID))
            Log.MutationStarted(_logger, displayName, command);
            await _decorated.Dispatch(sender,command, cancellationToken);
            Log.MutationCompleted(_logger, displayName);
            return Unit.Value;
        }
        catch (JsonException ex)
        {
            Log.MutationJsonError(_logger, ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.JsonError(command, ex);
        }
        catch (HttpRequestException ex)
        {
            Log.MutationHttpInterceptionError(_logger, ex, displayName);
            throw MutationCommandFluxException<TState, TCommand>.HttpError(command, ex);
        }
        catch (JSException ex)
        {
            Log.ViewModelJsInteropRefreshError(_logger, ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.JSInteropError(command, ex);
        }
        catch (ValidationException ex)
        {
            Log.MutationValidationError(_logger, ex, displayName);
            throw MutationCommandFluxException<TState, TCommand>.ValidationError(command, ex);
        }
        catch (ArgumentException ex) when (ex.ParamName == nameof(HttpRefreshAttribute) || ex.ParamName == nameof(HttpRefreshParameterAttribute))
        {
            //URL Construction Errors
            Log.MutationHttpUrlError(_logger, ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpUrlError(command, ex);
        }
        catch (ArgumentException ex) when (ex.ParamName == typeof(HttpResponseTypeAttribute<>).GetDisplayName() || ex.ParamName == nameof(HttpResponsePropertyAttribute))
        {
            //Response Update Errors
            Log.MutationHttpResponseUpdateError(_logger, ex, typeof(TCommand).GetDisplayName());
            throw MutationCommandFluxException<TState, TCommand>.HttpResponseUpdateError(command, ex);
        }
        catch (Exception ex)
        {
            Log.MutationUnhandledError(_logger, ex, displayName);
            throw new MutationCommandFluxException<TState, TCommand>(command, ex);
        }
    }
}
