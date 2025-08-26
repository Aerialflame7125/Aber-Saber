using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace System.Web;

/// <summary>Serves as the base class for classes that enable the server to gather information about the capabilities of the browser that made the current request.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpBrowserCapabilitiesBase : IFilterResolutionService
{
	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is capable of supporting ActiveX controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser can support ActiveX controls; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool ActiveXControls
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the collection of available control adapters.</summary>
	/// <returns>The registered control adapters for the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IDictionary Adapters
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the client is an America Online (AOL) browser.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is an AOL browser; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool AOL
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports playing background sounds by using the <see langword="bgsounds" /> HTML element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports playing background sounds; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool BackgroundSounds
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is a beta version.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a beta version; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Beta
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the browser string (if any) that was sent by the browser in the <see langword="User-Agent" /> request header.</summary>
	/// <returns>The contents of the <see langword="User-Agent" /> request header that was sent by the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Browser
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a collection of browsers for which capabilities are recognized.</summary>
	/// <returns>The browsers for which capabilities are recognized.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual ArrayList Browsers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports decks that contain multiple forms, such as separate cards.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports decks that contain multiple forms; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanCombineFormsInDeck
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser device is capable of initiating a voice call.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser device is capable of initiating a voice call; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanInitiateVoiceCall
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports page content that follows WML <see langword="select" /> or <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports page content that follows HTML <see langword="select " />or <see langword="input " />elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderAfterInputOrSelectElement
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports empty HTML <see langword="select" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports empty HTML <see langword="select" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderEmptySelects
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML <see langword="input" /> and <see langword="select" /> elements together in the same card.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="input" /> and <see langword="select" /> elements together; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderInputAndSelectElementsTogether
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML <see langword="option" /> elements that specify both <see langword="onpick" /> and <see langword="value" /> attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="option" /> elements that specify both <see langword="onpick" /> and <see langword="value" /> attributes; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderMixedSelects
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML <see langword="onevent" /> and <see langword="prev" /> elements in the same card.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="onevent" /> and <see langword="prev" /> elements in the same WML card; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderOneventAndPrevElementsTogether
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML cards for postback.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML cards for postback; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderPostBackCards
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports WML <see langword="setvar" /> elements that have a <see langword="value" /> attribute of 0.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports WML <see langword="setvar" /> elements that have a <see langword="value" /> attribute of 0; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanRenderSetvarZeroWithMultiSelectionList
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports sending e-mail messages by using the HTML <see langword="mailto" /> scheme.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports sending e-mail message by using the HTML <see langword="mailto" /> scheme; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CanSendMail
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, used internally to get the defined capabilities of the browser.</summary>
	/// <returns>The defined capabilities of the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IDictionary Capabilities
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports Channel Definition Format (CDF) for webcasting.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports CDF; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool CDF
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the version of the .NET Framework that is installed on the client.</summary>
	/// <returns>The common language runtime (CLR) version.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Version ClrVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is capable of supporting cookies.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser can support cookies; otherwise, <see langword="false" />.This property does not indicate whether cookies are currently enabled in the browser, only whether the browser can support cookies.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Cookies
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is a search-engine Web crawler.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a search-engine crawler; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Crawler
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the maximum number of submit buttons that are allowed for a form.</summary>
	/// <returns>The maximum number of submit buttons that are allowed for a form.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int DefaultSubmitButtonLimit
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the version number of ECMAScript (JavaScript) that the browser supports.</summary>
	/// <returns>The version number of ECMAScript (JavaScript) that the browser supports.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Version EcmaScriptVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML frames.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports frames; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Frames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the major version number of the wireless gateway that is used to access the server, if known. </summary>
	/// <returns>The major version number of the wireless gateway that is used to access the server, if known.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int GatewayMajorVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the minor version number of the wireless gateway that is used to access the server, if known. </summary>
	/// <returns>The minor version number of the wireless gateway that is used to access the server, if known.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual double GatewayMinorVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the version of the wireless gateway that is used to access the server, if known.</summary>
	/// <returns>The version number of the wireless gateway that is used to access the server, if known.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string GatewayVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser has a dedicated Back button.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser has a dedicated Back button; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool HasBackButton
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the scrollbar of an HTML <see langword="select multiple" /> element that has an <see langword="align" /> attribute value of <see langword="right" /> is obscured upon rendering.</summary>
	/// <returns>
	///     <see langword="true" /> if the scrollbar of an HTML <see langword="select multiple" /> element that has an <see langword="align" /> attribute value of <see langword="right" /> is obscured upon rendering; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool HidesRightAlignedMultiselectScrollbars
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets or sets the fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</summary>
	/// <returns>The fully qualified class name of the <see cref="T:System.Web.UI.HtmlTextWriter" /> to use for writing markup characters and text.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string HtmlTextWriter
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the internal identifier of the browser as specified in the browser definition file.</summary>
	/// <returns>The internal identifier of the browser as specified in the browser definition file.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Id
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the type of input that is supported by the browser.</summary>
	/// <returns>The type of input supported by the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string InputType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser has a color display.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser has a color display; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsColor
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser is a recognized mobile device.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is a recognized mobile device; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsMobileDevice
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports Java.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports Java; otherwise, <see langword="false" />.This property does not indicate whether Java is currently enabled in the browser, only whether the browser can support Java.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool JavaApplets
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the JScript version that the browser supports.</summary>
	/// <returns>The version of JScript that the browser supports.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Version JScriptVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the major (integer) version number of the browser.</summary>
	/// <returns>The major version number of the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int MajorVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the maximum length in characters for the <see langword="href" /> attribute of an HTML <see langword="a" /> (anchor) element.</summary>
	/// <returns>The maximum length in characters for the <see langword="href" /> attribute of an HTML <see langword="a" /> (anchor) element.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int MaximumHrefLength
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the maximum length of the page, in bytes, that the browser can display. </summary>
	/// <returns>The maximum length of the page, in bytes, that the browser can display.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int MaximumRenderedPageSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the maximum length of the text that a soft-key label can display.</summary>
	/// <returns>The maximum length of the text that a soft-key label can display.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int MaximumSoftkeyLabelLength
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the minor (decimal) version number of the browser.</summary>
	/// <returns>The minor version number of the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual double MinorVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the minor (decimal) version number of the browser as a string.</summary>
	/// <returns>A string that represents the minor version number of the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string MinorVersionString
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the name of the manufacturer of a mobile device, if known.</summary>
	/// <returns>The name of the manufacturer of a mobile device, if known.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string MobileDeviceManufacturer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the model name of a mobile device, if known.</summary>
	/// <returns>The model name of a mobile device, if known.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string MobileDeviceModel
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the version of the Microsoft HTML (MSHTML) Document Object Model (DOM) that the browser supports.</summary>
	/// <returns>The MSHTML DOM version that the browser supports.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Version MSDomVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the number of softkeys on a mobile device.</summary>
	/// <returns>The number of softkeys supported on a mobile device.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int NumberOfSoftkeys
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the name of the operating system that the client is using, if known.</summary>
	/// <returns>The operating system that the client is using, if known.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Platform
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the MIME type of the type of image content that the browser typically prefers.</summary>
	/// <returns>The MIME type of the type of image content that the browser typically prefers.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string PreferredImageMime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the MIME type of the type of content that the browser typically prefers.</summary>
	/// <returns>The MIME type of the type of content that the browser typically prefers.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string PreferredRenderingMime
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the general name for the type of content that the browser prefers.</summary>
	/// <returns>The values "html32" or "chtml10".</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string PreferredRenderingType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the request encoding that the browser prefers.</summary>
	/// <returns>The request encoding that the browser prefers.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string PreferredRequestEncoding
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the response encoding that the browser prefers.</summary>
	/// <returns>The response encoding that the browser prefers.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string PreferredResponseEncoding
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break before WML <see langword="select" /> or <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break before <see langword="select" /> or <see langword="input" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RendersBreakBeforeWmlSelectAndInput
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break after list-item elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after list-item elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RendersBreaksAfterHtmlLists
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break after a standalone WML <see langword="a" /> (anchor) element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after a standalone WML <see langword="a" /> (anchor) element; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RendersBreaksAfterWmlAnchor
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders a line break after a WML <see langword="input" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders a line break after a WML <see langword="input" /> element; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RendersBreaksAfterWmlInput
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the mobile-device browser renders a WML <see langword="do" /> form accept construct as an inline button instead of as a softkey.</summary>
	/// <returns>
	///     <see langword="true" /> if the mobile-device browser renders a WML <see langword="do" /> form-accept construct as an inline button; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RendersWmlDoAcceptsInline
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser renders WML <see langword="select" /> elements as menu cards, instead of as a combo box.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser renders WML <see langword="select" /> elements as menu cards; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RendersWmlSelectsAsMenuCards
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, used internally to produce a meta-tag that is required by some browsers.</summary>
	/// <returns>A meta-tag that is required by some browsers.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string RequiredMetaTagNameValue
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires colons in element attribute values to be replaced with a different character.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires colons in element attribute values to be replaced with a different character; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresAttributeColonSubstitution
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires an HTML <see langword="meta" /> element for which the <see langword="content-type" /> attribute is specified.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires an HTML <see langword="meta" /> element for which the <see langword="content-type" /> attribute is specified; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresContentTypeMetaTag
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires control state to be maintained in sessions.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires control state to be maintained in sessions; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresControlStateInSession
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires a double-byte character set.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires a double-byte character set; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresDBCSCharacter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires nonstandard error messages.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires nonstandard error messages; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresHtmlAdaptiveErrorReporting
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires the first element in the body of a Web page to be an HTML <see langword="br" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires the first element in the body of a Web page to be an HTML <see langword="br" /> element; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresLeadingPageBreak
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser does not support HTML <see langword="br" /> elements to format line breaks.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser does not support HTML <see langword="br" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresNoBreakInFormatting
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires pages to contain a size-optimized form of markup language tags.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires pages to contain a size-optimized form of markup language tags; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresOutputOptimization
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports telephone dialing based on plain text, or whether it requires special markup.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports telephone dialing based on plain text; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresPhoneNumbersAsPlainText
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires view-state values to be specially encoded.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires view-state values to be specially encoded; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresSpecialViewStateEncoding
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires unique form-action URLs.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique form-action URLs; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresUniqueFilePathSuffix
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input type=&quot;checkbox&quot;" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input type=&quot;checkbox&quot;" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresUniqueHtmlCheckboxNames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser requires unique <see langword="name" /> attribute values for multiple HTML <see langword="input" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresUniqueHtmlInputNames
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether postback data that is sent by the browser will be URL-encoded.</summary>
	/// <returns>
	///     <see langword="true" /> if postback data that is sent by the browser will be URL-encoded; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool RequiresUrlEncodedPostfieldValues
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the depth of the display, in bits per pixel.</summary>
	/// <returns>The depth of the display, in bits per pixel.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int ScreenBitDepth
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the approximate height of the display, in character lines.</summary>
	/// <returns>The approximate height of the display, in character lines.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int ScreenCharactersHeight
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the approximate width of the display, in characters.</summary>
	/// <returns>The approximate width of the display, in characters.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int ScreenCharactersWidth
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the approximate height of the display, in pixels.</summary>
	/// <returns>The approximate height of the display, in pixels.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int ScreenPixelsHeight
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the approximate width of the display, in pixels.</summary>
	/// <returns>The approximate width of the display, in pixels.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int ScreenPixelsWidth
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="accesskey" /> attribute of HTML <see langword="a" /> (anchor) and <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="accesskey" /> attribute of HTML <see langword="a" /> (anchor) and <see langword="input" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsAccesskeyAttribute
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="bgcolor" /> attribute of the HTML <see langword="body" /> element.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="bgcolor" /> attribute of the HTML <see langword="body" /> element; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsBodyColor
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML <see langword="b" /> elements to format bold text.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="b" /> elements to format bold text; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsBold
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="cache-control" /> value for the <see langword="http-equiv" /> attribute of HTML <see langword="meta" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="cache-control" /> value for the <see langword="http-equiv" /> attribute of HTML <see langword="meta" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsCacheControlMetaTag
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports callback scripts.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports callback scripts; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsCallback
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports cascading style sheets (CSS).</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports CSS; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsCss
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="align" /> attribute of HTML <see langword="div" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="align" /> attribute of HTML <see langword="div" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsDivAlign
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="nowrap" /> attribute of HTML <see langword="div " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="nowrap" /> HTML <see langword="div" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsDivNoWrap
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports empty strings in cookie values.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports empty strings in cookie values; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsEmptyStringInCookieValue
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="color" /> attribute of HTML <see langword="font" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="color" /> attribute of HTML <see langword="font" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsFontColor
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="name" /> attribute of HTML <see langword="font" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="name" /> attribute of HTML <see langword="font" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsFontName
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="size" /> attribute of HTML <see langword="font " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="size" /> attribute of HTML <see langword="font" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsFontSize
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the use of a custom image in place of a standard form submit button.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the use of a custom image in place of a standard form submit button; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsImageSubmit
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports i-mode symbols.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports i-mode symbols; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsIModeSymbols
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="istyle" /> attribute of HTML <see langword="input" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="istyle" /> attribute of HTML <see langword="input" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsInputIStyle
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="mode" /> attribute of HTML <see langword="input " />elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="mode" /> attribute of HTML <see langword="input" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsInputMode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML <see langword="i" /> elements to format italic text.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="i" /> elements to format italic text; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsItalic
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports J-Phone multimedia attributes.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports J-Phone multimedia attributes; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsJPhoneMultiMediaAttributes
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports J-Phone–specific picture symbols.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports J-Phone–specific picture symbols; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsJPhoneSymbols
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports a query string in the <see langword="action" /> attribute value of HTML <see langword="form" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports a query string in the <see langword="action" /> attribute value of HTML <see langword="form" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsQueryStringInFormAction
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports cookies on redirection.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports cookies on redirection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsRedirectWithCookie
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports the <see langword="multiple" /> attribute of HTML <see langword="select" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports the <see langword="multiple" /> attribute of HTML <see langword="select" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsSelectMultiple
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether clearing a checked HTML <see langword="input type=&quot;checkbox&quot;" /> element is reflected in postback data.</summary>
	/// <returns>
	///     <see langword="true" /> if clearing a checked HTML <see langword="input type=&quot;checkbox&quot;" /> element is reflected in postback data; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsUncheck
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports receiving XML over HTTP.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports receiving XML over HTTP; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SupportsXmlHttp
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports HTML <see langword="table" /> elements.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports HTML <see langword="table" /> elements; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Tables
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, used internally to get the type of the object that is used to write tags for the browser.</summary>
	/// <returns>The type of the object that is used to write tags for the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Type TagWriter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the name and major (integer) version number of the browser.</summary>
	/// <returns>The name and major version number of the browser.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Type
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, used internally to get a value that indicates whether to use an optimized cache key.</summary>
	/// <returns>
	///     <see langword="true" /> to use an optimized cache key; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool UseOptimizedCacheKey
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the browser supports Visual Basic Scripting edition (VBScript).</summary>
	/// <returns>
	///     <see langword="true" /> if the browser supports VBScript; otherwise, <see langword="false" />.This property does not indicate whether VBScript is currently enabled in the browser, only whether the browser can support VBScript.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool VBScript
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the full version number (integer and decimal) of the browser as a string.</summary>
	/// <returns>The full version number of the browser as a string.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Version
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the version of the World Wide Web Consortium (W3C) XML Document Object Model (DOM) that the browser supports.</summary>
	/// <returns>The number of the W3C XML DOM version number that the browser supports.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Version W3CDomVersion
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the client is a Win16-based computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the browser is running on a Win16-based computer; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Win16
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the client is a Win32-based computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is a Win32-based computer; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Win32
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the value of the specified browser capability. In C#, this property is the indexer for the class.</summary>
	/// <param name="key">The name of the browser capability to retrieve.</param>
	/// <returns>The browser capability with the specified key name.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string this[string key]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, used internally to add an entry to the internal collection of browsers for which capabilities are recognized.</summary>
	/// <param name="browserName">The name of the browser to add.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddBrowser(string browserName)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, creates a new instance of the <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render markup to the browser.</summary>
	/// <param name="w">The object to be created.</param>
	/// <returns>A new instance of the object.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, used internally to disable use of an optimized cache key.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void DisableOptimizedCacheKey()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, gets all versions of the .NET Framework common language runtime (CLR) that are installed on the client.</summary>
	/// <returns>An array of <see cref="T:System.Version" /> objects.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Version[] GetClrVersions()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the client browser is the same as the specified browser.</summary>
	/// <param name="browserName">The specified browser.</param>
	/// <returns>
	///     <see langword="true" /> if the client browser is the same as the specified browser; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsBrowser(string browserName)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, used internally to compare filters.</summary>
	/// <param name="filter1">The first filter to compare.</param>
	/// <param name="filter2">The second filter to compare.</param>
	/// <returns>1 if <paramref name="filter1" /> is a parent of <paramref name="filter2" />; -1 if <paramref name="filter2" /> is a parent of <paramref name="filter1" />; or 0 if there is no parent-child relationship between <paramref name="filter1" /> and <paramref name="filter2" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int CompareFilters(string filter1, string filter2)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, used internally to evaluate a filter.</summary>
	/// <param name="filterName">The filter to evaluate.</param>
	/// <returns>
	///     <see langword="true" /> if the filter was successfully evaluated; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool EvaluateFilter(string filterName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpBrowserCapabilitiesBase()
	{
	}
}
