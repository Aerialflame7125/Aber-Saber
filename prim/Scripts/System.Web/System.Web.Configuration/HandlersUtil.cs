using System.Configuration;
using System.Xml;

namespace System.Web.Configuration;

internal sealed class HandlersUtil
{
	private HandlersUtil()
	{
	}

	public static string ExtractAttributeValue(string attKey, XmlNode node)
	{
		return ExtractAttributeValue(attKey, node, optional: false);
	}

	public static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
	{
		return ExtractAttributeValue(attKey, node, optional, allowEmpty: false);
	}

	public static string ExtractAttributeValue(string attKey, XmlNode node, bool optional, bool allowEmpty)
	{
		if (node.Attributes == null)
		{
			if (optional)
			{
				return null;
			}
			ThrowException("Required attribute not found: " + attKey, node);
		}
		XmlNode xmlNode = node.Attributes.RemoveNamedItem(attKey);
		if (xmlNode == null)
		{
			if (optional)
			{
				return null;
			}
			ThrowException("Required attribute not found: " + attKey, node);
		}
		string value = xmlNode.Value;
		if (!allowEmpty && value == string.Empty)
		{
			ThrowException((optional ? "Optional" : "Required") + " attribute is empty: " + attKey, node);
		}
		return value;
	}

	public static void ThrowException(string msg, XmlNode node)
	{
		if (node != null && node.Name != string.Empty)
		{
			msg = msg + " (node name: " + node.Name + ") ";
		}
		throw new ConfigurationException(msg, node);
	}
}
