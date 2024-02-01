using Carlton.Core.Flux.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Carlton.Core.BrowserStroage;
using Carlton.Core.Flux.Debug.Storage;
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

        services.AddSingleton<ILogsDataAccess, LogsDataAccess>();

        services.AddCarltonFlux(debugState);
        services.AddSingleton<IIndexDbService<IndexedLogEntry>>(_ => new IndexDbService<IndexedLogEntry>(_.GetService<IBlazorDbFactory>(), CarltonFlux, Logs));
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


