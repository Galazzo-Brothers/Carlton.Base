using Carlton.Core.Components.Flux.Attributes;

namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ViewModelHttpRefreshParameterAttribute : Attribute
{
    public string Name { get; init; }
    public DataEndpointParameterType ParameterType { get; init; }
    public string DestinationPropertyName { get; init; }

    public ViewModelHttpRefreshParameterAttribute(string name, DataEndpointParameterType parameterType) : this(name, parameterType, name)
    {
    }

    public ViewModelHttpRefreshParameterAttribute(string name, DataEndpointParameterType parameterType, string destinationPropertyName)
        => (Name, ParameterType, DestinationPropertyName) = (name, parameterType, destinationPropertyName);
}


