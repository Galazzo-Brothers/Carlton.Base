using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Models;
using FluentValidation;
using MapsterMapper;
using Microsoft.JSInterop;
using MockHttp;
using MockHttp.Json.SystemTextJson;
using Moq;


namespace Carlton.Core.Components.Flux.Test.Common;

public static class Extensions
{
    public static void VerifyDispatch<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, ViewModelQuery query)
    {
        dispatcher.Verify(mock => mock.Dispatch<TViewModel>(It.IsAny<object>(), query, It.IsAny<CancellationToken>()), Times.Once);
    }

    public static void VerifyMapperCalled<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, ViewModelQuery query)
    {
        dispatcher.Verify(mock => mock.Dispatch<TViewModel>(It.IsAny<object>(), query, It.IsAny<CancellationToken>()), Times.Once);
    }

    public static void VerifyDispatchCalled<TCommand>(this Mock<IMutationCommandDispatcher<TestState>> dispatcher, TCommand command)
        where TCommand : MutationCommand
    {
        dispatcher.Verify(mock => mock.Dispatch(It.IsAny<object>(), command, It.IsAny<CancellationToken>()), Times.Once);
    }

    public static void VerifyMapper(this Mock<IMapper> mapper)
    {
        mapper.Verify(_ => _.Map(It.IsAny<TestState>(), It.IsAny<TestState>()), Times.Exactly(2));
    }

    public static void SetUpMutation<T>(this Mock<IFluxStateMutation<TestState, T>> mutation, string stateEvent, bool isRefreshMutation)
    {
        mutation.Setup(_ => _.StateEvent).Returns(stateEvent);
        mutation.Setup(_ => _.IsRefreshMutation).Returns(isRefreshMutation);
    }

    public static void SetupServiceProvider<T>(this Mock<IServiceProvider> serviceProvider, object implementation)
    {
        serviceProvider.Setup(_ => _.GetService(typeof(T))).Returns(implementation);
    }

    public static void SetupHandler<T>(this Mock<IMutationCommandHandler<TestState, T>> handler)
        where T : MutationCommand
    {
        handler.Setup(_ => _.Handle(It.IsAny<T>(), CancellationToken.None)).Returns(Task.FromResult(Unit.Value));
    }

    public static void SetupHandler<T>(this Mock<IViewModelQueryHandler<TestState, T>> handler, T response)
    {
        handler.Setup(_ => _.Handle(It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(response));
    }

    public static void VerifyHandler<T>(this Mock<IViewModelQueryHandler<TestState, T>> handler)
    {
        handler.Verify(_ => _.Handle(It.IsAny<ViewModelQuery>(), CancellationToken.None), Times.Once);
    }

    public static void VerifyHandler<T>(this Mock<IMutationCommandHandler<TestState, T>> handler)
      where T : MutationCommand
    {
        handler.Verify(_ => _.Handle(It.IsAny<T>(), CancellationToken.None), Times.Once);
    }

    public static void SetupDispatcher<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, TViewModel response)
    {
        dispatcher.Setup(_ => _.Dispatch<TViewModel>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>()))
                               .Returns(Task.FromResult(response));
    }

    public static void VerifyDispatcher<TViewModel>(this Mock<IViewModelQueryDispatcher<TestState>> dispatcher, int times)
    {
        dispatcher.Verify(_ => _.Dispatch<TViewModel>(It.IsAny<object>(), It.IsAny<ViewModelQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(times));
    }

    public static void VerifyDispatcher<TCommand>(this Mock<IMutationCommandDispatcher<TestState>> dispatcher)
        where TCommand : MutationCommand
    {
        dispatcher.Verify(_ => _.Dispatch(It.IsAny<object>(), It.IsAny<TCommand>(), It.IsAny<CancellationToken>()), Times.Once());
    }

    public static void VerifyValidator<T>(this Mock<IValidator<T>> validator)
    {
        validator.Verify(_ => _.Validate(It.IsAny<ValidationContext<T>>()), Times.Once);
    }

    public static void SetupMockHttpHandler<TResponse>(this MockHttpHandler handler, string httpVerb, string url, int statusCode, TResponse response)
    {
        handler
           .When(matching => matching
               .Method(httpVerb)
               .RequestUri(url)
           )
           .Respond(with => with
               .StatusCode(statusCode)
               .JsonBody(response)
           );
    }

    public static void SetupMockHttpHandler<TRequest, TResponse>(this MockHttpHandler handler, string httpVerb, string url, int statusCode, TRequest request, TResponse response)
    {
        handler
           .When(matching => matching
               .Method(httpVerb)
               .RequestUri(url)
               .JsonBody(request, new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web))
           )
           .Respond(with => with
               .StatusCode(statusCode)
               .JsonBody(response)
           );
    }

    public static void VerifyMockHttpHandler(this MockHttpHandler handler, string httpVerb, string url)
    {
        handler.Verify(matching => matching
                                        .Method(httpVerb)
                                        .RequestUri(url), IsSent.Once);
               

         
    }

    public static void SetupJsRuntime(this Mock<IJSRuntime> jsRuntime, string moduleName, IJSObjectReference result)
    {
        jsRuntime.Setup(_ => _.InvokeAsync<IJSObjectReference>("import", new object[] { moduleName }))
                 .Returns(ValueTask.FromResult(result));
    }

    public static void SetupIJsRuntime(this Mock<IJSRuntime> jsRuntime, string moduleName, IJSObjectReference result)
    {
        jsRuntime.Setup(_ => _.InvokeAsync<IJSObjectReference>("import", new object[] { moduleName }))
                 .Returns(ValueTask.FromResult(result));
    }

    public static void VerifyJsRuntime(this Mock<IJSRuntime> jsRuntime, int times)
    {
        jsRuntime.Verify(_ => _.InvokeAsync<IJSObjectReference>("import", new object[] { "test_module" }), Times.Exactly(times));
    }

    public static void VerifyJsObjectReference<TResponse>(this Mock<IJSObjectReference> jsObj, string methodName, object[] parameters)
    {
        jsObj.Verify(_ => _.InvokeAsync<TResponse>(methodName, It.IsAny<CancellationToken>(), parameters));
    }

    public static void VerifyStateMutation<T>(this Mock<IMutableFluxState<TestState>> mutation, T input, int times)
    {
        mutation.Verify(_ => _.MutateState(input), Times.Exactly(times));
    }
}


