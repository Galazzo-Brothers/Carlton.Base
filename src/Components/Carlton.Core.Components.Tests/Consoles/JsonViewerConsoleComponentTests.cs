using Carlton.Core.Components.Consoles;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Text.Json;
using Console = Carlton.Core.Components.Consoles.Console;
namespace Carlton.Core.Components.Tests.Consoles;

[Trait("Component", nameof(JsonViewerConsole))]
public class JsonViewerConsoleComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void JsonViewerConsole_Markup_RendersCorrectly(object obj, bool isReadOnly)
    {
        //Arrange
        ComponentFactories.AddStub<Console>();
        var expectedMarkup = @$"<div class=""json-viewer-console""></div>";

        //Act
        var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
                .Add(p => p.Object, obj)
                .Add(p => p.IsReadOnly, isReadOnly));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Parameter Test"), AutoData]
    public void JsonViewerConsole_ObjectParameter_RendersCorrectly(object obj, bool isReadOnly)
    {
        //Arrange
        var expectedContent = JsonSerializer.Serialize(obj);
        ComponentFactories.AddStub<Console>(parameters => $"<div class='console'>{parameters.Get(x => x.Text)}</div>");

        //Act
        var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
                .Add(p => p.Object, obj)
                .Add(p => p.IsReadOnly, isReadOnly));
        var actualContent = cut.Find(".console").TextContent;

        //Assert
        actualContent.ShouldBe(expectedContent);
    }

    [Theory(DisplayName = "Parameter Test"), AutoData]
    public void JsonViewerConsole_IsReadOnlyParameter_RendersCorrectly(object obj, bool isReadOnly)
    {
        //Arrange
        ComponentFactories.AddStub<Console>(parameters => $"<div class='console'>{parameters.Get(x => x.IsReadOnly)}</div>");

        //Act
        var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
                .Add(p => p.Object, obj)
                .Add(p => p.IsReadOnly, isReadOnly));
        var actualContent = bool.Parse(cut.Find(".console").TextContent);

        //Assert
        actualContent.ShouldBe(isReadOnly);
    }

    [Theory(DisplayName = "OnInputCallback Parameter Test"), AutoData]
    public void JsonViewerConsole_OnInputCallbackParameter_FiresCallback(string expectedText)
    {
        //Arrange
        var obj = new { Value = "Some Initial Text" };
        var eventCalled = false;
        var actualValue = new object();

        var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Object, obj)
            .Add(p => p.OnValueChange, (args) =>
            {
                eventCalled = true;
                actualValue = args.Value;
            }));

        var consoleElement = cut.Find("textarea");

        //Act
        var updateObj = obj with { Value = expectedText };
        consoleElement.Input(new ChangeEventArgs { Value = JsonSerializer.Serialize(updateObj) });

        //Assert
        eventCalled.ShouldBeTrue();
        actualValue.ShouldBe(updateObj);
    }


    [Theory(DisplayName = "Reset Function Test"), AutoData]
    public void JsonViewerConsole_ResetCall_ShouldResetConsoleText(string newText)
    {
        //Arrange
        var obj = new { Value = "Some Initial Text" };

        var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Object, obj));

        var console = cut.FindComponent<Console>();
        console.SetParametersAndRender(ComponentParameter.CreateParameter("Text", newText));

        //Act
        cut.Instance.Reset();
        cut.Render();

        //Assert
        console.Instance.Text.ShouldBe(JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true }));
    }
}
