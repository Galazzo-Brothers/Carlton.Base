using System.Text;
namespace Carlton.Core.Flux.Exceptions;

/// <summary>
/// Represents an exception specific to view model queries in the Flux framework.
/// </summary>
/// <typeparam name="TViewModel">The type of view model associated with the exception.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="ViewModelQueryFluxException{TViewModel}"/> class with the specified message, event ID, and inner exception.
/// </remarks>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="eventId">The event ID associated with the exception.</param>
/// <param name="innerException">The exception that caused the current exception.</param>
public class ViewModelQueryFluxException<TViewModel>(
	string message,
	int eventId,
	Exception innerException) : FluxException(message, eventId, FluxOperationKind.ViewModelQuery, innerException)
{
	/// <summary>
	/// Gets the display name of the type of view model.
	/// </summary>
	public string ViewModelTypeName { get => typeof(TViewModel).GetDisplayName(); }

	/// <summary>
	/// Initializes a new instance of the <see cref="ViewModelQueryFluxException{TViewModel}"/> class with the specified message, event ID, and inner exception.
	/// </summary>
	/// <param name="message">The error message that explains the reason for the exception.</param>
	/// <param name="eventId">The event ID associated with the exception.</param>
	public ViewModelQueryFluxException(
		string message,
		int eventId)
		: this(message, eventId, null)
	{
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	public override string ToString()
	{
		var sb = new StringBuilder(base.ToString());
		sb.AppendLine($"ViewModel Type Name: {ViewModelTypeName}");
		return sb.ToString();
	}
}