namespace Carlton.Base.State;

public class UtilityViewModelDecorator : IViewModelDispatcher 
{
    private readonly IViewModelDispatcher _decorated;
    private readonly ILogger<UtilityViewModelDecorator> _logger;

    public UtilityViewModelDecorator(IViewModelDispatcher decorated, ILogger<UtilityViewModelDecorator> logger)
        => (_decorated, _logger) = (decorated, logger);

    public Task<TViewModel> Dispatch<TViewModel>(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken)
    {
        try
        {
            Log.ViewModelRequestHandlingStarted(_logger, request.DisplayName, request);
            var response = _decorated.Dispatch(request, cancellationToken);
            request.MarkCompleted();
            Log.ViewModelRequestHandlingCompleted(_logger, request.DisplayName, request);

            return response;
        }
        catch(Exception ex)
        {
            request.MarkErrored();
            throw new ViewModelRequestException<TViewModel>(request, ex);
        }
    }
}
