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
     
        var state = new TestBedStateProcessor(options, testResults);
        builder.Services.AddSingleton<TestBedState>(state);
        builder.Services.AddSingleton<IStateStore<TestBedStateEvents>>(state);

        //var viewModelStateFacade = new TestBedViewModelStateFacade(state);

        builder.Services.AddTransient<IViewModelStateFacade, TestBedViewModelStateFacade>();

        builder.Services.AddSingleton<IStateProcessor>(state);

        //builder.Services.AddTransient<IStateProcessor>(_ => 
        //    new TransactionalStateProcessor<TestBedState>(
        //        state, _.GetService<ILogger<TransactionalStateProcessor<TestBedState>>>()));


        builder.Services.AddTransient<IExceptionDisplayService, ExceptionDisplayService>();
        builder.Services.AddCarltonState(assemblies);

        builder.Services.RegisterMapsterConfiguration();
    }
}
