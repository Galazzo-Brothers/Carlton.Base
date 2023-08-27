namespace Carlton.Core.Components.Lab;

public static class MapsterConfig
{
    public static TypeAdapterConfig BuildMapsterConfig()
    {
        var config = new TypeAdapterConfig
        {
            RequireExplicitMapping = true,
            RequireDestinationMemberSource = true
        };

        config.NewConfig<LabState, LabState>()
            .Ignore(_ => _.ComponentTestResults)
            .ConstructUsing(_ => new LabState(_.ComponentStates, _.ComponentTestResults));

        config.NewConfig<LabState, NavMenuViewModel>()
            .Map(dest => dest.SelectedItem, src => src.SelectedComponentState)
            .Map(dest => dest.MenuItems, src => src.ComponentStates);

        config.NewConfig<LabState, ComponentViewerViewModel>()
            .Map(dest => dest.ComponentType, src => src.SelectedComponentType)
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        config.NewConfig<LabState, EventConsoleViewModel>()
            .Map(dest => dest.RecordedEvents, src => src.ComponentEvents);

        config.NewConfig<LabState, ParametersViewerViewModel>()
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        config.NewConfig<LabState, BreadCrumbsViewModel>()
            .Map(dest => dest.SelectedComponentState, src => src.SelectedComponentState);

        config.NewConfig<LabState, TestResultsViewModel>()
            .ConstructUsing(_ => new TestResultsViewModel(_.SelectedComponentTestReport));

        config.NewConfig<LabState, SourceViewerViewModel>()
            .Map(dest => dest.ComponentSource, src => src.SelectedComponentMarkup);

        config.NewConfig<ComponentRecordedEvent, ComponentRecordedEvent>();

        config.NewConfig<ComponentParameters, ComponentParameters>();

        config.NewConfig<ComponentState, ComponentState>();

        config.NewConfig<TestResultModel, TestResultModel>();

        config.Compile();

        return config;
    }

}

