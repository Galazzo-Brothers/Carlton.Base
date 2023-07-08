
namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ViewModelEndpointParameterAttribute : Attribute
{
    public string Name { get; init; }
    public DataEndpointParameterType ParameterType { get; init; }
    public string DestinationPropertyName { get; init; }

    public ViewModelEndpointParameterAttribute(string name, DataEndpointParameterType parameterType) : this(name, parameterType, name)
    {
    }

    public ViewModelEndpointParameterAttribute(string name, DataEndpointParameterType parameterType, string destinationPropertyName)
        => (Name, ParameterType, DestinationPropertyName) = (name, parameterType, destinationPropertyName);
}


