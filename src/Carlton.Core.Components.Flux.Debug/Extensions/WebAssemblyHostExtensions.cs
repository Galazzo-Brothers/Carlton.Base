using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Carlton.Core.Flux.Extensions;
namespace Carlton.Core.Flux.Debug.Extensions;

public static class WebAssemblyHostExtensions
{
    public static void UseCarltonFluxDebug(this WebAssemblyHost host)
    {
        host.UseCarltonFlux();
    }
}
