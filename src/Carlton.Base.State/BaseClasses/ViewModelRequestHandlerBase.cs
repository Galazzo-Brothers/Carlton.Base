namespace Carlton.Base.State;

public abstract class ViewModelRequestHandlerBase<TViewModel>
    : IRequestHandler<ViewModelRequest<TViewModel>, TViewModel>
{
    protected IViewModelStateFacade State { get; init; }

    protected ViewModelRequestHandlerBase(IViewModelStateFacade state, ILogger<ViewModelRequestHandlerBase<TViewModel>> logger)
        => (State, _logger) = (state, logger);

    private readonly ILogger<ViewModelRequestHandlerBase<TViewModel>> _logger;

    public Task<TViewModel> Handle(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling {RequestName} : {Request}", request.RequestName, request);
            var response = Task.FromResult(State.GetViewModel<TViewModel>());
            _logger.LogInformation("Handled {RequestName} : {Request}", request.RequestName, request);

            return response;
        }
        catch(Exception ex)
        {
            throw new ViewModelRequestException<TViewModel>(request, ex);
        }
    }
}


