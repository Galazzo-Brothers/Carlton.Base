using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Tests.Dropdowns;

[Trait("Component", nameof(DropdownMenu<int>))]
public class DropdownMenuComponentTests : TestContext
{
	[Theory(DisplayName = "Markup Test"), AutoData]
	public void DropdownMenu_Markup_RendersCorrectly(
	  IEnumerable<DropdownMenuItem<int>> expectedMenuItems,
	  string expectedMenuTemplate,
	  string expectedHeaderTemplate)
	{
		//Arrange
		var expectedMarkup = BuildExpectedMarkup(expectedMenuItems, expectedMenuTemplate, expectedHeaderTemplate);

		//Act
		var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
			.Add(p => p.MenuItems, expectedMenuItems)
			.Add(p => p.MenuTemplate, expectedMenuTemplate)
			.Add(p => p.HeaderTemplate, expectedHeaderTemplate));

		//Assert
		cut.MarkupMatches(expectedMarkup);
	}

	[Theory(DisplayName = "MenuTemplate Parameter Test"), AutoData]
	public void DropdownMenu_MenuTemplateParameter_RendersCorrectly(
	   IEnumerable<DropdownMenuItem<int>> expectedMenuItems,
	   string expectedMenuTemplate,
	   string expectedHeaderTemplate)
	{
		//Act
		var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
			.Add(p => p.MenuItems, expectedMenuItems)
			.Add(p => p.MenuTemplate, expectedMenuTemplate)
			.Add(p => p.HeaderTemplate, expectedHeaderTemplate));

		var actualMenuContent = cut.Find(".menu-template").TextContent;

		//Assert
		actualMenuContent.ShouldBe(expectedMenuTemplate);
	}

	[Theory(DisplayName = "HeaderTemplate Parameter Test"), AutoData]
	public void DropdownMenu_HeaderTemplateParemter_RendersCorrectly(
	   IEnumerable<DropdownMenuItem<int>> expectedMenuItems,
	   string expectedMenuTemplate,
	   string expectedHeaderTemplate)
	{
		//Act
		var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
			.Add(p => p.MenuItems, expectedMenuItems)
			.Add(p => p.MenuTemplate, expectedMenuTemplate)
			.Add(p => p.HeaderTemplate, expectedHeaderTemplate));

		var actualHeaderContent = cut.Find(".header-template").TextContent;

		//Assert
		actualHeaderContent.ShouldBe(expectedHeaderTemplate);
	}

	[Theory(DisplayName = "MenuItems Parameter Test"), AutoData]
	public void DropdownMenu_MenuItemsParameter_RendersCorrectly(
		IEnumerable<DropdownMenuItem<int>> expectedMenuItems,
		string expectedMenuTemplate,
		string expectedHeaderTemplate)
	{
		//Act
		var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
			.Add(p => p.MenuItems, expectedMenuItems)
			.Add(p => p.MenuTemplate, expectedMenuTemplate)
			.Add(p => p.HeaderTemplate, expectedHeaderTemplate));

		var actualItemContent = cut.FindAll("li a").Select(x => x.TextContent);
		var expectedHeaderContent = expectedMenuItems.Select(x => x.MenuItemName);

		//Assert
		actualItemContent.Count().ShouldBe(expectedMenuItems.Count());
		actualItemContent.ShouldBe(expectedHeaderContent);
	}

	[Theory(DisplayName = "MenuItems Parameter Callback Test"), AutoData]
	public void DropdownMenu_MenuItemsParameterCallback_RendersCorrectly(
		IEnumerable<DropdownMenuItem<int>> expectedMenuItems,
		string expectedMenuTemplate,
		string expectedHeaderTemplate)
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

		var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
			.Add(p => p.MenuItems, expectedItemsWithCallback)
			.Add(p => p.MenuTemplate, expectedMenuTemplate)
			.Add(p => p.HeaderTemplate, expectedHeaderTemplate));

		//Act
		cut.FindAll("a").ElementAt(selectedIndex).Click();

		//Assert
		callbackFired.ShouldBeTrue();
		callbackIndex.ShouldBe(selectedIndex);
	}

	[Theory(DisplayName = "MenuItems Expanded Options Click Test"), AutoData]
	public void DropdownMenu_ExpandOptionsInteraction_RendersCorrectly(
	   IEnumerable<DropdownMenuItem<int>> expectedMenuItems,
	   string expectedMenuTemplate,
	   string expectedHeaderTemplate)
	{
		//Arrange
		var selectedIndex = RandomUtilities.GetRandomIndex(expectedMenuItems.Count());
		var cut = RenderComponent<DropdownMenu<int>>(parameters => parameters
			.Add(p => p.MenuItems, expectedMenuItems)
			.Add(p => p.MenuTemplate, expectedMenuTemplate)
			.Add(p => p.HeaderTemplate, expectedHeaderTemplate));

		//Act
		cut.Find(".dropdown-menu").Click();
		var itemsElement = cut.Find(".dropdown-items");

		//Assert
		itemsElement.ClassList.ShouldContain("active");
	}

	private static string BuildExpectedMarkup(IEnumerable<DropdownMenuItem<int>> expectedMenuItems, string expectedMenuTemplate, string expectedHeaderTemplate)
	{
		var itemElements = string.Join(Environment.NewLine, expectedMenuItems.Select(item => $@"
        <li>
            <span class=""mdi mdi-{item.MenuIcon} accent-color-{item.AccentColorIndex}"" ></span>
            <a href=""#"">{item.MenuItemName}</a>
        </li>"));

		var expectedMarkup = $@"
        <div class=""dropdown-menu"">
            <div class=""menu-template"">{expectedMenuTemplate}</div>
            <div class=""dropdown-items"">
            <div class=""header-template"">{expectedHeaderTemplate}</div>
            <ul>{itemElements}</ul>
          </div>
        </div>";

		return expectedMarkup;
	}
}
