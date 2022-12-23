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
            builder.AddComponent<Card>($"Cards/Card", CardTestStates.DefaultState());
            builder.AddComponent<ListCard<string>>($"Cards/ListCard", CardTestStates.DefaultListState());
            builder.AddComponent<CountCard>($"Cards/CountCardAccent1", CardTestStates.CountCard1State());
            builder.AddComponent<CountCard>($"Cards/CountCardAccent2", CardTestStates.CountCard2State());
            builder.AddComponent<CountCard>($"Cards/CountCardAccent3", CardTestStates.CountCard3State());
            builder.AddComponent<CountCard>($"Cards/CountCardAccent4", CardTestStates.CountCard4State());
            builder.AddComponent<Logo>($"Logo");
            builder.AddComponent<HamburgerCollapser>($"HamburgerCollapser");
            builder.AddComponent<UserProfile>($"UserProfile");
            builder.AddComponent<Checkbox>($"checkbox/Checked", CheckboxTestStates.CheckedState());
            builder.AddComponent<Checkbox>($"checkbox/Unchecked", CheckboxTestStates.UncheckedState());
            builder.AddComponent<Select>($"Select/Default", SelectTestStates.Default());
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
