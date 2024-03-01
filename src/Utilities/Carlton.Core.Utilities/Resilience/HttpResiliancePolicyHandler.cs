using System.Net.Http;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Wrap;
using System.Net;

namespace Carlton.Core.Utilities.Resilience;

public class HttpResiliencePolicyHandler(ILogger<HttpResiliencePolicyHandler> logger) : IResiliencePolicyHandler<HttpResponseMessage>
{
    private readonly ILogger<HttpResiliencePolicyHandler> _logger = logger;

    public AsyncPolicyWrap<HttpResponseMessage> CreatePolicyWrap()
    {
        HttpStatusCode[] httpStatusCodesWorthRetrying = {
           HttpStatusCode.RequestTimeout, // 408
           HttpStatusCode.InternalServerError, // 500
           HttpStatusCode.BadGateway, // 502
           HttpStatusCode.ServiceUnavailable, // 503
           HttpStatusCode.GatewayTimeout // 504
        };

        var policy = Policy
           .Handle<HttpRequestException>()
           .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
           .RetryAsync(3, (exception, retryCount, context) =>
           {
               var methodThatRaisedException = context["methodName"];
               _logger.LogWarning(exception.Exception, $"Exception occurred in method {methodThatRaisedException}, retrying HTTP call. Retry Count {retryCount}");
           });

        var policyWrap = Policy.WrapAsync(policy);

        return policyWrap;
    }

    public void HandleResult(PolicyResult<HttpResponseMessage> policyResult)
    {
        if (policyResult.Outcome == OutcomeType.Failure)
        {
            var requestUrl = policyResult.Result.RequestMessage.RequestUri;
            var message = $"Failed to reach remote server: {requestUrl} with resilience policy";
            _logger.LogWarning($"{message}, throwing exception");
            throw new HttpRequestException(message, policyResult.FinalException, policyResult.Result.StatusCode);
        }
    }
}
