using System.Collections;
using System.Xml;

namespace System.Web.UI.WebControls;

internal class XmlHierarchicalEnumerable : IHierarchicalEnumerable, IEnumerable
{
	private XmlNodeList nodeList;

	internal XmlHierarchicalEnumerable(XmlNodeList nodeList)
	{
		this.nodeList = nodeList;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		ArrayList arrayList = new ArrayList(nodeList.Count);
		foreach (XmlNode node in nodeList)
		{
			if (node.NodeType == XmlNodeType.Element)
			{
				arrayList.Add(new XmlHierarchyData(node));
			}
		}
		return arrayList.GetEnumerator();
	}

	IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
	{
		return (IHierarchyData)enumeratedItem;
	}
}
