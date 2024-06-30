namespace Carlton.Core.LayoutServices.ViewState;

/// <summary>
/// Provides methods to manage view state within a web application.
/// </summary>
public interface IViewStateService
{
	/// <summary>
	/// Gets the value associated with the specified key.
	/// </summary>
	/// <typeparam name="T">The type of the value to get.</typeparam>
	/// <param name="key">The key of the value to get.</param>
	/// <returns>The value associated with the specified key.</returns>
	T Get<T>(string key);

	/// <summary>
	/// Sets the value for the specified key.
	/// </summary>
	/// <typeparam name="T">The type of the value to set.</typeparam>
	/// <param name="key">The key of the value to set.</param>
	/// <param name="value">The value to set.</param>
	void Set<T>(string key, T value);

	/// <summary>
	/// Clears the value associated with the specified key.
	/// </summary>
	/// <param name="key">The key of the value to clear.</param>
	void Clear(string key);

	/// <summary>
	/// Determines whether the specified key has been initialized.
	/// </summary>
	/// <param name="key">The key to check for initialization.</param>
	/// <returns><c>true</c> if the key has been initialized; otherwise, <c>false</c>.</returns>
	bool IsInitialized(string key);

	/// <summary>
	/// Initializes the specified key with the given value if it has not been initialized.
	/// </summary>
	/// <typeparam name="T">The type of the value to initialize.</typeparam>
	/// <param name="key">The key to initialize.</param>
	/// <param name="value">The value to initialize the key with.</param>
	void InitializeKey<T>(string key, T value);

	/// <summary>
	/// Marks the specified key as initialized without setting a value.
	/// </summary>
	/// <typeparam name="T">The type of the key to initialize.</typeparam>
	/// <param name="key">The key to mark as initialized.</param>
	void InitializeKey<T>(string key);
}
