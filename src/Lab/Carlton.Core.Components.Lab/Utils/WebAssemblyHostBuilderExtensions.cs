using Carlton.Core.Components.Flux.State;
using Carlton.Core.InProcessMessaging.Queries;

namespace Carlton.Core.Components.Lab;

public static class WebAssemblyHostBuilderExtensions
{
    public static void AddCarltonTestLab(this WebAssemblyHostBuilder builder,
        Action<NavMenuViewModelBuilder> navTreeAct,
        IDictionary<string, TestResultsReportModel> testResults = null,
        params Assembly[] assemblies)
    {
        /*NavMenu Builder*/
        var NavMenuBuilder = new NavMenuViewModelBuilder();
        navTreeAct(NavMenuBuilder);
        var options = NavMenuBuilder.Build();

        /*LabState*/
        var state = new LabState(options, testResults);
        builder.Services.AddSingleton(state);
        builder.Services.AddSingleton<FluxState<LabState>>();
        builder.Services.AddSingleton<IFluxStateObserver<LabState>>(_ => _.GetService<FluxState<LabState>>());
        builder.Services.AddSingleton<IMutableFluxState<LabState>>(_ => _.GetService<FluxState<LabState>>());
        builder.Services.AddSingleton<MutationResolver<LabState>>();

        /*Mapster Extensions*/
        MapsterConfig.RegisterMapsterConfiguration();

        /*Flux Registers*/
        builder.Services.AddCarltonFlux<LabState>(assemblies);
    }
}



//typeof(TestBedLayout).Assembly.DefinedTypes.Where(_ => _.IsAssignableTo(typeof(IConnectedComponent<>))).ToList().ForEach(comp =>
//{
//    var interfaceType = typeof(IConnectedComponent<>).MakeGenericType(typeof(IConnectedComponent<>).MakeGenericType(vm), vm);
//    var implementationType = typeof(ViewModelQueryHandler<,>).MakeGenericType(typeof(LabState), vm);
//    builder.Services.AddTransient(interfaceType, implementationType);
//});

//builder.Services.Scan(_ =>
//{
//    _.FromAssembliesOf(typeof(TestBedLayout))
//        .AddClasses(classes => classes.AssignableTo(typeof(IConnectedComponent<>)))
//        .AsImplementedInterfaces()
//        .WithTransientLifetime();
//});

//_.FromAssemblyOf<BreadCrumbsViewModel>
//           SelectMany(_ => _.DefinedTypes).Where(_ => _.Name.Contains("ViewModel"))
//            .ToList()
//            .ForEach(vm =>
//                {

//    var serviceType = typeof(IQueryHandler<,>).MakeGenericType(typeof(IQuery<>).MakeGenericType(vm), vm);
//    var implementationType = typeof(ViewModelQueryHandler<,>).MakeGenericType(typeof(TState), vm);
//    services.AddSingleton(serviceType, _ => ActivatorUtilities.CreateInstance(_, implementationType));
//});