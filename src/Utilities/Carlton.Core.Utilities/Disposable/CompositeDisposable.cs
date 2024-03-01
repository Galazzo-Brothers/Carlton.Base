namespace Carlton.Core.Utilities.Disposable;

/// <summary>
/// Represents a collection of disposable objects that are disposed together.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CompositeDisposable"/> class.
/// </remarks>
/// <param name="disposables">The disposable objects to add to the collection.</param>
public class CompositeDisposable(params IDisposable[] disposables) : IDisposable
{
    private readonly List<IDisposable> _disposables = new(disposables);

    /// <summary>
    /// Adds a disposable object to the collection.
    /// </summary>
    /// <param name="disposable">The disposable object to add.</param>  
    public void Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

    /// <summary>
    /// Disposes all disposable objects in the collection.
    /// </summary>
    public void Dispose()
    {
        foreach (var disposable in _disposables)
            disposable.Dispose();

        _disposables.Clear();

        GC.SuppressFinalize(this); // Suppress finalization for this object
    }
}