using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Carlton.Core.Flux.Extensions;

public static class WebAssemblyHostExtensions
{
    public static void UseCarltonFlux(this WebAssemblyHost host)
    {
        SetupMappingConfigs(host);

    }

    private static void SetupMappingConfigs(WebAssemblyHost host)
    {
        var mapperConfig = host.Services.GetService<TypeAdapterConfig>();

        host.Services
            .GetServices<IRegister>()
            .ToList()
            .ForEach(registration => mapperConfig.Apply(registration));
    }
}
