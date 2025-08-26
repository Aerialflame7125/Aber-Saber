using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap;

internal class SoapWriter : IComparer
{
	private struct EnqueuedObject
	{
		public long _id;

		public object _object;

		public long Id => _id;

		public object Object => _object;

		public EnqueuedObject(object currentObject, long id)
		{
			_id = id;
			_object = currentObject;
		}
	}

	private XmlTextWriter _xmlWriter;

	private Queue _objectQueue = new Queue();

	private Hashtable _objectToIdTable = new Hashtable();

	private ISurrogateSelector _surrogateSelector;

	private SoapTypeMapper _mapper;

	private StreamingContext _context;

	private ObjectIDGenerator idGen = new ObjectIDGenerator();

	private FormatterAssemblyStyle _assemblyFormat = FormatterAssemblyStyle.Full;

	private FormatterTypeStyle _typeFormat;

	private static string defaultMessageNamespace;

	private SerializationObjectManager _manager;

	public SoapTypeMapper Mapper => _mapper;

	public XmlTextWriter XmlWriter => _xmlWriter;

	internal FormatterAssemblyStyle AssemblyFormat
	{
		get
		{
			return _assemblyFormat;
		}
		set
		{
			_assemblyFormat = value;
		}
	}

	internal FormatterTypeStyle TypeFormat
	{
		get
		{
			return _typeFormat;
		}
		set
		{
			_typeFormat = value;
		}
	}

	~SoapWriter()
	{
	}

	internal SoapWriter(Stream outStream, ISurrogateSelector selector, StreamingContext context, ISoapMessage soapMessage)
	{
		_xmlWriter = new XmlTextWriter(outStream, null);
		_xmlWriter.Formatting = Formatting.Indented;
		_surrogateSelector = selector;
		_context = context;
		_manager = new SerializationObjectManager(_context);
	}

	static SoapWriter()
	{
		defaultMessageNamespace = typeof(SoapWriter).Assembly.GetName().FullName;
	}

	private void Id(long id)
	{
		_xmlWriter.WriteAttributeString(null, "id", null, "ref-" + id);
	}

	private void Href(long href)
	{
		_xmlWriter.WriteAttributeString(null, "href", null, "#ref-" + href);
	}

	private void Null()
	{
		_xmlWriter.WriteAttributeString("xsi", "null", "http://www.w3.org/2001/XMLSchema-instance", "1");
	}

	private bool IsEncodingNeeded(object componentObject, Type componentType)
	{
		if (componentObject == null)
		{
			return false;
		}
		if (_typeFormat == FormatterTypeStyle.TypesAlways)
		{
			return true;
		}
		if (componentType == null)
		{
			componentType = componentObject.GetType();
			if (componentType.IsPrimitive || componentType == typeof(string))
			{
				return false;
			}
			return true;
		}
		if (componentType == typeof(object) || componentType != componentObject.GetType())
		{
			return true;
		}
		return false;
	}

