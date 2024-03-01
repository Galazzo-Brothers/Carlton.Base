using System.Reflection;
namespace Carlton.Core.Utilities.PropertyAccessor;

/// <summary>
/// Represents a property accessor for accessing the value of a property.
/// </summary>
/// <typeparam name="T">The type of the object containing the property.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="PropertyAccessor{T}"/> class with the specified property.
/// </remarks>
/// <param name="propertyInfo">The <see cref="PropertyInfo"/> object representing the property to access.</param>
public class PropertyAccessor<T>(PropertyInfo propertyInfo)
{
    private readonly Func<T, object> _getter = (Func<T, object>)Delegate.CreateDelegate(typeof(Func<T, object>), propertyInfo.GetGetMethod());

    /// <summary>
    /// Gets the value of the property for the specified instance.
    /// </summary>
    /// <param name="instance">The instance of the object containing the property.</param>
    /// <returns>The value of the property.</returns>
    public object GetValue(T instance)
        => _getter(instance);
}