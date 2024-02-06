using Carlton.Core.Components.Panels;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(ResizablePanel))]
public class ResizeablePanelComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ResizeablePanel_Markup_RendersCorrectly(
        RenderFragment topContent,
        RenderFragment bottomContent)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(ResizablePanel.ImportPath);
        moduleInterop.SetupVoid(ResizablePanel.InitResizablePanel);
        var expectedMarkup = @$"
        <div class=""resizable-panel"">
            <div class=""panel-top""></div>
            <div class=""splitter-horizontal""></div>
            <div class=""panel-bottom""></div>
        </div>";


        //Act
        var cut = RenderComponent<ResizablePanel>(parameters => parameters
                .Add(p => p.TopContent, topContent)
                .Add(p => p.BottomContent, bottomContent));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "JSModule Init Method Test"), AutoData]
    public void ResizeablePanel_JSModule_Init_RendersCorrectly(
    RenderFragment topContent,
    RenderFragment bottomContent)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(ResizablePanel.ImportPath);
        moduleInterop.SetupVoid(ResizablePanel.InitResizablePanel);
        var expectedMarkup = @$"
        <div class=""resizable-panel"">
            <div class=""panel-top""></div>
            <div class=""splitter-horizontal""></div>
            <div class=""panel-bottom""></div>
        </div>";


        //Act
        var cut = RenderComponent<ResizablePanel>(parameters => parameters
                .Add(p => p.TopContent, topContent)
                .Add(p => p.BottomContent, bottomContent));

        //Assert
        JSInterop.VerifyInvoke(ResizablePanel.InitResizablePanel);
    }
}
