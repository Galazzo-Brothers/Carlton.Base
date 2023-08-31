using Carlton.Core.Components.Flux.Models;

namespace Carlton.Core.Components.Flux.Test.Common;

public record TestViewModel1(int ID, string Name, string Description);
public record TestViewModel2(int ID, string SomeOtherProperty, int YetAnotherProperty);

public record TestCommand1 : MutationCommand
{
    public string Name { get; private set; }
    public string Description { get; private set; }


    public TestCommand1(int sourceSystemID, string name, string description) : base(sourceSystemID)
        => (Name, Description) = (name, description);
}

public record TestCommand2 : MutationCommand
{
    public int ID { get; private set; }
    public string Name { get; private set; }    
    public int SomeNumber { get; private set; }

    public TestCommand2(int id, string name, int someNumber) 
        => (ID, Name, SomeNumber) = (id, name, someNumber);
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

