using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

internal class DiscoveryDocumentSerializationWriter : XmlSerializationWriter
{
	public void Write10_discovery(object o)
	{
		WriteStartDocument();
		if (o == null)
		{
			WriteNullTagLiteral("discovery", "http://schemas.xmlsoap.org/disco/");
			return;
		}
		TopLevelElement();
		Write9_DiscoveryDocument("discovery", "http://schemas.xmlsoap.org/disco/", (DiscoveryDocument)o, isNullable: true, needType: false);
	}

	private void Write9_DiscoveryDocument(string n, string ns, DiscoveryDocument o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(DiscoveryDocument)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("DiscoveryDocument", "http://schemas.xmlsoap.org/disco/");
		}
		IList list = o.References;
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				object obj = list[i];
				if (obj is SchemaReference)
				{
					Write7_SchemaReference("schemaRef", "http://schemas.xmlsoap.org/disco/schema/", (SchemaReference)obj, isNullable: false, needType: false);
				}
				else if (obj is ContractReference)
				{
					Write5_ContractReference("contractRef", "http://schemas.xmlsoap.org/disco/scl/", (ContractReference)obj, isNullable: false, needType: false);
				}
				else if (obj is DiscoveryDocumentReference)
				{
					Write3_DiscoveryDocumentReference("discoveryRef", "http://schemas.xmlsoap.org/disco/", (DiscoveryDocumentReference)obj, isNullable: false, needType: false);
				}
				else if (obj is SoapBinding)
				{
					Write8_SoapBinding("soap", "http://schemas.xmlsoap.org/disco/soap/", (SoapBinding)obj, isNullable: false, needType: false);
				}
				else if (obj != null)
				{
					throw CreateUnknownTypeException(obj);
				}
			}
		}
		WriteEndElement(o);
	}

	private void Write8_SoapBinding(string n, string ns, SoapBinding o, bool isNullable, bool needType)
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
			WriteXsiType("SoapBinding", "http://schemas.xmlsoap.org/disco/soap/");
		}
		WriteAttribute("address", "", o.Address);
		WriteAttribute("binding", "", FromXmlQualifiedName(o.Binding));
		WriteEndElement(o);
	}

	private void Write3_DiscoveryDocumentReference(string n, string ns, DiscoveryDocumentReference o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(DiscoveryDocumentReference)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("DiscoveryDocumentReference", "http://schemas.xmlsoap.org/disco/");
		}
		WriteAttribute("ref", "", o.Ref);
		WriteEndElement(o);
	}

	private void Write5_ContractReference(string n, string ns, ContractReference o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(ContractReference)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("ContractReference", "http://schemas.xmlsoap.org/disco/scl/");
		}
		WriteAttribute("ref", "", o.Ref);
		WriteAttribute("docRef", "", o.DocRef);
		WriteEndElement(o);
	}

	private void Write7_SchemaReference(string n, string ns, SchemaReference o, bool isNullable, bool needType)
	{
		if (o == null)
		{
			if (isNullable)
			{
				WriteNullTagLiteral(n, ns);
			}
			return;
		}
		if (!needType && !(o.GetType() == typeof(SchemaReference)))
		{
			throw CreateUnknownTypeException(o);
		}
		WriteStartElement(n, ns, o, writePrefixed: false, null);
		if (needType)
		{
			WriteXsiType("SchemaReference", "http://schemas.xmlsoap.org/disco/schema/");
		}
		WriteAttribute("ref", "", o.Ref);
		WriteAttribute("targetNamespace", "", o.TargetNamespace);
		WriteEndElement(o);
	}

	protected override void InitCallbacks()
	{
	}
}
