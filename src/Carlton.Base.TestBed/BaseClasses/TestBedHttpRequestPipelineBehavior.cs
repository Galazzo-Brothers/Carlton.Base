using Microsoft.Extensions.Logging;

namespace Carlton.Base.TestBed;

public class TestBedHttpRequestPipelineBehavior<TRequest, TViewModel> : HttpRequestPipelineBehaviorBase<TRequest, TViewModel>
    where TRequest : ViewModelRequest<TViewModel>
{
    public TestBedHttpRequestPipelineBehavior(HttpClient client, ICommandProcessor commandProcessor) : base(client, commandProcessor)
    {
    }
}
