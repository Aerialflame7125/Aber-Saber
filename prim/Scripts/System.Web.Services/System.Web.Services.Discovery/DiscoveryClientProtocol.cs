using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Provides support for programmatically invoking XML Web services discovery.</summary>
public class DiscoveryClientProtocol : HttpWebClientProtocol
{
	/// <summary>Represents the root element of an XML document containing the results of all files written when the <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.WriteAll(System.String,System.String)" /> method is invoked.</summary>
	public sealed class DiscoveryClientResultsFile
	{
		private DiscoveryClientResultCollection results = new DiscoveryClientResultCollection();

		/// <summary>Gets a collection of <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> containing the results from a <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.ReadAll(System.String)" /> or <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.WriteAll(System.String,System.String)" /> invocation.</returns>
		public DiscoveryClientResultCollection Results => results;

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol.DiscoveryClientResultsFile" /> class. </summary>
		public DiscoveryClientResultsFile()
		{
		}
	}

	private DiscoveryClientReferenceCollection references = new DiscoveryClientReferenceCollection();

	private DiscoveryClientDocumentCollection documents = new DiscoveryClientDocumentCollection();

	private Hashtable inlinedSchemas = new Hashtable();

	private ArrayList additionalInformation = new ArrayList();

	private DiscoveryExceptionDictionary errors = new DiscoveryExceptionDictionary();

	/// <summary>Gets information in addition to references found in the discovery document.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> containing additional information found in the discovery document.</returns>
	public IList AdditionalInformation => additionalInformation;

	/// <summary>Gets a collection of discovery documents.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> representing the collection of discovery documents found.</returns>
	public DiscoveryClientDocumentCollection Documents => documents;

	/// <summary>Gets a collection of exceptions that occurred during invocation of method from this class.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> of exceptions.</returns>
	public DiscoveryExceptionDictionary Errors => errors;

	/// <summary>A collection of references founds in resolved discovery documents.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientReferenceCollection" /> of references discovered.</returns>
	public DiscoveryClientReferenceCollection References => references;

	internal Hashtable InlinedSchemas => inlinedSchemas;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> class.</summary>
	public DiscoveryClientProtocol()
	{
	}

	internal DiscoveryClientProtocol(HttpWebClientProtocol protocol)
		: base(protocol)
	{
	}

	/// <summary>Discovers the supplied URL to determine if it is a discovery document.</summary>
	/// <param name="url">The URL where XML Web services discovery begins. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the results of XML Web services discovery at the supplied URL.</returns>
	/// <exception cref="T:System.Net.WebException">Accessing the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="url" /> parameteris a valid URL, but does not point to a valid discovery document. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public DiscoveryDocument Discover(string url)
	{
		if (Documents[url] is DiscoveryDocument result)
		{
			return result;
		}
		DiscoveryDocumentReference discoveryDocumentReference = new DiscoveryDocumentReference(url);
		discoveryDocumentReference.ClientProtocol = this;
		References[url] = discoveryDocumentReference;
		Errors.Clear();
		return discoveryDocumentReference.Document;
	}

