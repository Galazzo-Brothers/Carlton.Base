using AutoFixture;
using Carlton.Core.Lab.Components.BreadCrumbs;
using Carlton.Core.Lab.Components.EventConsole;
using Carlton.Core.Lab.Models.Common;
using Carlton.Core.Lab.State;
using Carlton.Core.Utilities.Random;

namespace Carlton.Core.Lab.Test.Common;

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

			var randomIndex = RandomUtilities.GetRandomIndex(types.Count);
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

			var randomIndex = RandomUtilities.GetRandomIndex(objects.Count);
			return objects[randomIndex];
		});

		//Create base LabState
		var componentStates = fixture.CreateMany<ComponentAvailableStates>();

		//Set a random but allowable selected index
		var selectedIndex = RandomUtilities.GetRandomIndex(componentStates.Count());

		//Set component events
		var componentEvents = fixture.CreateMany<ComponentRecordedEvent>();

		//Setup Markup
		var markup = fixture.Create<string>();

		return new LabState(componentStates) with
		{
			SelectedComponentIndex = selectedIndex,
			ComponentEvents = componentEvents,
			SelectedComponentMarkup = markup
		};
	}
}