using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap;

internal class SoapTypeMapper
{
	private static Hashtable xmlNodeToTypeTable;

	private static Hashtable typeToXmlNodeTable;

	public static readonly string SoapEncodingNamespace;

	public static readonly string SoapEncodingPrefix;

	public static readonly string SoapEnvelopeNamespace;

	public static readonly string SoapEnvelopePrefix;

	private XmlTextWriter _xmlWriter;

	private long _prefixNumber;

	private Hashtable namespaceToPrefixTable = new Hashtable();

	private SerializationBinder _binder;

	private static ArrayList _canBeValueTypeList;

	private FormatterAssemblyStyle _assemblyFormat = FormatterAssemblyStyle.Full;

	private Element elementString;

	public SoapTypeMapper(SerializationBinder binder)
	{
		_binder = binder;
	}

	public SoapTypeMapper(XmlTextWriter xmlWriter, FormatterAssemblyStyle assemblyFormat, FormatterTypeStyle typeFormat)
	{
		_xmlWriter = xmlWriter;
		_assemblyFormat = assemblyFormat;
		_prefixNumber = 1L;
		if (typeFormat == FormatterTypeStyle.XsdString)
		{
			elementString = new Element("xsd", "string", "http://www.w3.org/2001/XMLSchema");
		}
		else
		{
			elementString = new Element(SoapEncodingPrefix, "string", SoapEncodingNamespace);
		}
	}

	static SoapTypeMapper()
	{
		xmlNodeToTypeTable = new Hashtable();
		typeToXmlNodeTable = new Hashtable();
		SoapEncodingNamespace = "http://schemas.xmlsoap.org/soap/encoding/";
		SoapEncodingPrefix = "SOAP-ENC";
		SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
		SoapEnvelopePrefix = "SOAP-ENV";
		_canBeValueTypeList = new ArrayList();
		_canBeValueTypeList.Add(typeof(DateTime).ToString());
		_canBeValueTypeList.Add(typeof(TimeSpan).ToString());
		_canBeValueTypeList.Add(typeof(string).ToString());
		_canBeValueTypeList.Add(typeof(decimal).ToString());
		_canBeValueTypeList.Sort();
		InitMappingTables();
	}

	private static string GetKey(string localName, string namespaceUri)
	{
		return localName + " " + namespaceUri;
	}

