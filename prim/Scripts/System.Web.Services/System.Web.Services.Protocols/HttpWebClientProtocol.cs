using System.Collections;
using System.ComponentModel;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace System.Web.Services.Protocols;

/// <summary>Represents the base class for all XML Web service client proxies that use the HTTP transport protocol.</summary>
[ComVisible(true)]
public abstract class HttpWebClientProtocol : WebClientProtocol
{
	private bool allowAutoRedirect;

	private bool enableDecompression;

	private CookieContainer cookieJar;

	private X509CertificateCollection clientCertificates;

	private IWebProxy proxy;

	private static string UserAgentDefault = "Mozilla/4.0 (compatible; MSIE 6.0; MS Web Services Client Protocol " + Environment.Version.ToString() + ")";

	private string userAgent;

	private bool unsafeAuthenticatedConnectionSharing;

	/// <summary>Gets or sets whether the client automatically follows server redirects.</summary>
	/// <returns>
	///     <see langword="true" /> to automatically redirect the client to follow server redirects; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.Net.WebException">The <see cref="P:System.Web.Services.Protocols.HttpWebClientProtocol.AllowAutoRedirect" /> property is <see langword="false" /> and the Web server attempts to redirect the request. </exception>
	[DefaultValue(false)]
	[WebServicesDescription("ClientProtocolAllowAutoRedirect")]
	public bool AllowAutoRedirect
	{
		get
		{
			return allowAutoRedirect;
		}
		set
		{
			allowAutoRedirect = value;
		}
	}

	/// <summary>Gets or sets the collection of cookies.</summary>
	/// <returns>A <see cref="T:System.Net.CookieContainer" /> that represents the cookies for a Web Services client.</returns>
	[DefaultValue(null)]
	[WebServicesDescription("ClientProtocolCookieContainer")]
	public CookieContainer CookieContainer
	{
		get
		{
			return cookieJar;
		}
		set
		{
			cookieJar = value;
		}
	}

