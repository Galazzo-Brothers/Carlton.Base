using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Test.Common;
using Carlton.Core.Components.Lab.Test.Mocks;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Web;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedParametersViewerTests : TestContext
{
    [Theory, AutoData]
    public void ConnectedParametersViewerComponentRendersCorrectly(MockObject obj)
    {
        //Arrange
        var expectedMarkup = BuildExpectedMarkup(obj);
        var vm = new ParametersViewerViewModel(new ComponentParameters(obj, ParameterObjectType.ParameterObject));

        //Act
        var cut = RenderComponent<ConnectedParametersViewer>(parameters => parameters
                    .Add(p => p.ViewModel, vm));


        //Assert
        cut.MarkupMatches(expectedMarkup);
    }


    [Theory, AutoData]
    public void ConnectedParameterViewerComponent_EventCallback(MockObject obj)
    {
        //Arrange
        var command = new MutationCommand();
        var onComponentEventCalled = false;
        var vm = new ParametersViewerViewModel(new ComponentParameters(obj, ParameterObjectType.ParameterObject));

        var cut = RenderComponent<ConnectedParametersViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    onComponentEventCalled = true;
                    command = cmd;
                }));

        var txt = cut.Find("textarea");
        var newJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

        //Act
        txt.Change(new ChangeEventArgs() { Value = newJson });

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<UpdateParametersCommand>(command);
        Assert.Equal(obj, command.Cast<UpdateParametersCommand>().Parameters);
    }

    private static string BuildExpectedMarkup(object obj)
    {
        var parameterString = HttpUtility.HtmlEncode(JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true }));

        return @$"
<div class=""parameters-viewer"">
    <div class=""json-viewer-console"">
        <div class=""console"">
            <textarea rows = ""15"" class="""" value=""{parameterString}""></textarea>
        </div>
    </div>
</div>";
    }
}

