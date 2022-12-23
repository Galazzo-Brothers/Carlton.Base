namespace Carlton.Base.TestBedFramework;

public record NavTreeViewModel
(
    IEnumerable<TreeItem<NavTreeItemModel>> TreeItems,
    TreeItem<NavTreeItemModel> SelectedNode,
    IEnumerable<TreeItem<NavTreeItemModel>> ExpandedNodes
);
