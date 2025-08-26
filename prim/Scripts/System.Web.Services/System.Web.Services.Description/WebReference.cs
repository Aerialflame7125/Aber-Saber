using System.CodeDom;
using System.Collections.Specialized;
using System.Web.Services.Discovery;

namespace System.Web.Services.Description;

/// <summary>Describes a reference to a collection of XML Web services.</summary>
public sealed class WebReference
{
	private CodeNamespace proxyCode;

	private DiscoveryClientDocumentCollection documents;

	private string appSettingUrlKey;

	private string appSettingBaseUrl;

	private string protocolName;

	private ServiceDescriptionImportWarnings warnings;

	private StringCollection validationWarnings;

	/// <summary>Gets the base URL of the Web reference.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the base URL of the Web reference.</returns>
	public string AppSettingBaseUrl => appSettingBaseUrl;

	/// <summary>Gets the URL key of the web reference.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the URL key of the Web reference.</returns>
	public string AppSettingUrlKey => appSettingUrlKey;

	/// <summary>Gets the collection of description documents associated with the Web reference.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> used to initialize the <see cref="T:System.Web.Services.Description.WebReference" /> instance.</returns>
	public DiscoveryClientDocumentCollection Documents => documents;

	/// <summary>Gets the code namespace associated with the Web reference.</summary>
	/// <returns>The <see cref="T:System.CodeDom.CodeNamespace" /> in which proxy code will be generated when the <see cref="M:System.Web.Services.Description.ServiceDescriptionImporter.GenerateWebReferences(System.Web.Services.Description.WebReferenceCollection,System.CodeDom.Compiler.CodeDomProvider,System.CodeDom.CodeCompileUnit,System.Web.Services.Description.WebReferenceOptions)" /> method is called.</returns>
	public CodeNamespace ProxyCode => proxyCode;

	/// <summary>Gets a collection of warnings generated when validating the description documents.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> of validation warning text.</returns>
	public StringCollection ValidationWarnings
	{
		get
		{
			if (validationWarnings == null)
			{
				validationWarnings = new StringCollection();
			}
			return validationWarnings;
		}
	}

	/// <summary>Gets a collection of warnings generated when importing Web Services Description Language (WSDL) service description documents.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionImportWarnings" /> collection of warnings generated when importing WSDL service description documents.</returns>
	public ServiceDescriptionImportWarnings Warnings
	{
		get
		{
			return warnings;
		}
		set
		{
			warnings = value;
		}
	}

	/// <summary>Gets or sets the protocol associated with the Web reference.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the name of the protocol associated with the Web reference.</returns>
	public string ProtocolName
	{
		get
		{
			if (protocolName != null)
			{
				return protocolName;
			}
			return string.Empty;
		}
		set
		{
			protocolName = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReference" /> class with the given data.</summary>
	/// <param name="documents">A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />  that specifies a collection of description documents.</param>
	/// <param name="proxyCode">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies a namespace for code compilation.</param>
	/// <param name="protocolName">The protocol used by the XML Web service.</param>
	/// <param name="appSettingUrlKey">The URL key of the Web reference.</param>
	/// <param name="appSettingBaseUrl">The base URL of the Web reference.</param>
	public WebReference(DiscoveryClientDocumentCollection documents, CodeNamespace proxyCode, string protocolName, string appSettingUrlKey, string appSettingBaseUrl)
	{
		if (documents == null)
		{
			throw new ArgumentNullException("documents");
		}
		if (proxyCode == null)
		{
			throw new ArgumentNullException("proxyCode");
		}
		if (appSettingBaseUrl != null && appSettingUrlKey == null)
		{
			throw new ArgumentNullException("appSettingUrlKey");
		}
		this.protocolName = protocolName;
		this.appSettingUrlKey = appSettingUrlKey;
		this.appSettingBaseUrl = appSettingBaseUrl;
		this.documents = documents;
		this.proxyCode = proxyCode;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReference" /> class with the given description document collection and proxy code namespace.</summary>
	/// <param name="documents">A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />  that specifies a collection of description documents.</param>
	/// <param name="proxyCode">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies a namespace for code compilation.</param>
	public WebReference(DiscoveryClientDocumentCollection documents, CodeNamespace proxyCode)
		: this(documents, proxyCode, null, null, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.WebReference" /> class with the given data.</summary>
	/// <param name="documents">A <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />  that specifies a collection of description documents.</param>
	/// <param name="proxyCode">A <see cref="T:System.CodeDom.CodeNamespace" /> that specifies a namespace for code compilation.</param>
	/// <param name="appSettingUrlKey">The URL key of the Web reference.</param>
	/// <param name="appSettingBaseUrl">The base URL of the Web reference.</param>
	public WebReference(DiscoveryClientDocumentCollection documents, CodeNamespace proxyCode, string appSettingUrlKey, string appSettingBaseUrl)
		: this(documents, proxyCode, null, appSettingUrlKey, appSettingBaseUrl)
	{
	}
}
