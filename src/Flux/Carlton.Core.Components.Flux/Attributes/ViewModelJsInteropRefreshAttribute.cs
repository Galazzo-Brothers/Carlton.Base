namespace Carlton.Core.Components.Flux;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ViewModelJsInteropRefreshAttribute : Attribute
{
    public string Module { get; init; }
    public string Function { get; init; }
    public object[] Parameters { get; init; }



    public ViewModelJsInteropRefreshAttribute(string module, string function, params object[] parameters)
    => (Module, Function, Parameters) = (module, function, parameters);
}
