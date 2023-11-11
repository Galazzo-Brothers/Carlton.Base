namespace Carlton.Core.Utilities.PropertyAccessor;

public class PropertyAccessor<T>
{
    private readonly Func<T, object> _getter;

    public PropertyAccessor(PropertyInfo propertyInfo)
    {
        _getter = (Func<T, object>)Delegate.CreateDelegate(
            typeof(Func<T, object>),
            propertyInfo.GetGetMethod());
    }

    public object GetValue(T instance)
    {
        return _getter(instance);
    }
}