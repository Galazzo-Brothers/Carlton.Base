namespace Carlton.Core.Utilities.Disposable;

public class CompositeDisposable : IDisposable
{
    private readonly List<IDisposable> _disposables = [];

    public CompositeDisposable() 
    {
    }

    public CompositeDisposable(params IDisposable[] disposables)
    {
        _disposables = new List<IDisposable>(disposables); 
    }

    public void Add(IDisposable disposable)
    {
        _disposables.Add(disposable);
    }

    public void Dispose()
    {
        foreach (var disposable in _disposables)
            disposable.Dispose();

        _disposables.Clear();
    }
}