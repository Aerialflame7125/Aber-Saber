using System.Reflection;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap;

internal class Element
{
	private string _prefix;

	private string _localName;

	private string _namespaceURI;

	private MethodInfo _parseMethod;

	public string Prefix => _prefix;

	public string LocalName => _localName;

	public string NamespaceURI => _namespaceURI;

	public MethodInfo ParseMethod
	{
		get
		{
			return _parseMethod;
		}
		set
		{
			_parseMethod = value;
		}
	}

	public Element(string prefix, string localName, string namespaceURI)
	{
		_prefix = prefix;
		_localName = localName;
		_namespaceURI = namespaceURI;
	}

	public Element(string localName, string namespaceURI)
		: this(null, localName, namespaceURI)
	{
	}

	public override bool Equals(object obj)
	{
		Element element = obj as Element;
		if (!(_localName == XmlConvert.DecodeName(element._localName)) || !(_namespaceURI == XmlConvert.DecodeName(element._namespaceURI)))
		{
			return false;
		}
		return true;
	}

	public override int GetHashCode()
	{
		return $"{XmlConvert.DecodeName(_localName)} {XmlConvert.DecodeName(_namespaceURI)}".GetHashCode();
	}

	public override string ToString()
	{
		return $"Element.Prefix = {Prefix}, Element.LocalName = {LocalName}, Element.NamespaceURI = {NamespaceURI}";
	}
}
