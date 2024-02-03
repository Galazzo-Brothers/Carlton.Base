using AutoFixture;
using Carlton.Core.Components.Navigation;

namespace Carlton.Core.Components.Library.Tests.Common;

public static class TestAccordionSelectGroupBuilder<TValue>
{
    public static IEnumerable<SelectGroup<TValue>> BuildTestSelectGroups()
    {
        var fixture = new Fixture();
        var numOfGroups = fixture.Create<int>();
        var builder = new AccordionSelectGroupBuilder<TValue>();

        for (var i = 0; i < numOfGroups; i++)
        {
            var name = fixture.Create<string>();
            var isExpanded = fixture.Create<bool>();
            var items = fixture.Create<Dictionary<string, TValue>>();
            builder.AddGroup(name, isExpanded, items);
        }

        return builder.Build();
    }
}
