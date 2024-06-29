namespace Carlton.Core.Utilities.Extensions;

/// <summary>
/// Provides extension methods for working with delegates.
/// </summary>
public static class DelegateExtensions
{
    /// <summary>
    /// Asynchronously invokes a collection of delegates with the specified arguments.
    /// </summary>
    /// <typeparam name="TArgs">The type of the arguments passed to the delegates.</typeparam>
    /// <param name="delegates">The collection of delegates to invoke.</param>
    /// <param name="args">The arguments to pass to the delegates.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task RaiseAsyncDelegates<TArgs>(this Delegate[] delegates, TArgs args)
    {
        var tasks = delegates.Select(handlers =>
                        {
                            var castedHandler = handlers as Func<TArgs, Task>;
                            return castedHandler(args);
                        });

        await Task.WhenAll(tasks);
    }
}
