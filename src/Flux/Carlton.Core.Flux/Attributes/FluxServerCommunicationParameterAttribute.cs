using System.Runtime.CompilerServices;
namespace Carlton.Core.Flux.Attributes;


[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class FluxServerCommunicationParameterAttribute([CallerMemberName] string parameterName = null) : Attribute
{
    public string ParameterName { get; init; } = parameterName;
}