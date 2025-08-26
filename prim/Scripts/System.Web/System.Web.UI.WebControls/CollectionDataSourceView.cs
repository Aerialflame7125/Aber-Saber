using System.Collections;

namespace System.Web.UI.WebControls;

internal class CollectionDataSourceView : DataSourceView
{
	private IEnumerable collection;

	public CollectionDataSourceView(IDataSource owner, string viewName, IEnumerable collection)
		: base(owner, viewName)
	{
		this.collection = collection;
	}

	protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
	{
		return collection;
	}
}
