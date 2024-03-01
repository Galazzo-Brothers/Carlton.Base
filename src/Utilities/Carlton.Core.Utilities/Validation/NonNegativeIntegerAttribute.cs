using System.ComponentModel.DataAnnotations;
namespace Carlton.Core.Utilities.Validation;


/// <summary>
/// Validates that the value of a property, field, or parameter is a non-negative integer.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class NonNegativeIntegerAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            // null values are considered valid; use [Required] attribute for required fields
            return ValidationResult.Success;
        }

        if (value is not int intValue || intValue < 0)
        {
            return new ValidationResult($"{validationContext.DisplayName} must be a non-negative integer.");
        }

        return ValidationResult.Success;
    }
}