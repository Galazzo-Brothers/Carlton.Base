using Carlton.Core.Components.Checkboxes;
namespace Carlton.Core.Components.Tests.Checkboxes;

[Trait("Component", nameof(Checkbox))]
public class CheckboxComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Checkbox_Markup_RendersCorrectly(bool expectedIsChecked)
    {
        //Arrange
        var expectedClass = expectedIsChecked ? "check-circle" : "checkbox-blank-circle-outline";
        var expectedMarkup = @$"<div class=""checkbox mdi mdi-24px mdi-{expectedClass}""></div>";
        
        //Act
        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, expectedIsChecked));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "IsChecked Parameter Test")]
    [InlineData(true)]
    [InlineData(false)]
    public void Checkbox_IsCheckedParameter_FiresCallback(bool expectedIsChecked)
    {
        //Arrange
        var eventCalled = false;
        var actualCheckedState = expectedIsChecked;
        var expectedState = !expectedIsChecked;

        var cut = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.IsChecked, expectedIsChecked)
            .Add(p => p.OnValueChange, (state) => { eventCalled = true; actualCheckedState = state; }));

        //Act
        cut.Find(".checkbox").Click();

        //Assert
        eventCalled.ShouldBeTrue();
        actualCheckedState.ShouldBe(expectedState);
    }
}
