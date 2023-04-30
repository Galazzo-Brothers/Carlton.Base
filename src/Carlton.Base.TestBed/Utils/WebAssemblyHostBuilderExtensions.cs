using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Base.TestBed;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestBed(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        params Assembly[] assemblies)
    {
        //NavMenu Initialization
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();

        var state = new TestBedStateProcessor(options);
        builder.Services.AddSingleton<TestBedState>(state);
        builder.Services.AddSingleton<IStateStore<TestBedStateEvents>>(state);
        builder.Services.AddSingleton<ICommandProcessor>(state);
        builder.Services.AddTransient<IViewModelStateFacade, TestBedViewModelStateFacade>();
        builder.Services.AddSingleton<ITrxParser, TrxParser>();
        builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
                cfg.AddOpenBehavior(typeof(TestBedHttpRequestPipelineBehavior<,>));
                // cfg.AddBehavior<IPipelineBehavior<ViewModelRequest<TestResultsViewModel>, TestResultsViewModel>, TestResultsViewModelHttpBehavior>();
            });

        builder.Services.AddCarltonState(assemblies);
        builder.Services.RegisterMapsterConfiguration();
    }
}