	/// <summary>Discovers the supplied URL to determine if it is a discovery document, service description or an XML Schema Definition (XSD) schema.</summary>
	/// <param name="url">The URL where XML Web services discovery begins. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the results of XML Web services discovery at the supplied URL. If the <paramref name="url" /> parameter refers to a service description or an XSD Schema, a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is created in memory for it.</returns>
	/// <exception cref="T:System.Net.WebException">Accessing the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="url" /> parameteris a valid URL, but does not point to a valid discovery document, service description, or XSD schema. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public DiscoveryDocument DiscoverAny(string url)
	{
		Type[] discoveryReferenceTypes = WebServicesSection.Current.DiscoveryReferenceTypes;
		DiscoveryReference discoveryReference = null;
		string contentType = null;
		Stream stream = Download(ref url, ref contentType);
		Errors.Clear();
		bool flag = true;
		Exception ex = null;
		ArrayList arrayList = new ArrayList();
		Type[] array = discoveryReferenceTypes;
		foreach (Type type in array)
		{
			if (typeof(DiscoveryReference).IsAssignableFrom(type))
			{
				discoveryReference = (DiscoveryReference)Activator.CreateInstance(type);
				discoveryReference.Url = url;
				discoveryReference.ClientProtocol = this;
				stream.Position = 0L;
				Exception ex2 = discoveryReference.AttemptResolve(contentType, stream);
				if (ex2 == null)
				{
					break;
				}
				Errors[type.FullName] = ex2;
				discoveryReference = null;
				if (!(ex2 is InvalidContentTypeException ex3) || !ContentType.MatchesBase(ex3.ContentType, "text/html"))
				{
					flag = false;
				}
				if (ex2 is InvalidDocumentContentsException)
				{
					ex = ex2;
					break;
				}
				if (ex2.InnerException != null && ex2.InnerException.InnerException == null)
				{
					arrayList.Add(ex2.InnerException.Message);
				}
			}
		}
		if (discoveryReference == null)
		{
			if (ex != null)
			{
				StringBuilder stringBuilder = new StringBuilder(Res.GetString("TheDocumentWasUnderstoodButContainsErrors"));
				while (ex != null)
				{
					stringBuilder.Append("\n  - ").Append(ex.Message);
					ex = ex.InnerException;
				}
				throw new InvalidOperationException(stringBuilder.ToString());
			}
			if (flag)
			{
				throw new InvalidOperationException(Res.GetString("TheHTMLDocumentDoesNotContainDiscoveryInformation"));
			}
			bool flag2 = arrayList.Count == Errors.Count && Errors.Count > 0;
			int num = 1;
			while (flag2 && num < arrayList.Count)
			{
				if ((string)arrayList[num - 1] != (string)arrayList[num])
				{
					flag2 = false;
				}
				num++;
			}
			if (flag2)
			{
				throw new InvalidOperationException(Res.GetString("TheDocumentWasNotRecognizedAsAKnownDocumentType", arrayList[0]));
			}
			StringBuilder stringBuilder2 = new StringBuilder(Res.GetString("WebMissingResource", url));
			foreach (DictionaryEntry error in Errors)
			{
				Exception ex4 = (Exception)error.Value;
				string text = (string)error.Key;
				if (string.Compare(text, typeof(ContractReference).FullName, StringComparison.Ordinal) == 0)
				{
					text = Res.GetString("WebContractReferenceName");
				}
				else if (string.Compare(text, typeof(SchemaReference).FullName, StringComparison.Ordinal) == 0)
				{
					text = Res.GetString("WebShemaReferenceName");
				}
				else if (string.Compare(text, typeof(DiscoveryDocumentReference).FullName, StringComparison.Ordinal) == 0)
				{
					text = Res.GetString("WebDiscoveryDocumentReferenceName");
				}
				stringBuilder2.Append("\n- ").Append(Res.GetString("WebDiscoRefReport", text, ex4.Message));
				while (ex4.InnerException != null)
				{
					stringBuilder2.Append("\n  - ").Append(ex4.InnerException.Message);
					ex4 = ex4.InnerException;
				}
			}
			throw new InvalidOperationException(stringBuilder2.ToString());
		}
		if (discoveryReference is DiscoveryDocumentReference)
		{
			return ((DiscoveryDocumentReference)discoveryReference).Document;
		}
		References[discoveryReference.Url] = discoveryReference;
		return new DiscoveryDocument
		{
			References = { (object)discoveryReference }
		};
	}

	/// <summary>Downloads the discovery document at the supplied URL into a <see cref="T:System.IO.Stream" /> object.</summary>
	/// <param name="url">The URL of the discovery document to download. </param>
	/// <returns>A <see cref="T:System.IO.Stream" /> containing the document at the supplied URL.</returns>
	/// <exception cref="T:System.Net.WebException">The download from the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public Stream Download(ref string url)
	{
		string contentType = null;
		return Download(ref url, ref contentType);
	}

