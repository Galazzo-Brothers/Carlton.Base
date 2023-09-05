using AutoFixture;
using AutoFixture.Xunit2;

namespace Carlton.Core.Components.Library.Tests;

[Trait("Component", nameof(ListCard<int>))]
public class ListCardComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ListCard_Markup_RendersCorrectly(
        Fixture fixture,
        string title,
        string subtitle,
        string headerContent)
    {
        //Arrange
        var items = fixture.CreateMany<int>(3);
        var expectedMarkup =
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
        <li>
          <span>{items.ElementAt(0)}</span>
        </li>
        <li>
          <span>{items.ElementAt(1)}</span>
        </li>
        <li>
          <span>{items.ElementAt(2)}</span>
        </li>
      </ul>
    </div>
  </div>
</div>";

        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
          .Add(p => p.CardTitle, title)
          .Add(p => p.SubTitle, subtitle)
          .Add(p => p.HeaderContent, headerContent)
          .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
          .Add(p => p.Items, items));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "Items Parameter Test"), AutoData]
    public void ListCard_ItemsParam_RendersCorrectly(
        string title,
        string subtitle,
        string headerContent,
        IReadOnlyCollection<int> items)
    {
        //Arrange
        var expectedCount = items.Count;

        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
         .Add(p => p.CardTitle, title)
         .Add(p => p.SubTitle, subtitle)
         .Add(p => p.HeaderContent, headerContent)
         .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
         .Add(p => p.Items, items));

        var liElements = cut.FindAll(".primary-content li");
        var actualCount = liElements.Count;
        var actualItems = liElements.Select(_ => int.Parse(_.TextContent));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(items, actualItems);
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
}
