namespace Carlton.Core.Components.Flux.Decorators.Commands;

//public class CommandValidationDecorator : ICommandDispatcher
//{
//    private readonly ICommandDispatcher _decorated;
//    private readonly IServiceProvider _provider;
//    private readonly ILogger<CommandValidationDecorator> _logger;

//    public CommandValidationDecorator(ICommandDispatcher decorated, IServiceProvider provider, ILogger<CommandValidationDecorator> logger)
//        => (_decorated, _provider, _logger) = (decorated, provider, logger);

//    public async Task<Unit> Dispatch<TCommand>(ComponentCommandRequest<TCommand> request, CancellationToken cancellationToken)
//    {
//        try
//        {
//            Log.CommandRequestValidationStarted(_logger, request.DisplayName, request);
//            var validator = _provider.GetService<IValidator<TCommand>>();
//            validator.ValidateAndThrow(request.Command);
//            request.MarkAsValidated();
//            Log.CommandRequestValidationCompleted(_logger, request.DisplayName, request);
//            return await _decorated.Dispatch(request, cancellationToken);
//        }
//        catch (ValidationException ex)
//        {
//            request.MarkErrored(LogEvents.Command_Validation_Error);
//            Log.CommandRequestValidationError(_logger, ex, request.DisplayName, request);
//            throw;
//        }
//    }
//}
