using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Services.Configuration;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides a means of creating and formatting a valid Web Services Description Language (WSDL) document file, complete with appropriate namespaces, elements, and attributes, for describing an XML Web service. This class cannot be inherited.</summary>
[XmlRoot("definitions", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
[XmlFormatExtensionPoint("Extensions")]
public sealed class ServiceDescription : NamedItem
{
	internal class ServiceDescriptionSerializer : XmlSerializer
	{
		protected override XmlSerializationReader CreateReader()
		{
			return new ServiceDescriptionSerializationReader();
		}

		protected override XmlSerializationWriter CreateWriter()
		{
			return new ServiceDescriptionSerializationWriter();
		}

		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("definitions", "http://schemas.xmlsoap.org/wsdl/");
		}

		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((ServiceDescriptionSerializationWriter)writer).Write125_definitions(objectToSerialize);
		}

		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((ServiceDescriptionSerializationReader)reader).Read125_definitions();
		}
	}

	/// <summary>The XML namespace in which the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class is defined ("http://schemas.xmlsoap.org/wsdl/"). This field is constant.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/wsdl/";

	internal const string Prefix = "wsdl";

	private Types types;

	private ImportCollection imports;

	private MessageCollection messages;

	private PortTypeCollection portTypes;

	private BindingCollection bindings;

	private ServiceCollection services;

	private string targetNamespace;

	private ServiceDescriptionFormatExtensionCollection extensions;

	private ServiceDescriptionCollection parent;

	private string appSettingUrlKey;

	private string appSettingBaseUrl;

	private string retrievalUrl;

	private static XmlSerializer serializer;

	private static XmlSerializerNamespaces namespaces;

	private const WsiProfiles SupportedClaims = WsiProfiles.BasicProfile1_1;

	private static XmlSchema schema = null;

	private static XmlSchema soapEncodingSchema = null;

	private StringCollection validationWarnings;

	private static StringCollection warnings = new StringCollection();

	private ServiceDescription next;

	/// <summary>Gets or sets the URL of the XML Web service to which the <see cref="T:System.Web.Services.Description.ServiceDescription" /> instance applies.</summary>
	/// <returns>The URL of the XML Web service. The default value is an empty string ("").</returns>
	[XmlIgnore]
	public string RetrievalUrl
	{
		get
		{
			if (retrievalUrl != null)
			{
				return retrievalUrl;
			}
			return string.Empty;
		}
		set
		{
			retrievalUrl = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> instance of which the <see cref="T:System.Web.Services.Description.ServiceDescription" /> is a member.</summary>
	/// <returns>A collection of service description.</returns>
	/// <exception cref="T:System.NullReferenceException">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> has not been assigned to a parent collection. </exception>
	[XmlIgnore]
	public ServiceDescriptionCollection ServiceDescriptions => parent;

	/// <summary>Gets the collection of extensibility elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>The collection of extensibility elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	[XmlIgnore]
	public override ServiceDescriptionFormatExtensionCollection Extensions
	{
		get
		{
			if (extensions == null)
			{
				extensions = new ServiceDescriptionFormatExtensionCollection(this);
			}
			return extensions;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Import" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>A collection of import elements contained in the service description.</returns>
	[XmlElement("import")]
	public ImportCollection Imports
	{
		get
		{
			if (imports == null)
			{
				imports = new ImportCollection(this);
			}
			return imports;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.Types" /> contained by the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.Types" /> instance that represents the data types of both the parameters and return values of the methods exposed by the XML Web service.</returns>
	[XmlElement("types")]
	public Types Types
	{
		get
		{
			if (types == null)
			{
				types = new Types();
			}
			return types;
		}
		set
		{
			types = value;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Message" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>A collection of message elements contained in the service description.</returns>
	[XmlElement("message")]
	public MessageCollection Messages
	{
		get
		{
			if (messages == null)
			{
				messages = new MessageCollection(this);
			}
			return messages;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.PortType" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>A collection of <see cref="T:System.Web.Services.Description.PortType" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	[XmlElement("portType")]
	public PortTypeCollection PortTypes
	{
		get
		{
			if (portTypes == null)
			{
				portTypes = new PortTypeCollection(this);
			}
			return portTypes;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Binding" /> elements contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>A collection of binding elements contained in the service description.</returns>
	[XmlElement("binding")]
	public BindingCollection Bindings
	{
		get
		{
			if (bindings == null)
			{
				bindings = new BindingCollection(this);
			}
			return bindings;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Service" /> instances contained in the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>A collection of service instances contained in the service description.</returns>
	[XmlElement("service")]
	public ServiceCollection Services
	{
		get
		{
			if (services == null)
			{
				services = new ServiceCollection(this);
			}
			return services;
		}
	}

	/// <summary>Gets or sets the XML <see langword="targetNamespace" /> attribute of the <see langword="descriptions" /> tag enclosing a Web Services Description Language (WSDL) file.</summary>
	/// <returns>The URL of the XML Web service described by the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	[XmlAttribute("targetNamespace")]
	public string TargetNamespace
	{
		get
		{
			return targetNamespace;
		}
		set
		{
			targetNamespace = value;
		}
	}

	/// <summary>Gets the schema associated with this <see cref="T:System.Web.Services.Description.ServiceDescription" />.</summary>
	/// <returns>The schema associated with this <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static XmlSchema Schema
	{
		get
		{
			if (schema == null)
			{
				schema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?> \n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\n           xmlns:wsdl='http://schemas.xmlsoap.org/wsdl/'\n           targetNamespace='http://schemas.xmlsoap.org/wsdl/'\n           elementFormDefault='qualified' >\n   \n  <xs:complexType mixed='true' name='tDocumentation' >\n    <xs:sequence>\n      <xs:any minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n    </xs:sequence>\n  </xs:complexType>\n\n  <xs:complexType name='tDocumented' >\n    <xs:annotation>\n      <xs:documentation>\n      This type is extended by  component types to allow them to be documented\n      </xs:documentation>\n    </xs:annotation>\n    <xs:sequence>\n      <xs:element name='documentation' type='wsdl:tDocumentation' minOccurs='0' />\n    </xs:sequence>\n  </xs:complexType>\n <!-- allow extensibility via elements and attributes on all elements swa124 -->\n <xs:complexType name='tExtensibleAttributesDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow attributes from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:sequence>\n          <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n        </xs:sequence>\n        <xs:anyAttribute namespace='##other' processContents='lax' />   \n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <xs:complexType name='tExtensibleDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow elements from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:sequence>\n          <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n        </xs:sequence>\n        <xs:anyAttribute namespace='##other' processContents='lax' />   \n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  <!-- original wsdl removed as part of swa124 resolution\n  <xs:complexType name='tExtensibleAttributesDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow attributes from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:anyAttribute namespace='##other' processContents='lax' />    \n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:complexType name='tExtensibleDocumented' abstract='true' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tDocumented' >\n        <xs:annotation>\n          <xs:documentation>\n          This type is extended by component types to allow elements from other namespaces to be added.\n          </xs:documentation>\n        </xs:annotation>\n        <xs:sequence>\n          <xs:any namespace='##other' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n        </xs:sequence>\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n -->\n  <xs:element name='definitions' type='wsdl:tDefinitions' >\n    <xs:key name='message' >\n      <xs:selector xpath='wsdl:message' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='portType' >\n      <xs:selector xpath='wsdl:portType' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='binding' >\n      <xs:selector xpath='wsdl:binding' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='service' >\n      <xs:selector xpath='wsdl:service' />\n      <xs:field xpath='@name' />\n    </xs:key>\n    <xs:key name='import' >\n      <xs:selector xpath='wsdl:import' />\n      <xs:field xpath='@namespace' />\n    </xs:key>\n  </xs:element>\n\n  <xs:group name='anyTopLevelOptionalElement' >\n    <xs:annotation>\n      <xs:documentation>\n      Any top level optional element allowed to appear more then once - any child of definitions element except wsdl:types. Any extensibility element is allowed in any place.\n      </xs:documentation>\n    </xs:annotation>\n    <xs:choice>\n      <xs:element name='import' type='wsdl:tImport' />\n      <xs:element name='types' type='wsdl:tTypes' />                     \n      <xs:element name='message'  type='wsdl:tMessage' >\n        <xs:unique name='part' >\n          <xs:selector xpath='wsdl:part' />\n          <xs:field xpath='@name' />\n        </xs:unique>\n      </xs:element>\n      <xs:element name='portType' type='wsdl:tPortType' />\n      <xs:element name='binding'  type='wsdl:tBinding' />\n      <xs:element name='service'  type='wsdl:tService' >\n        <xs:unique name='port' >\n          <xs:selector xpath='wsdl:port' />\n          <xs:field xpath='@name' />\n        </xs:unique>\n      </xs:element>\n    </xs:choice>\n  </xs:group>\n\n  <xs:complexType name='tDefinitions' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:group ref='wsdl:anyTopLevelOptionalElement'  minOccurs='0'   maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='targetNamespace' type='xs:anyURI' use='optional' />\n        <xs:attribute name='name' type='xs:NCName' use='optional' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n   \n  <xs:complexType name='tImport' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='namespace' type='xs:anyURI' use='required' />\n        <xs:attribute name='location' type='xs:anyURI' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n   \n  <xs:complexType name='tTypes' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleDocumented' />\n    </xs:complexContent>   \n  </xs:complexType>\n     \n  <xs:complexType name='tMessage' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='part' type='wsdl:tPart' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n\n  <xs:complexType name='tPart' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='element' type='xs:QName' use='optional' />\n        <xs:attribute name='type' type='xs:QName' use='optional' />    \n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n\n  <xs:complexType name='tPortType' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:sequence>\n          <xs:element name='operation' type='wsdl:tOperation' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n   \n  <xs:complexType name='tOperation' >\n    <xs:complexContent>   \n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:choice>\n            <xs:group ref='wsdl:request-response-or-one-way-operation' />\n            <xs:group ref='wsdl:solicit-response-or-notification-operation' />\n          </xs:choice>\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='parameterOrder' type='xs:NMTOKENS' use='optional' />\n      </xs:extension>\n    </xs:complexContent>   \n  </xs:complexType>\n    \n  <xs:group name='request-response-or-one-way-operation' >\n    <xs:sequence>\n      <xs:element name='input' type='wsdl:tParam' />\n      <xs:sequence minOccurs='0' >\n        <xs:element name='output' type='wsdl:tParam' />\n        <xs:element name='fault' type='wsdl:tFault' minOccurs='0' maxOccurs='unbounded' />\n      </xs:sequence>\n    </xs:sequence>\n  </xs:group>\n\n  <xs:group name='solicit-response-or-notification-operation' >\n    <xs:sequence>\n      <xs:element name='output' type='wsdl:tParam' />\n      <xs:sequence minOccurs='0' >\n        <xs:element name='input' type='wsdl:tParam' />\n        <xs:element name='fault' type='wsdl:tFault' minOccurs='0' maxOccurs='unbounded' />\n      </xs:sequence>\n    </xs:sequence>\n  </xs:group>\n        \n  <xs:complexType name='tParam' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='optional' />\n        <xs:attribute name='message' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:complexType name='tFault' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleAttributesDocumented' >\n        <xs:attribute name='name' type='xs:NCName'  use='required' />\n        <xs:attribute name='message' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n     \n  <xs:complexType name='tBinding' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='operation' type='wsdl:tBindingOperation' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='type' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n    \n  <xs:complexType name='tBindingOperationMessage' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='optional' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n  \n  <xs:complexType name='tBindingOperationFault' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:complexType name='tBindingOperation' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='input' type='wsdl:tBindingOperationMessage' minOccurs='0' />\n          <xs:element name='output' type='wsdl:tBindingOperationMessage' minOccurs='0' />\n          <xs:element name='fault' type='wsdl:tBindingOperationFault' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n     \n  <xs:complexType name='tService' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:sequence>\n          <xs:element name='port' type='wsdl:tPort' minOccurs='0' maxOccurs='unbounded' />\n        </xs:sequence>\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n     \n  <xs:complexType name='tPort' >\n    <xs:complexContent>\n      <xs:extension base='wsdl:tExtensibleDocumented' >\n        <xs:attribute name='name' type='xs:NCName' use='required' />\n        <xs:attribute name='binding' type='xs:QName' use='required' />\n      </xs:extension>\n    </xs:complexContent>\n  </xs:complexType>\n\n  <xs:attribute name='arrayType' type='xs:string' />\n  <xs:attribute name='required' type='xs:boolean' />\n  <xs:complexType name='tExtensibilityElement' abstract='true' >\n    <xs:attribute ref='wsdl:required' use='optional' />\n  </xs:complexType>\n\n</xs:schema>"), null);
			}
			return schema;
		}
	}

	internal static XmlSchema SoapEncodingSchema
	{
		get
		{
			if (soapEncodingSchema == null)
			{
				soapEncodingSchema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?>\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema'\n           xmlns:tns='http://schemas.xmlsoap.org/soap/encoding/'\n           targetNamespace='http://schemas.xmlsoap.org/soap/encoding/' >\n        \n <xs:attribute name='root' >\n   <xs:simpleType>\n     <xs:restriction base='xs:boolean'>\n       <xs:pattern value='0|1' />\n     </xs:restriction>\n   </xs:simpleType>\n </xs:attribute>\n\n  <xs:attributeGroup name='commonAttributes' >\n    <xs:attribute name='id' type='xs:ID' />\n    <xs:attribute name='href' type='xs:anyURI' />\n    <xs:anyAttribute namespace='##other' processContents='lax' />\n  </xs:attributeGroup>\n   \n  <xs:simpleType name='arrayCoordinate' >\n    <xs:restriction base='xs:string' />\n  </xs:simpleType>\n          \n  <xs:attribute name='arrayType' type='xs:string' />\n  <xs:attribute name='offset' type='tns:arrayCoordinate' />\n  \n  <xs:attributeGroup name='arrayAttributes' >\n    <xs:attribute ref='tns:arrayType' />\n    <xs:attribute ref='tns:offset' />\n  </xs:attributeGroup>    \n  \n  <xs:attribute name='position' type='tns:arrayCoordinate' /> \n  \n  <xs:attributeGroup name='arrayMemberAttributes' >\n    <xs:attribute ref='tns:position' />\n  </xs:attributeGroup>    \n\n  <xs:group name='Array' >\n    <xs:sequence>\n      <xs:any namespace='##any' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n    </xs:sequence>\n  </xs:group>\n\n  <xs:element name='Array' type='tns:Array' />\n  <xs:complexType name='Array' >\n    <xs:group ref='tns:Array' minOccurs='0' />\n    <xs:attributeGroup ref='tns:arrayAttributes' />\n    <xs:attributeGroup ref='tns:commonAttributes' />\n  </xs:complexType> \n  <xs:element name='Struct' type='tns:Struct' />\n  <xs:group name='Struct' >\n    <xs:sequence>\n      <xs:any namespace='##any' minOccurs='0' maxOccurs='unbounded' processContents='lax' />\n    </xs:sequence>\n  </xs:group>\n\n  <xs:complexType name='Struct' >\n    <xs:group ref='tns:Struct' minOccurs='0' />\n    <xs:attributeGroup ref='tns:commonAttributes'/>\n  </xs:complexType> \n  \n  <xs:simpleType name='base64' >\n    <xs:restriction base='xs:base64Binary' />\n  </xs:simpleType>\n\n  <xs:element name='duration' type='tns:duration' />\n  <xs:complexType name='duration' >\n    <xs:simpleContent>\n      <xs:extension base='xs:duration' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='dateTime' type='tns:dateTime' />\n  <xs:complexType name='dateTime' >\n    <xs:simpleContent>\n      <xs:extension base='xs:dateTime' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n\n\n  <xs:element name='NOTATION' type='tns:NOTATION' />\n  <xs:complexType name='NOTATION' >\n    <xs:simpleContent>\n      <xs:extension base='xs:QName' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n  \n\n  <xs:element name='time' type='tns:time' />\n  <xs:complexType name='time' >\n    <xs:simpleContent>\n      <xs:extension base='xs:time' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='date' type='tns:date' />\n  <xs:complexType name='date' >\n    <xs:simpleContent>\n      <xs:extension base='xs:date' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gYearMonth' type='tns:gYearMonth' />\n  <xs:complexType name='gYearMonth' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gYearMonth' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gYear' type='tns:gYear' />\n  <xs:complexType name='gYear' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gYear' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gMonthDay' type='tns:gMonthDay' />\n  <xs:complexType name='gMonthDay' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gMonthDay' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gDay' type='tns:gDay' />\n  <xs:complexType name='gDay' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gDay' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='gMonth' type='tns:gMonth' />\n  <xs:complexType name='gMonth' >\n    <xs:simpleContent>\n      <xs:extension base='xs:gMonth' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n  \n  <xs:element name='boolean' type='tns:boolean' />\n  <xs:complexType name='boolean' >\n    <xs:simpleContent>\n      <xs:extension base='xs:boolean' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='base64Binary' type='tns:base64Binary' />\n  <xs:complexType name='base64Binary' >\n    <xs:simpleContent>\n      <xs:extension base='xs:base64Binary' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='hexBinary' type='tns:hexBinary' />\n  <xs:complexType name='hexBinary' >\n    <xs:simpleContent>\n     <xs:extension base='xs:hexBinary' >\n       <xs:attributeGroup ref='tns:commonAttributes' />\n     </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='float' type='tns:float' />\n  <xs:complexType name='float' >\n    <xs:simpleContent>\n      <xs:extension base='xs:float' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='double' type='tns:double' />\n  <xs:complexType name='double' >\n    <xs:simpleContent>\n      <xs:extension base='xs:double' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='anyURI' type='tns:anyURI' />\n  <xs:complexType name='anyURI' >\n    <xs:simpleContent>\n      <xs:extension base='xs:anyURI' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='QName' type='tns:QName' />\n  <xs:complexType name='QName' >\n    <xs:simpleContent>\n      <xs:extension base='xs:QName' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  \n  <xs:element name='string' type='tns:string' />\n  <xs:complexType name='string' >\n    <xs:simpleContent>\n      <xs:extension base='xs:string' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='normalizedString' type='tns:normalizedString' />\n  <xs:complexType name='normalizedString' >\n    <xs:simpleContent>\n      <xs:extension base='xs:normalizedString' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='token' type='tns:token' />\n  <xs:complexType name='token' >\n    <xs:simpleContent>\n      <xs:extension base='xs:token' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='language' type='tns:language' />\n  <xs:complexType name='language' >\n    <xs:simpleContent>\n      <xs:extension base='xs:language' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='Name' type='tns:Name' />\n  <xs:complexType name='Name' >\n    <xs:simpleContent>\n      <xs:extension base='xs:Name' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='NMTOKEN' type='tns:NMTOKEN' />\n  <xs:complexType name='NMTOKEN' >\n    <xs:simpleContent>\n      <xs:extension base='xs:NMTOKEN' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='NCName' type='tns:NCName' />\n  <xs:complexType name='NCName' >\n    <xs:simpleContent>\n      <xs:extension base='xs:NCName' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='NMTOKENS' type='tns:NMTOKENS' />\n  <xs:complexType name='NMTOKENS' >\n    <xs:simpleContent>\n      <xs:extension base='xs:NMTOKENS' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='ID' type='tns:ID' />\n  <xs:complexType name='ID' >\n    <xs:simpleContent>\n      <xs:extension base='xs:ID' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='IDREF' type='tns:IDREF' />\n  <xs:complexType name='IDREF' >\n    <xs:simpleContent>\n      <xs:extension base='xs:IDREF' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='ENTITY' type='tns:ENTITY' />\n  <xs:complexType name='ENTITY' >\n    <xs:simpleContent>\n      <xs:extension base='xs:ENTITY' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='IDREFS' type='tns:IDREFS' />\n  <xs:complexType name='IDREFS' >\n    <xs:simpleContent>\n      <xs:extension base='xs:IDREFS' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='ENTITIES' type='tns:ENTITIES' />\n  <xs:complexType name='ENTITIES' >\n    <xs:simpleContent>\n      <xs:extension base='xs:ENTITIES' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='decimal' type='tns:decimal' />\n  <xs:complexType name='decimal' >\n    <xs:simpleContent>\n      <xs:extension base='xs:decimal' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='integer' type='tns:integer' />\n  <xs:complexType name='integer' >\n    <xs:simpleContent>\n      <xs:extension base='xs:integer' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='nonPositiveInteger' type='tns:nonPositiveInteger' />\n  <xs:complexType name='nonPositiveInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:nonPositiveInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='negativeInteger' type='tns:negativeInteger' />\n  <xs:complexType name='negativeInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:negativeInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='long' type='tns:long' />\n  <xs:complexType name='long' >\n    <xs:simpleContent>\n      <xs:extension base='xs:long' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='int' type='tns:int' />\n  <xs:complexType name='int' >\n    <xs:simpleContent>\n      <xs:extension base='xs:int' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='short' type='tns:short' />\n  <xs:complexType name='short' >\n    <xs:simpleContent>\n      <xs:extension base='xs:short' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='byte' type='tns:byte' />\n  <xs:complexType name='byte' >\n    <xs:simpleContent>\n      <xs:extension base='xs:byte' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='nonNegativeInteger' type='tns:nonNegativeInteger' />\n  <xs:complexType name='nonNegativeInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:nonNegativeInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedLong' type='tns:unsignedLong' />\n  <xs:complexType name='unsignedLong' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedLong' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedInt' type='tns:unsignedInt' />\n  <xs:complexType name='unsignedInt' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedInt' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedShort' type='tns:unsignedShort' />\n  <xs:complexType name='unsignedShort' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedShort' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='unsignedByte' type='tns:unsignedByte' />\n  <xs:complexType name='unsignedByte' >\n    <xs:simpleContent>\n      <xs:extension base='xs:unsignedByte' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='positiveInteger' type='tns:positiveInteger' />\n  <xs:complexType name='positiveInteger' >\n    <xs:simpleContent>\n      <xs:extension base='xs:positiveInteger' >\n        <xs:attributeGroup ref='tns:commonAttributes' />\n      </xs:extension>\n    </xs:simpleContent>\n  </xs:complexType>\n\n  <xs:element name='anyType' />\n</xs:schema>"), null);
			}
			return soapEncodingSchema;
		}
	}

	/// <summary>Gets a <see cref="T:System.Collections.Specialized.StringCollection" /> that contains any validation warnings that were generated during a call to <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.Stream,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.TextReader,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.String,System.Boolean)" />, or <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.Xml.XmlReader,System.Boolean)" /> with the <paramref name="validate" /> parameter set to <see langword="true" />.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains any validation warnings that were generated during a call to <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.Stream,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.IO.TextReader,System.Boolean)" />, <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.String,System.Boolean)" />, or <see cref="M:System.Web.Services.Description.ServiceDescription.Read(System.Xml.XmlReader,System.Boolean)" /> with the <paramref name="validate" /> parameter set to <see langword="true" />.</returns>
	[XmlIgnore]
	public StringCollection ValidationWarnings
	{
		get
		{
			if (validationWarnings == null)
			{
				validationWarnings = new StringCollection();
			}
			return validationWarnings;
		}
	}

	/// <summary>Gets the XML serializer used to serialize and deserialize between a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object and a Web Services Description Language (WSDL) document.</summary>
	/// <returns>The XML serializer used to serialize and deserialize between a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object and a Web Services Description Language (WSDL) document.</returns>
	[XmlIgnore]
	public static XmlSerializer Serializer
	{
		get
		{
			if (serializer == null)
			{
				WebServicesSection current = WebServicesSection.Current;
				XmlAttributeOverrides overrides = new XmlAttributeOverrides();
				XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
				xmlSerializerNamespaces.Add("s", "http://www.w3.org/2001/XMLSchema");
				WebServicesSection.LoadXmlFormatExtensions(current.GetAllFormatExtensionTypes(), overrides, xmlSerializerNamespaces);
				namespaces = xmlSerializerNamespaces;
				if (current.ServiceDescriptionExtended)
				{
					serializer = new XmlSerializer(typeof(ServiceDescription), overrides);
				}
				else
				{
					serializer = new ServiceDescriptionSerializer();
				}
				serializer.UnknownElement += RuntimeUtils.OnUnknownElement;
			}
			return serializer;
		}
	}

	internal string AppSettingBaseUrl
	{
		get
		{
			return appSettingBaseUrl;
		}
		set
		{
			appSettingBaseUrl = value;
		}
	}

	internal string AppSettingUrlKey
	{
		get
		{
			return appSettingUrlKey;
		}
		set
		{
			appSettingUrlKey = value;
		}
	}

	internal ServiceDescription Next
	{
		get
		{
			return next;
		}
		set
		{
			next = value;
		}
	}

	private static void InstanceValidation(object sender, ValidationEventArgs args)
	{
		warnings.Add(Res.GetString("WsdlInstanceValidationDetails", args.Message, args.Exception.LineNumber.ToString(CultureInfo.InvariantCulture), args.Exception.LinePosition.ToString(CultureInfo.InvariantCulture)));
	}

	internal void SetParent(ServiceDescriptionCollection parent)
	{
		this.parent = parent;
	}

	private bool ShouldSerializeTypes()
	{
		return Types.HasItems();
	}

	internal void SetWarnings(StringCollection warnings)
	{
		validationWarnings = warnings;
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.TextReader" />.</summary>
	/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> instance, passed by reference, which contains the text to be read. </param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(TextReader textReader)
	{
		return Read(textReader, validate: false);
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.Stream" /> instance.</summary>
	/// <param name="stream">A <see cref="T:System.IO.Stream" />, passed by reference, which contains the bytes to be read.</param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(Stream stream)
	{
		return Read(stream, validate: false);
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from an <see cref="T:System.Xml.XmlReader" />.</summary>
	/// <param name="reader">An <see cref="T:System.Xml.XmlReader" />, passed by reference, which contains the XML data to be read. </param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(XmlReader reader)
	{
		return Read(reader, validate: false);
	}

	/// <summary>Initializes an instance of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object by directly loading the XML from the specified file.</summary>
	/// <param name="fileName">The path to the file to be read. </param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(string fileName)
	{
		return Read(fileName, validate: false);
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.TextReader" />.</summary>
	/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> instance, passed by reference, which contains the text to be read. </param>
	/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(TextReader textReader, bool validate)
	{
		return Read(new XmlTextReader(textReader)
		{
			WhitespaceHandling = WhitespaceHandling.Significant,
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		}, validate);
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from a <see cref="T:System.IO.Stream" /> instance.</summary>
	/// <param name="stream">A <see cref="T:System.IO.Stream" />, passed by reference, which contains the bytes to be read. </param>
	/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(Stream stream, bool validate)
	{
		return Read(new XmlTextReader(stream)
		{
			WhitespaceHandling = WhitespaceHandling.Significant,
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		}, validate);
	}

	/// <summary>Initializes an instance of a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object by directly loading the XML from the specified file.</summary>
	/// <param name="fileName">The path to the file to be read. </param>
	/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(string fileName, bool validate)
	{
		StreamReader streamReader = new StreamReader(fileName, Encoding.Default, detectEncodingFromByteOrderMarks: true);
		try
		{
			return Read(streamReader, validate);
		}
		finally
		{
			streamReader.Close();
		}
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class by directly loading the XML from an <see cref="T:System.Xml.XmlReader" />.</summary>
	/// <param name="reader">An <see cref="T:System.Xml.XmlReader" />, passed by reference, which contains the XML data to be read. </param>
	/// <param name="validate">A <see cref="T:System.Boolean" /> that indicates whether the XML should be validated against the schema specified by <see cref="P:System.Web.Services.Description.ServiceDescription.Schema" />.</param>
	/// <returns>An instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public static ServiceDescription Read(XmlReader reader, bool validate)
	{
		if (validate)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ValidationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
			xmlReaderSettings.Schemas.Add(Schema);
			xmlReaderSettings.Schemas.Add(SoapBinding.Schema);
			xmlReaderSettings.ValidationEventHandler += InstanceValidation;
			warnings.Clear();
			XmlReader xmlReader = XmlReader.Create(reader, xmlReaderSettings);
			if (reader.ReadState != 0)
			{
				xmlReader.Read();
			}
			ServiceDescription obj = (ServiceDescription)Serializer.Deserialize(xmlReader);
			obj.SetWarnings(warnings);
			return obj;
		}
		return (ServiceDescription)Serializer.Deserialize(reader);
	}

	/// <summary>Gets a value that indicates whether an <see cref="T:System.Xml.XmlReader" /> represents a valid Web Services Description Language (WSDL) file that can be parsed.</summary>
	/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /></param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Xml.Serialization.XmlSerializer" /> can recognize the node on which the <see cref="T:System.Xml.XmlReader" /> is positioned; otherwise <see langword="false" />.</returns>
	public static bool CanRead(XmlReader reader)
	{
		return Serializer.CanDeserialize(reader);
	}

	/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> as a Web Services Description Language (WSDL) file to the specified path.</summary>
	/// <param name="fileName">The path to which the WSDL file is written. </param>
	public void Write(string fileName)
	{
		StreamWriter streamWriter = new StreamWriter(fileName);
		try
		{
			Write(streamWriter);
		}
		finally
		{
			streamWriter.Close();
		}
	}

	/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> as a Web Services Description Language (WSDL) file to the <see cref="T:System.IO.TextWriter" />.</summary>
	/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> that contains the WSDL file produced. </param>
	public void Write(TextWriter writer)
	{
		XmlTextWriter xmlTextWriter = new XmlTextWriter(writer);
		xmlTextWriter.Formatting = Formatting.Indented;
		xmlTextWriter.Indentation = 2;
		Write(xmlTextWriter);
	}

	/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="stream">A <see cref="T:System.IO.Stream" />, passed by reference, which contains the Web Services Description Language (WSDL) file produced. </param>
	public void Write(Stream stream)
	{
		TextWriter textWriter = new StreamWriter(stream);
		Write(textWriter);
		textWriter.Flush();
	}

	/// <summary>Writes out the <see cref="T:System.Web.Services.Description.ServiceDescription" /> to the <see cref="T:System.Xml.XmlWriter" /> as a Web Services Description Language (WSDL) file.</summary>
	/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" />, passed by reference, which contains the WSDL file produced. </param>
	public void Write(XmlWriter writer)
	{
		XmlSerializer xmlSerializer = Serializer;
		XmlSerializerNamespaces xmlSerializerNamespaces;
		if (base.Namespaces == null || base.Namespaces.Count == 0)
		{
			xmlSerializerNamespaces = new XmlSerializerNamespaces(namespaces);
			xmlSerializerNamespaces.Add("wsdl", "http://schemas.xmlsoap.org/wsdl/");
			if (TargetNamespace != null && TargetNamespace.Length != 0)
			{
				xmlSerializerNamespaces.Add("tns", TargetNamespace);
			}
			for (int i = 0; i < Types.Schemas.Count; i++)
			{
				string text = Types.Schemas[i].TargetNamespace;
				if (text != null && text.Length > 0 && text != TargetNamespace && text != "http://schemas.xmlsoap.org/wsdl/")
				{
					xmlSerializerNamespaces.Add("s" + i.ToString(CultureInfo.InvariantCulture), text);
				}
			}
			for (int j = 0; j < Imports.Count; j++)
			{
				Import import = Imports[j];
				if (import.Namespace.Length > 0)
				{
					xmlSerializerNamespaces.Add("i" + j.ToString(CultureInfo.InvariantCulture), import.Namespace);
				}
			}
		}
		else
		{
			xmlSerializerNamespaces = base.Namespaces;
		}
		xmlSerializer.Serialize(writer, this, xmlSerializerNamespaces);
	}

	internal static WsiProfiles GetConformanceClaims(XmlElement documentation)
	{
		if (documentation == null)
		{
			return WsiProfiles.None;
		}
		WsiProfiles wsiProfiles = WsiProfiles.None;
		XmlNode xmlNode = documentation.FirstChild;
		while (xmlNode != null)
		{
			XmlNode nextSibling = xmlNode.NextSibling;
			if (xmlNode is XmlElement)
			{
				XmlElement xmlElement = (XmlElement)xmlNode;
				if (xmlElement.LocalName == "Claim" && xmlElement.NamespaceURI == "http://ws-i.org/schemas/conformanceClaim/" && "http://ws-i.org/profiles/basic/1.1" == xmlElement.GetAttribute("conformsTo"))
				{
					wsiProfiles |= WsiProfiles.BasicProfile1_1;
				}
			}
			xmlNode = nextSibling;
		}
		return wsiProfiles;
	}

	internal static void AddConformanceClaims(XmlElement documentation, WsiProfiles claims)
	{
		claims &= WsiProfiles.BasicProfile1_1;
		if (claims == WsiProfiles.None)
		{
			return;
		}
		WsiProfiles conformanceClaims = GetConformanceClaims(documentation);
		claims &= ~conformanceClaims;
		if (claims != 0)
		{
			XmlDocument ownerDocument = documentation.OwnerDocument;
			if ((claims & WsiProfiles.BasicProfile1_1) != 0)
			{
				XmlElement xmlElement = ownerDocument.CreateElement("wsi", "Claim", "http://ws-i.org/schemas/conformanceClaim/");
				xmlElement.SetAttribute("conformsTo", "http://ws-i.org/profiles/basic/1.1");
				documentation.InsertBefore(xmlElement, null);
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescription" /> class.</summary>
	public ServiceDescription()
	{
	}
}
