namespace Carlton.Base.TestBed;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestBed(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        IDictionary<string, TestResultsReport> testResults = null,
        params Assembly[] assemblies)
    {
        //NavMenu Initialization
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();
     
        var state = new TestBedStateMutationCommands(options, testResults);
        builder.Services.AddSingleton<TestBedState>(state);
        builder.Services.AddSingleton<IStateStore<TestBedStateEvents>>(state);

        var stateProcessor = new TestBedStateProcessor(state);
        builder.Services.AddSingleton<IStateProcessor>(stateProcessor);
        builder.Services.AddSingleton<IStateProcessor<TestBedStateMutationCommands>>(stateProcessor);


        builder.Services.AddTransient<IViewModelStateFacade, TestBedViewModelStateFacade>();

        builder.Services.AddTransient<IExceptionDisplayService, ExceptionDisplayService>();
        builder.Services.AddCarltonState(assemblies);

        builder.Services.RegisterMapsterConfiguration();
    }
}
