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

    [Fact]
    public void ListCard_RendersCorrectNumberOfItems()
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Title")
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var liElements = cut.FindAll(".primary-content li");

        //Assert
        Assert.Equal(3, liElements.Count);
    }

    [Fact]
    public void ListCard_CardTitleParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Test Title")
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var cardTitle = cut.Find(".card-title").TextContent;

        //Assert
        Assert.Equal("List Card Test Title", cardTitle);
    }

    [Fact]
    public void ListCard_CardSubTitleParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Test Title")
            .Add(p => p.SubTitle, "List Card Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var cardTitle = cut.Find(".sub-title").TextContent;

        //Assert
        Assert.Equal("List Card Test Subtitle", cardTitle);
    }

    [Fact]
    public void ListCard_HeaderContentChildParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Test Title")
            .Add(p => p.SubTitle, "List Card Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Testing Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, _items)
            );

        var headerContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal("<span>Header Testing Content</span>", headerContent);
    }
}
