using AutoFixture;
using Carlton.Core.Components.Library.AccordionSelect;

namespace Carlton.Core.Components.Library.Tests.Common;

public class TestAccordionSelectGroupBuilder<TValue>
{
    private readonly IFixture _fixture = new Fixture();

    public IEnumerable<SelectGroup<TValue>> BuildTestSelectGroup(int numOfGroups, int[] numOfItems)
    {
        var isExpanded = new List<bool>();

        for (var i = 0; i < numOfGroups; i++)
            isExpanded.Add(false);

        return BuildTestSelectGroup(numOfGroups, numOfItems, isExpanded.ToArray());
    }

    public IEnumerable<SelectGroup<TValue>> BuildTestSelectGroup(int numOfGroups, int[] numOfItems, bool[] isExpanded)
    {  
        var builder = new AccordionSelectGroupBuilder<TValue>();

        for (var i = 0; i < numOfGroups; i++)
        {
            var name = _fixture.Create<string>();
            var kvps =_fixture.CreateMany<KeyValuePair<string, TValue>>(numOfItems[i]);
            var dictionary = new Dictionary<string, TValue>(kvps);
            builder.AddGroup(name, isExpanded[i], dictionary);
        }

        return builder.Build();
    }
}
