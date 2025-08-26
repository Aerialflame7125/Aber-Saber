using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

internal class DiscoveryDocumentSerializationReader : XmlSerializationReader
{
	private string id1_discovery;

	private string id4_discoveryRef;

	private string id19_docRef;

	private string id8_Item;

	private string id14_binding;

	private string id20_DiscoveryDocumentReference;

	private string id17_targetNamespace;

	private string id5_contractRef;

	private string id10_Item;

	private string id13_Item;

	private string id7_schemaRef;

	private string id3_DiscoveryDocument;

	private string id9_soap;

	private string id12_address;

	private string id16_ref;

	private string id11_SoapBinding;

	private string id18_ContractReference;

	private string id2_Item;

	private string id15_SchemaReference;

	private string id6_Item;

	public object Read10_discovery()
	{
		object result = null;
		base.Reader.MoveToContent();
		if (base.Reader.NodeType == XmlNodeType.Element)
		{
			if ((object)base.Reader.LocalName != id1_discovery || (object)base.Reader.NamespaceURI != id2_Item)
			{
				throw CreateUnknownNodeException();
			}
			result = Read9_DiscoveryDocument(isNullable: true, checkType: true);
		}
		else
		{
			UnknownNode(null, "http://schemas.xmlsoap.org/disco/:discovery");
		}
		return result;
	}

	private DiscoveryDocument Read9_DiscoveryDocument(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id3_DiscoveryDocument || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		DiscoveryDocument discoveryDocument = new DiscoveryDocument();
		IList references = discoveryDocument.References;
		_ = new bool[1];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(discoveryDocument);
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return discoveryDocument;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if ((object)base.Reader.LocalName == id4_discoveryRef && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (references == null)
					{
						base.Reader.Skip();
					}
					else
					{
						references.Add(Read3_DiscoveryDocumentReference(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id5_contractRef && (object)base.Reader.NamespaceURI == id6_Item)
				{
					if (references == null)
					{
						base.Reader.Skip();
					}
					else
					{
						references.Add(Read5_ContractReference(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id7_schemaRef && (object)base.Reader.NamespaceURI == id8_Item)
				{
					if (references == null)
					{
						base.Reader.Skip();
					}
					else
					{
						references.Add(Read7_SchemaReference(isNullable: false, checkType: true));
					}
				}
				else if ((object)base.Reader.LocalName == id9_soap && (object)base.Reader.NamespaceURI == id10_Item)
				{
					if (references == null)
					{
						base.Reader.Skip();
					}
					else
					{
						references.Add(Read8_SoapBinding(isNullable: false, checkType: true));
					}
				}
				else
				{
					UnknownNode(discoveryDocument, "http://schemas.xmlsoap.org/disco/:discoveryRef, http://schemas.xmlsoap.org/disco/scl/:contractRef, http://schemas.xmlsoap.org/disco/schema/:schemaRef, http://schemas.xmlsoap.org/disco/soap/:soap");
				}
			}
			else
			{
				UnknownNode(discoveryDocument, "http://schemas.xmlsoap.org/disco/:discoveryRef, http://schemas.xmlsoap.org/disco/scl/:contractRef, http://schemas.xmlsoap.org/disco/schema/:schemaRef, http://schemas.xmlsoap.org/disco/soap/:soap");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return discoveryDocument;
	}

	private SoapBinding Read8_SoapBinding(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id11_SoapBinding || (object)xmlQualifiedName.Namespace != id10_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SoapBinding soapBinding = new SoapBinding();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id12_address && (object)base.Reader.NamespaceURI == id13_Item)
			{
				soapBinding.Address = base.Reader.Value;
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id14_binding && (object)base.Reader.NamespaceURI == id13_Item)
			{
				soapBinding.Binding = ToXmlQualifiedName(base.Reader.Value);
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(soapBinding, ":address, :binding");
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

	private SchemaReference Read7_SchemaReference(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id15_SchemaReference || (object)xmlQualifiedName.Namespace != id8_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		SchemaReference schemaReference = new SchemaReference();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id16_ref && (object)base.Reader.NamespaceURI == id13_Item)
			{
				schemaReference.Ref = base.Reader.Value;
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id17_targetNamespace && (object)base.Reader.NamespaceURI == id13_Item)
			{
				schemaReference.TargetNamespace = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(schemaReference, ":ref, :targetNamespace");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return schemaReference;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(schemaReference, "");
			}
			else
			{
				UnknownNode(schemaReference, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return schemaReference;
	}

	private ContractReference Read5_ContractReference(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id18_ContractReference || (object)xmlQualifiedName.Namespace != id6_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		ContractReference contractReference = new ContractReference();
		bool[] array = new bool[2];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id16_ref && (object)base.Reader.NamespaceURI == id13_Item)
			{
				contractReference.Ref = base.Reader.Value;
				array[0] = true;
			}
			else if (!array[1] && (object)base.Reader.LocalName == id19_docRef && (object)base.Reader.NamespaceURI == id13_Item)
			{
				contractReference.DocRef = base.Reader.Value;
				array[1] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(contractReference, ":ref, :docRef");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return contractReference;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(contractReference, "");
			}
			else
			{
				UnknownNode(contractReference, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return contractReference;
	}

	private DiscoveryDocumentReference Read3_DiscoveryDocumentReference(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id20_DiscoveryDocumentReference || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		DiscoveryDocumentReference discoveryDocumentReference = new DiscoveryDocumentReference();
		bool[] array = new bool[1];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!array[0] && (object)base.Reader.LocalName == id16_ref && (object)base.Reader.NamespaceURI == id13_Item)
			{
				discoveryDocumentReference.Ref = base.Reader.Value;
				array[0] = true;
			}
			else if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(discoveryDocumentReference, ":ref");
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return discoveryDocumentReference;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				UnknownNode(discoveryDocumentReference, "");
			}
			else
			{
				UnknownNode(discoveryDocumentReference, "");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return discoveryDocumentReference;
	}

	protected override void InitCallbacks()
	{
	}

	protected override void InitIDs()
	{
		id1_discovery = base.Reader.NameTable.Add("discovery");
		id4_discoveryRef = base.Reader.NameTable.Add("discoveryRef");
		id19_docRef = base.Reader.NameTable.Add("docRef");
		id8_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/schema/");
		id14_binding = base.Reader.NameTable.Add("binding");
		id20_DiscoveryDocumentReference = base.Reader.NameTable.Add("DiscoveryDocumentReference");
		id17_targetNamespace = base.Reader.NameTable.Add("targetNamespace");
		id5_contractRef = base.Reader.NameTable.Add("contractRef");
		id10_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/soap/");
		id13_Item = base.Reader.NameTable.Add("");
		id7_schemaRef = base.Reader.NameTable.Add("schemaRef");
		id3_DiscoveryDocument = base.Reader.NameTable.Add("DiscoveryDocument");
		id9_soap = base.Reader.NameTable.Add("soap");
		id12_address = base.Reader.NameTable.Add("address");
		id16_ref = base.Reader.NameTable.Add("ref");
		id11_SoapBinding = base.Reader.NameTable.Add("SoapBinding");
		id18_ContractReference = base.Reader.NameTable.Add("ContractReference");
		id2_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/");
		id15_SchemaReference = base.Reader.NameTable.Add("SchemaReference");
		id6_Item = base.Reader.NameTable.Add("http://schemas.xmlsoap.org/disco/scl/");
	}
}
