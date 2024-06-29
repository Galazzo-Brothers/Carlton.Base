using System.ComponentModel.DataAnnotations;
namespace Carlton.Core.Utilities.Extensions;

public static class ValidationExtensions
{
    public static bool TryValidate<T>(this T instance, out IEnumerable<string> validationErrors)
    {
        var results = new List<ValidationResult>();
        var validationContext = new ValidationContext(instance);

        bool isValid = Validator.TryValidateObject(instance, validationContext, results, true);

        validationErrors = isValid ? new List<string>() : results.Select(result => result.ErrorMessage);
        return isValid;
    }

}
