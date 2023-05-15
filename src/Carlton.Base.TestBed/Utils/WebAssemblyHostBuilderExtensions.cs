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

        var viewModelStateFacade = new TestBedViewModelStateFacade(state);

        builder.Services.AddTransient(_ =>
        DispatchProxyLoggingDecorator<IViewModelStateFacade>.Decorate
            (
                viewModelStateFacade,
                _.GetService<ILogger<DispatchProxyLoggingDecorator<IViewModelStateFacade>>>()
            )
        );

        builder.Services.AddTransient(_ =>
        DispatchProxyLoggingDecorator<ICommandProcessor>.Decorate
            (
                state,
                _.GetService<ILogger<DispatchProxyLoggingDecorator<ICommandProcessor>>>()
             )
        );

        builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
                //cfg.AddOpenBehavior(typeof(TestBedHttpRequestPipelineBehavior<,>));
                // cfg.AddBehavior<IPipelineBehavior<ViewModelRequest<TestResultsViewModel>, TestResultsViewModel>, TestResultsViewModelHttpBehavior>();
            });

        builder.Services.AddTransient<IExceptionDisplayService, ExceptionDisplayService>();
        builder.Services.AddCarltonState(assemblies);
        builder.Services.RegisterMapsterConfiguration();
    }
}
