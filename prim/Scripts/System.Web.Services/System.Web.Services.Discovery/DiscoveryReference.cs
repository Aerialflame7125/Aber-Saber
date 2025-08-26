using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>The base class for discoverable references using XML Web services discovery.</summary>
public abstract class DiscoveryReference
{
	private DiscoveryClientProtocol clientProtocol;

	/// <summary>Gets or sets the instance of <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> used in a discovery process.</summary>
	/// <returns>An instance of <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> used in a discovery process </returns>
	[XmlIgnore]
	public DiscoveryClientProtocol ClientProtocol
	{
		get
		{
			return clientProtocol;
		}
		set
		{
			clientProtocol = value;
		}
	}

	/// <summary>Gets the name of the default file to use when saving the referenced discovery document, XSD schema, or Service Description.</summary>
	/// <returns>Name of the default file to use when saving the referenced document.</returns>
	[XmlIgnore]
	public virtual string DefaultFilename => FilenameFromUrl(Url);

	/// <summary>Gets or sets the URL of the referenced document.</summary>
	/// <returns>The URL of the referenced document.</returns>
	[XmlIgnore]
	public abstract string Url { get; set; }

	/// <summary>When overridden in a derived class, writes the document to a <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="document">The document to write into a <see cref="T:System.IO.Stream" />. </param>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which the <paramref name="document" /> is written. </param>
	public abstract void WriteDocument(object document, Stream stream);

	/// <summary>Reads the passed <see cref="T:System.IO.Stream" /> and returns an instance of the class representing the type of referenced document.</summary>
	/// <param name="stream">
	///       <see cref="T:System.IO.Stream" /> containing the reference document. </param>
	/// <returns>An <see cref="T:System.Object" /> with an underlying type matching the type of referenced document.</returns>
	public abstract object ReadDocument(Stream stream);

	internal virtual void LoadExternals(Hashtable loadedExternals)
	{
	}

	/// <summary>Returns a file name based on the passed URL.</summary>
	/// <param name="url">The URL on which the name of the file is based. </param>
	/// <returns>Name of the file based on the passed URL.</returns>
	public static string FilenameFromUrl(string url)
	{
		int num = url.LastIndexOf('/', url.Length - 1);
		if (num >= 0)
		{
			url = url.Substring(num + 1);
		}
		int num2 = url.IndexOf('.');
		if (num2 >= 0)
		{
			url = url.Substring(0, num2);
		}
		int num3 = url.IndexOf('?');
		if (num3 >= 0)
		{
			url = url.Substring(0, num3);
		}
		if (url == null || url.Length == 0)
		{
			return "item";
		}
		return MakeValidFilename(url);
	}

	private static bool FindChar(char ch, char[] chars)
	{
		for (int i = 0; i < chars.Length; i++)
		{
			if (ch == chars[i])
			{
				return true;
			}
		}
		return false;
	}

	internal static string MakeValidFilename(string filename)
	{
		if (filename == null)
		{
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder(filename.Length);
		foreach (char c in filename)
		{
			if (!FindChar(c, Path.InvalidPathChars))
			{
				stringBuilder.Append(c);
			}
		}
		string text = stringBuilder.ToString();
		if (text.Length == 0)
		{
			text = "item";
		}
		return Path.GetFileName(text);
	}

	/// <summary>Downloads the referenced document at <see cref="P:System.Web.Services.Discovery.DiscoveryReference.Url" /> to resolve whether the referenced document is valid.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Services.Discovery.DiscoveryReference.ClientProtocol" /> property is <see langword="null" />. </exception>
	public void Resolve()
	{
		if (ClientProtocol == null)
		{
			throw new InvalidOperationException(Res.GetString("WebResolveMissingClientProtocol"));
		}
		if (ClientProtocol.Documents[Url] != null || ClientProtocol.InlinedSchemas[Url] != null)
		{
			return;
		}
		string url = Url;
		string url2 = Url;
		string contentType = null;
		Stream stream = ClientProtocol.Download(ref url, ref contentType);
		if (ClientProtocol.Documents[url] != null)
		{
			Url = url;
			return;
		}
		try
		{
			Url = url;
			Resolve(contentType, stream);
		}
		catch
		{
			Url = url2;
			throw;
		}
		finally
		{
			stream.Close();
		}
	}

	internal Exception AttemptResolve(string contentType, Stream stream)
	{
		try
		{
			Resolve(contentType, stream);
			return null;
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "AttemptResolve", ex);
			}
			return ex;
		}
	}

	/// <summary>Resolves whether the referenced document is valid.</summary>
	/// <param name="contentType">The MIME type of <paramref name="stream" />. </param>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> containing the referenced document. </param>
	protected internal abstract void Resolve(string contentType, Stream stream);

	internal static string UriToString(string baseUrl, string relUrl)
	{
		return new Uri(new Uri(baseUrl), relUrl).GetComponents(UriComponents.AbsoluteUri, UriFormat.SafeUnescaped);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryReference" /> class. </summary>
	protected DiscoveryReference()
	{
	}
}
