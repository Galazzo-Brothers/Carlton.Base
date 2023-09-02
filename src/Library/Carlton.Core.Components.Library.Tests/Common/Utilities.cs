
namespace Carlton.Core.Components.Library.Tests.Common;

public static class Utilities
{
    public static int GetRandomActiveIndex(int count)
    {
        return new Random().Next(0, count);
    }
}
