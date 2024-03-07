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
}

[ViewModelHttpRefresh("http://test.carlton.com/",
    HttpVerb = HttpVerb.GET,
    DataRefreshPolicy = DataEndpointRefreshPolicy.Never)]
public class HttpNeverRefreshCaller
{
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[HttpRefreshParameter("ClientID", DataEndpointParameterType.ComponentParameter)]
[HttpRefreshParameter("UserID", DataEndpointParameterType.ComponentParameter)]
public class HttpRefreshWithComponentParametersCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[HttpRefreshParameter("ClientID", DataEndpointParameterType.StateStoreParameter)]
[HttpRefreshParameter("UserID", DataEndpointParameterType.StateStoreParameter)]
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
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

