using Carlton.Core.Utilities.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Admin;

public static class ContainerExtensions
{
    public static void AddCarltonFluxAdmin<TState>(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var logger = new InMemoryLogger();
        services.AddSingleton(logger);
        services.AddLogging(builder =>
        {
            builder.AddConfiguration(configuration.GetSection("Logging"));
            builder.AddProvider(new InMemoryLoggerProvider(logger));
        });
    }
}


