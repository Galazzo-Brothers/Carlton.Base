namespace Carlton.Base.TestBedFramework;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestBed(this WebAssemblyHostBuilder builder,
        Action<NavMenuBuilder> navTreeAct,
        params Assembly[] assemblies)
    {
        var NavTreeBuilder = new NavMenuBuilder();
        navTreeAct(NavTreeBuilder);
        var options = NavTreeBuilder.Build();
        var state = new TestBedState(options);

        builder.Services.AddSingleton(state);
        builder.Services.AddSingleton<ICarltonStateStore>(state);
        builder.Services.AddMediatR(assemblies);

        builder.Services.AddCarltonState(builder =>
            builder.ForComponent<NavMenuViewModel>(_ =>
            {
                _.AddStateEvent(TestBedState.SELECTED_ITEM);
                _.AddStateEvent(TestBedState.VIEW_MODEL_CHANGED);
                _.AddStateEvent(TestBedState.STATUS_CHANGED);
            })
            .ForComponent<ComponentViewerViewModel>(_ =>
            {
                _.AddStateEvent(TestBedState.SELECTED_ITEM);
                _.AddStateEvent(TestBedState.VIEW_MODEL_CHANGED);
                _.AddStateEvent(TestBedState.STATUS_CHANGED);
            })
            .ForComponent<EventConsoleViewModel>(_ =>
            {
                _.AddStateEvent(TestBedState.COMPONENT_EVENT_ADDED);
            })
            .ForComponent<SourceViewerViewModel>(_ =>
            {
                _.AddStateEvent(TestBedState.SELECTED_ITEM);
            })
            .ForComponent<ModelViewerViewModel>(_ =>
            {
                _.AddStateEvent(TestBedState.SELECTED_ITEM);
            }),
            assemblies);
    }
}
