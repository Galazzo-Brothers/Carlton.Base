using AutoFixture;
using Carlton.Core.Components.Lab.Test.Mocks;
using Carlton.Core.Utilities.Extensions;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Lab.Test.Common;

internal class LabStateFactory
{
    public static LabState BuildLabState()
    {
        //Setup Fixture
        var fixture = new Fixture();

        //Register Types
        fixture.Register(() =>
        {
            var types = new List<Type>
            {
               typeof(DummyComponent),
               typeof(ConnectedBreadCrumbs),
               typeof(ConnectedEventConsole)
            };

            var random = new Random();
            var randomIndex = random.Next(types.Count);
            return types[randomIndex];
        });

        //Register Objects
        fixture.Register(() =>
        {
            var objects = new List<object>
            {
                new { Param1 = "Testing", Param2 = 7, Param3 = false},
                new { Param1 = "Hello World", Param2 = 2.2 },
                new { Param1 = new List<int> { 1, 2, 3, 77 } }
            };

            var random = new Random();
            var randomIndex = random.Next(objects.Count);
            return objects[randomIndex];
        });

        //Create base LabState
        var componentStates = fixture.CreateMany<ComponentState>();

        //Register Test Dictionary
        fixture.Register(() =>
        {
            var random = new Random();
            var kvp = new List<KeyValuePair<string, TestResultsReport>>();

            foreach(var type in componentStates.Select(_ => _.Type).Distinct())
            {
                kvp.Add(new KeyValuePair<string, TestResultsReport>(
                  type.GetDisplayName(),
                  fixture.Create<TestResultsReport>()));
            }

            return new Dictionary<string, TestResultsReport>(kvp);
        });
      
      
        var dictionary = fixture.Create<Dictionary<string, TestResultsReport>>();
        
        //Set a random but allowable selected index
        var random = new Random();
        var selectedIndex = random.Next(0, componentStates.Count() - 1);

        //Set component events
        var componentEvents = fixture.CreateMany<ComponentRecordedEvent>();

        //Setup Markup
        var markup = fixture.Create<string>();

        return new LabState(componentStates, dictionary) with 
        {
            SelectedComponentIndex = selectedIndex,
            ComponentEvents = componentEvents,
            SelectedComponentMarkup = markup
        };
    }
}