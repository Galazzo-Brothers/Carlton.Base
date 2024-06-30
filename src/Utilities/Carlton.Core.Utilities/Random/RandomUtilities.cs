namespace Carlton.Core.Utilities.Random;

/// <summary>
/// Provides utility methods for generating random values.
/// </summary>
public static class RandomUtilities
{
    private static readonly System.Random _random = new();

    /// <summary>
    /// Generates a random index between 0 (inclusive) and the specified count (exclusive).
    /// </summary>
    /// <param name="count">The upper bound (exclusive) of the random index.</param>
    /// <returns>A random index between 0 (inclusive) and the specified count (exclusive).</returns>
    public static int GetRandomIndex(int count)
    {
        //Greater than or Equal to 0
        //Strictly Less Than Count
        return _random.Next(0, count);
    }

    /// <summary>
    /// Generates a random non-zero index between 1 (inclusive) and the specified count (exclusive).
    /// </summary>
    /// <param name="count">The upper bound (exclusive) of the random index.</param>
    /// <returns>A random non-zero index between 1 (inclusive) and the specified count (exclusive).</returns>
    public static int GetRandomNonZeroIndex(int count)
    {
        //Greater than or Equal to 1
        //Strictly Less Than Count
        return _random.Next(1, count);
    }
}
