using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Handlers;

/// <summary>Provides an HTTP handler used to load embedded resources from assemblies. This class cannot be inherited.</summary>
public sealed class AssemblyResourceLoader : IHttpHandler
{
	private sealed class PerformSubstitutionHelper
	{
		private readonly Assembly _assembly;

		private static readonly Regex _regex = new Regex("\\<%=[ ]*WebResource[ ]*\\([ ]*\"([^\"]+)\"[ ]*\\)[ ]*%\\>");

		public PerformSubstitutionHelper(Assembly assembly)
		{
			_assembly = assembly;
		}

		public void PerformSubstitution(TextReader reader, TextWriter writer)
		{
			for (string text = reader.ReadLine(); text != null; text = reader.ReadLine())
			{
				if (text.Length > 0 && _regex.IsMatch(text))
				{
					text = _regex.Replace(text, PerformSubstitutionReplace);
				}
				writer.WriteLine(text);
			}
		}

		private string PerformSubstitutionReplace(Match m)
		{
			string value = m.Groups[1].Value;
			return GetResourceUrl(_assembly, value, notifyScriptLoaded: false);
		}
	}

	private sealed class EmbeddedResource
	{
		public string Name;

		public string Url;

		public WebResourceAttribute Attribute;
	}

	private sealed class AssemblyEmbeddedResources
	{
		public string AssemblyName = string.Empty;

		public Dictionary<string, EmbeddedResource> Resources = new Dictionary<string, EmbeddedResource>(StringComparer.Ordinal);
	}

	private const string HandlerFileName = "WebResource.axd";

	private static Assembly currAsm = typeof(AssemblyResourceLoader).Assembly;

	private const char QueryParamSeparator = '&';

	private static readonly Dictionary<string, AssemblyEmbeddedResources> _embeddedResources = new Dictionary<string, AssemblyEmbeddedResources>(StringComparer.Ordinal);

	private static readonly ReaderWriterLockSlim _embeddedResourcesLock = new ReaderWriterLockSlim();

	private static readonly ReaderWriterLockSlim _stringHashCacheLock = new ReaderWriterLockSlim();

	private static readonly Dictionary<string, string> stringHashCache = new Dictionary<string, string>(StringComparer.Ordinal);

	[ThreadStatic]
	private static KeyedHashAlgorithm hashAlg;

	private static bool canReuseHashAlg = true;

	private static KeyedHashAlgorithm ReusableHashAlgorithm
	{
		get
		{
			if (!canReuseHashAlg)
			{
				return null;
			}
			if (hashAlg == null)
			{
				MachineKeySection config = MachineKeySection.Config;
				hashAlg = MachineKeySectionUtils.GetValidationAlgorithm(config);
				if (!hashAlg.CanReuseTransform)
				{
					canReuseHashAlg = false;
					hashAlg = null;
					return null;
				}
				hashAlg.Key = MachineKeySectionUtils.GetValidationKey(config);
			}
			if (hashAlg != null)
			{
				hashAlg.Initialize();
			}
			return hashAlg;
		}
	}

	/// <summary>Gets a value that indicates whether another request can reuse the <see cref="T:System.Web.IHttpHandler" /> instance. </summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	bool IHttpHandler.IsReusable => true;

