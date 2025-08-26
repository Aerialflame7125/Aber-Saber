namespace System.Web.UI;

internal class ReadOnlyDataSourceView : HierarchicalDataSourceView
{
	private IHierarchicalEnumerable datasource;

	public ReadOnlyDataSourceView(IHierarchicalEnumerable datasource)
	{
		this.datasource = datasource;
	}

	public override IHierarchicalEnumerable Select()
	{
		return datasource;
	}
}
