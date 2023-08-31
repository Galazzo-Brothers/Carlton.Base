using MockHttp;
using MockHttp.Json.SystemTextJson;

namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockHttpExtensions
{
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
}
