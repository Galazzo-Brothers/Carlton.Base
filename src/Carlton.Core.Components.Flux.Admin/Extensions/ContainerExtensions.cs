
using Blazored.LocalStorage;
using Carlton.Core.Components.Flux.Admin.State;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Decorators.Mutations;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Handlers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.State;
using Carlton.Core.Components.Lab;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Core.Components.Flux.Admin;

public static class ContainerExtensions
{
    public static void AddCarltonFluxDebug<TState>(this WebAssemblyHostBuilder builder, TState state)
    {
        var mapster = FluxDebugMapsterConfig.BuildMapsterConfig();
        var adminState = new FluxDebugState<TState>(state, null);

        //RegisterMapster(builder, mapster);
        RegisterFluxState(builder, adminState, false);
        RegisterFluxDispatchers<TState>(builder, false);
        RegisterFluxConnectedComponents<TState>(builder);
        RegisterFluxHandlers<TState>(builder);
        RegisterFluxStateMutations<TState>(builder);

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
        //  builder.Services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelValidationDecorator<TState>>();
        builder.Services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelExceptionDecorator<TState>>();

        /*Mutation Dispatchers*/
        builder.Services.AddSingleton<IMutationCommandDispatcher<TState>, MutationCommandDispatcher<TState>>();
        builder.Services.Decorate<IMutationCommandDispatcher<TState>, MutationValidationDecorator<TState>>();
        // services.Decorate<IMutationCommandDispatcher<TState>, MutationHttpDecorator<TState>>();
        if (usesLocalStorage)
            builder.Services.Decorate<IMutationCommandDispatcher<TState>, MutationLocalStorageDecorator<TState>>();
        builder.Services.Decorate<IMutationCommandDispatcher<TState>, MutationExceptionDecorator<TState>>();
    }

    private static void RegisterFluxConnectedComponents<TState>(WebAssemblyHostBuilder builder)
    {
        builder.Services.Scan(scan => scan
                    .FromApplicationDependencies()
                    .AddClasses(classes => classes.AssignableTo(typeof(IConnectedComponent<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
    }

    private static void RegisterFluxHandlers<TState>(WebAssemblyHostBuilder builder)
    {
        //bool interfacePredicate(Type _) => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(IConnectedComponent<>);
        //bool baseConnComPredicate(Type _) => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(BaseConnectedComponent<>);
        //bool isCommandPredicate(Type _) => _.IsAssignableTo(typeof(MutationCommand));
        //bool isConnComPredicate(Type _) => _.GetInterfaces().Any(interfacePredicate) && !baseConnComPredicate(_);

        //AppDomain.CurrentDomain.GetAssemblies()
        //                .SelectMany(_ => _.DefinedTypes)
        //                .Where(_ => _.IsClass && !_.IsAbstract)
        //                .Where(_ => isCommandPredicate(_) || isConnComPredicate(_))
        //                .ToList().ForEach(_ =>
        //                {
        //                    var isMutationCommand = isCommandPredicate(_);
        //                    var isConnectedComponent = isConnComPredicate(_);

        //                    if (isConnectedComponent)
        //                    {
        //                        //Register ViewModel Queries
        //                        var vmType = _.GetInterfaces().First(interfacePredicate).GetGenericArguments()[0];
        //                        var interfaceType = typeof(IViewModelQueryHandler<,>).MakeGenericType(typeof(TState), vmType);
        //                        var implementationType = typeof(ViewModelQueryHandler<,>).MakeGenericType(typeof(TState), vmType);
        //                        builder.Services.AddTransient(interfaceType, implementationType);
        //                    }
        //                    else if (isMutationCommand)
        //                    {
        //                        //Register Mutation Commands
        //                        var interfaceType = typeof(IMutationCommandHandler<>).MakeGenericType(typeof(TState));
        //                        var implementationType = typeof(MutationCommandHandler<>).MakeGenericType(typeof(TState));
        //                        builder.Services.AddTransient(interfaceType, implementationType);
        //                    }
        //                });
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

    private static TState CheckLocalStorageState<TState>(TState state, IServiceProvider provider, bool usesLocalStorage)
      where TState : class
    {
        return state;
    }
}


