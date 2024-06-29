using Carlton.Core.Lab.Models.Common;
namespace Carlton.Core.Lab.Test;

public class NavMenuViewModelBuilderTests
{
	[Fact]
	public void NavMenuViewModelBuilder_AddComponentGeneric_ShouldAddDefaultConfiguration()
	{
		//Arrange
		var expectedResult = new List<ComponentConfigurations>
		{
			new() {
				ComponentType = typeof(DummyComponent),
				ComponentStates = new List<ComponentState>
				{
					new() {
						DisplayName = "Default",
						ComponentParameters = new object(),
					}
				}
			}
		};
		var sut = new NavMenuViewModelBuilder();

		//Act
		var result = sut.AddComponent<DummyComponent>()
						.Build();

		//Assert
		result.ShouldHaveSingleItem();
		result.First().ComponentType.ShouldBe(expectedResult.First().ComponentType);
		result.First().ComponentStates.ShouldHaveSingleItem();
		Assert.Collection(result.First().ComponentStates, item =>
		{
			item.DisplayName.ShouldBe(expectedResult.First().ComponentStates.First().DisplayName);
			item.ComponentParameters.ShouldBeOfType<object>();
		});
	}

	[Theory, AutoData]
	public void NavMenuViewModelBuilder_AddComponentStateWithParameters_ShouldAddDefaultConfigurationWithParameters(
		object componentParameters)
	{
		//Arrange
		var expectedResult = new List<ComponentConfigurations>
		{
			new() {
				ComponentType = typeof(DummyComponent),
				ComponentStates = new List<ComponentState>
				{
					new() {
						DisplayName = "Default",
						ComponentParameters = new object(),
					}
				}
			}
		};
		var sut = new NavMenuViewModelBuilder();

		//Act
		var result = sut.AddComponentState<DummyComponent>(componentParameters)
						.Build();

		//Assert
		result.ShouldHaveSingleItem();
		result.First().ComponentType.ShouldBe(expectedResult.First().ComponentType);
		result.First().ComponentStates.ShouldHaveSingleItem();
		Assert.Collection(result.First().ComponentStates, item =>
		{
			item.DisplayName.ShouldBe(expectedResult.First().ComponentStates.First().DisplayName);
			item.ComponentParameters.ShouldBe(componentParameters);
		});
	}

	[Theory, AutoData]
	public void NavMenuViewModelBuilder_AdComponentWithNameAndParameters_ShouldAddConfigurationWithDisplayNameAndParameters(
		string displayName,
		object componentParameters)
	{
		//Arrange
		var expectedResult = new List<ComponentConfigurations>
		{
			new() {
				ComponentType = typeof(DummyComponent),
				ComponentStates = new List<ComponentState>
				{
					new() {
						DisplayName = displayName,
						ComponentParameters = new object(),
					}
				}
			}
		};
		var sut = new NavMenuViewModelBuilder();

		//Act
		var result = sut.AddComponentState<DummyComponent>(displayName, componentParameters)
						.Build();

		//Assert
		result.ShouldHaveSingleItem();
		result.First().ComponentType.ShouldBe(expectedResult.First().ComponentType);
		result.First().ComponentStates.ShouldHaveSingleItem();
		Assert.Collection(result.First().ComponentStates, item =>
		{
			item.DisplayName.ShouldBe(expectedResult.First().ComponentStates.First().DisplayName);
			item.ComponentParameters.ShouldBe(componentParameters);
		});
	}

	[Fact]
	public void NavMenuViewModelBuilder_AddMultipleComponentStates_ShouldAddMultipleComponentStates()
	{
		const string DisplayName_1 = "Display Name 1";
		const string DisplayName_2 = "Display Name 2";
		const string DisplayName_3 = "Display Name 3";

		var Parameters_1 = new object();
		var Parameters_2 = new object();
		var Parameters_3 = new object();

		//Arrange
		var expectedResult = new List<ComponentConfigurations>
		{
			new()
			{
				ComponentType = typeof(TestArgs),
				ComponentStates = new List<ComponentState>
				{
					new()
					{
						DisplayName = DisplayName_1,
						ComponentParameters = Parameters_1,
					},
					new()
					{
						DisplayName = DisplayName_2,
						ComponentParameters = Parameters_2,
					},
					new()
					{
						DisplayName = DisplayName_3,
						ComponentParameters = Parameters_3,
					}
				}
			},
			new()
			{
				ComponentType = typeof(DummyComponent),
				ComponentStates = new List<ComponentState>
				{
					new()
					{
						DisplayName = DisplayName_1,
						ComponentParameters = Parameters_1,
					},
					new()
					{
						DisplayName = DisplayName_2,
						ComponentParameters = Parameters_2,
					},
					new()
					{
						DisplayName = DisplayName_3,
						ComponentParameters = Parameters_3,
					}

				}
			}
		};

		var sut = new NavMenuViewModelBuilder();

		//Act
		var result = sut
			.AddComponentState<DummyComponent>(DisplayName_1, Parameters_1)
			.AddComponentState<TestArgs>(DisplayName_1, Parameters_1)
			.AddComponentState<DummyComponent>(DisplayName_2, Parameters_2)
			.AddComponentState<TestArgs>(DisplayName_2, Parameters_2)
			.AddComponentState<DummyComponent>(DisplayName_3, Parameters_3)
			.AddComponentState<TestArgs>(DisplayName_3, Parameters_3)
			.Build();

		//Assert


		Assert.Collection(result, item =>
		{
			item.ComponentType.ShouldBe(typeof(DummyComponent));
		},
		item =>
		{
			item.ComponentType.ShouldBe(typeof(TestArgs));
		});

		Assert.Collection(result.First().ComponentStates, item =>
		{
			item.DisplayName.ShouldBe(DisplayName_1);
			item.ComponentParameters.ShouldBe(Parameters_1);
		},
		item =>
		{
			item.DisplayName.ShouldBe(DisplayName_2);
			item.ComponentParameters.ShouldBe(Parameters_2);
		},
		item =>
		{
			item.DisplayName.ShouldBe(DisplayName_3);
			item.ComponentParameters.ShouldBe(Parameters_3);
		});
	}
}
