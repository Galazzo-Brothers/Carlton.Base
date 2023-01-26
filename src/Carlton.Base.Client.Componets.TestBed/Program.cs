using System.Linq;

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
            builder.AddComponent<Card>($"Cards", _ =>
                        _.WithTestState("Card", CardTestStates.DefaultState()))
                  .AddComponent<ListCard<string>>("ListCard", _ =>
                        _.WithTestState("Default", CardTestStates.DefaultListState()))
                  .AddComponent<CountCard>($"Count Cards", _ =>
                        _.WithTestState("CountCardAccent1", CardTestStates.CountCard1State())
                         .WithTestState("CountCardAccent2", CardTestStates.CountCard2State())
                         .WithTestState("CountCardAccent3", CardTestStates.CountCard3State())
                         .WithTestState("CountCardAccent4", CardTestStates.CountCard4State()))
                  .AddComponent<Logo>($"Logo", _ =>
                        _.WithTestState("Default", LogoTestStates.DefaultState()))
                  .AddComponent<ProfileAvatar>("ProfileAvatar", _ =>
                        _.WithTestState("Default", ProfileAvatarTestStates.DefaultState()))
                  .AddComponent<Checkbox>($"checkbox", _ =>
                        _.WithTestState("Checked", CheckboxTestStates.CheckedState())
                         .WithTestState("Unchecked", CheckboxTestStates.UncheckedState()))
                  .AddComponent<Select>("Select", _ =>
                        _.WithTestState("Default", SelectTestStates.Default()))
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
