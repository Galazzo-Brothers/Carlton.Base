//using Blazored.LocalStorage;
//namespace Carlton.Core.Components.Flux.Tests.Common.Extensions;

//public static class MockLocalStorageExtensions
//{
//    public static void VerifyLocalStorageCalled<T>(this Mock<ILocalStorageService> localStorage, string storageKey)
//    {
//        localStorage.Verify(mock => mock.SetItemAsync(storageKey, It.IsAny<T>(), It.IsAny<CancellationToken>()), Times.Once);
//    }

//    public static void SetupLocalStorageException<TException>(this Mock<ILocalStorageService> dispatcher, TException ex)
//      where TException : Exception
//    {
//        dispatcher.Setup(_ => _.SetItemAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
//                  .ThrowsAsync(ex);
//    }
//}
