namespace Carlton.Base.Components.TestBed;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var sourceBasePath = builder.Configuration.GetSection("sourceSamplesSettings").GetValue<string>("sourceBasePath");

        builder.AddCarltonTestBed(builder =>
        {
            //Base Components
            builder.AddComponent<Card>(CardTestStates.DefaultState())
                   .AddComponent<ListCard<string>>(CardTestStates.DefaultListState())
                   .AddComponent<CountCard>("CountCardAccent1", CardTestStates.CountCard1State())
                   .AddComponent<CountCard>("CountCardAccent2", CardTestStates.CountCard2State())
                   .AddComponent<CountCard>("CountCardAccent3", CardTestStates.CountCard3State())
                   .AddComponent<CountCard>("CountCardAccent4", CardTestStates.CountCard4State())
                   .AddComponent<Logo>(LogoTestStates.DefaultState())
                   .AddComponent<ProfileAvatar>(ProfileAvatarTestStates.DefaultState())
                   .AddComponent<PageTitle>(PageTitleTestStates.DefaultState())
                   .AddComponent<Checkbox>("Checked", CheckboxTestStates.CheckedState())
                   .AddComponent<Checkbox>("Unchecked", CheckboxTestStates.UncheckedState())
                   .AddComponent<Select>(SelectTestStates.Default())
                   .AddComponent<SuccessNotification>(NotificationStates.SuccessState())
                   .AddComponent<InfoNotification>(NotificationStates.InfoState())
                   .AddComponent<WarningNotification>(NotificationStates.WarningState())
                   .AddComponent<FailureNotification>(NotificationStates.FailureState())
                   .Build();
        },
        sourceBasePath,
        typeof(TestBedFramework.TestBed).Assembly);


        builder.Services.AddScoped(sp =>
            new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });


        builder.RootComponents.Add<App>("app");

        await builder.Build().RunAsync().ConfigureAwait(true);
    }
}