	private static string GetStringHash(KeyedHashAlgorithm kha, string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return string.Empty;
		}
		try
		{
			_stringHashCacheLock.EnterUpgradeableReadLock();
			if (stringHashCache.TryGetValue(str, out var value))
			{
				return value;
			}
			try
			{
				_stringHashCacheLock.EnterWriteLock();
				if (stringHashCache.TryGetValue(str, out value))
				{
					return value;
				}
				value = Convert.ToBase64String(kha.ComputeHash(Encoding.UTF8.GetBytes(str)));
				stringHashCache.Add(str, value);
				return value;
			}
			finally
			{
				_stringHashCacheLock.ExitWriteLock();
			}
		}
		finally
		{
			_stringHashCacheLock.ExitUpgradeableReadLock();
		}
	}

	private static void InitEmbeddedResourcesUrls(KeyedHashAlgorithm kha, Assembly assembly, string assemblyName, string assemblyHash, AssemblyEmbeddedResources entry)
	{
		WebResourceAttribute[] array = (WebResourceAttribute[])assembly.GetCustomAttributes(typeof(WebResourceAttribute), inherit: false);
		string location = assembly.Location;
		foreach (WebResourceAttribute webResourceAttribute in array)
		{
			string webResource = webResourceAttribute.WebResource;
			if (!string.IsNullOrEmpty(webResource))
			{
				string stringHash = GetStringHash(kha, webResource);
				if (!entry.Resources.ContainsKey(stringHash))
				{
					EmbeddedResource value = new EmbeddedResource
					{
						Name = webResource,
						Attribute = webResourceAttribute,
						Url = CreateResourceUrl(kha, assemblyName, assemblyHash, location, stringHash, debug: false, notifyScriptLoaded: false, includeTimeStamp: true)
					};
					entry.Resources.Add(stringHash, value);
				}
			}
		}
	}

	internal static string GetResourceUrl(Type type, string resourceName)
	{
		return GetResourceUrl(type.Assembly, resourceName, notifyScriptLoaded: false);
	}

	private static EmbeddedResource DecryptAssemblyResource(string val, out AssemblyEmbeddedResources entry)
	{
		entry = null;
		string[] array = val.Split('_');
		if (array.Length != 3)
		{
			return null;
		}
		string key = array[0];
		string key2 = array[1];
		try
		{
			_embeddedResourcesLock.EnterReadLock();
			if (!_embeddedResources.TryGetValue(key, out entry) || entry == null)
			{
				return null;
			}
			if (!entry.Resources.TryGetValue(key2, out var value) || value == null)
			{
				return null;
			}
			return value;
		}
		finally
		{
			_embeddedResourcesLock.ExitReadLock();
		}
	}

	private static void GetAssemblyNameAndHashes(KeyedHashAlgorithm kha, Assembly assembly, string resourceName, out string assemblyName, out string assemblyNameHash, out string resourceNameHash)
	{
		assemblyName = ((assembly == currAsm) ? "s" : assembly.GetName().FullName);
		assemblyNameHash = GetStringHash(kha, assemblyName);
		resourceNameHash = GetStringHash(kha, resourceName);
	}

	private static AssemblyEmbeddedResources GetAssemblyEmbeddedResource(KeyedHashAlgorithm kha, Assembly assembly, string assemblyNameHash, string assemblyName)
	{
		if (!_embeddedResources.TryGetValue(assemblyNameHash, out var value) || value == null)
		{
			try
			{
				_embeddedResourcesLock.EnterWriteLock();
				value = new AssemblyEmbeddedResources
				{
					AssemblyName = assemblyName
				};
				InitEmbeddedResourcesUrls(kha, assembly, assemblyName, assemblyNameHash, value);
				_embeddedResources.Add(assemblyNameHash, value);
			}
			finally
			{
				_embeddedResourcesLock.ExitWriteLock();
			}
		}
		return value;
	}

	internal static string GetResourceUrl(Assembly assembly, string resourceName, bool notifyScriptLoaded)
	{
		if (assembly == null)
		{
			return string.Empty;
		}
		KeyedHashAlgorithm reusableHashAlgorithm = ReusableHashAlgorithm;
		if (reusableHashAlgorithm != null)
		{
			return GetResourceUrl(reusableHashAlgorithm, assembly, resourceName, notifyScriptLoaded);
		}
		MachineKeySection config = MachineKeySection.Config;
		using (reusableHashAlgorithm = MachineKeySectionUtils.GetValidationAlgorithm(config))
		{
			reusableHashAlgorithm.Key = MachineKeySectionUtils.GetValidationKey(config);
			return GetResourceUrl(reusableHashAlgorithm, assembly, resourceName, notifyScriptLoaded);
		}
	}

	private static string GetResourceUrl(KeyedHashAlgorithm kha, Assembly assembly, string resourceName, bool notifyScriptLoaded)
	{
		GetAssemblyNameAndHashes(kha, assembly, resourceName, out var assemblyName, out var assemblyNameHash, out var resourceNameHash);
		bool debug = false;
		bool includeTimeStamp = true;
		string text;
		try
		{
			_embeddedResourcesLock.EnterUpgradeableReadLock();
			AssemblyEmbeddedResources assemblyEmbeddedResource = GetAssemblyEmbeddedResource(kha, assembly, assemblyNameHash, assemblyName);
			string key = resourceNameHash;
			text = ((!assemblyEmbeddedResource.Resources.TryGetValue(key, out var value) || value == null) ? null : value.Url);
		}
		finally
		{
			_embeddedResourcesLock.ExitUpgradeableReadLock();
		}
		if (text == null)
		{
			text = CreateResourceUrl(kha, assemblyName, assemblyNameHash, assembly.Location, resourceNameHash, debug, notifyScriptLoaded, includeTimeStamp);
		}
		return text;
	}

	private static string CreateResourceUrl(KeyedHashAlgorithm kha, string assemblyName, string assemblyNameHash, string assemblyPath, string resourceNameHash, bool debug, bool notifyScriptLoaded, bool includeTimeStamp)
	{
		string text = string.Empty;
		string empty = string.Empty;
		if (includeTimeStamp)
		{
			text = ((string.IsNullOrEmpty(assemblyPath) || !File.Exists(assemblyPath)) ? ("&t=" + DateTime.UtcNow.Ticks) : ("&t=" + File.GetLastWriteTimeUtc(assemblyPath).Ticks));
		}
		string text2 = HttpUtility.UrlEncode(assemblyNameHash + "_" + resourceNameHash + (debug ? "_t" : "_f"));
		string text3 = "WebResource.axd?d=" + text2 + text + empty;
		HttpRequest httpRequest = HttpContext.Current?.Request;
		if (httpRequest != null)
		{
			text3 = VirtualPathUtility.AppendTrailingSlash(httpRequest.ApplicationPath) + text3;
		}
		return text3;
	}

	private bool HasIfModifiedSince(HttpRequest request, out DateTime modified)
	{
		string text = request.Headers["If-Modified-Since"];
		if (string.IsNullOrEmpty(text))
		{
			modified = DateTime.MinValue;
			return false;
		}
		try
		{
			if (DateTime.TryParseExact(text, "r", null, DateTimeStyles.None, out modified))
			{
				return true;
			}
		}
		catch
		{
			modified = DateTime.MinValue;
		}
		return false;
	}

	private void RespondWithNotModified(HttpContext context)
	{
		HttpResponse response = context.Response;
		response.Clear();
		response.StatusCode = 304;
		response.ContentType = null;
		context.ApplicationInstance.CompleteRequest();
	}

	private unsafe void SendEmbeddedResource(HttpContext context, out EmbeddedResource res, out Assembly assembly)
	{
		HttpRequest request = context.Request;
		string text = request.QueryString["d"];
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Replace(' ', '+');
		}
		res = DecryptAssemblyResource(text, out var entry);
		WebResourceAttribute webResourceAttribute = ((res != null) ? res.Attribute : null);
		if (webResourceAttribute == null)
		{
			throw new HttpException(404, "Resource not found");
		}
		if (entry.AssemblyName == "s")
		{
			assembly = currAsm;
		}
		else
		{
			assembly = Assembly.Load(entry.AssemblyName);
		}
		if (HasIfModifiedSince(request, out var modified) && File.GetLastWriteTimeUtc(assembly.Location) <= modified)
		{
			RespondWithNotModified(context);
			return;
		}
		HttpResponse response = context.Response;
		response.ContentType = webResourceAttribute.ContentType;
		DateTime utcNow = DateTime.UtcNow;
		response.Headers.Add("Last-Modified", utcNow.ToString("r"));
		response.ExpiresAbsolute = utcNow.AddYears(1);
		response.CacheControl = "public";
		Stream manifestResourceStream = assembly.GetManifestResourceStream(res.Name);
		if (manifestResourceStream == null)
		{
			throw new HttpException(404, "Resource " + res.Name + " not found");
		}
		if (webResourceAttribute.PerformSubstitution)
		{
			using (StreamReader reader = new StreamReader(manifestResourceStream))
			{
				TextWriter output = response.Output;
				new PerformSubstitutionHelper(assembly).PerformSubstitution(reader, output);
				return;
			}
		}
		if (response.OutputStream is HttpResponseStream)
		{
			UnmanagedMemoryStream unmanagedMemoryStream = (UnmanagedMemoryStream)manifestResourceStream;
			((HttpResponseStream)response.OutputStream).WritePtr(new IntPtr(unmanagedMemoryStream.PositionPointer), (int)unmanagedMemoryStream.Length);
			return;
		}
		byte[] buffer = new byte[1024];
		Stream outputStream = response.OutputStream;
		int num;
		do
		{
			num = manifestResourceStream.Read(buffer, 0, 1024);
			outputStream.Write(buffer, 0, num);
		}
		while (num > 0);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.IHttpHandler.ProcessRequest(System.Web.HttpContext)" />.</summary>
	/// <param name="context">The context of the request.</param>
	/// <exception cref="T:System.Web.HttpException">The Web resource request is invalid.- or -The assembly name could not be found.- or -The resource name could not be found in the assembly.</exception>
	void IHttpHandler.ProcessRequest(HttpContext context)
	{
		SendEmbeddedResource(context, out var _, out var _);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Handlers.AssemblyResourceLoader" /> class. This constructor supports the ASP.NET infrastructure and is not intended to be used directly from your code.</summary>
	public AssemblyResourceLoader()
	{
	}
}