	/// <summary>Downloads the discovery document at the supplied URL into a <see cref="T:System.IO.Stream" /> object, setting the <paramref name="contentType" /> parameter to the MIME encoding of the discovery document.</summary>
	/// <param name="url">The URL of the discovery document to download. </param>
	/// <param name="contentType">The MIME encoding of the downloaded discovery document. </param>
	/// <returns>A <see cref="T:System.IO.Stream" /> containing the document at the supplied URL.</returns>
	/// <exception cref="T:System.Net.WebException">The download from the supplied URL returned an HTTP status code other than <see cref="F:System.Net.HttpStatusCode.OK" />. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public Stream Download(ref string url, ref string contentType)
	{
		WebRequest webRequest = GetWebRequest(new Uri(url));
		webRequest.Method = "GET";
		WebResponse webResponse = null;
		try
		{
			webResponse = GetWebResponse(webRequest);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			throw new WebException(Res.GetString("ThereWasAnErrorDownloading0", url), ex);
		}
		if (webResponse is HttpWebResponse { StatusCode: not HttpStatusCode.OK } httpWebResponse)
		{
			string message = RequestResponseUtils.CreateResponseExceptionString(httpWebResponse);
			throw new WebException(Res.GetString("ThereWasAnErrorDownloading0", url), new WebException(message, null, WebExceptionStatus.ProtocolError, webResponse));
		}
		Stream responseStream = webResponse.GetResponseStream();
		try
		{
			url = webResponse.ResponseUri.ToString();
			contentType = webResponse.ContentType;
			if (webResponse.ResponseUri.Scheme == Uri.UriSchemeFtp || webResponse.ResponseUri.Scheme == Uri.UriSchemeFile)
			{
				int num = webResponse.ResponseUri.AbsolutePath.LastIndexOf('.');
				if (num != -1)
				{
					switch (webResponse.ResponseUri.AbsolutePath.Substring(num + 1).ToLower(CultureInfo.InvariantCulture))
					{
					case "xml":
					case "wsdl":
					case "xsd":
					case "disco":
						contentType = "text/xml";
						break;
					}
				}
			}
			return RequestResponseUtils.StreamToMemoryStream(responseStream);
		}
		finally
		{
			responseStream.Close();
		}
	}

	/// <summary>Instructs the <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol" /> object to load any external references.</summary>
	[Obsolete("This method will be removed from a future version. The method call is no longer required for resource discovery", false)]
	[ComVisible(false)]
	public void LoadExternals()
	{
	}

	internal void FixupReferences()
	{
		foreach (DiscoveryReference value in References.Values)
		{
			value.LoadExternals(InlinedSchemas);
		}
		foreach (string key in InlinedSchemas.Keys)
		{
			Documents.Remove(key);
		}
	}

	private static bool IsFilenameInUse(Hashtable filenames, string path)
	{
		return filenames[path.ToLower(CultureInfo.InvariantCulture)] != null;
	}

	private static void AddFilename(Hashtable filenames, string path)
	{
		filenames.Add(path.ToLower(CultureInfo.InvariantCulture), path);
	}

	private static string GetUniqueFilename(Hashtable filenames, string path)
	{
		if (IsFilenameInUse(filenames, path))
		{
			string extension = Path.GetExtension(path);
			string text = path.Substring(0, path.Length - extension.Length);
			int num = 0;
			do
			{
				path = text + num.ToString(CultureInfo.InvariantCulture) + extension;
				num++;
			}
			while (IsFilenameInUse(filenames, path));
		}
		AddFilename(filenames, path);
		return path;
	}

