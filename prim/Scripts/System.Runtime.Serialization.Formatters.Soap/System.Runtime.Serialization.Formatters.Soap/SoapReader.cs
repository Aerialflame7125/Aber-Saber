using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Threading;
using System.Xml;

namespace System.Runtime.Serialization.Formatters.Soap;

internal sealed class SoapReader
{
	private class TypeMetadata
	{
		public MemberInfo[] MemberInfos;

		public Hashtable Indices;
	}

	private SerializationBinder _binder;

	private SoapTypeMapper mapper;

	private ObjectManager objMgr;

	private StreamingContext _context;

	private long _nextAvailableId = long.MaxValue;

	private ISurrogateSelector _surrogateSelector;

	private XmlTextReader xmlReader;

	private Hashtable _fieldIndices;

	private long _topObjectId = 1L;

	private long NextAvailableId
	{
		get
		{
			_nextAvailableId--;
			return _nextAvailableId;
		}
	}

	public SoapTypeMapper Mapper => mapper;

	public XmlTextReader XmlReader => xmlReader;

	private object TopObject
	{
		get
		{
			objMgr.DoFixups();
			objMgr.RaiseDeserializationEvent();
			return objMgr.GetObject(_topObjectId);
		}
	}

	public SoapReader(SerializationBinder binder, ISurrogateSelector selector, StreamingContext context)
	{
		_binder = binder;
		objMgr = new ObjectManager(selector, context);
		_context = context;
		_surrogateSelector = selector;
		_fieldIndices = new Hashtable();
	}

