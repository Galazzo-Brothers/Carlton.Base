using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Utilities.Extensions;
using MapsterMapper;

namespace Carlton.Core.Components.Lab.Test.MappingTests;

public class LabStateMappingTests
{
    [Fact]
    public void ValidateNavMenuViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();
        
        //Act
        var result = mapper.Map<NavMenuViewModel>(labState);


        //Assert
        Assert.Equal(labState.SelectedComponentIndex, result.SelectedIndex);
        Assert.Equal(labState.ComponentStates, result.MenuItems);
    }

    [Fact]
    public void ValidateComponentViewerViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<ComponentViewerViewModel>(labState);


        //Assert
        Assert.Equal(labState.SelectedComponentType, result.ComponentType);
        Assert.Equal(labState.SelectedComponentParameters, result.ComponentParameters);
    }

    [Fact]
    public void ValidateEventConsoleViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<EventConsoleViewModel>(labState);


        //Assert
        Assert.Equal(labState.ComponentEvents, result.RecordedEvents);
    }

    [Fact]
    public void ValidateParametersViewerViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<ParametersViewerViewModel>(labState);


        //Assert
        Assert.Equal(labState.SelectedComponentParameters, result.ComponentParameters);
    }

    [Fact]
    public void ValidateBreadCrumbsViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<BreadCrumbsViewModel>(labState);


        //Assert
        Assert.Equal(labState.SelectedComponentType.GetDisplayName(), result.SelectedComponent);
        Assert.Equal(labState.SelectedComponentState.DisplayName, result.SelectedComponentState);
    }

    [Fact]
    public void ValidateSourceViewerViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<SourceViewerViewModel>(labState);


        //Assert
        Assert.Equal(labState.SelectedComponentMarkup, result.ComponentSource);
    }

    [Fact]
    public void ValidateTestResultsViewModelMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<TestResultsViewModel>(labState);


        //Assert
        Assert.Equal(labState.SelectedComponentTestReport.TestResults, result.TestResults);
    }

    [Fact]
    public void ValidateLabStateSelfMapping()
    {
        //Arrange
        var mapper = new Mapper(MapsterConfig.BuildMapsterConfig());
        var labState = LabStateFactory.BuildLabState();

        //Act
        var result = mapper.Map<LabState>(labState);


        //Assert
        Assert.Equal(labState.ComponentStates, result.ComponentStates);
        Assert.Equal(labState.ComponentTestResults, result.ComponentTestResults);
        Assert.Equal(labState.ComponentEvents, result.ComponentEvents);
        Assert.Equal(labState.SelectedComponentState, result.SelectedComponentState);
        Assert.Equal(labState.SelectedComponentIndex, result.SelectedComponentIndex);
        Assert.Equal(labState.SelectedComponentMarkup, result.SelectedComponentMarkup);
        Assert.Equal(labState.SelectedComponentTestReport, result.SelectedComponentTestReport);
        Assert.Equal(labState.SelectedComponentType, result.SelectedComponentType);
        Assert.Equal(labState.SelectedComponentParameters, result.SelectedComponentParameters);
    }
}
