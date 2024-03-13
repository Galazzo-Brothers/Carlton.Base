using Carlton.Core.Lab.State;
using Carlton.Core.Flux.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Carlton.Core.Flux.Debug.Extensions;
namespace Carlton.Core.Lab.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddCarltonTestLab(this IServiceCollection services,
		Action<NavMenuViewModelBuilder> navTreeAct)
	{
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
		services.AddCarltonFluxDebug(state);
	}
}

