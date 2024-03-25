namespace Carlton.Core.Utilities.Results;

/// <summary>
/// Represents a result of an operation that can either be a success with a value of type <typeparamref name="TValue"/>,
/// or a failure with an error of type <typeparamref name="TError"/>.
/// </summary>
/// <typeparam name="TValue">The type of the successful value.</typeparam>
/// <typeparam name="TError">The type of the error in case of failure.</typeparam>
public readonly record struct Result<TValue, TError>
{
	private readonly TValue? _value;
	private readonly TError? _error;

	public bool IsSuccess { get; }

	private Result(TValue value)
	{
		IsSuccess = true;
		_value = value;
		_error = default;
	}

	private Result(TError error)
	{
		IsSuccess = false;
		_value = default;
		_error = error;
	}

	//happy path
	public static implicit operator Result<TValue, TError>(TValue value) => new(value);

	//error path
	public static implicit operator Result<TValue, TError>(TError error) => new(error);

	/// <summary>
	/// Matches the result and executes the appropriate action based on whether the result is a success or a failure.
	/// </summary>
	/// <typeparam name="TResult">The type of the result of the actions.</typeparam>
	/// <param name="success">The action to execute if the result is a success.</param>
	/// <param name="failure">The action to execute if the result is a failure.</param>
	/// <returns>The result of executing the appropriate action.</returns>
	public TResult Match<TResult>(
		Func<TValue, TResult> success,
		Func<TError, TResult> failure)
		=> IsSuccess ? success(_value!) : failure(_error!);


	/// <summary>
	/// Gets the error associated with this result.
	/// </summary>
	/// <returns>The error associated with this result.</returns>
	/// <exception cref="InvalidOperationException">Thrown when attempting to retrieve an error from a successful result.</exception>
	public TError GetError()
	{
		if (IsSuccess)
		{
			throw new InvalidOperationException("Cannot retrieve error from a successful result.");
		}

		return _error!;
	}
}

