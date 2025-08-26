using System.Collections;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap;

internal class MethodSignature
{
	private Type[] types;

	public MethodSignature(Type[] types)
	{
		this.types = types;
	}

	public static object ReadXmlValue(SoapReader reader)
	{
		reader.XmlReader.MoveToElement();
		if (reader.XmlReader.IsEmptyElement)
		{
			reader.XmlReader.Skip();
			return new Type[0];
		}
		reader.XmlReader.ReadStartElement();
		string text = reader.XmlReader.ReadString();
		while (reader.XmlReader.NodeType != XmlNodeType.EndElement)
		{
			reader.XmlReader.Skip();
		}
		ArrayList arrayList = new ArrayList();
		string[] array = text.Split(' ');
		foreach (string text2 in array)
		{
			if (text2.Length != 0)
			{
				arrayList.Add(reader.GetTypeFromQName(text2));
			}
		}
		reader.XmlReader.ReadEndElement();
		return (Type[])arrayList.ToArray(typeof(Type));
	}

	public string GetXmlValue(SoapWriter writer)
	{
		StringBuilder stringBuilder = new StringBuilder();
		Type[] array = types;
		foreach (Type type in array)
		{
			Element xmlElement = writer.Mapper.GetXmlElement(type);
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(' ');
			}
			string namespacePrefix = writer.GetNamespacePrefix(xmlElement);
			stringBuilder.Append(namespacePrefix).Append(':').Append(xmlElement.LocalName);
		}
		return stringBuilder.ToString();
	}
}
