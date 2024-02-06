using Carlton.Core.Components.Buttons;
using Carlton.Core.Components.DynamicComponents;
namespace Carlton.Core.Components.Tests;

[Trait("Component", nameof(DynamicEventCapturingWrapper))]
public class DynamicEventCaptureWrapperComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ComponentViewer_Markup_RendersCorrectly(bool expectedIsChecked)
    {
        //Arrange
        var expectedMarkup = @$"
        <div class=""dynamic-event-capturing-wrapper"">
            <div class=""checkbox mdi mdi-24px mdi-{(expectedIsChecked ? "check-circle" : "checkbox-blank-circle-outline")}""></div>
        </div>";
        
        var checkboxComponentType = typeof(Checkbox);
        var ComponentParameters = new Dictionary<string, object>
        {
            { "IsChecked",  expectedIsChecked }
        };

        
        //Act
        var cut = RenderComponent<DynamicEventCapturingWrapper>(parameters => parameters
            .Add(p => p.ComponentType, checkboxComponentType)
            .Add(p => p.ComponentParameters, ComponentParameters));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory(DisplayName = "ComponentEventRaisedParameter GenricEventCallback Test"), AutoData]
    public void ComponentViewer_ComponentEventRaisedParameter_GenricEventCallback_RendersCorrectly(bool expectedIsChecked)
    {
        //Arrange
        var eventRaised = false;

        var expectedEventName = "CheckboxValueChangedCallback";
        var expectedEventArgs = !expectedIsChecked;

        var eventName = string.Empty;
        var eventArgs = new object();

        var checkboxComponentType = typeof(Checkbox);
        var ComponentParameters = new Dictionary<string, object>
        {
            { "IsChecked",  expectedIsChecked }
        };

        var cut = RenderComponent<DynamicEventCapturingWrapper>(parameters => parameters
            .Add(p => p.ComponentType, checkboxComponentType)
            .Add(p => p.ComponentParameters, ComponentParameters)
            .Add(p => p.ComponentEventRaised, (args) =>
            {
                eventRaised = true;
                eventName = args.EventName;
                eventArgs = args.EventArgs;
            }));

        //Act
        cut.Find(".checkbox").Click();

        //Assert
        eventRaised.ShouldBeTrue();
        eventName.ShouldBe(expectedEventName);
        eventArgs.ShouldBe(expectedEventArgs);
    }

    [Theory(DisplayName = "ComponentEventRaisedParameter NonGenricEventCallback Test"), AutoData]
    public void ComponentViewer_ComponentEventRaisedParameter_NonGenricEventCallback_RendersCorrectly(
        string expectedText)
    {
        //Arrange
        var eventRaised = false;
        var expectedEventName = "OnClickCallback";
        var eventName = string.Empty;
        object eventArgs = null;

        var actionButtonComponentType = typeof(ActionButton);
        var ComponentParameters = new Dictionary<string, object>
        {
            { "Text",  expectedText }
        };

        var cut = RenderComponent<DynamicEventCapturingWrapper>(parameters => parameters
            .Add(p => p.ComponentType, actionButtonComponentType)
            .Add(p => p.ComponentParameters, ComponentParameters)
            .Add(p => p.ComponentEventRaised, (args) =>
            {
                eventRaised = true;
                eventName = args.EventName;
                eventArgs = args.EventArgs;
            }));

        //Act
        cut.Find(".action-btn").Click();

        //Assert
        eventRaised.ShouldBeTrue();
        eventName.ShouldBe(expectedEventName);
        eventArgs.ShouldBeEquivalentTo(new object());
    }
}
