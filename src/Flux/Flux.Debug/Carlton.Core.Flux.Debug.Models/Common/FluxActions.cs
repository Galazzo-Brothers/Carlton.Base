namespace Carlton.Core.Flux.Debug.Models.Common;

/// <summary>
/// Represents the types of actions used in a Flux architecture.
/// </summary>
public enum FluxActions
{
	/// <summary>
	/// Indicates a query action for retrieving view model data.
	/// </summary>
	ViewModelQuery,

	/// <summary>
	/// Indicates a mutation command action for updating state.
	/// </summary>
	MutationCommand
}
