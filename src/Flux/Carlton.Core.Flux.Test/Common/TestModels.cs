using Carlton.Core.Flux.Attributes;
using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Tests.Common;

public record TestError() : FluxError("This is a test", -1);

public record TestViewModel([property: NonNegativeInteger] int Id, string Name, string Description);

public record TestCommand([property: NonNegativeInteger] int Id, string Name, string Description);


[FluxServerCommunication("http://test.carlton.com/", HttpVerb.GET, FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationAlwaysGet
{
	public const string MockRefreshUrl = "http://test.carlton.com/";
}

[FluxServerCommunication("http://test.carlton.com/", HttpVerb.POST, FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationAlwaysPost
{
	public const string MockRefreshUrl = "http://test.carlton.com/";
}

[FluxServerCommunication(
	"http://test.carlton.com/",
	HttpVerb.POST,
	FluxServerCommunicationPolicy.Always,
	UpdateWithResponseBody = true)]
public record FluxServerCommunicationAlwaysPostUpdateWithResponseBody
{
	public const string MockRefreshUrl = "http://test.carlton.com/";

	public int Id { get; init; }
	public string Name { get; init; }

}


[FluxServerCommunication("http://test.carlton.com/", HttpVerb.GET, FluxServerCommunicationPolicy.Never)]
public record FluxServerCommunicationNever
{
}


[FluxServerCommunication(
	serverUrl: "http://test.carlton.com/clients/{ClientId}/users/{UserId}",
	httpVerb: HttpVerb.GET,
	serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationWithComponentParametersGet
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	[FluxServerCommunicationParameter]
	public int ClientId { get; set; } = 5;
	[FluxServerCommunicationParameter]
	public int UserId { get; set; } = 10;
}

[FluxServerCommunication(
	serverUrl: "http://test.carlton.com/clients/{ClientId}/users/{UserId}",
	httpVerb: HttpVerb.POST,
	serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationWithComponentParametersPost
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
public record FluxServerCommunicationWithUnreplacedParametersGet
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	public int ClientId { get; set; } = 5;
	public int UserId { get; set; } = 10;
}

[FluxServerCommunication(
	serverUrl: "http://test.carlton.com/clients/{ClientId}/users/{UserId}",
	httpVerb: HttpVerb.POST,
	serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationWithUnreplacedParametersPost
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	public int ClientId { get; set; } = 5;
	public int UserId { get; set; } = 10;
}


[FluxServerCommunication(
	serverUrl: "http://test.#%$@#carlton.com/clients/",
	httpVerb: HttpVerb.GET,
	serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationWithInvalidUrlGet
{
	public int ClientID { get; set; } = 5;
	public int UserID { get; set; } = 10;
}

[FluxServerCommunication(
	serverUrl: "http://test.#%$@#carlton.com/clients/",
	httpVerb: HttpVerb.GET,
	serverCommunicationPolicy: FluxServerCommunicationPolicy.Always)]
public record FluxServerCommunicationWithInvalidUrlPost
{
	public int ClientID { get; set; } = 5;
	public int UserID { get; set; } = 10;
}

public record TestState
{
	public int ClientId { get; set; } = 5;
	public int UserId { get; set; } = 10;
}

