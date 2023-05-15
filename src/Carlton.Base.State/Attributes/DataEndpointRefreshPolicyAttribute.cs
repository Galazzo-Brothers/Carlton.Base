namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataEndpointRefreshPolicyAttribute : Attribute
{
    public bool InitialRequestOccurred { get; set; }
    public DataEndpointRefreshPolicy DataEndpointRefreshPolicy { get; init; }

    public DataEndpointRefreshPolicyAttribute(DataEndpointRefreshPolicy policy) 
        => DataEndpointRefreshPolicy = policy;
}
