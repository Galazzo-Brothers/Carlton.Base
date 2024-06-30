using Carlton.Core.Components.Toggles;
namespace Carlton.Core.Components.Tests.Toggles;

[Trait("Component", nameof(ToggleSelect<int>))]
public class ToggleComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test")]
	[InlineAutoData(ToggleSelectOption.FirstOption)]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void ToggleSelect_Markup_RendersCorrectly(
		ToggleSelectOption selectedOption,
		KeyValuePair<string, int> firstOption,
		KeyValuePair<string, int> secondOption)
	{
		//Arrange
		var expectedMarkup =
@$"
<div class=""toggle-select"" >
    <input type=""radio"" class=""left radio-input {(selectedOption == ToggleSelectOption.FirstOption ? "checked" : "")}"" id=""option1"" value=""{firstOption.Value}""  >
    <label for=""option1"" class=""selector-option"" role=""button"" >{firstOption.Key}</label>
    <input type=""radio"" name=""selector"" class=""right radio-input  {(selectedOption == ToggleSelectOption.SecondOption ? "checked" : "")}"" id=""option2"" value=""{secondOption.Value}""  >
    <label for=""option2"" class=""selector-option"" role=""button"" >{secondOption.Key}</label>
    <div class=""button"" ></div>
</div>";

		//Act
		var cut = RenderComponent<ToggleSelect<int>>(parameters => parameters
			.Add(p => p.FirstOption, firstOption)
			.Add(p => p.SecondOption, secondOption)
			.Add(p => p.SelectedOption, selectedOption));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "FirstOption Parameter Test"), AutoData]
	public void ToggleSelect_FirstOptionParameter_RendersCorrectly(
		ToggleSelectOption selectedOption,
		KeyValuePair<string, int> firstOption,
		KeyValuePair<string, int> secondOption)
	{
		//Act
		var cut = RenderComponent<ToggleSelect<int>>(parameters => parameters
			.Add(p => p.FirstOption, firstOption)
			.Add(p => p.SecondOption, secondOption)
			.Add(p => p.SelectedOption, selectedOption));

		var actualLabelText = cut.FindAll("label")[0].TextContent;
		var actualValue = cut.FindAll("input")[0].GetAttribute("value");

		//Assert
		actualLabelText.ShouldBe(firstOption.Key);
		actualValue.ShouldBe(firstOption.Value.ToString());
	}

	[Theory(DisplayName = "SecondOption Parameter Test"), AutoData]
	public void ToggleSelect_SecondOptionParameter_RendersCorrectly(
		ToggleSelectOption selectedOption,
		KeyValuePair<string, int> firstOption,
		KeyValuePair<string, int> secondOption)
	{
		//Act
		var cut = RenderComponent<ToggleSelect<int>>(parameters => parameters
			.Add(p => p.FirstOption, firstOption)
			.Add(p => p.SecondOption, secondOption)
			.Add(p => p.SelectedOption, selectedOption));

		var actualLabelText = cut.FindAll("label")[cut.FindAll("label").Count - 1].TextContent;
		var actualValue = cut.FindAll("input")[cut.FindAll("input").Count - 1].GetAttribute("value");

		//Assert
		actualLabelText.ShouldBe(secondOption.Key);
		actualValue.ShouldBe(secondOption.Value.ToString());
	}

	[Theory(DisplayName = "SelectedOption Parameter Test")]
	[InlineAutoData(ToggleSelectOption.FirstOption)]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void ToggleSelect_SelectedOptionParameter_RendersCorrectly(
		ToggleSelectOption selectedOption,
		KeyValuePair<string, int> firstOption,
		KeyValuePair<string, int> secondOption)
	{
		//Act
		var cut = RenderComponent<ToggleSelect<int>>(parameters => parameters
			.Add(p => p.FirstOption, firstOption)
			.Add(p => p.SecondOption, secondOption)
			.Add(p => p.SelectedOption, selectedOption));

		var inputs = cut.FindAll("input");
		var firstInputClasses = inputs[0].ClassList;
		var secondInputClasses = inputs[inputs.Count - 1].ClassList;
		var firstShouldBeChecked = selectedOption == ToggleSelectOption.FirstOption;
		var firstIsChecked = firstInputClasses.Contains("checked");
		var secondShouldBeChecked = selectedOption == ToggleSelectOption.SecondOption;
		var secondIsChecked = secondInputClasses.Contains("checked");

		//Assert
		firstIsChecked.ShouldBe(firstShouldBeChecked);
		secondIsChecked.ShouldBe(secondShouldBeChecked);
	}

	[Theory(DisplayName = "OnOptionChange Parameter Test")]
	[InlineAutoData(ToggleSelectOption.FirstOption)]
	[InlineAutoData(ToggleSelectOption.SecondOption)]
	public void ToggleSelect_OnOptionChangeParameter_RendersCorrectly(
		ToggleSelectOption selectedOption,
		KeyValuePair<string, int> firstOption,
		KeyValuePair<string, int> secondOption)
	{
		//Arrange
		var eventFired = false;
		var eventArgsOption = ToggleSelectOption.FirstOption;
		var cut = RenderComponent<ToggleSelect<int>>(parameters => parameters
			.Add(p => p.FirstOption, firstOption)
			.Add(p => p.SecondOption, secondOption)
			.Add(p => p.SelectedOption, selectedOption)
			.Add(p => p.OnOptionChange, args =>
			{
				eventFired = true;
				eventArgsOption = args;
			}));

		var inputs = cut.FindAll("input");
		var inputToClick =
			(selectedOption == ToggleSelectOption.FirstOption) ?
			inputs.First(x => x.ClassList.Contains("right")) :
			inputs.First(x => x.ClassList.Contains("left"));
		var expectedToggleSelectOption =
			selectedOption == ToggleSelectOption.FirstOption ?
			ToggleSelectOption.SecondOption :
			ToggleSelectOption.FirstOption;

		//Act
		inputToClick.Click();

		//Assert
		eventFired.ShouldBeTrue();
		eventArgsOption.ShouldBe(expectedToggleSelectOption);
	}
}



