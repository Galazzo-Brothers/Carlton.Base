namespace Carlton.Core.Components.Tests;


[Trait("Component", nameof(ErrorPrompt))]
public class ErrorPromptComponentUnitTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ErrorPrompt_Markup_RendersCorrectly(
        string expectedErrorHeader,
        string expectedErrorMessage,
        string expectedIconClass)
    {
        //Arrange
        var expectedMarkup = @$"
<div class=""error-prompt"">
    <div class=""error-header"">
        <div class=""content"">
            <div class=""icon mdi mdi-48px {expectedIconClass}""></div>
            <span class=""error-header-message"">{expectedErrorHeader}</span>
        </div>
    </div>
    <div class=""error-body"">
        <div class=""content"">
            <span class=""error-message"">{expectedErrorMessage}</span>
            <button class=""retry-btn"">Retry</button>
        </div>
    </div>
</div>";

        //Act
        var cut = RenderComponent<ErrorPrompt>(parameters => parameters
                .Add(p => p.ErrorHeader, expectedErrorHeader)
                .Add(p => p.ErrorMessage, expectedErrorMessage)
                .Add(p => p.ErrorIconClass, expectedIconClass));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "ErrorHeader Parameter Test"), AutoData]
    public void ErrorPrompt_ErrorHeaderParameter_RendersCorrectly(
       string expectedErrorHeader,
       string expectedErrorMessage,
       string expectedIconClass)
    {
        //Act
        var cut = RenderComponent<ErrorPrompt>(parameters => parameters
                .Add(p => p.ErrorHeader, expectedErrorHeader)
                .Add(p => p.ErrorMessage, expectedErrorMessage)
                .Add(p => p.ErrorIconClass, expectedIconClass));


        var errorHeaderMessage = cut.Find(".error-header-message").TextContent;

        //Assert
        errorHeaderMessage.ShouldBe(expectedErrorHeader);
    }

    [Theory(DisplayName = "ErrorMessage Parameter Test"), AutoData]
    public void ErrorMessage_Parameter_RendersCorrectly(
       string expectedErrorHeader,
       string expectedErrorMessage,
       string expectedIconClass)
    {
        //Act
        var cut = RenderComponent<ErrorPrompt>(parameters => parameters
                .Add(p => p.ErrorHeader, expectedErrorHeader)
                .Add(p => p.ErrorMessage, expectedErrorMessage)
                .Add(p => p.ErrorIconClass, expectedIconClass));


        var errorHeaderMessage = cut.Find(".error-message").TextContent;

        //Assert
        errorHeaderMessage.ShouldBe(expectedErrorMessage);
    }

    [Theory(DisplayName = "ErrorIcon Parameter Test"), AutoData]
    public void ErrorIcon_Parameter_RendersCorrectly(
      string expectedErrorHeader,
      string expectedErrorMessage,
      string expectedIconClass)
    {
        //Act
        var cut = RenderComponent<ErrorPrompt>(parameters => parameters
                .Add(p => p.ErrorHeader, expectedErrorHeader)
                .Add(p => p.ErrorMessage, expectedErrorMessage)
                .Add(p => p.ErrorIconClass, expectedIconClass));


        var errorIcon = cut.Find(".icon");

        //Assert
        errorIcon.ClassList.ShouldContain(expectedIconClass);
    }

    [Theory(DisplayName = "Retry Parameter Test"), AutoData]
    public void Retry_Parameter_RendersCorrectly(
     string expectedErrorHeader,
     string expectedErrorMessage,
     string expectedIconClass)
    {
        //Arrange
        var retryWasCalled = false;

        var cut = RenderComponent<ErrorPrompt>(parameters => parameters
                .Add(p => p.ErrorHeader, expectedErrorHeader)
                .Add(p => p.ErrorMessage, expectedErrorMessage)
                .Add(p => p.ErrorIconClass, expectedIconClass)
                .Add(p => p.Retry, () => retryWasCalled = true));

        var retryBtn = cut.Find(".retry-btn");

        //Act
        retryBtn.Click();

        //Assert
        retryWasCalled.ShouldBeTrue();
    }
}
