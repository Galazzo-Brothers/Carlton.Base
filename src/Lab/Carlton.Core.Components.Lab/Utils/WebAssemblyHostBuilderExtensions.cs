using Carlton.Core.Components.Flux.Mutations;

namespace Carlton.Core.Components.Lab;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestBed(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        IDictionary<string, TestResultsReportModel> testResults = null,
        params Assembly[] assemblies)
    {
        //NavMenu Initialization
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();
     
        var state = new LabState(options, testResults);
        builder.Services.AddSingleton<LabState>(state);
        builder.Services.AddSingleton<IStateStore<LabStateEvents>>(state);

        builder.Services.AddTransient<IStateMutator, StateMutator<LabState, LabStateEvents>>();
     //   builder.Services.AddSingleton<IStateProcessor>(stateProcessor);
      //  builder.Services.AddSingleton<IStateProcessor<LabState>>(stateProcessor);


        builder.Services.AddTransient<IViewModelStateFacade, TestBedViewModelStateFacade>();
        builder.Services.RegisterMapsterConfiguration();
        builder.Services.AddTransient<IExceptionDisplayService, ExceptionDisplayService>();
        builder.Services.AddCarltonState<LabState>(assemblies);

  
    }
}
