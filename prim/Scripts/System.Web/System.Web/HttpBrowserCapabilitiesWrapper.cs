using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that enables the server to gather information about the capabilities of the browser that has made the current request.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class HttpBrowserCapabilitiesWrapper : HttpBrowserCapabilitiesBase
{
	private HttpBrowserCapabilities _browser;

	/// <summary>Gets the browser string (if any) that was sent by the browser in the <see langword="User-Agent" /> request header.</summary>
	/// <returns>The contents of the <see langword="User-Agent" /> request header sent by the browser.</returns>
	public override string Browser => _browser.Browser;

	/// <summary>Gets the version number of ECMAScript (JavaScript) that the browser supports.</summary>
	/// <returns>The version number of ECMAScript (JavaScript) that the browser supports.</returns>
	public override Version EcmaScriptVersion => _browser.EcmaScriptVersion;

	/// <summary>Gets the JScript version that the browser supports.</summary>
	/// <returns>The version of JScript that the browser supports.</returns>
	public override Version JScriptVersion => _browser.JScriptVersion;

	/// <summary>Gets a value that indicates whether the browser supports callback scripts.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports callback scripts; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsCallback => _browser.SupportsCallback;

	/// <summary>Gets the version of the World Wide Web Consortium (W3C) XML Document Object Model (DOM) that the browser supports.</summary>
	/// <returns>The number of the W3C XML DOM version number that the browser supports.</returns>
	public override Version W3CDomVersion => _browser.W3CDomVersion;

	/// <summary>Gets a value that indicates whether the browser is capable of supporting ActiveX controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser can support ActiveX controls; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool ActiveXControls => _browser.ActiveXControls;

	/// <summary>Gets the collection of available control adapters.</summary>
	/// <returns>The collection of registered control adapters for the browser.</returns>
	public override IDictionary Adapters => _browser.Adapters;

	/// <summary>Gets a value that indicates whether the client is an America Online (AOL) browser.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is an AOL browser; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool AOL => _browser.AOL;

	/// <summary>Gets a value that indicates whether the browser supports playing background sounds by using the <see langword="bgsounds" /> HTML element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports playing background sounds; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool BackgroundSounds => _browser.BackgroundSounds;

	/// <summary>Gets a value that indicates whether the browser is a beta version.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a beta version; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool Beta => _browser.Beta;

	/// <summary>Gets a collection of browsers for which capabilities are recognized.</summary>
	/// <returns>The browsers for which capabilities are recognized.</returns>
	public override ArrayList Browsers => _browser.Browsers;

	/// <summary>Gets a value that indicates whether the browser supports decks that contain multiple forms, such as separate cards.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports decks that contain multiple forms; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanCombineFormsInDeck => _browser.CanCombineFormsInDeck;

	/// <summary>Gets a value that indicates whether the browser device is capable of initiating a voice call.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser device is capable of initiating a voice call; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool CanInitiateVoiceCall => _browser.CanInitiateVoiceCall;

	/// <summary>Gets a value that indicates whether the browser supports page content that follows WML <see langword="select" /> or <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports page content that follows HTML <see langword="select " />or <see langword="input " />elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanRenderAfterInputOrSelectElement => _browser.CanRenderAfterInputOrSelectElement;

	/// <summary>Gets a value that indicates whether the browser supports empty HTML <see langword="select" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports empty HTML <see langword="select" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanRenderEmptySelects => _browser.CanRenderEmptySelects;

	/// <summary>Gets a value that indicates whether the browser supports WML <see langword="input" /> and <see langword="select" /> elements together in the same card.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="input" /> and <see langword="select" /> elements together; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool CanRenderInputAndSelectElementsTogether => _browser.CanRenderInputAndSelectElementsTogether;

	/// <summary>Gets a value that indicates whether the browser supports WML <see langword="option" /> elements that specify both <see langword="onpick" /> and <see langword="value" /> attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="option" /> elements that specify both <see langword="onpick" /> and <see langword="value" /> attributes; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanRenderMixedSelects => _browser.CanRenderMixedSelects;

	/// <summary>Gets a value that indicates whether the browser supports WML <see langword="onevent" /> and <see langword="prev" /> elements in the same card.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="onevent" /> and <see langword="prev" /> elements in the same card; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanRenderOneventAndPrevElementsTogether => _browser.CanRenderOneventAndPrevElementsTogether;

	/// <summary>Gets a value that indicates whether the browser supports WML cards for postback.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML cards for postback; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanRenderPostBackCards => _browser.CanRenderPostBackCards;

	/// <summary>Gets a value that indicates whether the browser supports WML <see langword="setvar" /> elements that have a <see langword="value" /> attribute of 0.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="setvar" /> elements that have a <see langword="value" /> attribute of 0; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanRenderSetvarZeroWithMultiSelectionList => _browser.CanRenderSetvarZeroWithMultiSelectionList;

	/// <summary>Gets a value that indicates whether the browser supports sending e-mail messages by using the HTML <see langword="mailto" /> scheme.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports sending e-mail message by using the HTML <see langword="mailto" /> scheme; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool CanSendMail => _browser.CanSendMail;

	/// <summary>Used internally to get the defined capabilities of the browser.</summary>
	/// <returns>The defined capabilities of the browser.</returns>
	public override IDictionary Capabilities
	{
		get
		{
			return _browser.Capabilities;
		}
		set
		{
			_browser.Capabilities = value;
		}
	}

	/// <summary>Gets a value that indicates whether the browser supports Channel Definition Format (CDF) for webcasting.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports CDF; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool CDF => _browser.CDF;

	/// <summary>Gets the version of the .NET Framework that is installed on the client.</summary>
	/// <returns>The common language runtime (CLRS) version.</returns>
	public override Version ClrVersion => _browser.ClrVersion;

	/// <summary>Gets a value that indicates whether the browser is capable of supporting cookies.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser can support cookies; otherwise, <see langword="false" />. The default is <see langword="false" />.This property does not indicate whether cookies are currently enabled in the browser, only whether the browser can support cookies.</returns>
	public override bool Cookies => _browser.Cookies;

	/// <summary>Gets a value that indicates whether the browser is a search-engine Web crawler.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a search-engine crawler; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool Crawler => _browser.Crawler;

	/// <summary>Gets the maximum number of submit buttons that are allowed for a form.</summary>
	/// <returns>The maximum number of submit buttons that are allowed for a form.</returns>
	public override int DefaultSubmitButtonLimit => _browser.DefaultSubmitButtonLimit;

	/// <summary>Gets a value that indicates whether the browser supports HTML frames.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports frames; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool Frames => _browser.Frames;

	/// <summary>Gets the major version number of the wireless gateway that is used to access the server, if known. </summary>
	/// <returns>The major version number of the wireless gateway that is used to access the server, if known. The default is 0.</returns>
	public override int GatewayMajorVersion => _browser.GatewayMajorVersion;

	/// <summary>Gets the minor version number of the wireless gateway that is used to access the server, if known. </summary>
	/// <returns>The minor version number of the wireless gateway that is used to access the server, if known. The default is 0.</returns>
	public override double GatewayMinorVersion => _browser.GatewayMinorVersion;

	/// <summary>Gets the version of the wireless gateway that is used to access the server, if known.</summary>
	/// <returns>The version number of the wireless gateway that is used to access the server, if known. The default is "None".</returns>
	public override string GatewayVersion => _browser.GatewayVersion;

	/// <summary>Gets a value that indicates whether the browser has a dedicated Back button.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser has a dedicated Back button; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool HasBackButton => _browser.HasBackButton;

	/// <summary>Gets a value that indicates whether the scrollbar of an HTML <see langword="select multiple" /> element that has an <see langword="align" /> attribute value of <see langword="right" /> is obscured upon rendering.</summary>
	/// <returns>
	///     <see langword="true" /> if the scrollbar of an HTML <see langword="select multiple" /> element that has an <see langword="align" /> attribute value of <see langword="right" /> is obscured upon rendering; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool HidesRightAlignedMultiselectScrollbars => _browser.HidesRightAlignedMultiselectScrollbars;

	/// <summary>Gets or sets the fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</summary>
	/// <returns>The fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</returns>
	public override string HtmlTextWriter
	{
		get
		{
			return _browser.HtmlTextWriter;
		}
		set
		{
			_browser.HtmlTextWriter = value;
		}
	}

	/// <summary>Gets the internal identifier of the browser as specified in the browser definition file.</summary>
	/// <returns>The internal identifier of the browser as specified in the browser definition file.</returns>
	public override string Id => _browser.Id;

	/// <summary>Gets the type of input that is supported by the browser.</summary>
	/// <returns>The type of input supported by the browser. The default is "telephoneKeypad".</returns>
	public override string InputType => _browser.InputType;

	/// <summary>Gets a value that indicates whether the browser has a color display.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser has a color display; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool IsColor => _browser.IsColor;

	/// <summary>Gets a value that indicates whether the browser is a recognized mobile device.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a recognized mobile device; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool IsMobileDevice => _browser.IsMobileDevice;

	/// <summary>Gets a value that indicates whether the browser supports Java.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports Java; otherwise, <see langword="false" />. The default is <see langword="false" />.This property does not indicate whether Java is currently enabled in the browser, only whether the browser can support Java.</returns>
	public override bool JavaApplets => _browser.JavaApplets;

	/// <summary>Gets the major (integer) version number of the browser.</summary>
	/// <returns>The major version number of the browser.</returns>
	public override int MajorVersion => _browser.MajorVersion;

	/// <summary>Gets the maximum length in characters for the <see langword="href" /> attribute of an HTML <see langword="a" /> (anchor) element.</summary>
	/// <returns>The maximum length in characters for the <see langword="href" /> attribute of an HTML <see langword="a" /> (anchor) element. The default value is the value in the <see cref="P:System.Web.HttpBrowserCapabilitiesWrapper.Item(System.String)" /> property with the key name of "maximumHrefLength".</returns>
	public override int MaximumHrefLength => _browser.MaximumHrefLength;

	/// <summary>Gets the maximum length of the page, in bytes, that the browser can display. </summary>
	/// <returns>The maximum length of the page, in bytes, that the browser can display. The default is 2000.</returns>
	public override int MaximumRenderedPageSize => _browser.MaximumRenderedPageSize;

	/// <summary>Gets the maximum length of the text that a soft-key label can display.</summary>
	/// <returns>The maximum length of the text that a soft-key label can display. The default is 5.</returns>
	public override int MaximumSoftkeyLabelLength => _browser.MaximumSoftkeyLabelLength;

	/// <summary>Gets the minor (decimal) version number of the browser.</summary>
	/// <returns>The minor version number of the browser.</returns>
	public override double MinorVersion => _browser.MinorVersion;

	/// <summary>Gets the minor (decimal) version number of the browser as a string.</summary>
	/// <returns>A string that represents the minor version number of the browser.</returns>
	public override string MinorVersionString => _browser.MinorVersionString;

	/// <summary>Gets the name of the manufacturer of a mobile device, if known.</summary>
	/// <returns>The name of the manufacturer of a mobile device, if known. The default is "Unknown".</returns>
	public override string MobileDeviceManufacturer => _browser.MobileDeviceManufacturer;

	/// <summary>Gets the model name of a mobile device, if known.</summary>
	/// <returns>The model name of a mobile device, if known. The default is "Unknown".</returns>
	public override string MobileDeviceModel => _browser.MobileDeviceModel;

	/// <summary>Gets the version of the Microsoft HTML (MSHTML) Document Object Model (DOM) that the browser supports.</summary>
	/// <returns>The MSHTML DOM version that the browser supports. The default is 0.0.</returns>
	public override Version MSDomVersion => _browser.MSDomVersion;

	/// <summary>Gets the number of softkeys on a mobile device.</summary>
	/// <returns>The number of softkeys supported on a mobile device. The default is 0.</returns>
	public override int NumberOfSoftkeys => _browser.NumberOfSoftkeys;

	/// <summary>Gets the name of the operating system that the client is using, if known.</summary>
	/// <returns>The operating system that the client is using, if known, otherwise the value is set to "Unknown".</returns>
	public override string Platform => _browser.Platform;

	/// <summary>Gets the MIME type of the type of image content that the browser typically prefers.</summary>
	/// <returns>The MIME type of the type of image content that the browser typically prefers. The default is "image/gif".</returns>
	public override string PreferredImageMime => _browser.PreferredImageMime;

	/// <summary>Gets the MIME type of the type of content that the browser typically prefers.</summary>
	/// <returns>The MIME type of the type of content that the browser typically prefers. The default is "text/html".</returns>
	public override string PreferredRenderingMime => _browser.PreferredRenderingMime;

	/// <summary>Gets the general name for the type of content that the browser prefers.</summary>
	/// <returns>The values "html32" or "chtml10". The default is "html32".</returns>
	public override string PreferredRenderingType => _browser.PreferredRenderingType;

	/// <summary>Gets the request encoding that the browser prefers.</summary>
	/// <returns>The request encoding preferred by the browser.</returns>
	public override string PreferredRequestEncoding => _browser.PreferredRequestEncoding;

	/// <summary>Gets the response encoding that the browser prefers.</summary>
	/// <returns>The response encoding preferred by the browser.</returns>
	public override string PreferredResponseEncoding => _browser.PreferredResponseEncoding;

	/// <summary>Gets a value that indicates whether the browser renders a line break before <see langword="select" /> or <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break before <see langword="select" /> or <see langword="input" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RendersBreakBeforeWmlSelectAndInput => _browser.RendersBreakBeforeWmlSelectAndInput;

	/// <summary>Gets a value that indicates whether the browser renders a line break after list-item elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after list-item elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool RendersBreaksAfterHtmlLists => _browser.RendersBreaksAfterHtmlLists;

	/// <summary>Gets a value that indicates whether the browser renders a line break after a standalone WML <see langword="a" /> (anchor) element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after a standalone WML <see langword="a" /> (anchor) element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RendersBreaksAfterWmlAnchor => _browser.RendersBreaksAfterWmlAnchor;

	/// <summary>Gets a value that indicates whether the browser renders a line break after a WML <see langword="input" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after a WML <see langword="input" /> element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RendersBreaksAfterWmlInput => _browser.RendersBreaksAfterWmlInput;

	/// <summary>Gets a value that indicates whether the mobile-device browser renders a WML <see langword="do" /> form accept construct as an inline button instead of as a softkey.</summary>
	/// <returns>
	///     <see langword="true" /> if the mobile-device browser renders a WML <see langword="do" /> form-accept construct as an inline button; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool RendersWmlDoAcceptsInline => _browser.RendersWmlDoAcceptsInline;

	/// <summary>Gets a value that indicates whether the browser renders WML <see langword="select" /> elements as menu cards, instead of as a combo box.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders WML <see langword="select" /> elements as menu cards; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RendersWmlSelectsAsMenuCards => _browser.RendersWmlSelectsAsMenuCards;

	/// <summary>Used internally to produce a meta-tag that is required by some browsers.</summary>
	/// <returns>A meta-tag that is required by some browsers.</returns>
	public override string RequiredMetaTagNameValue => _browser.RequiredMetaTagNameValue;

	/// <summary>Gets a value that indicates whether the browser requires colons in element attribute values to be replaced with a different character.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires colons in element attribute values to be replaced with a different character; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresAttributeColonSubstitution => _browser.RequiresAttributeColonSubstitution;

	/// <summary>Gets a value that indicates whether the browser requires an HTML <see langword="meta" /> element for which the <see langword="content-type" /> attribute is specified.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires an HTML <see langword="meta" /> element for which the <see langword="content-type" /> attribute is specified; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresContentTypeMetaTag => _browser.RequiresContentTypeMetaTag;

	/// <summary>Gets a value that indicates whether the browser requires control state to be maintained in sessions.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires control state to be maintained in sessions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresControlStateInSession => _browser.RequiresControlStateInSession;

	/// <summary>Gets a value that indicates whether the browser requires a double-byte character set.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires a double-byte character set; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresDBCSCharacter => _browser.RequiresDBCSCharacter;

	/// <summary>Gets a value that indicates whether the browser requires nonstandard error messages.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires nonstandard error messages; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresHtmlAdaptiveErrorReporting => _browser.RequiresHtmlAdaptiveErrorReporting;

	/// <summary>Gets a value that indicates whether the browser requires the first element in the body of a Web page to be an HTML <see langword="br" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires the first element in the body of a Web page to be an HTML <see langword="br" /> element; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresLeadingPageBreak => _browser.RequiresLeadingPageBreak;

	/// <summary>Gets a value that indicates whether the browser does not support HTML <see langword="br" /> elements to format line breaks.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser does not support HTML <see langword="br" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresNoBreakInFormatting => _browser.RequiresNoBreakInFormatting;

	/// <summary>Gets a value that indicates whether the browser requires pages to contain a size-optimized form of markup language tags.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires pages to contain a size-optimized form of markup language tags; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresOutputOptimization => _browser.RequiresOutputOptimization;

	/// <summary>Gets a value that indicates whether the browser supports telephone dialing based on plain text, or whether it requires special markup.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports telephone dialing based on plain text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresPhoneNumbersAsPlainText => _browser.RequiresPhoneNumbersAsPlainText;

	/// <summary>Gets a value that indicates whether the browser requires view-state values to be specially encoded.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires view-state values to be specially encoded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresSpecialViewStateEncoding => _browser.RequiresSpecialViewStateEncoding;

	/// <summary>Gets a value that indicates whether the browser requires unique form-action URLs.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique form-action URLs; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresUniqueFilePathSuffix => _browser.RequiresUniqueFilePathSuffix;

	/// <summary>Gets a value that indicates whether the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input type=&quot;checkbox&quot;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input type=&quot;checkbox&quot;" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresUniqueHtmlCheckboxNames => _browser.RequiresUniqueHtmlCheckboxNames;

	/// <summary>Gets a value that indicates whether the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresUniqueHtmlInputNames => _browser.RequiresUniqueHtmlInputNames;

	/// <summary>Gets a value that indicates whether postback data that is sent by the browser will be URL-encoded.</summary>
	/// <returns>
	///     <see langword="true" /> if postback data that is sent by the browser will be URL-encoded; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool RequiresUrlEncodedPostfieldValues => _browser.RequiresUrlEncodedPostfieldValues;

	/// <summary>Gets the depth of the display, in bits per pixel.</summary>
	/// <returns>The depth of the display, in bits per pixel. The default is 1.</returns>
	public override int ScreenBitDepth => _browser.ScreenBitDepth;

	/// <summary>Gets the approximate height of the display, in character lines.</summary>
	/// <returns>The approximate height of the display, in character lines. The default is 6.</returns>
	public override int ScreenCharactersHeight => _browser.ScreenCharactersHeight;

	/// <summary>Gets the approximate width of the display, in characters.</summary>
	/// <returns>The approximate width of the display, in characters. The default is 12.</returns>
	public override int ScreenCharactersWidth => _browser.ScreenCharactersWidth;

	/// <summary>Gets the approximate height of the display, in pixels.</summary>
	/// <returns>The approximate height of the display, in pixels. The default is 72.</returns>
	public override int ScreenPixelsHeight => _browser.ScreenPixelsHeight;

	/// <summary>Gets the approximate width of the display, in pixels.</summary>
	/// <returns>The approximate width of the display, in pixels. The default is 96.</returns>
	public override int ScreenPixelsWidth => _browser.ScreenPixelsWidth;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="accesskey" /> attribute of HTML <see langword="a" /> (anchor) and <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="accesskey" /> attribute of HTML <see langword="a" /> (anchor) and <see langword="input" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsAccesskeyAttribute => _browser.SupportsAccesskeyAttribute;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="bgcolor" /> attribute of the HTML <see langword="body" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="bgcolor" /> attribute of the HTML <see langword="body" /> element; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsBodyColor => _browser.SupportsBodyColor;

	/// <summary>Gets a value that indicates whether the browser supports HTML <see langword="b" /> elements to format bold text.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="b" /> elements to format bold text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsBold => _browser.SupportsBold;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="cache-control" /> value for the <see langword="http-equiv" /> attribute of HTML <see langword="meta" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="cache-control" /> value for the <see langword="http-equiv" /> attribute of HTML <see langword="meta" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsCacheControlMetaTag => _browser.SupportsCacheControlMetaTag;

	/// <summary>Gets a value that indicates whether the browser supports cascading style sheets (CSS).</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports CSS; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsCss => _browser.SupportsCss;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="align" /> attribute of HTML <see langword="div" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="align" /> attribute of HTML <see langword="div" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsDivAlign => _browser.SupportsDivAlign;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="nowrap" /> attribute of HTML <see langword="div " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="nowrap" /> HTML <see langword="div" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsDivNoWrap => _browser.SupportsDivNoWrap;

	/// <summary>Gets a value that indicates whether the browser supports empty strings in cookie values.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports empty strings in cookie values; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsEmptyStringInCookieValue => _browser.SupportsEmptyStringInCookieValue;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="color" /> attribute of HTML <see langword="font" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="color" /> attribute of HTML <see langword="font" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsFontColor => _browser.SupportsFontColor;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="name" /> attribute of HTML <see langword="font" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="name" /> attribute of HTML <see langword="font" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsFontName => _browser.SupportsFontName;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="size" /> attribute of HTML <see langword="font " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="size" /> attribute of HTML <see langword="font" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsFontSize => _browser.SupportsFontSize;

	/// <summary>Gets a value that indicates whether the browser supports the use of a custom image in place of a standard form submit button.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the use of a custom image in place of a standard form submit button; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsImageSubmit => _browser.SupportsImageSubmit;

	/// <summary>Gets a value that indicates whether the browser supports i-mode symbols.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports i-mode symbols; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsIModeSymbols => _browser.SupportsIModeSymbols;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="istyle" /> attribute of HTML <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="istyle" /> attribute of HTML <see langword="input" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsInputIStyle => _browser.SupportsInputIStyle;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="mode" /> attribute of HTML <see langword="input " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="mode" /> attribute of HTML <see langword="input" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsInputMode => _browser.SupportsInputMode;

	/// <summary>Gets a value that indicates whether the browser supports HTML <see langword="i" /> elements to format italic text.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="i" /> elements to format italic text; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsItalic => _browser.SupportsItalic;

	/// <summary>Gets a value that indicates whether the browser supports J-Phone multimedia attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports J-Phone multimedia attributes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsJPhoneMultiMediaAttributes => _browser.SupportsJPhoneMultiMediaAttributes;

	/// <summary>Gets a value that indicates whether the browser supports J-Phone–specific picture symbols.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports J-Phone–specific picture symbols; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsJPhoneSymbols => _browser.SupportsJPhoneSymbols;

	/// <summary>Gets a value that indicates whether the browser supports a query string in the <see langword="action" /> attribute value of HTML <see langword="form" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports a query string in the <see langword="action" /> attribute value of HTML <see langword="form" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsQueryStringInFormAction => _browser.SupportsQueryStringInFormAction;

	/// <summary>Gets a value that indicates whether the browser supports cookies on redirection.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports cookies on redirection; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsRedirectWithCookie => _browser.SupportsRedirectWithCookie;

	/// <summary>Gets a value that indicates whether the browser supports the <see langword="multiple" /> attribute of HTML <see langword="select" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="multiple" /> attribute of HTML <see langword="select" /> elements; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsSelectMultiple => _browser.SupportsSelectMultiple;

	/// <summary>Gets a value that indicates whether clearing a checked HTML <see langword="input type=&quot;checkbox&quot;" /> element is reflected in postback data.</summary>
	/// <returns>
	///     <see langword="true" /> if clearing a checked HTML <see langword="input type=&quot;checkbox&quot;" /> element is reflected in postback data; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool SupportsUncheck => _browser.SupportsUncheck;

	/// <summary>Gets a value that indicates whether the browser supports receiving XML over HTTP.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports receiving XML over HTTP; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool SupportsXmlHttp => _browser.SupportsXmlHttp;

	/// <summary>Gets a value that indicates whether the browser supports HTML <see langword="table" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="table" /> elements; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool Tables => _browser.Tables;

	/// <summary>Used internally to get the type of the object that is used to write tags for the browser.</summary>
	/// <returns>The type of the object that is used to write tags for the browser.</returns>
	public override Type TagWriter => _browser.TagWriter;

	/// <summary>Gets the name and major (integer) version number of the browser.</summary>
	/// <returns>The name and major version number of the browser.</returns>
	public override string Type => _browser.Type;

	/// <summary>Used internally to get a value that indicates whether to use an optimized cache key.</summary>
	/// <returns>
	///     <see langword="true" /> to use an optimized cache key; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool UseOptimizedCacheKey => _browser.UseOptimizedCacheKey;

	/// <summary>Gets a value that indicates whether the browser supports Visual Basic Scripting edition (VBScript).</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports VBScript; otherwise, <see langword="false" />. The default is <see langword="false" />.This property does not indicate whether VBScript is currently enabled in the browser, only whether the browser can support VBScript.</returns>
	public override bool VBScript => _browser.VBScript;

	/// <summary>Gets the full version number (integer and decimal) of the browser as a string.</summary>
	/// <returns>The full version number of the browser as a string.</returns>
	public override string Version => _browser.Version;

	/// <summary>Gets a value that indicates whether the client is a Win16-based computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is running on a Win16-based computer; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool Win16 => _browser.Win16;

	/// <summary>Gets a value that indicates whether the client is a Win32-based computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is a Win32-based computer; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool Win32 => _browser.Win32;

	/// <summary>Gets the value of the specified browser capability. In C#, this property is the indexer for the class.</summary>
	/// <param name="key">The name of the browser capability to retrieve.</param>
	/// <returns>The browser capability with the specified key name.</returns>
	public override string this[string key] => _browser[key];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpBrowserCapabilitiesWrapper" /> class. </summary>
	/// <param name="httpBrowserCapabilities">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpBrowserCapabilities" /> is <see langword="null" />.</exception>
	public HttpBrowserCapabilitiesWrapper(HttpBrowserCapabilities httpBrowserCapabilities)
	{
		if (httpBrowserCapabilities == null)
		{
			throw new ArgumentNullException("httpBrowserCapabilities");
		}
		_browser = httpBrowserCapabilities;
	}

	/// <summary>Used internally to add an entry to the internal collection of browsers for which capabilities are recognized.</summary>
	/// <param name="browserName">The name of the browser to add.</param>
	public override void AddBrowser(string browserName)
	{
		_browser.AddBrowser(browserName);
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render markup to the browser.</summary>
	/// <param name="w">The object to be created.</param>
	/// <returns>A new instance of the object.</returns>
	/// <exception cref="T:System.Exception">An error occurred when creating the <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</exception>
	public override HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
	{
		return _browser.CreateHtmlTextWriter(w);
	}

	/// <summary>Used internally to disable use of an optimized cache key.</summary>
	public override void DisableOptimizedCacheKey()
	{
		_browser.DisableOptimizedCacheKey();
	}

	/// <summary>Gets all versions of the .NET Framework common language runtime (CLR) that are installed on the client.</summary>
	/// <returns>An array of <see cref="T:System.Version" /> objects.</returns>
	public override Version[] GetClrVersions()
	{
		return _browser.GetClrVersions();
	}

	/// <summary>Gets a value that indicates whether the client browser is the same as the specified browser.</summary>
	/// <param name="browserName">The specified browser.</param>
	/// <returns>
	///     <see langword="true" /> if the client browser is the same as the specified browser; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public override bool IsBrowser(string browserName)
	{
		return _browser.IsBrowser(browserName);
	}

	/// <summary>Used internally to compare filters.</summary>
	/// <param name="filter1">The first filter to compare.</param>
	/// <param name="filter2">The second filter to compare.</param>
	/// <returns>1 if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1 if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; or 0 if there is no parent-child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
	public override int CompareFilters(string filter1, string filter2)
	{
		return ((IFilterResolutionService)_browser).CompareFilters(filter1, filter2);
	}

	/// <summary>Used internally to evaluate a filter.</summary>
	/// <param name="filterName">The filter to evaluate.</param>
	/// <returns>
	///     <see langword="true" /> if the filter was successfully evaluated; otherwise, <see langword="false" />.</returns>
	public override bool EvaluateFilter(string filterName)
	{
		return ((IFilterResolutionService)_browser).EvaluateFilter(filterName);
	}
}
