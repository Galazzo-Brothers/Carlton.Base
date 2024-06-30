namespace Carlton.Core.LayoutServices.ViewState;

/// <summary>
/// Provides a service to manage view state within a web application.
/// </summary>
public class ViewStateService : IViewStateService
{
	private readonly Dictionary<string, object> _state = [];

	/// <summary>
	/// Gets the value associated with the specified key.
	/// </summary>
	/// <typeparam name="T">The type of the value to get.</typeparam>
	/// <param name="key">The key of the value to get.</param>
	/// <returns>The value associated with the specified key, or the default value for the type if the key is not found.</returns>
	public T Get<T>(string key)
	{
		if (_state.TryGetValue(key, out var value) && value is T typedValue)
		{
			return typedValue;
		}
		return default;
	}

	/// <summary>
	/// Sets the value for the specified key.
	/// </summary>
	/// <typeparam name="T">The type of the value to set.</typeparam>
	/// <param name="key">The key of the value to set.</param>
	/// <param name="value">The value to set.</param>
	public void Set<T>(string key, T value)
	{
		_state[key] = value;
	}

	/// <summary>
	/// Clears the value associated with the specified key.
	/// </summary>
	/// <param name="key">The key of the value to clear.</param>
	public void Clear(string key)
	{
		_state.Remove(key);
	}

	/// <summary>
	/// Determines whether the specified key has been initialized.
	/// </summary>
	/// <param name="key">The key to check for initialization.</param>
	/// <returns><c>true</c> if the key has been initialized; otherwise, <c>false</c>.</returns>
	public bool IsInitialized(string key)
	{
		return _state.ContainsKey(key);
	}

	/// <summary>
	/// Initializes the specified key with the given value if it has not been initialized.
	/// </summary>
	/// <typeparam name="T">The type of the value to initialize.</typeparam>
	/// <param name="key">The key to initialize.</param>
	/// <param name="value">The value to initialize the key with.</param>
	public void InitializeKey<T>(string key, T value)
	{
		if (!_state.ContainsKey(key))
			_state[key] = value;
	}

	/// <summary>
	/// Marks the specified key as initialized without setting a value.
	/// </summary>
	/// <typeparam name="T">The type of the key to initialize.</typeparam>
	/// <param name="key">The key to mark as initialized.</param>
	public void InitializeKey<T>(string key)
	{
		if (!_state.ContainsKey(key))
			_state[key] = default(T);
	}
}

