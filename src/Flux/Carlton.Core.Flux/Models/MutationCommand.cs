namespace Carlton.Core.Flux.Models;

public record class MutationCommand
{
    public Guid CommandID { get; } = Guid.NewGuid();
    public bool IsCompleted { get => Errored || Succeded; }
    public bool Errored { get; private set; }
    public bool Succeded { get; private set; }
    public bool WasHttpIntercepted { get; private set; }
    public bool IsValid { get; private set; }

    public bool RequiresNavigationChange { get; init; }
    public string NewRoute { get; init; }

    public MutationCommand()
    {
        RequiresNavigationChange = false;
        NewRoute = null;
    }

    public MutationCommand(bool requiresNavigationChange, string newRoute)
    {
        RequiresNavigationChange= requiresNavigationChange;
        NewRoute= newRoute;
    }
}

