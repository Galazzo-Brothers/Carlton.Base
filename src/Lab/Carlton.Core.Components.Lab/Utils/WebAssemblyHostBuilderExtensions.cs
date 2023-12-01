namespace Carlton.Core.Components.Lab;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestLab(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        IDictionary<string, TestResultsReport> testResults = null)
    {
        /*NavMenu Builder*/
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();

        /*Mapster Configuration*/
        var typeAdapterConfig = MapsterConfig.BuildMapsterConfig();

        /*Flux Registers*/
        var state = new LabState(options, testResults);
        builder.Services.AddCarltonFlux(state, builder.Configuration, typeAdapterConfig, true);
        builder.Services.AddCarltonFluxAdmin<LabState>(builder.Configuration);
    }
}

