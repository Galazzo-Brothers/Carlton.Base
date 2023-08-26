using Carlton.Core.Components.Flux.State;

namespace Carlton.Core.Components.Lab;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestLab(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        IDictionary<string, TestResultsReportModel> testResults = null)
    {
        /*NavMenu Builder*/
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();

        /*LabState*/
        var state = new LabState(options, testResults);
        builder.Services.AddSingleton(state);
        builder.Services.AddSingleton<IMutableFluxState<LabState>, FluxState<LabState>>();
        builder.Services.AddSingleton<IFluxState<LabState>>(_ => _.GetService<IMutableFluxState<LabState>>());
        builder.Services.AddSingleton<IFluxStateObserver<LabState>>(_ => _.GetService<IFluxState<LabState>>());
        builder.Services.AddSingleton<MutationResolver<LabState>>();

        /*Mapster Extensions*/
        MapsterConfig.RegisterMapsterConfiguration();

        /*Flux Registers*/
        builder.Services.AddCarltonFlux<LabState>();
    }
}

