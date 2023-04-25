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

        var state = new TestBedState(options);
        builder.Services.AddSingleton(state);
        builder.Services.AddSingleton<ICarltonStateStore<TestBedStateEvents>>(state);
        builder.Services.AddSingleton<ITrxParser, TrxParser>();
        builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);
            });

        builder.Services.AddCarltonState(assemblies);
    }
}
