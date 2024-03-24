using Carlton.Core.Flux.Extensions;
using Microsoft.Extensions.DependencyInjection;
using BlazorDB;
namespace Carlton.Core.Flux.Debug.Extensions;

public static class ContainerExtensions
{
	private const string CarltonFlux = "CarltonFlux";
	private const string Logs = "Logs";

	public static void AddCarltonFluxDebug<TState>(this IServiceCollection services, TState state)
	{
		var debugState = new FluxDebugState();
		RegisterIndexDbStorage(services);

		services.AddCarltonFlux(debugState, opts =>
		{
			opts.AddHttpInterception = false;
			opts.AddLocalStorage = false;
		});
	}

	private static void RegisterIndexDbStorage(IServiceCollection services)
	{
		services.AddBlazorDB(options =>
		{
			options.Name = "CarltonFlux";
			options.Version = 1;
			options.StoreSchemas =
			[
				new()
				{
					Name = "Logs",
					PrimaryKey = "key",
					Indexes = ["indexDate"]
				},
				new()
				{
					Name = "AppState",
					PrimaryKey = "id",
					PrimaryKeyAuto = true
				}
			];
		});
	}
}


