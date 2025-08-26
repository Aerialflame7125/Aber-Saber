using System.Collections;

namespace System.Web.UI.WebControls;

internal class CollectionDataSource : IDataSource
{
	private static readonly string[] names = new string[0];

	private IEnumerable collection;

	public event EventHandler DataSourceChanged
	{
		add
		{
		}
		remove
		{
		}
	}

	public CollectionDataSource(IEnumerable collection)
	{
		this.collection = collection;
	}

	public DataSourceView GetView(string viewName)
	{
		return new CollectionDataSourceView(this, viewName, collection);
	}

	public ICollection GetViewNames()
	{
		return names;
	}
}
