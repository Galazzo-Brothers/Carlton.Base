using AutoFixture.Xunit2;
using Bunit;


namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedBreadCrumbsTests : TestContext
{

    [Theory, AutoData]
    public void ConnectedBreadCrumbsComponentRendersCorrectly(string componentName, string stateName)
    {
        //Arrange
        var vm = new BreadCrumbsViewModel(componentName, $"{componentName} > {stateName}");

        //ACt
        var cut = RenderComponent<ConnectedBreadCrumbs>(parameters => parameters
                    .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@$"
<div class=""page-title"" b-k79v7d1s3z><span class=""title"" b-k79v7d1s3z>{componentName}</span>
   <span class=""bread-crumbs"" b-k79v7d1s3z>{componentName} &gt; {stateName}</span>
</div>");
    }
}





