using Carlton.Core.Components.Tabs;

namespace Carlton.Core.Components.Tests.Common;

public static class Extensions
{
    public record TabConstructionData(string DisplayText, string IconClass, string ChildContent);

    public static void AddTabs<T>(this ComponentParameterCollectionBuilder<T> builder,
        IList<TabConstructionData> data)
        where T : IComponent
    {
        var numberOfTabs = data.Count;

        if (numberOfTabs == 0)
            return;

        for(var i = 0; i < numberOfTabs; i++)
        {
            builder.AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, data[i].DisplayText)
                .Add(p => p.Icon, data[i].IconClass)
                .Add(p => p.ChildContent, data[i].ChildContent));
        }
    }
}
