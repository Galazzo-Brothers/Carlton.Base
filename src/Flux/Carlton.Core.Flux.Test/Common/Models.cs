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



[FluxServerCommunication("http://test.carlton.com/", HttpVerb.GET, FluxServerCommunicationPolicy.Always)]
public class FluxServerCommunicationAlways
{
    public const string MockRefreshUrl = "http://test.carlton.com/";
}

[FluxServerCommunication("http://test.carlton.com/", HttpVerb.GET, FluxServerCommunicationPolicy.Never)]
public class FluxServerCommunicationNever
{
}

[FluxServerCommunication(
    serverUrl: "http://test.carlton.com/clients/{ClientId}/users/{UserId}",
    httpVerb: HttpVerb.GET,
    serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public class FluxServerCommunicationWithComponentParameters
{
    public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

    [FluxServerCommunicationParameter]
    public int ClientId { get; set; } = 5;
    [FluxServerCommunicationParameter]
    public int UserId { get; set; } = 10;
}


[FluxServerCommunication(
    serverUrl: "http://test.carlton.com/clients/{ClientId}/users/{UserId}",
    httpVerb: HttpVerb.GET,
    serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public class FluxServerCommunicationWithUnreplacedParameters
{
    public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

    public int ClientId { get; set; } = 5;
    public int UserId { get; set; } = 10;
}

[FluxServerCommunication(
    serverUrl: "http://test.#%$@#carlton.com/clients/",
    httpVerb: HttpVerb.GET,
    serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public class FluxServerCommunicationWithInvalidUrl
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

public class TestState
{
    public int ClientId { get; set; } = 5;
    public int UserId { get; set; } = 10;
}

