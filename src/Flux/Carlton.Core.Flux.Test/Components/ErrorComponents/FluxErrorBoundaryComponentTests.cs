using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Errors;
using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Logging;
using Microsoft.AspNetCore.Components;
using System.Reflection;
namespace Carlton.Core.Flux.Tests.Components.ErrorComponents;

public class FluxErrorBoundaryComponentTests : TestContext
{
    [Fact]
    public void FluxErrorBoundary_RendersCorrectly()
    {
        //Arrange
        var expectedErrorMarkupTemplate = @"
          <div class=""error-prompt"">
            <span class=""header"">{0}</span>
            <span class=""icon {1}""></span>
            <span class=""message"">{2}</span>
            <button>Command Event Test</button>
          </div>";
        var expectedMarkup = "<div>No Error</div>";

        // Act
        var cut = RenderComponent<FluxErrorBoundary>(parameters =>
            parameters.Add(p => p.ChildContent, expectedMarkup)
                      .Add(p => p.ErrorPrompt, err => string.Format(expectedErrorMarkupTemplate, err.Header, err.IconClass, err.Message)));

        // Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact]
    public void FluxErrorBoundary_Error_RendersCorrectly()
    {
        //Arrange
        var expectedErrorMarkupTemplate = @"
          <div class=""error-prompt"">
            <span class=""header"">{0}</span>
            <span class=""icon {1}""></span>
            <span class=""message"">{2}</span>
            <button>Command Event Test</button>
          </div>";
        var nonErrorMarkup = "<div>No Error</div>";
        var expectedMarkup = string.Format(expectedErrorMarkupTemplate, "Error", "mdi-alert-circle-outline", FluxLogs.FriendlyErrorMsg);

        var cut = RenderComponent<FluxErrorBoundary>(parameters =>
            parameters.Add(p => p.ChildContent, nonErrorMarkup)
                      .Add(p => p.ErrorPrompt, err => string.Format(expectedErrorMarkupTemplate, err.Header, err.IconClass, err.Message)));

        var propertyInfo = typeof(ErrorBoundaryBase).GetProperty("CurrentException", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
        var onErrorMethodInfo = cut.Instance.GetType().GetMethod("OnErrorAsync", BindingFlags.Instance | BindingFlags.NonPublic);

        //Act 
        //Simulate Exception
        propertyInfo.SetValue(cut.Instance, new Exception());
        onErrorMethodInfo.Invoke(cut.Instance, new object[] { new Exception() });
        cut.Render();

        // Assert
        cut.MarkupMatches(expectedMarkup);
    }
}


