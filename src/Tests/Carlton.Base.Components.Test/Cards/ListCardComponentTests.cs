namespace Carlton.Base.Components.Test;

public class ListCardComponentTests : TestContext
{
    [Fact]
    public void ListCard_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Title")
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, CardTestHelper.Items)
            );

        //Assert
        cut.MarkupMatches(CardTestHelper.ListCardMarkup);
    }

    [Theory]
    [MemberData(nameof(CardTestHelper.GetItems), MemberType = typeof(CardTestHelper))]
    public void ListCard_ItemsParam_RendersCorrectly(ReadOnlyCollection<int> expectedItems)
    {
        //Arrange
        var expectedCount = expectedItems.Count;

        //Act
        var cut = RenderComponent<ListCard<int>>(parameters => parameters
            .Add(p => p.CardTitle, "List Card Title")
            .Add(p => p.SubTitle, "Some Test Subtitle")
            .Add(p => p.HeaderContent, "<span>Header Content</span>")
            .Add(p => p.ItemTemplate, item => $"<span>{item}</span>")
            .Add(p => p.Items, expectedItems)
            );

        var liElements = cut.FindAll(".primary-content li");
        var actualCount = liElements.Count;
        var actualItems = liElements.Select(_ => int.Parse(_.TextContent));

        //Assert
        Assert.Equal(expectedCount, actualCount);
        Assert.Equal(expectedItems, actualItems);
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
            .Add(p => p.Items, CardTestHelper.Items)
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
            .Add(p => p.Items, CardTestHelper.Items)
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
            .Add(p => p.Items, CardTestHelper.Items)
            );

        var headerContent = cut.Find(".header-content").InnerHtml;

        //Assert
        Assert.Equal(expectedHeaderContent, headerContent);
    }
}
