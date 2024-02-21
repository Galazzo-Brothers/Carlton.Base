namespace Carlton.Core.Components.Tests.JavaScriptComponents;

[Trait("Component", nameof(JavaScriptMarkupViewer))]
public class JavaScriptMarkupViewerComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void JavaScriptMarkupViewer_Markup_RendersCorrectly(
        string expectedSource,
        string expectedLanguage)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(JavaScriptMarkupViewer.ImportPath);
        moduleInterop.SetupVoid(JavaScriptMarkupViewer.SetCodeBlock, JavaScriptMarkupViewer.Selector, expectedSource);
        moduleInterop.SetupVoid(JavaScriptMarkupViewer.HighlightCodeBlock, JavaScriptMarkupViewer.Selector);
        var expectedMarkup = @$"
        <div class=""javascript-markup-viewer"" >
            <pre><code class=""{expectedLanguage}""></code></pre>
        </div>";

        //Act
        var cut = RenderComponent<JavaScriptMarkupViewer>(parameters => parameters
            .Add(p => p.Source, expectedSource)
            .Add(p => p.Language, expectedLanguage));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "JSModule Init Method Test"), AutoData]
    public void JavaScriptMarkupViewer_JSModule_Init_RendersCorrectly(
        string expectedSource,
        string expectedLanguage)
    {
        //Arrange
        var moduleInterop = JSInterop.SetupModule(JavaScriptMarkupViewer.ImportPath);
        moduleInterop.SetupVoid(JavaScriptMarkupViewer.SetCodeBlock, JavaScriptMarkupViewer.Selector, expectedSource);
        moduleInterop.SetupVoid(JavaScriptMarkupViewer.HighlightCodeBlock, JavaScriptMarkupViewer.Selector);

        //Act
        var cut = RenderComponent<JavaScriptMarkupViewer>(parameters => parameters
            .Add(p => p.Source, expectedSource)
            .Add(p => p.Language, expectedLanguage));

        //Assert
        JSInterop.VerifyInvoke(JavaScriptMarkupViewer.SetCodeBlock)
                 .Arguments[0].ShouldBe(JavaScriptMarkupViewer.Selector);
        
        JSInterop.VerifyInvoke(JavaScriptMarkupViewer.SetCodeBlock)
                 .Arguments[1].ShouldBe(expectedSource);

        JSInterop.VerifyInvoke(JavaScriptMarkupViewer.HighlightCodeBlock)
               .Arguments[0].ShouldBe(JavaScriptMarkupViewer.Selector);
    }
}
