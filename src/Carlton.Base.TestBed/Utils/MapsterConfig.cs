namespace Carlton.Base.TestBed;

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

        TypeAdapterConfig<TestBedState, ModelViewerViewModel>
           .NewConfig()
           .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<TestBedState, SelectedComponentBreadCrumbsViewModel>
         .NewConfig()
         .Map(dest => dest.SelectedComponent, src => src.SelectedComponentState.Type.GetDisplayName())
         .Map(dest => dest.SelectedState, src => src.SelectedComponentState.DisplayName);
    }

}

