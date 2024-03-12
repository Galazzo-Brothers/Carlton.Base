using System.Text;
namespace Carlton.Core.Flux.Exceptions;

public class ViewModelQueryFluxException<TViewModel>(
	string message,
	int eventId,
	Exception innerException) : FluxException(message, eventId, FluxOperationKind.ViewModelQuery, innerException)
{
	public string ViewModelTypeName { get => typeof(TViewModel).GetDisplayName(); }

	public ViewModelQueryFluxException(
		string message,
		int eventId)
		: this(message, eventId, null)
	{
	}

	public override string ToString()
	{
		var sb = new StringBuilder(base.ToString());
		sb.AppendLine($"ViewModel Type Name: {ViewModelTypeName}");
		return sb.ToString();
	}
}