using AutoFixture;

namespace Carlton.Core.Components.Library.Tests.Common;

public static class Extensions
{
    public static void AddTabs<T>(this ComponentParameterCollectionBuilder<T> builder, int numberOfTabs)
        where T : TabBarBase
    {
        var dummy = string.Empty;
        AddTabs(builder, numberOfTabs, 0, ref dummy);
    }

    public static string AddTabs<T>(this ComponentParameterCollectionBuilder<T> builder,
        int numberOfTabs,
        int activeTabIndex,
        ref string expectedContent)
        where T : TabBarBase
    {
        var fixture = new Fixture();
        var displayText = fixture.CreateMany<string>(numberOfTabs).ToList();
        var icon = fixture.CreateMany<string>(numberOfTabs).ToList();
        var childContent = fixture.CreateMany<string>(numberOfTabs).ToList();

        if(numberOfTabs == 0)
            return string.Empty;

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

    public static IEnumerable<SelectGroup<int>> FixIndexes(this IEnumerable<SelectGroup<int>> templateGroups)
    {
        var groups = new List<SelectGroup<int>>();

        foreach(var (group, groupIndex) in templateGroups.WithIndex())
        {
            var groupItems = new List<SelectItem<int>>();
            foreach(var (item, itemIndex) in group.Items.WithIndex())
            {
                groupItems.Add(item with { Index = itemIndex });
            }
            groups.Add(new SelectGroup<int>(group.Name, groupIndex, groupItems));
        }

        return groups;
    }

}
