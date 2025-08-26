using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI.WebControls;

internal class XmlDataSourceNodeDescriptor : ICustomTypeDescriptor, IXPathNavigable
{
	private XmlNode node;

	public XmlNode Node => node;

	public XmlDataSourceNodeDescriptor(XmlNode node)
	{
		this.node = node;
	}

	public System.ComponentModel.AttributeCollection GetAttributes()
	{
		return System.ComponentModel.AttributeCollection.Empty;
	}

	public string GetClassName()
	{
		return "XmlDataSourceNodeDescriptor";
	}

	public string GetComponentName()
	{
		return null;
	}

	public TypeConverter GetConverter()
	{
		return null;
	}

	public EventDescriptor GetDefaultEvent()
	{
		return null;
	}

	public PropertyDescriptor GetDefaultProperty()
	{
		return null;
	}

	public object GetEditor(Type editorBaseType)
	{
		return null;
	}

	public EventDescriptorCollection GetEvents()
	{
		return null;
	}

	public EventDescriptorCollection GetEvents(Attribute[] arr)
	{
		return null;
	}

	public PropertyDescriptorCollection GetProperties()
	{
		if (node.Attributes != null)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[node.Attributes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new XmlDataSourcePropertyDescriptor(node.Attributes[i].Name, node.IsReadOnly);
			}
			return new PropertyDescriptorCollection(array);
		}
		return PropertyDescriptorCollection.Empty;
	}

	public PropertyDescriptorCollection GetProperties(Attribute[] arr)
	{
		return GetProperties();
	}

	public object GetPropertyOwner(PropertyDescriptor pd)
	{
		if (pd is XmlDataSourcePropertyDescriptor)
		{
			return this;
		}
		return null;
	}

	public XPathNavigator CreateNavigator()
	{
		return node.CreateNavigator();
	}
}
