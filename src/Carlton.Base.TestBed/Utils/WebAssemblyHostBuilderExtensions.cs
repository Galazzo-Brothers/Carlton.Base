using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Base.TestBed;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestBed(this WebAssemblyHostBuilder builder,
        Action<TestBedNavMenuViewModelBuilder> navTreeAct,
        params Assembly[] assemblies)
    {
        //NavMenu Initialization
        var NavMenuBuilder = new TestBedNavMenuViewModelBuilder();
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
                
                //cfg.AddBehavior<IPipelineBehavior<ViewModelRequest<TestResultsReportViewerViewModel>, TestResultsReportViewerViewModel>, TestResultsReportViewerViewModelHttpBehavior>();
            });

        builder.Services.AddCarltonState(assemblies);
        builder.Services.RegisterMapsterConfiguration();
    }
}
