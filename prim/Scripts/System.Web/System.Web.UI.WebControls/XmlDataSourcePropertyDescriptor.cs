using System.ComponentModel;
using System.Xml;

namespace System.Web.UI.WebControls;

internal class XmlDataSourcePropertyDescriptor : PropertyDescriptor
{
	private bool readOnly;

	public override Type ComponentType => typeof(XmlDataSourceNodeDescriptor);

	public override bool IsReadOnly => readOnly;

	public override Type PropertyType => typeof(string);

	public XmlDataSourcePropertyDescriptor(string name, bool readOnly)
		: base(name, null)
	{
		this.readOnly = readOnly;
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
		if (o is XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor && xmlDataSourceNodeDescriptor.Node.Attributes != null)
		{
			XmlAttribute xmlAttribute = xmlDataSourceNodeDescriptor.Node.Attributes[Name];
			if (xmlAttribute != null)
			{
				return xmlAttribute.Value;
			}
		}
		return string.Empty;
	}

	public override void SetValue(object o, object value)
	{
		if (o is XmlDataSourceNodeDescriptor xmlDataSourceNodeDescriptor && xmlDataSourceNodeDescriptor.Node.Attributes != null)
		{
			XmlAttribute xmlAttribute = xmlDataSourceNodeDescriptor.Node.Attributes[Name];
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
