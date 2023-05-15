namespace Carlton.Base.State;

public class ViewModelRequestExceptionBehavior<TRequest, TViewModel> : IPipelineBehavior<TRequest, TViewModel>
    where TRequest : ViewModelRequest<TViewModel>
{
    private readonly ILogger<ViewModelRequestExceptionBehavior<TRequest, TViewModel>> _logger;

    public ViewModelRequestExceptionBehavior(ILogger<ViewModelRequestExceptionBehavior<TRequest, TViewModel>> logger)
    {
        _logger = logger;
    }

    public async Task<TViewModel> Handle(TRequest request, RequestHandlerDelegate<TViewModel> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"Handled {typeof(TRequest).Name}");

            return response;
        }
        catch(Exception ex)
        {
            throw new ViewModelRequestException<TViewModel>(request, ex);
        }
    }
}
