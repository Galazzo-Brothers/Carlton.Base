namespace Carlton.Base.Components.TestBed;

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

        var testResultsContent = await http.GetStringAsync("/UnitTestResults/ComponentTestResults.xml");
        var testResults = TestBedUtils.ParseXUnitTestResults(testResultsContent);

        //var testResultsContent = await http.GetStringAsync("/UnitTestResults/ComponentTestResults.trx");
        //var testResults = TestBedUtils.ParseTrxTestResults(testResultsContent);


        //var x = builder.Logging;
        builder.AddCarltonTestBed(builder =>
        {
            //Base Components
            builder.AddParameterObjComponent<Card>(CardTestStates.DefaultState)
                   .AddParameterObjComponent<ListCard<string>>(CardTestStates.DefaultListState)
                   .AddParameterObjComponent<CountCard>("Accent 1", CardTestStates.CountCard1State)
                   .AddParameterObjComponent<CountCard>("Accent 2", CardTestStates.CountCard2State)
                   .AddParameterObjComponent<CountCard>("Accent 3", CardTestStates.CountCard3State)
                   .AddParameterObjComponent<CountCard>("Accent 4", CardTestStates.CountCard4State)
                   .AddParameterObjComponent<FloatingActionButton>("Default", ButtonTestStates.DefaultState)
                   .AddParameterObjComponent<Logo>(LogoTestStates.DefaultState)
                   .AddParameterObjComponent<ProfileAvatar>(ProfileAvatarTestStates.DefaultState)
                   .AddParameterObjComponent<BreadCrumbs>("Carrot MultiCrumb", BreadCrumbsTestStates.CarrotMultiCrumb)
                   .AddParameterObjComponent<BreadCrumbs>("Carrot SingleCrumb", BreadCrumbsTestStates.CarrotSingleCrumb)
                   .AddParameterObjComponent<BreadCrumbs>("Slash MultiCrumb", BreadCrumbsTestStates.SlashMultiCrumb)
                   .AddParameterObjComponent<BreadCrumbs>("Slash SingleCrumb", BreadCrumbsTestStates.SlashSingleCrumb)
                   .AddParameterObjComponent<Spinner>("Default", null)
                   .AddParameterObjComponent<ErrorPrompt>("Default", null)
                   .AddParameterObjComponent<Checkbox>("Checked", CheckboxTestStates.CheckedState)
                   .AddParameterObjComponent<Checkbox>("Unchecked", CheckboxTestStates.UncheckedState)
                   .AddParameterObjComponent<Select>(SelectTestStates.Default)
                   .AddParameterObjComponent<Table<TableTestStates.TableTestObject>>("Large Item List", TableTestStates.LargeItemList)
                   .AddParameterObjComponent<Table<TableTestStates.TableTestObject>>("Small Item List", TableTestStates.SmallItemList)
                   .AddParameterObjComponent<Table<TableTestStates.TableTestObject>>("Without Pagination Row", TableTestStates.WithOutPaginationRow)
                   .AddParameterObjComponent<NotificationBar>("FadeOut Disabled", NotificationStates.NotificationBarFadeOutDisabledStated)
                   .AddParameterObjComponent<NotificationBar>("FadeOut Enabled", NotificationStates.NotificationBarFadeOutEnabledStated)
                   .AddParameterObjComponent<SuccessNotification>("FadeOut Disabled", NotificationStates.SuccessFadeOutDisabledState)
                   .AddParameterObjComponent<SuccessNotification>("FadeOut Enabled", NotificationStates.SuccessFadeOutEnabledState)
                   .AddParameterObjComponent<InfoNotification>(NotificationStates.InfoState)
                   .AddParameterObjComponent<WarningNotification>(NotificationStates.WarningState)
                   .AddParameterObjComponent<ErrorNotification>(NotificationStates.ErrorState)
                   .Build();
        },
        testResults,
        typeof(TestBedLayout).Assembly);


        builder.RootComponents.Add<App>("app");

        await builder.Build().RunAsync().ConfigureAwait(true);
    }
}
