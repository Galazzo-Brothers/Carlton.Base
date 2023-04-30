namespace Carlton.Base.TestBed;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<TestBedState, TestBedNavMenuViewModel>
            .NewConfig()
            .Map(dest => dest.SelectedItem, src => src.SelectedComponentState)
            .Map(dest => dest.MenuItems, src => src.ComponentStates);

        TypeAdapterConfig<TestBedState, TestBedComponentViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentType, src => src.SelectedComponentType)
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<TestBedState, TestBedEventConsoleViewModel>
            .NewConfig()
            .Map(dest => dest.RecordedEvents, src => src.ComponentEvents);

        TypeAdapterConfig<TestBedState, TestBedParameterViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<TestBedState, TestBedBreadCrumbsViewModel>
            .NewConfig()
            .Map(dest => dest.SelectedComponentState, src => src.SelectedComponentState);
    }

}

