using Carlton.Core.Flux.Attributes;
using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Tests.Common;

public record TestError() : FluxError("This is a test", -1);

public record TestViewModel([property: NonNegativeInteger] int Id, string Name, string Description);

public record TestCommand1([property: NonNegativeInteger] int Id, string Name, string Description);


[FluxServerCommunication("http://test.carlton.com/", HttpVerb.GET, FluxServerCommunicationPolicy.Always)]
public class FluxServerCommunicationAlwaysGet
{
	public const string MockRefreshUrl = "http://test.carlton.com/";
}

[FluxServerCommunication("http://test.carlton.com/", HttpVerb.POST, FluxServerCommunicationPolicy.Always)]
public class FluxServerCommunicationAlwaysPost
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

