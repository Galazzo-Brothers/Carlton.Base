using Carlton.Core.Flux.Attributes;
using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Tests.Common;

public record TestError() : FluxError("This is a test", -1);

public record TestViewModel([property: NonNegativeInteger] int ID, string Name, string Description);

[HttpResponseType<MockServerResponse>]
public record TestCommand1([property: HttpResponseProperty("ServerName")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description);

public record TestCommand2(int ID, string Name, int SomeNumber);

[HttpResponseType<string>]
public record TestCommand3([property: HttpResponseProperty("ServerName")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description);

[HttpResponseType<MockServerResponse>]
public record TestCommand4([property: HttpResponseProperty("ServerNameXXX")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description);

[HttpResponseType<string>]
public record TestCommand5([property: HttpResponseProperty("ServerNameXXX")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description, Type WillNotSerialize);

public record MockServerResponse(string ServerName, string ServerDescription);



[ViewModelHttpRefresh("http://test.carlton.com/")]
[MutationHttpRefresh("http://test.carlton.com/")]
public class HttpRefreshCaller
{
    public Type type { get; set; }
    public const string MockRefreshUrl = "http://test.carlton.com/";
}

[ViewModelHttpRefresh("http://test.carlton.com/",
    HttpVerb = HttpVerb.GET,
    DataRefreshPolicy = DataEndpointRefreshPolicy.Never)]
public class HttpNeverRefreshCaller
{
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
[HttpRefreshParameter("ClientId", DataEndpointParameterType.ComponentParameter)]
[HttpRefreshParameter("UserId", DataEndpointParameterType.ComponentParameter)]
public class HttpRefreshWithComponentParametersCaller
{
    public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

    public int ClientId { get; set; } = 5;
    public int UserId { get; set; } = 10;
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
public class HttpRefreshWithComponentUnreplacedParametersCaller
{
    public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

    public int ClientId { get; set; } = 5;
    public int UserId { get; set; } = 10;
}

[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
[HttpRefreshParameter("ClientId", DataEndpointParameterType.StateStoreParameter)]
[HttpRefreshParameter("UserId", DataEndpointParameterType.StateStoreParameter)]
public class HttpRefreshWithStateParametersCaller
{
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
public class HttpRefreshWithInvalidParametersCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

[ViewModelHttpRefresh("http://test.#%$@#carlton.com/clients/")]
[MutationHttpRefresh("http://test.#%$@#carlton.com/clients/")]
public class HttpRefreshWithInvalidHttpUrlCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

public class NoRefreshCaller
{

}

public class TestState
{
    public int ClientId { get; set; } = 5;
    public int UserId { get; set; } = 10;
}

