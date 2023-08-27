namespace Carlton.Core.Components.Lab.State.Mutations.RefreshMutations;

public class SourceViewerRefreshMutation : IFluxStateMutation<LabState, SourceViewerViewModel>
{
    public bool IsRefreshMutation => true;
    public string StateEvent => "SelectComponentSourceRefresh";

    public LabState Mutate(LabState state, SourceViewerViewModel input)
    {
        return state with { SelectedComponentMarkup = input.ComponentSource };
    }
}
