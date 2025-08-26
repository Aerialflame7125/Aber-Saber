using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents a reference in a discovery document to an XML Schema Definition (XSD) language schema. This class cannot be inherited.</summary>
[XmlRoot("schemaRef", Namespace = "http://schemas.xmlsoap.org/disco/schema/")]
public sealed class SchemaReference : DiscoveryReference
{
	/// <summary>XML namespace for XSD schema references in discovery documents.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/disco/schema/";

	private string reference;

	private string targetNamespace;

	/// <summary>Gets or sets the URL to the referenced XSD schema.</summary>
	/// <returns>The URL for the referenced XSD schema. The default value is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets or sets the <see langword="targetNamespace" /> XML attribute of the XSD schema.</summary>
	/// <returns>The <see langword="targetNamespace" /> XML attribute of the XSD schema. The default value is <see langword="null" />.</returns>
	[XmlAttribute("targetNamespace")]
	[DefaultValue(null)]
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

	/// <summary>Gets or sets the URL for the schema reference.</summary>
	/// <returns>The URL for the referenced XSD schema. The default value is <see cref="F:System.String.Empty" />.</returns>
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

	/// <summary>Gets the name of the default file to use when saving the referenced XSD schema.</summary>
	/// <returns>Default name to use when saving the referenced XSD schema to a file.</returns>
	[XmlIgnore]
	public override string DefaultFilename
	{
		get
		{
			string text = DiscoveryReference.MakeValidFilename(Schema.Id);
			if (text == null || text.Length == 0)
			{
				text = DiscoveryReference.FilenameFromUrl(Url);
			}
			return Path.ChangeExtension(text, ".xsd");
		}
	}

	/// <summary>Gets an <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XSD schema.</summary>
	/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> object representing the XSD schema.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///         <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is <see langword="null" />.-or- An error occurred during the download or resolution of the XSD schema using <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" />. </exception>
	[XmlIgnore]
	public XmlSchema Schema
	{
		get
		{
			if (base.ClientProtocol == null)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingClientProtocol"));
			}
			object obj = base.ClientProtocol.InlinedSchemas[Url];
			if (obj == null)
			{
				obj = base.ClientProtocol.Documents[Url];
			}
			if (obj == null)
			{
				Resolve();
				obj = base.ClientProtocol.Documents[Url];
			}
			if (!(obj is XmlSchema result))
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidDocType", typeof(XmlSchema).FullName, (obj == null) ? string.Empty : obj.GetType().FullName, Url));
			}
			return result;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.SchemaReference" /> class using default values.</summary>
	public SchemaReference()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.SchemaReference" /> class using the supplied URL as the XSD schema reference.</summary>
	/// <param name="url">The URL for the XSD schema. Initializes the <see cref="P:System.Web.Services.Discovery.SchemaReference.Ref" /> property. </param>
	public SchemaReference(string url)
	{
		Ref = url;
	}

	internal XmlSchema GetSchema()
	{
		try
		{
			return Schema;
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
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "GetSchema", ex);
			}
		}
		return null;
	}

	internal override void LoadExternals(Hashtable loadedExternals)
	{
		LoadExternals(GetSchema(), Url, base.ClientProtocol, loadedExternals);
	}

	internal static void LoadExternals(XmlSchema schema, string url, DiscoveryClientProtocol client, Hashtable loadedExternals)
	{
		if (schema == null)
		{
			return;
		}
		foreach (XmlSchemaExternal include in schema.Includes)
		{
			if (include.SchemaLocation == null || include.SchemaLocation.Length == 0 || include.Schema != null || (!(include is XmlSchemaInclude) && !(include is XmlSchemaRedefine)))
			{
				continue;
			}
			string text = DiscoveryReference.UriToString(url, include.SchemaLocation);
			if (client.References[text] is SchemaReference)
			{
				SchemaReference schemaReference = (SchemaReference)client.References[text];
				include.Schema = schemaReference.GetSchema();
				if (include.Schema != null)
				{
					loadedExternals[text] = include.Schema;
				}
				schemaReference.LoadExternals(loadedExternals);
			}
		}
	}

	/// <summary>Writes the passed XSD schema into the passed <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="document">The <see cref="T:System.Xml.Schema.XmlSchema" /> to write into <paramref name="stream" />. </param>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the serialized XSD schema is written. </param>
	public override void WriteDocument(object document, Stream stream)
	{
		((XmlSchema)document).Write(new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)));
	}

	/// <summary>Reads and returns the XSD schema from the passed <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="stream">
	///       <see cref="T:System.IO.Stream" /> containing the XSD schema. </param>
	/// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> containing the contents of the referenced XSD schema.</returns>
	public override object ReadDocument(Stream stream)
	{
		return XmlSchema.Read(new XmlTextReader(Url, stream)
		{
			XmlResolver = null
		}, null);
	}

	protected internal override void Resolve(string contentType, Stream stream)
	{
		if (ContentType.IsHtml(contentType))
		{
			base.ClientProtocol.Errors[Url] = new InvalidContentTypeException(Res.GetString("WebInvalidContentType", contentType), contentType);
		}
		XmlSchema xmlSchema = base.ClientProtocol.Documents[Url] as XmlSchema;
		if (xmlSchema == null)
		{
			if (base.ClientProtocol.Errors[Url] != null)
			{
				throw base.ClientProtocol.Errors[Url];
			}
			xmlSchema = (XmlSchema)ReadDocument(stream);
			base.ClientProtocol.Documents[Url] = xmlSchema;
		}
		if (base.ClientProtocol.References[Url] != this)
		{
			base.ClientProtocol.References[Url] = this;
		}
		foreach (XmlSchemaExternal include in xmlSchema.Includes)
		{
			string text = null;
			try
			{
				if (include.SchemaLocation != null && include.SchemaLocation.Length > 0)
				{
					text = DiscoveryReference.UriToString(Url, include.SchemaLocation);
					SchemaReference schemaReference = new SchemaReference(text);
					schemaReference.ClientProtocol = base.ClientProtocol;
					base.ClientProtocol.References[text] = schemaReference;
					schemaReference.Resolve();
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidDocumentContentsException(Res.GetString("TheSchemaDocumentContainsLinksThatCouldNotBeResolved", text), ex);
			}
		}
	}
}
