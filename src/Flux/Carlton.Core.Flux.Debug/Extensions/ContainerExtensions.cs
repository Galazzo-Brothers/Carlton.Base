using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Carlton.Core.Flux.Extensions;
namespace Carlton.Core.Flux.Debug.Extensions;

public static class ContainerExtensions
{
    public static void AddCarltonFluxDebug<TState>(this WebAssemblyHostBuilder builder, TState state)
    {
        var adminState = new FluxDebugState(state);
        builder.AddCarltonFlux(adminState, false);
    }
}


