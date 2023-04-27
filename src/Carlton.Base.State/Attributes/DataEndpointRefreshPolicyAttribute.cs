namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataEndpointRefreshPolicyAttribute : Attribute
{
    public DataEndpointRefreshPolicy DataEndpointRefreshPolicy { get; init; }

    public DataEndpointRefreshPolicyAttribute(DataEndpointRefreshPolicy policy) 
        => DataEndpointRefreshPolicy = policy;
}
