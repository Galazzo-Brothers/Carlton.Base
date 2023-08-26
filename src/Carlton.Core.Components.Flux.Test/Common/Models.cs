using Carlton.Core.Components.Flux.Models;
using FluentValidation;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Test.Common;

public record TestViewModel1(int ID, string Name, string Description);
public record TestViewModel2(int ID, string SomeOtherProperty, int YetAnotherProperty);

public record TestCommand1 : MutationCommand
{
    public string Name { get; private set; }
    public string Description { get; private set; }


    public TestCommand1(object sender, int sourceSystemID, string name, string description) : base(sender, sourceSystemID)
        => (Name, Description) = (name, description);
}

public record TestCommand2 : MutationCommand
{
    public int ID { get; private set; }
    public string Name { get; private set; }    
    public int SomeNumber { get; private set; }

    public TestCommand2(object sender, int id, string name, int someNumber) : base(sender)
        => (ID, Name, SomeNumber) = (id, name, someNumber);
}


public static class TestDataGenerator
{
    public readonly static TestViewModel1 ExpectedViewModel_1 = new(1, "Testing", "This is a test");
    public readonly static TestViewModel2 ExpectedViewModel_2 = new(2, "Testing Again", 17);

    public readonly static TestCommand1 ExpectedCommand_1 = new(new HttpRefreshCaller(), 1, "Testing", "This is a test");
    public readonly static TestCommand2 ExpectedCommand_2 = new(new HttpRefreshCaller(), 2, "Testing Again", 17);
    public readonly static TestCommand1 ExpectedCommand_3 = new(new HttpRefreshWithComponentParametersCaller(), 1, "Testing", "This is a test");
    public readonly static TestCommand2 ExpectedCommand_4 = new(new HttpRefreshWithComponentParametersCaller(), 2, "Testing Again", 17);
    public readonly static TestCommand1 ExpectedCommand_5 = new(new HttpRefreshWithStateParametersCaller(), 1, "Testing", "This is a test");
    public readonly static TestCommand2 ExpectedCommand_6 = new(new HttpRefreshWithStateParametersCaller(), 2, "Testing Again", 17);

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

    public static IEnumerable<object[]> GetCommandDataWithComponentParametersCallers()
    {
        var testData = new List<object[]>
        {
            new object[] { ExpectedCommand_3  },
            new object[] { ExpectedCommand_4 },
        };

        return testData;
    }

    public static IEnumerable<object[]> GetCommandDataWithStateParametersCallers()
    {
        var testData = new List<object[]>
        {
            new object[] { ExpectedCommand_5  },
            new object[] { ExpectedCommand_6 },
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

    public static IEnumerable<object[]> GetMutationExceptionData()
    {
        var testData = new List<object[]>
        {
            new object[] { new JsonException(), LogEvents.Mutation_JSON_ErrorMsg  },
            new object[] { new HttpRequestException(), LogEvents.Mutation_HTTP_ErrorMsg },
            new object[] { new JSException("There was a JS error."), LogEvents.Mutation_JSInterop_ErrorMsg },
            new object[] { new ValidationException("There was a validation error."), LogEvents.Mutation_Validation_ErrorMsg },
            new object[] { new Exception(), LogEvents.Mutation_Unhandled_ErrorMsg }
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

[ViewModelHttpRefresh("http://test.carlton.com/")]
[MutationHttpRefresh("http://test.carlton.com/")]
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
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[HttpRefreshParameter("ClientID", Attributes.DataEndpointParameterType.ComponentParameter)]
[HttpRefreshParameter("UserID", Attributes.DataEndpointParameterType.ComponentParameter)]
public class HttpRefreshWithComponentParametersCaller
{
    public int ClientID { get; set; } = 5;
    public int UserID { get; set; } = 10;
}

[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[HttpRefreshParameter("ClientID", Attributes.DataEndpointParameterType.StateStoreParameter)]
[HttpRefreshParameter("UserID", Attributes.DataEndpointParameterType.StateStoreParameter)]
public class HttpRefreshWithStateParametersCaller
{
}


[ViewModelHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
[MutationHttpRefresh("http://test.carlton.com/clients/{ClientID}/users/{UserID}")]
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

