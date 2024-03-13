namespace Carlton.Core.Flux.Extensions;

/// <summary>
/// Represents the options for configuring the Flux library.
/// </summary>
public class FluxOptions
{
	/// <summary>
	/// Gets or sets a value indicating whether to add support for local storage.
	/// </summary>
	/// <value><c>true</c> if support for local storage should be added; otherwise, <c>false</c>.</value>
	public bool AddLocalStorage { get; set; } = false;

	/// <summary>
	/// Gets or sets a value indicating whether to add support for HTTP interception.
	/// </summary>
	/// <value><c>true</c> if support for HTTP interception should be added; otherwise, <c>false</c>.</value>
	public bool AddHttpInterception { get; set; } = true;
}
