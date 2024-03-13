using System.Runtime.CompilerServices;
namespace Carlton.Core.Flux.Attributes;

/// <summary>
/// Specifies that the annotated property or field is a parameter for server communication settings in the Flux framework.
/// </summary>
/// <remarks>
/// This attribute is used to mark properties or fields within view models or mutation command classes as parameters
/// for server communication settings.
/// </remarks>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class FluxServerCommunicationParameterAttribute([CallerMemberName] string parameterName = null) : Attribute
{
	public string ParameterName { get; init; } = parameterName;
}