using Carlton.Core.Flux.Attributes;
using Carlton.Core.Utilities.Validation;
namespace Carlton.Core.Flux.Tests.Common;

internal record TestError() : FluxError("This is a test", -1);

public record TestViewModel([property: NonNegativeInteger] int Id, string Name, string Description);

public record TestCommand([property: NonNegativeInteger] int Id, string Name, string Description, int ClientId, int UserId);

public class TestMutation : IFluxStateMutation<TestState, TestCommand>
{
	public string StateEvent => "TestMutation";

	public TestState Mutate(TestState state, TestCommand command)
	{
		return state with
		{
			ClientId = command.ClientId,
			UserId = command.UserId
		};
	}
}


[FluxViewModelServerCommunication("http://test.carlton.com/")]
public record FluxServerCommunicationAlwaysGet
{
	public const string MockRefreshUrl = "http://test.carlton.com/";
}

[FluxMutationCommandServerCommunication("http://test.carlton.com/", HttpVerb.POST)]
public record FluxServerCommunicationAlwaysPost
{
	public const string MockRefreshUrl = "http://test.carlton.com/";
}

[FluxMutationCommandServerCommunication(
	"http://test.carlton.com/",
	HttpVerb.POST,
	UpdateWithResponseBody = true)]
public record FluxServerCommunicationAlwaysPostUpdateWithResponseBody
{
	public const string MockRefreshUrl = "http://test.carlton.com/";

	public int Id { get; init; }
	public string Name { get; init; }

}


[FluxViewModelServerCommunication("http://test.carlton.com/",
	ServerCommunicationPolicy = FluxServerCommunicationPolicy.Never)]
public record FluxServerCommunicationNever
{
}


[FluxViewModelServerCommunication("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
public record FluxServerCommunicationWithComponentParametersGet
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	[FluxServerCommunicationParameter]
	public int ClientId { get; set; } = 5;
	[FluxServerCommunicationParameter]
	public int UserId { get; set; } = 10;
}

[FluxMutationCommandServerCommunication("http://test.carlton.com/clients/{ClientId}/users/{UserId}", HttpVerb.POST)]
public record FluxServerCommunicationWithComponentParametersPost
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	[FluxServerCommunicationParameter]
	public int ClientId { get; set; } = 5;
	[FluxServerCommunicationParameter]
	public int UserId { get; set; } = 10;
}


[FluxViewModelServerCommunication("http://test.carlton.com/clients/{ClientId}/users/{UserId}")]
public record FluxServerCommunicationWithUnreplacedParametersGet
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	public int ClientId { get; set; } = 5;
	public int UserId { get; set; } = 10;
}

[FluxMutationCommandServerCommunication("http://test.carlton.com/clients/{ClientId}/users/{UserId}", HttpVerb.POST)]
public record FluxServerCommunicationWithUnreplacedParametersPost
{
	public const string MockRefreshUrlTemplate = "http://test.carlton.com/clients/{ClientId}/users/{UserId}";

	public int ClientId { get; set; } = 5;
	public int UserId { get; set; } = 10;
}


[FluxViewModelServerCommunication("http://test.#%$@#carlton.com/clients/")]
public record FluxServerCommunicationWithInvalidUrlGet
{
	public int ClientID { get; set; } = 5;
	public int UserID { get; set; } = 10;
}

[FluxMutationCommandServerCommunication("http://test.#%$@#carlton.com/clients/", HttpVerb.POST)]
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

