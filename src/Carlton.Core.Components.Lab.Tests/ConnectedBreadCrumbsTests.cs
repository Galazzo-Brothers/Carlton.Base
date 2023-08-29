using Bunit;


namespace Carlton.Core.Components.Lab.Tests;

public class ConnectedBreadCrumbsTests : TestContext
{
    [Fact]
    public void ConnectedBreadCrumbsComponentRendersCorrectly()
    {
        //Arrange
        var vm = new BreadCrumbsViewModel("TestComponent", "TestComponent > Test State 1");
        
        //ACt
        var cut = RenderComponent<ConnectedBreadCrumbs>(parameters => parameters
                    .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@"
<div class=""page-title"" b-k79v7d1s3z><span class=""title"" b-k79v7d1s3z>TestComponent</span>
   <span class=""bread-crumbs"" b-k79v7d1s3z>TestComponent &gt; Test State 1</span>
</div>");
    }
}





