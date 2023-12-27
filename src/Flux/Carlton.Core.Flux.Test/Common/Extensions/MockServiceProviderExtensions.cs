﻿namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockServiceProviderExtensions
{
    public static void SetupServiceProvider<T>(this Mock<IServiceProvider> serviceProvider, object implementation)
    {
        serviceProvider.Setup(_ => _.GetService(typeof(T))).Returns(implementation);
    }
}