using AutoFixture;
using Carlton.Core.Components.Navigation;
namespace Carlton.Core.Components.Tests.Navigation;

internal static class TestAccordionSelectGroupBuilder<TValue>
{
    public static IEnumerable<AccordionSelectGroupModel<TValue>> BuildTestSelectGroups()
    {
        var fixture = new Fixture();
        var numOfGroups = fixture.Create<int>();
        var builder = new AccordionSelectGroupModelBuilder<TValue>();

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
