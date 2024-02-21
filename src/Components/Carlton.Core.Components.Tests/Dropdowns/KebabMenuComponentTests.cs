using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Tests.Dropdowns;

[Trait("Component", nameof(KebabMenu<int>))]
public class KebabMenuComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData(true)]
    [InlineAutoData(false)]
    public void KebabMenu_Markup_RendersCorrectly(
        bool expectedIsDisabled,
        IEnumerable<DropdownMenuItem<int>> expectedMenuItems)
    {
        //Arrange
        var expectedMarkup = BuildExpectedMarkup(expectedMenuItems, expectedIsDisabled);

        //Act
        var cut = RenderComponent<KebabMenu<int>>(parameters => parameters
            .Add(p => p.MenuItems, expectedMenuItems)
            .Add(p => p.IsDisabled, expectedIsDisabled));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact(DisplayName = "Markup Test")]
    public void KebabMenu_EmptyItems_Markup_RendersCorrectly()
    {
        //Arrange
        var emptyItems = Enumerable.Empty<DropdownMenuItem<int>>();
        var expectedMarkup = BuildExpectedMarkup(emptyItems, true);

        //Act
        var cut = RenderComponent<KebabMenu<int>>(parameters => parameters
            .Add(p => p.MenuItems, emptyItems)
            .Add(p => p.IsDisabled, false));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Click Test"), AutoData]
    public void KebabMenu_Click_RendersCorrectly(
        IEnumerable<DropdownMenuItem<int>> expectedMenuItems)
    {
        //Arrange
        var expectedMarkup = BuildExpectedMarkup(expectedMenuItems, false);
        var cut = RenderComponent<KebabMenu<int>>(parameters => parameters
            .Add(p => p.MenuItems, expectedMenuItems)
            .Add(p => p.IsDisabled, false));
        var optionsElement = cut.Find(".options");
        var iElement = cut.Find("i");

        //Act
        iElement.Click();

        //Assert
        iElement.ClassList.ShouldNotContain("mdi-dots-vertical");
        iElement.ClassList.ShouldContain("mdi-close-circle");
        optionsElement.ClassList.ShouldContain("active");
    }

    [Theory(DisplayName = "Inactive Click Test"), AutoData]
    public void KebabMenu_Inactive_Click_RendersCorrectly(
       IEnumerable<DropdownMenuItem<int>> expectedMenuItems)
    {
        //Arrange
        var expectedMarkup = BuildExpectedMarkup(expectedMenuItems, true);
        var cut = RenderComponent<KebabMenu<int>>(parameters => parameters
            .Add(p => p.MenuItems, expectedMenuItems)
            .Add(p => p.IsDisabled, true));
        var optionsElement = cut.Find(".options");
        var iElement = cut.Find("i");

        //Act
        iElement.Click();

        //Assert
        iElement.ClassList.ShouldContain("mdi-dots-vertical");
        iElement.ClassList.ShouldNotContain("mdi-close-circle");
        optionsElement.ClassList.ShouldNotContain("active");
    }

    [Theory(DisplayName = "MenuItems Parameter Callback Test"), AutoData]
    public void Kebab_MenuItemsParameterCallback_RendersCorrectly(
        IEnumerable<DropdownMenuItem<int>> expectedMenuItems)
    {
        //Arrange
        var selectedIndex = RandomUtilities.GetRandomIndex(expectedMenuItems.Count());
        var callbackFired = false;
        var callbackIndex = -1;
        var expectedItemsWithCallback = expectedMenuItems.Select((item, i) =>
            item with
            {
                MenuItemSelected = () =>
                {
                    callbackFired = true;
                    callbackIndex = i;
                }
            });

        var cut = RenderComponent<KebabMenu<int>>(parameters => parameters
             .Add(p => p.MenuItems, expectedItemsWithCallback)
             .Add(p => p.IsDisabled, false));

        //Act
        cut.FindAll(".option").ElementAt(selectedIndex).Click();

        //Assert
        callbackFired.ShouldBeTrue();
        callbackIndex.ShouldBe(selectedIndex);
    }

    private static string BuildExpectedMarkup(IEnumerable<DropdownMenuItem<int>> items, bool isDisabled)
    {
        var optionsMarkup = string.Join(Environment.NewLine, items.Select(item =>
          $@"<div class=""option"">{item.MenuItemName}</div>"
        ));

        return
        @$"
        <div class=""kebab-dropdown"">
            <i class=""{(isDisabled ? "disabled" : string.Empty )} mdi mdi-24px mdi-dots-vertical""  ></i>
            <div class=""options"">
                {optionsMarkup}
            </div>
        </div>";
    }
}