	internal void Serialize(object objGraph, Header[] headers, FormatterTypeStyle typeFormat, FormatterAssemblyStyle assemblyFormat)
	{
		CultureInfo currentCulture = CultureInfo.CurrentCulture;
		try
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Serialize_inner(objGraph, headers, typeFormat, assemblyFormat);
		}
		finally
		{
			Thread.CurrentThread.CurrentCulture = currentCulture;
		}
		_manager.RaiseOnSerializedEvent();
	}

	private void Serialize_inner(object objGraph, Header[] headers, FormatterTypeStyle typeFormat, FormatterAssemblyStyle assemblyFormat)
	{
		_typeFormat = typeFormat;
		_assemblyFormat = assemblyFormat;
		_mapper = new SoapTypeMapper(_xmlWriter, assemblyFormat, typeFormat);
		_xmlWriter.WriteStartElement(SoapTypeMapper.SoapEnvelopePrefix, "Envelope", SoapTypeMapper.SoapEnvelopeNamespace);
		_xmlWriter.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
		_xmlWriter.WriteAttributeString("xmlns", "xsd", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema");
		_xmlWriter.WriteAttributeString("xmlns", SoapTypeMapper.SoapEncodingPrefix, "http://www.w3.org/2000/xmlns/", SoapTypeMapper.SoapEncodingNamespace);
		_xmlWriter.WriteAttributeString("xmlns", SoapTypeMapper.SoapEnvelopePrefix, "http://www.w3.org/2000/xmlns/", SoapTypeMapper.SoapEnvelopeNamespace);
		_xmlWriter.WriteAttributeString("xmlns", "clr", "http://www.w3.org/2000/xmlns/", SoapServices.XmlNsForClrType);
		_xmlWriter.WriteAttributeString(SoapTypeMapper.SoapEnvelopePrefix, "encodingStyle", SoapTypeMapper.SoapEnvelopeNamespace, "http://schemas.xmlsoap.org/soap/encoding/");
		ISoapMessage soapMessage = objGraph as ISoapMessage;
		if (soapMessage != null)
		{
			headers = soapMessage.Headers;
		}
		if (headers != null && headers.Length != 0)
		{
			_xmlWriter.WriteStartElement(SoapTypeMapper.SoapEnvelopePrefix, "Header", SoapTypeMapper.SoapEnvelopeNamespace);
			Header[] array = headers;
			foreach (Header header in array)
			{
				SerializeHeader(header);
			}
			WriteObjectQueue();
			_xmlWriter.WriteEndElement();
		}
		_xmlWriter.WriteStartElement(SoapTypeMapper.SoapEnvelopePrefix, "Body", SoapTypeMapper.SoapEnvelopeNamespace);
		bool firstTime = false;
		if (soapMessage != null)
		{
			SerializeMessage(soapMessage);
		}
		else
		{
			_objectQueue.Enqueue(new EnqueuedObject(objGraph, idGen.GetId(objGraph, out firstTime)));
		}
		WriteObjectQueue();
		_xmlWriter.WriteFullEndElement();
		_xmlWriter.WriteFullEndElement();
		_xmlWriter.Flush();
	}

	private void WriteObjectQueue()
	{
		while (_objectQueue.Count > 0)
		{
			EnqueuedObject enqueuedObject = (EnqueuedObject)_objectQueue.Dequeue();
			object @object = enqueuedObject.Object;
			Type type = @object.GetType();
			if (!type.IsValueType)
			{
				_objectToIdTable[@object] = enqueuedObject.Id;
			}
			if (type.IsArray)
			{
				SerializeArray((Array)@object, enqueuedObject.Id);
			}
			else
			{
				SerializeObject(@object, enqueuedObject.Id);
			}
		}
	}

	private void SerializeMessage(ISoapMessage message)
	{
		string ns = ((message.XmlNameSpace != null) ? message.XmlNameSpace : defaultMessageNamespace);
		_xmlWriter.WriteStartElement("i2", message.MethodName, ns);
		Id(idGen.GetId(message, out var _));
		string[] paramNames = message.ParamNames;
		object[] paramValues = message.ParamValues;
		int num = ((paramNames != null) ? paramNames.Length : 0);
		for (int i = 0; i < num; i++)
		{
			_xmlWriter.WriteStartElement(paramNames[i]);
			SerializeComponent(paramValues[i], specifyEncoding: true);
			_xmlWriter.WriteEndElement();
		}
		_xmlWriter.WriteFullEndElement();
	}

	private void SerializeHeader(Header header)
	{
		string ns = ((header.HeaderNamespace != null) ? header.HeaderNamespace : "http://schemas.microsoft.com/clr/soap");
		_xmlWriter.WriteStartElement("h4", header.Name, ns);
		if (header.MustUnderstand)
		{
			_xmlWriter.WriteAttributeString("mustUnderstand", SoapTypeMapper.SoapEnvelopeNamespace, "1");
		}
		_xmlWriter.WriteAttributeString("root", SoapTypeMapper.SoapEncodingNamespace, "1");
		if (header.Name == "__MethodSignature")
		{
			if (!(header.Value is Type[] types))
			{
				throw new SerializationException("Invalid method signature.");
			}
			SerializeComponent(new MethodSignature(types), specifyEncoding: true);
		}
		else
		{
			SerializeComponent(header.Value, specifyEncoding: true);
		}
		_xmlWriter.WriteEndElement();
	}

	private void SerializeObject(object currentObject, long currentObjectId)
	{
		bool flag = false;
		ISerializationSurrogate serializationSurrogate = null;
		if (_surrogateSelector != null)
		{
			serializationSurrogate = _surrogateSelector.GetSurrogate(currentObject.GetType(), _context, out var _);
		}
		if (currentObject is ISerializable || serializationSurrogate != null)
		{
			flag = true;
		}
		_manager.RegisterObject(currentObject);
		if (flag)
		{
			SerializeISerializableObject(currentObject, currentObjectId, serializationSurrogate);
			return;
		}
		if (!currentObject.GetType().IsSerializable)
		{
			throw new SerializationException($"Type {currentObject.GetType()} in assembly {currentObject.GetType().Assembly.FullName} is not marked as serializable.");
		}
		SerializeSimpleObject(currentObject, currentObjectId);
	}

	public int Compare(object x, object y)
	{
		MemberInfo obj = x as MemberInfo;
		MemberInfo memberInfo = y as MemberInfo;
		return string.Compare(obj.Name, memberInfo.Name);
	}

	private void SerializeSimpleObject(object currentObject, long currentObjectId)
	{
		Type type = currentObject.GetType();
		if (currentObjectId > 0)
		{
			Element xmlElement = _mapper.GetXmlElement(type);
			_xmlWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
			Id(currentObjectId);
		}
		if (type == typeof(TimeSpan))
		{
			_xmlWriter.WriteString(SoapTypeMapper.GetXsdValue(currentObject));
		}
		else if (type == typeof(string))
		{
			_xmlWriter.WriteString(currentObject.ToString());
		}
		else
		{
			MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(type, _context);
			object[] objectData = FormatterServices.GetObjectData(currentObject, serializableMembers);
			for (int i = 0; i < serializableMembers.Length; i++)
			{
				FieldInfo fieldInfo = (FieldInfo)serializableMembers[i];
				SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(fieldInfo);
				_xmlWriter.WriteStartElement(XmlConvert.EncodeLocalName(soapFieldAttribute.XmlElementName));
				SerializeComponent(objectData[i], IsEncodingNeeded(objectData[i], fieldInfo.FieldType));
				_xmlWriter.WriteEndElement();
			}
		}
		if (currentObjectId > 0)
		{
			_xmlWriter.WriteFullEndElement();
		}
	}

	private void SerializeISerializableObject(object currentObject, long currentObjectId, ISerializationSurrogate surrogate)
	{
		SerializationInfo serializationInfo = new SerializationInfo(currentObject.GetType(), new FormatterConverter());
		ISerializable serializable = currentObject as ISerializable;
		if (surrogate != null)
		{
			surrogate.GetObjectData(currentObject, serializationInfo, _context);
		}
		else
		{
			serializable.GetObjectData(serializationInfo, _context);
		}
		if (currentObjectId > 0)
		{
			Element xmlElement = _mapper.GetXmlElement(serializationInfo.FullTypeName, serializationInfo.AssemblyName);
			_xmlWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
			Id(currentObjectId);
		}
		SerializationInfoEnumerator enumerator = serializationInfo.GetEnumerator();
		while (enumerator.MoveNext())
		{
			SerializationEntry current = enumerator.Current;
			_xmlWriter.WriteStartElement(XmlConvert.EncodeLocalName(current.Name));
			SerializeComponent(current.Value, IsEncodingNeeded(current.Value, null));
			_xmlWriter.WriteEndElement();
		}
		if (currentObjectId > 0)
		{
			_xmlWriter.WriteFullEndElement();
		}
	}

	private void SerializeArray(Array currentArray, long currentArrayId)
	{
		Element xmlElement = _mapper.GetXmlElement(typeof(Array));
		Type elementType = currentArray.GetType().GetElementType();
		Element xmlElement2 = _mapper.GetXmlElement(elementType);
		_xmlWriter.WriteStartElement(xmlElement.Prefix, xmlElement.LocalName, xmlElement.NamespaceURI);
		if (currentArrayId > 0)
		{
			Id(currentArrayId);
		}
		if (elementType == typeof(byte))
		{
			EncodeType(currentArray.GetType());
			_xmlWriter.WriteString(Convert.ToBase64String((byte[])currentArray));
			_xmlWriter.WriteFullEndElement();
			return;
		}
		string namespacePrefix = GetNamespacePrefix(xmlElement2);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat("{0}:{1}[", namespacePrefix, xmlElement2.LocalName);
		for (int i = 0; i < currentArray.Rank; i++)
		{
			stringBuilder.AppendFormat("{0},", currentArray.GetUpperBound(i) + 1);
		}
		stringBuilder.Replace(',', ']', stringBuilder.Length - 1, 1);
		_xmlWriter.WriteAttributeString(SoapTypeMapper.SoapEncodingPrefix, "arrayType", SoapTypeMapper.SoapEncodingNamespace, stringBuilder.ToString());
		int num = 0;
		int num2 = 0;
		foreach (object item in currentArray)
		{
			if (item != null)
			{
				for (int j = num; j < num2; j++)
				{
					_xmlWriter.WriteStartElement("item");
					Null();
					_xmlWriter.WriteEndElement();
				}
				num = num2 + 1;
				_xmlWriter.WriteStartElement("item");
				SerializeComponent(item, IsEncodingNeeded(item, elementType));
				_xmlWriter.WriteEndElement();
			}
			num2++;
		}
		_xmlWriter.WriteFullEndElement();
	}

	private void SerializeComponent(object obj, bool specifyEncoding)
	{
		if (_typeFormat == FormatterTypeStyle.TypesAlways)
		{
			specifyEncoding = true;
		}
		if (obj == null)
		{
			Null();
			return;
		}
		Type type = obj.GetType();
		bool flag = _mapper.IsInternalSoapType(type);
		long num = 0L;
		if ((num = idGen.HasId(obj, out var firstTime)) != 0L)
		{
			Href(idGen.GetId(obj, out firstTime));
			return;
		}
		if (type == typeof(string) && _typeFormat != FormatterTypeStyle.XsdString)
		{
			num = idGen.GetId(obj, out firstTime);
			Id(num);
		}
		if (!flag && !type.IsValueType)
		{
			long id = idGen.GetId(obj, out firstTime);
			Href(id);
			_objectQueue.Enqueue(new EnqueuedObject(obj, id));
			return;
		}
		if (specifyEncoding)
		{
			EncodeType(type);
		}
		if (!flag && type.IsValueType)
		{
			SerializeObject(obj, 0L);
		}
		else
		{
			_xmlWriter.WriteString(_mapper.GetInternalSoapValue(this, obj));
		}
	}

	private void EncodeType(Type type)
	{
		if (type == null)
		{
			throw new SerializationException("Oooops");
		}
		Element xmlElement = _mapper.GetXmlElement(type);
		string namespacePrefix = GetNamespacePrefix(xmlElement);
		_xmlWriter.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", namespacePrefix + ":" + xmlElement.LocalName);
	}

	public string GetNamespacePrefix(Element xmlType)
	{
		string text = _xmlWriter.LookupPrefix(xmlType.NamespaceURI);
		if (text == null || text == string.Empty)
		{
			_xmlWriter.WriteAttributeString("xmlns", xmlType.Prefix, "http://www.w3.org/2000/xmlns/", xmlType.NamespaceURI);
			return xmlType.Prefix;
		}
		return text;
	}
}
