namespace Carlton.Core.Components.Tree;

public static class TreeItemEnumerableExtensions
{
	public static TreeItem<T> GetFirstSelectableTestState<T>(this IEnumerable<TreeItem<T>> treeItems)
	{
		var item = treeItems.First();

		while (item.Children.Any())
		{
			item = item.Children.First();
		}

		return item;
	}

	public static TreeItem<T> GetLeafById<T>(this IEnumerable<TreeItem<T>> treeItems, int leafId)
	{
		foreach (var item in treeItems)
		{
			if (item.LeafId == leafId)
				return item;

			var leaf = item.Children.Where(x => !x.IsParentNode)
									.FirstOrDefault(x => x.LeafId == leafId);
			var foundLeaf = leaf != null;

			if (foundLeaf)
			{
				return leaf;
			}
			else
			{
				leaf = item.Children.Where(x => x.IsParentNode).GetLeafById(leafId);
				if (leaf != null)
					return leaf;
			}
		}

		return null;
	}
}