	/// <summary>Reads in a file containing a map of saved discovery documents populating the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> and <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.References" /> properties, with discovery documents, XML Schema Definition (XSD) schemas, and service descriptions referenced in the file.</summary>
	/// <param name="topLevelFilename">Name of file to read in, containing the map of saved discovery documents. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> containing the results found in the file with the map of saved discovery documents. The file format is a <see cref="T:System.Web.Services.Discovery.DiscoveryClientProtocol.DiscoveryClientResultsFile" /> class serialized into XML; however, one would typically create the file using only the <see cref="M:System.Web.Services.Discovery.DiscoveryClientProtocol.WriteAll(System.String,System.String)" /> method or Disco.exe.</returns>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public DiscoveryClientResultCollection ReadAll(string topLevelFilename)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(DiscoveryClientResultsFile));
		Stream stream = File.OpenRead(topLevelFilename);
		string directoryName = Path.GetDirectoryName(topLevelFilename);
		DiscoveryClientResultsFile discoveryClientResultsFile = null;
		try
		{
			discoveryClientResultsFile = (DiscoveryClientResultsFile)xmlSerializer.Deserialize(stream);
			for (int i = 0; i < discoveryClientResultsFile.Results.Count; i++)
			{
				if (discoveryClientResultsFile.Results[i] == null)
				{
					throw new InvalidOperationException(Res.GetString("WebNullRef"));
				}
				string referenceTypeName = discoveryClientResultsFile.Results[i].ReferenceTypeName;
				if (referenceTypeName == null || referenceTypeName.Length == 0)
				{
					throw new InvalidOperationException(Res.GetString("WebRefInvalidAttribute", "referenceType"));
				}
				DiscoveryReference discoveryReference = (DiscoveryReference)Activator.CreateInstance(Type.GetType(referenceTypeName));
				discoveryReference.ClientProtocol = this;
				string url = discoveryClientResultsFile.Results[i].Url;
				if (url == null || url.Length == 0)
				{
					throw new InvalidOperationException(Res.GetString("WebRefInvalidAttribute2", discoveryReference.GetType().FullName, "url"));
				}
				discoveryReference.Url = url;
				string filename = discoveryClientResultsFile.Results[i].Filename;
				if (filename == null || filename.Length == 0)
				{
					throw new InvalidOperationException(Res.GetString("WebRefInvalidAttribute2", discoveryReference.GetType().FullName, "filename"));
				}
				Stream stream2 = File.OpenRead(Path.Combine(directoryName, discoveryClientResultsFile.Results[i].Filename));
				try
				{
					Documents[discoveryReference.Url] = discoveryReference.ReadDocument(stream2);
				}
				finally
				{
					stream2.Close();
				}
				References[discoveryReference.Url] = discoveryReference;
			}
			ResolveAll();
		}
		finally
		{
			stream.Close();
		}
		return discoveryClientResultsFile.Results;
	}

	/// <summary>Resolves all references to discovery documents, XML Schema Definition (XSD) schemas, and service descriptions in the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.References" /> property, as well as references found in referenced discovery documents.</summary>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public void ResolveAll()
	{
		Errors.Clear();
		int count = InlinedSchemas.Keys.Count;
		while (count != References.Count)
		{
			count = References.Count;
			DiscoveryReference[] array = new DiscoveryReference[References.Count];
			References.Values.CopyTo(array, 0);
			foreach (DiscoveryReference discoveryReference in array)
			{
				if (discoveryReference is DiscoveryDocumentReference)
				{
					try
					{
						((DiscoveryDocumentReference)discoveryReference).ResolveAll(throwOnError: true);
					}
					catch (Exception ex)
					{
						if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
						{
							throw;
						}
						Errors[discoveryReference.Url] = ex;
						if (Tracing.On)
						{
							Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex);
						}
					}
					continue;
				}
				try
				{
					discoveryReference.Resolve();
				}
				catch (Exception ex2)
				{
					if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
					{
						throw;
					}
					Errors[discoveryReference.Url] = ex2;
					if (Tracing.On)
					{
						Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveAll", ex2);
					}
				}
			}
		}
		FixupReferences();
	}

	/// <summary>Resolves all references to discovery documents, XML Schema Definition (XSD) schemas and service descriptions in <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.References" />, as well as references found in those discovery documents.</summary>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public void ResolveOneLevel()
	{
		Errors.Clear();
		DiscoveryReference[] array = new DiscoveryReference[References.Count];
		References.Values.CopyTo(array, 0);
		for (int i = 0; i < array.Length; i++)
		{
			try
			{
				array[i].Resolve();
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				Errors[array[i].Url] = ex;
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "ResolveOneLevel", ex);
				}
			}
		}
	}

	private static string GetRelativePath(string fullPath, string relativeTo)
	{
		string text = Path.GetDirectoryName(Path.GetFullPath(relativeTo));
		string text2 = "";
		while (text.Length > 0)
		{
			if (text.Length <= fullPath.Length && string.Compare(text, fullPath.Substring(0, text.Length), StringComparison.OrdinalIgnoreCase) == 0)
			{
				text2 += fullPath.Substring(text.Length);
				if (text2.StartsWith(Path.DirectorySeparatorChar.ToString() ?? "", StringComparison.Ordinal))
				{
					text2 = text2.Substring(1);
				}
				return text2;
			}
			text2 = text2 + ".." + Path.DirectorySeparatorChar;
			if (text.Length < 2)
			{
				break;
			}
			int num = text.LastIndexOf(Path.DirectorySeparatorChar, text.Length - 2);
			text = text.Substring(0, num + 1);
		}
		return fullPath;
	}

	/// <summary>Writes all discovery documents, XML Schema Definition (XSD) schemas, and Service Descriptions in the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> property to the supplied directory and creates a file in that directory.</summary>
	/// <param name="directory">The directory in which to save all documents currently in the <see cref="P:System.Web.Services.Discovery.DiscoveryClientProtocol.Documents" /> property. </param>
	/// <param name="topLevelFilename">The name of the file to create or overwrite containing a map of all documents saved. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryClientResultCollection" /> containing the results of all files saved.</returns>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public DiscoveryClientResultCollection WriteAll(string directory, string topLevelFilename)
	{
		DiscoveryClientResultsFile discoveryClientResultsFile = new DiscoveryClientResultsFile();
		Hashtable filenames = new Hashtable();
		string text = Path.Combine(directory, topLevelFilename);
		DictionaryEntry[] array = new DictionaryEntry[Documents.Count + InlinedSchemas.Keys.Count];
		int num = 0;
		foreach (DictionaryEntry document in Documents)
		{
			array[num++] = document;
		}
		foreach (DictionaryEntry inlinedSchema in InlinedSchemas)
		{
			array[num++] = inlinedSchema;
		}
		DictionaryEntry[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DictionaryEntry dictionaryEntry3 = array2[i];
			string url = (string)dictionaryEntry3.Key;
			object value = dictionaryEntry3.Value;
			if (value != null)
			{
				DiscoveryReference discoveryReference = References[url];
				string path = ((discoveryReference == null) ? DiscoveryReference.FilenameFromUrl(base.Url) : discoveryReference.DefaultFilename);
				path = GetUniqueFilename(filenames, Path.GetFullPath(Path.Combine(directory, path)));
				discoveryClientResultsFile.Results.Add(new DiscoveryClientResult(discoveryReference?.GetType(), url, GetRelativePath(path, text)));
				Stream stream = File.Create(path);
				try
				{
					discoveryReference.WriteDocument(value, stream);
				}
				finally
				{
					stream.Close();
				}
			}
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(DiscoveryClientResultsFile));
		Stream stream2 = File.Create(text);
		try
		{
			xmlSerializer.Serialize(new StreamWriter(stream2, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)), discoveryClientResultsFile);
		}
		finally
		{
			stream2.Close();
		}
		return discoveryClientResultsFile.Results;
	}
}
