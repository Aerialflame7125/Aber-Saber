using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class WebReferenceOptionsSerializationReader : XmlSerializationReader
{
	private Hashtable _CodeGenerationOptionsValues;

	private string id2_Item;

	private string id5_type;

	private string id4_schemaImporterExtensions;

	private string id3_codeGenerationOptions;

	private string id6_style;

	private string id7_verbose;

	private string id1_webReferenceOptions;

	internal Hashtable CodeGenerationOptionsValues
	{
		get
		{
			if (_CodeGenerationOptionsValues == null)
			{
				Hashtable hashtable = new Hashtable();
				hashtable.Add("properties", 1L);
				hashtable.Add("newAsync", 2L);
				hashtable.Add("oldAsync", 4L);
				hashtable.Add("order", 8L);
				hashtable.Add("enableDataBinding", 16L);
				_CodeGenerationOptionsValues = hashtable;
			}
			return _CodeGenerationOptionsValues;
		}
	}

	private CodeGenerationOptions Read1_CodeGenerationOptions(string s)
	{
		return (CodeGenerationOptions)XmlSerializationReader.ToEnum(s, CodeGenerationOptionsValues, "System.Xml.Serialization.CodeGenerationOptions");
	}

	private ServiceDescriptionImportStyle Read2_ServiceDescriptionImportStyle(string s)
	{
		return s switch
		{
			"client" => ServiceDescriptionImportStyle.Client, 
			"server" => ServiceDescriptionImportStyle.Server, 
			"serverInterface" => ServiceDescriptionImportStyle.ServerInterface, 
			_ => throw CreateUnknownConstantException(s, typeof(ServiceDescriptionImportStyle)), 
		};
	}

	private WebReferenceOptions Read4_WebReferenceOptions(bool isNullable, bool checkType)
	{
		XmlQualifiedName xmlQualifiedName = (checkType ? GetXsiType() : null);
		bool flag = false;
		if (isNullable)
		{
			flag = ReadNull();
		}
		if (checkType && !(xmlQualifiedName == null) && ((object)xmlQualifiedName.Name != id1_webReferenceOptions || (object)xmlQualifiedName.Namespace != id2_Item))
		{
			throw CreateUnknownTypeException(xmlQualifiedName);
		}
		if (flag)
		{
			return null;
		}
		WebReferenceOptions webReferenceOptions = new WebReferenceOptions();
		_ = webReferenceOptions.SchemaImporterExtensions;
		bool[] array = new bool[4];
		while (base.Reader.MoveToNextAttribute())
		{
			if (!IsXmlnsAttribute(base.Reader.Name))
			{
				UnknownNode(webReferenceOptions);
			}
		}
		base.Reader.MoveToElement();
		if (base.Reader.IsEmptyElement)
		{
			base.Reader.Skip();
			return webReferenceOptions;
		}
		base.Reader.ReadStartElement();
		base.Reader.MoveToContent();
		int whileIterations = 0;
		int readerCount = base.ReaderCount;
		while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
		{
			if (base.Reader.NodeType == XmlNodeType.Element)
			{
				if (!array[0] && (object)base.Reader.LocalName == id3_codeGenerationOptions && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (base.Reader.IsEmptyElement)
					{
						base.Reader.Skip();
					}
					else
					{
						webReferenceOptions.CodeGenerationOptions = Read1_CodeGenerationOptions(base.Reader.ReadElementString());
					}
					array[0] = true;
				}
				else if ((object)base.Reader.LocalName == id4_schemaImporterExtensions && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (!ReadNull())
					{
						StringCollection schemaImporterExtensions = webReferenceOptions.SchemaImporterExtensions;
						if (schemaImporterExtensions == null || base.Reader.IsEmptyElement)
						{
							base.Reader.Skip();
						}
						else
						{
							base.Reader.ReadStartElement();
							base.Reader.MoveToContent();
							int whileIterations2 = 0;
							int readerCount2 = base.ReaderCount;
							while (base.Reader.NodeType != XmlNodeType.EndElement && base.Reader.NodeType != 0)
							{
								if (base.Reader.NodeType == XmlNodeType.Element)
								{
									if ((object)base.Reader.LocalName == id5_type && (object)base.Reader.NamespaceURI == id2_Item)
									{
										if (ReadNull())
										{
											schemaImporterExtensions.Add(null);
										}
										else
										{
											schemaImporterExtensions.Add(base.Reader.ReadElementString());
										}
									}
									else
									{
										UnknownNode(null, "http://microsoft.com/webReference/:type");
									}
								}
								else
								{
									UnknownNode(null, "http://microsoft.com/webReference/:type");
								}
								base.Reader.MoveToContent();
								CheckReaderCount(ref whileIterations2, ref readerCount2);
							}
							ReadEndElement();
						}
					}
				}
				else if (!array[2] && (object)base.Reader.LocalName == id6_style && (object)base.Reader.NamespaceURI == id2_Item)
				{
					if (base.Reader.IsEmptyElement)
					{
						base.Reader.Skip();
					}
					else
					{
						webReferenceOptions.Style = Read2_ServiceDescriptionImportStyle(base.Reader.ReadElementString());
					}
					array[2] = true;
				}
				else if (!array[3] && (object)base.Reader.LocalName == id7_verbose && (object)base.Reader.NamespaceURI == id2_Item)
				{
					webReferenceOptions.Verbose = XmlConvert.ToBoolean(base.Reader.ReadElementString());
					array[3] = true;
				}
				else
				{
					UnknownNode(webReferenceOptions, "http://microsoft.com/webReference/:codeGenerationOptions, http://microsoft.com/webReference/:schemaImporterExtensions, http://microsoft.com/webReference/:style, http://microsoft.com/webReference/:verbose");
				}
			}
			else
			{
				UnknownNode(webReferenceOptions, "http://microsoft.com/webReference/:codeGenerationOptions, http://microsoft.com/webReference/:schemaImporterExtensions, http://microsoft.com/webReference/:style, http://microsoft.com/webReference/:verbose");
			}
			base.Reader.MoveToContent();
			CheckReaderCount(ref whileIterations, ref readerCount);
		}
		ReadEndElement();
		return webReferenceOptions;
	}

	protected override void InitCallbacks()
	{
	}

	internal object Read5_webReferenceOptions()
	{
		object result = null;
		base.Reader.MoveToContent();
		if (base.Reader.NodeType == XmlNodeType.Element)
		{
			if ((object)base.Reader.LocalName != id1_webReferenceOptions || (object)base.Reader.NamespaceURI != id2_Item)
			{
				throw CreateUnknownNodeException();
			}
			result = Read4_WebReferenceOptions(isNullable: true, checkType: true);
		}
		else
		{
			UnknownNode(null, "http://microsoft.com/webReference/:webReferenceOptions");
		}
		return result;
	}

	protected override void InitIDs()
	{
		id2_Item = base.Reader.NameTable.Add("http://microsoft.com/webReference/");
		id5_type = base.Reader.NameTable.Add("type");
		id4_schemaImporterExtensions = base.Reader.NameTable.Add("schemaImporterExtensions");
		id3_codeGenerationOptions = base.Reader.NameTable.Add("codeGenerationOptions");
		id6_style = base.Reader.NameTable.Add("style");
		id7_verbose = base.Reader.NameTable.Add("verbose");
		id1_webReferenceOptions = base.Reader.NameTable.Add("webReferenceOptions");
	}
}
