using Microsoft.AspNetCore.Components;
namespace Carlton.Core.Flux.Contracts;

/// <summary>
/// Represents a connected component that is bound to a view model.
/// </summary>
/// <typeparam name="TViewModel">The type of the view model.</typeparam>
public interface IConnectedComponent<TViewModel> : IComponent
{
	/// <summary>
	/// Gets or sets the view model associated with the component.
	/// </summary>
	TViewModel ViewModel { get; set; }

	/// <summary>
	/// Gets or sets the event callback for component events.
	/// </summary>
	EventCallback OnComponentEvent { get; init; }
}
