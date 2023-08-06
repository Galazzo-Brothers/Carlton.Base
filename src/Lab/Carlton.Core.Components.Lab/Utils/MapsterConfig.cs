namespace Carlton.Core.Components.Lab;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<TestBedState, NavMenuViewModel>
            .NewConfig()
            .Map(dest => dest.SelectedItem, src => src.SelectedComponentState)
            .Map(dest => dest.MenuItems, src => src.ComponentStates);

        TypeAdapterConfig<TestBedState, ComponentViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentType, src => src.SelectedComponentType)
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<TestBedState, EventConsoleViewModel>
            .NewConfig()
            .Map(dest => dest.RecordedEvents, src => src.ComponentEvents);

        TypeAdapterConfig<TestBedState, ParametersViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<TestBedState, BreadCrumbsViewModel>
            .NewConfig()
            .Map(dest => dest.SelectedComponentState, src => src.SelectedComponentState);

        TypeAdapterConfig<TestBedState, TestResultsViewModel>
            .NewConfig()
            .ConstructUsing(_ => new TestResultsViewModel(_.SelectedComponentTestReport));

        TypeAdapterConfig<TestBedState, SourceViewerViewModel>
            .NewConfig()
            .TwoWays()
            .Map(dest => dest.ComponentSource, src => src.SelectedComponentMarkup);
    }

}

