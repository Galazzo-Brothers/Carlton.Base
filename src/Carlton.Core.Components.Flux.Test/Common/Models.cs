using Carlton.Core.Components.Flux.Models;
using FluentValidation;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Test.Common;

public record TestViewModel1(int ID, string Name, string Description);
public record TestViewModel2(int ID, string SomeOtherProperty, int YetAnotherProperty);

public record TestCommand1(object Sender, int ID, string Name, string Description) : MutationCommand(Sender);
public record TestCommand2(object Sender, int ID, string SomeOtherProperty, int YetAnotherProperty) : MutationCommand(Sender);


public static class TestDataGenerator
{
    public readonly static TestViewModel1 ExpectedViewModel_1 = new(1, "Testing", "This is a test");
    public readonly static TestViewModel2 ExpectedViewModel_2 = new(2, "Testing Again", 17);

    public readonly static TestCommand1 ExpectedCommand_1 = new(new object(), 1, "Testing", "This is a test");
    public readonly static TestCommand2 ExpectedCommand_2 = new(new object(), 2, "Testing Again", 17);


    public static IEnumerable<object[]> GetViewModelData()
    {
        var testData = new List<object[]>
        {
            new object[] { ExpectedViewModel_1  },
            new object[] {  ExpectedViewModel_2 },
        };

        return testData;
    }

    public static IEnumerable<object[]> GetCommandData()
    {
        var testData = new List<object[]>
        {
            new object[] { ExpectedCommand_1  },
            new object[] { ExpectedCommand_2 },
        };

        return testData;
    }

    public static IEnumerable<object[]> GetJsCallersData()
    {
        var testData = new List<object[]>
        {
            new object[] { new JsRefreshCaller()  },
            new object[] { new JsRefreshCaller2() },
        };

        return testData;
    }

    public static IEnumerable<object[]> GetViewModelExceptionData()
    {
        var testData = new List<object[]>
        {
            new object[] { new JsonException(), LogEvents.ViewModel_JSON_ErrorMsg  },
            new object[] { new HttpRequestException(), LogEvents.ViewModel_HTTP_ErrorMsg },
            new object[] { new JSException("There was a JS error."), LogEvents.ViewModel_JSInterop_ErrorMsg },
            new object[] { new ValidationException("There was a validation error."), LogEvents.ViewModel_Validation_ErrorMsg },
            new object[] { new Exception(), LogEvents.ViewModel_Unhandled_ErrorMsg }
        };

        return testData;
    }
}

[ViewModelJsInteropRefresh("test_module", "test_function", "param1", 17, false)]
public class JsRefreshCaller
{

}

[ViewModelJsInteropRefresh("test_module", "test_function_2", "param2", 177, true)]
public class JsRefreshCaller2
{

}

[ViewModelHttpRefresh("http://test.carlton.com/",
    HttpVerb = Attributes.HttpVerb.GET,
    DataRefreshPolicy = Attributes.DataEndpointRefreshPolicy.Always)]
public class HttpRefreshCaller
{
}

[ViewModelHttpRefresh("http://test.carlton.com/",
    HttpVerb = Attributes.HttpVerb.GET,
    DataRefreshPolicy = Attributes.DataEndpointRefreshPolicy.Never)]
public class HttpNeverRefreshCaller
{
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[ViewModelHttpRefreshParameter("ClientID", Attributes.DataEndpointParameterType.ComponentParameter)]
[ViewModelHttpRefreshParameter("UserID", Attributes.DataEndpointParameterType.ComponentParameter)]
public class HttpRefreshWithParametersCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[ViewModelHttpRefreshParameter("ClientID", Attributes.DataEndpointParameterType.StateStoreParameter)]
[ViewModelHttpRefreshParameter("UserID", Attributes.DataEndpointParameterType.StateStoreParameter)]
public class HttpRefreshWithStateParametersCaller
{
}



[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
public class HttpRefreshWithInvalidParametersCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}


public class NoRefreshCaller
{

}

public class TestState
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

