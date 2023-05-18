namespace Carlton.Base.TestBed;

public class TestBedHttpBehavior<TRequest, TViewModel> : HttpRequestPipelineBehaviorBase<TRequest, TViewModel>
    where TRequest : ViewModelRequest<TViewModel>
{
    public TestBedHttpBehavior(HttpClient client, ILogger<TestBedHttpBehavior<TRequest, TViewModel>> logger)
        : base(client, logger)
    {
    }
}
