namespace Carlton.Core.Components.Library.Lab.TestData;

internal class ErrorPromptTestStates
{
    public static object DefaultState
    {
        get => new
        {
            ErrorHeader = "Error",
            ErrorMessage = "This is an unhandled error.",
            ErrorIconClass = "mdi-alert-circle-outline"
        };
    }
}

