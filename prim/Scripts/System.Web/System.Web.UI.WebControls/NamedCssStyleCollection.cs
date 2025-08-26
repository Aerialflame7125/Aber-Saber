namespace System.Web.UI.WebControls;

internal sealed class NamedCssStyleCollection
{
	private CssStyleCollection collection;

	public CssStyleCollection Collection
	{
		get
		{
			if (collection == null)
			{
				collection = new CssStyleCollection();
			}
			return collection;
		}
	}

	public string Name { get; private set; }

	public NamedCssStyleCollection(string name)
	{
		if (name == null)
		{
			name = string.Empty;
		}
		Name = name;
	}

	public NamedCssStyleCollection CopyFrom(CssStyleCollection coll)
	{
		if (coll == null)
		{
			return this;
		}
		CssStyleCollection cssStyleCollection = Collection;
		foreach (string key in coll.Keys)
		{
			cssStyleCollection.Add(key, coll[key]);
		}
		return this;
	}

	public NamedCssStyleCollection Add(HtmlTextWriterStyle key, string value)
	{
		Collection.Add(key, value);
		return this;
	}

	public NamedCssStyleCollection Add(string key, string value)
	{
		Collection.Add(key, value);
		return this;
	}

	public NamedCssStyleCollection Add(Style style)
	{
		if (style != null)
		{
			CopyFrom(style.GetStyleAttributes(null));
		}
		return this;
	}
}