	public Type GetType(string xmlName, string xmlNamespace)
	{
		Type type = null;
		string text = XmlConvert.DecodeName(xmlName);
		string arg = XmlConvert.DecodeName(xmlNamespace);
		SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlNamespace, out var typeNamespace, out var assemblyName);
		string text2 = ((typeNamespace == null || typeNamespace == string.Empty) ? text : (typeNamespace + Type.Delimiter + text));
		if (assemblyName != null && assemblyName != string.Empty && _binder != null)
		{
			type = _binder.BindToType(assemblyName, text2);
		}
		if (type == null)
		{
			string text3 = (string)xmlNodeToTypeTable[GetKey(xmlName, xmlNamespace)];
			if (text3 != null)
			{
				type = Type.GetType(text3);
			}
			else
			{
				type = Type.GetType(text2);
				if (type == null)
				{
					if (assemblyName == null || assemblyName == string.Empty)
					{
						throw new SerializationException($"Parse Error, no assembly associated with XML key {text} {arg}");
					}
					type = FormatterServices.GetTypeFromAssembly(Assembly.Load(assemblyName), text2);
				}
			}
			if (type == null)
			{
				throw new SerializationException();
			}
		}
		return type;
	}

	public Element GetXmlElement(string typeFullName, string assemblyName)
	{
		string text = string.Empty;
		string text2 = typeFullName;
		if (_assemblyFormat == FormatterAssemblyStyle.Simple)
		{
			assemblyName = assemblyName.Split(',')[0];
		}
		string key = typeFullName + ", " + assemblyName;
		Element element = (Element)typeToXmlNodeTable[key];
		if (element == null)
		{
			int num = typeFullName.LastIndexOf('.');
			if (num != -1)
			{
				text = typeFullName.Substring(0, num);
				text2 = typeFullName.Substring(text.Length + 1);
			}
			string text3 = SoapServices.CodeXmlNamespaceForClrTypeNamespace(text, (!assemblyName.StartsWith("mscorlib")) ? assemblyName : string.Empty);
			string text4 = (string)namespaceToPrefixTable[text3];
			if (text4 == null || text4 == string.Empty)
			{
				text4 = "a" + _prefixNumber++;
				namespaceToPrefixTable[text3] = text4;
			}
			int num2 = text2.IndexOf("[");
			if (num2 != -1)
			{
				text2 = XmlConvert.EncodeName(text2.Substring(0, num2)) + text2.Substring(num2);
			}
			else
			{
				int num3 = text2.IndexOf("&");
				text2 = ((num3 == -1) ? XmlConvert.EncodeName(text2) : (XmlConvert.EncodeName(text2.Substring(0, num3)) + text2.Substring(num3)));
			}
			element = new Element(text4, text2, text3);
		}
		return element;
	}

	public Element GetXmlElement(Type type)
	{
		if (type == typeof(string))
		{
			return elementString;
		}
		Element element = (Element)typeToXmlNodeTable[type.AssemblyQualifiedName];
		if (element == null)
		{
			element = GetXmlElement(type.FullName, type.Assembly.FullName);
		}
		else if (_xmlWriter != null)
		{
			element = new Element((element.Prefix == null) ? _xmlWriter.LookupPrefix(element.NamespaceURI) : element.Prefix, element.LocalName, element.NamespaceURI);
		}
		if (element == null)
		{
			throw new SerializationException("Oooops");
		}
		return element;
	}

	private static void RegisterType(Type type, string name, string namspace)
	{
		RegisterType(type, name, namspace, reverseMap: true);
	}

	private static Element RegisterType(Type type, string name, string namspace, bool reverseMap)
	{
		Element element = new Element(name, namspace);
		xmlNodeToTypeTable.Add(GetKey(name, namspace), type.AssemblyQualifiedName);
		if (reverseMap)
		{
			typeToXmlNodeTable.Add(type.AssemblyQualifiedName, element);
		}
		return element;
	}

	private static void RegisterType(Type type)
	{
		string name = (string)type.GetProperty("XsdType", BindingFlags.Static | BindingFlags.Public).GetValue(null, null);
		Element element = RegisterType(type, name, "http://www.w3.org/2001/XMLSchema", reverseMap: true);
		element.ParseMethod = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public);
		if (element.ParseMethod == null)
		{
			throw new InvalidOperationException("Parse method not found in class " + type);
		}
	}

	private static void InitMappingTables()
	{
		RegisterType(typeof(Array), "Array", SoapEncodingNamespace);
		RegisterType(typeof(string), "string", "http://www.w3.org/2001/XMLSchema", reverseMap: false);
		RegisterType(typeof(string), "string", SoapEncodingNamespace, reverseMap: false);
		RegisterType(typeof(bool), "boolean", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(sbyte), "byte", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(byte), "unsignedByte", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(long), "long", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(ulong), "unsignedLong", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(int), "int", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(uint), "unsignedInt", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(float), "float", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(double), "double", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(decimal), "decimal", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(short), "short", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(ushort), "unsignedShort", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(object), "anyType", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(DateTime), "dateTime", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(TimeSpan), "duration", "http://www.w3.org/2001/XMLSchema");
		RegisterType(typeof(SoapFault), "Fault", SoapEnvelopeNamespace);
		RegisterType(typeof(byte[]), "base64", SoapEncodingNamespace);
		RegisterType(typeof(MethodSignature), "methodSignature", SoapEncodingNamespace);
		RegisterType(typeof(SoapAnyUri));
		RegisterType(typeof(SoapEntity));
		RegisterType(typeof(SoapMonth));
		RegisterType(typeof(SoapNonNegativeInteger));
		RegisterType(typeof(SoapToken));
		RegisterType(typeof(SoapBase64Binary));
		RegisterType(typeof(SoapHexBinary));
		RegisterType(typeof(SoapMonthDay));
		RegisterType(typeof(SoapNonPositiveInteger));
		RegisterType(typeof(SoapYear));
		RegisterType(typeof(SoapDate));
		RegisterType(typeof(SoapId));
		RegisterType(typeof(SoapName));
		RegisterType(typeof(SoapNormalizedString));
		RegisterType(typeof(SoapYearMonth));
		RegisterType(typeof(SoapIdref));
		RegisterType(typeof(SoapNcName));
		RegisterType(typeof(SoapNotation));
		RegisterType(typeof(SoapDay));
		RegisterType(typeof(SoapIdrefs));
		RegisterType(typeof(SoapNegativeInteger));
		RegisterType(typeof(SoapPositiveInteger));
		RegisterType(typeof(SoapInteger));
		RegisterType(typeof(SoapNmtoken));
		RegisterType(typeof(SoapQName));
		RegisterType(typeof(SoapEntities));
		RegisterType(typeof(SoapLanguage));
		RegisterType(typeof(SoapNmtokens));
		RegisterType(typeof(SoapTime));
	}

	public static string GetXsdValue(object value)
	{
		if (value is DateTime)
		{
			return SoapDateTime.ToString((DateTime)value);
		}
		if (value is decimal num)
		{
			return num.ToString(CultureInfo.InvariantCulture);
		}
		if (value is double num2)
		{
			return num2.ToString("G17", CultureInfo.InvariantCulture);
		}
		if (value is float num3)
		{
			return num3.ToString("G9", CultureInfo.InvariantCulture);
		}
		if (value is TimeSpan)
		{
			return SoapDuration.ToString((TimeSpan)value);
		}
		if (value is bool)
		{
			if (!(bool)value)
			{
				return "false";
			}
			return "true";
		}
		if (value is MethodSignature)
		{
			return null;
		}
		return value.ToString();
	}

	public static object ParseXsdValue(string value, Type type)
	{
		if (type == typeof(DateTime))
		{
			return SoapDateTime.Parse(value);
		}
		if (type == typeof(decimal))
		{
			return decimal.Parse(value, CultureInfo.InvariantCulture);
		}
		if (type == typeof(double))
		{
			return double.Parse(value, CultureInfo.InvariantCulture);
		}
		if (type == typeof(float))
		{
			return float.Parse(value, CultureInfo.InvariantCulture);
		}
		if (type == typeof(TimeSpan))
		{
			return SoapDuration.Parse(value);
		}
		if (type.IsEnum)
		{
			return Enum.Parse(type, value);
		}
		return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
	}

	public static bool CanBeValue(Type type)
	{
		if (type.IsPrimitive)
		{
			return true;
		}
		if (type.IsEnum)
		{
			return true;
		}
		if (_canBeValueTypeList.BinarySearch(type.ToString()) >= 0)
		{
			return true;
		}
		return false;
	}

	public bool IsInternalSoapType(Type type)
	{
		if (CanBeValue(type))
		{
			return true;
		}
		if (typeof(ISoapXsd).IsAssignableFrom(type))
		{
			return true;
		}
		if (type == typeof(MethodSignature))
		{
			return true;
		}
		return false;
	}

	public object ReadInternalSoapValue(SoapReader reader, Type type)
	{
		if (CanBeValue(type))
		{
			return ParseXsdValue(reader.XmlReader.ReadElementString(), type);
		}
		if (type == typeof(MethodSignature))
		{
			return MethodSignature.ReadXmlValue(reader);
		}
		string text = reader.XmlReader.ReadElementString();
		Element xmlElement = GetXmlElement(type);
		if (xmlElement.ParseMethod != null)
		{
			return xmlElement.ParseMethod.Invoke(null, new object[1] { text });
		}
		throw new SerializationException("Can't parse type " + type);
	}

	public string GetInternalSoapValue(SoapWriter writer, object value)
	{
		if (CanBeValue(value.GetType()))
		{
			return GetXsdValue(value);
		}
		if (value is MethodSignature)
		{
			return ((MethodSignature)value).GetXmlValue(writer);
		}
		return value.ToString();
	}
}
