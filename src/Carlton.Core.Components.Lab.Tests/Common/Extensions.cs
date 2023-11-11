using Carlton.Core.Components.Flux.Models;

namespace Carlton.Core.Components.Lab.Test.Common;

internal static class Extensions
{
    public static TComand Cast<TComand>(this MutationCommand command)
        where TComand : MutationCommand
    {
        return (TComand)command;
    }
}
