using System.Net.Http;

namespace Carlton.Core.Utilities.Extensions;

public static class HttpContentExtensions
{
	public static bool IsJsonContent(this HttpContent content)
	{
		var contentType = content.Headers.ContentType?.MediaType;
		var isJsonContentType = (contentType == "application/json" || contentType == "text/json");
		return isJsonContentType;
	}
}
