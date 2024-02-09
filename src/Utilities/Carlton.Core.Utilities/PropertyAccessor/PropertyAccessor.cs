namespace Carlton.Core.Utilities.PropertyAccessor;

public class PropertyAccessor<T>(PropertyInfo propertyInfo)
{
    private readonly Func<T, object> _getter = (Func<T, object>)Delegate.CreateDelegate(typeof(Func<T, object>), propertyInfo.GetGetMethod());

    public object GetValue(T instance)
        => _getter(instance);
}