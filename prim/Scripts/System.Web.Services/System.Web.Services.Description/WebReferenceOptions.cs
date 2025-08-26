using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>The <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> class represents code generation options specified in an XML text file.</summary>
[XmlType("webReferenceOptions", Namespace = "http://microsoft.com/webReference/")]
[XmlRoot("webReferenceOptions", Namespace = "http://microsoft.com/webReference/")]
public class WebReferenceOptions
{
	/// <summary>A <see cref="T:System.String" /> that contains the target namespace for the <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
	public const string TargetNamespace = "http://microsoft.com/webReference/";

	private static XmlSchema schema;

	private CodeGenerationOptions codeGenerationOptions = CodeGenerationOptions.GenerateOldAsync;

	private ServiceDescriptionImportStyle style;

	private StringCollection schemaImporterExtensions;

	private bool verbose;

	/// <summary>Gets or sets the <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
	/// <returns>The <see cref="T:System.Xml.Serialization.CodeGenerationOptions" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
	[XmlElement("codeGenerationOptions")]
	[DefaultValue(CodeGenerationOptions.GenerateOldAsync)]
	public CodeGenerationOptions CodeGenerationOptions
	{
		get
		{
			return codeGenerationOptions;
		}
		set
		{
			codeGenerationOptions = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Collections.Specialized.StringCollection" /> that represents the schema importer extensions associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that represents the schema importer extensions associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
	[XmlArray("schemaImporterExtensions")]
	[XmlArrayItem("type")]
	public StringCollection SchemaImporterExtensions
	{
		get
		{
			if (schemaImporterExtensions == null)
			{
				schemaImporterExtensions = new StringCollection();
			}
			return schemaImporterExtensions;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.ServiceDescriptionImportStyle" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionImportStyle" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
	[DefaultValue(ServiceDescriptionImportStyle.Client)]
	[XmlElement("style")]
	public ServiceDescriptionImportStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			style = value;
		}
	}

	/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether verbose warning messages are to be generated during compilation of a client proxy or a server stub.</summary>
	/// <returns>
	///     <see langword="true" /> if verbose warning messages are to be generated during compilation of a client proxy or a server stub; otherwise, <see langword="false" />.</returns>
	[XmlElement("verbose")]
	public bool Verbose
	{
		get
		{
			return verbose;
		}
		set
		{
			verbose = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchema" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
	/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> associated with this <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</returns>
	public static XmlSchema Schema
	{
		get
		{
			if (schema == null)
			{
				schema = XmlSchema.Read(new StringReader("<?xml version='1.0' encoding='UTF-8' ?>\n<xs:schema xmlns:tns='http://microsoft.com/webReference/' elementFormDefault='qualified' targetNamespace='http://microsoft.com/webReference/' xmlns:xs='http://www.w3.org/2001/XMLSchema'>\n  <xs:simpleType name='options'>\n    <xs:list>\n      <xs:simpleType>\n        <xs:restriction base='xs:string'>\n          <xs:enumeration value='properties' />\n          <xs:enumeration value='newAsync' />\n          <xs:enumeration value='oldAsync' />\n          <xs:enumeration value='order' />\n          <xs:enumeration value='enableDataBinding' />\n        </xs:restriction>\n      </xs:simpleType>\n    </xs:list>\n  </xs:simpleType>\n  <xs:simpleType name='style'>\n    <xs:restriction base='xs:string'>\n      <xs:enumeration value='client' />\n      <xs:enumeration value='server' />\n      <xs:enumeration value='serverInterface' />\n    </xs:restriction>\n  </xs:simpleType>\n  <xs:complexType name='webReferenceOptions'>\n    <xs:all>\n      <xs:element minOccurs='0' default='oldAsync' name='codeGenerationOptions' type='tns:options' />\n      <xs:element minOccurs='0' default='client' name='style' type='tns:style' />\n      <xs:element minOccurs='0' default='false' name='verbose' type='xs:boolean' />\n      <xs:element minOccurs='0' name='schemaImporterExtensions'>\n        <xs:complexType>\n          <xs:sequence>\n            <xs:element minOccurs='0' maxOccurs='unbounded' name='type' type='xs:string' />\n          </xs:sequence>\n        </xs:complexType>\n      </xs:element>\n    </xs:all>\n  </xs:complexType>\n  <xs:element name='webReferenceOptions' type='tns:webReferenceOptions' />\n  <xs:complexType name='wsdlParameters'>\n    <xs:all>\n      <xs:element minOccurs='0' name='appSettingBaseUrl' type='xs:string' />\n      <xs:element minOccurs='0' name='appSettingUrlKey' type='xs:string' />\n      <xs:element minOccurs='0' name='domain' type='xs:string' />\n      <xs:element minOccurs='0' name='out' type='xs:string' />\n      <xs:element minOccurs='0' name='password' type='xs:string' />\n      <xs:element minOccurs='0' name='proxy' type='xs:string' />\n      <xs:element minOccurs='0' name='proxydomain' type='xs:string' />\n      <xs:element minOccurs='0' name='proxypassword' type='xs:string' />\n      <xs:element minOccurs='0' name='proxyusername' type='xs:string' />\n      <xs:element minOccurs='0' name='username' type='xs:string' />\n      <xs:element minOccurs='0' name='namespace' type='xs:string' />\n      <xs:element minOccurs='0' name='language' type='xs:string' />\n      <xs:element minOccurs='0' name='protocol' type='xs:string' />\n      <xs:element minOccurs='0' name='nologo' type='xs:boolean' />\n      <xs:element minOccurs='0' name='parsableerrors' type='xs:boolean' />\n      <xs:element minOccurs='0' name='sharetypes' type='xs:boolean' />\n      <xs:element minOccurs='0' name='webReferenceOptions' type='tns:webReferenceOptions' />\n      <xs:element minOccurs='0' name='documents'>\n        <xs:complexType>\n          <xs:sequence>\n            <xs:element minOccurs='0' maxOccurs='unbounded' name='document' type='xs:string' />\n          </xs:sequence>\n        </xs:complexType>\n      </xs:element>\n    </xs:all>\n  </xs:complexType>\n  <xs:element name='wsdlParameters' type='tns:wsdlParameters' />\n</xs:schema>"), null);
			}
			return schema;
		}
	}

	/// <summary>Returns a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.IO.TextReader" />.</summary>
	/// <param name="reader">The <see cref="T:System.IO.TextReader" /> that contains the code generation options.</param>
	/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> to be associated with the new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</param>
	/// <returns>A new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.IO.TextReader" />.</returns>
	public static WebReferenceOptions Read(TextReader reader, ValidationEventHandler validationEventHandler)
	{
		return Read(new XmlTextReader(reader)
		{
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		}, validationEventHandler);
	}

	/// <summary>Returns a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified stream.</summary>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> that contains the code generation options.</param>
	/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> to be associated with the new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</param>
	/// <returns>A new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified stream.</returns>
	public static WebReferenceOptions Read(Stream stream, ValidationEventHandler validationEventHandler)
	{
		return Read(new XmlTextReader(stream)
		{
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		}, validationEventHandler);
	}

	/// <summary>Returns a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.Xml.XmlReader" />.</summary>
	/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that contains the code generation options.</param>
	/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> to be associated with the new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</param>
	/// <returns>A new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" /> based on the code generation options described in the specified <see cref="T:System.Xml.XmlReader" />.</returns>
	public static WebReferenceOptions Read(XmlReader xmlReader, ValidationEventHandler validationEventHandler)
	{
		XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(xmlReader);
		xmlValidatingReader.ValidationType = ValidationType.Schema;
		if (validationEventHandler != null)
		{
			xmlValidatingReader.ValidationEventHandler += validationEventHandler;
		}
		else
		{
			xmlValidatingReader.ValidationEventHandler += SchemaValidationHandler;
		}
		xmlValidatingReader.Schemas.Add(Schema);
		webReferenceOptionsSerializer webReferenceOptionsSerializer2 = new webReferenceOptionsSerializer();
		try
		{
			return (WebReferenceOptions)webReferenceOptionsSerializer2.Deserialize(xmlValidatingReader);
		}
		catch (Exception ex)
		{
			throw ex;
		}
		finally
		{
			xmlValidatingReader.Close();
		}
	}

	private static void SchemaValidationHandler(object sender, ValidationEventArgs args)
	{
		if (args.Severity != 0)
		{
			return;
		}
		throw new InvalidOperationException(Res.GetString("WsdlInstanceValidationDetails", args.Message, args.Exception.LineNumber.ToString(CultureInfo.InvariantCulture), args.Exception.LinePosition.ToString(CultureInfo.InvariantCulture)));
	}

	/// <summary>Initializes a new instance of <see cref="T:System.Web.Services.Description.WebReferenceOptions" />.</summary>
	public WebReferenceOptions()
	{
	}
}
