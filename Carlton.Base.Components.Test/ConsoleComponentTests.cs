using Microsoft.AspNetCore.Components;

namespace Carlton.Base.Components.Test;

public class ConsoleComponentTests : TestContext
{
    private static readonly string ConsoleMarkup = @"
<div class=""console"" b-26bfns5jah>
    <textarea rows=""15"" class="""" blazor:onchange=""1"" b-26bfns5jah>this is some dummt text</textarea>
</div>";


    [Fact]
    public void Console_Markup_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, false)
            .Add(p => p.Text, "this is some dummt text")
            );

        //Assert
        cut.MarkupMatches(ConsoleMarkup);
    }

    [Fact]
    public void Console_ReadOnlyParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "this is some dummt text")
            );

        var consoleElement = cut.Find("textarea");

        //Assert
        Assert.NotEmpty(consoleElement.Attributes.Where(_ => _.Name == "disabled"));
    }

    [Fact]
    public void Console_TextParam_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            );

        var consoleElement = cut.Find("textarea");

        //Assert
        Assert.Equal("here is some super special test text", consoleElement.TextContent);
    }

    [Fact]
    public void Console_ValidateFuncParam_ValidatesTrue_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            .Add(p => p.ValidateFunc, (_) => Task.FromResult(true))
            );

        var consoleElement = cut.Find("textarea");

        //Assert
        Assert.DoesNotContain("error", consoleElement.ClassList);
    }

    [Fact]
    public void Console_ValidateFuncParam_ValidatesFalse_RendersCorrectly()
    {
        //Act
        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            .Add(p => p.ValidateFunc, (_) => Task.FromResult(false))
            );

        var consoleElement = cut.Find("textarea");

        //Assert
        Assert.Contains("error", consoleElement.ClassList);
    }

    [Fact]
    public void Console_OnChangeCallbackParam_IfValidatesTrue_RendersCorrectly()
    {
        //Arrange
        var eventCalled = false;
        ChangeEventArgs changeEventArgs;

        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            .Add(p => p.ValidateFunc, (_) => Task.FromResult(true))
            .Add(p => p.OnChangeCallback, (_) => { eventCalled = true; changeEventArgs = _; })
            );

        var consoleElement = cut.Find("textarea");

        //Act
       consoleElement.Change(new ChangeEventArgs { Value = "New Value" });

        //Assert
        Assert.True(eventCalled);
    }

    [Fact]
    public void Console_OnChangeCallbackParam_IfValidatesFalse_RendersCorrectly()
    {
        //Arrange
        var eventCalled = false;
        ChangeEventArgs changeEventArgs;

        var cut = RenderComponent<Console>(parameters => parameters
            .Add(p => p.IsReadOnly, true)
            .Add(p => p.Text, "here is some super special test text")
            .Add(p => p.ValidateFunc, (_) => Task.FromResult(false))
            .Add(p => p.OnChangeCallback, (_) => { eventCalled = true; changeEventArgs = _; })
            );

        var consoleElement = cut.Find("textarea");

        //Act
        consoleElement.Change(new ChangeEventArgs { Value = "New Value" });

        //Assert
        Assert.False(eventCalled);
    }
}
