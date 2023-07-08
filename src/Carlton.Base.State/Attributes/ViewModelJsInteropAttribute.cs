namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelJsInteropAttribute : Attribute
{
    public string Module { get; init; }
    public string Function { get; init; }
    public object[] Parameters { get; init; }



    public ViewModelJsInteropAttribute(string module, string function, params object[] parameters)
    => (Module, Function, Parameters) = (module, function, parameters);
}
