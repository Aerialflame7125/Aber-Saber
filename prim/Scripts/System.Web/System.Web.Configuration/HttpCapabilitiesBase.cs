using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Configuration;

/// <summary>Provides access to detailed information about the capabilities of the client's browser.</summary>
public class HttpCapabilitiesBase : IFilterResolutionService
{
	internal IDictionary capabilities;

	internal static bool GetConfigCapabilities_called;

	private IDictionary adapters;

	private bool canCombineFormsInDeck;

	private bool canInitiateVoiceCall;

	private bool canRenderAfterInputOrSelectElement;

	private bool canRenderEmptySelects;

	private bool canRenderInputAndSelectElementsTogether;

	private bool canRenderMixedSelects;

	private bool canRenderOneventAndPrevElementsTogether;

	private bool canRenderPostBackCards;

	private bool canRenderSetvarZeroWithMultiSelectionList;

	private bool canSendMail;

	private int defaultSubmitButtonLimit;

	private int gatewayMajorVersion;

	private double gatewayMinorVersion;

	private string gatewayVersion;

	private bool hasBackButton;

	private bool hidesRightAlignedMultiselectScrollbars;

	private string htmlTextWriter;

	private string inputType;

	private bool isColor;

	private bool isMobileDevice;

	private Version jscriptVersion;

	private int maximumHrefLength;

	private int maximumRenderedPageSize;

	private int maximumSoftkeyLabelLength;

	private string minorVersionString;

	private string mobileDeviceManufacturer;

	private string mobileDeviceModel;

	private int numberOfSoftkeys;

	private string preferredImageMime;

	private string preferredRenderingMime;

	private string preferredRenderingType;

	private string preferredRequestEncoding;

	private string preferredResponseEncoding;

	private bool rendersBreakBeforeWmlSelectAndInput;

	private bool rendersBreaksAfterHtmlLists;

	private bool rendersBreaksAfterWmlAnchor;

	private bool rendersBreaksAfterWmlInput;

	private bool rendersWmlDoAcceptsInline;

	private bool rendersWmlSelectsAsMenuCards;

	private string requiredMetaTagNameValue;

	private bool requiresAttributeColonSubstitution;

	private bool requiresContentTypeMetaTag;

	private bool requiresControlStateInSession;

	private bool requiresDBCSCharacter;

	private bool requiresHtmlAdaptiveErrorReporting;

	private bool requiresLeadingPageBreak;

	private bool requiresNoBreakInFormatting;

	private bool requiresOutputOptimization;

	private bool requiresPhoneNumbersAsPlainText;

	private bool requiresSpecialViewStateEncoding;

	private bool requiresUniqueFilePathSuffix;

	private bool requiresUniqueHtmlCheckboxNames;

	private bool requiresUniqueHtmlInputNames;

	private bool requiresUrlEncodedPostfieldValues;

	private int screenBitDepth;

	private int screenCharactersHeight;

	private int screenCharactersWidth;

	private int screenPixelsHeight;

	private int screenPixelsWidth;

	private bool supportsAccesskeyAttribute;

	private bool supportsBodyColor;

	private bool supportsBold;

	private bool supportsCacheControlMetaTag;

	private bool supportsCallback;

	private bool supportsCss;

	private bool supportsDivAlign;

	private bool supportsDivNoWrap;

	private bool supportsEmptyStringInCookieValue;

	private bool supportsFontColor;

	private bool supportsFontName;

	private bool supportsFontSize;

	private bool supportsImageSubmit;

	private bool supportsIModeSymbols;

	private bool supportsInputIStyle;

	private bool supportsInputMode;

	private bool supportsItalic;

	private bool supportsJPhoneMultiMediaAttributes;

	private bool supportsJPhoneSymbols;

	private bool supportsQueryStringInFormAction;

	private bool supportsRedirectWithCookie;

	private bool supportsSelectMultiple;

	private bool supportsUncheck;

	private bool supportsXmlHttp;

	private bool useOptimizedCacheKey;

	private static HttpCapabilitiesProvider _provider = new HttpCapabilitiesDefaultProvider();

	private const int HaveActiveXControls = 1;

	private const int HaveAdapters = 2;

	private const int HaveAOL = 3;

	private const int HaveBackGroundSounds = 4;

	private const int HaveBeta = 5;

	private const int HaveBrowser = 6;

	private const int HaveBrowsers = 7;

	private const int HaveCanCombineFormsInDeck = 8;

	private const int HaveCanInitiateVoiceCall = 9;

	private const int HaveCanRenderAfterInputOrSelectElement = 10;

	private const int HaveCanRenderEmptySelects = 11;

	private const int HaveCanRenderInputAndSelectElementsTogether = 12;

	private const int HaveCanRenderMixedSelects = 13;

	private const int HaveCanRenderOneventAndPrevElementsTogether = 14;

	private const int HaveCanRenderPostBackCards = 15;

	private const int HaveCanRenderSetvarZeroWithMultiSelectionList = 16;

	private const int HaveCanSendMail = 17;

	private const int HaveCDF = 18;

	private const int HaveCookies = 20;

	private const int HaveCrawler = 21;

	private const int HaveDefaultSubmitButtonLimit = 22;

	private const int HaveEcmaScriptVersion = 23;

	private const int HaveFrames = 24;

	private const int HaveGatewayMajorVersion = 25;

	private const int HaveGatewayMinorVersion = 26;

	private const int HaveGatewayVersion = 27;

	private const int HaveHasBackButton = 28;

	private const int HaveHidesRightAlignedMultiselectScrollbars = 29;

	private const int HaveHtmlTextWriter = 30;

	private const int HaveId = 31;

	private const int HaveInputType = 32;

	private const int HaveIsColor = 33;

	private const int HaveIsMobileDevice = 34;

	private const int HaveJavaApplets = 35;

	private const int HaveJavaScript = 36;

	private const int HaveJScriptVersion = 37;

	private const int HaveMajorVersion = 38;

	private const int HaveMaximumHrefLength = 39;

	private const int HaveMaximumRenderedPageSize = 40;

	private const int HaveMaximumSoftkeyLabelLength = 41;

	private const int HaveMinorVersion = 42;

	private const int HaveMinorVersionString = 43;

	private const int HaveMobileDeviceManufacturer = 44;

	private const int HaveMobileDeviceModel = 45;

	private const int HaveMSDomVersion = 46;

	private const int HaveNumberOfSoftkeys = 47;

	private const int HavePlatform = 48;

	private const int HavePreferredImageMime = 49;

	private const int HavePreferredRenderingMime = 50;

	private const int HavePreferredRenderingType = 51;

	private const int HavePreferredRequestEncoding = 52;

	private const int HavePreferredResponseEncoding = 53;

	private const int HaveRendersBreakBeforeWmlSelectAndInput = 54;

	private const int HaveRendersBreaksAfterHtmlLists = 55;

	private const int HaveRendersBreaksAfterWmlAnchor = 56;

	private const int HaveRendersBreaksAfterWmlInput = 57;

	private const int HaveRendersWmlDoAcceptsInline = 58;

	private const int HaveRendersWmlSelectsAsMenuCards = 59;

	private const int HaveRequiredMetaTagNameValue = 60;

	private const int HaveRequiresAttributeColonSubstitution = 61;

	private const int HaveRequiresContentTypeMetaTag = 62;

	private const int HaveRequiresControlStateInSession = 63;

	private const int HaveRequiresDBCSCharacter = 64;

	private const int HaveRequiresHtmlAdaptiveErrorReporting = 65;

	private const int HaveRequiresLeadingPageBreak = 66;

	private const int HaveRequiresNoBreakInFormatting = 67;

	private const int HaveRequiresOutputOptimization = 68;

	private const int HaveRequiresPhoneNumbersAsPlainText = 69;

