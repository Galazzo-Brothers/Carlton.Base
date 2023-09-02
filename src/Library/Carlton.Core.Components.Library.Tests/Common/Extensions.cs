using AutoFixture;

namespace Carlton.Core.Components.Library.Tests.Common;

public static class Extensions
{
    public static void AddTabs(this ComponentParameterCollectionBuilder<MobileTabBar> builder, int numberOfTabs)
    {
        var dummy = string.Empty;
        AddTabs(builder, numberOfTabs, 0, ref dummy);
    }

    public static string AddTabs(this ComponentParameterCollectionBuilder<MobileTabBar> builder,
        int numberOfTabs,
        int activeTabIndex,
        ref string expectedContent)
    {
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numberOfTabs).ToList();
        var icon = fixture.CreateMany<string>(numberOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numberOfTabs).ToList();


        for(var i = 0; i < numberOfTabs; i++)
        {
            builder.AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[i])
                .Add(p => p.Icon, icon[i])
                .Add(p => p.ChildContent, childContent[i]));
        }

        expectedContent = childContent[activeTabIndex];
        return childContent[activeTabIndex];
    }
}
