namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelEndpointRefreshPolicyAttribute : Attribute
{
    public bool InitialRequestOccurred { get; set; }
    public DataEndpointRefreshPolicy DataEndpointRefreshPolicy { get; init; }

    public ViewModelEndpointRefreshPolicyAttribute(DataEndpointRefreshPolicy policy) 
        => DataEndpointRefreshPolicy = policy;
}
