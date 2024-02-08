using Carlton.Core.Components.Modals;
namespace Carlton.Core.Components.Tests.Modals;

[Trait("Component", nameof(ModalDismissMark))]
public class ModalDismissMarkComponentTests : TestContext
{
    [Fact(DisplayName = "Markup Test")]
    public void ModalDismissMark_Markup_RendersCorrectly()
    {
        //Arrange
        var expectedMarkup = @$"<span class=""close"">&times;</span>";

        //Act
        var cut = RenderComponent<ModalDismissMark>();

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact(DisplayName = "OnDismiss Parameter Test")]
    public void ModalDismiMark_OnDismissParameter_FiresEvent()
    {
        //Arrange
        var eventFired = false; ;
        var cut = RenderComponent<ModalDismissMark>(parameters =>
            parameters.Add(p => p.Dismiss, () => eventFired = true));

        //Act
        cut.Find(".close").Click();

        //Assert
        eventFired.ShouldBeTrue();
    }
}
