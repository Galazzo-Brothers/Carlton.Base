namespace Carlton.Core.Utilities.Logging;

internal class LogScope(object state, Action disposeAct) : IDisposable
{
    public object State { get; } = state;
    private readonly Action _disposeAct = disposeAct;

    public void Dispose()
    {
        //Remove the scope for the list of scopes
        //as it is being disposed
        _disposeAct();
    }

    public override string ToString() => State.ToString();
}