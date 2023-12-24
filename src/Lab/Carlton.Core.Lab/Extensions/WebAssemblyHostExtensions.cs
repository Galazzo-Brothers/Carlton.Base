using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Carlton.Core.Flux.Extensions;
namespace Carlton.Core.Lab.Extensions;

public static class WebAssemblyHostExtensions
{
    public static void UseCarltonTestLab(this WebAssemblyHost host)
    {
        host.UseCarltonFlux();
       // host.UseCarltonFluxDebug();
    }
}
