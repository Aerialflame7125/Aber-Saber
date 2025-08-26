using System.Xml;

namespace System.Web.UI.WebControls;

/// <summary>Represents a data view on an XML node or collection of XML nodes for an <see cref="T:System.Web.UI.WebControls.XmlDataSource" /> control.</summary>
public class XmlHierarchicalDataSourceView : HierarchicalDataSourceView
{
	private XmlNodeList nodeList;

	internal XmlHierarchicalDataSourceView(XmlNodeList nodeList)
	{
		this.nodeList = nodeList;
	}

	/// <summary>Gets a list of the data items from the underlying data source.</summary>
	/// <returns>An <see cref="T:System.Web.UI.IHierarchicalEnumerable" /> collection of data items based on the hierarchical level of the current view.</returns>
	public override IHierarchicalEnumerable Select()
	{
		return new XmlHierarchicalEnumerable(nodeList);
	}
}
