using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class ServiceDescriptionSerializationWriter : XmlSerializationWriter
{
	public void Write125_definitions(object o)
	{
		WriteStartDocument();
		if (o == null)
		{
			WriteNullTagLiteral("definitions", "http://schemas.xmlsoap.org/wsdl/");
			return;
		}
		TopLevelElement();
		Write124_ServiceDescription("definitions", "http://schemas.xmlsoap.org/wsdl/", (ServiceDescription)o, isNullable: true, needType: false);
	}

	private void Write124_ServiceDescription(string n, string ns, ServiceDescription o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(ServiceDescription)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("ServiceDescription", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("targetNamespace", "", o.TargetNamespace);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			ImportCollection imports = o.Imports;
			if (imports != null)
			{
				for (int k = 0; k < ((ICollection)imports).Count; k++)
				{
					Write4_Import("import", "http://schemas.xmlsoap.org/wsdl/", imports[k], isNullable: false, needType: false);
				}
			}
			Write67_Types("types", "http://schemas.xmlsoap.org/wsdl/", o.Types, isNullable: false, needType: false);
			MessageCollection messages = o.Messages;
			if (messages != null)
			{
				for (int l = 0; l < ((ICollection)messages).Count; l++)
				{
					Write69_Message("message", "http://schemas.xmlsoap.org/wsdl/", messages[l], isNullable: false, needType: false);
				}
			}
			PortTypeCollection portTypes = o.PortTypes;
			if (portTypes != null)
			{
				for (int m = 0; m < ((ICollection)portTypes).Count; m++)
				{
					Write75_PortType("portType", "http://schemas.xmlsoap.org/wsdl/", portTypes[m], isNullable: false, needType: false);
				}
			}
			BindingCollection bindings = o.Bindings;
			if (bindings != null)
			{
				for (int num = 0; num < ((ICollection)bindings).Count; num++)
				{
					Write117_Binding("binding", "http://schemas.xmlsoap.org/wsdl/", bindings[num], isNullable: false, needType: false);
				}
			}
			ServiceCollection services = o.Services;
			if (services != null)
			{
				for (int num2 = 0; num2 < ((ICollection)services).Count; num2++)
				{
					Write123_Service("service", "http://schemas.xmlsoap.org/wsdl/", services[num2], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write123_Service(string n, string ns, Service o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Service)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Service", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			PortCollection ports = o.Ports;
			if (ports != null)
			{
				for (int k = 0; k < ((ICollection)ports).Count; k++)
				{
					Write122_Port("port", "http://schemas.xmlsoap.org/wsdl/", ports[k], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write122_Port(string n, string ns, Port o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Port)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Port", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("binding", "", FromXmlQualifiedName(o.Binding));
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					object obj = extensions[j];
					if (obj is Soap12AddressBinding)
					{
						Write121_Soap12AddressBinding("address", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12AddressBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is HttpAddressBinding)
					{
						Write118_HttpAddressBinding("address", "http://schemas.xmlsoap.org/wsdl/http/", (HttpAddressBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapAddressBinding)
					{
						Write119_SoapAddressBinding("address", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapAddressBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw CreateInvalidAnyTypeException(xmlElement);
						}
						WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
					}
					else if (obj != null)
					{
						throw CreateUnknownTypeException(obj);
					}
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write119_SoapAddressBinding(string n, string ns, SoapAddressBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapAddressBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapAddressBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("location", "", o.Location);
		WriteEndElement(o);
	}

	private void Write118_HttpAddressBinding(string n, string ns, HttpAddressBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(HttpAddressBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("HttpAddressBinding", "http://schemas.xmlsoap.org/wsdl/http/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("location", "", o.Location);
		WriteEndElement(o);
	}

	private void Write121_Soap12AddressBinding(string n, string ns, Soap12AddressBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Soap12AddressBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("Soap12AddressBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("location", "", o.Location);
		WriteEndElement(o);
	}

	private void Write117_Binding(string n, string ns, Binding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Binding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Binding", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("type", "", FromXmlQualifiedName(o.Type));
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					object obj = extensions[j];
					if (obj is Soap12Binding)
					{
						Write84_Soap12Binding("binding", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12Binding)obj, isNullable: false, needType: false);
					}
					else if (obj is HttpBinding)
					{
						Write77_HttpBinding("binding", "http://schemas.xmlsoap.org/wsdl/http/", (HttpBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapBinding)
					{
						Write80_SoapBinding("binding", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw CreateInvalidAnyTypeException(xmlElement);
						}
						WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
					}
					else if (obj != null)
					{
						throw CreateUnknownTypeException(obj);
					}
				}
			}
			OperationBindingCollection operations = o.Operations;
			if (operations != null)
			{
				for (int k = 0; k < ((ICollection)operations).Count; k++)
				{
					Write116_OperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/", operations[k], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write116_OperationBinding(string n, string ns, OperationBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(OperationBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("OperationBinding", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					object obj = extensions[j];
					if (obj is Soap12OperationBinding)
					{
						Write88_Soap12OperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12OperationBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is HttpOperationBinding)
					{
						Write85_HttpOperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/http/", (HttpOperationBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapOperationBinding)
					{
						Write86_SoapOperationBinding("operation", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapOperationBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw CreateInvalidAnyTypeException(xmlElement);
						}
						WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
					}
					else if (obj != null)
					{
						throw CreateUnknownTypeException(obj);
					}
				}
			}
			Write110_InputBinding("input", "http://schemas.xmlsoap.org/wsdl/", o.Input, isNullable: false, needType: false);
			Write111_OutputBinding("output", "http://schemas.xmlsoap.org/wsdl/", o.Output, isNullable: false, needType: false);
			FaultBindingCollection faults = o.Faults;
			if (faults != null)
			{
				for (int k = 0; k < ((ICollection)faults).Count; k++)
				{
					Write115_FaultBinding("fault", "http://schemas.xmlsoap.org/wsdl/", faults[k], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write115_FaultBinding(string n, string ns, FaultBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(FaultBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("FaultBinding", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					object obj = extensions[j];
					if (obj is Soap12FaultBinding)
					{
						Write114_Soap12FaultBinding("fault", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12FaultBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapFaultBinding)
					{
						Write112_SoapFaultBinding("fault", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapFaultBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw CreateInvalidAnyTypeException(xmlElement);
						}
						WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
					}
					else if (obj != null)
					{
						throw CreateUnknownTypeException(obj);
					}
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write112_SoapFaultBinding(string n, string ns, SoapFaultBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapFaultBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapFaultBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write98_SoapBindingUse(o.Use));
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("namespace", "", o.Namespace);
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		WriteEndElement(o);
	}

	private string Write98_SoapBindingUse(SoapBindingUse v)
	{
		string text = null;
		return v switch
		{
			SoapBindingUse.Encoded => "encoded", 
			SoapBindingUse.Literal => "literal", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingUse"), 
		};
	}

	private void Write114_Soap12FaultBinding(string n, string ns, Soap12FaultBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Soap12FaultBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("Soap12FaultBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write100_SoapBindingUse(o.Use));
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("namespace", "", o.Namespace);
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		WriteEndElement(o);
	}

	private string Write100_SoapBindingUse(SoapBindingUse v)
	{
		string text = null;
		return v switch
		{
			SoapBindingUse.Encoded => "encoded", 
			SoapBindingUse.Literal => "literal", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingUse"), 
		};
	}

	private void Write111_OutputBinding(string n, string ns, OutputBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(OutputBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("OutputBinding", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					object obj = extensions[j];
					if (obj is Soap12BodyBinding)
					{
						Write102_Soap12BodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12BodyBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is Soap12HeaderBinding)
					{
						Write109_Soap12HeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12HeaderBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapHeaderBinding)
					{
						Write106_SoapHeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapHeaderBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapBodyBinding)
					{
						Write99_SoapBodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBodyBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeXmlBinding)
					{
						Write94_MimeXmlBinding("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeXmlBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeContentBinding)
					{
						Write93_MimeContentBinding("content", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeContentBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeTextBinding)
					{
						Write97_MimeTextBinding("text", "http://microsoft.com/wsdl/mime/textMatching/", (MimeTextBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeMultipartRelatedBinding)
					{
						Write104_MimeMultipartRelatedBinding("multipartRelated", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeMultipartRelatedBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw CreateInvalidAnyTypeException(xmlElement);
						}
						WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
					}
					else if (obj != null)
					{
						throw CreateUnknownTypeException(obj);
					}
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write104_MimeMultipartRelatedBinding(string n, string ns, MimeMultipartRelatedBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MimeMultipartRelatedBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("MimeMultipartRelatedBinding", "http://schemas.xmlsoap.org/wsdl/mime/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		MimePartCollection parts = o.Parts;
		if (parts != null)
		{
			for (int i = 0; i < ((ICollection)parts).Count; i++)
			{
				Write103_MimePart("part", "http://schemas.xmlsoap.org/wsdl/mime/", parts[i], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write103_MimePart(string n, string ns, MimePart o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MimePart)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("MimePart", "http://schemas.xmlsoap.org/wsdl/mime/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
		if (extensions != null)
		{
			for (int i = 0; i < ((ICollection)extensions).Count; i++)
			{
				object obj = extensions[i];
				if (obj is Soap12BodyBinding)
				{
					Write102_Soap12BodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12BodyBinding)obj, isNullable: false, needType: false);
				}
				else if (obj is SoapBodyBinding)
				{
					Write99_SoapBodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBodyBinding)obj, isNullable: false, needType: false);
				}
				else if (obj is MimeContentBinding)
				{
					Write93_MimeContentBinding("content", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeContentBinding)obj, isNullable: false, needType: false);
				}
				else if (obj is MimeXmlBinding)
				{
					Write94_MimeXmlBinding("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeXmlBinding)obj, isNullable: false, needType: false);
				}
				else if (obj is MimeTextBinding)
				{
					Write97_MimeTextBinding("text", "http://microsoft.com/wsdl/mime/textMatching/", (MimeTextBinding)obj, isNullable: false, needType: false);
				}
				else if (obj is XmlElement)
				{
					XmlElement xmlElement = (XmlElement)obj;
					if (xmlElement == null && xmlElement != null)
					{
						throw CreateInvalidAnyTypeException(xmlElement);
					}
					WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
				}
				else if (obj != null)
				{
					throw CreateUnknownTypeException(obj);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write97_MimeTextBinding(string n, string ns, MimeTextBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MimeTextBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("MimeTextBinding", "http://microsoft.com/wsdl/mime/textMatching/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		MimeTextMatchCollection matches = o.Matches;
		if (matches != null)
		{
			for (int i = 0; i < ((ICollection)matches).Count; i++)
			{
				Write96_MimeTextMatch("match", "http://microsoft.com/wsdl/mime/textMatching/", matches[i], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write96_MimeTextMatch(string n, string ns, MimeTextMatch o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MimeTextMatch)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("MimeTextMatch", "http://microsoft.com/wsdl/mime/textMatching/");
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("type", "", o.Type);
		if (o.Group != 1)
		{
			WriteAttribute("group", "", XmlConvert.ToString(o.Group));
		}
		if (o.Capture != 0)
		{
			WriteAttribute("capture", "", XmlConvert.ToString(o.Capture));
		}
		if (o.RepeatsString != "1")
		{
			WriteAttribute("repeats", "", o.RepeatsString);
		}
		WriteAttribute("pattern", "", o.Pattern);
		WriteAttribute("ignoreCase", "", XmlConvert.ToString(o.IgnoreCase));
		MimeTextMatchCollection matches = o.Matches;
		if (matches != null)
		{
			for (int i = 0; i < ((ICollection)matches).Count; i++)
			{
				Write96_MimeTextMatch("match", "http://microsoft.com/wsdl/mime/textMatching/", matches[i], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write94_MimeXmlBinding(string n, string ns, MimeXmlBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MimeXmlBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("MimeXmlBinding", "http://schemas.xmlsoap.org/wsdl/mime/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("part", "", o.Part);
		WriteEndElement(o);
	}

	private void Write93_MimeContentBinding(string n, string ns, MimeContentBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MimeContentBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("MimeContentBinding", "http://schemas.xmlsoap.org/wsdl/mime/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("part", "", o.Part);
		WriteAttribute("type", "", o.Type);
		WriteEndElement(o);
	}

	private void Write99_SoapBodyBinding(string n, string ns, SoapBodyBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapBodyBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapBodyBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write98_SoapBindingUse(o.Use));
		}
		if (o.Namespace != null && o.Namespace.Length != 0)
		{
			WriteAttribute("namespace", "", o.Namespace);
		}
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		WriteAttribute("parts", "", o.PartsString);
		WriteEndElement(o);
	}

	private void Write102_Soap12BodyBinding(string n, string ns, Soap12BodyBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Soap12BodyBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("Soap12BodyBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write100_SoapBindingUse(o.Use));
		}
		if (o.Namespace != null && o.Namespace.Length != 0)
		{
			WriteAttribute("namespace", "", o.Namespace);
		}
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		WriteAttribute("parts", "", o.PartsString);
		WriteEndElement(o);
	}

	private void Write106_SoapHeaderBinding(string n, string ns, SoapHeaderBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapHeaderBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapHeaderBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		WriteAttribute("part", "", o.Part);
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write98_SoapBindingUse(o.Use));
		}
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		if (o.Namespace != null && o.Namespace.Length != 0)
		{
			WriteAttribute("namespace", "", o.Namespace);
		}
		Write105_SoapHeaderFaultBinding("headerfault", "http://schemas.xmlsoap.org/wsdl/soap/", o.Fault, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write105_SoapHeaderFaultBinding(string n, string ns, SoapHeaderFaultBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapHeaderFaultBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapHeaderFaultBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		WriteAttribute("part", "", o.Part);
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write98_SoapBindingUse(o.Use));
		}
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		if (o.Namespace != null && o.Namespace.Length != 0)
		{
			WriteAttribute("namespace", "", o.Namespace);
		}
		WriteEndElement(o);
	}

	private void Write109_Soap12HeaderBinding(string n, string ns, Soap12HeaderBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Soap12HeaderBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("Soap12HeaderBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		WriteAttribute("part", "", o.Part);
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write100_SoapBindingUse(o.Use));
		}
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		if (o.Namespace != null && o.Namespace.Length != 0)
		{
			WriteAttribute("namespace", "", o.Namespace);
		}
		Write107_SoapHeaderFaultBinding("headerfault", "http://schemas.xmlsoap.org/wsdl/soap12/", o.Fault, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write107_SoapHeaderFaultBinding(string n, string ns, SoapHeaderFaultBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapHeaderFaultBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapHeaderFaultBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		WriteAttribute("part", "", o.Part);
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write100_SoapBindingUse(o.Use));
		}
		if (o.Encoding != null && o.Encoding.Length != 0)
		{
			WriteAttribute("encodingStyle", "", o.Encoding);
		}
		if (o.Namespace != null && o.Namespace.Length != 0)
		{
			WriteAttribute("namespace", "", o.Namespace);
		}
		WriteEndElement(o);
	}

	private void Write110_InputBinding(string n, string ns, InputBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(InputBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("InputBinding", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					object obj = extensions[j];
					if (obj is Soap12BodyBinding)
					{
						Write102_Soap12BodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12BodyBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is Soap12HeaderBinding)
					{
						Write109_Soap12HeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap12/", (Soap12HeaderBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapBodyBinding)
					{
						Write99_SoapBodyBinding("body", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapBodyBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is SoapHeaderBinding)
					{
						Write106_SoapHeaderBinding("header", "http://schemas.xmlsoap.org/wsdl/soap/", (SoapHeaderBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeTextBinding)
					{
						Write97_MimeTextBinding("text", "http://microsoft.com/wsdl/mime/textMatching/", (MimeTextBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is HttpUrlReplacementBinding)
					{
						Write91_HttpUrlReplacementBinding("urlReplacement", "http://schemas.xmlsoap.org/wsdl/http/", (HttpUrlReplacementBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is HttpUrlEncodedBinding)
					{
						Write90_HttpUrlEncodedBinding("urlEncoded", "http://schemas.xmlsoap.org/wsdl/http/", (HttpUrlEncodedBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeContentBinding)
					{
						Write93_MimeContentBinding("content", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeContentBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeMultipartRelatedBinding)
					{
						Write104_MimeMultipartRelatedBinding("multipartRelated", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeMultipartRelatedBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is MimeXmlBinding)
					{
						Write94_MimeXmlBinding("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", (MimeXmlBinding)obj, isNullable: false, needType: false);
					}
					else if (obj is XmlElement)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement == null && xmlElement != null)
						{
							throw CreateInvalidAnyTypeException(xmlElement);
						}
						WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
					}
					else if (obj != null)
					{
						throw CreateUnknownTypeException(obj);
					}
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write90_HttpUrlEncodedBinding(string n, string ns, HttpUrlEncodedBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(HttpUrlEncodedBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("HttpUrlEncodedBinding", "http://schemas.xmlsoap.org/wsdl/http/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteEndElement(o);
	}

	private void Write91_HttpUrlReplacementBinding(string n, string ns, HttpUrlReplacementBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(HttpUrlReplacementBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("HttpUrlReplacementBinding", "http://schemas.xmlsoap.org/wsdl/http/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteEndElement(o);
	}

	private void Write86_SoapOperationBinding(string n, string ns, SoapOperationBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapOperationBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapOperationBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("soapAction", "", o.SoapAction);
		if (o.Style != 0)
		{
			WriteAttribute("style", "", Write79_SoapBindingStyle(o.Style));
		}
		WriteEndElement(o);
	}

	private string Write79_SoapBindingStyle(SoapBindingStyle v)
	{
		string text = null;
		return v switch
		{
			SoapBindingStyle.Document => "document", 
			SoapBindingStyle.Rpc => "rpc", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingStyle"), 
		};
	}

	private void Write85_HttpOperationBinding(string n, string ns, HttpOperationBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(HttpOperationBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("HttpOperationBinding", "http://schemas.xmlsoap.org/wsdl/http/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("location", "", o.Location);
		WriteEndElement(o);
	}

	private void Write88_Soap12OperationBinding(string n, string ns, Soap12OperationBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Soap12OperationBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("Soap12OperationBinding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("soapAction", "", o.SoapAction);
		if (o.Style != 0)
		{
			WriteAttribute("style", "", Write82_SoapBindingStyle(o.Style));
		}
		if (o.SoapActionRequired)
		{
			WriteAttribute("soapActionRequired", "", XmlConvert.ToString(o.SoapActionRequired));
		}
		WriteEndElement(o);
	}

	private string Write82_SoapBindingStyle(SoapBindingStyle v)
	{
		string text = null;
		return v switch
		{
			SoapBindingStyle.Document => "document", 
			SoapBindingStyle.Rpc => "rpc", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Services.Description.SoapBindingStyle"), 
		};
	}

	private void Write80_SoapBinding(string n, string ns, SoapBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SoapBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SoapBinding", "http://schemas.xmlsoap.org/wsdl/soap/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("transport", "", o.Transport);
		if (o.Style != SoapBindingStyle.Document)
		{
			WriteAttribute("style", "", Write79_SoapBindingStyle(o.Style));
		}
		WriteEndElement(o);
	}

	private void Write77_HttpBinding(string n, string ns, HttpBinding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(HttpBinding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("HttpBinding", "http://schemas.xmlsoap.org/wsdl/http/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("verb", "", o.Verb);
		WriteEndElement(o);
	}

	private void Write84_Soap12Binding(string n, string ns, Soap12Binding o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Soap12Binding)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("Soap12Binding", "http://schemas.xmlsoap.org/wsdl/soap12/");
		}
		if (o.Required)
		{
			WriteAttribute("required", "http://schemas.xmlsoap.org/wsdl/", XmlConvert.ToString(o.Required));
		}
		WriteAttribute("transport", "", o.Transport);
		if (o.Style != SoapBindingStyle.Document)
		{
			WriteAttribute("style", "", Write82_SoapBindingStyle(o.Style));
		}
		WriteEndElement(o);
	}

	private void Write75_PortType(string n, string ns, PortType o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(PortType)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("PortType", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			OperationCollection operations = o.Operations;
			if (operations != null)
			{
				for (int k = 0; k < ((ICollection)operations).Count; k++)
				{
					Write74_Operation("operation", "http://schemas.xmlsoap.org/wsdl/", operations[k], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write74_Operation(string n, string ns, Operation o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Operation)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Operation", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.ParameterOrderString != null && o.ParameterOrderString.Length != 0)
		{
			WriteAttribute("parameterOrder", "", o.ParameterOrderString);
		}
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			OperationMessageCollection messages = o.Messages;
			if (messages != null)
			{
				for (int k = 0; k < ((ICollection)messages).Count; k++)
				{
					OperationMessage operationMessage = messages[k];
					if (operationMessage is OperationOutput)
					{
						Write72_OperationOutput("output", "http://schemas.xmlsoap.org/wsdl/", (OperationOutput)operationMessage, isNullable: false, needType: false);
					}
					else if (operationMessage is OperationInput)
					{
						Write71_OperationInput("input", "http://schemas.xmlsoap.org/wsdl/", (OperationInput)operationMessage, isNullable: false, needType: false);
					}
					else if (operationMessage != null)
					{
						throw CreateUnknownTypeException(operationMessage);
					}
				}
			}
			OperationFaultCollection faults = o.Faults;
			if (faults != null)
			{
				for (int l = 0; l < ((ICollection)faults).Count; l++)
				{
					Write73_OperationFault("fault", "http://schemas.xmlsoap.org/wsdl/", faults[l], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write73_OperationFault(string n, string ns, OperationFault o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(OperationFault)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("OperationFault", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write71_OperationInput(string n, string ns, OperationInput o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(OperationInput)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("OperationInput", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write72_OperationOutput(string n, string ns, OperationOutput o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(OperationOutput)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("OperationOutput", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("message", "", FromXmlQualifiedName(o.Message));
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write69_Message(string n, string ns, Message o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Message)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Message", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			MessagePartCollection parts = o.Parts;
			if (parts != null)
			{
				for (int k = 0; k < ((ICollection)parts).Count; k++)
				{
					Write68_MessagePart("part", "http://schemas.xmlsoap.org/wsdl/", parts[k], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write68_MessagePart(string n, string ns, MessagePart o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(MessagePart)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("MessagePart", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("element", "", FromXmlQualifiedName(o.Element));
		WriteAttribute("type", "", FromXmlQualifiedName(o.Type));
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write67_Types(string n, string ns, Types o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Types)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Types", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			XmlSchemas schemas = o.Schemas;
			if (schemas != null)
			{
				for (int k = 0; k < ((ICollection)schemas).Count; k++)
				{
					Write66_XmlSchema("schema", "http://www.w3.org/2001/XMLSchema", schemas[k], isNullable: false, needType: false);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	private void Write66_XmlSchema(string n, string ns, XmlSchema o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchema)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchema", "http://www.w3.org/2001/XMLSchema");
		}
		if (o.AttributeFormDefault != 0)
		{
			WriteAttribute("attributeFormDefault", "", Write6_XmlSchemaForm(o.AttributeFormDefault));
		}
		if (o.BlockDefault != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("blockDefault", "", Write7_XmlSchemaDerivationMethod(o.BlockDefault));
		}
		if (o.FinalDefault != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("finalDefault", "", Write7_XmlSchemaDerivationMethod(o.FinalDefault));
		}
		if (o.ElementFormDefault != 0)
		{
			WriteAttribute("elementFormDefault", "", Write6_XmlSchemaForm(o.ElementFormDefault));
		}
		WriteAttribute("targetNamespace", "", o.TargetNamespace);
		WriteAttribute("version", "", o.Version);
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		XmlSchemaObjectCollection includes = o.Includes;
		if (includes != null)
		{
			for (int j = 0; j < ((ICollection)includes).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = includes[j];
				if (xmlSchemaObject is XmlSchemaRedefine)
				{
					Write64_XmlSchemaRedefine("redefine", "http://www.w3.org/2001/XMLSchema", (XmlSchemaRedefine)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaImport)
				{
					Write13_XmlSchemaImport("import", "http://www.w3.org/2001/XMLSchema", (XmlSchemaImport)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaInclude)
				{
					Write12_XmlSchemaInclude("include", "http://www.w3.org/2001/XMLSchema", (XmlSchemaInclude)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		XmlSchemaObjectCollection items = o.Items;
		if (items != null)
		{
			for (int k = 0; k < ((ICollection)items).Count; k++)
			{
				XmlSchemaObject xmlSchemaObject2 = items[k];
				if (xmlSchemaObject2 is XmlSchemaElement)
				{
					Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaComplexType)
				{
					Write62_XmlSchemaComplexType("complexType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexType)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaSimpleType)
				{
					Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaAttributeGroup)
				{
					Write40_XmlSchemaAttributeGroup("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroup)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaNotation)
				{
					Write65_XmlSchemaNotation("notation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaNotation)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaGroup)
				{
					Write63_XmlSchemaGroup("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroup)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaAnnotation)
				{
					Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAnnotation)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject2);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write11_XmlSchemaAnnotation(string n, string ns, XmlSchemaAnnotation o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAnnotation)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAnnotation", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		XmlSchemaObjectCollection items = o.Items;
		if (items != null)
		{
			for (int j = 0; j < ((ICollection)items).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = items[j];
				if (xmlSchemaObject is XmlSchemaAppInfo)
				{
					Write10_XmlSchemaAppInfo("appinfo", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAppInfo)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaDocumentation)
				{
					Write9_XmlSchemaDocumentation("documentation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaDocumentation)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write9_XmlSchemaDocumentation(string n, string ns, XmlSchemaDocumentation o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaDocumentation)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaDocumentation", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("source", "", o.Source);
		WriteAttribute("lang", "http://www.w3.org/XML/1998/namespace", o.Language);
		XmlNode[] markup = o.Markup;
		if (markup != null)
		{
			foreach (XmlNode xmlNode in markup)
			{
				if (xmlNode is XmlElement)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement == null && xmlElement != null)
					{
						throw CreateInvalidAnyTypeException(xmlElement);
					}
					WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
				}
				else if (xmlNode != null)
				{
					xmlNode.WriteTo(base.Writer);
				}
				else if (xmlNode != null)
				{
					throw CreateUnknownTypeException(xmlNode);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write10_XmlSchemaAppInfo(string n, string ns, XmlSchemaAppInfo o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAppInfo)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAppInfo", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("source", "", o.Source);
		XmlNode[] markup = o.Markup;
		if (markup != null)
		{
			foreach (XmlNode xmlNode in markup)
			{
				if (xmlNode is XmlElement)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					if (xmlElement == null && xmlElement != null)
					{
						throw CreateInvalidAnyTypeException(xmlElement);
					}
					WriteElementLiteral(xmlElement, "", null, isNullable: false, any: true);
				}
				else if (xmlNode != null)
				{
					xmlNode.WriteTo(base.Writer);
				}
				else if (xmlNode != null)
				{
					throw CreateUnknownTypeException(xmlNode);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write63_XmlSchemaGroup(string n, string ns, XmlSchemaGroup o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaGroup)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaGroup", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.Particle is XmlSchemaAll)
		{
			Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaChoice)
		{
			Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaSequence)
		{
			Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle != null)
		{
			throw CreateUnknownTypeException(o.Particle);
		}
		WriteEndElement(o);
	}

	private void Write53_XmlSchemaSequence(string n, string ns, XmlSchemaSequence o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSequence)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSequence", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("minOccurs", "", o.MinOccursString);
		WriteAttribute("maxOccurs", "", o.MaxOccursString);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		XmlSchemaObjectCollection items = o.Items;
		if (items != null)
		{
			for (int j = 0; j < ((ICollection)items).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = items[j];
				if (xmlSchemaObject is XmlSchemaChoice)
				{
					Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaSequence)
				{
					Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaGroupRef)
				{
					Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaElement)
				{
					Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAny)
				{
					Write46_XmlSchemaAny("any", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAny)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write46_XmlSchemaAny(string n, string ns, XmlSchemaAny o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAny)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAny", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("minOccurs", "", o.MinOccursString);
		WriteAttribute("maxOccurs", "", o.MaxOccursString);
		WriteAttribute("namespace", "", o.Namespace);
		if (o.ProcessContents != 0)
		{
			WriteAttribute("processContents", "", Write38_XmlSchemaContentProcessing(o.ProcessContents));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private string Write38_XmlSchemaContentProcessing(XmlSchemaContentProcessing v)
	{
		string text = null;
		return v switch
		{
			XmlSchemaContentProcessing.Skip => "skip", 
			XmlSchemaContentProcessing.Lax => "lax", 
			XmlSchemaContentProcessing.Strict => "strict", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Xml.Schema.XmlSchemaContentProcessing"), 
		};
	}

	private void Write52_XmlSchemaElement(string n, string ns, XmlSchemaElement o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaElement)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaElement", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("minOccurs", "", o.MinOccursString);
		WriteAttribute("maxOccurs", "", o.MaxOccursString);
		if (o.IsAbstract)
		{
			WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
		}
		if (o.Block != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("block", "", Write7_XmlSchemaDerivationMethod(o.Block));
		}
		WriteAttribute("default", "", o.DefaultValue);
		if (o.Final != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("final", "", Write7_XmlSchemaDerivationMethod(o.Final));
		}
		WriteAttribute("fixed", "", o.FixedValue);
		if (o.Form != 0)
		{
			WriteAttribute("form", "", Write6_XmlSchemaForm(o.Form));
		}
		if (o.Name != null && o.Name.Length != 0)
		{
			WriteAttribute("name", "", o.Name);
		}
		if (o.IsNillable)
		{
			WriteAttribute("nillable", "", XmlConvert.ToString(o.IsNillable));
		}
		WriteAttribute("ref", "", FromXmlQualifiedName(o.RefName));
		WriteAttribute("substitutionGroup", "", FromXmlQualifiedName(o.SubstitutionGroup));
		WriteAttribute("type", "", FromXmlQualifiedName(o.SchemaTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.SchemaType is XmlSchemaComplexType)
		{
			Write62_XmlSchemaComplexType("complexType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexType)o.SchemaType, isNullable: false, needType: false);
		}
		else if (o.SchemaType is XmlSchemaSimpleType)
		{
			Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)o.SchemaType, isNullable: false, needType: false);
		}
		else if (o.SchemaType != null)
		{
			throw CreateUnknownTypeException(o.SchemaType);
		}
		XmlSchemaObjectCollection constraints = o.Constraints;
		if (constraints != null)
		{
			for (int j = 0; j < ((ICollection)constraints).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = constraints[j];
				if (xmlSchemaObject is XmlSchemaKeyref)
				{
					Write51_XmlSchemaKeyref("keyref", "http://www.w3.org/2001/XMLSchema", (XmlSchemaKeyref)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaUnique)
				{
					Write50_XmlSchemaUnique("unique", "http://www.w3.org/2001/XMLSchema", (XmlSchemaUnique)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaKey)
				{
					Write49_XmlSchemaKey("key", "http://www.w3.org/2001/XMLSchema", (XmlSchemaKey)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write49_XmlSchemaKey(string n, string ns, XmlSchemaKey o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaKey)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaKey", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write47_XmlSchemaXPath("selector", "http://www.w3.org/2001/XMLSchema", o.Selector, isNullable: false, needType: false);
		XmlSchemaObjectCollection fields = o.Fields;
		if (fields != null)
		{
			for (int j = 0; j < ((ICollection)fields).Count; j++)
			{
				Write47_XmlSchemaXPath("field", "http://www.w3.org/2001/XMLSchema", (XmlSchemaXPath)fields[j], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write47_XmlSchemaXPath(string n, string ns, XmlSchemaXPath o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaXPath)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaXPath", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		if (o.XPath != null && o.XPath.Length != 0)
		{
			WriteAttribute("xpath", "", o.XPath);
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write50_XmlSchemaUnique(string n, string ns, XmlSchemaUnique o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaUnique)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaUnique", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write47_XmlSchemaXPath("selector", "http://www.w3.org/2001/XMLSchema", o.Selector, isNullable: false, needType: false);
		XmlSchemaObjectCollection fields = o.Fields;
		if (fields != null)
		{
			for (int j = 0; j < ((ICollection)fields).Count; j++)
			{
				Write47_XmlSchemaXPath("field", "http://www.w3.org/2001/XMLSchema", (XmlSchemaXPath)fields[j], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write51_XmlSchemaKeyref(string n, string ns, XmlSchemaKeyref o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaKeyref)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaKeyref", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("refer", "", FromXmlQualifiedName(o.Refer));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write47_XmlSchemaXPath("selector", "http://www.w3.org/2001/XMLSchema", o.Selector, isNullable: false, needType: false);
		XmlSchemaObjectCollection fields = o.Fields;
		if (fields != null)
		{
			for (int j = 0; j < ((ICollection)fields).Count; j++)
			{
				Write47_XmlSchemaXPath("field", "http://www.w3.org/2001/XMLSchema", (XmlSchemaXPath)fields[j], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write34_XmlSchemaSimpleType(string n, string ns, XmlSchemaSimpleType o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleType)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleType", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.Final != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("final", "", Write7_XmlSchemaDerivationMethod(o.Final));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.Content is XmlSchemaSimpleTypeUnion)
		{
			Write33_XmlSchemaSimpleTypeUnion("union", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleTypeUnion)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content is XmlSchemaSimpleTypeRestriction)
		{
			Write32_XmlSchemaSimpleTypeRestriction("restriction", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleTypeRestriction)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content is XmlSchemaSimpleTypeList)
		{
			Write17_XmlSchemaSimpleTypeList("list", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleTypeList)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content != null)
		{
			throw CreateUnknownTypeException(o.Content);
		}
		WriteEndElement(o);
	}

	private void Write17_XmlSchemaSimpleTypeList(string n, string ns, XmlSchemaSimpleTypeList o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleTypeList)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleTypeList", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("itemType", "", FromXmlQualifiedName(o.ItemTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.ItemType, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write32_XmlSchemaSimpleTypeRestriction(string n, string ns, XmlSchemaSimpleTypeRestriction o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleTypeRestriction)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleTypeRestriction", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("base", "", FromXmlQualifiedName(o.BaseTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.BaseType, isNullable: false, needType: false);
		XmlSchemaObjectCollection facets = o.Facets;
		if (facets != null)
		{
			for (int j = 0; j < ((ICollection)facets).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = facets[j];
				if (xmlSchemaObject is XmlSchemaLengthFacet)
				{
					Write23_XmlSchemaLengthFacet("length", "http://www.w3.org/2001/XMLSchema", (XmlSchemaLengthFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaTotalDigitsFacet)
				{
					Write24_XmlSchemaTotalDigitsFacet("totalDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaTotalDigitsFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMaxLengthFacet)
				{
					Write22_XmlSchemaMaxLengthFacet("maxLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxLengthFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaFractionDigitsFacet)
				{
					Write20_XmlSchemaFractionDigitsFacet("fractionDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaFractionDigitsFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMinLengthFacet)
				{
					Write31_XmlSchemaMinLengthFacet("minLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinLengthFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMaxExclusiveFacet)
				{
					Write28_XmlSchemaMaxExclusiveFacet("maxExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxExclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaWhiteSpaceFacet)
				{
					Write29_XmlSchemaWhiteSpaceFacet("whiteSpace", "http://www.w3.org/2001/XMLSchema", (XmlSchemaWhiteSpaceFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMinExclusiveFacet)
				{
					Write30_XmlSchemaMinExclusiveFacet("minExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinExclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaPatternFacet)
				{
					Write25_XmlSchemaPatternFacet("pattern", "http://www.w3.org/2001/XMLSchema", (XmlSchemaPatternFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMinInclusiveFacet)
				{
					Write21_XmlSchemaMinInclusiveFacet("minInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinInclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMaxInclusiveFacet)
				{
					Write27_XmlSchemaMaxInclusiveFacet("maxInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxInclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaEnumerationFacet)
				{
					Write26_XmlSchemaEnumerationFacet("enumeration", "http://www.w3.org/2001/XMLSchema", (XmlSchemaEnumerationFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write26_XmlSchemaEnumerationFacet(string n, string ns, XmlSchemaEnumerationFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaEnumerationFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaEnumerationFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write27_XmlSchemaMaxInclusiveFacet(string n, string ns, XmlSchemaMaxInclusiveFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaMaxInclusiveFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaMaxInclusiveFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write21_XmlSchemaMinInclusiveFacet(string n, string ns, XmlSchemaMinInclusiveFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaMinInclusiveFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaMinInclusiveFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write25_XmlSchemaPatternFacet(string n, string ns, XmlSchemaPatternFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaPatternFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaPatternFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write30_XmlSchemaMinExclusiveFacet(string n, string ns, XmlSchemaMinExclusiveFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaMinExclusiveFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaMinExclusiveFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write29_XmlSchemaWhiteSpaceFacet(string n, string ns, XmlSchemaWhiteSpaceFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaWhiteSpaceFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaWhiteSpaceFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write28_XmlSchemaMaxExclusiveFacet(string n, string ns, XmlSchemaMaxExclusiveFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaMaxExclusiveFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaMaxExclusiveFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write31_XmlSchemaMinLengthFacet(string n, string ns, XmlSchemaMinLengthFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaMinLengthFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaMinLengthFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write20_XmlSchemaFractionDigitsFacet(string n, string ns, XmlSchemaFractionDigitsFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaFractionDigitsFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaFractionDigitsFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write22_XmlSchemaMaxLengthFacet(string n, string ns, XmlSchemaMaxLengthFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaMaxLengthFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaMaxLengthFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write24_XmlSchemaTotalDigitsFacet(string n, string ns, XmlSchemaTotalDigitsFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaTotalDigitsFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaTotalDigitsFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write23_XmlSchemaLengthFacet(string n, string ns, XmlSchemaLengthFacet o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaLengthFacet)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaLengthFacet", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("value", "", o.Value);
		if (o.IsFixed)
		{
			WriteAttribute("fixed", "", XmlConvert.ToString(o.IsFixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write33_XmlSchemaSimpleTypeUnion(string n, string ns, XmlSchemaSimpleTypeUnion o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleTypeUnion)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleTypeUnion", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		XmlQualifiedName[] memberTypes = o.MemberTypes;
		if (memberTypes != null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int j = 0; j < memberTypes.Length; j++)
			{
				XmlQualifiedName xmlQualifiedName = memberTypes[j];
				if (j != 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(FromXmlQualifiedName(xmlQualifiedName));
			}
			if (stringBuilder.Length != 0)
			{
				WriteAttribute("memberTypes", "", stringBuilder.ToString());
			}
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		XmlSchemaObjectCollection baseTypes = o.BaseTypes;
		if (baseTypes != null)
		{
			for (int k = 0; k < ((ICollection)baseTypes).Count; k++)
			{
				Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)baseTypes[k], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private string Write7_XmlSchemaDerivationMethod(XmlSchemaDerivationMethod v)
	{
		string text = null;
		return v switch
		{
			XmlSchemaDerivationMethod.Empty => "", 
			XmlSchemaDerivationMethod.Substitution => "substitution", 
			XmlSchemaDerivationMethod.Extension => "extension", 
			XmlSchemaDerivationMethod.Restriction => "restriction", 
			XmlSchemaDerivationMethod.List => "list", 
			XmlSchemaDerivationMethod.Union => "union", 
			XmlSchemaDerivationMethod.All => "#all", 
			_ => XmlSerializationWriter.FromEnum((long)v, new string[7] { "", "substitution", "extension", "restriction", "list", "union", "#all" }, new long[7] { 0L, 1L, 2L, 4L, 8L, 16L, 255L }, "System.Xml.Schema.XmlSchemaDerivationMethod"), 
		};
	}

	private void Write62_XmlSchemaComplexType(string n, string ns, XmlSchemaComplexType o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaComplexType)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaComplexType", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		if (o.Final != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("final", "", Write7_XmlSchemaDerivationMethod(o.Final));
		}
		if (o.IsAbstract)
		{
			WriteAttribute("abstract", "", XmlConvert.ToString(o.IsAbstract));
		}
		if (o.Block != XmlSchemaDerivationMethod.None)
		{
			WriteAttribute("block", "", Write7_XmlSchemaDerivationMethod(o.Block));
		}
		if (o.IsMixed)
		{
			WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.ContentModel is XmlSchemaSimpleContent)
		{
			Write61_XmlSchemaSimpleContent("simpleContent", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleContent)o.ContentModel, isNullable: false, needType: false);
		}
		else if (o.ContentModel is XmlSchemaComplexContent)
		{
			Write58_XmlSchemaComplexContent("complexContent", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexContent)o.ContentModel, isNullable: false, needType: false);
		}
		else if (o.ContentModel != null)
		{
			throw CreateUnknownTypeException(o.ContentModel);
		}
		if (o.Particle is XmlSchemaChoice)
		{
			Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaAll)
		{
			Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaSequence)
		{
			Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaGroupRef)
		{
			Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle != null)
		{
			throw CreateUnknownTypeException(o.Particle);
		}
		XmlSchemaObjectCollection attributes = o.Attributes;
		if (attributes != null)
		{
			for (int j = 0; j < ((ICollection)attributes).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = attributes[j];
				if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write39_XmlSchemaAnyAttribute(string n, string ns, XmlSchemaAnyAttribute o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAnyAttribute)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAnyAttribute", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("namespace", "", o.Namespace);
		if (o.ProcessContents != 0)
		{
			WriteAttribute("processContents", "", Write38_XmlSchemaContentProcessing(o.ProcessContents));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write36_XmlSchemaAttribute(string n, string ns, XmlSchemaAttribute o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAttribute)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAttribute", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("default", "", o.DefaultValue);
		WriteAttribute("fixed", "", o.FixedValue);
		if (o.Form != 0)
		{
			WriteAttribute("form", "", Write6_XmlSchemaForm(o.Form));
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("ref", "", FromXmlQualifiedName(o.RefName));
		WriteAttribute("type", "", FromXmlQualifiedName(o.SchemaTypeName));
		if (o.Use != 0)
		{
			WriteAttribute("use", "", Write35_XmlSchemaUse(o.Use));
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.SchemaType, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private string Write35_XmlSchemaUse(XmlSchemaUse v)
	{
		string text = null;
		return v switch
		{
			XmlSchemaUse.Optional => "optional", 
			XmlSchemaUse.Prohibited => "prohibited", 
			XmlSchemaUse.Required => "required", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Xml.Schema.XmlSchemaUse"), 
		};
	}

	private string Write6_XmlSchemaForm(XmlSchemaForm v)
	{
		string text = null;
		return v switch
		{
			XmlSchemaForm.Qualified => "qualified", 
			XmlSchemaForm.Unqualified => "unqualified", 
			_ => throw CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Xml.Schema.XmlSchemaForm"), 
		};
	}

	private void Write37_XmlSchemaAttributeGroupRef(string n, string ns, XmlSchemaAttributeGroupRef o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAttributeGroupRef)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAttributeGroupRef", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("ref", "", FromXmlQualifiedName(o.RefName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write44_XmlSchemaGroupRef(string n, string ns, XmlSchemaGroupRef o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaGroupRef)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaGroupRef", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("minOccurs", "", o.MinOccursString);
		WriteAttribute("maxOccurs", "", o.MaxOccursString);
		WriteAttribute("ref", "", FromXmlQualifiedName(o.RefName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write55_XmlSchemaAll(string n, string ns, XmlSchemaAll o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAll)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAll", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("minOccurs", "", o.MinOccursString);
		WriteAttribute("maxOccurs", "", o.MaxOccursString);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		XmlSchemaObjectCollection items = o.Items;
		if (items != null)
		{
			for (int j = 0; j < ((ICollection)items).Count; j++)
			{
				Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)items[j], isNullable: false, needType: false);
			}
		}
		WriteEndElement(o);
	}

	private void Write54_XmlSchemaChoice(string n, string ns, XmlSchemaChoice o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaChoice)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaChoice", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("minOccurs", "", o.MinOccursString);
		WriteAttribute("maxOccurs", "", o.MaxOccursString);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		XmlSchemaObjectCollection items = o.Items;
		if (items != null)
		{
			for (int j = 0; j < ((ICollection)items).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = items[j];
				if (xmlSchemaObject is XmlSchemaSequence)
				{
					Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaChoice)
				{
					Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaGroupRef)
				{
					Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaElement)
				{
					Write52_XmlSchemaElement("element", "http://www.w3.org/2001/XMLSchema", (XmlSchemaElement)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAny)
				{
					Write46_XmlSchemaAny("any", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAny)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write58_XmlSchemaComplexContent(string n, string ns, XmlSchemaComplexContent o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaComplexContent)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaComplexContent", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("mixed", "", XmlConvert.ToString(o.IsMixed));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.Content is XmlSchemaComplexContentRestriction)
		{
			Write57_Item("restriction", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexContentRestriction)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content is XmlSchemaComplexContentExtension)
		{
			Write56_Item("extension", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexContentExtension)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content != null)
		{
			throw CreateUnknownTypeException(o.Content);
		}
		WriteEndElement(o);
	}

	private void Write56_Item(string n, string ns, XmlSchemaComplexContentExtension o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaComplexContentExtension)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaComplexContentExtension", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("base", "", FromXmlQualifiedName(o.BaseTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.Particle is XmlSchemaAll)
		{
			Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaSequence)
		{
			Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaChoice)
		{
			Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaGroupRef)
		{
			Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle != null)
		{
			throw CreateUnknownTypeException(o.Particle);
		}
		XmlSchemaObjectCollection attributes = o.Attributes;
		if (attributes != null)
		{
			for (int j = 0; j < ((ICollection)attributes).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = attributes[j];
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write57_Item(string n, string ns, XmlSchemaComplexContentRestriction o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaComplexContentRestriction)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaComplexContentRestriction", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("base", "", FromXmlQualifiedName(o.BaseTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.Particle is XmlSchemaAll)
		{
			Write55_XmlSchemaAll("all", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAll)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaSequence)
		{
			Write53_XmlSchemaSequence("sequence", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSequence)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaChoice)
		{
			Write54_XmlSchemaChoice("choice", "http://www.w3.org/2001/XMLSchema", (XmlSchemaChoice)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle is XmlSchemaGroupRef)
		{
			Write44_XmlSchemaGroupRef("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroupRef)o.Particle, isNullable: false, needType: false);
		}
		else if (o.Particle != null)
		{
			throw CreateUnknownTypeException(o.Particle);
		}
		XmlSchemaObjectCollection attributes = o.Attributes;
		if (attributes != null)
		{
			for (int j = 0; j < ((ICollection)attributes).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = attributes[j];
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write61_XmlSchemaSimpleContent(string n, string ns, XmlSchemaSimpleContent o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleContent)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleContent", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		if (o.Content is XmlSchemaSimpleContentExtension)
		{
			Write60_Item("extension", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleContentExtension)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content is XmlSchemaSimpleContentRestriction)
		{
			Write59_Item("restriction", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleContentRestriction)o.Content, isNullable: false, needType: false);
		}
		else if (o.Content != null)
		{
			throw CreateUnknownTypeException(o.Content);
		}
		WriteEndElement(o);
	}

	private void Write59_Item(string n, string ns, XmlSchemaSimpleContentRestriction o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleContentRestriction)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleContentRestriction", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("base", "", FromXmlQualifiedName(o.BaseTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", o.BaseType, isNullable: false, needType: false);
		XmlSchemaObjectCollection facets = o.Facets;
		if (facets != null)
		{
			for (int j = 0; j < ((ICollection)facets).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = facets[j];
				if (xmlSchemaObject is XmlSchemaMinLengthFacet)
				{
					Write31_XmlSchemaMinLengthFacet("minLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinLengthFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMaxLengthFacet)
				{
					Write22_XmlSchemaMaxLengthFacet("maxLength", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxLengthFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaLengthFacet)
				{
					Write23_XmlSchemaLengthFacet("length", "http://www.w3.org/2001/XMLSchema", (XmlSchemaLengthFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaFractionDigitsFacet)
				{
					Write20_XmlSchemaFractionDigitsFacet("fractionDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaFractionDigitsFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaTotalDigitsFacet)
				{
					Write24_XmlSchemaTotalDigitsFacet("totalDigits", "http://www.w3.org/2001/XMLSchema", (XmlSchemaTotalDigitsFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMinExclusiveFacet)
				{
					Write30_XmlSchemaMinExclusiveFacet("minExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinExclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMaxInclusiveFacet)
				{
					Write27_XmlSchemaMaxInclusiveFacet("maxInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxInclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMaxExclusiveFacet)
				{
					Write28_XmlSchemaMaxExclusiveFacet("maxExclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMaxExclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaMinInclusiveFacet)
				{
					Write21_XmlSchemaMinInclusiveFacet("minInclusive", "http://www.w3.org/2001/XMLSchema", (XmlSchemaMinInclusiveFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaWhiteSpaceFacet)
				{
					Write29_XmlSchemaWhiteSpaceFacet("whiteSpace", "http://www.w3.org/2001/XMLSchema", (XmlSchemaWhiteSpaceFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaEnumerationFacet)
				{
					Write26_XmlSchemaEnumerationFacet("enumeration", "http://www.w3.org/2001/XMLSchema", (XmlSchemaEnumerationFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaPatternFacet)
				{
					Write25_XmlSchemaPatternFacet("pattern", "http://www.w3.org/2001/XMLSchema", (XmlSchemaPatternFacet)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		XmlSchemaObjectCollection attributes = o.Attributes;
		if (attributes != null)
		{
			for (int k = 0; k < ((ICollection)attributes).Count; k++)
			{
				XmlSchemaObject xmlSchemaObject2 = attributes[k];
				if (xmlSchemaObject2 is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 is XmlSchemaAttributeGroupRef)
				{
					Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject2, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject2 != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject2);
				}
			}
		}
		Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write60_Item(string n, string ns, XmlSchemaSimpleContentExtension o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaSimpleContentExtension)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaSimpleContentExtension", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("base", "", FromXmlQualifiedName(o.BaseTypeName));
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		XmlSchemaObjectCollection attributes = o.Attributes;
		if (attributes != null)
		{
			for (int j = 0; j < ((ICollection)attributes).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = attributes[j];
				if (xmlSchemaObject is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write65_XmlSchemaNotation(string n, string ns, XmlSchemaNotation o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaNotation)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaNotation", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		WriteAttribute("public", "", o.Public);
		WriteAttribute("system", "", o.System);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write40_XmlSchemaAttributeGroup(string n, string ns, XmlSchemaAttributeGroup o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaAttributeGroup)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaAttributeGroup", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("name", "", o.Name);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		XmlSchemaObjectCollection attributes = o.Attributes;
		if (attributes != null)
		{
			for (int j = 0; j < ((ICollection)attributes).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = attributes[j];
				if (xmlSchemaObject is XmlSchemaAttributeGroupRef)
				{
					Write37_XmlSchemaAttributeGroupRef("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroupRef)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAttribute)
				{
					Write36_XmlSchemaAttribute("attribute", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttribute)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		Write39_XmlSchemaAnyAttribute("anyAttribute", "http://www.w3.org/2001/XMLSchema", o.AnyAttribute, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write12_XmlSchemaInclude(string n, string ns, XmlSchemaInclude o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaInclude)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaInclude", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("schemaLocation", "", o.SchemaLocation);
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write13_XmlSchemaImport(string n, string ns, XmlSchemaImport o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaImport)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaImport", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("schemaLocation", "", o.SchemaLocation);
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("namespace", "", o.Namespace);
		Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", o.Annotation, isNullable: false, needType: false);
		WriteEndElement(o);
	}

	private void Write64_XmlSchemaRedefine(string n, string ns, XmlSchemaRedefine o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(XmlSchemaRedefine)))
		{
			throw CreateUnknownTypeException(o);
		}
		base.EscapeName = false;
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("XmlSchemaRedefine", "http://www.w3.org/2001/XMLSchema");
		}
		WriteAttribute("schemaLocation", "", o.SchemaLocation);
		WriteAttribute("id", "", o.Id);
		XmlAttribute[] unhandledAttributes = o.UnhandledAttributes;
		if (unhandledAttributes != null)
		{
			foreach (XmlAttribute node in unhandledAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		XmlSchemaObjectCollection items = o.Items;
		if (items != null)
		{
			for (int j = 0; j < ((ICollection)items).Count; j++)
			{
				XmlSchemaObject xmlSchemaObject = items[j];
				if (xmlSchemaObject is XmlSchemaSimpleType)
				{
					Write34_XmlSchemaSimpleType("simpleType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaSimpleType)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaComplexType)
				{
					Write62_XmlSchemaComplexType("complexType", "http://www.w3.org/2001/XMLSchema", (XmlSchemaComplexType)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaGroup)
				{
					Write63_XmlSchemaGroup("group", "http://www.w3.org/2001/XMLSchema", (XmlSchemaGroup)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAttributeGroup)
				{
					Write40_XmlSchemaAttributeGroup("attributeGroup", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAttributeGroup)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject is XmlSchemaAnnotation)
				{
					Write11_XmlSchemaAnnotation("annotation", "http://www.w3.org/2001/XMLSchema", (XmlSchemaAnnotation)xmlSchemaObject, isNullable: false, needType: false);
				}
				else if (xmlSchemaObject != null)
				{
					throw CreateUnknownTypeException(xmlSchemaObject);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write4_Import(string n, string ns, Import o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(Import)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, o.Namespaces);
		if (needType)
		{
			WriteXsiType("Import", "http://schemas.xmlsoap.org/wsdl/");
		}
		XmlAttribute[] extensibleAttributes = o.ExtensibleAttributes;
		if (extensibleAttributes != null)
		{
			foreach (XmlAttribute node in extensibleAttributes)
			{
				WriteXmlAttribute(node, o);
			}
		}
		WriteAttribute("namespace", "", o.Namespace);
		WriteAttribute("location", "", o.Location);
		if (o.DocumentationElement != null || o.DocumentationElement == null)
		{
			WriteElementLiteral(o.DocumentationElement, "documentation", "http://schemas.xmlsoap.org/wsdl/", isNullable: false, any: true);
			ServiceDescriptionFormatExtensionCollection extensions = o.Extensions;
			if (extensions != null)
			{
				for (int j = 0; j < ((ICollection)extensions).Count; j++)
				{
					if (extensions[j] is XmlNode || extensions[j] == null)
					{
						WriteElementLiteral((XmlNode)extensions[j], "", null, isNullable: false, any: true);
						continue;
					}
					throw CreateInvalidAnyTypeException(extensions[j]);
				}
			}
			WriteEndElement(o);
			return;
		}
		throw CreateInvalidAnyTypeException(o.DocumentationElement);
	}

	protected override void InitCallbacks()
	{
	}
}
