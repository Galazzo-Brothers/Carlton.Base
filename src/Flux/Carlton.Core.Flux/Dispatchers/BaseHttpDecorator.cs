using Carlton.Core.Flux.Attributes;
using System.Text.RegularExpressions;
namespace Carlton.Core.Flux.Handlers.Base;

internal abstract partial class BaseHttpDecorator<TState>(HttpClient _client)
{
	protected HttpClient Client { get; init; } = _client;

	protected Result<string, FluxError> GetServerUrl(
	  FluxServerCommunicationAttribute attribute,
	  IEnumerable<FluxServerCommunicationParameterAttribute> routeParameters,
	  object sender)
	{
		//Tokenized Url
		var url = attribute.ServerUrl;

		//Replace Tokens
		foreach (var routeParam in routeParameters)
		{
			var value = GetDataEndPointParameterType(sender, routeParam);
			url = url.Replace($"{{{routeParam.ParameterName}}}", value);
		}

		//Check for unreplaced tokens
		var tokenMatches = TokenRegex().Matches(url);
		if (tokenMatches.Count > 0)
		{
			var unreplacedTokens = tokenMatches.Select(t => t.Value);
			return HttpUrlConstructionUnreplacedTokensError(url, unreplacedTokens);
		}

		//Check if the result is valid
		var isValidUri = Uri.IsWellFormedUriString(url, UriKind.Absolute);

		//Return an error if the URL is invalid
		if (!isValidUri)
			return HttpUrlConstructionError(url);

		return url;
	}

	protected bool GetRefreshPolicy(FluxServerCommunicationPolicy? policy)
	{
		return policy switch
		{
			FluxServerCommunicationPolicy.Never => false,
			FluxServerCommunicationPolicy.Always => true,
			_ => false
		};
	}

	protected static IEnumerable<FluxServerCommunicationParameterAttribute> GetParameterAttributes(object sender)
	{
		return sender.GetType().GetProperties().SelectMany(p => p.GetCustomAttributes<FluxServerCommunicationParameterAttribute>());
	}

	private static string GetDataEndPointParameterType(object sender, FluxServerCommunicationParameterAttribute fluxRouteParam)
	{
		return sender.GetType().GetProperty(fluxRouteParam.ParameterName).GetValue(sender).ToString();
	}


	[GeneratedRegex("\\{([^{}]+)\\}")]
	public static partial Regex TokenRegex();
}
