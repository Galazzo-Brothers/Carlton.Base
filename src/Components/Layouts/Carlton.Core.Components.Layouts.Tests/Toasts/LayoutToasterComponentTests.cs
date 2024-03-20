using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Modals;
using Carlton.Core.Components.Toasts;
namespace Carlton.Core.Components.Layouts.Tests.Toasts;


[Trait("Component", nameof(LayoutToaster))]
public class LayoutToasterComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), InlineAutoData]
    public void LayoutToaster_Markup_RendersCorrectly(
        int expectedTop,
        int expectedRight)
    {
        //Arrange
        ComponentFactories.AddStub<Modal>();
        var expectedMarkup = $@"
         <div class=""layout-toaster"" >
            <div class=""content"" style=""--toast-top:{expectedTop}px;--toast-right:{expectedRight}px;"" ></div>
        </div>";

        //Act
        var cut = RenderComponent<LayoutToaster>(parameters =>
            parameters.Add(p => p.Top, expectedTop)
                      .Add(p => p.Right, expectedRight));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Top/Left Parameter Test"), AutoData]
    public void LayoutToaster_TopLeftParameter_RendersCorrectly(
       int expectedTop,
       int expectedRight)
    {
        //Arrange
        ComponentFactories.AddStub<Modal>();

        //Act
        var cut = RenderComponent<LayoutToaster>(parameters =>
            parameters.Add(p => p.Top, expectedTop)
                      .Add(p => p.Right, expectedRight));

        var actualStyle = cut.Find(".content").Attributes["style"].TextContent.Split(';');
        var top = actualStyle[0].Split(":")[1];
        var right = actualStyle[1].Split(":")[1];
        var actualTopValue = int.Parse(top[..^2]);
        var actualRightValue = int.Parse(right[..^2]);

        //Assert
        actualTopValue.ShouldBe(expectedTop);
        actualRightValue.ShouldBe(expectedRight);
    }

    [Theory(DisplayName = "GenerateToast Test"), AutoData]
    public void LayoutToaster_GenerateToast_RendersCorrectly(
       int expectedTop,
       int expectedRight,
       bool expectedFadeOutEnabled,
       ToastViewModel expectedToastViewModel)
    {
        //Arrange
        ComponentFactories.AddStub<Modal>();
        var cut = RenderComponent<LayoutToaster>(parameters =>
            parameters.Add(p => p.Top, expectedTop)
                      .Add(p => p.Right, expectedRight)
                      .Add(p => p.FadeOutEnabled, expectedFadeOutEnabled));

        //Act
        cut.InvokeAsync(() => cut.Instance.GenerateToast(expectedToastViewModel));

        //Assert
        var toast = cut.FindComponent<Toast>();
        toast.Instance.Id.ShouldBe(expectedToastViewModel.Id);
        toast.Instance.FadeOutEnabled.ShouldBe(expectedFadeOutEnabled);
        toast.Instance.Title.ShouldBe(expectedToastViewModel.Title);
        toast.Instance.Message.ShouldBe(expectedToastViewModel.Message);
    }
}
