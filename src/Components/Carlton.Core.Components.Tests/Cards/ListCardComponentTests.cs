using Carlton.Core.Components.Cards;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(ListCard<int>))]
public class ListCardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData("<span>{0}</spa>n")]
    [InlineAutoData("<div>Some text {0} some more text</div>")]
    [InlineAutoData("<h1>{0}!!!!!!!!</h1>")]
    public void ListCard_Markup_RendersCorrectly(
        string expectedItemTemplate,
        IEnumerable<int> expectedItems,
        string expectedTitle,
        string expectedSubtitle,
        string expectedHeaderContent)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
          .Add(p => p.CardTitle, expectedTitle)
          .Add(p => p.SubTitle, expectedSubtitle)
          .Add(p => p.HeaderContent, expectedHeaderContent)
          .Add(p => p.ItemTemplate, item => string.Format(expectedItemTemplate, item))
          .Add(p => p.Items, expectedItems));

        //Assert
        var expectedMarkup = BuildExpectedMarktup(expectedTitle, expectedSubtitle, expectedHeaderContent, expectedItemTemplate, expectedItems);
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Items Parameter Test")]
    [InlineAutoData("<span>{0}</span>")]
    [InlineAutoData("<div>Some text {0} some more text</div>")]
    [InlineAutoData("<h1>{0}!!!!!!!!</h1>")]
    public void ListCard_ItemsParameter_RendersCorrectly(
        string expectedItemTemplate,
        string expectedTitle,
        string expectedSubtitle,
        string expectedHeaderContent,
        IEnumerable<int> expectedItems)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, expectedTitle)
         .Add(p => p.SubTitle, expectedSubtitle)
         .Add(p => p.HeaderContent, expectedHeaderContent)
         .Add(p => p.ItemTemplate, item => string.Format(expectedItemTemplate, item))
         .Add(p => p.Items, expectedItems));

        var expectedContent = expectedItems.Select(_ => string.Format(expectedItemTemplate, _));
        var liElements = cut.FindAll(".primary-content li");
        var actualCount = liElements.Count;
        var actualContent = liElements.Select(_ => _.InnerHtml);

        //Assert
        actualCount.ShouldBe(expectedItems.Count());
        actualContent.ShouldBe(expectedContent);
    }

    [Theory(DisplayName = "CardTitle Parameter Test"), AutoData]
    public void ListCard_CardTitleParameter_RendersCorrectly(
        string expectedTitle,
        string expectedSubtitle,
        string headerContent,
        IReadOnlyCollection<int> expectedItems)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, expectedTitle)
            .Add(p => p.SubTitle, expectedSubtitle)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, expectedItems));

        var actualCardTitle = cut.Find(".card-title").TextContent;

        //Assert
        actualCardTitle.ShouldBe(expectedTitle);
    }

    [Theory(DisplayName = "CardSubTitle Parameter Test"), AutoData]
    public void ListCard_CardSubTitleParameter_RendersCorrectly(
        string expectedTitle,
        string expectedSubtitle,
        string expectedHeaderContent,
        IReadOnlyCollection<int> expectedItems)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
          .Add(p => p.CardTitle, expectedTitle)
          .Add(p => p.SubTitle, expectedSubtitle)
          .Add(p => p.HeaderContent, expectedHeaderContent)
          .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
          .Add(p => p.Items, expectedItems));

        var actualCardTitle = cut.Find(".sub-title").TextContent;

        //Assert
        actualCardTitle.ShouldBe(expectedSubtitle);
    }

    [Theory(DisplayName = "HeaderContentChild Parameter Test"), AutoData]
    public void ListCard_HeaderContentChildParameter_RendersCorrectly(
        string expectedTitle,
        string expectedSubtitle,
        string expectedHeaderContent,
        IReadOnlyCollection<int> expectedItems)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, expectedTitle)
         .Add(p => p.SubTitle, expectedSubtitle)
         .Add(p => p.HeaderContent, expectedHeaderContent)
         .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
         .Add(p => p.Items, expectedItems));

        var actualHeaderContent = cut.Find(".header-content").InnerHtml;

        //Assert
        actualHeaderContent.ShouldBe(expectedHeaderContent);
    }

    [Theory(DisplayName = "ItemTemplate Parameter Test")]
    [InlineAutoData("<span>{0}</span>")]
    [InlineAutoData("<div>Some text {0} some more text</div>")]
    [InlineAutoData("<h1>{0}!!!!!!!!</h1>")]
    public void ListCard_ItemTemplateParameter_RendersCorrectly(
       string expectedItemTemplate,
       string expectedTitle,
       string expectedSubtitle,
       string expectedHeaderContent,
       IReadOnlyCollection<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, expectedTitle)
         .Add(p => p.SubTitle, expectedSubtitle)
         .Add(p => p.HeaderContent, expectedHeaderContent)
         .Add(p => p.ItemTemplate, item => string.Format(expectedItemTemplate, item))
         .Add(p => p.Items, items));

        var expectedContent = items.Select(_ => string.Format(expectedItemTemplate, _));
        var liElements = cut.FindAll(".primary-content li");
        var actualContent = liElements.Select(_ => _.InnerHtml);

        //Assert
        actualContent.ShouldBe(expectedContent);
    }

    private static string BuildExpectedMarktup(string title, string subtitle, string headerContent, string itemTemplate, IEnumerable<int> items)
    {
        var itemMarkup = string.Join(Environment.NewLine, items
           .Select(item => $@"
                    <li>
                        {string.Format(itemTemplate, item)}
                    </li>
                "));

        return
@$"<div class=""card"">
  <div class=""content"">
    <div class=""title-content"">
      <span class=""card-title"">{title}</span>
      <div class=""status-icon"">
       <div class=""kebab-dropdown"" >
          <i class=""mdi mdi-24px disabled mdi-dots-vertical""  ></i>
          <div class=""options"" ></div>
        </div>
      </div>
    </div>
    <div class=""header-content"">
      {headerContent}
    </div>
    <div class=""primary-content"">
      <div class=""sub-title"">{subtitle}</div>
      <ul>
        {itemMarkup}
      </ul>
    </div>
  </div>
</div>";
    }
}
