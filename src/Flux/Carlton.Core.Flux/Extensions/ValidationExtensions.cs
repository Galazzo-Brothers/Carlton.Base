using ValidationException = FluentValidation.ValidationException;

namespace Carlton.Core.Flux.Extensions;

public static class ValidationExtensions
{
    public static void ValidateAndThrow(this IValidator validator, object objToValidate)
    {
        var context = new ValidationContext<object>(objToValidate);
        var validationResult = validator.Validate(context);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }
}
