using Carlton.Core.Lab.State;
using Carlton.Core.Flux.Debug.Extensions;
using Carlton.Core.Flux.Extensions;
using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Lab.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCarltonTestLab(this IServiceCollection services,
        Action<NavMenuViewModelBuilder> navTreeAct)
    {
        /*NavMenu Builder*/
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();


        /*Flux Registers*/
        var state = new LabState(options);
        services.AddCarltonFlux(state);
        services.AddCarltonFluxDebug(state);
    }
}

