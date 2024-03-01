//using FluentValidation;
//namespace Carlton.Core.Components.Flux.Tests.Common.Extensions;

//public static class MockValidatorExtensions
//{
//    public static void VerifyValidator<T>(this Mock<IValidator<T>> validator)
//    {
//        validator.Verify(_ => _.Validate(It.IsAny<ValidationContext<T>>()), Times.Once);
//    }

//    public static void SetupValidationFailure<T>(this Mock<IValidator<T>> validator)
//    {
//        validator.Setup(_ => _.Validate(It.IsAny<ValidationContext<T>>())).Throws(new ValidationException("Validation Error"));
//    }
//}
