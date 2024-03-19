namespace Carlton.Core.Lab.Models.Common;
internal sealed record NavMenuBuilderItemState(string DisplayName, Type ComponentType, object ComponentParameters);

/// <summary>
/// Builder class for constructing navigation menu view models.
/// </summary>
public sealed class NavMenuViewModelBuilder
{
	private readonly List<NavMenuBuilderItemState> _internalState;

	/// <summary>
	/// Initializes a new instance of the <see cref="NavMenuViewModelBuilder"/> class.
	/// </summary>
	public NavMenuViewModelBuilder()
	{
		_internalState = [];
	}

	/// <summary>
	/// Adds a component to the navigation menu.
	/// </summary>
	/// <typeparam name="T">The type of the component to add.</typeparam>
	/// <returns>The current instance of <see cref="NavMenuViewModelBuilder"/>.</returns>
	public NavMenuViewModelBuilder AddComponent<T>()
	{
		return AddComponentState<T>("Default", new object());
	}

	/// <summary>
	/// Adds a component state to the navigation menu.
	/// </summary>
	/// <typeparam name="T">The type of the component to add.</typeparam>
	/// <param name="componentParameters">The parameters of the component state.</param>
	/// <returns>The current instance of <see cref="NavMenuViewModelBuilder"/>.</returns>
	public NavMenuViewModelBuilder AddComponentState<T>(object componentParameters)
	{
		return AddComponentState<T>("Default", componentParameters);
	}

	/// <summary>
	/// Adds a component state with a custom display name to the navigation menu.
	/// </summary>
	/// <typeparam name="T">The type of the component to add.</typeparam>
	/// <param name="displayName">The display name of the component state.</param>
	/// <param name="componentParameters">The parameters of the component state.</param>
	/// <returns>The current instance of <see cref="NavMenuViewModelBuilder"/>.</returns>
	public NavMenuViewModelBuilder AddComponentState<T>(string displayName, object componentParameters)
	{
		var testComp = new NavMenuBuilderItemState(displayName, typeof(T), componentParameters);
		_internalState.Add(testComp);

		return this;
	}

	/// <summary>
	/// Builds the navigation menu view model based on the added components and states.
	/// </summary>
	/// <returns>The collection of component configurations for the navigation menu.</returns>
	public IEnumerable<ComponentConfigurations> Build()
	{
		return _internalState.GroupBy(_ => _.ComponentType)
					  .Select(group => new ComponentConfigurations
					  {
						  ComponentType = group.Key,
						  ComponentStates = group.Select(BuildComponentState),
						  IsExpanded = IsExpanded(group.Key)
					  });


		bool IsExpanded(Type type)
			=> type == _internalState[0].ComponentType;

		static ComponentState BuildComponentState(NavMenuBuilderItemState state)
		{
			return new()
			{
				DisplayName = state.DisplayName,
				ComponentParameters = state.ComponentParameters
			};
		}
	}
}
