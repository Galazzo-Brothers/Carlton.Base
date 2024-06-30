namespace Carlton.Core.Flux;

/// <summary>
/// Represents the HTTP verbs used in server communication.
/// </summary>
public enum HttpVerb
{
	/// <summary>
	/// The GET method requests a representation of the specified resource.
	/// </summary>
	GET,

	/// <summary>
	/// The POST method is used to submit data to be processed to a specified resource.
	/// </summary>
	POST,

	/// <summary>
	/// The PUT method replaces all current representations of the target resource with the request payload.
	/// </summary>
	PUT,

	/// <summary>
	/// The PATCH method is used to apply partial modifications to a resource.
	/// </summary>
	PATCH,

	/// <summary>
	/// The DELETE method deletes the specified resource.
	/// </summary>
	DELETE
}

/// <summary>
/// Represents the communication policy for Flux server communication attributes.
/// </summary>
public enum FluxServerCommunicationPolicy
{
	/// <summary>
	/// Indicates that the server communication should never occur.
	/// </summary>
	Never,

	/// <summary>
	/// Indicates that the server communication should always occur.
	/// </summary>
	Always
}

/// <summary>
/// Represents the kind of Flux operation.
/// </summary>
public enum FluxOperationKind
{
	/// <summary>
	/// Represents a view model query operation.
	/// </summary>
	ViewModelQuery = 1,

	/// <summary>
	/// Represents a mutation command operation.
	/// </summary>
	MutationCommand = 2,
}