using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms;

/// <summary>Provides top-level programmatic access to an HTML document hosted by the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
/// <filterpriority>1</filterpriority>
public sealed class HtmlDocument
{
	private EventHandlerList events;

	private IWebBrowser webHost;

	private IDocument document;

	private WebBrowser owner;

	private static object ClickEvent;

	private static object ContextMenuShowingEvent;

	private static object FocusingEvent;

	private static object LosingFocusEvent;

	private static object MouseDownEvent;

	private static object MouseLeaveEvent;

	private static object MouseMoveEvent;

	private static object MouseOverEvent;

	private static object MouseUpEvent;

	private static object StopEvent;

	internal EventHandlerList Events
	{
		get
		{
			if (events == null)
			{
				events = new EventHandlerList();
			}
			return events;
		}
	}

	/// <summary>Provides the <see cref="T:System.Windows.Forms.HtmlElement" /> which currently has user input focus. </summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" />. </returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElement ActiveElement
	{
		get
		{
			IElement active = document.Active;
			if (active == null)
			{
				return null;
			}
			return new HtmlElement(owner, webHost, active);
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Drawing.Color" /> of a hyperlink when clicked by a user. </summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> for active links. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color ActiveLinkColor
	{
		get
		{
			return ParseColor(document.ActiveLinkColor);
		}
		set
		{
			document.ActiveLinkColor = value.ToArgb().ToString();
		}
	}

	/// <summary>Gets an instance of <see cref="T:System.Windows.Forms.HtmlElementCollection" />, which stores all <see cref="T:System.Windows.Forms.HtmlElement" /> objects for the document. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all elements in the document.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection All => new HtmlElementCollection(owner, webHost, document.DocumentElement.All);

	/// <summary>Gets or sets the background color of the HTML document.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> of the document's background.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color BackColor
	{
		get
		{
			return ParseColor(document.BackColor);
		}
		set
		{
			document.BackColor = value.ToArgb().ToString();
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlElement" /> for the BODY tag. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> object for the BODY tag.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElement Body => new HtmlElement(owner, webHost, document.Body);

	/// <summary>Gets or sets the HTTP cookies associated with this document.</summary>
	/// <returns>A <see cref="T:System.String" /> containing a list of cookies, with each cookie separated by a semicolon.</returns>
	/// <filterpriority>2</filterpriority>
	public string Cookie
	{
		get
		{
			return document.Cookie;
		}
		set
		{
			document.Cookie = value;
		}
	}

	/// <summary>Gets the encoding used by default for the current document. </summary>
	/// <returns>The <see cref="T:System.String" /> representing the encoding that the browser uses when the page is first displayed.</returns>
	public string DefaultEncoding => document.Charset;

	/// <summary>Gets or sets the string describing the domain of this document for security purposes.</summary>
	/// <returns>A valid domain. </returns>
	/// <exception cref="T:System.ArgumentException">The argument for the Domain property must be a valid domain name using Domain Name System (DNS) conventions.</exception>
	/// <filterpriority>1</filterpriority>
	public string Domain
	{
		get
		{
			return document.Domain;
		}
		set
		{
			throw new NotSupportedException("Setting the domain is not supported per the DOM Level 2 HTML specification. Sorry.");
		}
	}

	/// <summary>Gets the unmanaged interface pointer for this <see cref="T:System.Windows.Forms.HtmlDocument" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing an IDispatch pointer to the unmanaged document. </returns>
	/// <filterpriority>1</filterpriority>
	public object DomDocument
	{
		get
		{
			throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
		}
	}

	/// <summary>Gets or sets the character encoding for this document.</summary>
	/// <returns>The <see cref="T:System.String" /> representing the current character encoding.</returns>
	public string Encoding
	{
		get
		{
			return document.Charset;
		}
		set
		{
			document.Charset = value;
		}
	}

	/// <summary>Gets a value indicating whether the document has user input focus. </summary>
	/// <returns>true if the document has focus; otherwise, false.</returns>
	public bool Focused => webHost.Window.Document == document;

	/// <summary>Gets or sets the text color for the document.</summary>
	/// <returns>The color of the text in the document. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color ForeColor
	{
		get
		{
			return ParseColor(document.ForeColor);
		}
		set
		{
			document.ForeColor = value.ToArgb().ToString();
		}
	}

	/// <summary>Gets a collection of all of the &lt;FORM&gt; elements in the document. </summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of the &lt;FORM&gt; elements within the document.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection Forms => new HtmlElementCollection(owner, webHost, document.Forms);

	/// <summary>Gets a collection of all image tags in the document. </summary>
	/// <returns>A collection of <see cref="T:System.Windows.Forms.HtmlElement" /> objects, one for each IMG tag in the document. Elements are returned from the collection in source order. </returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection Images => new HtmlElementCollection(owner, webHost, document.Images);

	/// <summary>Gets or sets the color of hyperlinks.</summary>
	/// <returns>The color for hyperlinks in the current document.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color LinkColor
	{
		get
		{
			return ParseColor(document.LinkColor);
		}
		set
		{
			document.LinkColor = value.ToArgb().ToString();
		}
	}

	/// <summary>Gets a list of all the hyperlinks within this HTML document.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of <see cref="T:System.Windows.Forms.HtmlElement" /> objects.</returns>
	public HtmlElementCollection Links => new HtmlElementCollection(owner, webHost, document.Links);

	/// <summary>Gets or sets the direction of text in the current document.</summary>
	/// <returns>true if text renders from right to left; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool RightToLeft
	{
		get
		{
			IAttribute attribute = document.Attributes["dir"];
			return attribute != null && attribute.Value == "rtl";
		}
		set
		{
			IAttribute attribute = document.Attributes["dir"];
			if (attribute == null && value)
			{
				IAttribute attribute2 = document.CreateAttribute("dir");
				attribute2.Value = "rtl";
				document.AppendChild(attribute2);
			}
			else if (attribute != null && !value)
			{
				document.RemoveChild(attribute);
			}
		}
	}

	/// <summary>Gets or sets the text value of the &lt;TITLE&gt; tag in the current HTML document. </summary>
	/// <returns>The title of the current document.</returns>
	/// <filterpriority>1</filterpriority>
	public string Title
	{
		get
		{
			if (document == null)
			{
				return string.Empty;
			}
			return document.Title;
		}
		set
		{
			document.Title = value;
		}
	}

	/// <summary>Gets the URL describing the location of this document. </summary>
	/// <returns>A <see cref="T:System.Uri" /> representing this document's URL. </returns>
	public Uri Url => new Uri(document.Url);

	/// <summary>Gets or sets the Color of links to HTML pages that the user has already visited. </summary>
	/// <returns>The color of visited links. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color VisitedLinkColor
	{
		get
		{
			return ParseColor(document.VisitedLinkColor);
		}
		set
		{
			document.VisitedLinkColor = value.ToArgb().ToString();
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlWindow" /> associated with this document.</summary>
	/// <returns>The window for this document. </returns>
	/// <filterpriority>1</filterpriority>
	public HtmlWindow Window => new HtmlWindow(owner, webHost, webHost.Window);

	internal string DocType
	{
		get
		{
			if (document == null)
			{
				return string.Empty;
			}
			if (document.DocType != null)
			{
				return document.DocType.Name;
			}
			return string.Empty;
		}
	}

	/// <summary>Occurs when the user clicks anywhere on the document.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Click
	{
		add
		{
			Events.AddHandler(ClickEvent, value);
			document.Click += OnClick;
		}
		remove
		{
			Events.RemoveHandler(ClickEvent, value);
			document.Click -= OnClick;
		}
	}

	/// <summary>Occurs when the user requests to display the document's context menu. </summary>
	public event HtmlElementEventHandler ContextMenuShowing
	{
		add
		{
			Events.AddHandler(ContextMenuShowingEvent, value);
			owner.WebHost.ContextMenuShown += OnContextMenuShowing;
		}
		remove
		{
			Events.RemoveHandler(ContextMenuShowingEvent, value);
			owner.WebHost.ContextMenuShown -= OnContextMenuShowing;
		}
	}

	/// <summary>Occurs before focus is given to the document.</summary>
	public event HtmlElementEventHandler Focusing
	{
		add
		{
			Events.AddHandler(FocusingEvent, value);
			document.OnFocus += OnFocusing;
		}
		remove
		{
			Events.RemoveHandler(FocusingEvent, value);
			document.OnFocus -= OnFocusing;
		}
	}

	/// <summary>Occurs while focus is leaving a control.</summary>
	public event HtmlElementEventHandler LosingFocus
	{
		add
		{
			Events.AddHandler(LosingFocusEvent, value);
			document.OnBlur += OnLosingFocus;
		}
		remove
		{
			Events.RemoveHandler(LosingFocusEvent, value);
			document.OnBlur -= OnLosingFocus;
		}
	}

	/// <summary>Occurs when the user clicks the left mouse button.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseDown
	{
		add
		{
			Events.AddHandler(MouseDownEvent, value);
			document.MouseDown += OnMouseDown;
		}
		remove
		{
			Events.RemoveHandler(MouseDownEvent, value);
			document.MouseDown -= OnMouseDown;
		}
	}

	/// <summary>Occurs when the mouse is no longer hovering over the document. </summary>
	public event HtmlElementEventHandler MouseLeave
	{
		add
		{
			Events.AddHandler(MouseLeaveEvent, value);
			document.MouseLeave += OnMouseLeave;
		}
		remove
		{
			Events.RemoveHandler(MouseLeaveEvent, value);
			document.MouseLeave -= OnMouseLeave;
		}
	}

	/// <summary>Occurs when the mouse is moved over the document.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseMove
	{
		add
		{
			Events.AddHandler(MouseMoveEvent, value);
			document.MouseMove += OnMouseMove;
		}
		remove
		{
			Events.RemoveHandler(MouseMoveEvent, value);
			document.MouseMove -= OnMouseMove;
		}
	}

	/// <summary>Occurs when the mouse is moved over the document. </summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseOver
	{
		add
		{
			Events.AddHandler(MouseOverEvent, value);
			document.MouseOver += OnMouseOver;
		}
		remove
		{
			Events.RemoveHandler(MouseOverEvent, value);
			document.MouseOver -= OnMouseOver;
		}
	}

	/// <summary>Occurs when the user releases the left mouse button.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseUp
	{
		add
		{
			Events.AddHandler(MouseUpEvent, value);
			document.MouseUp += OnMouseUp;
		}
		remove
		{
			Events.RemoveHandler(MouseUpEvent, value);
			document.MouseUp -= OnMouseUp;
		}
	}

	/// <summary>Occurs when navigation to another Web page is halted.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Stop
	{
		add
		{
			Events.AddHandler(StopEvent, value);
			document.LoadStopped += OnStop;
		}
		remove
		{
			Events.RemoveHandler(StopEvent, value);
			document.LoadStopped -= OnStop;
		}
	}

	internal HtmlDocument(WebBrowser owner, IWebBrowser webHost)
		: this(owner, webHost, webHost.Document)
	{
	}

	internal HtmlDocument(WebBrowser owner, IWebBrowser webHost, IDocument doc)
	{
		this.webHost = webHost;
		document = doc;
		this.owner = owner;
	}

	static HtmlDocument()
	{
		Click = new object();
		ContextMenuShowing = new object();
		Focusing = new object();
		LosingFocus = new object();
		MouseDown = new object();
		MouseLeave = new object();
		MouseMove = new object();
		MouseOver = new object();
		MouseUp = new object();
		Stop = new object();
	}

	/// <summary>Adds an event handler for the named HTML DOM event.</summary>
	/// <param name="eventName">The name of the event you want to handle.</param>
	/// <param name="eventHandler">The managed code that handles the event. </param>
	public void AttachEventHandler(string eventName, EventHandler eventHandler)
	{
		document.AttachEventHandler(eventName, eventHandler);
	}

	/// <summary>Creates a new HtmlElement of the specified HTML tag type. </summary>
	/// <returns>A new element of the specified tag type. </returns>
	/// <param name="elementTag">The name of the HTML element to create. </param>
	/// <filterpriority>2</filterpriority>
	public HtmlElement CreateElement(string elementTag)
	{
		IElement element = document.CreateElement(elementTag);
		return new HtmlElement(owner, webHost, element);
	}

	/// <summary>Removes an event handler from a named event on the HTML DOM. </summary>
	/// <param name="eventName">The name of the event you want to cease handling.</param>
	/// <param name="eventHandler">The managed code that handles the event.</param>
	public void DetachEventHandler(string eventName, EventHandler eventHandler)
	{
		document.DetachEventHandler(eventName, eventHandler);
	}

	/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
	public override bool Equals(object obj)
	{
		return this == (HtmlDocument)obj;
	}

	/// <summary>Executes the specified command against the document. </summary>
	/// <param name="command">The name of the command to execute.</param>
	/// <param name="showUI">Whether or not to show command-specific dialog boxes or message boxes to the user. </param>
	/// <param name="value">The value to assign using the command. Not applicable for all commands.</param>
	public void ExecCommand(string command, bool showUI, object value)
	{
		throw new NotImplementedException("Not Supported");
	}

	/// <summary>Sets user input focus on the current document.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void Focus()
	{
		webHost.FocusIn(FocusOption.None);
	}

	/// <summary>Retrieves a single <see cref="T:System.Windows.Forms.HtmlElement" /> using the element's ID attribute as a search key.</summary>
	/// <returns>Returns the first object with the same ID attribute as the specified value, or null if the <paramref name="id" /> cannot be found. </returns>
	/// <param name="id">The ID attribute of the element to retrieve.</param>
	/// <filterpriority>1</filterpriority>
	public HtmlElement GetElementById(string id)
	{
		IElement elementById = document.GetElementById(id);
		if (elementById != null)
		{
			return new HtmlElement(owner, webHost, elementById);
		}
		return null;
	}

	/// <summary>Retrieves the HTML element located at the specified client coordinates.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> at the specified screen location in the document.</returns>
	/// <param name="point">The x,y position of the element on the screen, relative to the top-left corner of the document. </param>
	/// <filterpriority>1</filterpriority>
	public HtmlElement GetElementFromPoint(Point point)
	{
		IElement element = document.GetElement(point.X, point.Y);
		if (element != null)
		{
			return new HtmlElement(owner, webHost, element);
		}
		return null;
	}

	/// <summary>Retrieve a collection of elements with the specified HTML tag.</summary>
	/// <returns>The collection of elements who tag name is equal to the <paramref name="tagName" /> argument.</returns>
	/// <param name="tagName">The name of the HTML tag for the <see cref="T:System.Windows.Forms.HtmlElement" /> objects you want to retrieve.</param>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection GetElementsByTagName(string tagName)
	{
		IElementCollection elementsByTagName = document.GetElementsByTagName(tagName);
		if (elementsByTagName != null)
		{
			return new HtmlElementCollection(owner, webHost, elementsByTagName);
		}
		return null;
	}

	/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
	public override int GetHashCode()
	{
		if (document == null)
		{
			return 0;
		}
		return document.GetHashCode();
	}

	/// <summary>Executes an Active Scripting function defined in an HTML page.</summary>
	/// <returns>The object returned by the Active Scripting call. </returns>
	/// <param name="scriptName">The name of the script method to invoke.</param>
	/// <filterpriority>1</filterpriority>
	public object InvokeScript(string scriptName)
	{
		return document.InvokeScript("eval ('" + scriptName + "()');");
	}

	/// <summary>Executes an Active Scripting function defined in an HTML page.</summary>
	/// <returns>The object returned by the Active Scripting call. </returns>
	/// <param name="scriptName">The name of the script method to invoke.</param>
	/// <param name="args">The arguments to pass to the script method. </param>
	/// <filterpriority>1</filterpriority>
	public object InvokeScript(string scriptName, object[] args)
	{
		string[] array = new string[args.Length];
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i] is string)
			{
				array[i] = "\"" + args[i].ToString() + "\"";
			}
			else
			{
				array[i] = args[i].ToString();
			}
		}
		return document.InvokeScript("eval ('" + scriptName + "(" + string.Join(",", array) + ")');");
	}

	/// <summary>Gets a new <see cref="T:System.Windows.Forms.HtmlDocument" /> to use with the <see cref="M:System.Windows.Forms.HtmlDocument.Write(System.String)" /> method.</summary>
	/// <returns>A new document for writing.</returns>
	/// <param name="replaceInHistory">Whether the new window's navigation should replace the previous element in the navigation history of the DOM. </param>
	/// <filterpriority>1</filterpriority>
	public HtmlDocument OpenNew(bool replaceInHistory)
	{
		LoadFlags loadFlags = LoadFlags.None;
		if (replaceInHistory)
		{
			loadFlags |= LoadFlags.ReplaceHistory;
		}
		webHost.Navigation.Go("about:blank", loadFlags);
		return this;
	}

	/// <summary>Writes a new HTML page.</summary>
	/// <param name="text">The HTML text to write into the document.</param>
	/// <filterpriority>1</filterpriority>
	public void Write(string text)
	{
		document.Write(text);
	}

	private void OnClick(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Click];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnContextMenuShowing(object sender, ContextMenuEventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[ContextMenuShowing];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs htmlElementEventArgs = new HtmlElementEventArgs();
			htmlElementEventHandler(this, htmlElementEventArgs);
			if (htmlElementEventArgs.ReturnValue)
			{
				owner.OnWebHostContextMenuShown(sender, e);
			}
		}
	}

	private void OnFocusing(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Focusing];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnLosingFocus(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[LosingFocus];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnMouseDown(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseDown];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnMouseLeave(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseLeave];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnMouseMove(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseMove];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnMouseOver(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseOver];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnMouseUp(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseUp];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnStop(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Stop];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private Color ParseColor(string color)
	{
		if (color.IndexOf("#") >= 0)
		{
			return Color.FromArgb(int.Parse(color.Substring(color.IndexOf("#") + 1), NumberStyles.HexNumber));
		}
		return Color.FromName(color);
	}

	/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Windows.Forms.HtmlDocument" /> instances represent the same value. </summary>
	/// <returns>true if the specified instances are equal; otherwise, false.</returns>
	/// <param name="left">The first instance to compare.</param>
	/// <param name="right">The second instance to compare.</param>
	public static bool operator ==(HtmlDocument left, HtmlDocument right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		return left.document.Equals(right.document);
	}

	/// <summary>Returns a value that indicates whether the specified <see cref="T:System.Windows.Forms.HtmlDocument" /> instances do not represent the same value. </summary>
	/// <returns>true if the specified instances are not equal; otherwise, false.</returns>
	/// <param name="left">The first instance to compare.</param>
	/// <param name="right">The second instance to compare.</param>
	public static bool operator !=(HtmlDocument left, HtmlDocument right)
	{
		return !(left == right);
	}
}
