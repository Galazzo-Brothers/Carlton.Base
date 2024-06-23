using System.Text.Json;
using System.Web;
using Carlton.Core.Lab.Components.ParametersViewer;
namespace Carlton.Core.Lab.Test.Components.ParametersViewer;

public class ConnectedParametersViewerComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedParametersViewer_Markup_RendersCorrectly(
		TestParameters expectedParameters)
	{
		//Arrange
		var viewModel = new ParametersViewerViewModel { ComponentParameters = expectedParameters };
		var expectedMarkup = BuildExpectedMarkup(viewModel.ComponentParameters);

		//Act
		var cut = RenderComponent<ConnectedParametersViewer>(parameters => parameters
					.Add(p => p.ViewModel, viewModel));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}


	[Theory, AutoData]
	public void ConnectedParameterViewerComponent_OnParametersChanged_RaisesComponentEvent(
		TestParameters initialParameters,
		TestParameters expectedParameters)
	{
		//Arrange
		var eventFired = false;
		var evetArgs = (object)null;
		var viewModel = new ParametersViewerViewModel { ComponentParameters = initialParameters };
		var command = new UpdateParametersCommand { Parameters = expectedParameters };
		var cut = RenderComponent<ConnectedParametersViewer>(parameters => parameters
				.Add(p => p.ViewModel, viewModel)
				.Add(p => p.OnComponentEvent, args =>
				{
					eventFired = true;
					evetArgs = args;
				}));

		var newJson = JsonSerializer.Serialize(expectedParameters, new JsonSerializerOptions { WriteIndented = true });
		var txt = cut.Find("textarea");
		var submitBtn = cut.FindAll(".icon-btn")[0];

		//Act
		txt.Input(newJson);
		submitBtn.Click();

		//Assert
		eventFired.ShouldBeTrue();
		evetArgs.ShouldBeOfType<UpdateParametersCommand>()
			.Parameters.ShouldBe(expectedParameters);
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
	<div class=""parameters-viewer-actions"">
		<div class=""icon-btn disabled""  >
			<span class=""mdi mdi-24px mdi-check"" ></span>
		</div>
		<div class=""icon-btn disabled""  >
			<span class=""mdi mdi-24px mdi-undo"" ></span>
		</div>
	</div>
</div>";
	}
}

