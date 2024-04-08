using Carlton.Core.Flux.Debug.Components.Logging.EventLogging.LogTable;
using Carlton.Core.Flux.Debug.Components.Logging.TraceLogging.LogTable;
using Carlton.Core.Flux.Extensions;
using Carlton.Core.Foundation.State;
using Carlton.Core.Foundation.Web.ViewState;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Debug.Extensions;

/// <summary>
/// Provides extension methods for configuring Carlton Flux with debug options and logging services.
/// </summary>
public static class ContainerExtensions
{
	/// <summary>
	/// Adds Carlton Flux with debug options to the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <typeparam name="TState">The type of the state used by Carlton Flux.</typeparam>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="state">The initial state for Carlton Flux.</param>
	public static void AddCarltonFluxDebug<TState>(this IServiceCollection services, TState state)
	{
		services.AddViewStateService<TableInteractionState>(nameof(EventLogTable));
		services.AddViewStateService<TableInteractionState>(nameof(TraceLogTable));
		services.AddViewStateService<EventLogViewerFilterState>();
		services.AddViewStateService<TraceLogTableExpandedRowsState>();

		var debugState = new FluxDebugState();

		RegisterLogging(services);

		services.AddCarltonFlux(debugState, opts =>
		{
			opts.AddHttpInterception = false;
			opts.AddLocalStorage = false;
		});
	}

	private static void RegisterLogging(IServiceCollection services)
	{
		var logger = new MemoryLogger();
		services.AddSingleton(logger);
		services.AddLogging(b =>
		{
			b.AddProvider(new MemoryLoggerProvider(logger));
			b.AddFilter("Carlton.Core.Flux.Debug", LogLevel.None); // Apply the filter
		});

		services.AddSingleton<ILogger, MemoryLogger>();
	}
}


