using Bunit;
using Carlton.Core.Components.Lab.Models;
using Carlton.Core.Components.Library;

namespace Carlton.Core.Components.Lab.Test;

public class ConnectedSourceViewerTests : TestContext
{
    [Fact]
    public void ConnectedSourceViewerComponentRendersCorrectly()
    {
        //Arrange
        var source = "<div>Hello World!</div>";
        var moduleInterop = JSInterop.SetupModule(JavaScriptHelper.GetImportPath(typeof(SourceViewer)));
        moduleInterop.Setup<Task>(SourceViewer.SetCodeBlock, SourceViewer.Selector, source);
        moduleInterop.Setup<Task>(SourceViewer.HighlightCodeBlock, SourceViewer.Selector);
        var vm = new SourceViewerViewModel(source);

        //Act
        var cut = RenderComponent<ConnectedSourceViewer>(parameters => parameters
                    .Add(p => p.ViewModel, vm));

        cut.Render();

        //Assert
        cut.MarkupMatches(
@"<div class=""test-component-output-source""><pre><code class=""html""></code></pre></div>");
    }
}


