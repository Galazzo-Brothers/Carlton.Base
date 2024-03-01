using Polly;
using Polly.Wrap;
namespace Carlton.Core.Utilities.Resilience;

public interface IResiliencePolicyHandler<T>
{
    AsyncPolicyWrap<T> CreatePolicyWrap();
    void HandleResult(PolicyResult<T> result);
}

