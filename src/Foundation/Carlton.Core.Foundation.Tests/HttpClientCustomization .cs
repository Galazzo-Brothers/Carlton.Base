using AutoFixture;
using RichardSzalay.MockHttp;
namespace Carlton.Core.Foundation.Tests;

public class HttpClientCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var mockHttp = new MockHttpMessageHandler();
        var httpClient = new HttpClient(mockHttp);

        fixture.Register(() => httpClient);
        fixture.Register(() => mockHttp);
    }
}
