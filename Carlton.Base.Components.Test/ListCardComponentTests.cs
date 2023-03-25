namespace Carlton.Base.Components.Test;

public class ListCardComponentTests : TestContext
{
    private static readonly string ListCardMarkup = @"
<div class=""card"" b-g3swmy425k>
  <div class=""content"" b-g3swmy425k>
    <div class=""title-content"" b-g3swmy425k>
      <span class=""card-title"" b-g3swmy425k>List Card Title</span>
      <div class=""status-icon"" b-g3swmy425k>
        <div class=""dropdown-menu"" blazor:onclick=""1"" b-e7te7pxji7>
          <div class=""menu"" b-e7te7pxji7>
            <i class=""mdi mdi-24px mdi-dots-vertical"" b-m40yy2q1d3></i>
          </div>
          <div class=""dropdown "" style=""--dropdown-left:10px;--dropdown-top:10px;--dropdown-top-mobile:10px;"" b-e7te7pxji7>
            <ul b-e7te7pxji7>
                 <div class=""header"" b-e7te7pxji7></div>
            </ul>
          </div>
        </div>
      </div>
    </div>
    <div class=""header-content"" b-g3swmy425k>
      <span>Header Content</span>
    </div>
    <div class=""primary-content"" b-g3swmy425k>
      <div class=""sub-title"" b-22crlgp3bk>Some Test Subtitle</div>
      <ul b-22crlgp3bk>
        <li b-22crlgp3bk>
          <span>1</span>
        </li>
        <li b-22crlgp3bk>
          <span>2</span>
        </li>
        <li b-22crlgp3bk>
          <span>3</span>
        </li>
      </ul>
    </div>
  </div>
</div>";


    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
           {
               new List<int> { 1, 2, 3 }
           };
        yield return new object[]
           {
              new List<int> { 1, 2, 3, 10, 15 }
            };
        yield return new object[]
            {
                new List<int> { 7 }
            };
    }

    private static readonly IEnumerable<int> _items = new List<int> { 1, 2, 3 };


    [Fact]
    public void ListCard_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Title")
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        //Assert
        cut.MarkupMatches(ListCardMarkup);
    }

    [Theory]
    [MemberData(nameof(GetItems))]
    public void ListCard_ItemsParam_RendersCorrectly(IEnumerable<int> items)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Title")
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, items)
            );

        var liElements = cut.FindAll(".primary-content li");

        //Assert
        Assert.Equal(items.Count(), liElements.Count);
    }

    [Theory]
    [InlineData("Test Title 1")]
    [InlineData("Test Title 2")]
    [InlineData("Test Title 3")]
    public void ListCard_CardTitleParam_RendersCorrectly(string title)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, title)
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var cardTitle = cut.Find(".card-title").TextContent;

        //Assert
        Assert.Equal(title, cardTitle);
    }

    [Theory]
    [InlineData("Test Subtitle 1")]
    [InlineData("Test Subtitle 2")]
    [InlineData("Test Subtitle 3")]
    public void ListCard_CardSubTitleParam_RendersCorrectly(string subtitle)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Test Title")
            .Add(p => p.SubTitle, subtitle)
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var cardTitle = cut.Find(".sub-title").TextContent;

        //Assert
        Assert.Equal(subtitle, cardTitle);
    }

    [Theory]
    [InlineData("<span>Header Testing Content</span>")]
    [InlineData("<span>More Header Testing Content</span>")]
    [InlineData("<span>Event More Header Testing Content</span>")]
    public void ListCard_HeaderContentChildParam_RendersCorrectly(string expectedHeaderContent)
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Test Title")
            .Add(p => p.SubTitle, "List Card Test Subtitle")
            .Add(p => p.HeaderContent, expectedHeaderContent)
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var headerContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal(expectedHeaderContent, headerContent);
    }
}
