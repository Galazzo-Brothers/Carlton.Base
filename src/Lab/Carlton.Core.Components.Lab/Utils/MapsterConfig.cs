using System.Collections.Immutable;

namespace Carlton.Core.Components.Lab;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration()
    {
        TypeAdapterConfig.GlobalSettings.RequireExplicitMapping = true;
        TypeAdapterConfig.GlobalSettings.RequireDestinationMemberSource = true;

        TypeAdapterConfig<LabState, NavMenuViewModel>
            .NewConfig()
            .Map(dest => dest.SelectedItem, src => src.SelectedComponentState)
            .Map(dest => dest.MenuItems, src => src.ComponentStates);

        TypeAdapterConfig<LabState, ComponentViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentType, src => src.SelectedComponentType)
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<LabState, EventConsoleViewModel>
            .NewConfig()
            .Map(dest => dest.RecordedEvents, src => src.ComponentEvents);

        TypeAdapterConfig<LabState, ParametersViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentParameters, src => src.SelectedComponentParameters);

        TypeAdapterConfig<LabState, BreadCrumbsViewModel>
            .NewConfig()
            .Map(dest => dest.SelectedComponentState, src => src.SelectedComponentState);

        TypeAdapterConfig<LabState, TestResultsViewModel>
            .NewConfig()
            .ConstructUsing(_ => new TestResultsViewModel(_.SelectedComponentTestReport));

        TypeAdapterConfig<LabState, SourceViewerViewModel>
            .NewConfig()
            .Map(dest => dest.ComponentSource, src => src.SelectedComponentMarkup);

        TypeAdapterConfig<LabState, LabState>
          .NewConfig()
          .Ignore(_ => _.ComponentTestResults)
          .ConstructUsing(_ => new LabState(_.ComponentStates, _.ComponentTestResults));

        TypeAdapterConfig<ComponentRecordedEvent, ComponentRecordedEvent>
           .NewConfig();

        TypeAdapterConfig<ComponentParameters, ComponentParameters>
            .NewConfig();

        TypeAdapterConfig<ComponentState, ComponentState>
            .NewConfig();

        TypeAdapterConfig<TestResultsSummaryModel, TestResultsSummaryModel>
            .NewConfig()
            .ConstructUsing(_ => new TestResultsSummaryModel(_.Total, _.Passed, _.Failed, _.Duration));

        TypeAdapterConfig<TestResultModel, TestResultModel>
            .NewConfig();

        TypeAdapterConfig<TestResultsReportModel, TestResultsReportModel>
            .NewConfig()
            .ConstructUsing(_ => new TestResultsReportModel(_.TestResults));

        TypeAdapterConfig.GlobalSettings.Compile();
    }

}

