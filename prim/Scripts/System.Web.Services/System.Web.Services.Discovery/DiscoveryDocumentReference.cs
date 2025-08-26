using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents a reference to a discovery document. This class cannot be inherited.</summary>
[XmlRoot("discoveryRef", Namespace = "http://schemas.xmlsoap.org/disco/")]
public sealed class DiscoveryDocumentReference : DiscoveryReference
{
	private string reference;

	/// <summary>Gets or sets the reference to a discovery document.</summary>
	/// <returns>Reference to a discovery document.</returns>
	[XmlAttribute("ref")]
	public string Ref
	{
		get
		{
			if (reference != null)
			{
				return reference;
			}
			return "";
		}
		set
		{
			reference = value;
		}
	}

	/// <summary>Gets the name of the default file to use when saving the referenced discovery document.</summary>
	/// <returns>Name of the default file to use when saving the referenced document to a file.</returns>
	[XmlIgnore]
	public override string DefaultFilename => Path.ChangeExtension(DiscoveryReference.FilenameFromUrl(Url), ".disco");

	/// <summary>Gets the contents of the referenced discovery document as a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> object.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> representing the contents of the referenced discovery document.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is <see langword="null" />.-or- An error occurred during the download or resolution of the XSD schema using <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" />. </exception>
	[XmlIgnore]
	public DiscoveryDocument Document
	{
		get
		{
			if (base.ClientProtocol == null)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingClientProtocol"));
			}
			object obj = base.ClientProtocol.Documents[Url];
			if (obj == null)
			{
				Resolve();
				obj = base.ClientProtocol.Documents[Url];
			}
			if (!(obj is DiscoveryDocument result))
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidDocType", typeof(DiscoveryDocument).FullName, (obj == null) ? string.Empty : obj.GetType().FullName, Url));
			}
			return result;
		}
	}

	/// <summary>Gets or sets the URL of the referenced discovery document.</summary>
	/// <returns>The URL of the referenced discovery document.</returns>
	[XmlIgnore]
	public override string Url
	{
		get
		{
			return Ref;
		}
		set
		{
			Ref = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> class.</summary>
	public DiscoveryDocumentReference()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryDocumentReference" /> class, setting the <see cref="P:System.Web.Services.Discovery.DiscoveryDocumentReference.Ref" /> property to the value of <paramref name="href" />.</summary>
	/// <param name="href">Reference to a discovery document. The <see cref="P:System.Web.Services.Discovery.DiscoveryDocumentReference.Ref" /> property is set to the value of <paramref name="href" />. </param>
	public DiscoveryDocumentReference(string href)
	{
		Ref = href;
	}

	/// <summary>Writes the passed <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="document">The <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> to write into <paramref name="stream" />. </param>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the serialized discovery document is written. </param>
	public override void WriteDocument(object document, Stream stream)
	{
		WebServicesSection.Current.DiscoveryDocumentSerializer.Serialize(new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)), document);
	}

	/// <summary>Reads and returns the discovery document from the passed <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="stream">
	///       <see cref="T:System.IO.Stream" /> containing the discovery document. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of the referenced discovery document.</returns>
	public override object ReadDocument(Stream stream)
	{
		return WebServicesSection.Current.DiscoveryDocumentSerializer.Deserialize(stream);
	}

	private static DiscoveryDocument GetDocumentNoParse(ref string url, DiscoveryClientProtocol client)
	{
		DiscoveryDocument discoveryDocument = (DiscoveryDocument)client.Documents[url];
		if (discoveryDocument != null)
		{
			return discoveryDocument;
		}
		string contentType = null;
		Stream stream = client.Download(ref url, ref contentType);
		try
		{
			XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
			xmlTextReader.XmlResolver = null;
			xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
			if (!DiscoveryDocument.CanRead(xmlTextReader))
			{
				ArgumentException innerException = new ArgumentException(Res.GetString("WebInvalidFormat"));
				throw new InvalidOperationException(Res.GetString("WebMissingDocument", url), innerException);
			}
			return DiscoveryDocument.Read(xmlTextReader);
		}
		finally
		{
			stream.Close();
		}
	}

	protected internal override void Resolve(string contentType, Stream stream)
	{
		DiscoveryDocument discoveryDocument = null;
		if (ContentType.IsHtml(contentType))
		{
			string text = LinkGrep.SearchForLink(stream);
			if (text == null)
			{
				throw new InvalidContentTypeException(Res.GetString("WebInvalidContentType", contentType), contentType);
			}
			string url = DiscoveryReference.UriToString(Url, text);
			discoveryDocument = GetDocumentNoParse(ref url, base.ClientProtocol);
			Url = url;
		}
		if (discoveryDocument == null)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
			xmlTextReader.XmlResolver = null;
			xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
			xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
			if (DiscoveryDocument.CanRead(xmlTextReader))
			{
				discoveryDocument = DiscoveryDocument.Read(xmlTextReader);
			}
			else
			{
				stream.Position = 0L;
				XmlTextReader xmlTextReader2 = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
				xmlTextReader2.XmlResolver = null;
				xmlTextReader2.DtdProcessing = DtdProcessing.Prohibit;
				while (xmlTextReader2.NodeType != XmlNodeType.Element)
				{
					if (xmlTextReader2.NodeType == XmlNodeType.ProcessingInstruction)
					{
						StringBuilder stringBuilder = new StringBuilder("<pi ");
						stringBuilder.Append(xmlTextReader2.Value);
						stringBuilder.Append("/>");
						XmlTextReader xmlTextReader3 = new XmlTextReader(new StringReader(stringBuilder.ToString()));
						xmlTextReader3.XmlResolver = null;
						xmlTextReader3.DtdProcessing = DtdProcessing.Prohibit;
						xmlTextReader3.Read();
						string text2 = xmlTextReader3["type"];
						string text3 = xmlTextReader3["alternate"];
						string text4 = xmlTextReader3["href"];
						if (text2 != null && ContentType.MatchesBase(text2, "text/xml") && text3 != null && string.Compare(text3, "yes", StringComparison.OrdinalIgnoreCase) == 0 && text4 != null)
						{
							string url2 = DiscoveryReference.UriToString(Url, text4);
							discoveryDocument = GetDocumentNoParse(ref url2, base.ClientProtocol);
							Url = url2;
							break;
						}
					}
					xmlTextReader2.Read();
				}
			}
		}
		if (discoveryDocument == null)
		{
			throw new InvalidOperationException(innerException: (!ContentType.IsXml(contentType)) ? ((Exception)new InvalidContentTypeException(Res.GetString("WebInvalidContentType", contentType), contentType)) : ((Exception)new ArgumentException(Res.GetString("WebInvalidFormat"))), message: Res.GetString("WebMissingDocument", Url));
		}
		base.ClientProtocol.References[Url] = this;
		base.ClientProtocol.Documents[Url] = discoveryDocument;
		foreach (object reference in discoveryDocument.References)
		{
			if (reference is DiscoveryReference)
			{
				DiscoveryReference discoveryReference = (DiscoveryReference)reference;
				if (discoveryReference.Url.Length == 0)
				{
					throw new InvalidOperationException(Res.GetString("WebEmptyRef", discoveryReference.GetType().FullName, Url));
				}
				discoveryReference.Url = DiscoveryReference.UriToString(Url, discoveryReference.Url);
				if (discoveryReference is ContractReference { DocRef: not null } contractReference)
				{
					contractReference.DocRef = DiscoveryReference.UriToString(Url, contractReference.DocRef);
				}
				discoveryReference.ClientProtocol = base.ClientProtocol;
				base.ClientProtocol.References[discoveryReference.Url] = discoveryReference;
			}
			else
			{
				base.ClientProtocol.AdditionalInformation.Add(reference);
			}
		}
	}

	/// <summary>Verifies that all referenced documents within the discovery document are valid.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is <see langword="null" />.-or- The discovery document could not be downloaded and verified successfully. </exception>
	public void ResolveAll()
	{
		ResolveAll(throwOnError: true);
	}

	internal void ResolveAll(bool throwOnError)
	{
		try
		{
			Resolve();
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (throwOnError)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex);
			}
			return;
		}
		foreach (object reference in Document.References)
		{
			if (reference is DiscoveryDocumentReference discoveryDocumentReference && base.ClientProtocol.Documents[discoveryDocumentReference.Url] == null)
			{
				discoveryDocumentReference.ClientProtocol = base.ClientProtocol;
				discoveryDocumentReference.ResolveAll(throwOnError);
			}
		}
	}
}