	private const int HaveRequiresSpecialViewStateEncoding = 70;

	private const int HaveRequiresUniqueFilePathSuffix = 71;

	private const int HaveRequiresUniqueHtmlCheckboxNames = 72;

	private const int HaveRequiresUniqueHtmlInputNames = 73;

	private const int HaveRequiresUrlEncodedPostfieldValues = 74;

	private const int HaveScreenBitDepth = 75;

	private const int HaveScreenCharactersHeight = 76;

	private const int HaveScreenCharactersWidth = 77;

	private const int HaveScreenPixelsHeight = 78;

	private const int HaveScreenPixelsWidth = 79;

	private const int HaveSupportsAccesskeyAttribute = 80;

	private const int HaveSupportsBodyColor = 81;

	private const int HaveSupportsBold = 82;

	private const int HaveSupportsCacheControlMetaTag = 83;

	private const int HaveSupportsCallback = 84;

	private const int HaveSupportsCss = 85;

	private const int HaveSupportsDivAlign = 86;

	private const int HaveSupportsDivNoWrap = 87;

	private const int HaveSupportsEmptyStringInCookieValue = 88;

	private const int HaveSupportsFontColor = 89;

	private const int HaveSupportsFontName = 90;

	private const int HaveSupportsFontSize = 91;

	private const int HaveSupportsImageSubmit = 92;

	private const int HaveSupportsIModeSymbols = 93;

	private const int HaveSupportsInputIStyle = 94;

	private const int HaveSupportsInputMode = 95;

	private const int HaveSupportsItalic = 96;

	private const int HaveSupportsJPhoneMultiMediaAttributes = 97;

	private const int HaveSupportsJPhoneSymbols = 98;

	private const int HaveSupportsQueryStringInFormAction = 99;

	private const int HaveSupportsRedirectWithCookie = 100;

	private const int HaveSupportsSelectMultiple = 101;

	private const int HaveSupportsUncheck = 102;

	private const int HaveSupportsXmlHttp = 103;

	private const int HaveTables = 104;

	private const int HaveTagWriter = 105;

	private const int HaveType = 106;

	private const int HaveUseOptimizedCacheKey = 107;

	private const int HaveVBScript = 108;

	private const int HaveVersion = 109;

	private const int HaveW3CDomVersion = 110;

	private const int HaveWin16 = 111;

	private const int HaveWin32 = 112;

	private const int LastHaveFlag = 113;

	private BitArray flags = new BitArray(113);

	private bool activeXControls;

	private bool aol;

	private bool backgroundSounds;

	private bool beta;

	private string browser;

	private bool cdf;

	private Version clrVersion;

	private bool cookies;

	private bool crawler;

	private Version ecmaScriptVersion;

	private bool frames;

	private bool javaApplets;

	private bool javaScript;

	private int majorVersion;

	private double minorVersion;

	private Version msDomVersion;

	private string platform;

	private bool tables;

	private Type tagWriter;

	private bool vbscript;

	private string version;

	private Version w3CDomVersion;

	private bool win16;

	private bool win32;

	private Version[] clrVersions;

	internal string useragent;

	private ArrayList browsers;

	/// <summary>Gets the value of the specified browser capability. In C#, this property is the indexer for the class.</summary>
	/// <param name="key">The name of the browser capability to retrieve.</param>
	/// <returns>The browser capability with the specified key name.</returns>
	public virtual string this[string key] => capabilities[key] as string;

