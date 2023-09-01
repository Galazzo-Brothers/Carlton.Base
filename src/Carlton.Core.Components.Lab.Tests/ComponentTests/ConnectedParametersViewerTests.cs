using AutoFixture.Xunit2;
using Bunit;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Lab.Test.Common;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedParametersViewerTests : TestContext
{
    [Fact]
    public void ConnectedParametersViewerComponentRendersCorrectly()
    {
        //Arrange
        var vm = new ParametersViewerViewModel(new ComponentParameters(
            new
            {
                Param1 = "Testing",
                Param2 = 17,
                Param3 = false
            }, ParameterObjectType.ParameterObject));

        //Act
        var cut = RenderComponent<ConnectedParametersViewer>(parameters => parameters
                    .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@"
<div class=""parameters-viewer"">
    <div class=""json-viewer-console"">
        <div class=""console"">
            <textarea rows = ""15"" class="""" value=""{
  &quot;Param1&quot;: &quot;Testing&quot;,
  &quot;Param2&quot;: 17,
  &quot;Param3&quot;: false
}"">
            </textarea>
        </div>
    </div>
</div>");
    }

    [Theory, AutoData]
    public void ConnectedParameterViewerComponent_EventCallback(string param1, int param2, bool param3)
    {
        //Arrange
        var newValue = new
        {
            Param1 = param1,
            Param2 = param2,
            Param3 = param3
        };
        var command = new MutationCommand();
        var onComponentEventCalled = false;
        var vm = new ParametersViewerViewModel(new ComponentParameters(newValue, ParameterObjectType.ParameterObject));

        var cut = RenderComponent<ConnectedParametersViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm)
                .Add(p => p.OnComponentEvent, cmd =>
                {
                    onComponentEventCalled = true;
                    command = cmd;
                }));

        var txt = cut.Find("textarea");
        var newJson = JsonSerializer.Serialize(newValue);

        //Act
        txt.Change(new ChangeEventArgs() { Value = newJson });

        //Assert
        Assert.True(onComponentEventCalled);
        Assert.IsType<UpdateParametersCommand>(command);
        Assert.Equal(newValue, command.Cast<UpdateParametersCommand>().Parameters);
    }
}

