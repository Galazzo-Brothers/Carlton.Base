namespace Carlton.Core.Utilities.UnitTesting;

public static class TestingRndUtilities
{
    public static int GetRandomActiveIndex(int count)
    {
        return new Random().Next(0, count);
    }
}
