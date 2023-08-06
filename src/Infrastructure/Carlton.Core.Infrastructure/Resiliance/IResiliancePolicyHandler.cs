namespace Carlton.Core.Infrastructure.Resiliance;

public interface IResiliancePolicyHandler<T>
{
    AsyncPolicyWrap<T> CreatePolicyWrap();
    void HandleResult(PolicyResult<T> result);
}

