namespace Carlton.Core.Components.Library.Lab.TestData;

public class ErrorPromptTestStates
{
    public static Dictionary<string, object> DefaultState
    {
        get => new()
        {
            { nameof(ErrorPrompt.ErrorHeader), "Error" },
            { nameof(ErrorPrompt.ErrorMessage), "This is an unhandled error." },
            { nameof(ErrorPrompt.ErrorIconClass), "mdi-alert-circle-outline" }
        };
    }
}

