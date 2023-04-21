namespace Carlton.Base.State;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RefreshViewModelAttribute : Attribute
{
    public string Route { get; init; } 

    public RefreshViewModelAttribute(string route)
    {
        Route = route;
    }
}