	/// <summary>Returns the collection of available control adapters.</summary>
	/// <returns>The collection of registered control adapters.</returns>
	public IDictionary Adapters
	{
		get
		{
			if (!Get(2))
			{
				adapters = GetAdapters();
				Set(2);
			}
			return adapters;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports decks that contain multiple forms, such as separate cards.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports decks that contain multiple forms; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanCombineFormsInDeck
	{
		get
		{
			if (!Get(8))
			{
				canCombineFormsInDeck = ReadBoolean("cancombineformsindeck");
				Set(8);
			}
			return canCombineFormsInDeck;
		}
	}

	/// <summary>Gets a value indicating whether the browser device is capable of initiating a voice call.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser device is capable of initiating a voice call; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool CanInitiateVoiceCall
	{
		get
		{
			if (!Get(9))
			{
				canInitiateVoiceCall = ReadBoolean("caninitiatevoicecall");
				Set(9);
			}
			return canInitiateVoiceCall;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports page content following WML <see langword="&lt;select&gt;" /> or <see langword="&lt;input&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports page content following HTML <see langword="&lt;select&gt; " />or <see langword="&lt;input&gt; " />elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanRenderAfterInputOrSelectElement
	{
		get
		{
			if (!Get(10))
			{
				canRenderAfterInputOrSelectElement = ReadBoolean("canrenderafterinputorselectelement");
				Set(10);
			}
			return canRenderAfterInputOrSelectElement;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports empty HTML <see langword="&lt;select&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports empty HTML <see langword="&lt;select&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanRenderEmptySelects
	{
		get
		{
			if (!Get(11))
			{
				canRenderEmptySelects = ReadBoolean("canrenderemptyselects");
				Set(11);
			}
			return canRenderEmptySelects;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports WML <see langword="INPUT" /> and <see langword="SELECT" /> elements together on the same card.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="&lt;input&gt;" /> and <see langword="&lt;select&gt;" /> elements together; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool CanRenderInputAndSelectElementsTogether
	{
		get
		{
			if (!Get(12))
			{
				canRenderInputAndSelectElementsTogether = ReadBoolean("canrenderinputandselectelementstogether");
				Set(12);
			}
			return canRenderInputAndSelectElementsTogether;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports WML <see langword="&lt;option&gt;" /> elements that specify both <see langword="onpick" /> and <see langword="value" /> attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="&lt;option&gt;" /> elements that specify both <see langword="onpick" /> and <see langword="value" /> attributes; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanRenderMixedSelects
	{
		get
		{
			if (!Get(13))
			{
				canRenderMixedSelects = ReadBoolean("canrendermixedselects");
				Set(13);
			}
			return canRenderMixedSelects;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports WML <see langword="&lt;onevent&gt;" /> and <see langword="&lt;prev&gt;" /> elements that coexist within the same WML card.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="&lt;onevent&gt;" /> and <see langword="&lt;prev&gt;" /> elements that coexist within the same WML card; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanRenderOneventAndPrevElementsTogether
	{
		get
		{
			if (!Get(14))
			{
				canRenderOneventAndPrevElementsTogether = ReadBoolean("canrenderoneventandprevelementstogether");
				Set(14);
			}
			return canRenderOneventAndPrevElementsTogether;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports WML cards for postback.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML cards for postback; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanRenderPostBackCards
	{
		get
		{
			if (!Get(15))
			{
				canRenderPostBackCards = ReadBoolean("canrenderpostbackcards");
				Set(15);
			}
			return canRenderPostBackCards;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports WML <see langword="&lt;setvar&gt;" /> elements with a <see langword="value" /> attribute of 0.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="&lt;setvar&gt;" /> elements with a <see langword="value" /> attribute of <see langword="0" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanRenderSetvarZeroWithMultiSelectionList
	{
		get
		{
			if (!Get(16))
			{
				canRenderSetvarZeroWithMultiSelectionList = ReadBoolean("canrendersetvarzerowithmultiselectionlist");
				Set(16);
			}
			return canRenderSetvarZeroWithMultiSelectionList;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports sending e-mail by using the HTML <see langword="&lt;mailto&gt;" /> element for displaying electronic addresses.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports sending e-mail by using the HTML <see langword="&lt;mailto&gt;" /> element for displaying electronic addresses; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool CanSendMail
	{
		get
		{
			if (!Get(17))
			{
				canSendMail = ReadBoolean("cansendmail");
				Set(17);
			}
			return canSendMail;
		}
	}

	/// <summary>Used internally to get the defined capabilities of the browser.</summary>
	/// <returns>The defined capabilities of the browser.</returns>
	public IDictionary Capabilities
	{
		get
		{
			return capabilities;
		}
		set
		{
			capabilities = new Hashtable(value.Keys.Count, StringComparer.OrdinalIgnoreCase);
			foreach (object key in value.Keys)
			{
				if (!capabilities.Contains(key))
				{
					capabilities.Add(key, value[key]);
				}
			}
		}
	}

	/// <summary>Returns the maximum number of Submit buttons that are allowed for a form.</summary>
	/// <returns>The maximum number of Submit buttons that are allowed for a form.</returns>
	public virtual int DefaultSubmitButtonLimit
	{
		get
		{
			if (!Get(22))
			{
				defaultSubmitButtonLimit = ReadInt32("defaultsubmitbuttonlimit");
				Set(22);
			}
			return defaultSubmitButtonLimit;
		}
	}

	/// <summary>Gets the major version number of the wireless gateway used to access the server, if known. </summary>
	/// <returns>The major version number of the wireless gateway used to access the server, if known. The default is <see langword="0" />.</returns>
	/// <exception cref="T:System.Web.HttpUnhandledException">The major version number of the wireless gateway cannot be parsed.</exception>
	public virtual int GatewayMajorVersion
	{
		get
		{
			if (!Get(25))
			{
				gatewayMajorVersion = ReadInt32("gatewaymajorversion");
				Set(25);
			}
			return gatewayMajorVersion;
		}
	}

	/// <summary>Gets the minor version number of the wireless gateway used to access the server, if known. </summary>
	/// <returns>The minor version number of the wireless gateway used to access the server, if known. The default is <see langword="0" />.</returns>
	/// <exception cref="T:System.Web.HttpUnhandledException">The minor version number of the wireless gateway cannot be parsed.</exception>
	public virtual double GatewayMinorVersion
	{
		get
		{
			if (!Get(26))
			{
				gatewayMinorVersion = ReadDouble("gatewayminorversion");
				Set(26);
			}
			return gatewayMinorVersion;
		}
	}

	/// <summary>Gets the version of the wireless gateway used to access the server, if known.</summary>
	/// <returns>The version number of the wireless gateway used to access the server, if known. The default is <see langword="None" />.</returns>
	public virtual string GatewayVersion
	{
		get
		{
			if (!Get(27))
			{
				gatewayVersion = ReadString("gatewayversion");
				Set(27);
			}
			return gatewayVersion;
		}
	}

	/// <summary>Gets a value indicating whether the browser has a dedicated Back button.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser has a dedicated Back button; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool HasBackButton
	{
		get
		{
			if (!Get(28))
			{
				hasBackButton = ReadBoolean("hasbackbutton");
				Set(28);
			}
			return hasBackButton;
		}
	}

	/// <summary>Gets a value indicating whether the scrollbar of an HTML <see langword="&lt;select multiple&gt;" /> element with an <see langword="align" /> attribute value of <see langword="right" /> is obscured upon rendering.</summary>
	/// <returns>
	///     <see langword="true" /> if the scrollbar of an HTML <see langword="&lt;select multiple&gt;" /> element with an <see langword="align" /> attribute value of <see langword="right" /> is obscured upon rendering; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool HidesRightAlignedMultiselectScrollbars
	{
		get
		{
			if (!Get(29))
			{
				hidesRightAlignedMultiselectScrollbars = ReadBoolean("hidesrightalignedmultiselectscrollbars");
				Set(29);
			}
			return hidesRightAlignedMultiselectScrollbars;
		}
	}

	/// <summary>Gets or sets the fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use.</summary>
	/// <returns>The fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use.</returns>
	public string HtmlTextWriter
	{
		get
		{
			if (!Get(30))
			{
				htmlTextWriter = ReadString("htmlTextWriter");
				Set(30);
			}
			return htmlTextWriter;
		}
		set
		{
			Set(30);
			htmlTextWriter = value;
		}
	}

	/// <summary>Gets the internal identifier of the browser as specified in the browser definition file.</summary>
	/// <returns>Internal identifier of the browser as specified in the browser definition file.</returns>
	public string Id => Browser;

	/// <summary>Returns the type of input supported by browser.</summary>
	/// <returns>The type of input supported by browser. The default is telephoneKeypad.</returns>
	public virtual string InputType
	{
		get
		{
			if (!Get(32))
			{
				inputType = ReadString("inputtype");
				Set(32);
			}
			return inputType;
		}
	}

	/// <summary>Gets a value indicating whether the browser has a color display.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser has a color display; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool IsColor
	{
		get
		{
			if (!Get(33))
			{
				isColor = ReadBoolean("iscolor");
				Set(33);
			}
			return isColor;
		}
	}

	/// <summary>Gets a value indicating whether the browser is a recognized mobile device.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a recognized mobile device; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool IsMobileDevice
	{
		get
		{
			if (!Get(34))
			{
				isMobileDevice = ReadBoolean("ismobiledevice");
				Set(34);
			}
			return isMobileDevice;
		}
	}

	/// <summary>Gets the JScript version that the browser supports.</summary>
	/// <returns>The <see cref="T:System.Version" /> of JScript that the browser supports.</returns>
	public Version JScriptVersion
	{
		get
		{
			if (!Get(37))
			{
				jscriptVersion = ReadVersion("jscriptversion");
				Set(37);
			}
			return jscriptVersion;
		}
	}

	/// <summary>Gets the maximum length in characters for the <see langword="href" /> attribute of an HTML <see langword="&lt;a&gt;" /> (anchor) element.</summary>
	/// <returns>The maximum length in characters for the <see langword="href" /> attribute of an HTML <see langword="&lt;a&gt;" /> (anchor) element.</returns>
	public virtual int MaximumHrefLength
	{
		get
		{
			if (!Get(39))
			{
				maximumHrefLength = ReadInt32("maximumhreflength");
				Set(39);
			}
			return maximumHrefLength;
		}
	}

	/// <summary>Gets the maximum length of the page, in bytes, which the browser can display. </summary>
	/// <returns>The maximum length of the page, in bytes, which the browser can display. The default is 2000.</returns>
	public virtual int MaximumRenderedPageSize
	{
		get
		{
			if (!Get(40))
			{
				maximumRenderedPageSize = ReadInt32("maximumrenderedpagesize");
				Set(40);
			}
			return maximumRenderedPageSize;
		}
	}

	/// <summary>Returns the maximum length of the text that a soft-key label can display.</summary>
	/// <returns>The maximum length of the text that a soft-key label can display. The default is <see langword="5" />.</returns>
	public virtual int MaximumSoftkeyLabelLength
	{
		get
		{
			if (!Get(41))
			{
				maximumSoftkeyLabelLength = ReadInt32("maximumsoftkeylabellength");
				Set(41);
			}
			return maximumSoftkeyLabelLength;
		}
	}

	/// <summary>Gets the minor (decimal) version number of the browser as a string.</summary>
	/// <returns>The minor version number of the browser.</returns>
	public string MinorVersionString
	{
		get
		{
			if (!Get(43))
			{
				minorVersionString = ReadString("minorversionstring");
				Set(43);
			}
			return minorVersionString;
		}
	}

	/// <summary>Returns the name of the manufacturer of a mobile device, if known.</summary>
	/// <returns>The name of the manufacturer of a mobile device, if known. The default is <see langword="Unknown" />.</returns>
	public virtual string MobileDeviceManufacturer
	{
		get
		{
			if (!Get(44))
			{
				mobileDeviceManufacturer = ReadString("mobiledevicemanufacturer");
				Set(44);
			}
			return mobileDeviceManufacturer;
		}
	}

	/// <summary>Gets the model name of a mobile device, if known.</summary>
	/// <returns>The model name of a mobile device, if known. The default is <see langword="Unknown" />.</returns>
	public virtual string MobileDeviceModel
	{
		get
		{
			if (!Get(45))
			{
				mobileDeviceModel = ReadString("mobiledevicemodel");
				Set(45);
			}
			return mobileDeviceModel;
		}
	}

	/// <summary>Returns the number of soft keys on a mobile device.</summary>
	/// <returns>The number of soft keys supported on a mobile device. The default is <see langword="0" />.</returns>
	public virtual int NumberOfSoftkeys
	{
		get
		{
			if (!Get(47))
			{
				numberOfSoftkeys = ReadInt32("numberofsoftkeys");
				Set(47);
			}
			return numberOfSoftkeys;
		}
	}

	/// <summary>Returns the MIME type of the type of image content typically preferred by the browser.</summary>
	/// <returns>The MIME type of the type of image content typically preferred by the browser. The default is <see langword="image/gif" />.</returns>
	public virtual string PreferredImageMime
	{
		get
		{
			if (!Get(49))
			{
				preferredImageMime = ReadString("preferredimagemime");
				Set(49);
			}
			return preferredImageMime;
		}
	}

	/// <summary>Returns the MIME type of the type of content typically preferred by the browser.</summary>
	/// <returns>The MIME type of the type of content typically preferred by the browser. The default is <see langword="text/html" />.</returns>
	public virtual string PreferredRenderingMime
	{
		get
		{
			if (!Get(50))
			{
				preferredRenderingMime = ReadString("preferredrenderingmime");
				Set(50);
			}
			return preferredRenderingMime;
		}
	}

	/// <summary>Gets the general name for the type of content that the browser prefers.</summary>
	/// <returns>
	///     <see langword="html32" /> or <see langword="chtml10" />. The default is <see langword="html32" />.</returns>
	public virtual string PreferredRenderingType
	{
		get
		{
			if (!Get(51))
			{
				preferredRenderingType = ReadString("preferredrenderingtype");
				Set(51);
			}
			return preferredRenderingType;
		}
	}

	/// <summary>Gets the request encoding preferred by the browser.</summary>
	/// <returns>The request encoding preferred by the browser.</returns>
	public virtual string PreferredRequestEncoding
	{
		get
		{
			if (!Get(52))
			{
				preferredRequestEncoding = ReadString("preferredrequestencoding");
				Set(52);
			}
			return preferredRequestEncoding;
		}
	}

	/// <summary>Gets the response encoding preferred by the browser.</summary>
	/// <returns>The response encoding preferred by the browser.</returns>
	public virtual string PreferredResponseEncoding
	{
		get
		{
			if (!Get(53))
			{
				preferredResponseEncoding = ReadString("preferredresponseencoding");
				Set(53);
			}
			return preferredResponseEncoding;
		}
	}

	/// <summary>Gets a value indicating whether the browser renders a line break before <see langword="&lt;select&gt;" /> or <see langword="&lt;input&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break before <see langword="&lt;select&gt;" /> or <see langword="&lt;input&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RendersBreakBeforeWmlSelectAndInput
	{
		get
		{
			if (!Get(54))
			{
				rendersBreakBeforeWmlSelectAndInput = ReadBoolean("rendersbreakbeforewmlselectandinput");
				Set(54);
			}
			return rendersBreakBeforeWmlSelectAndInput;
		}
	}

	/// <summary>Gets a value indicating whether the browser renders a line break after list-item elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after list-item elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool RendersBreaksAfterHtmlLists
	{
		get
		{
			if (!Get(55))
			{
				rendersBreaksAfterHtmlLists = ReadBoolean("rendersbreaksafterhtmllists");
				Set(55);
			}
			return rendersBreaksAfterHtmlLists;
		}
	}

	/// <summary>Gets a value indicating whether the browser renders a line break after a stand-alone HTML <see langword="&lt;a&gt;" /> (anchor) element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after a stand-alone HTML <see langword="&lt;a&gt;" /> (anchor) element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RendersBreaksAfterWmlAnchor
	{
		get
		{
			if (!Get(56))
			{
				rendersBreaksAfterWmlAnchor = ReadBoolean("rendersbreaksafterwmlanchor");
				Set(56);
			}
			return rendersBreaksAfterWmlAnchor;
		}
	}

	/// <summary>Gets a value indicating whether the browser renders a line break after an HTML <see langword="&lt;input&gt;" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after an HTML <see langword="&lt;input&gt; " />element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RendersBreaksAfterWmlInput
	{
		get
		{
			if (!Get(57))
			{
				rendersBreaksAfterWmlInput = ReadBoolean("rendersbreaksafterwmlinput");
				Set(57);
			}
			return rendersBreaksAfterWmlInput;
		}
	}

	/// <summary>Gets a value indicating whether the mobile-device browser renders a WML <see langword="do" />-based form accept construct as an inline button rather than as a soft key.</summary>
	/// <returns>
	///     <see langword="true" /> if the mobile-device browser renders a WML <see langword="do" />-based form-accept construct as an inline button; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool RendersWmlDoAcceptsInline
	{
		get
		{
			if (!Get(58))
			{
				rendersWmlDoAcceptsInline = ReadBoolean("renderswmldoacceptsinline");
				Set(58);
			}
			return rendersWmlDoAcceptsInline;
		}
	}

	/// <summary>Gets a value indicating whether the browser renders WML <see langword="&lt;select&gt;" /> elements as menu cards, rather than as a combo box.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders WML <see langword="&lt;select&gt;" /> elements as menu cards; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RendersWmlSelectsAsMenuCards
	{
		get
		{
			if (!Get(59))
			{
				rendersWmlSelectsAsMenuCards = ReadBoolean("renderswmlselectsasmenucards");
				Set(59);
			}
			return rendersWmlSelectsAsMenuCards;
		}
	}

	/// <summary>Used internally to produce a meta-tag required by some browsers.</summary>
	/// <returns>A meta-tag required by some browsers.</returns>
	public virtual string RequiredMetaTagNameValue
	{
		get
		{
			if (!Get(60))
			{
				requiredMetaTagNameValue = ReadString("requiredmetatagnamevalue");
				Set(60);
			}
			return requiredMetaTagNameValue;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires colons in element attribute values to be substituted with a different character.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires colons in element attribute values to be substituted with a different character; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresAttributeColonSubstitution
	{
		get
		{
			if (!Get(61))
			{
				requiresAttributeColonSubstitution = ReadBoolean("requiresattributecolonsubstitution");
				Set(61);
			}
			return requiresAttributeColonSubstitution;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires an HTML <see langword="&lt;meta&gt;" /> element for which the <see langword="content-type" /> attribute is specified.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires an HTML <see langword="&lt;meta&gt;" /> element for which the <see langword="content-type" /> attribute is specified; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresContentTypeMetaTag
	{
		get
		{
			if (!Get(62))
			{
				requiresContentTypeMetaTag = ReadBoolean("requiresContentTypeMetaTag");
				Set(62);
			}
			return requiresContentTypeMetaTag;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires control state to be maintained in sessions.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires control state to be maintained in sessions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool RequiresControlStateInSession
	{
		get
		{
			if (!Get(63))
			{
				requiresControlStateInSession = ReadBoolean("requiresControlStateInSession");
				Set(63);
			}
			return requiresControlStateInSession;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires a double-byte character set.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires a double-byte character set; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresDBCSCharacter
	{
		get
		{
			if (!Get(64))
			{
				requiresDBCSCharacter = ReadBoolean("requiresdbcscharacter");
				Set(64);
			}
			return requiresDBCSCharacter;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires nonstandard error messages.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires nonstandard error messages; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresHtmlAdaptiveErrorReporting
	{
		get
		{
			if (!Get(65))
			{
				requiresHtmlAdaptiveErrorReporting = ReadBoolean("requireshtmladaptiveerrorreporting");
				Set(65);
			}
			return requiresHtmlAdaptiveErrorReporting;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires the first element in the body of a Web page to be an HTML <see langword="&lt;br&gt;" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires the first element in the body of a Web page to be an HTML <see langword="BR" /> element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresLeadingPageBreak
	{
		get
		{
			if (!Get(66))
			{
				requiresLeadingPageBreak = ReadBoolean("requiresleadingpagebreak");
				Set(66);
			}
			return requiresLeadingPageBreak;
		}
	}

	/// <summary>Gets a value indicating whether the browser does not support HTML <see langword="&lt;br&gt;" /> elements to format line breaks.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser does not support HTML <see langword="&lt;br&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresNoBreakInFormatting
	{
		get
		{
			if (!Get(67))
			{
				requiresNoBreakInFormatting = ReadBoolean("requiresnobreakinformatting");
				Set(67);
			}
			return requiresNoBreakInFormatting;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires pages to contain a size-optimized form of markup language tags.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires pages to contain a size-optimized form of markup language tags; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresOutputOptimization
	{
		get
		{
			if (!Get(68))
			{
				requiresOutputOptimization = ReadBoolean("requiresoutputoptimization");
				Set(68);
			}
			return requiresOutputOptimization;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports phone dialing based on plain text, or whether it requires special markup.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports phone dialing based on plain text only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresPhoneNumbersAsPlainText
	{
		get
		{
			if (!Get(69))
			{
				requiresPhoneNumbersAsPlainText = ReadBoolean("requiresphonenumbersasplaintext");
				Set(69);
			}
			return requiresPhoneNumbersAsPlainText;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires <see langword="VIEWSTATE" /> values to be specially encoded.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires <see langword="VIEWSTATE" /> values to be specially encoded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresSpecialViewStateEncoding
	{
		get
		{
			if (!Get(70))
			{
				requiresSpecialViewStateEncoding = ReadBoolean("requiresspecialviewstateencoding");
				Set(70);
			}
			return requiresSpecialViewStateEncoding;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires unique form-action URLs.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique form-action URLs; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresUniqueFilePathSuffix
	{
		get
		{
			if (!Get(71))
			{
				requiresUniqueFilePathSuffix = ReadBoolean("requiresuniquefilepathsuffix");
				Set(71);
			}
			return requiresUniqueFilePathSuffix;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires unique <see langword="name" /> attribute values of multiple HTML <see langword="&lt;input type=&quot;checkbox&quot;&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique <see langword="name" /> attribute values of multiple HTML <see langword="&lt;input type=&quot;checkbox&quot;&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresUniqueHtmlCheckboxNames
	{
		get
		{
			if (!Get(72))
			{
				requiresUniqueHtmlCheckboxNames = ReadBoolean("requiresuniquehtmlcheckboxnames");
				Set(72);
			}
			return requiresUniqueHtmlCheckboxNames;
		}
	}

	/// <summary>Gets a value indicating whether the browser requires unique <see langword="name" /> attribute values of multiple HTML <see langword="&lt;input&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique <see langword="name" /> attribute values of multiple HTML <see langword="&lt;input&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresUniqueHtmlInputNames
	{
		get
		{
			if (!Get(73))
			{
				requiresUniqueHtmlInputNames = ReadBoolean("requiresuniquehtmlinputnames");
				Set(73);
			}
			return requiresUniqueHtmlInputNames;
		}
	}

	/// <summary>Gets a value indicating whether postback data sent by the browser will be <see langword="UrlEncoded" />.</summary>
	/// <returns>
	///     <see langword="true" /> if postback data sent by the browser will be <see langword="UrlEncoded" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool RequiresUrlEncodedPostfieldValues
	{
		get
		{
			if (!Get(74))
			{
				requiresUrlEncodedPostfieldValues = ReadBoolean("requiresurlencodedpostfieldvalues");
				Set(74);
			}
			return requiresUrlEncodedPostfieldValues;
		}
	}

	/// <summary>Returns the depth of the display, in bits per pixel.</summary>
	/// <returns>The depth of the display, in bits per pixel. The default is <see langword="1" />.</returns>
	public virtual int ScreenBitDepth
	{
		get
		{
			if (!Get(75))
			{
				screenBitDepth = ReadInt32("screenbitdepth");
				Set(75);
			}
			return screenBitDepth;
		}
	}

	/// <summary>Returns the approximate height of the display, in character lines.</summary>
	/// <returns>The approximate height of the display, in character lines. The default is <see langword="6" />.</returns>
	public virtual int ScreenCharactersHeight
	{
		get
		{
			if (!Get(76))
			{
				screenCharactersHeight = ReadInt32("screencharactersheight");
				Set(76);
			}
			return screenCharactersHeight;
		}
	}

	/// <summary>Returns the approximate width of the display, in characters.</summary>
	/// <returns>The approximate width of the display, in characters. The default is <see langword="12" />.</returns>
	public virtual int ScreenCharactersWidth
	{
		get
		{
			if (!Get(77))
			{
				screenCharactersWidth = ReadInt32("screencharacterswidth");
				Set(77);
			}
			return screenCharactersWidth;
		}
	}

	/// <summary>Returns the approximate height of the display, in pixels.</summary>
	/// <returns>The approximate height of the display, in pixels. The default is <see langword="72" />.</returns>
	public virtual int ScreenPixelsHeight
	{
		get
		{
			if (!Get(78))
			{
				screenPixelsHeight = ReadInt32("screenpixelsheight");
				Set(78);
			}
			return screenPixelsHeight;
		}
	}

	/// <summary>Returns the approximate width of the display, in pixels.</summary>
	/// <returns>The approximate width of the display, in pixels. The default is <see langword="96" />.</returns>
	public virtual int ScreenPixelsWidth
	{
		get
		{
			if (!Get(79))
			{
				screenPixelsWidth = ReadInt32("screenpixelswidth");
				Set(79);
			}
			return screenPixelsWidth;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="ACCESSKEY" /> attribute of HTML <see langword="&lt;a&gt;" /> (anchor) and <see langword="&lt;input&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="accesskey" /> attribute of HTML <see langword="&lt;a&gt;" />  (anchor) and <see langword="&lt;input&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsAccesskeyAttribute
	{
		get
		{
			if (!Get(80))
			{
				supportsAccesskeyAttribute = ReadBoolean("supportsaccesskeyattribute");
				Set(80);
			}
			return supportsAccesskeyAttribute;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="bgcolor" /> attribute of the HTML <see langword="&lt;body&gt;" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="bgcolor" /> attribute of the HTML <see langword="&lt;body&gt;" /> element; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsBodyColor
	{
		get
		{
			if (!Get(81))
			{
				supportsBodyColor = ReadBoolean("supportsbodycolor");
				Set(81);
			}
			return supportsBodyColor;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports HTML <see langword="&lt;b&gt;" /> elements to format bold text.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="&lt;b&gt;" />  elements to format bold text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsBold
	{
		get
		{
			if (!Get(82))
			{
				supportsBold = ReadBoolean("supportsbold");
				Set(82);
			}
			return supportsBold;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="cache-control" /> value for the <see langword="http-equiv" /> attribute of HTML <see langword="&lt;meta&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="cache-control" /> value for the <see langword="http-equiv" /> attribute of HTML <see langword="&lt;meta&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsCacheControlMetaTag
	{
		get
		{
			if (!Get(83))
			{
				supportsCacheControlMetaTag = ReadBoolean("supportscachecontrolmetatag");
				Set(83);
			}
			return supportsCacheControlMetaTag;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports callback scripts.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports callback scripts; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsCallback
	{
		get
		{
			if (!Get(84))
			{
				supportsCallback = ReadBoolean("supportscallback");
				Set(84);
			}
			return supportsCallback;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports Cascading Style Sheets (CSS).</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports CSS; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsCss
	{
		get
		{
			if (!Get(85))
			{
				supportsCss = ReadBoolean("supportscss");
				Set(85);
			}
			return supportsCss;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="align" /> attribute of HTML <see langword="&lt;div&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="align" /> attribute of HTML <see langword="&lt;div&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsDivAlign
	{
		get
		{
			if (!Get(86))
			{
				supportsDivAlign = ReadBoolean("supportsdivalign");
				Set(86);
			}
			return supportsDivAlign;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="nowrap" /> attribute of HTML <see langword="&lt;div&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="nowrap" /> attribute of HTML <see langword="&lt;div&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsDivNoWrap
	{
		get
		{
			if (!Get(87))
			{
				supportsDivNoWrap = ReadBoolean("supportsdivnowrap");
				Set(64);
			}
			return supportsDivNoWrap;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports empty (<see langword="null" />) strings in cookie values.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports empty (<see langword="null" />) strings in cookie values; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsEmptyStringInCookieValue
	{
		get
		{
			if (!Get(88))
			{
				supportsEmptyStringInCookieValue = ReadBoolean("supportsemptystringincookievalue");
				Set(88);
			}
			return supportsEmptyStringInCookieValue;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="color" /> attribute of HTML <see langword="&lt;font&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="color" /> attribute of HTML <see langword="&lt;font&gt; " />elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsFontColor
	{
		get
		{
			if (!Get(89))
			{
				supportsFontColor = ReadBoolean("supportsfontcolor");
				Set(89);
			}
			return supportsFontColor;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="name" /> attribute of HTML <see langword="&lt;font&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="name" /> attribute of HTML <see langword="&lt;font&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsFontName
	{
		get
		{
			if (!Get(90))
			{
				supportsFontName = ReadBoolean("supportsfontname");
				Set(90);
			}
			return supportsFontName;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="size" /> attribute of HTML <see langword="&lt;font&gt; " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="size" /> attribute of HTML <see langword="&lt;font&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsFontSize
	{
		get
		{
			if (!Get(91))
			{
				supportsFontSize = ReadBoolean("supportsfontsize");
				Set(91);
			}
			return supportsFontSize;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports using a custom image in place of a standard form Submit button.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports using a custom image in place of a standard form Submit button; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsImageSubmit
	{
		get
		{
			if (!Get(92))
			{
				supportsImageSubmit = ReadBoolean("supportsimagesubmit");
				Set(92);
			}
			return supportsImageSubmit;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports i-mode symbols.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports i-mode symbols; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsIModeSymbols
	{
		get
		{
			if (!Get(93))
			{
				supportsIModeSymbols = ReadBoolean("supportsimodesymbols");
				Set(93);
			}
			return supportsIModeSymbols;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="istyle" /> attribute of HTML <see langword="&lt;input&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="istyle" /> attribute of HTML <see langword="&lt;input&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsInputIStyle
	{
		get
		{
			if (!Get(94))
			{
				supportsInputIStyle = ReadBoolean("supportsinputistyle");
				Set(94);
			}
			return supportsInputIStyle;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="mode" /> attribute of HTML <see langword="&lt;input&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="mode" /> attribute of HTML <see langword="&lt;input&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsInputMode
	{
		get
		{
			if (!Get(95))
			{
				supportsInputMode = ReadBoolean("supportsinputmode");
				Set(95);
			}
			return supportsInputMode;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports HTML <see langword="&lt;i&gt;" /> elements to format italic text.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="&lt;i&gt;" /> elements to format italic text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsItalic
	{
		get
		{
			if (!Get(96))
			{
				supportsItalic = ReadBoolean("supportsitalic");
				Set(96);
			}
			return supportsItalic;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports J-Phone multimedia attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports J-Phone multimedia attributes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsJPhoneMultiMediaAttributes
	{
		get
		{
			if (!Get(97))
			{
				supportsJPhoneMultiMediaAttributes = ReadBoolean("supportsjphonemultimediaattributes");
				Set(97);
			}
			return supportsJPhoneMultiMediaAttributes;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports J-Phone–specific picture symbols.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports J-Phone–specific picture symbols; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsJPhoneSymbols
	{
		get
		{
			if (!Get(98))
			{
				supportsJPhoneSymbols = ReadBoolean("supportsjphonesymbols");
				Set(98);
			}
			return supportsJPhoneSymbols;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports a query string in the <see langword="action" /> attribute value of HTML <see langword="&lt;form&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports a query string in the <see langword="action" /> attribute value of HTML <see langword="&lt;form&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsQueryStringInFormAction
	{
		get
		{
			if (!Get(99))
			{
				supportsQueryStringInFormAction = ReadBoolean("supportsquerystringinformaction");
				Set(99);
			}
			return supportsQueryStringInFormAction;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports cookies on redirection.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports cookies on redirection; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsRedirectWithCookie
	{
		get
		{
			if (!Get(100))
			{
				supportsRedirectWithCookie = ReadBoolean("supportsredirectwithcookie");
				Set(100);
			}
			return supportsRedirectWithCookie;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports the <see langword="multiple" /> attribute of HTML <see langword="&lt;select&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="multiple" /> attribute of HTML <see langword="&lt;select&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsSelectMultiple
	{
		get
		{
			if (!Get(101))
			{
				supportsSelectMultiple = ReadBoolean("supportsselectmultiple");
				Set(101);
			}
			return supportsSelectMultiple;
		}
	}

	/// <summary>Gets a value indicating whether the clearing of a checked HTML <see langword="&lt;input type=checkbox&gt;" /> element is reflected in postback data.</summary>
	/// <returns>
	///     <see langword="true" /> if the clearing of a checked HTML <see langword="&lt;input type=checkbox&gt;" /> element is reflected in postback data; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public virtual bool SupportsUncheck
	{
		get
		{
			if (!Get(102))
			{
				supportsUncheck = ReadBoolean("supportsuncheck");
				Set(102);
			}
			return supportsUncheck;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports receiving XML over HTTP.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports receiving XML over HTTP; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool SupportsXmlHttp
	{
		get
		{
			if (!Get(103))
			{
				supportsXmlHttp = ReadBoolean("supportsxmlhttp");
				Set(103);
			}
			return supportsXmlHttp;
		}
	}

	/// <summary>Used internally to get a value indicating whether to use an optimized cache key.</summary>
	/// <returns>
	///     <see langword="true" /> to use an optimized cache key; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool UseOptimizedCacheKey
	{
		get
		{
			if (!Get(107))
			{
				useOptimizedCacheKey = ReadBoolean("useoptimizedcachekey");
				Set(107);
			}
			return useOptimizedCacheKey;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpCapabilitiesProvider" /> object for the current browser.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HttpCapabilitiesProvider" /> object for the current browser.</returns>
	public static HttpCapabilitiesProvider BrowserCapabilitiesProvider
	{
		get
		{
			return _provider;
		}
		set
		{
			_provider = value;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports ActiveX controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports ActiveX controls; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool ActiveXControls
	{
		get
		{
			if (!Get(1))
			{
				activeXControls = ReadBoolean("activexcontrols");
				Set(1);
			}
			return activeXControls;
		}
	}

	/// <summary>Gets a value indicating whether the client is an America Online (AOL) browser.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is an AOL browser; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool AOL
	{
		get
		{
			if (!Get(3))
			{
				aol = ReadBoolean("aol");
				Set(3);
			}
			return aol;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports playing background sounds using the <see langword="&lt;bgsounds&gt;" /> HTML element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports playing background sounds; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool BackgroundSounds
	{
		get
		{
			if (!Get(4))
			{
				backgroundSounds = ReadBoolean("backgroundsounds");
				Set(4);
			}
			return backgroundSounds;
		}
	}

	/// <summary>Gets a value indicating whether the browser is a beta version.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a beta version; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Beta
	{
		get
		{
			if (!Get(5))
			{
				beta = ReadBoolean("beta");
				Set(5);
			}
			return beta;
		}
	}

	/// <summary>Gets the browser string (if any) that was sent by the browser in the <see langword="User-Agent" /> request header.</summary>
	/// <returns>The contents of the <see langword="User-Agent" /> request header sent by the browser.</returns>
	public string Browser
	{
		get
		{
			if (!Get(6))
			{
				browser = this["browser"];
				if (browser == null || browser.Length == 0)
				{
					browser = "Unknown";
				}
				Set(6);
			}
			return browser;
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.ArrayList" /> of the browsers in the <see cref="P:System.Web.Configuration.HttpCapabilitiesBase.Capabilities" /> dictionary.</summary>
	/// <returns>An <see cref="T:System.Collections.ArrayList" /> of the browsers in the <see cref="P:System.Web.Configuration.HttpCapabilitiesBase.Capabilities" /> dictionary.</returns>
	public ArrayList Browsers
	{
		get
		{
			if (!Get(7))
			{
				browsers = ReadArrayList("browsers");
				Set(7);
			}
			return browsers;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports Channel Definition Format (CDF) for webcasting.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports CDF; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool CDF
	{
		get
		{
			if (!Get(18))
			{
				cdf = ReadBoolean("cdf");
				Set(18);
			}
			return cdf;
		}
	}

	/// <summary>Gets the version of the .NET Framework that is installed on the client.</summary>
	/// <returns>The common language runtime <see cref="T:System.Version" />.</returns>
	public Version ClrVersion
	{
		get
		{
			if (clrVersion == null)
			{
				InternalGetClrVersions();
			}
			return clrVersion;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports cookies.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports cookies; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Cookies
	{
		get
		{
			if (!Get(20))
			{
				cookies = ReadBoolean("cookies");
				Set(20);
			}
			return cookies;
		}
	}

	/// <summary>Gets a value indicating whether the browser is a search engine Web crawler.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a search engine; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Crawler
	{
		get
		{
			if (!Get(21))
			{
				crawler = ReadBoolean("crawler");
				Set(21);
			}
			return crawler;
		}
	}

	/// <summary>Gets the version number of ECMAScript that the browser supports.</summary>
	/// <returns>The version number of ECMAScript that the browser supports.</returns>
	public Version EcmaScriptVersion
	{
		get
		{
			if (!Get(23))
			{
				ecmaScriptVersion = ReadVersion("ecmascriptversion");
				Set(23);
			}
			return ecmaScriptVersion;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports HTML frames.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports frames; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Frames
	{
		get
		{
			if (!Get(24))
			{
				frames = ReadBoolean("frames");
				Set(24);
			}
			return frames;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports Java.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports Java; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool JavaApplets
	{
		get
		{
			if (!Get(35))
			{
				javaApplets = ReadBoolean("javaapplets");
				Set(35);
			}
			return javaApplets;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports JavaScript.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports JavaScript; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Obsolete("The recommended alternative is the EcmaScriptVersion property. A Major version value greater than or equal to 1 implies JavaScript support. http://go.microsoft.com/fwlink/?linkid=14202")]
	public bool JavaScript
	{
		get
		{
			if (!Get(36))
			{
				javaScript = ReadBoolean("javascript");
				Set(36);
			}
			return javaScript;
		}
	}

	/// <summary>Gets the major (integer) version number of the browser.</summary>
	/// <returns>The major version number of the browser.</returns>
	/// <exception cref="T:System.Exception">The major version value is not an integer.</exception>
	public int MajorVersion
	{
		get
		{
			if (!Get(38))
			{
				if (this["majorversion"] != null)
				{
					majorVersion = ReadInt32("majorversion");
				}
				else
				{
					majorVersion = ReadInt32("majorver");
				}
				Set(38);
			}
			return majorVersion;
		}
	}

	/// <summary>Gets the minor (that is, decimal) version number of the browser.</summary>
	/// <returns>The minor version number of the browser.</returns>
	/// <exception cref="T:System.Web.HttpUnhandledException">The minor version number in the header is not valid.</exception>
	public double MinorVersion
	{
		get
		{
			if (!Get(42))
			{
				if (this["minorversion"] != null)
				{
					minorVersion = ReadDouble("minorversion");
				}
				else
				{
					minorVersion = ReadDouble("minorver");
				}
				Set(42);
			}
			return minorVersion;
		}
	}

	/// <summary>Gets the version of Microsoft HTML (MSHTML) Document Object Model (DOM) that the browser supports.</summary>
	/// <returns>The number of the MSHTML DOM version that the browser supports.</returns>
	public Version MSDomVersion
	{
		get
		{
			if (!Get(46))
			{
				msDomVersion = ReadVersion("msdomversion");
				Set(46);
			}
			return msDomVersion;
		}
	}

	/// <summary>Gets the name of the platform that the client uses, if it is known.</summary>
	/// <returns>The operating system that the client uses, if it is known, otherwise the value is set to <see langword="Unknown" />.</returns>
	public string Platform
	{
		get
		{
			if (!Get(48))
			{
				platform = ReadString("platform");
				Set(48);
			}
			return platform;
		}
	}

	/// <summary>Gets a value indicating whether the browser supports HTML <see langword="&lt;table&gt;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="&lt;table&gt;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Tables
	{
		get
		{
			if (!Get(104))
			{
				tables = ReadBoolean("tables");
				Set(104);
			}
			return tables;
		}
	}

	/// <summary>Used internally to get the type of the object that is used to write tags for the browser.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the object that is used to write tags for the browser.</returns>
	/// <exception cref="T:System.Web.HttpUnhandledException">There is a parse error.</exception>
	public Type TagWriter
	{
		get
		{
			if (!Get(105))
			{
				tagWriter = GetTagWriter();
				Set(105);
			}
			return tagWriter;
		}
	}

	/// <summary>Gets the name and major (integer) version number of the browser.</summary>
	/// <returns>The name and major version number of the browser.</returns>
	public string Type => Browser + MajorVersion;

	/// <summary>Gets a value indicating whether the browser supports Visual Basic Scripting edition (VBScript).</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports VBScript; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool VBScript
	{
		get
		{
			if (!Get(108))
			{
				vbscript = ReadBoolean("vbscript");
				Set(108);
			}
			return vbscript;
		}
	}

	/// <summary>Gets the full version number (integer and decimal) of the browser as a string.</summary>
	/// <returns>The full version number of the browser as a string.</returns>
	public string Version
	{
		get
		{
			if (!Get(109))
			{
				version = ReadString("version");
				Set(109);
			}
			return version;
		}
	}

	/// <summary>Gets the version of the World Wide Web Consortium (W3C) XML Document Object Model (DOM) that the browser supports.</summary>
	/// <returns>The number of the W3C XML DOM version number that the browser supports.</returns>
	public Version W3CDomVersion
	{
		get
		{
			if (!Get(110))
			{
				w3CDomVersion = ReadVersion("w3cdomversion");
				Set(110);
			}
			return w3CDomVersion;
		}
	}

	/// <summary>Gets a value indicating whether the client is a Win16-based computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is running on a Win16-based computer; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Win16
	{
		get
		{
			if (!Get(111))
			{
				win16 = ReadBoolean("win16");
				Set(111);
			}
			return win16;
		}
	}

	/// <summary>Gets a value indicating whether the client is a Win32-based computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is a Win32-based computer; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Win32
	{
		get
		{
			if (!Get(112))
			{
				string text = Platform;
				win32 = text != "Win16" && text.StartsWith("Win");
				Set(112);
			}
			return win32;
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> class.</summary>
	public HttpCapabilitiesBase()
	{
	}

	internal static string GetUserAgentForDetection(HttpRequest request)
	{
		string text = null;
		if (request.Context.CurrentHandler is Page)
		{
			text = ((Page)request.Context.CurrentHandler).ClientTarget;
		}
		if (string.IsNullOrEmpty(text))
		{
			text = request.ClientTarget;
			if (string.IsNullOrEmpty(text))
			{
				text = request.UserAgent;
			}
		}
		return text;
	}

	private static HttpBrowserCapabilities GetHttpBrowserCapabilitiesFromBrowscapini(string ua)
	{
		return new HttpBrowserCapabilities
		{
			capabilities = CapabilitiesLoader.GetCapabilities(ua)
		};
	}

	/// <summary>Used internally to return an instance of <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> representing the browser that generated the specified <see cref="T:System.Web.HttpRequest" />.</summary>
	/// <param name="configKey">The name of the configuration section that configures browser capabilities.</param>
	/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> generated by the browser for which to return capabilities and which is usually the current <see cref="T:System.Web.HttpRequest" />.</param>
	/// <returns>An instance of <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> representing the browser that generated the specified <see cref="T:System.Web.HttpRequest" />.</returns>
	public static HttpCapabilitiesBase GetConfigCapabilities(string configKey, HttpRequest request)
	{
		string userAgentForDetection = GetUserAgentForDetection(request);
		HttpBrowserCapabilities httpBrowserCapabilities = GetHttpBrowserCapabilitiesFromBrowscapini(userAgentForDetection);
		GetConfigCapabilities_called = true;
		if (HttpApplicationFactory.AppBrowsersFiles.Length != 0)
		{
			httpBrowserCapabilities = HttpApplicationFactory.CapabilitiesProcessor.Process(request, httpBrowserCapabilities.Capabilities);
		}
		httpBrowserCapabilities.useragent = userAgentForDetection;
		httpBrowserCapabilities.Init();
		return httpBrowserCapabilities;
	}

	/// <summary>Used internally to initialize an internal set of values.</summary>
	protected virtual void Init()
	{
	}

	/// <summary>Used internally to compare filters.</summary>
	/// <param name="filter1">The first filter to compare.</param>
	/// <param name="filter2">The second filter to compare.</param>
	/// <returns>1 if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1 if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; 0 if there is no parent-child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
	int IFilterResolutionService.CompareFilters(string filter1, string filter2)
	{
		throw new NotImplementedException();
	}

	/// <summary>Used internally to evaluate a filter.</summary>
	/// <param name="filterName">The filter to evaluate.</param>
	/// <returns>
	///     <see langword="true" /> if the filter was successfully evaluated; otherwise, <see langword="false" />.</returns>
	bool IFilterResolutionService.EvaluateFilter(string filterName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Used internally to add an entry to the internal collection of browsers for which capabilities are recognized.</summary>
	/// <param name="browserName">The name of the browser to add.</param>
	public void AddBrowser(string browserName)
	{
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to be used.</summary>
	/// <param name="w">
	///       <see cref="T:System.IO.TextWriter" /> to be created.</param>
	/// <returns>A new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to be used.</returns>
	/// <exception cref="T:System.Exception">The method failed to create an instance of <see cref="T:System.Web.UI.HtmlTextWriter" />.</exception>
	public HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
	{
		return (HtmlTextWriter)Activator.CreateInstance(TagWriter, w);
	}

	/// <summary>Used internally to disable use of an optimized cache key.</summary>
	public void DisableOptimizedCacheKey()
	{
		throw new NotImplementedException();
	}

	internal virtual IDictionary GetAdapters()
	{
		return new Hashtable();
	}

	/// <summary>Gets a value indicating whether the client browser is the same as the specified browser.</summary>
	/// <param name="browserName">The specified browser.</param>
	/// <returns>
	///     <see langword="true" /> if the client browser is the same as the specified browser; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool IsBrowser(string browserName)
	{
		foreach (string browser in Browsers)
		{
			if (string.Compare(browser, "Unknown", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(browserName, browser, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
		}
		return false;
	}

	internal virtual Type GetTagWriter()
	{
		return typeof(HtmlTextWriter);
	}

	/// <summary>Returns all versions of the .NET Framework common language runtime that are installed on the client.</summary>
	/// <returns>An array of <see cref="T:System.Version" /> objects.</returns>
	public Version[] GetClrVersions()
	{
		if (clrVersions == null)
		{
			InternalGetClrVersions();
		}
		return clrVersions;
	}

	private void InternalGetClrVersions()
	{
		char[] anyOf = new char[2] { ';', ')' };
		string text = useragent;
		ArrayList arrayList = null;
		int num;
		while (text != null && (num = text.IndexOf(".NET CLR ")) != -1)
		{
			int num2 = text.IndexOfAny(anyOf, num + 9);
			if (num2 == -1)
			{
				break;
			}
			string text2 = text.Substring(num + 9, num2 - num - 9);
			Version version = null;
			try
			{
				version = new Version(text2);
				if (clrVersion == null || version > clrVersion)
				{
					clrVersion = version;
				}
				if (arrayList == null)
				{
					arrayList = new ArrayList(4);
				}
				arrayList.Add(version);
			}
			catch
			{
			}
			text = text.Substring(num + 9);
		}
		if (arrayList == null || arrayList.Count == 0)
		{
			clrVersion = new Version();
			clrVersions = null;
		}
		else
		{
			arrayList.Sort();
			clrVersions = (Version[])arrayList.ToArray(typeof(Version));
		}
	}

	private bool ReadBoolean(string key)
	{
		return string.Compare(this[key] ?? throw CreateCapabilityNotFoundException(key), "True", ignoreCase: true, Helpers.InvariantCulture) == 0;
	}

	private int ReadInt32(string key)
	{
		string text = this[key];
		if (text == null)
		{
			throw CreateCapabilityNotFoundException(key);
		}
		try
		{
			return int.Parse(text);
		}
		catch
		{
			throw CreateCapabilityNotFoundException(key);
		}
	}

	private double ReadDouble(string key)
	{
		string text = this[key];
		if (text == null)
		{
			throw CreateCapabilityNotFoundException(key);
		}
		try
		{
			return double.Parse(text);
		}
		catch
		{
			throw CreateCapabilityNotFoundException(key);
		}
	}

	private string ReadString(string key)
	{
		return this[key] ?? throw CreateCapabilityNotFoundException(key);
	}

	private Version ReadVersion(string key)
	{
		string text = this[key];
		if (text == null)
		{
			throw CreateCapabilityNotFoundException(key);
		}
		try
		{
			return new Version(text);
		}
		catch
		{
			throw CreateCapabilityNotFoundException(key);
		}
	}

	private ArrayList ReadArrayList(string key)
	{
		return ((ArrayList)capabilities[key]) ?? throw CreateCapabilityNotFoundException(key);
	}

	private Exception CreateCapabilityNotFoundException(string key)
	{
		return new ArgumentNullException($"browscaps.ini does not contain a definition for capability {key} for userAgent {Browser}");
	}

	private bool Get(int idx)
	{
		return flags.Get(idx);
	}

	private void Set(int idx)
	{
		flags.Set(idx, value: true);
	}
}
