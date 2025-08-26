using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Xml;

namespace System.Web.UI.WebControls;

internal class XmlHierarchyData : IHierarchyData, ICustomTypeDescriptor
{
	private class XmlHierarchyDataPropertyDescriptor : PropertyDescriptor
	{
		private string name;

		private XmlNode xmlNode;

		public override Type ComponentType => typeof(XmlHierarchyData);

		public override bool IsReadOnly => xmlNode.IsReadOnly;

		public override Type PropertyType => typeof(string);

		public XmlHierarchyDataPropertyDescriptor(XmlNode xmlNode, string name)
			: base(name, null)
		{
			this.xmlNode = xmlNode;
			this.name = name;
		}

		public override bool CanResetValue(object o)
		{
			return false;
		}

		public override void ResetValue(object o)
		{
		}

		public override object GetValue(object o)
		{
			if (o is XmlHierarchyData)
			{
				switch (name)
				{
				case "##Name##":
					return xmlNode.Name;
				case "##Value##":
					return xmlNode.Value;
				case "##InnerText##":
					return xmlNode.InnerText;
				case null:
					return string.Empty;
				}
				if (xmlNode.Attributes != null)
				{
					XmlAttribute xmlAttribute = xmlNode.Attributes[name];
					if (xmlAttribute != null)
					{
						return xmlAttribute.Value;
					}
				}
			}
			return string.Empty;
		}

		public override void SetValue(object o, object value)
		{
			if (!(o is XmlHierarchyData))
			{
				return;
			}
			switch (name)
			{
			case "##Value##":
				xmlNode.Value = value.ToString();
				return;
			case "##InnerText##":
				xmlNode.InnerText = value.ToString();
				return;
			case null:
				return;
			}
			if (xmlNode.Attributes != null)
			{
				XmlAttribute xmlAttribute = xmlNode.Attributes[name];
				if (xmlAttribute != null)
				{
					xmlAttribute.Value = value.ToString();
				}
			}
		}

		public override bool ShouldSerializeValue(object o)
		{
			return o is XmlNode;
		}
	}

	private XmlNode item;

	bool IHierarchyData.HasChildren => item.HasChildNodes;

	object IHierarchyData.Item => item;

	string IHierarchyData.Path
	{
		get
		{
			StringBuilder stringBuilder = new StringBuilder();
			XmlNode parentNode = item;
			do
			{
				int num = 1;
				XmlNode previousSibling = parentNode.PreviousSibling;
				while (previousSibling != null)
				{
					previousSibling = previousSibling.PreviousSibling;
					num++;
				}
				stringBuilder.Insert(0, "/*[position()=" + num + "]");
				parentNode = parentNode.ParentNode;
			}
			while (parentNode != null && !(parentNode is XmlDocument));
			return stringBuilder.ToString();
		}
	}

	string IHierarchyData.Type => item.Name;

	internal XmlHierarchyData(XmlNode item)
	{
		this.item = item;
	}

	public override string ToString()
	{
		return item.Name;
	}

	System.ComponentModel.AttributeCollection ICustomTypeDescriptor.GetAttributes()
	{
		return System.ComponentModel.AttributeCollection.Empty;
	}

	string ICustomTypeDescriptor.GetClassName()
	{
		return "XmlHierarchyData";
	}

	string ICustomTypeDescriptor.GetComponentName()
	{
		return null;
	}

	TypeConverter ICustomTypeDescriptor.GetConverter()
	{
		return null;
	}

	EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
	{
		return null;
	}

	PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
	{
		return new XmlHierarchyDataPropertyDescriptor(item, "##Name##");
	}

	object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
	{
		return null;
	}

	EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
	{
		return null;
	}

	EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attrs)
	{
		return null;
	}

	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
	{
		return ((ICustomTypeDescriptor)this).GetProperties((Attribute[])null);
	}

	PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attrFilter)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add(new XmlHierarchyDataPropertyDescriptor(item, "##Name##"));
		arrayList.Add(new XmlHierarchyDataPropertyDescriptor(item, "##Value##"));
		arrayList.Add(new XmlHierarchyDataPropertyDescriptor(item, "##InnerText##"));
		if (item.Attributes != null)
		{
			foreach (XmlAttribute attribute in item.Attributes)
			{
				arrayList.Add(new XmlHierarchyDataPropertyDescriptor(item, attribute.Name));
			}
		}
		return new PropertyDescriptorCollection((PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor)));
	}

	object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
	{
		if (pd is XmlHierarchyDataPropertyDescriptor)
		{
			return this;
		}
		return null;
	}

	IHierarchicalEnumerable IHierarchyData.GetChildren()
	{
		return new XmlHierarchicalEnumerable(item.ChildNodes);
	}

	IHierarchyData IHierarchyData.GetParent()
	{
		return new XmlHierarchyData(item.ParentNode);
	}
}
