using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents a reference in a discovery document to a service description.</summary>
[XmlRoot("contractRef", Namespace = "http://schemas.xmlsoap.org/disco/scl/")]
public class ContractReference : DiscoveryReference
{
	/// <summary>XML namespace for service description references in discovery documents.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/disco/scl/";

	private string docRef;

	private string reference;

	/// <summary>Gets or sets the URL to the referenced service description.</summary>
	/// <returns>The URL to the referenced service description.</returns>
	[XmlAttribute("ref")]
	public string Ref
	{
		get
		{
			return reference;
		}
		set
		{
			reference = value;
		}
	}

	/// <summary>Gets and sets the URL for a XML Web service implementing the service description referenced in the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property.</summary>
	/// <returns>The URL for a XML Web service implementing the Service Description referenced in the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property.</returns>
	[XmlAttribute("docRef")]
	public string DocRef
	{
		get
		{
			return docRef;
		}
		set
		{
			docRef = value;
		}
	}

	/// <summary>Gets or sets the URL for the referenced Service Description.</summary>
	/// <returns>The URL for the referenced service description.</returns>
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

	/// <summary>Gets a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object representing the service description.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> object representing the service description.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is <see langword="null" />. </exception>
	/// <exception cref="T:System.Exception">The <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> property of <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> does not contain a discovery document with an URL of <see cref="P:System.Web.Services.Discovery.ContractReference.Url" />. </exception>
	[XmlIgnore]
	public ServiceDescription Contract
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
			if (!(obj is ServiceDescription result))
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidDocType", typeof(ServiceDescription).FullName, (obj == null) ? string.Empty : obj.GetType().FullName, Url));
			}
			return result;
		}
	}

	/// <summary>Gets the name of the file to use by default when saving the referenced service description.</summary>
	/// <returns>Name of the default file to use when saving the referenced service description to a file.</returns>
	[XmlIgnore]
	public override string DefaultFilename
	{
		get
		{
			string text = DiscoveryReference.MakeValidFilename(Contract.Name);
			if (text == null || text.Length == 0)
			{
				text = DiscoveryReference.FilenameFromUrl(Url);
			}
			return Path.ChangeExtension(text, ".wsdl");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractReference" /> class using default values.</summary>
	public ContractReference()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractReference" /> class using the supplied reference to a Service Description.</summary>
	/// <param name="href">The URL for a Sevice Descritpion. Initializes the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property value. </param>
	public ContractReference(string href)
	{
		Ref = href;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ContractReference" /> class using the supplied references to a service description and a XML Web service implementing the service description.</summary>
	/// <param name="href">The URL for a service description. Initializes the <see cref="P:System.Web.Services.Discovery.ContractReference.Ref" /> property value. </param>
	/// <param name="docRef">The URL for a XML Web service implementing the service description at <paramref name="href" />. Initializes the <see cref="P:System.Web.Services.Discovery.ContractReference.DocRef" /> property value. </param>
	public ContractReference(string href, string docRef)
	{
		Ref = href;
		DocRef = docRef;
	}

	internal override void LoadExternals(Hashtable loadedExternals)
	{
		ServiceDescription serviceDescription = null;
		try
		{
			serviceDescription = Contract;
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			base.ClientProtocol.Errors[Url] = ex;
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "LoadExternals", ex);
			}
		}
		if (serviceDescription == null)
		{
			return;
		}
		foreach (XmlSchema schema in Contract.Types.Schemas)
		{
			SchemaReference.LoadExternals(schema, Url, base.ClientProtocol, loadedExternals);
		}
	}

	/// <summary>Writes the passed-in service description into the passed-in <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="document">The <see cref="T:System.Web.Services.Description.ServiceDescription" /> to write into <paramref name="stream" />. </param>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the serialized <see cref="T:System.Web.Services.Description.ServiceDescription" /> is written. </param>
	public override void WriteDocument(object document, Stream stream)
	{
		((ServiceDescription)document).Write(new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)));
	}

	/// <summary>Reads the service description from the passed <see cref="T:System.IO.Stream" /> and returns the service description.</summary>
	/// <param name="stream">
	///       <see cref="T:System.IO.Stream" /> containing the service description. </param>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> containing the contents of the referenced service description.</returns>
	public override object ReadDocument(Stream stream)
	{
		return ServiceDescription.Read(stream, validate: true);
	}

	/// <summary>Resolves whether the the referenced document is valid.</summary>
	/// <param name="contentType">The MIME content type of <paramref name="stream" />. </param>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> containing the referenced document. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is <see langword="null" />.-or- The service description could not be downloaded and verified successfully. </exception>
	protected internal override void Resolve(string contentType, Stream stream)
	{
		if (ContentType.IsHtml(contentType))
		{
			throw new InvalidContentTypeException(Res.GetString("WebInvalidContentType", contentType), contentType);
		}
		ServiceDescription serviceDescription = base.ClientProtocol.Documents[Url] as ServiceDescription;
		if (serviceDescription == null)
		{
			serviceDescription = ServiceDescription.Read(stream, validate: true);
			serviceDescription.RetrievalUrl = Url;
			base.ClientProtocol.Documents[Url] = serviceDescription;
		}
		base.ClientProtocol.References[Url] = this;
		ArrayList arrayList = new ArrayList();
		foreach (Import import in serviceDescription.Imports)
		{
			if (import.Location != null)
			{
				arrayList.Add(import.Location);
			}
		}
		foreach (XmlSchema schema in serviceDescription.Types.Schemas)
		{
			foreach (XmlSchemaExternal include in schema.Includes)
			{
				if (include.SchemaLocation != null && include.SchemaLocation.Length > 0)
				{
					arrayList.Add(include.SchemaLocation);
				}
			}
		}
		foreach (string item in arrayList)
		{
			string url = DiscoveryReference.UriToString(Url, item);
			if (base.ClientProtocol.Documents[url] != null)
			{
				continue;
			}
			string url2 = url;
			try
			{
				stream = base.ClientProtocol.Download(ref url, ref contentType);
				try
				{
					if (base.ClientProtocol.Documents[url] != null)
					{
						continue;
					}
					XmlTextReader xmlTextReader = new XmlTextReader(new StreamReader(stream, RequestResponseUtils.GetEncoding(contentType)));
					xmlTextReader.WhitespaceHandling = WhitespaceHandling.Significant;
					xmlTextReader.XmlResolver = null;
					xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
					if (ServiceDescription.CanRead(xmlTextReader))
					{
						ServiceDescription serviceDescription2 = ServiceDescription.Read(xmlTextReader, validate: true);
						serviceDescription2.RetrievalUrl = url;
						base.ClientProtocol.Documents[url] = serviceDescription2;
						ContractReference contractReference = new ContractReference(url, null);
						contractReference.ClientProtocol = base.ClientProtocol;
						try
						{
							contractReference.Resolve(contentType, stream);
						}
						catch (Exception ex)
						{
							if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
							{
								throw;
							}
							contractReference.Url = url2;
							if (Tracing.On)
							{
								Tracing.ExceptionCatch(TraceEventType.Warning, this, "Resolve", ex);
							}
						}
					}
					else
					{
						if (!xmlTextReader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
						{
							continue;
						}
						base.ClientProtocol.Documents[url] = XmlSchema.Read(xmlTextReader, null);
						SchemaReference schemaReference = new SchemaReference(url);
						schemaReference.ClientProtocol = base.ClientProtocol;
						try
						{
							schemaReference.Resolve(contentType, stream);
						}
						catch (Exception ex2)
						{
							if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
							{
								throw;
							}
							schemaReference.Url = url2;
							if (Tracing.On)
							{
								Tracing.ExceptionCatch(TraceEventType.Warning, this, "Resolve", ex2);
							}
						}
						continue;
					}
				}
				finally
				{
					stream.Close();
				}
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidDocumentContentsException(Res.GetString("TheWSDLDocumentContainsLinksThatCouldNotBeResolved", url), ex3);
			}
		}
	}
}
