using Carlton.Core.Components.JavaScriptComponents;
using Carlton.Core.Components.Lab.Components.SourceViewer;
namespace Carlton.Core.Lab.Test.SourceViewer;

public class ConnectedSourceViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedSourceViewer_Markup_RendersCorrectly(
		string source)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(JavaScriptMarkupViewer.ImportPath);
		moduleInterop.SetupVoid(JavaScriptMarkupViewer.SetCodeBlock, JavaScriptMarkupViewer.Selector, source);
		moduleInterop.SetupVoid(JavaScriptMarkupViewer.HighlightCodeBlock, JavaScriptMarkupViewer.Selector);
		var vm = new SourceViewerViewModel { ComponentSource = source };

		//Act
		var cut = RenderComponent<ConnectedSourceViewer>(parameters => parameters
					.Add(p => p.ViewModel, vm));

		//Assert
		cut.MarkupMatches(
@$"<div class=""javascript-markup-viewer"" >
            <pre><code class=""html""></code></pre>
        </div>");
	}

	[Theory, AutoData]
	public void ConnectedSourceViewer_Init_CallsJavaScript(
		string source)
	{
		//Arrange
		var moduleInterop = JSInterop.SetupModule(JavaScriptMarkupViewer.ImportPath);
		moduleInterop.SetupVoid(JavaScriptMarkupViewer.SetCodeBlock, JavaScriptMarkupViewer.Selector, source);
		moduleInterop.SetupVoid(JavaScriptMarkupViewer.HighlightCodeBlock, JavaScriptMarkupViewer.Selector);
		var vm = new SourceViewerViewModel { ComponentSource = source };

		//Act
		var cut = RenderComponent<ConnectedSourceViewer>(parameters => parameters
					.Add(p => p.ViewModel, vm));


		//Assert
		JSInterop.VerifyInvoke(JavaScriptMarkupViewer.SetCodeBlock)
			   .Arguments[0].ShouldBe(JavaScriptMarkupViewer.Selector);

		JSInterop.VerifyInvoke(JavaScriptMarkupViewer.SetCodeBlock)
				 .Arguments[1].ShouldBe(source);

		JSInterop.VerifyInvoke(JavaScriptMarkupViewer.HighlightCodeBlock)
			   .Arguments[0].ShouldBe(JavaScriptMarkupViewer.Selector);
	}
}

