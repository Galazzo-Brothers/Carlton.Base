namespace Carlton.Core.Flux.Attributes;

/// <summary>
/// Base attribute for defining server communication settings for Flux server communication.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public abstract class FluxServerUrlAttribute : Attribute
{
	/// <summary>
	/// Gets or sets the server URL for communication.
	/// </summary>
	public string ServerUrl { get; init; }

	/// <summary>
	/// Gets or sets the HTTP verb used for communication.
	/// </summary>
	public HttpVerb HttpVerb { get; init; }

	/// <summary>
	/// Gets or sets the server communication policy.
	/// </summary>
	public FluxServerCommunicationPolicy ServerCommunicationPolicy { get; init; }
		= FluxServerCommunicationPolicy.Always;

	/// <summary>
	/// Gets or sets a value indicating whether to update with response body.
	/// </summary>
	public bool UpdateWithResponseBody { get; init; } = false;

	/// <summary>
	/// Initializes a new instance of the <see cref="FluxServerUrlAttribute"/> class with the specified server URL and HTTP verb.
	/// </summary>
	/// <param name="serverUrl">The server URL for communication.</param>
	/// <param name="httpVerb">The HTTP verb used for communication.</param>
	public FluxServerUrlAttribute(string serverUrl, HttpVerb httpVerb)
		=> (ServerUrl, HttpVerb) = (serverUrl, httpVerb);
}

/// <summary>
/// Attribute for defining server communication settings for Flux view model communication.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class FluxViewModelServerUrlAttribute(string serverUrl)
	: FluxServerUrlAttribute(serverUrl, HttpVerb.GET)
{
}

/// <summary>
/// Attribute for defining server communication settings for Flux mutation command communication.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class FluxCommandServerUrlAttribute<TCommand>(string serverUrl, HttpVerb httpVerb)
	: FluxServerUrlAttribute(serverUrl, httpVerb)
{
}