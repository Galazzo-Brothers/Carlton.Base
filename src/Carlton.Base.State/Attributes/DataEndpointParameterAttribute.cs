
namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DataEndpointParameterAttribute : Attribute
{
    public string Name { get; init; }
    public DataEndpointParameterType ParameterType { get; init; }
    public string DestinationPropertyName { get; init; }

    public DataEndpointParameterAttribute(string name, DataEndpointParameterType parameterType) : this(name, parameterType, name)
    {
    }

    public DataEndpointParameterAttribute(string name, DataEndpointParameterType parameterType, string destinationPropertyName)
        => (Name, ParameterType, DestinationPropertyName) = (name, parameterType, destinationPropertyName);
}


