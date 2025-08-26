using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class ServiceDescriptionSerializationReader : XmlSerializationReader
{
	private Hashtable _XmlSchemaDerivationMethodValues;

	private string id133_XmlSchemaSimpleTypeUnion;

	private string id143_maxInclusive;

	private string id46_body;

	private string id190_any;

	private string id88_OperationOutput;

	private string id6_targetNamespace;

	private string id158_XmlSchemaMaxLengthFacet;

	private string id11_portType;

	private string id182_mixed;

	private string id172_keyref;

	private string id187_all;

	private string id162_itemType;

	private string id68_InputBinding;

	private string id25_HttpAddressBinding;

	private string id82_HttpBinding;

	private string id17_address;

	private string id3_ServiceDescription;

	private string id38_SoapFaultBinding;

	private string id123_ref;

	private string id198_XmlSchemaComplexContent;

	private string id53_parts;

	private string id35_use;

	private string id157_XmlSchemaLengthFacet;

	private string id207_XmlSchemaImport;

	private string id44_text;

	private string id117_XmlSchemaAppInfo;

	private string id203_public;

	private string id69_urlEncoded;

	private string id7_documentation;

	private string id19_Item;

	private string id129_final;

	private string id163_XmlSchemaElement;

	private string id60_capture;

	private string id37_encodingStyle;

	private string id185_sequence;

	private string id166_abstract;

	private string id23_location;

	private string id111_XmlSchemaAttributeGroup;

	private string id192_XmlSchemaSequence;

	private string id33_FaultBinding;

	private string id153_XmlSchemaMaxInclusiveFacet;

	private string id201_XmlSchemaGroup;

	private string id43_multipartRelated;

	private string id168_nillable;

	private string id149_value;

	private string id64_MimeMultipartRelatedBinding;

	private string id193_XmlSchemaAny;

	private string id191_XmlSchemaGroupRef;

	private string id74_soapAction;

	private string id63_ignoreCase;

	private string id101_version;

	private string id47_header;

	private string id195_extension;

	private string id48_Soap12HeaderBinding;

	private string id134_memberTypes;

	private string id121_Item;

	private string id146_minExclusive;

	private string id84_PortType;

	private string id42_mimeXml;

	private string id138_minInclusive;

	private string id118_source;

	private string id73_Soap12OperationBinding;

	private string id131_restriction;

	private string id152_XmlSchemaMaxExclusiveFacet;

	private string id135_XmlSchemaSimpleTypeRestriction;

	private string id188_XmlSchemaAll;

	private string id116_appinfo;

	private string id86_parameterOrder;

	private string id147_minLength;

	private string id78_HttpOperationBinding;

	private string id161_XmlSchemaSimpleTypeList;

	private string id205_XmlSchemaRedefine;

	private string id194_XmlSchemaSimpleContent;

	private string id91_MessagePart;

	private string id92_element;

	private string id114_processContents;

	private string id18_Item;

	private string id50_headerfault;

	private string id154_XmlSchemaEnumerationFacet;

	private string id96_XmlSchema;

	private string id127_form;

	private string id176_field;

	private string id49_part;

	private string id5_Item;

	private string id57_match;

	private string id52_Soap12BodyBinding;

	private string id104_redefine;

	private string id20_Item;

	private string id21_Soap12AddressBinding;

	private string id142_enumeration;

	private string id24_SoapAddressBinding;

	private string id103_include;

	private string id139_maxLength;

	private string id165_maxOccurs;

	private string id65_MimePart;

	private string id102_id;

	private string id196_Item;

	private string id140_length;

	private string id27_type;

	private string id106_complexType;

	private string id31_output;

	private string id1_definitions;

	private string id4_name;

	private string id132_union;

	private string id29_OperationBinding;

	private string id170_key;

	private string id45_Item;

	private string id95_Item;

	private string id169_substitutionGroup;

	private string id178_xpath;

	private string id9_types;

	private string id97_attributeFormDefault;

	private string id62_pattern;

	private string id58_MimeTextMatch;

	private string id180_XmlSchemaKey;

	private string id10_message;

	private string id8_import;

	private string id148_XmlSchemaMinLengthFacet;

	private string id105_simpleType;

	private string id181_XmlSchemaComplexType;

	private string id164_minOccurs;

	private string id144_maxExclusive;

	private string id160_XmlSchemaFractionDigitsFacet;

	private string id124_XmlSchemaAttribute;

	private string id209_Import;

	private string id206_schemaLocation;

	private string id179_XmlSchemaUnique;

	private string id75_style;

	private string id119_XmlSchemaDocumentation;

	private string id136_base;

	private string id66_MimeXmlBinding;

	private string id30_input;

	private string id40_content;

	private string id93_Types;

	private string id94_schema;

	private string id200_Item;

	private string id67_MimeContentBinding;

	private string id59_group;

	private string id32_fault;

	private string id80_transport;

	private string id98_blockDefault;

	private string id13_service;

	private string id54_SoapHeaderBinding;

	private string id204_system;

	private string id16_Port;

	private string id108_notation;

	private string id186_choice;

	private string id110_attributeGroup;

	private string id79_Soap12Binding;

	private string id77_SoapOperationBinding;

	private string id115_XmlSchemaAnnotation;

	private string id83_verb;

	private string id72_HttpUrlEncodedBinding;

	private string id39_OutputBinding;

	private string id183_complexContent;

	private string id202_XmlSchemaNotation;

	private string id81_SoapBinding;

	private string id199_Item;

	private string id28_operation;

	private string id122_XmlSchemaAttributeGroupRef;

	private string id155_XmlSchemaPatternFacet;

	private string id76_soapActionRequired;

	private string id90_Message;

	private string id159_XmlSchemaMinInclusiveFacet;

	private string id208_XmlSchemaInclude;

	private string id85_Operation;

	private string id130_list;

	private string id14_Service;

	private string id22_required;

	private string id174_refer;

	private string id71_HttpUrlReplacementBinding;

	private string id56_MimeTextBinding;

	private string id87_OperationFault;

	private string id125_default;

	private string id15_port;

	private string id51_SoapHeaderFaultBinding;

	private string id128_XmlSchemaSimpleType;

	private string id36_namespace;

	private string id175_selector;

	private string id150_XmlSchemaMinExclusiveFacet;

	private string id100_elementFormDefault;

	private string id26_Binding;

	private string id197_Item;

	private string id126_fixed;

	private string id107_annotation;

	private string id99_finalDefault;

	private string id137_fractionDigits;

	private string id70_urlReplacement;

	private string id189_XmlSchemaChoice;

	private string id2_Item;

	private string id112_anyAttribute;

	private string id89_OperationInput;

	private string id141_totalDigits;

	private string id61_repeats;

	private string id184_simpleContent;

	private string id55_SoapBodyBinding;

	private string id145_whiteSpace;

	private string id167_block;

	private string id151_XmlSchemaWhiteSpaceFacet;

	private string id12_binding;

	private string id109_attribute;

	private string id171_unique;

	private string id120_lang;

	private string id173_XmlSchemaKeyref;

	private string id177_XmlSchemaXPath;

	private string id34_Soap12FaultBinding;

	private string id41_Item;

	private string id156_XmlSchemaTotalDigitsFacet;

	private string id113_XmlSchemaAnyAttribute;

	internal Hashtable XmlSchemaDerivationMethodValues
	{
		get
		{
			if (_XmlSchemaDerivationMethodValues == null)
			{
				Hashtable hashtable = new Hashtable();
				hashtable.Add("", 0L);
				hashtable.Add("substitution", 1L);
				hashtable.Add("extension", 2L);
				hashtable.Add("restriction", 4L);
				hashtable.Add("list", 8L);
				hashtable.Add("union", 16L);
				hashtable.Add("#all", 255L);
				_XmlSchemaDerivationMethodValues = hashtable;
			}
			return _XmlSchemaDerivationMethodValues;
		}
	}

	public object Read125_definitions()
	{
		object result = null;
		base.Reader.MoveToContent();
		if (base.Reader.NodeType == XmlNodeType.Element)
		{
			if ((object)base.Reader.LocalName != id1_definitions || (object)base.Reader.NamespaceURI != id2_Item)
			{
				throw CreateUnknownNodeException();
			}
			result = Read124_ServiceDescription(isNullable: true, checkType: true);
		}
		else
		{
			UnknownNode(null, "http://schemas.xmlsoap.org/wsdl/:definitions");
		}
		return result;
	}

	private ServiceDescription Read124_ServiceDescription(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id3_ServiceDescription || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		ServiceDescription serviceDescription = new ServiceDescription();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = serviceDescription.Extensions;
		ImportCollection imports = serviceDescription.Imports;
		MessageCollection messages = serviceDescription.Messages;
		PortTypeCollection portTypes = serviceDescription.PortTypes;
		BindingCollection bindings = serviceDescription.Bindings;
		ServiceCollection services = serviceDescription.Services;
		bool[] array2 = new bool[12];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				serviceDescription.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[11] && (object)base.Reader.LocalName == id6_targetNamespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				serviceDescription.TargetNamespace = base.Reader.Value;
				array2[11] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (serviceDescription.Namespaces == null)
				{
					serviceDescription.Namespaces = new XmlSerializerNamespaces();
				}
				serviceDescription.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		serviceDescription.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			serviceDescription.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return serviceDescription;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					serviceDescription.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id8_import && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (imports == null)
					{
						base.Reader.Skip();
					}
					else
					{
						imports.Add(Read4_Import(isNullable: false, checkType: true));
					}
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id9_types && (object)base.Reader.NamespaceURI == id2_Item)
				{
					serviceDescription.Types = Read67_Types(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else if ((object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (messages == null)
					{
						base.Reader.Skip();
					}
					else
					{
						messages.Add(Read69_Message(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id11_portType && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (portTypes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						portTypes.Add(Read75_PortType(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id12_binding && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (bindings == null)
					{
						base.Reader.Skip();
					}
					else
					{
						bindings.Add(Read117_Binding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id13_service && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (services == null)
					{
						base.Reader.Skip();
					}
					else
					{
						services.Add(Read123_Service(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(serviceDescription, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/:import, http://schemas.xmlsoap.org/wsdl/:types, http://schemas.xmlsoap.org/wsdl/:message, http://schemas.xmlsoap.org/wsdl/:portType, http://schemas.xmlsoap.org/wsdl/:binding, http://schemas.xmlsoap.org/wsdl/:service");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		serviceDescription.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return serviceDescription;
	}

	private Service Read123_Service(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id14_Service || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Service service = new Service();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = service.Extensions;
		PortCollection ports = service.Ports;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				service.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (service.Namespaces == null)
				{
					service.Namespaces = new XmlSerializerNamespaces();
				}
				service.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		service.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			service.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return service;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					service.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id15_port && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (ports == null)
					{
						base.Reader.Skip();
					}
					else
					{
						ports.Add(Read122_Port(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(service, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/:port");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		service.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return service;
	}

	private Port Read122_Port(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id16_Port || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Port port = new Port();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = port.Extensions;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				port.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id12_binding && (object)base.Reader.NamespaceURI == id5_Item)
			{
				port.Binding = ToXmlQualifiedName(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (port.Namespaces == null)
				{
					port.Namespaces = new XmlSerializerNamespaces();
				}
				port.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		port.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			port.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return port;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					port.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id17_address && (object)base.Reader.NamespaceURI == id18_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read118_HttpAddressBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id17_address && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read119_SoapAddressBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id17_address && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read121_Soap12AddressBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(port, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/http/:address, http://schemas.xmlsoap.org/wsdl/soap/:address, http://schemas.xmlsoap.org/wsdl/soap12/:address");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		port.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return port;
	}

	private Soap12AddressBinding Read121_Soap12AddressBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id21_Soap12AddressBinding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Soap12AddressBinding soap12AddressBinding = new Soap12AddressBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soap12AddressBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id23_location && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12AddressBinding.Location = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soap12AddressBinding, "http://schemas.xmlsoap.org/wsdl/:required, :location");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soap12AddressBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soap12AddressBinding, "");
			}
			else
			{
				UnknownNode(soap12AddressBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soap12AddressBinding;
	}

	private SoapAddressBinding Read119_SoapAddressBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id24_SoapAddressBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapAddressBinding soapAddressBinding = new SoapAddressBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapAddressBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id23_location && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapAddressBinding.Location = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapAddressBinding, "http://schemas.xmlsoap.org/wsdl/:required, :location");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapAddressBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapAddressBinding, "");
			}
			else
			{
				UnknownNode(soapAddressBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapAddressBinding;
	}

	private HttpAddressBinding Read118_HttpAddressBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id25_HttpAddressBinding || (object)xmlQualifiedName.Namespace != id18_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		HttpAddressBinding httpAddressBinding = new HttpAddressBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				httpAddressBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id23_location && (object)base.Reader.NamespaceURI == id5_Item)
			{
				httpAddressBinding.Location = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(httpAddressBinding, "http://schemas.xmlsoap.org/wsdl/:required, :location");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return httpAddressBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(httpAddressBinding, "");
			}
			else
			{
				UnknownNode(httpAddressBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return httpAddressBinding;
	}

	private Binding Read117_Binding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id26_Binding || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Binding binding = new Binding();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = binding.Extensions;
		OperationBindingCollection operations = binding.Operations;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				binding.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id27_type && (object)base.Reader.NamespaceURI == id5_Item)
			{
				binding.Type = ToXmlQualifiedName(base.Reader.Value);
				array2[6] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (binding.Namespaces == null)
				{
					binding.Namespaces = new XmlSerializerNamespaces();
				}
				binding.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		binding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			binding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return binding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					binding.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id12_binding && (object)base.Reader.NamespaceURI == id18_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read77_HttpBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id12_binding && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read80_SoapBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id12_binding && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read84_Soap12Binding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id28_operation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (operations == null)
					{
						base.Reader.Skip();
					}
					else
					{
						operations.Add(Read116_OperationBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(binding, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/http/:binding, http://schemas.xmlsoap.org/wsdl/soap/:binding, http://schemas.xmlsoap.org/wsdl/soap12/:binding, http://schemas.xmlsoap.org/wsdl/:operation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		binding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return binding;
	}

	private OperationBinding Read116_OperationBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id29_OperationBinding || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		OperationBinding operationBinding = new OperationBinding();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = operationBinding.Extensions;
		FaultBindingCollection faults = operationBinding.Faults;
		bool[] array2 = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationBinding.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (operationBinding.Namespaces == null)
				{
					operationBinding.Namespaces = new XmlSerializerNamespaces();
				}
				operationBinding.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		operationBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			operationBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return operationBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operationBinding.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id28_operation && (object)base.Reader.NamespaceURI == id18_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read85_HttpOperationBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id28_operation && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read86_SoapOperationBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id28_operation && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read88_Soap12OperationBinding(isNullable: false, checkType: true));
					}
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id30_input && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operationBinding.Input = Read110_InputBinding(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id31_output && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operationBinding.Output = Read111_OutputBinding(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else if ((object)base.Reader.LocalName == id32_fault && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (faults == null)
					{
						base.Reader.Skip();
					}
					else
					{
						faults.Add(Read115_FaultBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(operationBinding, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/http/:operation, http://schemas.xmlsoap.org/wsdl/soap/:operation, http://schemas.xmlsoap.org/wsdl/soap12/:operation, http://schemas.xmlsoap.org/wsdl/:input, http://schemas.xmlsoap.org/wsdl/:output, http://schemas.xmlsoap.org/wsdl/:fault");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		operationBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return operationBinding;
	}

	private FaultBinding Read115_FaultBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id33_FaultBinding || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		FaultBinding faultBinding = new FaultBinding();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = faultBinding.Extensions;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				faultBinding.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (faultBinding.Namespaces == null)
				{
					faultBinding.Namespaces = new XmlSerializerNamespaces();
				}
				faultBinding.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		faultBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			faultBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return faultBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					faultBinding.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id32_fault && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read112_SoapFaultBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id32_fault && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read114_Soap12FaultBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(faultBinding, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/soap/:fault, http://schemas.xmlsoap.org/wsdl/soap12/:fault");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		faultBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return faultBinding;
	}

	private Soap12FaultBinding Read114_Soap12FaultBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id34_Soap12FaultBinding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Soap12FaultBinding soap12FaultBinding = new Soap12FaultBinding();
		bool[] array = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soap12FaultBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12FaultBinding.Use = Read100_SoapBindingUse(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12FaultBinding.Name = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12FaultBinding.Namespace = base.Reader.Value;
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12FaultBinding.Encoding = base.Reader.Value;
				array[4] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soap12FaultBinding, "http://schemas.xmlsoap.org/wsdl/:required, :use, :name, :namespace, :encodingStyle");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soap12FaultBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soap12FaultBinding, "");
			}
			else
			{
				UnknownNode(soap12FaultBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soap12FaultBinding;
	}

	private SoapBindingUse Read100_SoapBindingUse(string s)
	{
		if (!(s == "encoded"))
		{
			if (s == "literal")
			{
				return SoapBindingUse.Literal;
			}
			throw CreateUnknownConstantException(s, typeof(SoapBindingUse));
		}
		return SoapBindingUse.Encoded;
	}

	private SoapFaultBinding Read112_SoapFaultBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id38_SoapFaultBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapFaultBinding soapFaultBinding = new SoapFaultBinding();
		bool[] array = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapFaultBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapFaultBinding.Use = Read98_SoapBindingUse(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapFaultBinding.Name = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapFaultBinding.Namespace = base.Reader.Value;
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapFaultBinding.Encoding = base.Reader.Value;
				array[4] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapFaultBinding, "http://schemas.xmlsoap.org/wsdl/:required, :use, :name, :namespace, :encodingStyle");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapFaultBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapFaultBinding, "");
			}
			else
			{
				UnknownNode(soapFaultBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapFaultBinding;
	}

	private SoapBindingUse Read98_SoapBindingUse(string s)
	{
		if (!(s == "encoded"))
		{
			if (s == "literal")
			{
				return SoapBindingUse.Literal;
			}
			throw CreateUnknownConstantException(s, typeof(SoapBindingUse));
		}
		return SoapBindingUse.Encoded;
	}

	private OutputBinding Read111_OutputBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id39_OutputBinding || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		OutputBinding outputBinding = new OutputBinding();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = outputBinding.Extensions;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				outputBinding.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (outputBinding.Namespaces == null)
				{
					outputBinding.Namespaces = new XmlSerializerNamespaces();
				}
				outputBinding.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		outputBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			outputBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return outputBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					outputBinding.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id40_content && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read93_MimeContentBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id42_mimeXml && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read94_MimeXmlBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id43_multipartRelated && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read104_MimeMultipartRelatedBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id44_text && (object)base.Reader.NamespaceURI == id45_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read97_MimeTextBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id46_body && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read99_SoapBodyBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id47_header && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read106_SoapHeaderBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id46_body && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read102_Soap12BodyBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id47_header && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read109_Soap12HeaderBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(outputBinding, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/mime/:content, http://schemas.xmlsoap.org/wsdl/mime/:mimeXml, http://schemas.xmlsoap.org/wsdl/mime/:multipartRelated, http://microsoft.com/wsdl/mime/textMatching/:text, http://schemas.xmlsoap.org/wsdl/soap/:body, http://schemas.xmlsoap.org/wsdl/soap/:header, http://schemas.xmlsoap.org/wsdl/soap12/:body, http://schemas.xmlsoap.org/wsdl/soap12/:header");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		outputBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return outputBinding;
	}

	private Soap12HeaderBinding Read109_Soap12HeaderBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id48_Soap12HeaderBinding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Soap12HeaderBinding soap12HeaderBinding = new Soap12HeaderBinding();
		bool[] array = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soap12HeaderBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12HeaderBinding.Message = ToXmlQualifiedName(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12HeaderBinding.Part = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12HeaderBinding.Use = Read100_SoapBindingUse(base.Reader.Value);
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12HeaderBinding.Encoding = base.Reader.Value;
				array[4] = true;
			}
			else if (!array[5] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12HeaderBinding.Namespace = base.Reader.Value;
				array[5] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soap12HeaderBinding, "http://schemas.xmlsoap.org/wsdl/:required, :message, :part, :use, :encodingStyle, :namespace");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soap12HeaderBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array[6] && (object)base.Reader.LocalName == id50_headerfault && (object)base.Reader.NamespaceURI == id20_Item)
				{
					soap12HeaderBinding.Fault = Read107_SoapHeaderFaultBinding(isNullable: false, checkType: true);
					array[6] = true;
				}
				else
				{
					UnknownNode(soap12HeaderBinding, "http://schemas.xmlsoap.org/wsdl/soap12/:headerfault");
				}
			}
			else
			{
				UnknownNode(soap12HeaderBinding, "http://schemas.xmlsoap.org/wsdl/soap12/:headerfault");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soap12HeaderBinding;
	}

	private SoapHeaderFaultBinding Read107_SoapHeaderFaultBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id51_SoapHeaderFaultBinding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapHeaderFaultBinding soapHeaderFaultBinding = new SoapHeaderFaultBinding();
		bool[] array = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapHeaderFaultBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Message = ToXmlQualifiedName(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Part = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Use = Read100_SoapBindingUse(base.Reader.Value);
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Encoding = base.Reader.Value;
				array[4] = true;
			}
			else if (!array[5] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Namespace = base.Reader.Value;
				array[5] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapHeaderFaultBinding, "http://schemas.xmlsoap.org/wsdl/:required, :message, :part, :use, :encodingStyle, :namespace");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapHeaderFaultBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapHeaderFaultBinding, "");
			}
			else
			{
				UnknownNode(soapHeaderFaultBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapHeaderFaultBinding;
	}

	private Soap12BodyBinding Read102_Soap12BodyBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id52_Soap12BodyBinding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Soap12BodyBinding soap12BodyBinding = new Soap12BodyBinding();
		bool[] array = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soap12BodyBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12BodyBinding.Use = Read100_SoapBindingUse(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12BodyBinding.Namespace = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12BodyBinding.Encoding = base.Reader.Value;
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id53_parts && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12BodyBinding.PartsString = base.Reader.Value;
				array[4] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soap12BodyBinding, "http://schemas.xmlsoap.org/wsdl/:required, :use, :namespace, :encodingStyle, :parts");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soap12BodyBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soap12BodyBinding, "");
			}
			else
			{
				UnknownNode(soap12BodyBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soap12BodyBinding;
	}

	private SoapHeaderBinding Read106_SoapHeaderBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id54_SoapHeaderBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapHeaderBinding soapHeaderBinding = new SoapHeaderBinding();
		bool[] array = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapHeaderBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderBinding.Message = ToXmlQualifiedName(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderBinding.Part = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderBinding.Use = Read98_SoapBindingUse(base.Reader.Value);
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderBinding.Encoding = base.Reader.Value;
				array[4] = true;
			}
			else if (!array[5] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderBinding.Namespace = base.Reader.Value;
				array[5] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapHeaderBinding, "http://schemas.xmlsoap.org/wsdl/:required, :message, :part, :use, :encodingStyle, :namespace");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapHeaderBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array[6] && (object)base.Reader.LocalName == id50_headerfault && (object)base.Reader.NamespaceURI == id19_Item)
				{
					soapHeaderBinding.Fault = Read105_SoapHeaderFaultBinding(isNullable: false, checkType: true);
					array[6] = true;
				}
				else
				{
					UnknownNode(soapHeaderBinding, "http://schemas.xmlsoap.org/wsdl/soap/:headerfault");
				}
			}
			else
			{
				UnknownNode(soapHeaderBinding, "http://schemas.xmlsoap.org/wsdl/soap/:headerfault");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapHeaderBinding;
	}

	private SoapHeaderFaultBinding Read105_SoapHeaderFaultBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id51_SoapHeaderFaultBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapHeaderFaultBinding soapHeaderFaultBinding = new SoapHeaderFaultBinding();
		bool[] array = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapHeaderFaultBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Message = ToXmlQualifiedName(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Part = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Use = Read98_SoapBindingUse(base.Reader.Value);
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Encoding = base.Reader.Value;
				array[4] = true;
			}
			else if (!array[5] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapHeaderFaultBinding.Namespace = base.Reader.Value;
				array[5] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapHeaderFaultBinding, "http://schemas.xmlsoap.org/wsdl/:required, :message, :part, :use, :encodingStyle, :namespace");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapHeaderFaultBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapHeaderFaultBinding, "");
			}
			else
			{
				UnknownNode(soapHeaderFaultBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapHeaderFaultBinding;
	}

	private SoapBodyBinding Read99_SoapBodyBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id55_SoapBodyBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapBodyBinding soapBodyBinding = new SoapBodyBinding();
		bool[] array = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapBodyBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapBodyBinding.Use = Read98_SoapBindingUse(base.Reader.Value);
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapBodyBinding.Namespace = base.Reader.Value;
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id37_encodingStyle && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapBodyBinding.Encoding = base.Reader.Value;
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id53_parts && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapBodyBinding.PartsString = base.Reader.Value;
				array[4] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapBodyBinding, "http://schemas.xmlsoap.org/wsdl/:required, :use, :namespace, :encodingStyle, :parts");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapBodyBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapBodyBinding, "");
			}
			else
			{
				UnknownNode(soapBodyBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapBodyBinding;
	}

	private MimeTextBinding Read97_MimeTextBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id56_MimeTextBinding || (object)xmlQualifiedName.Namespace != id45_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MimeTextBinding mimeTextBinding = new MimeTextBinding();
		MimeTextMatchCollection matches = mimeTextBinding.Matches;
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				mimeTextBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(mimeTextBinding, "http://schemas.xmlsoap.org/wsdl/:required");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return mimeTextBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id57_match && (object)base.Reader.NamespaceURI == id45_Item)
				{
					if (matches == null)
					{
						base.Reader.Skip();
					}
					else
					{
						matches.Add(Read96_MimeTextMatch(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(mimeTextBinding, "http://microsoft.com/wsdl/mime/textMatching/:match");
				}
			}
			else
			{
				UnknownNode(mimeTextBinding, "http://microsoft.com/wsdl/mime/textMatching/:match");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return mimeTextBinding;
	}

	private MimeTextMatch Read96_MimeTextMatch(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id58_MimeTextMatch || (object)xmlQualifiedName.Namespace != id45_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MimeTextMatch mimeTextMatch = new MimeTextMatch();
		MimeTextMatchCollection matches = mimeTextMatch.Matches;
		bool[] array = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.Name = base.Reader.Value;
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id27_type && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.Type = base.Reader.Value;
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.Group = XmlConvert.ToInt32(base.Reader.Value);
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id60_capture && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.Capture = XmlConvert.ToInt32(base.Reader.Value);
				array[3] = true;
			}
			else if (!array[4] && (object)base.Reader.LocalName == id61_repeats && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.RepeatsString = base.Reader.Value;
				array[4] = true;
			}
			else if (!array[5] && (object)base.Reader.LocalName == id62_pattern && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.Pattern = base.Reader.Value;
				array[5] = true;
			}
			else if (!array[6] && (object)base.Reader.LocalName == id63_ignoreCase && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeTextMatch.IgnoreCase = XmlConvert.ToBoolean(base.Reader.Value);
				array[6] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(mimeTextMatch, ":name, :type, :group, :capture, :repeats, :pattern, :ignoreCase");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return mimeTextMatch;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id57_match && (object)base.Reader.NamespaceURI == id45_Item)
				{
					if (matches == null)
					{
						base.Reader.Skip();
					}
					else
					{
						matches.Add(Read96_MimeTextMatch(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(mimeTextMatch, "http://microsoft.com/wsdl/mime/textMatching/:match");
				}
			}
			else
			{
				UnknownNode(mimeTextMatch, "http://microsoft.com/wsdl/mime/textMatching/:match");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return mimeTextMatch;
	}

	private MimeMultipartRelatedBinding Read104_MimeMultipartRelatedBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id64_MimeMultipartRelatedBinding || (object)xmlQualifiedName.Namespace != id41_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MimeMultipartRelatedBinding mimeMultipartRelatedBinding = new MimeMultipartRelatedBinding();
		MimePartCollection parts = mimeMultipartRelatedBinding.Parts;
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				mimeMultipartRelatedBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(mimeMultipartRelatedBinding, "http://schemas.xmlsoap.org/wsdl/:required");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return mimeMultipartRelatedBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (parts == null)
					{
						base.Reader.Skip();
					}
					else
					{
						parts.Add(Read103_MimePart(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(mimeMultipartRelatedBinding, "http://schemas.xmlsoap.org/wsdl/mime/:part");
				}
			}
			else
			{
				UnknownNode(mimeMultipartRelatedBinding, "http://schemas.xmlsoap.org/wsdl/mime/:part");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return mimeMultipartRelatedBinding;
	}

	private MimePart Read103_MimePart(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id65_MimePart || (object)xmlQualifiedName.Namespace != id41_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MimePart mimePart = new MimePart();
		ServiceDescriptionFormatExtensionCollection extensions = mimePart.Extensions;
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				mimePart.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(mimePart, "http://schemas.xmlsoap.org/wsdl/:required");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return mimePart;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id40_content && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read93_MimeContentBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id42_mimeXml && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read94_MimeXmlBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id44_text && (object)base.Reader.NamespaceURI == id45_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read97_MimeTextBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id46_body && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read99_SoapBodyBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id46_body && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read102_Soap12BodyBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(mimePart, "http://schemas.xmlsoap.org/wsdl/mime/:content, http://schemas.xmlsoap.org/wsdl/mime/:mimeXml, http://microsoft.com/wsdl/mime/textMatching/:text, http://schemas.xmlsoap.org/wsdl/soap/:body, http://schemas.xmlsoap.org/wsdl/soap12/:body");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return mimePart;
	}

	private MimeXmlBinding Read94_MimeXmlBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id66_MimeXmlBinding || (object)xmlQualifiedName.Namespace != id41_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MimeXmlBinding mimeXmlBinding = new MimeXmlBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				mimeXmlBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeXmlBinding.Part = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(mimeXmlBinding, "http://schemas.xmlsoap.org/wsdl/:required, :part");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return mimeXmlBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(mimeXmlBinding, "");
			}
			else
			{
				UnknownNode(mimeXmlBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return mimeXmlBinding;
	}

	private MimeContentBinding Read93_MimeContentBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id67_MimeContentBinding || (object)xmlQualifiedName.Namespace != id41_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MimeContentBinding mimeContentBinding = new MimeContentBinding();
		bool[] array = new bool[3];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				mimeContentBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeContentBinding.Part = base.Reader.Value;
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id27_type && (object)base.Reader.NamespaceURI == id5_Item)
			{
				mimeContentBinding.Type = base.Reader.Value;
				array[2] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(mimeContentBinding, "http://schemas.xmlsoap.org/wsdl/:required, :part, :type");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return mimeContentBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(mimeContentBinding, "");
			}
			else
			{
				UnknownNode(mimeContentBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return mimeContentBinding;
	}

	private InputBinding Read110_InputBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id68_InputBinding || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		InputBinding inputBinding = new InputBinding();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = inputBinding.Extensions;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				inputBinding.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (inputBinding.Namespaces == null)
				{
					inputBinding.Namespaces = new XmlSerializerNamespaces();
				}
				inputBinding.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		inputBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			inputBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return inputBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					inputBinding.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id69_urlEncoded && (object)base.Reader.NamespaceURI == id18_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read90_HttpUrlEncodedBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id70_urlReplacement && (object)base.Reader.NamespaceURI == id18_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read91_HttpUrlReplacementBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id40_content && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read93_MimeContentBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id42_mimeXml && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read94_MimeXmlBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id43_multipartRelated && (object)base.Reader.NamespaceURI == id41_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read104_MimeMultipartRelatedBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id44_text && (object)base.Reader.NamespaceURI == id45_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read97_MimeTextBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id46_body && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read99_SoapBodyBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id47_header && (object)base.Reader.NamespaceURI == id19_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read106_SoapHeaderBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id46_body && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read102_Soap12BodyBinding(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id47_header && (object)base.Reader.NamespaceURI == id20_Item)
				{
					if (extensions == null)
					{
						base.Reader.Skip();
					}
					else
					{
						extensions.Add(Read109_Soap12HeaderBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(inputBinding, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/http/:urlEncoded, http://schemas.xmlsoap.org/wsdl/http/:urlReplacement, http://schemas.xmlsoap.org/wsdl/mime/:content, http://schemas.xmlsoap.org/wsdl/mime/:mimeXml, http://schemas.xmlsoap.org/wsdl/mime/:multipartRelated, http://microsoft.com/wsdl/mime/textMatching/:text, http://schemas.xmlsoap.org/wsdl/soap/:body, http://schemas.xmlsoap.org/wsdl/soap/:header, http://schemas.xmlsoap.org/wsdl/soap12/:body, http://schemas.xmlsoap.org/wsdl/soap12/:header");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		inputBinding.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return inputBinding;
	}

	private HttpUrlReplacementBinding Read91_HttpUrlReplacementBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id71_HttpUrlReplacementBinding || (object)xmlQualifiedName.Namespace != id18_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		HttpUrlReplacementBinding httpUrlReplacementBinding = new HttpUrlReplacementBinding();
		bool[] array = new bool[1];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				httpUrlReplacementBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(httpUrlReplacementBinding, "http://schemas.xmlsoap.org/wsdl/:required");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return httpUrlReplacementBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(httpUrlReplacementBinding, "");
			}
			else
			{
				UnknownNode(httpUrlReplacementBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return httpUrlReplacementBinding;
	}

	private HttpUrlEncodedBinding Read90_HttpUrlEncodedBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id72_HttpUrlEncodedBinding || (object)xmlQualifiedName.Namespace != id18_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		HttpUrlEncodedBinding httpUrlEncodedBinding = new HttpUrlEncodedBinding();
		bool[] array = new bool[1];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				httpUrlEncodedBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(httpUrlEncodedBinding, "http://schemas.xmlsoap.org/wsdl/:required");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return httpUrlEncodedBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(httpUrlEncodedBinding, "");
			}
			else
			{
				UnknownNode(httpUrlEncodedBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return httpUrlEncodedBinding;
	}

	private Soap12OperationBinding Read88_Soap12OperationBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id73_Soap12OperationBinding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Soap12OperationBinding soap12OperationBinding = new Soap12OperationBinding();
		bool[] array = new bool[4];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soap12OperationBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id74_soapAction && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12OperationBinding.SoapAction = base.Reader.Value;
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id75_style && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12OperationBinding.Style = Read82_SoapBindingStyle(base.Reader.Value);
				array[2] = true;
			}
			else if (!array[3] && (object)base.Reader.LocalName == id76_soapActionRequired && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12OperationBinding.SoapActionRequired = XmlConvert.ToBoolean(base.Reader.Value);
				array[3] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soap12OperationBinding, "http://schemas.xmlsoap.org/wsdl/:required, :soapAction, :style, :soapActionRequired");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soap12OperationBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soap12OperationBinding, "");
			}
			else
			{
				UnknownNode(soap12OperationBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soap12OperationBinding;
	}

	private SoapBindingStyle Read82_SoapBindingStyle(string s)
	{
		if (!(s == "document"))
		{
			if (s == "rpc")
			{
				return SoapBindingStyle.Rpc;
			}
			throw CreateUnknownConstantException(s, typeof(SoapBindingStyle));
		}
		return SoapBindingStyle.Document;
	}

	private SoapOperationBinding Read86_SoapOperationBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id77_SoapOperationBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapOperationBinding soapOperationBinding = new SoapOperationBinding();
		bool[] array = new bool[3];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapOperationBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id74_soapAction && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapOperationBinding.SoapAction = base.Reader.Value;
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id75_style && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapOperationBinding.Style = Read79_SoapBindingStyle(base.Reader.Value);
				array[2] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapOperationBinding, "http://schemas.xmlsoap.org/wsdl/:required, :soapAction, :style");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapOperationBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapOperationBinding, "");
			}
			else
			{
				UnknownNode(soapOperationBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapOperationBinding;
	}

	private SoapBindingStyle Read79_SoapBindingStyle(string s)
	{
		if (!(s == "document"))
		{
			if (s == "rpc")
			{
				return SoapBindingStyle.Rpc;
			}
			throw CreateUnknownConstantException(s, typeof(SoapBindingStyle));
		}
		return SoapBindingStyle.Document;
	}

	private HttpOperationBinding Read85_HttpOperationBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id78_HttpOperationBinding || (object)xmlQualifiedName.Namespace != id18_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		HttpOperationBinding httpOperationBinding = new HttpOperationBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				httpOperationBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id23_location && (object)base.Reader.NamespaceURI == id5_Item)
			{
				httpOperationBinding.Location = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(httpOperationBinding, "http://schemas.xmlsoap.org/wsdl/:required, :location");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return httpOperationBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(httpOperationBinding, "");
			}
			else
			{
				UnknownNode(httpOperationBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return httpOperationBinding;
	}

	private Soap12Binding Read84_Soap12Binding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id79_Soap12Binding || (object)xmlQualifiedName.Namespace != id20_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Soap12Binding soap12Binding = new Soap12Binding();
		bool[] array = new bool[3];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soap12Binding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id80_transport && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12Binding.Transport = base.Reader.Value;
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id75_style && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soap12Binding.Style = Read82_SoapBindingStyle(base.Reader.Value);
				array[2] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soap12Binding, "http://schemas.xmlsoap.org/wsdl/:required, :transport, :style");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soap12Binding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soap12Binding, "");
			}
			else
			{
				UnknownNode(soap12Binding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soap12Binding;
	}

	private SoapBinding Read80_SoapBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id81_SoapBinding || (object)xmlQualifiedName.Namespace != id19_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapBinding soapBinding = new SoapBinding();
		bool[] array = new bool[3];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				soapBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id80_transport && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapBinding.Transport = base.Reader.Value;
				array[1] = true;
			}
			else if (!array[2] && (object)base.Reader.LocalName == id75_style && (object)base.Reader.NamespaceURI == id5_Item)
			{
				soapBinding.Style = Read79_SoapBindingStyle(base.Reader.Value);
				array[2] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapBinding, "http://schemas.xmlsoap.org/wsdl/:required, :transport, :style");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return soapBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(soapBinding, "");
			}
			else
			{
				UnknownNode(soapBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return soapBinding;
	}

	private HttpBinding Read77_HttpBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id82_HttpBinding || (object)xmlQualifiedName.Namespace != id18_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		HttpBinding httpBinding = new HttpBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id22_required && (object)base.Reader.NamespaceURI == id2_Item)
			{
				httpBinding.Required = XmlConvert.ToBoolean(base.Reader.Value);
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id83_verb && (object)base.Reader.NamespaceURI == id5_Item)
			{
				httpBinding.Verb = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(httpBinding, "http://schemas.xmlsoap.org/wsdl/:required, :verb");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return httpBinding;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(httpBinding, "");
			}
			else
			{
				UnknownNode(httpBinding, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return httpBinding;
	}

	private PortType Read75_PortType(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id84_PortType || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		PortType portType = new PortType();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = portType.Extensions;
		OperationCollection operations = portType.Operations;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				portType.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (portType.Namespaces == null)
				{
					portType.Namespaces = new XmlSerializerNamespaces();
				}
				portType.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		portType.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			portType.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return portType;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					portType.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id28_operation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (operations == null)
					{
						base.Reader.Skip();
					}
					else
					{
						operations.Add(Read74_Operation(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(portType, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/:operation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		portType.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return portType;
	}

	private Operation Read74_Operation(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id85_Operation || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Operation operation = new Operation();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = operation.Extensions;
		OperationMessageCollection messages = operation.Messages;
		OperationFaultCollection faults = operation.Faults;
		bool[] array2 = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operation.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id86_parameterOrder && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operation.ParameterOrderString = base.Reader.Value;
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (operation.Namespaces == null)
				{
					operation.Namespaces = new XmlSerializerNamespaces();
				}
				operation.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		operation.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			operation.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return operation;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operation.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id30_input && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (messages == null)
					{
						base.Reader.Skip();
					}
					else
					{
						messages.Add(Read71_OperationInput(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id31_output && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (messages == null)
					{
						base.Reader.Skip();
					}
					else
					{
						messages.Add(Read72_OperationOutput(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id32_fault && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (faults == null)
					{
						base.Reader.Skip();
					}
					else
					{
						faults.Add(Read73_OperationFault(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(operation, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/:input, http://schemas.xmlsoap.org/wsdl/:output, http://schemas.xmlsoap.org/wsdl/:fault");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		operation.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return operation;
	}

	private OperationFault Read73_OperationFault(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id87_OperationFault || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		OperationFault operationFault = new OperationFault();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = operationFault.Extensions;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationFault.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationFault.Message = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (operationFault.Namespaces == null)
				{
					operationFault.Namespaces = new XmlSerializerNamespaces();
				}
				operationFault.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		operationFault.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			operationFault.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return operationFault;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operationFault.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(operationFault, "http://schemas.xmlsoap.org/wsdl/:documentation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		operationFault.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return operationFault;
	}

	private OperationOutput Read72_OperationOutput(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id88_OperationOutput || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		OperationOutput operationOutput = new OperationOutput();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = operationOutput.Extensions;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationOutput.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationOutput.Message = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (operationOutput.Namespaces == null)
				{
					operationOutput.Namespaces = new XmlSerializerNamespaces();
				}
				operationOutput.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		operationOutput.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			operationOutput.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return operationOutput;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operationOutput.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(operationOutput, "http://schemas.xmlsoap.org/wsdl/:documentation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		operationOutput.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return operationOutput;
	}

	private OperationInput Read71_OperationInput(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id89_OperationInput || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		OperationInput operationInput = new OperationInput();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = operationInput.Extensions;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationInput.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id10_message && (object)base.Reader.NamespaceURI == id5_Item)
			{
				operationInput.Message = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (operationInput.Namespaces == null)
				{
					operationInput.Namespaces = new XmlSerializerNamespaces();
				}
				operationInput.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		operationInput.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			operationInput.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return operationInput;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					operationInput.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(operationInput, "http://schemas.xmlsoap.org/wsdl/:documentation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		operationInput.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return operationInput;
	}

	private Message Read69_Message(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id90_Message || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Message message = new Message();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = message.Extensions;
		MessagePartCollection parts = message.Parts;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				message.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (message.Namespaces == null)
				{
					message.Namespaces = new XmlSerializerNamespaces();
				}
				message.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		message.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			message.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return message;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					message.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id49_part && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (parts == null)
					{
						base.Reader.Skip();
					}
					else
					{
						parts.Add(Read68_MessagePart(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(message, "http://schemas.xmlsoap.org/wsdl/:documentation, http://schemas.xmlsoap.org/wsdl/:part");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		message.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return message;
	}

	private MessagePart Read68_MessagePart(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id91_MessagePart || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		MessagePart messagePart = new MessagePart();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = messagePart.Extensions;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[3] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				messagePart.Name = base.Reader.Value;
				array2[3] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id92_element && (object)base.Reader.NamespaceURI == id5_Item)
			{
				messagePart.Element = ToXmlQualifiedName(base.Reader.Value);
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id27_type && (object)base.Reader.NamespaceURI == id5_Item)
			{
				messagePart.Type = ToXmlQualifiedName(base.Reader.Value);
				array2[6] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (messagePart.Namespaces == null)
				{
					messagePart.Namespaces = new XmlSerializerNamespaces();
				}
				messagePart.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		messagePart.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			messagePart.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return messagePart;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					messagePart.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(messagePart, "http://schemas.xmlsoap.org/wsdl/:documentation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		messagePart.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return messagePart;
	}

	private Types Read67_Types(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id93_Types || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Types types = new Types();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = types.Extensions;
		XmlSchemas schemas = types.Schemas;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (types.Namespaces == null)
				{
					types.Namespaces = new XmlSerializerNamespaces();
				}
				types.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		types.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			types.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return types;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					types.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else if ((object)base.Reader.LocalName == id94_schema && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (schemas == null)
					{
						base.Reader.Skip();
					}
					else
					{
						schemas.Add(Read66_XmlSchema(isNullable: false, checkType: true));
					}
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(types, "http://schemas.xmlsoap.org/wsdl/:documentation, http://www.w3.org/2001/XMLSchema:schema");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		types.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return types;
	}

	private XmlSchema Read66_XmlSchema(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id96_XmlSchema || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchema xmlSchema = new XmlSchema();
		XmlSchemaObjectCollection includes = xmlSchema.Includes;
		XmlSchemaObjectCollection items = xmlSchema.Items;
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[11];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id97_attributeFormDefault && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.AttributeFormDefault = Read6_XmlSchemaForm(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[2] && (object)base.Reader.LocalName == id98_blockDefault && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.BlockDefault = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[2] = true;
			}
			else if (!array2[3] && (object)base.Reader.LocalName == id99_finalDefault && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.FinalDefault = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[3] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id100_elementFormDefault && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.ElementFormDefault = Read6_XmlSchemaForm(base.Reader.Value);
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id6_targetNamespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.TargetNamespace = CollapseWhitespace(base.Reader.Value);
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id101_version && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.Version = CollapseWhitespace(base.Reader.Value);
				array2[6] = true;
			}
			else if (!array2[9] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchema.Id = CollapseWhitespace(base.Reader.Value);
				array2[9] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchema.Namespaces == null)
				{
					xmlSchema.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchema.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchema.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchema.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchema;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id103_include && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (includes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						includes.Add(Read12_XmlSchemaInclude(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id8_import && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (includes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						includes.Add(Read13_XmlSchemaImport(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id104_redefine && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (includes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						includes.Add(Read64_XmlSchemaRedefine(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read34_XmlSchemaSimpleType(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id106_complexType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read62_XmlSchemaComplexType(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read11_XmlSchemaAnnotation(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id108_notation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read65_XmlSchemaNotation(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read63_XmlSchemaGroup(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id92_element && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read52_XmlSchemaElement(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read40_XmlSchemaAttributeGroup(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchema, "http://www.w3.org/2001/XMLSchema:include, http://www.w3.org/2001/XMLSchema:import, http://www.w3.org/2001/XMLSchema:redefine, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:complexType, http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:notation, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:element, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:attributeGroup");
				}
			}
			else
			{
				UnknownNode(xmlSchema, "http://www.w3.org/2001/XMLSchema:include, http://www.w3.org/2001/XMLSchema:import, http://www.w3.org/2001/XMLSchema:redefine, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:complexType, http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:notation, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:element, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:attributeGroup");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchema.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchema;
	}

	private XmlSchemaAttributeGroup Read40_XmlSchemaAttributeGroup(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id111_XmlSchemaAttributeGroup || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAttributeGroup xmlSchemaAttributeGroup = new XmlSchemaAttributeGroup();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection attributes = xmlSchemaAttributeGroup.Attributes;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttributeGroup.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttributeGroup.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAttributeGroup.Namespaces == null)
				{
					xmlSchemaAttributeGroup.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAttributeGroup.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAttributeGroup.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAttributeGroup.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAttributeGroup;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAttributeGroup.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read37_XmlSchemaAttributeGroupRef(isNullable: false, checkType: true));
					}
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id112_anyAttribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAttributeGroup.AnyAttribute = Read39_XmlSchemaAnyAttribute(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else
				{
					UnknownNode(xmlSchemaAttributeGroup, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:anyAttribute");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAttributeGroup, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:anyAttribute");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAttributeGroup.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAttributeGroup;
	}

	private XmlSchemaAnyAttribute Read39_XmlSchemaAnyAttribute(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id113_XmlSchemaAnyAttribute || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAnyAttribute.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAnyAttribute.Namespace = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id114_processContents && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAnyAttribute.ProcessContents = Read38_XmlSchemaContentProcessing(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAnyAttribute.Namespaces == null)
				{
					xmlSchemaAnyAttribute.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAnyAttribute.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAnyAttribute.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAnyAttribute.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAnyAttribute;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAnyAttribute.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaAnyAttribute, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAnyAttribute, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAnyAttribute.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAnyAttribute;
	}

	private XmlSchemaAnnotation Read11_XmlSchemaAnnotation(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id115_XmlSchemaAnnotation || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAnnotation xmlSchemaAnnotation = new XmlSchemaAnnotation();
		XmlSchemaObjectCollection items = xmlSchemaAnnotation.Items;
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[4];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAnnotation.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAnnotation.Namespaces == null)
				{
					xmlSchemaAnnotation.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAnnotation.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAnnotation.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAnnotation.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAnnotation;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read9_XmlSchemaDocumentation(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id116_appinfo && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read10_XmlSchemaAppInfo(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaAnnotation, "http://www.w3.org/2001/XMLSchema:documentation, http://www.w3.org/2001/XMLSchema:appinfo");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAnnotation, "http://www.w3.org/2001/XMLSchema:documentation, http://www.w3.org/2001/XMLSchema:appinfo");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAnnotation.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAnnotation;
	}

	private XmlSchemaAppInfo Read10_XmlSchemaAppInfo(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id117_XmlSchemaAppInfo || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAppInfo xmlSchemaAppInfo = new XmlSchemaAppInfo();
		XmlNode[] array = null;
		int num = 0;
		bool[] array2 = new bool[3];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id118_source && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAppInfo.Source = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAppInfo.Namespaces == null)
				{
					xmlSchemaAppInfo.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAppInfo.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				UnknownNode(xmlSchemaAppInfo, ":source");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAppInfo.Markup = (XmlNode[])ShrinkArray(array, num, typeof(XmlNode), isNullable: true);
			return xmlSchemaAppInfo;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				array = (XmlNode[])EnsureArrayIndex(array, num, typeof(XmlNode));
				array[num++] = ReadXmlNode(wrapped: false);
			}
			else if (base.Reader.NodeType == XmlNodeType.Text || base.Reader.NodeType == XmlNodeType.CDATA || base.Reader.NodeType == XmlNodeType.Whitespace || base.Reader.NodeType == XmlNodeType.SignificantWhitespace)
			{
				array = (XmlNode[])EnsureArrayIndex(array, num, typeof(XmlNode));
				array[num++] = base.Document.CreateTextNode(base.Reader.ReadString());
			}
			else
			{
				UnknownNode(xmlSchemaAppInfo, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAppInfo.Markup = (XmlNode[])ShrinkArray(array, num, typeof(XmlNode), isNullable: true);
		ReadEndElement();
		return xmlSchemaAppInfo;
	}

	private XmlSchemaDocumentation Read9_XmlSchemaDocumentation(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id119_XmlSchemaDocumentation || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaDocumentation xmlSchemaDocumentation = new XmlSchemaDocumentation();
		XmlNode[] array = null;
		int num = 0;
		bool[] array2 = new bool[4];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id118_source && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaDocumentation.Source = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[2] && (object)base.Reader.LocalName == id120_lang && (object)base.Reader.NamespaceURI == id121_Item)
			{
				xmlSchemaDocumentation.Language = base.Reader.Value;
				array2[2] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaDocumentation.Namespaces == null)
				{
					xmlSchemaDocumentation.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaDocumentation.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				UnknownNode(xmlSchemaDocumentation, ":source, http://www.w3.org/XML/1998/namespace");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaDocumentation.Markup = (XmlNode[])ShrinkArray(array, num, typeof(XmlNode), isNullable: true);
			return xmlSchemaDocumentation;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				array = (XmlNode[])EnsureArrayIndex(array, num, typeof(XmlNode));
				array[num++] = ReadXmlNode(wrapped: false);
			}
			else if (base.Reader.NodeType == XmlNodeType.Text || base.Reader.NodeType == XmlNodeType.CDATA || base.Reader.NodeType == XmlNodeType.Whitespace || base.Reader.NodeType == XmlNodeType.SignificantWhitespace)
			{
				array = (XmlNode[])EnsureArrayIndex(array, num, typeof(XmlNode));
				array[num++] = base.Document.CreateTextNode(base.Reader.ReadString());
			}
			else
			{
				UnknownNode(xmlSchemaDocumentation, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaDocumentation.Markup = (XmlNode[])ShrinkArray(array, num, typeof(XmlNode), isNullable: true);
		ReadEndElement();
		return xmlSchemaDocumentation;
	}

	private XmlSchemaContentProcessing Read38_XmlSchemaContentProcessing(string s)
	{
		return s switch
		{
			"skip" => XmlSchemaContentProcessing.Skip, 
			"lax" => XmlSchemaContentProcessing.Lax, 
			"strict" => XmlSchemaContentProcessing.Strict, 
			_ => throw CreateUnknownConstantException(s, typeof(XmlSchemaContentProcessing)), 
		};
	}

	private XmlSchemaAttributeGroupRef Read37_XmlSchemaAttributeGroupRef(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id122_XmlSchemaAttributeGroupRef || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAttributeGroupRef xmlSchemaAttributeGroupRef = new XmlSchemaAttributeGroupRef();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttributeGroupRef.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id123_ref && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttributeGroupRef.RefName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAttributeGroupRef.Namespaces == null)
				{
					xmlSchemaAttributeGroupRef.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAttributeGroupRef.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAttributeGroupRef.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAttributeGroupRef.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAttributeGroupRef;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAttributeGroupRef.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaAttributeGroupRef, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAttributeGroupRef, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAttributeGroupRef.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAttributeGroupRef;
	}

	private XmlSchemaAttribute Read36_XmlSchemaAttribute(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id124_XmlSchemaAttribute || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[12];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id125_default && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.DefaultValue = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.FixedValue = base.Reader.Value;
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id127_form && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.Form = Read6_XmlSchemaForm(base.Reader.Value);
				array2[6] = true;
			}
			else if (!array2[7] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.Name = base.Reader.Value;
				array2[7] = true;
			}
			else if (!array2[8] && (object)base.Reader.LocalName == id123_ref && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.RefName = ToXmlQualifiedName(base.Reader.Value);
				array2[8] = true;
			}
			else if (!array2[9] && (object)base.Reader.LocalName == id27_type && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.SchemaTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[9] = true;
			}
			else if (!array2[11] && (object)base.Reader.LocalName == id35_use && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAttribute.Use = Read35_XmlSchemaUse(base.Reader.Value);
				array2[11] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAttribute.Namespaces == null)
				{
					xmlSchemaAttribute.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAttribute.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAttribute.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAttribute.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAttribute;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAttribute.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[10] && (object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAttribute.SchemaType = Read34_XmlSchemaSimpleType(isNullable: false, checkType: true);
					array2[10] = true;
				}
				else
				{
					UnknownNode(xmlSchemaAttribute, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAttribute, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAttribute.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAttribute;
	}

	private XmlSchemaSimpleType Read34_XmlSchemaSimpleType(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id128_XmlSchemaSimpleType || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleType.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleType.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id129_final && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleType.Final = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleType.Namespaces == null)
				{
					xmlSchemaSimpleType.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleType.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleType.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleType.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSimpleType;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleType.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id130_list && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleType.Content = Read17_XmlSchemaSimpleTypeList(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id131_restriction && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleType.Content = Read32_XmlSchemaSimpleTypeRestriction(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id132_union && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleType.Content = Read33_XmlSchemaSimpleTypeUnion(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else
				{
					UnknownNode(xmlSchemaSimpleType, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:list, http://www.w3.org/2001/XMLSchema:restriction, http://www.w3.org/2001/XMLSchema:union");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleType, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:list, http://www.w3.org/2001/XMLSchema:restriction, http://www.w3.org/2001/XMLSchema:union");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleType.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleType;
	}

	private XmlSchemaSimpleTypeUnion Read33_XmlSchemaSimpleTypeUnion(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id133_XmlSchemaSimpleTypeUnion || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleTypeUnion xmlSchemaSimpleTypeUnion = new XmlSchemaSimpleTypeUnion();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection baseTypes = xmlSchemaSimpleTypeUnion.BaseTypes;
		XmlQualifiedName[] array2 = null;
		int num2 = 0;
		bool[] array3 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array3[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleTypeUnion.Id = CollapseWhitespace(base.Reader.Value);
				array3[1] = true;
			}
			else if ((object)base.Reader.LocalName == id134_memberTypes && (object)base.Reader.NamespaceURI == id5_Item)
			{
				string[] array4 = base.Reader.Value.Split(null);
				for (int i = 0; i < array4.Length; i++)
				{
					array2 = (XmlQualifiedName[])EnsureArrayIndex(array2, num2, typeof(XmlQualifiedName));
					array2[num2++] = ToXmlQualifiedName(array4[i]);
				}
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleTypeUnion.Namespaces == null)
				{
					xmlSchemaSimpleTypeUnion.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleTypeUnion.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleTypeUnion.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		xmlSchemaSimpleTypeUnion.MemberTypes = (XmlQualifiedName[])ShrinkArray(array2, num2, typeof(XmlQualifiedName), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleTypeUnion.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			xmlSchemaSimpleTypeUnion.MemberTypes = (XmlQualifiedName[])ShrinkArray(array2, num2, typeof(XmlQualifiedName), isNullable: true);
			return xmlSchemaSimpleTypeUnion;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array3[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleTypeUnion.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array3[2] = true;
				}
				else if ((object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (baseTypes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						baseTypes.Add(Read34_XmlSchemaSimpleType(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaSimpleTypeUnion, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleTypeUnion, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleTypeUnion.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		xmlSchemaSimpleTypeUnion.MemberTypes = (XmlQualifiedName[])ShrinkArray(array2, num2, typeof(XmlQualifiedName), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleTypeUnion;
	}

	private XmlSchemaSimpleTypeRestriction Read32_XmlSchemaSimpleTypeRestriction(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id135_XmlSchemaSimpleTypeRestriction || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = new XmlSchemaSimpleTypeRestriction();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection facets = xmlSchemaSimpleTypeRestriction.Facets;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleTypeRestriction.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id136_base && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleTypeRestriction.BaseTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleTypeRestriction.Namespaces == null)
				{
					xmlSchemaSimpleTypeRestriction.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleTypeRestriction.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleTypeRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleTypeRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSimpleTypeRestriction;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleTypeRestriction.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleTypeRestriction.BaseType = Read34_XmlSchemaSimpleType(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id137_fractionDigits && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read20_XmlSchemaFractionDigitsFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id138_minInclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read21_XmlSchemaMinInclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id139_maxLength && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read22_XmlSchemaMaxLengthFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id140_length && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read23_XmlSchemaLengthFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id141_totalDigits && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read24_XmlSchemaTotalDigitsFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id62_pattern && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read25_XmlSchemaPatternFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id142_enumeration && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read26_XmlSchemaEnumerationFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id143_maxInclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read27_XmlSchemaMaxInclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id144_maxExclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read28_XmlSchemaMaxExclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id145_whiteSpace && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read29_XmlSchemaWhiteSpaceFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id146_minExclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read30_XmlSchemaMinExclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id147_minLength && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read31_XmlSchemaMinLengthFacet(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaSimpleTypeRestriction, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:fractionDigits, http://www.w3.org/2001/XMLSchema:minInclusive, http://www.w3.org/2001/XMLSchema:maxLength, http://www.w3.org/2001/XMLSchema:length, http://www.w3.org/2001/XMLSchema:totalDigits, http://www.w3.org/2001/XMLSchema:pattern, http://www.w3.org/2001/XMLSchema:enumeration, http://www.w3.org/2001/XMLSchema:maxInclusive, http://www.w3.org/2001/XMLSchema:maxExclusive, http://www.w3.org/2001/XMLSchema:whiteSpace, http://www.w3.org/2001/XMLSchema:minExclusive, http://www.w3.org/2001/XMLSchema:minLength");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleTypeRestriction, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:fractionDigits, http://www.w3.org/2001/XMLSchema:minInclusive, http://www.w3.org/2001/XMLSchema:maxLength, http://www.w3.org/2001/XMLSchema:length, http://www.w3.org/2001/XMLSchema:totalDigits, http://www.w3.org/2001/XMLSchema:pattern, http://www.w3.org/2001/XMLSchema:enumeration, http://www.w3.org/2001/XMLSchema:maxInclusive, http://www.w3.org/2001/XMLSchema:maxExclusive, http://www.w3.org/2001/XMLSchema:whiteSpace, http://www.w3.org/2001/XMLSchema:minExclusive, http://www.w3.org/2001/XMLSchema:minLength");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleTypeRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleTypeRestriction;
	}

	private XmlSchemaMinLengthFacet Read31_XmlSchemaMinLengthFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id148_XmlSchemaMinLengthFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaMinLengthFacet xmlSchemaMinLengthFacet = new XmlSchemaMinLengthFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinLengthFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinLengthFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinLengthFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaMinLengthFacet.Namespaces == null)
				{
					xmlSchemaMinLengthFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaMinLengthFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaMinLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaMinLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaMinLengthFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaMinLengthFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaMinLengthFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaMinLengthFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaMinLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaMinLengthFacet;
	}

	private XmlSchemaMinExclusiveFacet Read30_XmlSchemaMinExclusiveFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id150_XmlSchemaMinExclusiveFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaMinExclusiveFacet xmlSchemaMinExclusiveFacet = new XmlSchemaMinExclusiveFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinExclusiveFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinExclusiveFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinExclusiveFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaMinExclusiveFacet.Namespaces == null)
				{
					xmlSchemaMinExclusiveFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaMinExclusiveFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaMinExclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaMinExclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaMinExclusiveFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaMinExclusiveFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaMinExclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaMinExclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaMinExclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaMinExclusiveFacet;
	}

	private XmlSchemaWhiteSpaceFacet Read29_XmlSchemaWhiteSpaceFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id151_XmlSchemaWhiteSpaceFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaWhiteSpaceFacet xmlSchemaWhiteSpaceFacet = new XmlSchemaWhiteSpaceFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaWhiteSpaceFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaWhiteSpaceFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaWhiteSpaceFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaWhiteSpaceFacet.Namespaces == null)
				{
					xmlSchemaWhiteSpaceFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaWhiteSpaceFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaWhiteSpaceFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaWhiteSpaceFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaWhiteSpaceFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaWhiteSpaceFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaWhiteSpaceFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaWhiteSpaceFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaWhiteSpaceFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaWhiteSpaceFacet;
	}

	private XmlSchemaMaxExclusiveFacet Read28_XmlSchemaMaxExclusiveFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id152_XmlSchemaMaxExclusiveFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaMaxExclusiveFacet xmlSchemaMaxExclusiveFacet = new XmlSchemaMaxExclusiveFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxExclusiveFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxExclusiveFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxExclusiveFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaMaxExclusiveFacet.Namespaces == null)
				{
					xmlSchemaMaxExclusiveFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaMaxExclusiveFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaMaxExclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaMaxExclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaMaxExclusiveFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaMaxExclusiveFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaMaxExclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaMaxExclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaMaxExclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaMaxExclusiveFacet;
	}

	private XmlSchemaMaxInclusiveFacet Read27_XmlSchemaMaxInclusiveFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id153_XmlSchemaMaxInclusiveFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaMaxInclusiveFacet xmlSchemaMaxInclusiveFacet = new XmlSchemaMaxInclusiveFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxInclusiveFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxInclusiveFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxInclusiveFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaMaxInclusiveFacet.Namespaces == null)
				{
					xmlSchemaMaxInclusiveFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaMaxInclusiveFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaMaxInclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaMaxInclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaMaxInclusiveFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaMaxInclusiveFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaMaxInclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaMaxInclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaMaxInclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaMaxInclusiveFacet;
	}

	private XmlSchemaEnumerationFacet Read26_XmlSchemaEnumerationFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id154_XmlSchemaEnumerationFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaEnumerationFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaEnumerationFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaEnumerationFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaEnumerationFacet.Namespaces == null)
				{
					xmlSchemaEnumerationFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaEnumerationFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaEnumerationFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaEnumerationFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaEnumerationFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaEnumerationFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaEnumerationFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaEnumerationFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaEnumerationFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaEnumerationFacet;
	}

	private XmlSchemaPatternFacet Read25_XmlSchemaPatternFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id155_XmlSchemaPatternFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaPatternFacet xmlSchemaPatternFacet = new XmlSchemaPatternFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaPatternFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaPatternFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaPatternFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaPatternFacet.Namespaces == null)
				{
					xmlSchemaPatternFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaPatternFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaPatternFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaPatternFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaPatternFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaPatternFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaPatternFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaPatternFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaPatternFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaPatternFacet;
	}

	private XmlSchemaTotalDigitsFacet Read24_XmlSchemaTotalDigitsFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id156_XmlSchemaTotalDigitsFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaTotalDigitsFacet xmlSchemaTotalDigitsFacet = new XmlSchemaTotalDigitsFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaTotalDigitsFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaTotalDigitsFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaTotalDigitsFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaTotalDigitsFacet.Namespaces == null)
				{
					xmlSchemaTotalDigitsFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaTotalDigitsFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaTotalDigitsFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaTotalDigitsFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaTotalDigitsFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaTotalDigitsFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaTotalDigitsFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaTotalDigitsFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaTotalDigitsFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaTotalDigitsFacet;
	}

	private XmlSchemaLengthFacet Read23_XmlSchemaLengthFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id157_XmlSchemaLengthFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaLengthFacet xmlSchemaLengthFacet = new XmlSchemaLengthFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaLengthFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaLengthFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaLengthFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaLengthFacet.Namespaces == null)
				{
					xmlSchemaLengthFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaLengthFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaLengthFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaLengthFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaLengthFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaLengthFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaLengthFacet;
	}

	private XmlSchemaMaxLengthFacet Read22_XmlSchemaMaxLengthFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id158_XmlSchemaMaxLengthFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaMaxLengthFacet xmlSchemaMaxLengthFacet = new XmlSchemaMaxLengthFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxLengthFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxLengthFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMaxLengthFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaMaxLengthFacet.Namespaces == null)
				{
					xmlSchemaMaxLengthFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaMaxLengthFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaMaxLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaMaxLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaMaxLengthFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaMaxLengthFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaMaxLengthFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaMaxLengthFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaMaxLengthFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaMaxLengthFacet;
	}

	private XmlSchemaMinInclusiveFacet Read21_XmlSchemaMinInclusiveFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id159_XmlSchemaMinInclusiveFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaMinInclusiveFacet xmlSchemaMinInclusiveFacet = new XmlSchemaMinInclusiveFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinInclusiveFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinInclusiveFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaMinInclusiveFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaMinInclusiveFacet.Namespaces == null)
				{
					xmlSchemaMinInclusiveFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaMinInclusiveFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaMinInclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaMinInclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaMinInclusiveFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaMinInclusiveFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaMinInclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaMinInclusiveFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaMinInclusiveFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaMinInclusiveFacet;
	}

	private XmlSchemaFractionDigitsFacet Read20_XmlSchemaFractionDigitsFacet(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id160_XmlSchemaFractionDigitsFacet || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaFractionDigitsFacet xmlSchemaFractionDigitsFacet = new XmlSchemaFractionDigitsFacet();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaFractionDigitsFacet.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id149_value && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaFractionDigitsFacet.Value = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaFractionDigitsFacet.IsFixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaFractionDigitsFacet.Namespaces == null)
				{
					xmlSchemaFractionDigitsFacet.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaFractionDigitsFacet.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaFractionDigitsFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaFractionDigitsFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaFractionDigitsFacet;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaFractionDigitsFacet.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaFractionDigitsFacet, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaFractionDigitsFacet, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaFractionDigitsFacet.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaFractionDigitsFacet;
	}

	private XmlSchemaSimpleTypeList Read17_XmlSchemaSimpleTypeList(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id161_XmlSchemaSimpleTypeList || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleTypeList xmlSchemaSimpleTypeList = new XmlSchemaSimpleTypeList();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleTypeList.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id162_itemType && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleTypeList.ItemTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleTypeList.Namespaces == null)
				{
					xmlSchemaSimpleTypeList.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleTypeList.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleTypeList.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleTypeList.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSimpleTypeList;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleTypeList.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleTypeList.ItemType = Read34_XmlSchemaSimpleType(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else
				{
					UnknownNode(xmlSchemaSimpleTypeList, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleTypeList, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleTypeList.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleTypeList;
	}

	private XmlSchemaDerivationMethod Read7_XmlSchemaDerivationMethod(string s)
	{
		return (XmlSchemaDerivationMethod)XmlSerializationReader.ToEnum(s, XmlSchemaDerivationMethodValues, "global::System.Xml.Schema.XmlSchemaDerivationMethod");
	}

	private XmlSchemaUse Read35_XmlSchemaUse(string s)
	{
		return s switch
		{
			"optional" => XmlSchemaUse.Optional, 
			"prohibited" => XmlSchemaUse.Prohibited, 
			"required" => XmlSchemaUse.Required, 
			_ => throw CreateUnknownConstantException(s, typeof(XmlSchemaUse)), 
		};
	}

	private XmlSchemaForm Read6_XmlSchemaForm(string s)
	{
		if (!(s == "qualified"))
		{
			if (s == "unqualified")
			{
				return XmlSchemaForm.Unqualified;
			}
			throw CreateUnknownConstantException(s, typeof(XmlSchemaForm));
		}
		return XmlSchemaForm.Qualified;
	}

	private XmlSchemaElement Read52_XmlSchemaElement(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id163_XmlSchemaElement || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection constraints = xmlSchemaElement.Constraints;
		bool[] array2 = new bool[19];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id164_minOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.MinOccursString = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id165_maxOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.MaxOccursString = base.Reader.Value;
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id166_abstract && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.IsAbstract = XmlConvert.ToBoolean(base.Reader.Value);
				array2[6] = true;
			}
			else if (!array2[7] && (object)base.Reader.LocalName == id167_block && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.Block = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[7] = true;
			}
			else if (!array2[8] && (object)base.Reader.LocalName == id125_default && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.DefaultValue = base.Reader.Value;
				array2[8] = true;
			}
			else if (!array2[9] && (object)base.Reader.LocalName == id129_final && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.Final = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[9] = true;
			}
			else if (!array2[10] && (object)base.Reader.LocalName == id126_fixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.FixedValue = base.Reader.Value;
				array2[10] = true;
			}
			else if (!array2[11] && (object)base.Reader.LocalName == id127_form && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.Form = Read6_XmlSchemaForm(base.Reader.Value);
				array2[11] = true;
			}
			else if (!array2[12] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.Name = base.Reader.Value;
				array2[12] = true;
			}
			else if (!array2[13] && (object)base.Reader.LocalName == id168_nillable && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.IsNillable = XmlConvert.ToBoolean(base.Reader.Value);
				array2[13] = true;
			}
			else if (!array2[14] && (object)base.Reader.LocalName == id123_ref && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.RefName = ToXmlQualifiedName(base.Reader.Value);
				array2[14] = true;
			}
			else if (!array2[15] && (object)base.Reader.LocalName == id169_substitutionGroup && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.SubstitutionGroup = ToXmlQualifiedName(base.Reader.Value);
				array2[15] = true;
			}
			else if (!array2[16] && (object)base.Reader.LocalName == id27_type && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaElement.SchemaTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[16] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaElement.Namespaces == null)
				{
					xmlSchemaElement.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaElement.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaElement.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaElement.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaElement;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaElement.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[17] && (object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaElement.SchemaType = Read34_XmlSchemaSimpleType(isNullable: false, checkType: true);
					array2[17] = true;
				}
				else if (!array2[17] && (object)base.Reader.LocalName == id106_complexType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaElement.SchemaType = Read62_XmlSchemaComplexType(isNullable: false, checkType: true);
					array2[17] = true;
				}
				else if ((object)base.Reader.LocalName == id170_key && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (constraints == null)
					{
						base.Reader.Skip();
					}
					else
					{
						constraints.Add(Read49_XmlSchemaKey(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id171_unique && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (constraints == null)
					{
						base.Reader.Skip();
					}
					else
					{
						constraints.Add(Read50_XmlSchemaUnique(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id172_keyref && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (constraints == null)
					{
						base.Reader.Skip();
					}
					else
					{
						constraints.Add(Read51_XmlSchemaKeyref(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaElement, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:complexType, http://www.w3.org/2001/XMLSchema:key, http://www.w3.org/2001/XMLSchema:unique, http://www.w3.org/2001/XMLSchema:keyref");
				}
			}
			else
			{
				UnknownNode(xmlSchemaElement, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:complexType, http://www.w3.org/2001/XMLSchema:key, http://www.w3.org/2001/XMLSchema:unique, http://www.w3.org/2001/XMLSchema:keyref");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaElement.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaElement;
	}

	private XmlSchemaKeyref Read51_XmlSchemaKeyref(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id173_XmlSchemaKeyref || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaKeyref xmlSchemaKeyref = new XmlSchemaKeyref();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection fields = xmlSchemaKeyref.Fields;
		bool[] array2 = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaKeyref.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaKeyref.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[7] && (object)base.Reader.LocalName == id174_refer && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaKeyref.Refer = ToXmlQualifiedName(base.Reader.Value);
				array2[7] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaKeyref.Namespaces == null)
				{
					xmlSchemaKeyref.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaKeyref.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaKeyref.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaKeyref.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaKeyref;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaKeyref.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id175_selector && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaKeyref.Selector = Read47_XmlSchemaXPath(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id176_field && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (fields == null)
					{
						base.Reader.Skip();
					}
					else
					{
						fields.Add(Read47_XmlSchemaXPath(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaKeyref, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:selector, http://www.w3.org/2001/XMLSchema:field");
				}
			}
			else
			{
				UnknownNode(xmlSchemaKeyref, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:selector, http://www.w3.org/2001/XMLSchema:field");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaKeyref.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaKeyref;
	}

	private XmlSchemaXPath Read47_XmlSchemaXPath(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id177_XmlSchemaXPath || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaXPath xmlSchemaXPath = new XmlSchemaXPath();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaXPath.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id178_xpath && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaXPath.XPath = base.Reader.Value;
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaXPath.Namespaces == null)
				{
					xmlSchemaXPath.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaXPath.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaXPath.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaXPath.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaXPath;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaXPath.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaXPath, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaXPath, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaXPath.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaXPath;
	}

	private XmlSchemaUnique Read50_XmlSchemaUnique(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id179_XmlSchemaUnique || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaUnique xmlSchemaUnique = new XmlSchemaUnique();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection fields = xmlSchemaUnique.Fields;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaUnique.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaUnique.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaUnique.Namespaces == null)
				{
					xmlSchemaUnique.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaUnique.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaUnique.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaUnique.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaUnique;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaUnique.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id175_selector && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaUnique.Selector = Read47_XmlSchemaXPath(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id176_field && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (fields == null)
					{
						base.Reader.Skip();
					}
					else
					{
						fields.Add(Read47_XmlSchemaXPath(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaUnique, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:selector, http://www.w3.org/2001/XMLSchema:field");
				}
			}
			else
			{
				UnknownNode(xmlSchemaUnique, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:selector, http://www.w3.org/2001/XMLSchema:field");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaUnique.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaUnique;
	}

	private XmlSchemaKey Read49_XmlSchemaKey(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id180_XmlSchemaKey || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaKey xmlSchemaKey = new XmlSchemaKey();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection fields = xmlSchemaKey.Fields;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaKey.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaKey.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaKey.Namespaces == null)
				{
					xmlSchemaKey.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaKey.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaKey.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaKey.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaKey;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaKey.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id175_selector && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaKey.Selector = Read47_XmlSchemaXPath(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id176_field && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (fields == null)
					{
						base.Reader.Skip();
					}
					else
					{
						fields.Add(Read47_XmlSchemaXPath(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaKey, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:selector, http://www.w3.org/2001/XMLSchema:field");
				}
			}
			else
			{
				UnknownNode(xmlSchemaKey, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:selector, http://www.w3.org/2001/XMLSchema:field");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaKey.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaKey;
	}

	private XmlSchemaComplexType Read62_XmlSchemaComplexType(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id181_XmlSchemaComplexType || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection attributes = xmlSchemaComplexType.Attributes;
		bool[] array2 = new bool[13];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexType.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexType.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id129_final && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexType.Final = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id166_abstract && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexType.IsAbstract = XmlConvert.ToBoolean(base.Reader.Value);
				array2[6] = true;
			}
			else if (!array2[7] && (object)base.Reader.LocalName == id167_block && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexType.Block = Read7_XmlSchemaDerivationMethod(base.Reader.Value);
				array2[7] = true;
			}
			else if (!array2[8] && (object)base.Reader.LocalName == id182_mixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexType.IsMixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[8] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaComplexType.Namespaces == null)
				{
					xmlSchemaComplexType.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaComplexType.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaComplexType.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaComplexType.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaComplexType;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[9] && (object)base.Reader.LocalName == id183_complexContent && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.ContentModel = Read58_XmlSchemaComplexContent(isNullable: false, checkType: true);
					array2[9] = true;
				}
				else if (!array2[9] && (object)base.Reader.LocalName == id184_simpleContent && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.ContentModel = Read61_XmlSchemaSimpleContent(isNullable: false, checkType: true);
					array2[9] = true;
				}
				else if (!array2[10] && (object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.Particle = Read44_XmlSchemaGroupRef(isNullable: false, checkType: true);
					array2[10] = true;
				}
				else if (!array2[10] && (object)base.Reader.LocalName == id185_sequence && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.Particle = Read53_XmlSchemaSequence(isNullable: false, checkType: true);
					array2[10] = true;
				}
				else if (!array2[10] && (object)base.Reader.LocalName == id186_choice && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.Particle = Read54_XmlSchemaChoice(isNullable: false, checkType: true);
					array2[10] = true;
				}
				else if (!array2[10] && (object)base.Reader.LocalName == id187_all && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.Particle = Read55_XmlSchemaAll(isNullable: false, checkType: true);
					array2[10] = true;
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read37_XmlSchemaAttributeGroupRef(isNullable: false, checkType: true));
					}
				}
				else if (!array2[12] && (object)base.Reader.LocalName == id112_anyAttribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexType.AnyAttribute = Read39_XmlSchemaAnyAttribute(isNullable: false, checkType: true);
					array2[12] = true;
				}
				else
				{
					UnknownNode(xmlSchemaComplexType, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:complexContent, http://www.w3.org/2001/XMLSchema:simpleContent, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:all, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:anyAttribute");
				}
			}
			else
			{
				UnknownNode(xmlSchemaComplexType, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:complexContent, http://www.w3.org/2001/XMLSchema:simpleContent, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:all, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:anyAttribute");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaComplexType.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaComplexType;
	}

	private XmlSchemaAll Read55_XmlSchemaAll(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id188_XmlSchemaAll || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAll xmlSchemaAll = new XmlSchemaAll();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection items = xmlSchemaAll.Items;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAll.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id164_minOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAll.MinOccursString = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id165_maxOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAll.MaxOccursString = base.Reader.Value;
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAll.Namespaces == null)
				{
					xmlSchemaAll.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAll.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAll.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAll.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAll;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAll.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if ((object)base.Reader.LocalName == id92_element && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read52_XmlSchemaElement(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaAll, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:element");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAll, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:element");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAll.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAll;
	}

	private XmlSchemaChoice Read54_XmlSchemaChoice(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id189_XmlSchemaChoice || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaChoice xmlSchemaChoice = new XmlSchemaChoice();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection items = xmlSchemaChoice.Items;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaChoice.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id164_minOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaChoice.MinOccursString = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id165_maxOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaChoice.MaxOccursString = base.Reader.Value;
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaChoice.Namespaces == null)
				{
					xmlSchemaChoice.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaChoice.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaChoice.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaChoice.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaChoice;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaChoice.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if ((object)base.Reader.LocalName == id190_any && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read46_XmlSchemaAny(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id186_choice && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read54_XmlSchemaChoice(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id185_sequence && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read53_XmlSchemaSequence(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id92_element && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read52_XmlSchemaElement(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read44_XmlSchemaGroupRef(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaChoice, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:any, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:element, http://www.w3.org/2001/XMLSchema:group");
				}
			}
			else
			{
				UnknownNode(xmlSchemaChoice, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:any, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:element, http://www.w3.org/2001/XMLSchema:group");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaChoice.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaChoice;
	}

	private XmlSchemaGroupRef Read44_XmlSchemaGroupRef(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id191_XmlSchemaGroupRef || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaGroupRef xmlSchemaGroupRef = new XmlSchemaGroupRef();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaGroupRef.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id164_minOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaGroupRef.MinOccursString = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id165_maxOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaGroupRef.MaxOccursString = base.Reader.Value;
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id123_ref && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaGroupRef.RefName = ToXmlQualifiedName(base.Reader.Value);
				array2[6] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaGroupRef.Namespaces == null)
				{
					xmlSchemaGroupRef.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaGroupRef.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaGroupRef.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaGroupRef.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaGroupRef;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaGroupRef.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaGroupRef, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaGroupRef, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaGroupRef.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaGroupRef;
	}

	private XmlSchemaSequence Read53_XmlSchemaSequence(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id192_XmlSchemaSequence || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection items = xmlSchemaSequence.Items;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSequence.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id164_minOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSequence.MinOccursString = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id165_maxOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSequence.MaxOccursString = base.Reader.Value;
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSequence.Namespaces == null)
				{
					xmlSchemaSequence.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSequence.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSequence.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSequence.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSequence;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSequence.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if ((object)base.Reader.LocalName == id92_element && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read52_XmlSchemaElement(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id185_sequence && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read53_XmlSchemaSequence(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id190_any && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read46_XmlSchemaAny(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id186_choice && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read54_XmlSchemaChoice(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read44_XmlSchemaGroupRef(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaSequence, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:element, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:any, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:group");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSequence, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:element, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:any, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:group");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSequence.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSequence;
	}

	private XmlSchemaAny Read46_XmlSchemaAny(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id193_XmlSchemaAny || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAny.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id164_minOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAny.MinOccursString = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id165_maxOccurs && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAny.MaxOccursString = base.Reader.Value;
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAny.Namespace = base.Reader.Value;
				array2[6] = true;
			}
			else if (!array2[7] && (object)base.Reader.LocalName == id114_processContents && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaAny.ProcessContents = Read38_XmlSchemaContentProcessing(base.Reader.Value);
				array2[7] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaAny.Namespaces == null)
				{
					xmlSchemaAny.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaAny.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaAny.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaAny.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaAny;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaAny.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaAny, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaAny, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaAny.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaAny;
	}

	private XmlSchemaSimpleContent Read61_XmlSchemaSimpleContent(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id194_XmlSchemaSimpleContent || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleContent.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleContent.Namespaces == null)
				{
					xmlSchemaSimpleContent.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleContent.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleContent.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleContent.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSimpleContent;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContent.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[4] && (object)base.Reader.LocalName == id131_restriction && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContent.Content = Read59_Item(isNullable: false, checkType: true);
					array2[4] = true;
				}
				else if (!array2[4] && (object)base.Reader.LocalName == id195_extension && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContent.Content = Read60_Item(isNullable: false, checkType: true);
					array2[4] = true;
				}
				else
				{
					UnknownNode(xmlSchemaSimpleContent, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:restriction, http://www.w3.org/2001/XMLSchema:extension");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleContent, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:restriction, http://www.w3.org/2001/XMLSchema:extension");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleContent.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleContent;
	}

	private XmlSchemaSimpleContentExtension Read60_Item(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id196_Item || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = new XmlSchemaSimpleContentExtension();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection attributes = xmlSchemaSimpleContentExtension.Attributes;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleContentExtension.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id136_base && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleContentExtension.BaseTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleContentExtension.Namespaces == null)
				{
					xmlSchemaSimpleContentExtension.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleContentExtension.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleContentExtension.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleContentExtension.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSimpleContentExtension;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContentExtension.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read37_XmlSchemaAttributeGroupRef(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if (!array2[6] && (object)base.Reader.LocalName == id112_anyAttribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContentExtension.AnyAttribute = Read39_XmlSchemaAnyAttribute(isNullable: false, checkType: true);
					array2[6] = true;
				}
				else
				{
					UnknownNode(xmlSchemaSimpleContentExtension, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleContentExtension, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleContentExtension.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleContentExtension;
	}

	private XmlSchemaSimpleContentRestriction Read59_Item(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id197_Item || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaSimpleContentRestriction xmlSchemaSimpleContentRestriction = new XmlSchemaSimpleContentRestriction();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection facets = xmlSchemaSimpleContentRestriction.Facets;
		XmlSchemaObjectCollection attributes = xmlSchemaSimpleContentRestriction.Attributes;
		bool[] array2 = new bool[9];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleContentRestriction.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id136_base && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaSimpleContentRestriction.BaseTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaSimpleContentRestriction.Namespaces == null)
				{
					xmlSchemaSimpleContentRestriction.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaSimpleContentRestriction.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaSimpleContentRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaSimpleContentRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaSimpleContentRestriction;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContentRestriction.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContentRestriction.BaseType = Read34_XmlSchemaSimpleType(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id138_minInclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read21_XmlSchemaMinInclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id144_maxExclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read28_XmlSchemaMaxExclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id145_whiteSpace && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read29_XmlSchemaWhiteSpaceFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id147_minLength && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read31_XmlSchemaMinLengthFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id62_pattern && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read25_XmlSchemaPatternFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id142_enumeration && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read26_XmlSchemaEnumerationFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id143_maxInclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read27_XmlSchemaMaxInclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id140_length && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read23_XmlSchemaLengthFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id139_maxLength && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read22_XmlSchemaMaxLengthFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id146_minExclusive && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read30_XmlSchemaMinExclusiveFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id141_totalDigits && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read24_XmlSchemaTotalDigitsFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id137_fractionDigits && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (facets == null)
					{
						base.Reader.Skip();
					}
					else
					{
						facets.Add(Read20_XmlSchemaFractionDigitsFacet(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read37_XmlSchemaAttributeGroupRef(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if (!array2[8] && (object)base.Reader.LocalName == id112_anyAttribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaSimpleContentRestriction.AnyAttribute = Read39_XmlSchemaAnyAttribute(isNullable: false, checkType: true);
					array2[8] = true;
				}
				else
				{
					UnknownNode(xmlSchemaSimpleContentRestriction, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:minInclusive, http://www.w3.org/2001/XMLSchema:maxExclusive, http://www.w3.org/2001/XMLSchema:whiteSpace, http://www.w3.org/2001/XMLSchema:minLength, http://www.w3.org/2001/XMLSchema:pattern, http://www.w3.org/2001/XMLSchema:enumeration, http://www.w3.org/2001/XMLSchema:maxInclusive, http://www.w3.org/2001/XMLSchema:length, http://www.w3.org/2001/XMLSchema:maxLength, http://www.w3.org/2001/XMLSchema:minExclusive, http://www.w3.org/2001/XMLSchema:totalDigits, http://www.w3.org/2001/XMLSchema:fractionDigits, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
				}
			}
			else
			{
				UnknownNode(xmlSchemaSimpleContentRestriction, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:minInclusive, http://www.w3.org/2001/XMLSchema:maxExclusive, http://www.w3.org/2001/XMLSchema:whiteSpace, http://www.w3.org/2001/XMLSchema:minLength, http://www.w3.org/2001/XMLSchema:pattern, http://www.w3.org/2001/XMLSchema:enumeration, http://www.w3.org/2001/XMLSchema:maxInclusive, http://www.w3.org/2001/XMLSchema:length, http://www.w3.org/2001/XMLSchema:maxLength, http://www.w3.org/2001/XMLSchema:minExclusive, http://www.w3.org/2001/XMLSchema:totalDigits, http://www.w3.org/2001/XMLSchema:fractionDigits, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaSimpleContentRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaSimpleContentRestriction;
	}

	private XmlSchemaComplexContent Read58_XmlSchemaComplexContent(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id198_XmlSchemaComplexContent || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexContent.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id182_mixed && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexContent.IsMixed = XmlConvert.ToBoolean(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaComplexContent.Namespaces == null)
				{
					xmlSchemaComplexContent.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaComplexContent.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaComplexContent.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaComplexContent.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaComplexContent;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContent.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id195_extension && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContent.Content = Read56_Item(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id131_restriction && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContent.Content = Read57_Item(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else
				{
					UnknownNode(xmlSchemaComplexContent, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:extension, http://www.w3.org/2001/XMLSchema:restriction");
				}
			}
			else
			{
				UnknownNode(xmlSchemaComplexContent, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:extension, http://www.w3.org/2001/XMLSchema:restriction");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaComplexContent.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaComplexContent;
	}

	private XmlSchemaComplexContentRestriction Read57_Item(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id199_Item || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection attributes = xmlSchemaComplexContentRestriction.Attributes;
		bool[] array2 = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexContentRestriction.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id136_base && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexContentRestriction.BaseTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaComplexContentRestriction.Namespaces == null)
				{
					xmlSchemaComplexContentRestriction.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaComplexContentRestriction.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaComplexContentRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaComplexContentRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaComplexContentRestriction;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentRestriction.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id186_choice && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentRestriction.Particle = Read54_XmlSchemaChoice(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentRestriction.Particle = Read44_XmlSchemaGroupRef(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id187_all && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentRestriction.Particle = Read55_XmlSchemaAll(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id185_sequence && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentRestriction.Particle = Read53_XmlSchemaSequence(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read37_XmlSchemaAttributeGroupRef(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if (!array2[7] && (object)base.Reader.LocalName == id112_anyAttribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentRestriction.AnyAttribute = Read39_XmlSchemaAnyAttribute(isNullable: false, checkType: true);
					array2[7] = true;
				}
				else
				{
					UnknownNode(xmlSchemaComplexContentRestriction, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:all, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
				}
			}
			else
			{
				UnknownNode(xmlSchemaComplexContentRestriction, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:all, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaComplexContentRestriction.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaComplexContentRestriction;
	}

	private XmlSchemaComplexContentExtension Read56_Item(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id200_Item || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaComplexContentExtension xmlSchemaComplexContentExtension = new XmlSchemaComplexContentExtension();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection attributes = xmlSchemaComplexContentExtension.Attributes;
		bool[] array2 = new bool[8];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexContentExtension.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id136_base && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaComplexContentExtension.BaseTypeName = ToXmlQualifiedName(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaComplexContentExtension.Namespaces == null)
				{
					xmlSchemaComplexContentExtension.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaComplexContentExtension.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaComplexContentExtension.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaComplexContentExtension.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaComplexContentExtension;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentExtension.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentExtension.Particle = Read44_XmlSchemaGroupRef(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id186_choice && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentExtension.Particle = Read54_XmlSchemaChoice(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id187_all && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentExtension.Particle = Read55_XmlSchemaAll(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id185_sequence && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentExtension.Particle = Read53_XmlSchemaSequence(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read37_XmlSchemaAttributeGroupRef(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id109_attribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (attributes == null)
					{
						base.Reader.Skip();
					}
					else
					{
						attributes.Add(Read36_XmlSchemaAttribute(isNullable: false, checkType: true));
					}
				}
				else if (!array2[7] && (object)base.Reader.LocalName == id112_anyAttribute && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaComplexContentExtension.AnyAttribute = Read39_XmlSchemaAnyAttribute(isNullable: false, checkType: true);
					array2[7] = true;
				}
				else
				{
					UnknownNode(xmlSchemaComplexContentExtension, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:all, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
				}
			}
			else
			{
				UnknownNode(xmlSchemaComplexContentExtension, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:group, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:all, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:attribute, http://www.w3.org/2001/XMLSchema:anyAttribute");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaComplexContentExtension.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaComplexContentExtension;
	}

	private XmlSchemaGroup Read63_XmlSchemaGroup(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id201_XmlSchemaGroup || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaGroup xmlSchemaGroup = new XmlSchemaGroup();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaGroup.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaGroup.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaGroup.Namespaces == null)
				{
					xmlSchemaGroup.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaGroup.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaGroup.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaGroup.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaGroup;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaGroup.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id185_sequence && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaGroup.Particle = Read53_XmlSchemaSequence(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id186_choice && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaGroup.Particle = Read54_XmlSchemaChoice(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else if (!array2[5] && (object)base.Reader.LocalName == id187_all && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaGroup.Particle = Read55_XmlSchemaAll(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else
				{
					UnknownNode(xmlSchemaGroup, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:all");
				}
			}
			else
			{
				UnknownNode(xmlSchemaGroup, "http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:sequence, http://www.w3.org/2001/XMLSchema:choice, http://www.w3.org/2001/XMLSchema:all");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaGroup.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaGroup;
	}

	private XmlSchemaNotation Read65_XmlSchemaNotation(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id202_XmlSchemaNotation || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaNotation xmlSchemaNotation = new XmlSchemaNotation();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[7];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaNotation.Id = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id4_name && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaNotation.Name = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id203_public && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaNotation.Public = base.Reader.Value;
				array2[5] = true;
			}
			else if (!array2[6] && (object)base.Reader.LocalName == id204_system && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaNotation.System = base.Reader.Value;
				array2[6] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaNotation.Namespaces == null)
				{
					xmlSchemaNotation.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaNotation.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaNotation.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaNotation.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaNotation;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[2] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaNotation.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[2] = true;
				}
				else
				{
					UnknownNode(xmlSchemaNotation, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaNotation, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaNotation.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaNotation;
	}

	private XmlSchemaRedefine Read64_XmlSchemaRedefine(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id205_XmlSchemaRedefine || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaRedefine xmlSchemaRedefine = new XmlSchemaRedefine();
		XmlAttribute[] array = null;
		int num = 0;
		XmlSchemaObjectCollection items = xmlSchemaRedefine.Items;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id206_schemaLocation && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaRedefine.SchemaLocation = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[2] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaRedefine.Id = CollapseWhitespace(base.Reader.Value);
				array2[2] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaRedefine.Namespaces == null)
				{
					xmlSchemaRedefine.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaRedefine.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaRedefine.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaRedefine.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaRedefine;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id110_attributeGroup && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read40_XmlSchemaAttributeGroup(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id106_complexType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read62_XmlSchemaComplexType(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id105_simpleType && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read34_XmlSchemaSimpleType(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read11_XmlSchemaAnnotation(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id59_group && (object)base.Reader.NamespaceURI == id95_Item)
				{
					if (items == null)
					{
						base.Reader.Skip();
					}
					else
					{
						items.Add(Read63_XmlSchemaGroup(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(xmlSchemaRedefine, "http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:complexType, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:group");
				}
			}
			else
			{
				UnknownNode(xmlSchemaRedefine, "http://www.w3.org/2001/XMLSchema:attributeGroup, http://www.w3.org/2001/XMLSchema:complexType, http://www.w3.org/2001/XMLSchema:simpleType, http://www.w3.org/2001/XMLSchema:annotation, http://www.w3.org/2001/XMLSchema:group");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaRedefine.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaRedefine;
	}

	private XmlSchemaImport Read13_XmlSchemaImport(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id207_XmlSchemaImport || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id206_schemaLocation && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaImport.SchemaLocation = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[2] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaImport.Id = CollapseWhitespace(base.Reader.Value);
				array2[2] = true;
			}
			else if (!array2[4] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaImport.Namespace = CollapseWhitespace(base.Reader.Value);
				array2[4] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaImport.Namespaces == null)
				{
					xmlSchemaImport.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaImport.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaImport.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaImport.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaImport;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[5] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaImport.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[5] = true;
				}
				else
				{
					UnknownNode(xmlSchemaImport, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaImport, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaImport.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaImport;
	}

	private XmlSchemaInclude Read12_XmlSchemaInclude(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id208_XmlSchemaInclude || (object)xmlQualifiedName.Namespace != id95_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		base.DecodeName = false;
		XmlSchemaInclude xmlSchemaInclude = new XmlSchemaInclude();
		XmlAttribute[] array = null;
		int num = 0;
		bool[] array2 = new bool[5];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[1] && (object)base.Reader.LocalName == id206_schemaLocation && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaInclude.SchemaLocation = CollapseWhitespace(base.Reader.Value);
				array2[1] = true;
			}
			else if (!array2[2] && (object)base.Reader.LocalName == id102_id && (object)base.Reader.NamespaceURI == id5_Item)
			{
				xmlSchemaInclude.Id = CollapseWhitespace(base.Reader.Value);
				array2[2] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (xmlSchemaInclude.Namespaces == null)
				{
					xmlSchemaInclude.Namespaces = new XmlSerializerNamespaces();
				}
				xmlSchemaInclude.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		xmlSchemaInclude.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			xmlSchemaInclude.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return xmlSchemaInclude;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[4] && (object)base.Reader.LocalName == id107_annotation && (object)base.Reader.NamespaceURI == id95_Item)
				{
					xmlSchemaInclude.Annotation = Read11_XmlSchemaAnnotation(isNullable: false, checkType: true);
					array2[4] = true;
				}
				else
				{
					UnknownNode(xmlSchemaInclude, "http://www.w3.org/2001/XMLSchema:annotation");
				}
			}
			else
			{
				UnknownNode(xmlSchemaInclude, "http://www.w3.org/2001/XMLSchema:annotation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		xmlSchemaInclude.UnhandledAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return xmlSchemaInclude;
	}

	private Import Read4_Import(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id209_Import || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		Import import = new Import();
		XmlAttribute[] array = null;
		int num = 0;
		ServiceDescriptionFormatExtensionCollection extensions = import.Extensions;
		bool[] array2 = new bool[6];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array2[4] && (object)base.Reader.LocalName == id36_namespace && (object)base.Reader.NamespaceURI == id5_Item)
			{
				import.Namespace = base.Reader.Value;
				array2[4] = true;
			}
			else if (!array2[5] && (object)base.Reader.LocalName == id23_location && (object)base.Reader.NamespaceURI == id5_Item)
			{
				import.Location = base.Reader.Value;
				array2[5] = true;
			}
			else if (IsXmlnsAttribute(base.Reader.Name))
			{
				if (import.Namespaces == null)
				{
					import.Namespaces = new XmlSerializerNamespaces();
				}
				import.Namespaces.Add((base.Reader.Name.Length == 5) ? "" : base.Reader.LocalName, base.Reader.Value);
			}
			else
			{
				XmlAttribute xmlAttribute = (XmlAttribute)base.Document.ReadNode(base.Reader);
				ParseWsdlArrayType(xmlAttribute);
				array = (XmlAttribute[])EnsureArrayIndex(array, num, typeof(XmlAttribute));
				array[num++] = xmlAttribute;
			}
		}
		import.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			import.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
			return import;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array2[0] && (object)base.Reader.LocalName == id7_documentation && (object)base.Reader.NamespaceURI == id2_Item)
				{
					import.DocumentationElement = (XmlElement)ReadXmlNode(wrapped: false);
					array2[0] = true;
				}
				else
				{
					extensions.Add((XmlElement)ReadXmlNode(wrapped: false));
				}
			}
			else
			{
				UnknownNode(import, "http://schemas.xmlsoap.org/wsdl/:documentation");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		import.ExtensibleAttributes = (XmlAttribute[])ShrinkArray(array, num, typeof(XmlAttribute), isNullable: true);
		ReadEndElement();
		return import;
	}

	protected override void InitCallbacks()
	{
	}

	protected override void InitIDs()
	{
		id133_XmlSchemaSimpleTypeUnion = base.Reader.NameTable.Add("XmlSchemaSimpleTypeUnion");
		id143_maxInclusive = base.Reader.NameTable.Add("maxInclusive");
		id46_body = base.Reader.NameTable.Add("body");
		id190_any = base.Reader.NameTable.Add("any");
		id88_OperationOutput = base.Reader.NameTable.Add("OperationOutput");
		id6_targetNamespace = base.Reader.NameTable.Add("targetNamespace");
		id158_XmlSchemaMaxLengthFacet = base.Reader.NameTable.Add("XmlSchemaMaxLengthFacet");
		id11_portType = base.Reader.NameTable.Add("portType");
		id182_mixed = base.Reader.NameTable.Add("mixed");
		id172_keyref = base.Reader.NameTable.Add("keyref");
		id187_all = base.Reader.NameTable.Add("all");
		id162_itemType = base.Reader.NameTable.Add("itemType");
		id68_InputBinding = base.Reader.NameTable.Add("InputBinding");
		id25_HttpAddressBinding = base.Reader.NameTable.Add("HttpAddressBinding");
		id82_HttpBinding = base.Reader.NameTable.Add("HttpBinding");
		id17_address = base.Reader.NameTable.Add("address");
		id3_ServiceDescription = base.Reader.NameTable.Add("ServiceDescription");
		id38_SoapFaultBinding = base.Reader.NameTable.Add("SoapFaultBinding");
		id123_ref = base.Reader.NameTable.Add("ref");
		id198_XmlSchemaComplexContent = base.Reader.NameTable.Add("XmlSchemaComplexContent");
		id53_parts = base.Reader.NameTable.Add("parts");
		id35_use = base.Reader.NameTable.Add("use");
		id157_XmlSchemaLengthFacet = base.Reader.NameTable.Add("XmlSchemaLengthFacet");
		id207_XmlSchemaImport = base.Reader.NameTable.Add("XmlSchemaImport");
		id44_text = base.Reader.NameTable.Add("text");
		id117_XmlSchemaAppInfo = base.Reader.NameTable.Add("XmlSchemaAppInfo");
		id203_public = base.Reader.NameTable.Add("public");
		id69_urlEncoded = base.Reader.NameTable.Add("urlEncoded");
		id7_documentation = base.Reader.NameTable.Add("documentation");
		id19_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/soap/");
		id129_final = base.Reader.NameTable.Add("final");
		id163_XmlSchemaElement = base.Reader.NameTable.Add("XmlSchemaElement");
		id60_capture = base.Reader.NameTable.Add("capture");
		id37_encodingStyle = base.Reader.NameTable.Add("encodingStyle");
		id185_sequence = base.Reader.NameTable.Add("sequence");
		id166_abstract = base.Reader.NameTable.Add("abstract");
		id23_location = base.Reader.NameTable.Add("location");
		id111_XmlSchemaAttributeGroup = base.Reader.NameTable.Add("XmlSchemaAttributeGroup");
		id192_XmlSchemaSequence = base.Reader.NameTable.Add("XmlSchemaSequence");
		id33_FaultBinding = base.Reader.NameTable.Add("FaultBinding");
		id153_XmlSchemaMaxInclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMaxInclusiveFacet");
		id201_XmlSchemaGroup = base.Reader.NameTable.Add("XmlSchemaGroup");
		id43_multipartRelated = base.Reader.NameTable.Add("multipartRelated");
		id168_nillable = base.Reader.NameTable.Add("nillable");
		id149_value = base.Reader.NameTable.Add("value");
		id64_MimeMultipartRelatedBinding = base.Reader.NameTable.Add("MimeMultipartRelatedBinding");
		id193_XmlSchemaAny = base.Reader.NameTable.Add("XmlSchemaAny");
		id191_XmlSchemaGroupRef = base.Reader.NameTable.Add("XmlSchemaGroupRef");
		id74_soapAction = base.Reader.NameTable.Add("soapAction");
		id63_ignoreCase = base.Reader.NameTable.Add("ignoreCase");
		id101_version = base.Reader.NameTable.Add("version");
		id47_header = base.Reader.NameTable.Add("header");
		id195_extension = base.Reader.NameTable.Add("extension");
		id48_Soap12HeaderBinding = base.Reader.NameTable.Add("Soap12HeaderBinding");
		id134_memberTypes = base.Reader.NameTable.Add("memberTypes");
		id121_Item = base.Reader.NameTable.Add("http://www.w3.org/XML/1998/namespace");
		id146_minExclusive = base.Reader.NameTable.Add("minExclusive");
		id84_PortType = base.Reader.NameTable.Add("PortType");
		id42_mimeXml = base.Reader.NameTable.Add("mimeXml");
		id138_minInclusive = base.Reader.NameTable.Add("minInclusive");
		id118_source = base.Reader.NameTable.Add("source");
		id73_Soap12OperationBinding = base.Reader.NameTable.Add("Soap12OperationBinding");
		id131_restriction = base.Reader.NameTable.Add("restriction");
		id152_XmlSchemaMaxExclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMaxExclusiveFacet");
		id135_XmlSchemaSimpleTypeRestriction = base.Reader.NameTable.Add("XmlSchemaSimpleTypeRestriction");
		id188_XmlSchemaAll = base.Reader.NameTable.Add("XmlSchemaAll");
		id116_appinfo = base.Reader.NameTable.Add("appinfo");
		id86_parameterOrder = base.Reader.NameTable.Add("parameterOrder");
		id147_minLength = base.Reader.NameTable.Add("minLength");
		id78_HttpOperationBinding = base.Reader.NameTable.Add("HttpOperationBinding");
		id161_XmlSchemaSimpleTypeList = base.Reader.NameTable.Add("XmlSchemaSimpleTypeList");
		id205_XmlSchemaRedefine = base.Reader.NameTable.Add("XmlSchemaRedefine");
		id194_XmlSchemaSimpleContent = base.Reader.NameTable.Add("XmlSchemaSimpleContent");
		id91_MessagePart = base.Reader.NameTable.Add("MessagePart");
		id92_element = base.Reader.NameTable.Add("element");
		id114_processContents = base.Reader.NameTable.Add("processContents");
		id18_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/http/");
		id50_headerfault = base.Reader.NameTable.Add("headerfault");
		id154_XmlSchemaEnumerationFacet = base.Reader.NameTable.Add("XmlSchemaEnumerationFacet");
		id96_XmlSchema = base.Reader.NameTable.Add("XmlSchema");
		id127_form = base.Reader.NameTable.Add("form");
		id176_field = base.Reader.NameTable.Add("field");
		id49_part = base.Reader.NameTable.Add("part");
		id5_Item = base.Reader.NameTable.Add("");
		id57_match = base.Reader.NameTable.Add("match");
		id52_Soap12BodyBinding = base.Reader.NameTable.Add("Soap12BodyBinding");
		id104_redefine = base.Reader.NameTable.Add("redefine");
		id20_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/soap12/");
		id21_Soap12AddressBinding = base.Reader.NameTable.Add("Soap12AddressBinding");
		id142_enumeration = base.Reader.NameTable.Add("enumeration");
		id24_SoapAddressBinding = base.Reader.NameTable.Add("SoapAddressBinding");
		id103_include = base.Reader.NameTable.Add("include");
		id139_maxLength = base.Reader.NameTable.Add("maxLength");
		id165_maxOccurs = base.Reader.NameTable.Add("maxOccurs");
		id65_MimePart = base.Reader.NameTable.Add("MimePart");
		id102_id = base.Reader.NameTable.Add("id");
		id196_Item = base.Reader.NameTable.Add("XmlSchemaSimpleContentExtension");
		id140_length = base.Reader.NameTable.Add("length");
		id27_type = base.Reader.NameTable.Add("type");
		id106_complexType = base.Reader.NameTable.Add("complexType");
		id31_output = base.Reader.NameTable.Add("output");
		id1_definitions = base.Reader.NameTable.Add("definitions");
		id4_name = base.Reader.NameTable.Add("name");
		id132_union = base.Reader.NameTable.Add("union");
		id29_OperationBinding = base.Reader.NameTable.Add("OperationBinding");
		id170_key = base.Reader.NameTable.Add("key");
		id45_Item = base.Reader.NameTable.Add("http://microsoft.com/wsdl/mime/textMatching/");
		id95_Item = base.Reader.NameTable.Add("http://www.w3.org/2001/XMLSchema");
		id169_substitutionGroup = base.Reader.NameTable.Add("substitutionGroup");
		id178_xpath = base.Reader.NameTable.Add("xpath");
		id9_types = base.Reader.NameTable.Add("types");
		id97_attributeFormDefault = base.Reader.NameTable.Add("attributeFormDefault");
		id62_pattern = base.Reader.NameTable.Add("pattern");
		id58_MimeTextMatch = base.Reader.NameTable.Add("MimeTextMatch");
		id180_XmlSchemaKey = base.Reader.NameTable.Add("XmlSchemaKey");
		id10_message = base.Reader.NameTable.Add("message");
		id8_import = base.Reader.NameTable.Add("import");
		id148_XmlSchemaMinLengthFacet = base.Reader.NameTable.Add("XmlSchemaMinLengthFacet");
		id105_simpleType = base.Reader.NameTable.Add("simpleType");
		id181_XmlSchemaComplexType = base.Reader.NameTable.Add("XmlSchemaComplexType");
		id164_minOccurs = base.Reader.NameTable.Add("minOccurs");
		id144_maxExclusive = base.Reader.NameTable.Add("maxExclusive");
		id160_XmlSchemaFractionDigitsFacet = base.Reader.NameTable.Add("XmlSchemaFractionDigitsFacet");
		id124_XmlSchemaAttribute = base.Reader.NameTable.Add("XmlSchemaAttribute");
		id209_Import = base.Reader.NameTable.Add("Import");
		id206_schemaLocation = base.Reader.NameTable.Add("schemaLocation");
		id179_XmlSchemaUnique = base.Reader.NameTable.Add("XmlSchemaUnique");
		id75_style = base.Reader.NameTable.Add("style");
		id119_XmlSchemaDocumentation = base.Reader.NameTable.Add("XmlSchemaDocumentation");
		id136_base = base.Reader.NameTable.Add("base");
		id66_MimeXmlBinding = base.Reader.NameTable.Add("MimeXmlBinding");
		id30_input = base.Reader.NameTable.Add("input");
		id40_content = base.Reader.NameTable.Add("content");
		id93_Types = base.Reader.NameTable.Add("Types");
		id94_schema = base.Reader.NameTable.Add("schema");
		id200_Item = base.Reader.NameTable.Add("XmlSchemaComplexContentExtension");
		id67_MimeContentBinding = base.Reader.NameTable.Add("MimeContentBinding");
		id59_group = base.Reader.NameTable.Add("group");
		id32_fault = base.Reader.NameTable.Add("fault");
		id80_transport = base.Reader.NameTable.Add("transport");
		id98_blockDefault = base.Reader.NameTable.Add("blockDefault");
		id13_service = base.Reader.NameTable.Add("service");
		id54_SoapHeaderBinding = base.Reader.NameTable.Add("SoapHeaderBinding");
		id204_system = base.Reader.NameTable.Add("system");
		id16_Port = base.Reader.NameTable.Add("Port");
		id108_notation = base.Reader.NameTable.Add("notation");
		id186_choice = base.Reader.NameTable.Add("choice");
		id110_attributeGroup = base.Reader.NameTable.Add("attributeGroup");
		id79_Soap12Binding = base.Reader.NameTable.Add("Soap12Binding");
		id77_SoapOperationBinding = base.Reader.NameTable.Add("SoapOperationBinding");
		id115_XmlSchemaAnnotation = base.Reader.NameTable.Add("XmlSchemaAnnotation");
		id83_verb = base.Reader.NameTable.Add("verb");
		id72_HttpUrlEncodedBinding = base.Reader.NameTable.Add("HttpUrlEncodedBinding");
		id39_OutputBinding = base.Reader.NameTable.Add("OutputBinding");
		id183_complexContent = base.Reader.NameTable.Add("complexContent");
		id202_XmlSchemaNotation = base.Reader.NameTable.Add("XmlSchemaNotation");
		id81_SoapBinding = base.Reader.NameTable.Add("SoapBinding");
		id199_Item = base.Reader.NameTable.Add("XmlSchemaComplexContentRestriction");
		id28_operation = base.Reader.NameTable.Add("operation");
		id122_XmlSchemaAttributeGroupRef = base.Reader.NameTable.Add("XmlSchemaAttributeGroupRef");
		id155_XmlSchemaPatternFacet = base.Reader.NameTable.Add("XmlSchemaPatternFacet");
		id76_soapActionRequired = base.Reader.NameTable.Add("soapActionRequired");
		id90_Message = base.Reader.NameTable.Add("Message");
		id159_XmlSchemaMinInclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMinInclusiveFacet");
		id208_XmlSchemaInclude = base.Reader.NameTable.Add("XmlSchemaInclude");
		id85_Operation = base.Reader.NameTable.Add("Operation");
		id130_list = base.Reader.NameTable.Add("list");
		id14_Service = base.Reader.NameTable.Add("Service");
		id22_required = base.Reader.NameTable.Add("required");
		id174_refer = base.Reader.NameTable.Add("refer");
		id71_HttpUrlReplacementBinding = base.Reader.NameTable.Add("HttpUrlReplacementBinding");
		id56_MimeTextBinding = base.Reader.NameTable.Add("MimeTextBinding");
		id87_OperationFault = base.Reader.NameTable.Add("OperationFault");
		id125_default = base.Reader.NameTable.Add("default");
		id15_port = base.Reader.NameTable.Add("port");
		id51_SoapHeaderFaultBinding = base.Reader.NameTable.Add("SoapHeaderFaultBinding");
		id128_XmlSchemaSimpleType = base.Reader.NameTable.Add("XmlSchemaSimpleType");
		id36_namespace = base.Reader.NameTable.Add("namespace");
		id175_selector = base.Reader.NameTable.Add("selector");
		id150_XmlSchemaMinExclusiveFacet = base.Reader.NameTable.Add("XmlSchemaMinExclusiveFacet");
		id100_elementFormDefault = base.Reader.NameTable.Add("elementFormDefault");
		id26_Binding = base.Reader.NameTable.Add("Binding");
		id197_Item = base.Reader.NameTable.Add("XmlSchemaSimpleContentRestriction");
		id126_fixed = base.Reader.NameTable.Add("fixed");
		id107_annotation = base.Reader.NameTable.Add("annotation");
		id99_finalDefault = base.Reader.NameTable.Add("finalDefault");
		id137_fractionDigits = base.Reader.NameTable.Add("fractionDigits");
		id70_urlReplacement = base.Reader.NameTable.Add("urlReplacement");
		id189_XmlSchemaChoice = base.Reader.NameTable.Add("XmlSchemaChoice");
		id2_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/");
		id112_anyAttribute = base.Reader.NameTable.Add("anyAttribute");
		id89_OperationInput = base.Reader.NameTable.Add("OperationInput");
		id141_totalDigits = base.Reader.NameTable.Add("totalDigits");
		id61_repeats = base.Reader.NameTable.Add("repeats");
		id184_simpleContent = base.Reader.NameTable.Add("simpleContent");
		id55_SoapBodyBinding = base.Reader.NameTable.Add("SoapBodyBinding");
		id145_whiteSpace = base.Reader.NameTable.Add("whiteSpace");
		id167_block = base.Reader.NameTable.Add("block");
		id151_XmlSchemaWhiteSpaceFacet = base.Reader.NameTable.Add("XmlSchemaWhiteSpaceFacet");
		id12_binding = base.Reader.NameTable.Add("binding");
		id109_attribute = base.Reader.NameTable.Add("attribute");
		id171_unique = base.Reader.NameTable.Add("unique");
		id120_lang = base.Reader.NameTable.Add("lang");
		id173_XmlSchemaKeyref = base.Reader.NameTable.Add("XmlSchemaKeyref");
		id177_XmlSchemaXPath = base.Reader.NameTable.Add("XmlSchemaXPath");
		id34_Soap12FaultBinding = base.Reader.NameTable.Add("Soap12FaultBinding");
		id41_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/wsdl/mime/");
		id156_XmlSchemaTotalDigitsFacet = base.Reader.NameTable.Add("XmlSchemaTotalDigitsFacet");
		id113_XmlSchemaAnyAttribute = base.Reader.NameTable.Add("XmlSchemaAnyAttribute");
	}
}
