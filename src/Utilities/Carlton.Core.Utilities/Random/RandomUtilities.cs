namespace Carlton.Core.Utilities.Random;

public static class RandomUtilities
{
    private static readonly System.Random _random = new();

    public static int GetRandomIndex(int count)
    {
        //Greater than or Equal to 0
        //Strictly Less Than Count
        return _random.Next(0, count);
    }

    public static int GetRandomNonZeroIndex(int count)
    {
        //Greater than or Equal to 1
        //Strictly Less Than Count
        return _random.Next(1, count);
    }
}
