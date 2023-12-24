using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Carlton.Core.Flux.Extensions;
using Carlton.Core.Lab.State;
namespace Carlton.Core.Lab.Extensions;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestLab(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct)
    {
        /*NavMenu Builder*/
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();


        /*Flux Registers*/
        var state = new LabState(options);
        builder.AddCarltonFlux(state, true);
      //  builder.AddCarltonFluxDebug(state);
    }
}

