//namespace Carlton.Base.Infrastructure.PipelineBehaviors;

//public abstract class BasePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
//    where TRequest : IRequest<TResponse>
//{
//    protected ILogger Logger { get; }

//    protected string RequestType
//    {
//        get { return $"{typeof(TRequest).Name} Request"; }
//    }

//    protected BasePipelineBehavior(ILogger logger)
//    {
//        Logger = logger;
//    }

//    public abstract Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
//}
