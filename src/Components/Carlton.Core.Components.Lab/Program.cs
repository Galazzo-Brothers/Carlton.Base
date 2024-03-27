using Carlton.Core.Components.Buttons;
using Carlton.Core.Components.Cards;
using Carlton.Core.Components.Checkboxes;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Components.Error;
using Carlton.Core.Components.Logos;
using Carlton.Core.Components.Navigation;
using Carlton.Core.Components.Pills;
using Carlton.Core.Components.Spinners;
using Carlton.Core.Components.Tables;
using Carlton.Core.Components.Toasts;
using Carlton.Core.Lab.Extensions;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Components.Lab;

/// <summary>
/// Provides the entry point for the Carlton.Core.Components.Lab Blazor WebAssembly application.
/// </summary>
public static class Program
{
	/// <summary>
	/// The entry point for the application.
	/// </summary>
	/// <param name="args">Command-line arguments passed to the application.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		// Configure logging
		builder.Services.AddLogging
		(
			loggingBuilder => loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"))
											.AddSeq("http://localhost:5341/")
		);

		// Configure CarltonTestLab
		builder.Services.AddCarltonTestLab(builder =>
		{
			// Configure base components and their states
			builder.AddComponentState<Card>(CardTestStates.DefaultState)
				   .AddComponentState<ListCard<string>>(CardTestStates.DefaultListState)
				   .AddComponentState<CountCard>("Accent 1", CardTestStates.CountCard1State)
				   .AddComponentState<CountCard>("Accent 2", CardTestStates.CountCard2State)
				   .AddComponentState<CountCard>("Accent 3", CardTestStates.CountCard3State)
				   .AddComponentState<CountCard>("Accent 4", CardTestStates.CountCard4State)
				   .AddComponentState<ActionButton>("Default", ButtonTestStates.ButtonState)
				   .AddComponentState<IconButton>("Default", ButtonTestStates.IconButtonState)
				   .AddComponentState<LinkButton>("Default", ButtonTestStates.ButtonState)
				   .AddComponentState<ProfileAvatarDropdown>(ProfileAvatarTestStates.DefaultState)
				   .AddComponentState<BreadCrumbs>("Carrot MultiCrumb", BreadCrumbsTestStates.CarrotMultiCrumb)
				   .AddComponentState<BreadCrumbs>("Carrot SingleCrumb", BreadCrumbsTestStates.CarrotSingleCrumb)
				   .AddComponentState<BreadCrumbs>("Slash MultiCrumb", BreadCrumbsTestStates.SlashMultiCrumb)
				   .AddComponentState<BreadCrumbs>("Slash SingleCrumb", BreadCrumbsTestStates.SlashSingleCrumb)
				   .AddComponent<Spinner>()
				   .AddComponentState<Logo>("Expanded", LogoTestStates.Expanded)
				   .AddComponentState<Logo>("Collapsed", LogoTestStates.Collapsed)
				   .AddComponentState<ErrorPrompt>(ErrorPromptTestStates.DefaultState)
				   .AddComponentState<Checkbox>("Checked", CheckboxTestStates.CheckedState)
				   .AddComponentState<Checkbox>("Unchecked", CheckboxTestStates.UncheckedState)
				   .AddComponentState<Dropdown<int>>("Default", DropdownTestStates.Default)
				   .AddComponentState<Dropdown<int>>("Disabled", DropdownTestStates.Disabled)
				   .AddComponentState<Dropdown<int>>("Pristine", DropdownTestStates.Pristine)
				   .AddComponentState<KebabMenu<int>>(KebabMenuTestStates.Default)
				   .AddComponentState<Pill>(PillTestStates.Default)
				   .AddComponentState<Table<TableTestStates.TableTestObject>>("Large Item List", TableTestStates.LargeItemList)
				   .AddComponentState<Table<TableTestStates.TableTestObject>>("Small Item List", TableTestStates.SmallItemList)
				   .AddComponentState<Table<TableTestStates.TableTestObject>>("Without Pagination Row", TableTestStates.WithOutPaginationRow)
				   .AddComponentState<Toast>("Success", ToastTestStates.Success)
				   .AddComponentState<Toast>("Info", ToastTestStates.Info)
				   .AddComponentState<Toast>("Warning", ToastTestStates.Warning)
				   .AddComponentState<Toast>("Error", ToastTestStates.Error);
		});


		// Add the root component
		builder.RootComponents.Add<App>("app");

		// Build the application
		var app = builder.Build();

		// Run the application asynchronously
		await app.RunAsync().ConfigureAwait(true);
	}
}
