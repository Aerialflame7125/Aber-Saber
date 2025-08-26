using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls;

/// <summary>Represents a tabular data source view on XML data for an <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
public sealed class XmlDataSourceView : DataSourceView
{
	private ArrayList nodes;

	private XmlDataSource owner;

	/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> class, and associates the specified <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> with it.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> that the <see cref="T:System.Web.UI.WebControls.XmlDataSourceView" /> is associated with. </param>
	/// <param name="name">The name of the view. </param>
	public XmlDataSourceView(XmlDataSource owner, string name)
		: base(owner, name)
	{
		this.owner = owner;
	}

	/// <summary>Retrieves a list of data rows from the underlying XML.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that is used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of data items.</returns>
	public IEnumerable Select(DataSourceSelectArguments arguments)
	{
		return ExecuteSelect(arguments);
	}

	private void DoXPathSelect()
	{
		XmlNodeList xmlNodeList = owner.GetXmlDocument().SelectNodes((owner.XPath != "") ? owner.XPath : "/*/*");
		nodes = new ArrayList(xmlNodeList.Count);
		foreach (XmlNode item in xmlNodeList)
		{
			if (item.NodeType == XmlNodeType.Element)
			{
				nodes.Add(item);
			}
		}
	}

	protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
	{
		if (nodes == null)
		{
			DoXPathSelect();
		}
		ArrayList arrayList = new ArrayList();
		int num = arguments.StartRowIndex + ((arguments.MaximumRows > 0) ? arguments.MaximumRows : nodes.Count);
		if (num > nodes.Count)
		{
			num = nodes.Count;
		}
		for (int i = arguments.StartRowIndex; i < num; i++)
		{
			arrayList.Add(new XmlDataSourceNodeDescriptor((XmlElement)nodes[i]));
		}
		if (arguments.RetrieveTotalRowCount)
		{
			arguments.TotalRowCount = nodes.Count;
		}
		return arrayList;
	}
}
