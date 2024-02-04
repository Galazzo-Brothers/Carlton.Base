using Carlton.Core.Components;
using Carlton.Core.Components.Buttons;
using Carlton.Core.Components.Cards;
using Carlton.Core.Components.Lab.TestData;
using Carlton.Core.Components.Layouts.Extensions;
using Carlton.Core.Components.Navigation;
using Carlton.Core.Components.Dropdowns;
using Carlton.Core.Components.Table;
using Carlton.Core.Lab.Extensions;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Library.Lab;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        var http = new HttpClient()
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        };

        builder.Services.AddScoped(sp => http);

        builder.Services.AddLogging
        (
            loggingBuilder => loggingBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"))
                                            .AddSeq("http://localhost:5341/")
        );

        builder.Services.AddCarltonLayout(b =>
            b.AddSetting("AppName", "Carlton Test Lab")
             .AddSetting("LogoUrl", "_content/Carlton.Core.Components/images/CarltonLogo.png")
             .AddSetting("ShowPanel", true)); ;
        builder.Services.AddCarltonTestLab(builder =>
        {
            //Base Components
            builder.AddComponentState<Card>(CardTestStates.DefaultState)
                   .AddComponentState<ListCard<string>>(CardTestStates.DefaultListState)
                   .AddComponentState<CountCard>("Accent 1", CardTestStates.CountCard1State)
                   .AddComponentState<CountCard>("Accent 2", CardTestStates.CountCard2State)
                   .AddComponentState<CountCard>("Accent 3", CardTestStates.CountCard3State)
                   .AddComponentState<CountCard>("Accent 4", CardTestStates.CountCard4State)
                   .AddComponentState<ActionButton>("Default", ButtonTestStates.DefaultState)
                   .AddComponentState<IconButton>("Default", IconButtonTestStates.DefaultState)
                   .AddComponentState<LinkButton>("Default", ButtonTestStates.DefaultState)
                   .AddComponentState<ProfileAvatar>(ProfileAvatarTestStates.DefaultState)
                   .AddComponentState<BreadCrumbs>("Carrot MultiCrumb", BreadCrumbsTestStates.CarrotMultiCrumb)
                   .AddComponentState<BreadCrumbs>("Carrot SingleCrumb", BreadCrumbsTestStates.CarrotSingleCrumb)
                   .AddComponentState<BreadCrumbs>("Slash MultiCrumb", BreadCrumbsTestStates.SlashMultiCrumb)
                   .AddComponentState<BreadCrumbs>("Slash SingleCrumb", BreadCrumbsTestStates.SlashSingleCrumb)
                   .AddComponent<Spinner>()
                   .AddComponentState<ErrorPrompt>(ErrorPromptTestStates.DefaultState)
                   .AddComponentState<Checkbox>("Checked", CheckboxTestStates.CheckedState)
                   .AddComponentState<Checkbox>("Unchecked", CheckboxTestStates.UncheckedState)
                   .AddComponentState<Dropdown<int>>(SelectTestStates.Default)
                   .AddComponentState<KebabMenu>(SelectTestStates.Default)
                   .AddComponentState<Table<TableTestStates.TableTestObject>>("Large Item List", TableTestStates.LargeItemList)
                   .AddComponentState<Table<TableTestStates.TableTestObject>>("Small Item List", TableTestStates.SmallItemList)
                   .AddComponentState<Table<TableTestStates.TableTestObject>>("Without Pagination Row", TableTestStates.WithOutPaginationRow)
                   .Build();
        });


        builder.RootComponents.Add<App>("app");
        var app = builder.Build();
        await app.RunAsync().ConfigureAwait(true);
    }
}
