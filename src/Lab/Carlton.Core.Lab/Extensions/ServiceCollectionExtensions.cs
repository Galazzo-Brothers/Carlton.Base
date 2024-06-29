using Microsoft.Extensions.DependencyInjection;
using Carlton.Core.Flux.Extensions;
using Carlton.Core.Lab.State;
using Carlton.Core.LayoutServices.Extensions;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Lab.Components.ComponentViewer;
namespace Carlton.Core.Lab.Extensions;

/// <summary>
/// Extension methods for configuring Carlton Test Lab services.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// Adds Carlton Test Lab services to the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="navTreeAct">An action to register the components for the test lab and their desired state which in turn configure the navigation menu.</param>
	/// <remarks>
	/// This method allows configuring the navigation menu using the provided <paramref name="navTreeAct"/> action.
	/// </remarks>
	public static void AddCarltonTestLab(this IServiceCollection services,
		Action<NavMenuViewModelBuilder> navTreeAct)
	{
		/*Layout Services*/
		services.AddCarltonLayout(opt =>
		{
			opt.ShowPanel = true;
		});

		/*NavMenu Builder*/
		var NavMenuBuilder = new NavMenuViewModelBuilder();
		navTreeAct(NavMenuBuilder);
		var navMenuOptions = NavMenuBuilder.Build();

		/*Flux Registers*/
		var state = new LabState(navMenuOptions);
		services.AddCarltonFlux(state, opts =>
		{
			opts.AddLocalStorage = false;
			opts.AddHttpInterception = false;
		});

		/*Flux Debug Registrations*/
		services.AddCarltonFluxDebug<LabState>(typeof(SourceViewerViewModel), typeof(InitSelectionCommand));
	}
}

