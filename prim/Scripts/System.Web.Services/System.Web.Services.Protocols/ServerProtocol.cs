using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;

namespace System.Web.Services.Protocols;

/// <summary>The .NET Framework uses classes that are derived from the <see cref="T:System.Web.Services.Protocols.ServerProtocol" /> class to process XML Web service requests.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public abstract class ServerProtocol
{
	private delegate string CreateCustomKeyForAspNetWebServiceMetadataCache(Type protocolType, Type serverType, string originalKey);

	private class ServerProtocolCachePressure
	{
		public int Pressure;
	}

	private Type type;

	private HttpRequest request;

	private HttpResponse response;

	private HttpContext context;

	private object target;

	private WebMethodAttribute methodAttr;

	private static object s_InternalSyncObject;

	internal static object InternalSyncObject
	{
		get
		{
			if (s_InternalSyncObject == null)
			{
				object value = new object();
				Interlocked.CompareExchange(ref s_InternalSyncObject, value, null);
			}
			return s_InternalSyncObject;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the derived class.</summary>
	/// <returns>An <see cref="T:System.Web.HttpContext" /> object.</returns>
	protected internal HttpContext Context => context;

	/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the derived class.</summary>
	/// <returns>An <see cref="T:System.Web.HttpRequest" /> object. </returns>
	protected internal HttpRequest Request => request;

	/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the derived class.</summary>
	/// <returns>An <see cref="T:System.Web.HttpResponse" /> object.</returns>
	protected internal HttpResponse Response => response;

	internal Type Type => type;

	/// <summary>Gets the service object that is invoked.</summary>
	/// <returns>The service object that is invoked.</returns>
	protected internal virtual object Target => target;

	internal abstract LogicalMethodInfo MethodInfo { get; }

	internal abstract ServerType ServerType { get; }

	internal abstract bool IsOneWay { get; }

	internal virtual Exception OnewayInitException => null;

	internal WebMethodAttribute MethodAttribute
	{
		get
		{
			if (methodAttr == null)
			{
				methodAttr = MethodInfo.MethodAttribute;
			}
			return methodAttr;
		}
	}

	internal void SetContext(Type type, HttpContext context, HttpRequest request, HttpResponse response)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		this.type = type;
		this.context = context;
		this.request = request;
		this.response = response;
		Initialize();
	}

	internal virtual void CreateServerInstance()
	{
		target = Activator.CreateInstance(ServerType.Type);
		if (target is WebService webService)
		{
			webService.SetContext(context);
		}
	}

	internal virtual void DisposeServerInstance()
	{
		if (target != null)
		{
			if (target is IDisposable disposable)
			{
				disposable.Dispose();
			}
			target = null;
		}
	}

	internal virtual bool WriteException(Exception e, Stream outputStream)
	{
		return false;
	}

	internal abstract bool Initialize();

	internal abstract object[] ReadParameters();

	internal abstract void WriteReturns(object[] returns, Stream outputStream);

	internal string GenerateFaultString(Exception e)
	{
		return GenerateFaultString(e, htmlEscapeMessage: false);
	}

	internal static void SetHttpResponseStatusCode(HttpResponse httpResponse, int statusCode)
	{
		httpResponse.TrySkipIisCustomErrors = true;
		httpResponse.StatusCode = statusCode;
	}

	internal string GenerateFaultString(Exception e, bool htmlEscapeMessage)
	{
		bool flag = Context != null && !Context.IsCustomErrorEnabled;
		if (flag && !htmlEscapeMessage)
		{
			return e.ToString();
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (flag)
		{
			GenerateFaultString(e, stringBuilder);
		}
		else
		{
			for (Exception ex = e; ex != null; ex = ex.InnerException)
			{
				string text = (htmlEscapeMessage ? HttpUtility.HtmlEncode(ex.Message) : ex.Message);
				if (text.Length == 0)
				{
					text = e.GetType().Name;
				}
				stringBuilder.Append(text);
				if (ex.InnerException != null)
				{
					stringBuilder.Append(" ---> ");
				}
			}
		}
		return stringBuilder.ToString();
	}

	private static void GenerateFaultString(Exception e, StringBuilder builder)
	{
		builder.Append(e.GetType().FullName);
		if (e.Message != null && e.Message.Length > 0)
		{
			builder.Append(": ");
			builder.Append(HttpUtility.HtmlEncode(e.Message));
		}
		if (e.InnerException != null)
		{
			builder.Append(" ---> ");
			GenerateFaultString(e.InnerException, builder);
			builder.Append(Environment.NewLine);
			builder.Append("   ");
			builder.Append(Res.GetString("StackTraceEnd"));
		}
		if (e.StackTrace != null)
		{
			builder.Append(Environment.NewLine);
			builder.Append(e.StackTrace);
		}
	}

	internal void WriteOneWayResponse()
	{
		context.Response.ContentType = null;
		Response.StatusCode = 202;
	}

	private static string DefaultCreateCustomKeyForAspNetWebServiceMetadataCache(Type protocolType, Type serverType, string originalKey)
	{
		return originalKey;
	}

	private static CreateCustomKeyForAspNetWebServiceMetadataCache GetCreateCustomKeyForAspNetWebServiceMetadataCacheDelegate(Type serverType)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		string key = "CreateCustomKeyForAspNetWebServiceMetadataCache-" + serverType.FullName;
		CreateCustomKeyForAspNetWebServiceMetadataCache createCustomKeyForAspNetWebServiceMetadataCache = (CreateCustomKeyForAspNetWebServiceMetadataCache)HttpRuntime.Cache.Get(key);
		if (createCustomKeyForAspNetWebServiceMetadataCache == null)
		{
			MethodInfo createKeyMethod = serverType.GetMethod("CreateCustomKeyForAspNetWebServiceMetadataCache", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.ExactBinding, null, new Type[3]
			{
				typeof(Type),
				typeof(Type),
				typeof(string)
			}, null);
			createCustomKeyForAspNetWebServiceMetadataCache = ((!(createKeyMethod == null)) ? ((CreateCustomKeyForAspNetWebServiceMetadataCache)((Type pt, Type st, string originalString) => (string)createKeyMethod.Invoke(null, new object[3] { pt, st, originalString }))) : new CreateCustomKeyForAspNetWebServiceMetadataCache(DefaultCreateCustomKeyForAspNetWebServiceMetadataCache));
			HttpRuntime.Cache.Add(key, createCustomKeyForAspNetWebServiceMetadataCache, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
		}
		return createCustomKeyForAspNetWebServiceMetadataCache;
	}

	private string CreateKey(Type protocolType, Type serverType, bool excludeSchemeHostPort = false, string keySuffix = null)
	{
		string fullName = protocolType.FullName;
		string fullName2 = serverType.FullName;
		string text = serverType.TypeHandle.Value.ToString();
		string text2 = (excludeSchemeHostPort ? Request.Url.AbsolutePath : Request.Url.GetLeftPart(UriPartial.Path));
		StringBuilder stringBuilder = new StringBuilder(fullName.Length + text2.Length + fullName2.Length + text.Length);
		stringBuilder.Append(fullName);
		stringBuilder.Append(text2);
		stringBuilder.Append(fullName2);
		stringBuilder.Append(text);
		if (keySuffix != null)
		{
			stringBuilder.Append(keySuffix);
		}
		return GetCreateCustomKeyForAspNetWebServiceMetadataCacheDelegate(serverType)(protocolType, serverType, stringBuilder.ToString());
	}

	/// <summary>Stores a <see cref="T:System.Object" /> in the cache using a key that is created from the specified protocol type and server type.</summary>
	/// <param name="protocolType">A <see cref="T:System.Type" /> that is used to create the key to store <paramref name="value" /> in the cache.</param>
	/// <param name="serverType">A <see cref="T:System.Type" /> that is used to create the key to store <paramref name="value" /> in the cache.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to be stored in the cache.</param>
	protected void AddToCache(Type protocolType, Type serverType, object value)
	{
		AddToCache(protocolType, serverType, value, excludeSchemeHostPort: false);
	}

	internal void AddToCache(Type protocolType, Type serverType, object value, bool excludeSchemeHostPort)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		HttpRuntime.Cache.Insert(CreateKey(protocolType, serverType, excludeSchemeHostPort), value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
	}

	/// <summary>Retrieves the <see cref="T:System.Object" /> that is stored in the cache using the key that was created from the specified protocol type and server type.</summary>
	/// <param name="protocolType">A <see cref="T:System.Type" /> that is used to create the key to retrieve <paramref name="value" /> from the cache.</param>
	/// <param name="serverType">A <see cref="T:System.Type" /> that is used to create the key to retrieve <paramref name="value" /> from the cache.</param>
	/// <returns>The <see cref="T:System.Object" /> that is stored in the cache using the key that was created from <paramref name="protocolType" /> and <paramref name="serverType" />.</returns>
	protected object GetFromCache(Type protocolType, Type serverType)
	{
		return GetFromCache(protocolType, serverType, excludeSchemeHostPort: false);
	}

	internal object GetFromCache(Type protocolType, Type serverType, bool excludeSchemeHostPort)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		return HttpRuntime.Cache.Get(CreateKey(protocolType, serverType, excludeSchemeHostPort));
	}

	internal bool IsCacheUnderPressure(Type protocolType, Type serverType)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		string key = CreateKey(protocolType, serverType, excludeSchemeHostPort: true, "CachePressure");
		ServerProtocolCachePressure serverProtocolCachePressure = (ServerProtocolCachePressure)HttpRuntime.Cache.Get(key);
		if (serverProtocolCachePressure != null)
		{
			if (serverProtocolCachePressure.Pressure >= 10)
			{
				return false;
			}
			return Interlocked.Increment(ref serverProtocolCachePressure.Pressure) >= 10;
		}
		HttpRuntime.Cache.Insert(key, new ServerProtocolCachePressure
		{
			Pressure = 1
		}, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
		return false;
	}

	/// <summary>The constructor for <see cref="T:System.Web.Services.Protocols.ServerProtocol" />.</summary>
	protected ServerProtocol()
	{
	}
}
