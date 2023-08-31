using FluentValidation;

namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockValidatorExtensions
{
    public static void VerifyValidator<T>(this Mock<IValidator<T>> validator)
    {
        validator.Verify(_ => _.Validate(It.IsAny<ValidationContext<T>>()), Times.Once);
    }

}
