using Carlton.Core.Components.Flux.Extensions;

namespace Carlton.Core.Components.Lab.Extensions;

public static class WebAssemblyHostExtensions
{
    public static void UseCarltonTestLab(this WebAssemblyHost host)
    {
        host.UseCarltonFlux();
    }
}
