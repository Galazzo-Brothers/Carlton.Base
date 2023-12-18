using BlazorDB;
using Blazored.LocalStorage;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Decorators.Mutations;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.ExceptionHandling;
using Carlton.Core.Components.Flux.Handlers;
using Carlton.Core.Components.Flux.Services;
using Carlton.Core.Components.Flux.State;
using Carlton.Core.Utilities.JsonConverters;
using Carlton.Core.Utilities.Logging;
using MapsterMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

namespace Carlton.Core.Components.Flux;

public static class ContainerExtensions
{
    public static void AddCarltonFlux<TState>(
        this WebAssemblyHostBuilder builder,
        TState state,
        TypeAdapterConfig typeAdapterConfig,
        bool usesLocalStorage)
        where TState : class
    {
        /*Register Logging*/
        RegisterLogging(builder);
        
        /*Local Storage*/
        RegisterLocalStorage(builder);

        /*Mapster*/
        RegisterMapster(builder, typeAdapterConfig);

        /*Flux State*/
        RegisterFluxState(builder, state, usesLocalStorage);

        /*Connected Components*/
        RegisterFluxConnectedComponents(builder);

        /*Dispatchers*/
        RegisterFluxDispatchers<TState>(builder, usesLocalStorage);

        /*Handlers*/
        RegisterFluxHandlers<TState>(builder);

        /*State Mutations*/
        RegisterFluxStateMutations<TState>(builder);

        /*Validators*/
        RegisterValidators(builder);

        /*Exception Handling*/
        RegisterExceptionHandling(builder);
    }

    private static void RegisterLogging(WebAssemblyHostBuilder builder)
    {
        var logger = new InMemoryLogger();
        builder.Services.AddSingleton(logger);
        builder.Services.AddLogging(b =>
        {
            //builder.ClearProviders();
            b.AddConfiguration(builder.Configuration.GetSection("Logging"));
            b.AddProvider(new InMemoryLoggerProvider(logger));
        });

        builder.Services.AddSingleton<ILogger, InMemoryLogger>();
    }

    private static void RegisterLocalStorage(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddBlazoredLocalStorageAsSingleton(
            config =>
            {
                config.JsonSerializerOptions.Converters.Add(new JsonTypeConverter());
                config.JsonSerializerOptions.Converters.Add(new JsonEventIdConverter());
            });

        builder.Services.AddBlazorDB(options =>
        {
            options.Name = "CarltonFlux";
            options.Version = 1;
            options.StoreSchemas = new List<StoreSchema>()
            {
                new()
                {
                    Name = "Logs",
                    PrimaryKey = "key",
                    Indexes = new List<string> {"indexDate"}
                }
            };
        });

        builder.Services.AddSingleton<IBrowserStorageService, BrowserStorageService>();
    }

    private static void RegisterMapster(WebAssemblyHostBuilder builder, TypeAdapterConfig config)
    {
        builder.Services.AddSingleton(config);
        builder.Services.AddSingleton<IMapper, ServiceMapper>();
    }

    private static void RegisterFluxState<TState>(WebAssemblyHostBuilder builder, TState state, bool usesLocalStorage)
        where TState : class
    {
        builder.Services.AddSingleton(provider => CheckLocalStorageState(state, provider, usesLocalStorage));
        builder.Services.AddSingleton<IMutableFluxState<TState>, FluxState<TState>>();
        builder.Services.AddSingleton<IFluxState<TState>>(_ => _.GetService<IMutableFluxState<TState>>());
        builder.Services.AddSingleton<IFluxStateObserver<TState>>(_ => _.GetService<IFluxState<TState>>());
        builder.Services.AddSingleton<MutationResolver<TState>>();
    }

    private static void RegisterFluxDispatchers<TState>(WebAssemblyHostBuilder builder, bool usesLocalStorage)
    {
        /*ViewModel Dispatchers*/
        builder.Services.AddSingleton<IViewModelQueryDispatcher<TState>, ViewModelQueryDispatcher<TState>>();
        //services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelHttpDecorator<TState>>();
        builder.Services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelJsDecorator<TState>>();
        builder.Services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelValidationDecorator<TState>>();
        builder.Services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelExceptionDecorator<TState>>();

        /*Mutation Dispatchers*/
        builder.Services.AddSingleton<IMutationCommandDispatcher<TState>, MutationCommandDispatcher<TState>>();
        builder.Services.Decorate<IMutationCommandDispatcher<TState>, MutationValidationDecorator<TState>>();
        // services.Decorate<IMutationCommandDispatcher<TState>, MutationHttpDecorator<TState>>();
        if (usesLocalStorage)
            builder.Services.Decorate<IMutationCommandDispatcher<TState>, MutationLocalStorageDecorator<TState>>();
        builder.Services.Decorate<IMutationCommandDispatcher<TState>, MutationExceptionDecorator<TState>>();
    }

    private static void RegisterFluxConnectedComponents(WebAssemblyHostBuilder builder)
    {
        builder.Services.Scan(scan => scan
                    .FromApplicationDependencies()
                    .AddClasses(classes => classes.AssignableTo(typeof(IConnectedComponent<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
    }

    private static void RegisterFluxHandlers<TState>(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddSingleton<IMutationCommandHandler<TState>, MutationCommandHandler<TState>>();
        builder.Services.AddSingleton<IViewModelQueryHandler<TState>, ViewModelQueryHandler<TState>>();
    }

    private static void RegisterFluxStateMutations<TState>(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddSingleton<IMutationResolver<TState>, MutationResolver<TState>>();

        builder.Services.Scan(_ => _
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IFluxStateMutation<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }

    private static void RegisterValidators(WebAssemblyHostBuilder builder)
    {
        builder.Services.Scan(_ =>
            _.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }

    private static void RegisterExceptionHandling(WebAssemblyHostBuilder builder)
    {
        builder.Services.AddSingleton<IExceptionDisplayService, FluxExceptionDisplayService>();
        builder.Services.AddSingleton<IComponentExceptionLoggingService, ComponentExceptionLoggingService>();
    }

    private static TState CheckLocalStorageState<TState>(TState state, IServiceProvider provider, bool usesLocalStorage)
        where TState : class
    {
        if (usesLocalStorage)
            return state;
        else
            return provider.GetService<ISyncLocalStorageService>().GetItem<TState>("carltonFluxState") ?? state;
    }
}


