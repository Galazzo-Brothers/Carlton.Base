namespace Carlton.Core.Flux.Components.ErrorComponents;

internal class ErrorPromptModelBuilder
{
    public static ErrorPromptModel GetErrorPromptModel(ViewModelQueryError error, Action recoverAct)
    {
        return error switch
        {
            Errors.ViewModelQueryErrors.ValidationError validationError => new ErrorPromptModel
                                        (
                                            "Error",
                                            validationError.Message,
                                            "mdi-alert-circle-outline",
                                            recoverAct
                                        ),
            _ => new ErrorPromptModel
                                        (
                                            "Error",
                                            GetConditionalErrorMessage(error.Message),
                                            "mdi-alert-circle-outline",
                                            recoverAct
                                        ),
        };
    }

    public static ErrorPromptModel GetErrorPromptModel(MutationCommandError error, Action recoverAct)
    {
        return error switch
        {
            Errors.MutationCommandErrors.ValidationError validationError => new ErrorPromptModel
                                        (
                                            "Error",
                                            validationError.Message,
                                            "mdi-alert-circle-outline",
                                            recoverAct
                                        ),
            _ => new ErrorPromptModel
                                        (
                                            "Error",
                                            GetConditionalErrorMessage(error.Message),
                                            "mdi-alert-circle-outline",
                                            recoverAct
                                        ),
        };
    }

    public static ErrorPromptModel FriendlyErrorPrompt(Action recoverAct) => new
    (
                                            "Error",
                                            FluxLogs.FriendlyErrorMsg,
                                            "mdi-alert-circle-outline",
                                            recoverAct
    );

    private static string GetConditionalErrorMessage(string errMsg)
    {
#if DEBUG
        return errMsg;

#else
           return FluxLogs.FriendlyErrorMsg;
#endif
    }
}