	/// <summary>Gets the collection of client certificates.</summary>
	/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> that represents the client certificates.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("ClientProtocolClientCertificates")]
	public X509CertificateCollection ClientCertificates
	{
		get
		{
			if (clientCertificates == null)
			{
				clientCertificates = new X509CertificateCollection();
			}
			return clientCertificates;
		}
	}

	/// <summary>Gets or sets a value that indicates whether decompression is enabled for this <see cref="T:System.Web.Services.Protocols.HttpWebClientProtocol" />. </summary>
	/// <returns>
	///     <see langword="true" /> if decompression is enabled for this <see cref="T:System.Web.Services.Protocols.HttpWebClientProtocol" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebServicesDescription("ClientProtocolEnableDecompression")]
	public bool EnableDecompression
	{
		get
		{
			return enableDecompression;
		}
		set
		{
			enableDecompression = value;
		}
	}

	/// <summary>Gets or sets the value for the user agent header that is sent with each request.</summary>
	/// <returns>The value of the HTTP protocol user agent header. The default is "MS Web Services Client Protocol <paramref name="number" /> ", where <paramref name="number" /> is the version of the common language runtime (for example, 1.0.3705.0).</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebServicesDescription("ClientProtocolUserAgent")]
	public string UserAgent
	{
		get
		{
			if (userAgent != null)
			{
				return userAgent;
			}
			return string.Empty;
		}
		set
		{
			userAgent = value;
		}
	}

	/// <summary>Gets or sets proxy information for making an XML Web service request through a firewall.</summary>
	/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains the proxy information for making requests through a firewall. The default value is the operating system proxy settings.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IWebProxy Proxy
	{
		get
		{
			return proxy;
		}
		set
		{
			proxy = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether connection sharing is enabled when the client uses NTLM authentication to connect to the Web server that hosts the XML Web service.</summary>
	/// <returns>
	///     <see langword="true" /> if connection sharing is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public bool UnsafeAuthenticatedConnectionSharing
	{
		get
		{
			return unsafeAuthenticatedConnectionSharing;
		}
		set
		{
			unsafeAuthenticatedConnectionSharing = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpWebClientProtocol" /> class.</summary>
	protected HttpWebClientProtocol()
	{
		allowAutoRedirect = false;
		userAgent = UserAgentDefault;
	}

	internal HttpWebClientProtocol(HttpWebClientProtocol protocol)
		: base(protocol)
	{
		allowAutoRedirect = protocol.allowAutoRedirect;
		enableDecompression = protocol.enableDecompression;
		cookieJar = protocol.cookieJar;
		clientCertificates = protocol.clientCertificates;
		proxy = protocol.proxy;
		userAgent = protocol.userAgent;
	}

	/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> for the specified URI.</summary>
	/// <param name="uri">The <see cref="T:System.Uri" /> for creating the <see cref="T:System.Net.WebRequest" />. </param>
	/// <returns>The created <see cref="T:System.Net.WebRequest" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is <see langword="null" />. </exception>
	protected override WebRequest GetWebRequest(Uri uri)
	{
		WebRequest webRequest = base.GetWebRequest(uri);
		if (webRequest is HttpWebRequest httpWebRequest)
		{
			httpWebRequest.UserAgent = UserAgent;
			httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
			httpWebRequest.AutomaticDecompression = (enableDecompression ? DecompressionMethods.GZip : DecompressionMethods.None);
			httpWebRequest.AllowWriteStreamBuffering = true;
			httpWebRequest.SendChunked = false;
			if (unsafeAuthenticatedConnectionSharing != httpWebRequest.UnsafeAuthenticatedConnectionSharing)
			{
				httpWebRequest.UnsafeAuthenticatedConnectionSharing = unsafeAuthenticatedConnectionSharing;
			}
			if (proxy != null)
			{
				httpWebRequest.Proxy = proxy;
			}
			if (clientCertificates != null && clientCertificates.Count > 0)
			{
				httpWebRequest.ClientCertificates.AddRange(clientCertificates);
			}
			httpWebRequest.CookieContainer = cookieJar;
		}
		return webRequest;
	}

	/// <summary>Returns a response from a synchronous request to an XML Web service method.</summary>
	/// <param name="request">The <see cref="T:System.Net.WebRequest" /> from which to get the response. </param>
	/// <returns>A response from a synchronous request to an XML Web service method.</returns>
	protected override WebResponse GetWebResponse(WebRequest request)
	{
		return base.GetWebResponse(request);
	}

	/// <summary>Returns a response from an asynchronous request to an XML Web service method.</summary>
	/// <param name="request">The <see cref="T:System.Net.WebRequest" /> from which to get the response. </param>
	/// <param name="result">The <see cref="T:System.IAsyncResult" /> to pass to <see cref="M:System.Net.HttpWebRequest.EndGetResponse(System.IAsyncResult)" /> when the response has completed. </param>
	/// <returns>A response from an asynchronous request to an XML Web service method.</returns>
	protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
	{
		return base.GetWebResponse(request, result);
	}

	/// <summary>Cancels an asynchronous call to an XML Web service method, unless the call has already completed.</summary>
	/// <param name="userState">The object provided in the last parameter to the asynchronous call of the <see langword="Begin" /> method.</param>
	protected void CancelAsync(object userState)
	{
		if (userState == null)
		{
			userState = base.NullToken;
		}
		OperationCompleted(userState, new object[1], null, canceled: true)?.Abort();
	}

	internal WebClientAsyncResult OperationCompleted(object userState, object[] parameters, Exception e, bool canceled)
	{
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)base.AsyncInvokes[userState];
		if (webClientAsyncResult != null)
		{
			AsyncOperation obj = (AsyncOperation)webClientAsyncResult.AsyncState;
			UserToken userToken = (UserToken)obj.UserSuppliedState;
			InvokeCompletedEventArgs arg = new InvokeCompletedEventArgs(parameters, e, canceled, userState);
			base.AsyncInvokes.Remove(userState);
			obj.PostOperationCompleted(userToken.Callback, arg);
		}
		return webClientAsyncResult;
	}

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> for each XML Web service method exposed by the specified type, and stores the mappings in the specified <see cref="T:System.Collections.ArrayList" />.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> that exposes the XML Web service methods.</param>
	/// <param name="mappings">A <see cref="T:System.Collections.ArrayList" /> that is used to store the mappings.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="type" /> can be assigned to a <see cref="T:System.Web.Services.Protocols.SoapHttpClientProtocol" />; otherwise, <see langword="false" />.</returns>
	public static bool GenerateXmlMappings(Type type, ArrayList mappings)
	{
		if (typeof(SoapHttpClientProtocol).IsAssignableFrom(type))
		{
			string @namespace = (WebServiceBindingReflector.GetAttribute(type) ?? throw new InvalidOperationException(Res.GetString("WebClientBindingAttributeRequired"))).Namespace;
			bool serviceDefaultIsEncoded = SoapReflector.ServiceDefaultIsEncoded(type);
			ArrayList soapMethodList = new ArrayList();
			SoapClientType.GenerateXmlMappings(type, soapMethodList, @namespace, serviceDefaultIsEncoded, mappings);
			return true;
		}
		return false;
	}

	/// <summary>Gets the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> for each XML Web service method exposed by the specified types, and stores the mappings in the specified <see cref="T:System.Collections.ArrayList" />, as well as in a <see cref="T:System.Collections.Hashtable" /> that this method returns.</summary>
	/// <param name="types">An array of type <see cref="T:System.Type" /> that contains the types that expose the XML Web service methods.</param>
	/// <param name="mappings">A <see cref="T:System.Collections.ArrayList" /> that is used to store the mappings.</param>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> that contains the <see cref="T:System.Xml.Serialization.XmlMembersMapping" /> for each XML Web service method exposed by the specified types. The types contained in <paramref name="types" /> are used as keys.</returns>
	public static Hashtable GenerateXmlMappings(Type[] types, ArrayList mappings)
	{
		if (types == null)
		{
			throw new ArgumentNullException("types");
		}
		Hashtable hashtable = new Hashtable();
		foreach (Type type in types)
		{
			ArrayList value = new ArrayList();
			if (GenerateXmlMappings(type, mappings))
			{
				hashtable.Add(type, value);
				mappings.Add(value);
			}
		}
		return hashtable;
	}
}
