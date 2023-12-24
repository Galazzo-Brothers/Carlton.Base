namespace Carlton.Core.Components.Library.Tests.Common;

public static class Extensions
{
    public static string AddTabs<T>(this ComponentParameterCollectionBuilder<T> builder,
        int numberOfTabs,
        int activeTabIndex,
        List<string> displayText,
        List<string> icon,
        List<string> childContent)
        where T : TabBarBase
    {
        if(numberOfTabs == 0)
            return string.Empty;

        for(var i = 0; i < numberOfTabs; i++)
        {
            builder.AddChildContent<Tab>(parameters => parameters
                .Add(p => p.DisplayText, displayText[i])
                .Add(p => p.Icon, icon[i])
                .Add(p => p.ChildContent, childContent[i]));
        }

        return childContent[activeTabIndex];
    }
}
