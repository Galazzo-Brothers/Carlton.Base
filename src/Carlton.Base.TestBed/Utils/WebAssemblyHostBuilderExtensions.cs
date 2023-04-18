namespace Carlton.Base.TestBedFramework;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestBed(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        params Assembly[] assemblies)
    {
        var NavTreeBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavTreeBuilder);
        var options = NavTreeBuilder.Build();
        var state = new TestBedState(options);

        builder.Services.AddSingleton(state);
        builder.Services.AddSingleton<ICarltonStateStore<TestBedStateEvents>>(state);
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        builder.Services.AddCarltonState(assemblies);
    }
}
