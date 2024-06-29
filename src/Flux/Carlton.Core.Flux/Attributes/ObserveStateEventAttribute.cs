namespace Carlton.Core.Flux.Attributes;

/// <summary>
/// Specifies that the annotated class observes a state event in the Flux framework.
/// </summary>
/// <remarks>
/// This attribute is used to mark classes that observe specific state events in the Flux framework.
/// It initializes a new instance of the <see cref="ObserveStateEventAttribute"/> class
/// with the specified state event name.
/// </remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class ObserveStateEventAttribute(string stateEvent) : Attribute
{
	/// <summary>
	/// Gets the name of the observed state event.
	/// </summary>
	public string StateEvent { get; } = stateEvent;
}
