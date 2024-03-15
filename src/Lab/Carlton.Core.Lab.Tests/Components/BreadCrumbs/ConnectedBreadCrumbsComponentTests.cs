using Carlton.Core.Lab.Components.BreadCrumbs;
namespace Carlton.Core.Lab.Test.Components.BreadCrumbs;

public class ConnectedBreadCrumbsComponentTests : TestContext
{
	[Theory, AutoData]
	public void ConnectedBreadCrumbs_RendersCorrectly(
		BreadCrumbsViewModel expectedViewModel)
	{
		//ACt
		var cut = RenderComponent<ConnectedBreadCrumbs>(parameters => parameters
					.Add(p => p.ViewModel, expectedViewModel));

		//Assert
		cut.MarkupMatches(@$"
<div class=""bread-crumbs"">
	<span class=""page-title"">{expectedViewModel.SelectedComponent}</span>
   <span class=""page-bread-crumbs"">{expectedViewModel.SelectedComponentState}</span>
</div>");
	}
}





