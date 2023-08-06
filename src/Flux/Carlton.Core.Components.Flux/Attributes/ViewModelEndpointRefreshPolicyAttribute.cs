namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelEndpointRefreshPolicyAttribute : Attribute
{
    public bool InitialRequestOccurred { get; set; }
    public DataEndpointRefreshPolicy DataEndpointRefreshPolicy { get; init; }

    public ViewModelEndpointRefreshPolicyAttribute(DataEndpointRefreshPolicy policy) 
        => DataEndpointRefreshPolicy = policy;
}
