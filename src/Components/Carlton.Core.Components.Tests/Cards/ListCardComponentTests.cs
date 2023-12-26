using AutoFixture.Xunit2;
namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(ListCard<int>))]
public class ListCardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test")]
    [InlineAutoData("<span>{0}</spa>n")]
    [InlineAutoData("<div>Some text {0} some more text</div>")]
    [InlineAutoData("<h1>{0}!!!!!!!!</h1>")]
    public void ListCard_Markup_RendersCorrectly(
        string itemTemplate,
        IEnumerable<int> items,
        string title,
        string subtitle,
        string headerContent)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
          .Add(p => p.CardTitle, title)
          .Add(p => p.SubTitle, subtitle)
          .Add(p => p.HeaderContent, headerContent)
          .Add(p => p.ItemTemplate, item => string.Format(itemTemplate, item))
          .Add(p => p.Items, items));

        //Assert
        var expectedMarkup = BuildExpectedMarktup(title, subtitle, headerContent, itemTemplate, items);
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Items Parameter Test")]
    [InlineAutoData("<span>{0}</span>")]
    [InlineAutoData("<div>Some text {0} some more text</div>")]
    [InlineAutoData("<h1>{0}!!!!!!!!</h1>")]
    public void ListCard_ItemsParam_RendersCorrectly(
        string itemTemplate,
        string title,
        string subtitle,
        string headerContent,
        IEnumerable<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, title)
         .Add(p => p.SubTitle, subtitle)
         .Add(p => p.HeaderContent, headerContent)
         .Add(p => p.ItemTemplate, item => string.Format(itemTemplate, item))
         .Add(p => p.Items, items));

        var liElements = cut.FindAll(".primary-content li");
        var actualCount = liElements.Count;
        var actualContent = liElements.Select(_ => _.InnerHtml);

        //Assert
        Assert.Equal(items.Count(), actualCount);
        Assert.All(actualContent, (actual, i) =>
        {
            var condition = actual.Contains(items.ElementAt(i).ToString());
            Assert.True(condition);
        });
    }

    [Theory(DisplayName = "CardTitle Parameter Test"), AutoData]
    public void ListCard_CardTitleParam_RendersCorrectly(
        string title,
        string subtitle,
        string headerContent,
        IReadOnlyCollection<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, title)
            .Add(p => p.SubTitle, subtitle)
            .Add(p => p.HeaderContent, headerContent)
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, items));

        var cardTitle = cut.Find(".card-title").TextContent;

        //Assert
        Assert.Equal(title, cardTitle);
    }

    [Theory(DisplayName = "CardSubTitle Parameter Test"), AutoData]
    public void ListCard_CardSubTitleParam_RendersCorrectly(string title, string subtitle, string headerContent, IReadOnlyCollection<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
          .Add(p => p.CardTitle, title)
          .Add(p => p.SubTitle, subtitle)
          .Add(p => p.HeaderContent, headerContent)
          .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
          .Add(p => p.Items, items));

        var cardTitle = cut.Find(".sub-title").TextContent;

        //Assert
        Assert.Equal(subtitle, cardTitle);
    }

    [Theory(DisplayName = "HeaderContentChild Parameter Test"), AutoData]
    public void ListCard_HeaderContentChildParam_RendersCorrectly(
        string title,
        string subtitle,
        string headerContent,
        IReadOnlyCollection<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, title)
         .Add(p => p.SubTitle, subtitle)
         .Add(p => p.HeaderContent, headerContent)
         .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
         .Add(p => p.Items, items));

        var actualHeaderContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal(actualHeaderContent, headerContent);
    }

    [Theory(DisplayName = "ItemTemplate Parameter Test")]
    [InlineAutoData("<span>{0}</span>")]
    [InlineAutoData("<div>Some text {0} some more text</div>")]
    [InlineAutoData("<h1>{0}!!!!!!!!</h1>")]
    public void ListCard_ItemTemplateParam_RendersCorrectly(
       string itemTemplate,
       string title,
       string subtitle,
       string headerContent,
       IReadOnlyCollection<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, title)
         .Add(p => p.SubTitle, subtitle)
         .Add(p => p.HeaderContent, headerContent)
         .Add(p => p.ItemTemplate, item => string.Format(itemTemplate, item))
         .Add(p => p.Items, items));

        var liElements = cut.FindAll(".primary-content li");
        var actualContent = liElements.Select(_ => _.InnerHtml);

        //Assert
        Assert.All(actualContent, (actual, i) =>
        {
            var expected = string.Format(itemTemplate, items.ElementAt(i));
            Assert.Equal(expected, actual);
        });
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
        <div class=""dropdown-menu"">
          <div class=""menu"">
            <i class=""mdi mdi-24px mdi-dots-vertical""></i>
          </div>
          <div class=""dropdown "" style=""--dropdown-left:10px;--dropdown-top:10px;--dropdown-top-mobile:10px;"">
            <ul>
                 <div class=""header""></div>
            </ul>
          </div>
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