	public object Deserialize(Stream inStream, ISoapMessage soapMessage)
	{
		CultureInfo currentCulture = CultureInfo.CurrentCulture;
		try
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Deserialize_inner(inStream, soapMessage);
		}
		finally
		{
			Thread.CurrentThread.CurrentCulture = currentCulture;
		}
		return TopObject;
	}

	private void Deserialize_inner(Stream inStream, ISoapMessage soapMessage)
	{
		ArrayList arrayList = null;
		xmlReader = new XmlTextReader(inStream);
		xmlReader.WhitespaceHandling = WhitespaceHandling.None;
		mapper = new SoapTypeMapper(_binder);
		try
		{
			xmlReader.MoveToContent();
			xmlReader.ReadStartElement();
			xmlReader.MoveToContent();
			while (xmlReader.NodeType != XmlNodeType.Element || !(xmlReader.LocalName == "Body") || !(xmlReader.NamespaceURI == SoapTypeMapper.SoapEnvelopeNamespace))
			{
				if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "Header" && xmlReader.NamespaceURI == SoapTypeMapper.SoapEnvelopeNamespace)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					DeserializeHeaders(arrayList);
				}
				else
				{
					xmlReader.Skip();
				}
				xmlReader.MoveToContent();
			}
			xmlReader.ReadStartElement();
			xmlReader.MoveToContent();
			if (soapMessage != null)
			{
				if (DeserializeMessage(soapMessage))
				{
					_topObjectId = NextAvailableId;
					RegisterObject(_topObjectId, soapMessage, null, 0L, null, null);
				}
				xmlReader.MoveToContent();
				if (arrayList != null)
				{
					soapMessage.Headers = (Header[])arrayList.ToArray(typeof(Header));
				}
			}
			while (xmlReader.NodeType != XmlNodeType.EndElement)
			{
				Deserialize();
			}
			xmlReader.ReadEndElement();
			xmlReader.MoveToContent();
			xmlReader.ReadEndElement();
		}
		finally
		{
			if (xmlReader != null)
			{
				xmlReader.Close();
			}
		}
	}

	private bool IsNull()
	{
		string text = xmlReader["null", "http://www.w3.org/2001/XMLSchema-instance"];
		if (text != null && !(text == string.Empty))
		{
			return true;
		}
		return false;
	}

	private long GetId()
	{
		string text = xmlReader["id"];
		if (text == null || text == string.Empty)
		{
			return 0L;
		}
		return Convert.ToInt64(text.Substring(4));
	}

	private long GetHref()
	{
		string text = xmlReader["href"];
		if (text == null || text == string.Empty)
		{
			return 0L;
		}
		return Convert.ToInt64(text.Substring(5));
	}

	private Type GetComponentType()
	{
		string text = xmlReader["type", "http://www.w3.org/2001/XMLSchema-instance"];
		if (text == null)
		{
			if (GetId() != 0L)
			{
				return typeof(string);
			}
			return null;
		}
		return GetTypeFromQName(text);
	}

	private bool DeserializeMessage(ISoapMessage message)
	{
		if (xmlReader.Name == SoapTypeMapper.SoapEnvelopePrefix + ":Fault")
		{
			Deserialize();
			return false;
		}
		SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlReader.NamespaceURI, out var _, out var _);
		message.MethodName = xmlReader.LocalName;
		message.XmlNameSpace = xmlReader.NamespaceURI;
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		long nextAvailableId = NextAvailableId;
		int[] array = new int[1];
		if (!xmlReader.IsEmptyElement)
		{
			int depth = xmlReader.Depth;
			xmlReader.Read();
			int num = 0;
			while (xmlReader.Depth > depth)
			{
				object obj = null;
				arrayList.Add(xmlReader.Name);
				Type componentType = null;
				if (message.ParamTypes != null)
				{
					if (num >= message.ParamTypes.Length)
					{
						throw new SerializationException("Not enough parameter types in SoapMessages");
					}
					componentType = message.ParamTypes[num];
				}
				array[0] = num;
				obj = DeserializeComponent(componentType, out var _, out var componentHref, nextAvailableId, null, array);
				array[0] = arrayList2.Add(obj);
				if (componentHref != 0L)
				{
					RecordFixup(nextAvailableId, componentHref, arrayList2.ToArray(), null, null, null, array);
				}
				num++;
			}
			xmlReader.ReadEndElement();
		}
		else
		{
			xmlReader.Read();
		}
		message.ParamNames = (string[])arrayList.ToArray(typeof(string));
		message.ParamValues = arrayList2.ToArray();
		RegisterObject(nextAvailableId, message.ParamValues, null, 0L, null, null);
		return true;
	}

	private void DeserializeHeaders(ArrayList headers)
	{
		xmlReader.ReadStartElement();
		xmlReader.MoveToContent();
		while (xmlReader.NodeType != XmlNodeType.EndElement)
		{
			if (xmlReader.NodeType != XmlNodeType.Element)
			{
				xmlReader.Skip();
				continue;
			}
			if (xmlReader.GetAttribute("root", SoapTypeMapper.SoapEncodingNamespace) == "1")
			{
				headers.Add(DeserializeHeader());
			}
			else
			{
				Deserialize();
			}
			xmlReader.MoveToContent();
		}
		xmlReader.ReadEndElement();
	}

	private Header DeserializeHeader()
	{
		Header header = new Header(xmlReader.LocalName, null);
		header.HeaderNamespace = xmlReader.NamespaceURI;
		header.MustUnderstand = xmlReader.GetAttribute("mustUnderstand", SoapTypeMapper.SoapEnvelopeNamespace) == "1";
		long nextAvailableId = NextAvailableId;
		FieldInfo field = typeof(Header).GetField("Value");
		long componentId;
		long componentHref;
		object obj = (header.Value = DeserializeComponent(null, out componentId, out componentHref, nextAvailableId, field, null));
		if (componentHref != 0L && obj == null)
		{
			RecordFixup(nextAvailableId, componentHref, header, null, null, field, null);
		}
		else if (obj != null && obj.GetType().IsValueType && componentId != 0L)
		{
			RecordFixup(nextAvailableId, componentId, header, null, null, field, null);
		}
		else if (componentId != 0L)
		{
			RegisterObject(componentId, obj, null, nextAvailableId, field, null);
		}
		RegisterObject(nextAvailableId, header, null, 0L, null, null);
		return header;
	}

	private object DeserializeArray(long id)
	{
		if (GetComponentType() == typeof(byte[]))
		{
			byte[] array = Convert.FromBase64String(xmlReader.ReadElementString());
			RegisterObject(id, array, null, 0L, null, null);
			return array;
		}
		string[] array2 = xmlReader["arrayType", SoapTypeMapper.SoapEncodingNamespace].Split(':');
		int num = array2[1].LastIndexOf('[');
		string xmlName = array2[1].Substring(0, num);
		string text = array2[1].Substring(num);
		string[] array3 = text.Substring(1, text.Length - 2).Trim().Split(',');
		int num2 = array3.Length;
		int[] array4 = new int[num2];
		for (int i = 0; i < num2; i++)
		{
			array4[i] = Convert.ToInt32(array3[i]);
		}
		int[] array5 = new int[num2];
		Array array6 = Array.CreateInstance(mapper.GetType(xmlName, xmlReader.LookupNamespace(array2[0])), array4);
		for (int j = 0; j < num2; j++)
		{
			array5[j] = array6.GetLowerBound(j);
		}
		int depth = xmlReader.Depth;
		xmlReader.Read();
		while (xmlReader.Depth > depth)
		{
			Type type = GetComponentType();
			if (type == null)
			{
				type = array6.GetType().GetElementType();
			}
			long componentId;
			long componentHref;
			object obj = DeserializeComponent(type, out componentId, out componentHref, id, null, array5);
			if (componentHref != 0L)
			{
				object @object = objMgr.GetObject(componentHref);
				if (@object != null)
				{
					array6.SetValue(@object, array5);
				}
				else
				{
					RecordFixup(id, componentHref, array6, null, null, null, array5);
				}
			}
			else if (obj != null && obj.GetType().IsValueType && componentId != 0L)
			{
				RecordFixup(id, componentId, array6, null, null, null, array5);
			}
			else if (componentId != 0L)
			{
				RegisterObject(componentId, obj, null, id, null, array5);
				array6.SetValue(obj, array5);
			}
			else
			{
				array6.SetValue(obj, array5);
			}
			for (int num3 = array6.Rank - 1; num3 >= 0; num3--)
			{
				array5[num3]++;
				if (array5[num3] <= array6.GetUpperBound(num3) || num3 <= 0)
				{
					break;
				}
				array5[num3] = array6.GetLowerBound(num3);
			}
		}
		RegisterObject(id, array6, null, 0L, null, null);
		xmlReader.ReadEndElement();
		return array6;
	}

	private object Deserialize()
	{
		object obj = null;
		Type type = mapper.GetType(xmlReader.LocalName, xmlReader.NamespaceURI);
		long id = GetId();
		id = ((id == 0L) ? 1 : id);
		if (type == typeof(Array))
		{
			return DeserializeArray(id);
		}
		return DeserializeObject(type, id, 0L, null, null);
	}

	private object DeserializeObject(Type type, long id, long parentId, MemberInfo parentMemberInfo, int[] indices)
	{
		SerializationInfo info = null;
		bool flag = false;
		if (type == typeof(string) || type == typeof(TimeSpan) || (mapper.IsInternalSoapType(type) && (indices != null || parentMemberInfo != null)))
		{
			object obj = mapper.ReadInternalSoapValue(this, type);
			if (id != 0L)
			{
				RegisterObject(id, obj, info, parentId, parentMemberInfo, indices);
			}
			return obj;
		}
		object uninitializedObject = FormatterServices.GetUninitializedObject(type);
		objMgr.RaiseOnDeserializingEvent(uninitializedObject);
		if (uninitializedObject is ISerializable)
		{
			flag = true;
		}
		if (_surrogateSelector != null && !flag)
		{
			ISurrogateSelector selector;
			ISerializationSurrogate surrogate = _surrogateSelector.GetSurrogate(type, _context, out selector);
			flag = flag || surrogate != null;
		}
		bool hasFixup;
		if (flag)
		{
			uninitializedObject = DeserializeISerializableObject(uninitializedObject, id, out info, out hasFixup);
		}
		else
		{
			uninitializedObject = DeserializeSimpleObject(uninitializedObject, id, out hasFixup);
			if (!hasFixup && uninitializedObject is IObjectReference)
			{
				uninitializedObject = ((IObjectReference)uninitializedObject).GetRealObject(_context);
			}
		}
		RegisterObject(id, uninitializedObject, info, parentId, parentMemberInfo, indices);
		xmlReader.ReadEndElement();
		return uninitializedObject;
	}

	private object DeserializeSimpleObject(object obj, long id, out bool hasFixup)
	{
		hasFixup = false;
		Type type = obj.GetType();
		TypeMetadata typeMetadata = GetTypeMetadata(type);
		object[] array = new object[typeMetadata.MemberInfos.Length];
		xmlReader.Read();
		xmlReader.MoveToContent();
		while (xmlReader.NodeType != XmlNodeType.EndElement)
		{
			if (xmlReader.NodeType != XmlNodeType.Element)
			{
				xmlReader.Skip();
				continue;
			}
			int num = (int)(typeMetadata.Indices[xmlReader.LocalName] ?? throw new SerializationException("Field \"" + xmlReader.LocalName + "\" not found in class " + type.FullName));
			FieldInfo fieldInfo = typeMetadata.MemberInfos[num] as FieldInfo;
			long componentId;
			long componentHref;
			object obj2 = (array[num] = DeserializeComponent(fieldInfo.FieldType, out componentId, out componentHref, id, fieldInfo, null));
			if (componentHref != 0L && obj2 == null)
			{
				RecordFixup(id, componentHref, obj, null, null, fieldInfo, null);
				hasFixup = true;
			}
			else if (obj2 != null && obj2.GetType().IsValueType && componentId != 0L)
			{
				RecordFixup(id, componentId, obj, null, null, fieldInfo, null);
				hasFixup = true;
			}
			else if (componentId != 0L)
			{
				RegisterObject(componentId, obj2, null, id, fieldInfo, null);
			}
		}
		FormatterServices.PopulateObjectMembers(obj, typeMetadata.MemberInfos, array);
		return obj;
	}

	private object DeserializeISerializableObject(object obj, long id, out SerializationInfo info, out bool hasFixup)
	{
		info = new SerializationInfo(obj.GetType(), new FormatterConverter());
		hasFixup = false;
		int depth = xmlReader.Depth;
		xmlReader.Read();
		while (xmlReader.Depth > depth)
		{
			Type componentType = GetComponentType();
			string text = XmlConvert.DecodeName(xmlReader.LocalName);
			long componentId;
			long componentHref;
			object obj2 = DeserializeComponent(componentType, out componentId, out componentHref, id, null, null);
			if (componentHref != 0L && obj2 == null)
			{
				RecordFixup(id, componentHref, obj, info, text, null, null);
				hasFixup = true;
				continue;
			}
			if (componentId != 0L && obj2.GetType().IsValueType)
			{
				RecordFixup(id, componentId, obj, info, text, null, null);
				hasFixup = true;
				continue;
			}
			if (componentId != 0L)
			{
				RegisterObject(componentId, obj2, null, id, null, null);
			}
			info.AddValue(text, obj2, (componentType != null) ? componentType : typeof(object));
		}
		return obj;
	}

	private object DeserializeComponent(Type componentType, out long componentId, out long componentHref, long parentId, MemberInfo parentMemberInfo, int[] indices)
	{
		componentId = 0L;
		componentHref = 0L;
		if (IsNull())
		{
			xmlReader.Read();
			return null;
		}
		Type componentType2 = GetComponentType();
		if (componentType2 != null)
		{
			componentType = componentType2;
		}
		if (xmlReader.HasAttributes)
		{
			componentId = GetId();
			componentHref = GetHref();
		}
		if (componentId != 0L)
		{
			string text = xmlReader.ReadElementString();
			objMgr.RegisterObject(text, componentId);
			return text;
		}
		if (componentHref != 0L)
		{
			xmlReader.Read();
			return objMgr.GetObject(componentHref);
		}
		if (componentType == null)
		{
			return xmlReader.ReadElementString();
		}
		componentId = NextAvailableId;
		return DeserializeObject(componentType, componentId, parentId, parentMemberInfo, indices);
	}

	public void RecordFixup(long parentObjectId, long childObjectId, object parentObject, SerializationInfo info, string fieldName, MemberInfo memberInfo, int[] indices)
	{
		if (info != null)
		{
			objMgr.RecordDelayedFixup(parentObjectId, fieldName, childObjectId);
		}
		else if (parentObject is Array)
		{
			if (indices.Length == 1)
			{
				objMgr.RecordArrayElementFixup(parentObjectId, indices[0], childObjectId);
			}
			else
			{
				objMgr.RecordArrayElementFixup(parentObjectId, (int[])indices.Clone(), childObjectId);
			}
		}
		else
		{
			objMgr.RecordFixup(parentObjectId, memberInfo, childObjectId);
		}
	}

	private void RegisterObject(long objectId, object objectInstance, SerializationInfo info, long parentObjectId, MemberInfo parentObjectMember, int[] indices)
	{
		if (parentObjectId == 0L)
		{
			indices = null;
		}
		if (!objectInstance.GetType().IsValueType || parentObjectId == 0L)
		{
			if (objMgr.GetObject(objectId) != objectInstance)
			{
				objMgr.RegisterObject(objectInstance, objectId, info, 0L, null, null);
			}
			return;
		}
		if (objMgr.GetObject(objectId) != null)
		{
			throw new SerializationException("Object already registered");
		}
		if (indices != null)
		{
			indices = (int[])indices.Clone();
		}
		objMgr.RegisterObject(objectInstance, objectId, info, parentObjectId, parentObjectMember, indices);
	}

	private TypeMetadata GetTypeMetadata(Type type)
	{
		if (_fieldIndices[type] is TypeMetadata result)
		{
			return result;
		}
		TypeMetadata typeMetadata = new TypeMetadata();
		typeMetadata.MemberInfos = FormatterServices.GetSerializableMembers(type, _context);
		typeMetadata.Indices = new Hashtable();
		for (int i = 0; i < typeMetadata.MemberInfos.Length; i++)
		{
			SoapFieldAttribute soapFieldAttribute = (SoapFieldAttribute)InternalRemotingServices.GetCachedSoapAttribute(typeMetadata.MemberInfos[i]);
			typeMetadata.Indices[XmlConvert.EncodeLocalName(soapFieldAttribute.XmlElementName)] = i;
		}
		_fieldIndices[type] = typeMetadata;
		return typeMetadata;
	}

	public Type GetTypeFromQName(string qname)
	{
		string[] array = qname.Split(':');
		string xmlNamespace = xmlReader.LookupNamespace(array[0]);
		return mapper.GetType(array[1], xmlNamespace);
	}
}
