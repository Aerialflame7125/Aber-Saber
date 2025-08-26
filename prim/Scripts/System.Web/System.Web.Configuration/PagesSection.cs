using System.ComponentModel;
using System.Configuration;
using System.Web.UI;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Provides programmatic access to the  section of the configuration file. This class cannot be inherited.</summary>
public sealed class PagesSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty asyncTimeoutProp;

	private static ConfigurationProperty autoEventWireupProp;

	private static ConfigurationProperty bufferProp;

	private static ConfigurationProperty controlsProp;

	private static ConfigurationProperty enableEventValidationProp;

	private static ConfigurationProperty enableSessionStateProp;

	private static ConfigurationProperty enableViewStateProp;

	private static ConfigurationProperty enableViewStateMacProp;

	private static ConfigurationProperty maintainScrollPositionOnPostBackProp;

	private static ConfigurationProperty masterPageFileProp;

	private static ConfigurationProperty maxPageStateFieldLengthProp;

	private static ConfigurationProperty modeProp;

	private static ConfigurationProperty namespacesProp;

	private static ConfigurationProperty pageBaseTypeProp;

	private static ConfigurationProperty pageParserFilterTypeProp;

	private static ConfigurationProperty smartNavigationProp;

	private static ConfigurationProperty styleSheetThemeProp;

	private static ConfigurationProperty tagMappingProp;

	private static ConfigurationProperty themeProp;

	private static ConfigurationProperty userControlBaseTypeProp;

	private static ConfigurationProperty validateRequestProp;

	private static ConfigurationProperty viewStateEncryptionModeProp;

	private static ConfigurationProperty clientIDModeProp;

	private static ConfigurationProperty controlRenderingCompatibilityVersionProp;

	/// <summary>Gets or sets a value indicating the number of seconds to wait for an asynchronous handler to complete during asynchronous page processing.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the amount of time in seconds to wait for an asynchronous handler to complete during asynchronous page processing.</returns>
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[TypeConverter(typeof(TimeSpanSecondsConverter))]
	[ConfigurationProperty("asyncTimeout", DefaultValue = "00:00:45")]
	public TimeSpan AsyncTimeout
	{
		get
		{
			return (TimeSpan)base[asyncTimeoutProp];
		}
		set
		{
			base[asyncTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether events for ASP.NET pages are automatically connected to event-handling functions.</summary>
	/// <returns>
	///     <see langword="true" /> if events for ASP.NET pages are automatically connected to event-handling functions; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("autoEventWireup", DefaultValue = true)]
	public bool AutoEventWireup
	{
		get
		{
			return (bool)base[autoEventWireupProp];
		}
		set
		{
			base[autoEventWireupProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether .aspx pages and .ascx controls use response buffering.</summary>
	/// <returns>
	///     <see langword="true" /> if .aspx pages and .ascx controls use response buffering; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("buffer", DefaultValue = true)]
	public bool Buffer
	{
		get
		{
			return (bool)base[bufferProp];
		}
		set
		{
			base[bufferProp] = value;
		}
	}

	/// <summary>Gets or sets a value that determines how .aspx pages and .ascx controls are compiled.</summary>
	/// <returns>One of the values for the <see cref="P:System.Web.Configuration.PagesSection.CompilationMode" /> property, which specifies how .aspx pages and .ascx controls are compiled.</returns>
	[ConfigurationProperty("compilationMode", DefaultValue = CompilationMode.Always)]
	public CompilationMode CompilationMode
	{
		get
		{
			return (CompilationMode)base[modeProp];
		}
		set
		{
			base[modeProp] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.TagPrefixCollection" /> of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</returns>
	[ConfigurationProperty("controls")]
	public TagPrefixCollection Controls => (TagPrefixCollection)base[controlsProp];

	/// <summary>Gets or sets a value that specifies whether event validation is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if event validation is enabled; otherwise, <see langword="false" />.</returns>
	[ConfigurationProperty("enableEventValidation", DefaultValue = true)]
	public bool EnableEventValidation
	{
		get
		{
			return (bool)base[enableEventValidationProp];
		}
		set
		{
			base[enableEventValidationProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether the session state is enabled, disabled, or read-only.</summary>
	/// <returns>One of the values for the <see cref="P:System.Web.Configuration.PagesSection.EnableSessionState" /> property, which specifies whether the session state is enabled, disabled, or read-only. The default is <see cref="F:System.Web.Configuration.PagesEnableSessionState.True" />, which indicates that session state is enabled.</returns>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is not a valid <see cref="T:System.Web.Configuration.PagesEnableSessionState" /> enumeration value.</exception>
	[ConfigurationProperty("enableSessionState", DefaultValue = "true")]
	public PagesEnableSessionState EnableSessionState
	{
		get
		{
			return (string)base[enableSessionStateProp] switch
			{
				"true" => PagesEnableSessionState.True, 
				"false" => PagesEnableSessionState.False, 
				"ReadOnly" => PagesEnableSessionState.ReadOnly, 
				_ => throw new ConfigurationErrorsException("The 'enableSessionState' attribute must be one of the following values: true,false, ReadOnly."), 
			};
		}
		set
		{
			switch (value)
			{
			case PagesEnableSessionState.False:
				base[enableSessionStateProp] = "false";
				break;
			case PagesEnableSessionState.ReadOnly:
				base[enableSessionStateProp] = "ReadOnly";
				break;
			default:
				base[enableSessionStateProp] = "true";
				break;
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether view state is enabled or disabled.</summary>
	/// <returns>
	///     <see langword="true" /> if view state is enabled; <see langword="false" /> if view state is disabled. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enableViewState", DefaultValue = true)]
	public bool EnableViewState
	{
		get
		{
			return (bool)base[enableViewStateProp];
		}
		set
		{
			base[enableViewStateProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether ASP.NET should run a message authentication code (MAC) on the page's view state when the page is posted back from the client.</summary>
	/// <returns>
	///     <see langword="true" /> if ASP.NET should run a message authentication code (MAC) on the page's view state when the page is posted back from the client; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enableViewStateMac", DefaultValue = true)]
	public bool EnableViewStateMac
	{
		get
		{
			return (bool)base[enableViewStateMacProp];
		}
		set
		{
			base[enableViewStateMacProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the page scroll position should be maintained upon returning from a postback from the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the page-scroll position should be maintained after postback; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[ConfigurationProperty("maintainScrollPositionOnPostBack", DefaultValue = false)]
	public bool MaintainScrollPositionOnPostBack
	{
		get
		{
			return (bool)base[maintainScrollPositionOnPostBackProp];
		}
		set
		{
			base[maintainScrollPositionOnPostBackProp] = value;
		}
	}

	/// <summary>Gets or sets a reference to the master page for the application. </summary>
	/// <returns>A reference to the master page for the application.</returns>
	[ConfigurationProperty("masterPageFile", DefaultValue = "")]
	public string MasterPageFile
	{
		get
		{
			return (string)base[masterPageFileProp];
		}
		set
		{
			base[masterPageFileProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of characters that a single view-state field can contain.</summary>
	/// <returns>The maximum number of characters that a single view-state field can contain.</returns>
	[ConfigurationProperty("maxPageStateFieldLength", DefaultValue = -1)]
	public int MaxPageStateFieldLength
	{
		get
		{
			return (int)base[maxPageStateFieldLengthProp];
		}
		set
		{
			base[maxPageStateFieldLengthProp] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.NamespaceCollection" /> of <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects.</returns>
	[ConfigurationProperty("namespaces")]
	public NamespaceCollection Namespaces => (NamespaceCollection)base[namespacesProp];

	/// <summary>Gets or sets a value that specifies a code-behind class that .aspx pages inherit by default.</summary>
	/// <returns>A string that specifies a code-behind class that .aspx pages inherit by default.</returns>
	[ConfigurationProperty("pageBaseType", DefaultValue = "System.Web.UI.Page")]
	public string PageBaseType
	{
		get
		{
			return (string)base[pageBaseTypeProp];
		}
		set
		{
			base[pageBaseTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies the parser filter type.</summary>
	/// <returns>A string that specifies the parser filter type.</returns>
	[ConfigurationProperty("pageParserFilterType", DefaultValue = "")]
	public string PageParserFilterType
	{
		get
		{
			return (string)base[pageParserFilterTypeProp];
		}
		set
		{
			base[pageParserFilterTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether smart navigation is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if smart navigation is enabled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[ConfigurationProperty("smartNavigation", DefaultValue = false)]
	public bool SmartNavigation
	{
		get
		{
			return (bool)base[smartNavigationProp];
		}
		set
		{
			base[smartNavigationProp] = value;
		}
	}

	/// <summary>Gets or sets the name of an ASP.NET style sheet theme.</summary>
	/// <returns>The name of an ASP.NET style sheet theme.</returns>
	[ConfigurationProperty("styleSheetTheme", DefaultValue = "")]
	public string StyleSheetTheme
	{
		get
		{
			return (string)base[styleSheetThemeProp];
		}
		set
		{
			base[styleSheetThemeProp] = value;
		}
	}

	/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.TagMapCollection" /> of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects.</returns>
	[ConfigurationProperty("tagMapping")]
	public TagMapCollection TagMapping => (TagMapCollection)base[tagMappingProp];

	/// <summary>Gets or sets the name of an ASP.NET page theme.</summary>
	/// <returns>The name of an ASP.NET page theme.</returns>
	[ConfigurationProperty("theme", DefaultValue = "")]
	public string Theme
	{
		get
		{
			return (string)base[themeProp];
		}
		set
		{
			base[themeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies a code-behind class that user controls inherit by default.</summary>
	/// <returns>A string that specifies a code-behind file that user controls inherit by default.</returns>
	[ConfigurationProperty("userControlBaseType", DefaultValue = "System.Web.UI.UserControl")]
	public string UserControlBaseType
	{
		get
		{
			return (string)base[userControlBaseTypeProp];
		}
		set
		{
			base[userControlBaseTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that determines whether ASP.NET examines input from the browser for dangerous values. For more information, see Script Exploits Overview.</summary>
	/// <returns>
	///     <see langword="true" /> if ASP.NET examines input from the browser for dangerous values; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("validateRequest", DefaultValue = true)]
	public bool ValidateRequest
	{
		get
		{
			return (bool)base[validateRequestProp];
		}
		set
		{
			base[validateRequestProp] = value;
		}
	}

	/// <summary>Gets or sets the encryption mode that ASP.NET uses when maintaining <see langword="ViewState" /> values.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> enumeration value indicating when the <see langword="ViewState" /> values are encrypted.</returns>
	[ConfigurationProperty("viewStateEncryptionMode", DefaultValue = ViewStateEncryptionMode.Auto)]
	public ViewStateEncryptionMode ViewStateEncryptionMode
	{
		get
		{
			return (ViewStateEncryptionMode)base[viewStateEncryptionModeProp];
		}
		set
		{
			base[viewStateEncryptionModeProp] = value;
		}
	}

	/// <summary>Gets or sets the default algorithm that is used to generate a control's identifier.</summary>
	/// <returns>A value that indicates how the value in the <see cref="P:System.Web.UI.Control.ClientID" /> property is generated. The default value is <see cref="F:System.Web.UI.ClientIDMode.Predictable" />.</returns>
	[ConfigurationProperty("clientIDMode", DefaultValue = ClientIDMode.Predictable)]
	public ClientIDMode ClientIDMode
	{
		get
		{
			return (ClientIDMode)base[clientIDModeProp];
		}
		set
		{
			base[clientIDModeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that specifies the ASP.NET version that any rendered HTML will be compatible with.</summary>
	/// <returns>The ASP.NET version that any rendered HTML will be compatible with.</returns>
	/// <exception cref="T:System.ArgumentNullException">An attempt was made to set this property to <see langword="null" />.</exception>
	[ConfigurationProperty("controlRenderingCompatibilityVersion", DefaultValue = "4.0")]
	public Version ControlRenderingCompatibilityVersion
	{
		get
		{
			return (Version)base[controlRenderingCompatibilityVersionProp];
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base[controlRenderingCompatibilityVersionProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static PagesSection()
	{
		asyncTimeoutProp = new ConfigurationProperty("asyncTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(45.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		autoEventWireupProp = new ConfigurationProperty("autoEventWireup", typeof(bool), true);
		bufferProp = new ConfigurationProperty("buffer", typeof(bool), true);
		controlsProp = new ConfigurationProperty("controls", typeof(TagPrefixCollection), null, null, null, ConfigurationPropertyOptions.None);
		enableEventValidationProp = new ConfigurationProperty("enableEventValidation", typeof(bool), true);
		enableSessionStateProp = new ConfigurationProperty("enableSessionState", typeof(string), "true");
		enableViewStateProp = new ConfigurationProperty("enableViewState", typeof(bool), true);
		enableViewStateMacProp = new ConfigurationProperty("enableViewStateMac", typeof(bool), true);
		maintainScrollPositionOnPostBackProp = new ConfigurationProperty("maintainScrollPositionOnPostBack", typeof(bool), false);
		masterPageFileProp = new ConfigurationProperty("masterPageFile", typeof(string), "");
		maxPageStateFieldLengthProp = new ConfigurationProperty("maxPageStateFieldLength", typeof(int), -1);
		modeProp = new ConfigurationProperty("compilationMode", typeof(CompilationMode), CompilationMode.Always, new GenericEnumConverter(typeof(CompilationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		namespacesProp = new ConfigurationProperty("namespaces", typeof(NamespaceCollection), null, null, null, ConfigurationPropertyOptions.None);
		pageBaseTypeProp = new ConfigurationProperty("pageBaseType", typeof(string), "System.Web.UI.Page");
		pageParserFilterTypeProp = new ConfigurationProperty("pageParserFilterType", typeof(string), "");
		smartNavigationProp = new ConfigurationProperty("smartNavigation", typeof(bool), false);
		styleSheetThemeProp = new ConfigurationProperty("styleSheetTheme", typeof(string), "");
		tagMappingProp = new ConfigurationProperty("tagMapping", typeof(TagMapCollection), null, null, null, ConfigurationPropertyOptions.None);
		themeProp = new ConfigurationProperty("theme", typeof(string), "");
		userControlBaseTypeProp = new ConfigurationProperty("userControlBaseType", typeof(string), "System.Web.UI.UserControl");
		validateRequestProp = new ConfigurationProperty("validateRequest", typeof(bool), true);
		viewStateEncryptionModeProp = new ConfigurationProperty("viewStateEncryptionMode", typeof(ViewStateEncryptionMode), ViewStateEncryptionMode.Auto, new GenericEnumConverter(typeof(ViewStateEncryptionMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		clientIDModeProp = new ConfigurationProperty("clientIDMode", typeof(ClientIDMode), ClientIDMode.Predictable, new GenericEnumConverter(typeof(ClientIDMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		controlRenderingCompatibilityVersionProp = new ConfigurationProperty("controlRenderingCompatibilityVersion", typeof(Version), new Version(4, 0), new VersionConverter(3, 5, "The value for the property 'controlRenderingCompatibilityVersion' is not valid. The error is: The control rendering compatibility version must not be less than {1}."), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(asyncTimeoutProp);
		properties.Add(autoEventWireupProp);
		properties.Add(bufferProp);
		properties.Add(controlsProp);
		properties.Add(enableEventValidationProp);
		properties.Add(enableSessionStateProp);
		properties.Add(enableViewStateProp);
		properties.Add(enableViewStateMacProp);
		properties.Add(maintainScrollPositionOnPostBackProp);
		properties.Add(masterPageFileProp);
		properties.Add(maxPageStateFieldLengthProp);
		properties.Add(modeProp);
		properties.Add(namespacesProp);
		properties.Add(pageBaseTypeProp);
		properties.Add(pageParserFilterTypeProp);
		properties.Add(smartNavigationProp);
		properties.Add(styleSheetThemeProp);
		properties.Add(tagMappingProp);
		properties.Add(themeProp);
		properties.Add(userControlBaseTypeProp);
		properties.Add(validateRequestProp);
		properties.Add(viewStateEncryptionModeProp);
		properties.Add(clientIDModeProp);
		properties.Add(controlRenderingCompatibilityVersionProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.PagesSection" /> class using default settings.</summary>
	public PagesSection()
	{
	}

	protected internal override void DeserializeSection(XmlReader reader)
	{
		base.DeserializeSection(reader);
	}
}
