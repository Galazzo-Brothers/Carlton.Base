using Carlton.Core.Components.Consoles;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Text.Json;
using Console = Carlton.Core.Components.Consoles.Console;
namespace Carlton.Core.Components.Tests.Consoles;

[Trait("Component", nameof(JsonViewerConsole))]
public class JsonViewerConsoleComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(true)]
	[InlineAutoData(false)]
	public void JsonViewerConsole_Markup_RendersCorrectly(bool isReadOnly, object obj)
	{
		//Arrange
		ComponentFactories.AddStub<Console>();
		var expectedMarkup = isReadOnly ? @$"<div class=""json-viewer-console""></div>"
			: @"<div class=""json-viewer-console"">
					<div class=""json-viewer-actions"" >
						<div class=""icon-btn disabled""  >
							<span class=""mdi mdi-24px mdi-check"" ></span>
						</div>
						<div class=""icon-btn disabled""  >
							<span class=""mdi mdi-24px mdi-undo"" ></span>
						</div>
					</div>
				</div>";

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

		//Act
		var consoleElement = cut.Find("textarea");
		var updateObj = obj with { Value = expectedText };
		consoleElement.Input(new ChangeEventArgs { Value = JsonSerializer.Serialize(updateObj) });

		//Assert
		eventCalled.ShouldBeTrue();
		actualValue.ShouldBe(updateObj);
	}

	[Theory(DisplayName = "OnSubmit Parameter Test"), AutoData]
	public void JsonViewerConsole_OnSubmitParameter_FiresCallback(string expectedText)
	{
		//Arrange
		var obj = new { Value = "Some Initial Text" };
		var eventCalled = false;
		var actualValue = new object();

		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false)
			.Add(p => p.Object, obj)
			.Add(p => p.OnSubmit, (args) =>
			{
				eventCalled = true;
				actualValue = args.UpdatedJson;
			}));

		var submitBtn = cut.FindAll(".icon-btn")[0];

		//Act
		var consoleElement = cut.Find("textarea");
		var updateObj = obj with { Value = expectedText };
		consoleElement.Input(new ChangeEventArgs { Value = JsonSerializer.Serialize(updateObj) });
		submitBtn.Click();

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
			.Add(p => p.IsReadOnly, false)
			.Add(p => p.Object, obj));

		var consoleElement = cut.Find("textarea"); ;
		consoleElement.Input(new ChangeEventArgs { Value = newText });
		var btn = cut.FindAll(".icon-btn")[1];

		//Act
		btn.Click();
		consoleElement = cut.Find("textarea");

		//Assert
		consoleElement.GetAttribute("value").Replace("\n", "\r\n").ShouldBe(JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true }));
	}

	[Fact(DisplayName = "Dirty Disabled Submit Test")]
	public void JsonViewerConsole_CleanConsole_ShouldHaveDisabledSubmit()
	{
		//Act
		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false));

		var submitBtn = cut.FindAll(".icon-btn")[0];

		//Assert
		submitBtn.ClassList.ShouldContain("disabled");
	}

	[Theory(DisplayName = "Dirty Invalid Console Submit Test"), AutoData]
	public void JsonViewerConsole_InvalidConsole_ShouldHaveDisabledSubmit(string invalidText)
	{
		//Arrange
		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false));

		//Act
		var consoleElement = cut.Find("textarea"); ;
		consoleElement.Input(new ChangeEventArgs { Value = invalidText });
		var submitBtn = cut.FindAll(".icon-btn")[0];

		//Assert
		submitBtn.ClassList.ShouldContain("disabled");
	}

	[Theory(DisplayName = "Dirty Valid Submit Test"), AutoData]
	public void JsonViewerConsole_DirtyValidConsole_ShouldHaveEnabledSubmit(string validText)
	{
		//Arrange
		var validJson = JsonSerializer.Serialize(new { Value = validText });
		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false));

		//Act
		var consoleElement = cut.Find("textarea"); ;
		consoleElement.Input(new ChangeEventArgs { Value = validJson });
		var submitBtn = cut.FindAll(".icon-btn")[0];

		//Assert
		submitBtn.ClassList.ShouldNotContain("disabled");
	}

	[Fact(DisplayName = "Dirty Disabled Undo Test")]
	public void JsonViewerConsole_CleanConsole_ShouldHaveDisabledUndo()
	{
		//Act
		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false));
		var submitBtn = cut.FindAll(".icon-btn")[1];

		//Assert
		submitBtn.ClassList.ShouldContain("disabled");
	}

	[Theory(DisplayName = "Dirty Valid Undo Test"), AutoData]
	public void JsonViewerConsole_ValidConsole_ShouldHaveDisabledUndo(string validText)
	{
		//Arrange
		var validJson = JsonSerializer.Serialize(new { Value = validText });
		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false));

		//Act
		var consoleElement = cut.Find("textarea"); ;
		consoleElement.Input(new ChangeEventArgs { Value = validJson });
		var submitBtn = cut.FindAll(".icon-btn")[1];

		//Assert
		submitBtn.ClassList.ShouldContain("disabled");
	}

	[Theory(DisplayName = "Dirty Valid Undo Test"), AutoData]
	public void JsonViewerConsole_DirtyInvalidConsole_ShouldHaveEnabledUndo(string invalidText)
	{
		//Arrange
		var cut = RenderComponent<JsonViewerConsole>(parameters => parameters
			.Add(p => p.IsReadOnly, false));

		//Act
		var consoleElement = cut.Find("textarea"); ;
		consoleElement.Input(new ChangeEventArgs { Value = invalidText });
		var submitBtn = cut.FindAll(".icon-btn")[1];

		//Assert
		submitBtn.ClassList.ShouldNotContain("disabled");
	}
}
