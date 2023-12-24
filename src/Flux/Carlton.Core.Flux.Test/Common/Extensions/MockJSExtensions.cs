using Microsoft.JSInterop;

namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockJSExtensions
{
    public static void SetupIJSRuntime(this Mock<IJSRuntime> jsRuntime, string moduleName, IJSObjectReference result)
    {
        jsRuntime.Setup(_ => _.InvokeAsync<IJSObjectReference>("import", new object[] { moduleName }))
                 .Returns(ValueTask.FromResult(result));
    }

    public static void SetupIJSRuntimeException<TException>(this Mock<IJSRuntime> jsRuntime, TException exception)
        where TException : Exception
    {
        jsRuntime.Setup(_ => _.InvokeAsync<IJSObjectReference>(It.IsAny<string>(), It.IsAny<object[]>()))
                 .ThrowsAsync(exception);
    }
    public static void SetupIJSObjectReference<TResult>(this Mock<IJSObjectReference> jsObject, string funcName, object[] parameters, TResult result)
    {
        jsObject.Setup(_ => _.InvokeAsync<TResult>(funcName, It.IsAny<CancellationToken>(), parameters)).Returns(ValueTask.FromResult(result));
    }

    public static void VerifyJSRuntime(this Mock<IJSRuntime> jsRuntime, int times)
    {
        jsRuntime.Verify(_ => _.InvokeAsync<IJSObjectReference>("import", new object[] { "test_module" }), Times.Exactly(times));
    }

    public static void VerifyJSObjectReference<TResponse>(this Mock<IJSObjectReference> jsObj, string methodName, object[] parameters)
    {
        jsObj.Verify(_ => _.InvokeAsync<TResponse>(methodName, It.IsAny<CancellationToken>(), parameters));
    }

}
