using Carlton.Core.Components.Flux.Attributes;
using Carlton.Core.Components.Flux.Models;

namespace Carlton.Core.Components.Flux.Test.Common;

public record TestViewModel(int ID, string Name, string Description);

[HttpResponseType<MockServerResponse>]
public record TestCommand1([property: HttpResponseProperty("ServerName")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description) : MutationCommand;

public record TestCommand2(int ID, string Name, int SomeNumber) : MutationCommand;

[HttpResponseType<string>]
public record TestCommand3([property: HttpResponseProperty("ServerName")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description) : MutationCommand;

[HttpResponseType<MockServerResponse>]
public record TestCommand4([property: HttpResponseProperty("ServerNameXXX")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description) : MutationCommand;

[HttpResponseType<string>]
public record TestCommand5([property: HttpResponseProperty("ServerNameXXX")] string Name, [property: HttpResponseProperty("ServerDescription")] string Description, Type WillNotSerialize) : MutationCommand;

public record MockServerResponse(string ServerName, string ServerDescription);



[ViewModelJsInteropRefresh("test_module", "test_function", "param1", 17, false)]
public class JsRefreshCaller
{

}


[ViewModelHttpRefresh("http://test.carlton.com/")]
[MutationHttpRefresh("http://test.carlton.com/")]
public class HttpRefreshCaller
{
}

[ViewModelHttpRefresh("http://test.carlton.com/",
    HttpVerb = Attributes.HttpVerb.GET,
    DataRefreshPolicy = Attributes.DataEndpointRefreshPolicy.Never)]
public class HttpNeverRefreshCaller
{
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[HttpRefreshParameter("ClientID", Attributes.DataEndpointParameterType.ComponentParameter)]
[HttpRefreshParameter("UserID", Attributes.DataEndpointParameterType.ComponentParameter)]
public class HttpRefreshWithComponentParametersCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[HttpRefreshParameter("ClientID", Attributes.DataEndpointParameterType.StateStoreParameter)]
[HttpRefreshParameter("UserID", Attributes.DataEndpointParameterType.StateStoreParameter)]
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

