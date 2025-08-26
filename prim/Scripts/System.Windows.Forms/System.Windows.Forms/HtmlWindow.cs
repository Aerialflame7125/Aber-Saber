using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.WebBrowserDialogs;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms;

/// <summary>Represents the logical window that contains one or more instances of <see cref="T:System.Windows.Forms.HtmlDocument" />.</summary>
/// <filterpriority>2</filterpriority>
public sealed class HtmlWindow
{
	private EventHandlerList event_handlers;

	private IWindow window;

	private IWebBrowser webHost;

	private WebBrowser owner;

	private static object ErrorEvent;

	private static object GotFocusEvent;

	private static object LostFocusEvent;

	private static object LoadEvent;

	private static object UnloadEvent;

	private static object ScrollEvent;

	private static object ResizeEvent;

	private EventHandlerList Events
	{
		get
		{
			if (event_handlers == null)
			{
				event_handlers = new EventHandlerList();
			}
			return event_handlers;
		}
	}

	/// <summary>Gets the HTML document contained within the window.</summary>
	/// <returns>A valid instance of <see cref="T:System.Windows.Forms.HtmlDocument" />, if a document is loaded. If this window contains a FRAMESET, or no document is currently loaded, it will return null.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlDocument Document => new HtmlDocument(owner, webHost, window.Document);

	/// <summary>Gets the unmanaged interface wrapped by this class. </summary>
	/// <returns>An object that can be cast to an IHTMLWindow2, IHTMLWindow3, or IHTMLWindow4 pointer.</returns>
	/// <filterpriority>1</filterpriority>
	public object DomWindow
	{
		get
		{
			throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
		}
	}

	/// <summary>Gets a reference to each of the FRAME elements defined within the Web page.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindowCollection" /> of a document's FRAME and IFRAME objects.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlWindowCollection Frames => new HtmlWindowCollection(owner, webHost, window.Frames);

	/// <summary>Gets an object containing the user's most recently visited URLs. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlHistory" />  for the current window.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlHistory History => new HtmlHistory(webHost, window.History);

	/// <summary>Gets a value indicating whether this window is open or closed.</summary>
	/// <returns>true if the window is still open on the screen; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Windows are always open")]
	public bool IsClosed => false;

	/// <summary>Gets or sets the name of the window. </summary>
	/// <returns>A <see cref="T:System.String" /> representing the name. </returns>
	public string Name
	{
		get
		{
			return window.Name;
		}
		set
		{
			window.Name = value;
		}
	}

	/// <summary>Gets a reference to the window that opened the current window. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> that was created by a call to the <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> or <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" /> methods. If the window was not created using one of these methods, this property returns null.</returns>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Separate windows are not supported yet")]
	public HtmlWindow Opener => null;

	/// <summary>Gets the window which resides above the current one in a page containing frames.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlWindow" /> that owns the current window. If the current window is not a FRAME, or is not embedded inside of a FRAME, it returns null.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlWindow Parent => new HtmlWindow(owner, webHost, window.Parent);

	/// <summary>Gets the position of the window's client area on the screen. </summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> describing the x -and y-coordinates of the top-left corner of the screen, in pixels. </returns>
	/// <filterpriority>1</filterpriority>
	public Point Position => owner.Location;

	/// <summary>Gets or sets the size of the current window.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> describing the size of the window in pixels. </returns>
	public Size Size
	{
		get
		{
			return owner.Size;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the text displayed in the status bar of a window.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the current status text.</returns>
	/// <filterpriority>1</filterpriority>
	public string StatusBarText
	{
		get
		{
			return window.StatusText;
		}
		set
		{
		}
	}

	/// <summary>Gets the frame element corresponding to this window.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> corresponding to this window's FRAME element. If this window is not a frame, it returns null. </returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElement WindowFrameElement => new HtmlElement(owner, webHost, window.Document.DocumentElement);

	/// <summary>Gets the URL corresponding to the current item displayed in the window. </summary>
	/// <returns>A <see cref="T:System.Uri" /> describing the URL.</returns>
	public Uri Url => Document.Url;

	/// <summary>Occurs when script running inside of the window encounters a run-time error.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementErrorEventHandler Error
	{
		add
		{
			Events.AddHandler(ErrorEvent, value);
			window.Error += OnError;
		}
		remove
		{
			Events.RemoveHandler(ErrorEvent, value);
			window.Error -= OnError;
		}
	}

	/// <summary>Occurs when the current window obtains user input focus.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler GotFocus
	{
		add
		{
			Events.AddHandler(GotFocusEvent, value);
			window.OnFocus += OnGotFocus;
		}
		remove
		{
			Events.RemoveHandler(GotFocusEvent, value);
			window.OnFocus -= OnGotFocus;
		}
	}

	/// <summary>Occurs when user input focus has left the window.</summary>
	public event HtmlElementEventHandler LostFocus
	{
		add
		{
			Events.AddHandler(LostFocusEvent, value);
			window.OnBlur += OnLostFocus;
		}
		remove
		{
			Events.RemoveHandler(LostFocusEvent, value);
			window.OnBlur -= OnLostFocus;
		}
	}

	/// <summary>Occurs when the window's document and all of its elements have finished initializing.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Load
	{
		add
		{
			Events.AddHandler(LoadEvent, value);
			window.Load += OnLoad;
		}
		remove
		{
			Events.RemoveHandler(LoadEvent, value);
			window.Load -= OnLoad;
		}
	}

	/// <summary>Occurs when the current page is unloading, and a new page is about to be displayed. </summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Unload
	{
		add
		{
			Events.AddHandler(UnloadEvent, value);
			window.Unload += OnUnload;
		}
		remove
		{
			Events.RemoveHandler(UnloadEvent, value);
			window.Unload -= OnUnload;
		}
	}

	/// <summary>Occurs when the user scrolls through the window to view off-screen text. </summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Scroll
	{
		add
		{
			Events.AddHandler(ScrollEvent, value);
			window.Scroll += OnScroll;
		}
		remove
		{
			Events.RemoveHandler(ScrollEvent, value);
			window.Scroll -= OnScroll;
		}
	}

	/// <summary>Occurs when the user uses the mouse to change the dimensions of the window.</summary>
	public event HtmlElementEventHandler Resize
	{
		add
		{
			Events.AddHandler(ResizeEvent, value);
		}
		remove
		{
			Events.RemoveHandler(ResizeEvent, value);
		}
	}

	internal HtmlWindow(WebBrowser owner, IWebBrowser webHost, IWindow iWindow)
	{
		window = iWindow;
		this.webHost = webHost;
		this.owner = owner;
		window.Load += OnLoad;
		window.Unload += OnUnload;
	}

	static HtmlWindow()
	{
		Error = new object();
		GotFocus = new object();
		LostFocus = new object();
		Load = new object();
		Unload = new object();
		Scroll = new object();
		Resize = new object();
	}

	/// <summary>Displays a message box. </summary>
	/// <param name="message">The <see cref="T:System.String" /> to display in the message box.</param>
	/// <filterpriority>1</filterpriority>
	public void Alert(string message)
	{
		MessageBox.Show("Alert", message);
	}

	/// <summary>Displays a dialog box with a message and buttons to solicit a yes/no response.</summary>
	/// <returns>true if the user clicked Yes; false if the user clicked No or closed the dialog box.</returns>
	/// <param name="message">The text to display to the user.</param>
	/// <filterpriority>1</filterpriority>
	public bool Confirm(string message)
	{
		DialogResult dialogResult = MessageBox.Show(message, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
		return dialogResult == DialogResult.OK;
	}

	/// <summary>Shows a dialog box that displays a message and a text box to the user. </summary>
	/// <returns>A <see cref="T:System.String" /> representing the text entered by the user.</returns>
	/// <param name="message">The message to display to the user.</param>
	/// <param name="defaultInputValue">The default value displayed in the text box.</param>
	/// <filterpriority>1</filterpriority>
	public string Prompt(string message, string defaultInputValue)
	{
		Prompt prompt = new Prompt("Prompt", message, defaultInputValue);
		prompt.Show();
		return prompt.Text;
	}

	/// <summary>Displays or downloads the new content located at the specified URL. </summary>
	/// <param name="urlString">The resource to display, described by a Uniform Resource Locator. </param>
	/// <filterpriority>1</filterpriority>
	public void Navigate(string urlString)
	{
		webHost.Navigation.Go(urlString);
	}

	/// <summary>Displays a new document in the current window. </summary>
	/// <param name="url">The location, specified as a <see cref="T:System.Uri" />, of the document or object to display in the current window.</param>
	public void Navigate(Uri url)
	{
		webHost.Navigation.Go(url.ToString());
	}

	/// <summary>Moves the window to the specified coordinates. </summary>
	/// <param name="point">The x- and y-coordinates, relative to the top-left corner of the current window, toward which the page should scroll. </param>
	public void ScrollTo(Point point)
	{
		ScrollTo(point.X, point.Y);
	}

	/// <summary>Scrolls the window to the designated position.</summary>
	/// <param name="x">The x-coordinate, relative to the top-left corner of the current window, toward which the page should scroll.</param>
	/// <param name="y">The y-coordinate, relative to the top-left corner of the current window, toward which the page should scroll.</param>
	/// <filterpriority>1</filterpriority>
	public void ScrollTo(int x, int y)
	{
		window.ScrollTo(x, y);
	}

	/// <summary>Displays a file in the named window.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window, or the previously created window named by the <paramref name="target" /> parameter.</returns>
	/// <param name="url">The Uniform Resource Locator that describes the location of the file to load.</param>
	/// <param name="target">The name of the window in which to open the resource. This can be a developer-supplied name, or one of the following special values:_blank: Opens <paramref name="url" /> in a new window. Works the same as a call to <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" />._media: Opens <paramref name="url" /> in the Media bar. _parent: Opens <paramref name="url" /> in the window that created the current window._search: Opens <paramref name="url" /> in the Search bar._self: Opens <paramref name="url" /> in the current window. _top: If called against a window belonging to a FRAME element, opens <paramref name="url" /> in the window hosting its FRAMESET. Otherwise, acts the same as _self.</param>
	/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. Except for the left, top, height, and width options, which take arbitrary integers, each option accepts yes or 1, and no or 0, as valid values.channelmode: Used with the deprecated channels technology of Internet Explorer 4.0. Default is no.directories: Whether the window should display directory navigation buttons. Default is yes. height: The height of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to The Internet Explorer defaults. left: The left (x-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.location: Whether to display the Address bar, which enables users to navigate the window to a new URL. Default is yes. menubar: Whether to display menus on the new window. Default is yes.resizable: Whether the window can be resized by the user. Default is yes.scrollbars: Whether the window has horizontal and vertical scroll bars. Default is yes.status: Whether the window has a status bar at the bottom. Default is yes.titlebar: Whether the title of the current page is displayed. Setting this option to no has no effect within a managed application; the title bar will always appear.toolbar: Whether toolbar buttons such as Back, Forward, and Stop are visible. Default is yes.top: The top (y-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.width: The width of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to The Internet Explorer defaults.</param>
	/// <param name="replaceEntry">Whether <paramref name="url" /> replaces the current window's URL in the navigation history. This will effect the operation of methods on the <see cref="T:System.Windows.Forms.HtmlHistory" /> class. </param>
	[System.MonoTODO("Blank opens in current window at the moment. Missing media and search implementations. No options implemented")]
	public HtmlWindow Open(Uri url, string target, string windowOptions, bool replaceEntry)
	{
		return Open(url.ToString(), target, windowOptions, replaceEntry);
	}

	/// <summary>Displays a file in the named window.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window, or the previously created window named by the <paramref name="target" /> parameter.</returns>
	/// <param name="urlString">The Uniform Resource Locator that describes the location of the file to load.</param>
	/// <param name="target">The name of the window in which to open the resource. This may be a developer-supplied name, or one of the following special values:_blank: Opens <paramref name="url" /> in a new window. Works the same as a call to <see cref="M:System.Windows.Forms.HtmlWindow.OpenNew(System.String,System.String)" />._media: Opens <paramref name="url" /> in the Media bar. _parent: Opens <paramref name="url" /> in the window that created the current window._search: Opens <paramref name="url" /> in the Search bar._self: Opens <paramref name="url" /> in the current window. _top: If called against a window belonging to a FRAME element, opens <paramref name="url" /> in the window hosting its FRAMESET. Otherwise, acts the same as _self.</param>
	/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. Except for the left, top, height, and width options, which take arbitrary integers, each option accepts yes or 1, and no or 0, as valid values.channelmode: Used with the deprecated channels technology of Internet Explorer 4.0. Default is no.directories: Whether the window should display directory navigation buttons. Default is yes. height: The height of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to the Internet Explorer defaults. left: The left (x-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.location: Whether to display the Address bar, which enables users to navigate the window to a new URL. Default is yes. menubar: Whether to display menus on the new window. Default is yes.resizable: Whether the window can be resized by the user. Default is yes.scrollbars: Whether the window has horizontal and vertical scroll bars. Default is yes.status: Whether the window has a status bar at the bottom. Default is yes.titlebar: Whether the title of the current page is displayed. Setting this option to no has no effect within a managed application; the title bar will always appear.toolbar: Whether toolbar buttons such as Back, Forward, and Stop are visible. Default is yes.top: The top (y-coordinate) position of the window, relative to the upper-left corner of the user's screen, in pixels. Must be a positive integer.width: The width of the window's client area, in pixels. The minimum is 100; attempts to open a window smaller than this will cause the window to open according to the Internet Explorer defaults.</param>
	/// <param name="replaceEntry">Whether <paramref name="url" /> replaces the current window's URL in the navigation history. This will effect the operation of methods on the <see cref="T:System.Windows.Forms.HtmlHistory" /> class.</param>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Blank opens in current window at the moment. Missing media and search implementations. No options implemented")]
	public HtmlWindow Open(string urlString, string target, string windowOptions, bool replaceEntry)
	{
		switch (target)
		{
		case "_blank":
			window.Open(urlString);
			break;
		case "_parent":
			window.Parent.Open(urlString);
			break;
		case "_self":
			window.Open(urlString);
			break;
		case "_top":
			window.Top.Open(urlString);
			break;
		}
		return this;
	}

	/// <summary>Displays a file in a new window.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window. </returns>
	/// <param name="urlString">The Uniform Resource Locator that describes the location of the file to load.</param>
	/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. See <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> for a full description of the valid options. </param>
	/// <filterpriority>1</filterpriority>
	[System.MonoTODO("Opens in current window at the moment.")]
	public HtmlWindow OpenNew(string urlString, string windowOptions)
	{
		return Open(urlString, "_blank", windowOptions, replaceEntry: false);
	}

	/// <summary>Displays a file in a new window.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlWindow" /> representing the new window. </returns>
	/// <param name="url">The Uniform Resource Locator that describes the location of the file to load.</param>
	/// <param name="windowOptions">A comma-delimited string consisting of zero or more of the following options in the form <paramref name="name=value" />. See <see cref="M:System.Windows.Forms.HtmlWindow.Open(System.String,System.String,System.String,System.Boolean)" /> for a full description of the valid options. </param>
	[System.MonoTODO("Opens in current window at the moment.")]
	public HtmlWindow OpenNew(Uri url, string windowOptions)
	{
		return OpenNew(url.ToString(), windowOptions);
	}

	/// <summary>Adds an event handler for the named HTML DOM event.</summary>
	/// <param name="eventName">The name of the event you want to handle.</param>
	/// <param name="eventHandler">A reference to the managed code that handles the event.</param>
	public void AttachEventHandler(string eventName, EventHandler eventHandler)
	{
		window.AttachEventHandler(eventName, eventHandler);
	}

	/// <summary>Closes the window.</summary>
	/// <filterpriority>1</filterpriority>
	public void Close()
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes the named event handler.</summary>
	/// <param name="eventName">The name of the event you want to handle.</param>
	/// <param name="eventHandler">A reference to the managed code that handles the event.</param>
	public void DetachEventHandler(string eventName, EventHandler eventHandler)
	{
		window.DetachEventHandler(eventName, eventHandler);
	}

	/// <summary>Puts the focus on the current window.</summary>
	public void Focus()
	{
		window.Focus();
	}

	/// <summary>Moves the window to the specified coordinates on the screen. </summary>
	/// <param name="point">The x- and y-coordinates of the window's upper-left corner. </param>
	/// <exception cref="T:System.UnauthorizedAccessException">The code trying to execute this operation does not have permission to manipulate this window. See the Remarks section for details.</exception>
	public void MoveTo(Point point)
	{
		throw new NotImplementedException();
	}

	/// <summary>Moves the window to the specified coordinates on the screen. </summary>
	/// <param name="x">The x-coordinate of the window's upper-left corner.</param>
	/// <param name="y">The y-coordinate of the window's upper-left corner.</param>
	/// <exception cref="T:System.UnauthorizedAccessException">The code trying to execute this operation does not have permission to manipulate this window. See the Remarks section for details.</exception>
	/// <filterpriority>1</filterpriority>
	public void MoveTo(int x, int y)
	{
		throw new NotImplementedException();
	}

	/// <summary>Takes focus off of the current window. </summary>
	/// <filterpriority>1</filterpriority>
	public void RemoveFocus()
	{
		webHost.FocusOut();
	}

	/// <summary>Changes the size of the window to the specified dimensions. </summary>
	/// <param name="size">A <see cref="T:System.Drawing.Size" /> describing the desired width and height of the window, in pixels. Must be 100 pixels or more in both dimensions. </param>
	/// <exception cref="T:System.UnauthorizedAccessException">The window you are trying to resize is in a different domain than its parent window. This restriction is part of cross-frame scripting security; for more information, see About Cross-Frame Scripting and Security.</exception>
	public void ResizeTo(Size size)
	{
		throw new NotImplementedException();
	}

	/// <summary>Changes the size of the window to the specified dimensions. </summary>
	/// <param name="width">Describes the desired width of the window, in pixels. Must be 100 pixels or more.</param>
	/// <param name="height">Describes the desired height of the window, in pixels. Must be 100 pixels or more.</param>
	/// <exception cref="T:System.UnauthorizedAccessException">The window you are trying to resize is in a different domain than its parent window. This restriction is part of cross-frame scripting security; for more information, see About Cross-Frame Scripting and Security.</exception>
	/// <filterpriority>1</filterpriority>
	public void ResizeTo(int width, int height)
	{
		throw new NotImplementedException();
	}

	internal void OnError(object sender, EventArgs ev)
	{
		HtmlElementErrorEventHandler htmlElementErrorEventHandler = (HtmlElementErrorEventHandler)Events[Error];
		if (htmlElementErrorEventHandler != null)
		{
			HtmlElementErrorEventArgs e = new HtmlElementErrorEventArgs(string.Empty, 0, null);
			htmlElementErrorEventHandler(this, e);
		}
	}

	internal void OnGotFocus(object sender, EventArgs ev)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[GotFocus];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e);
		}
	}

	internal void OnLostFocus(object sender, EventArgs ev)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[LostFocus];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e);
		}
	}

	internal void OnLoad(object sender, EventArgs ev)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Load];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e);
		}
	}

	internal void OnUnload(object sender, EventArgs ev)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Unload];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e);
		}
	}

	internal void OnScroll(object sender, EventArgs ev)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Scroll];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e);
		}
	}

	internal void OnResize(object sender, EventArgs ev)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Resize];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e);
		}
	}

	/// <returns>System.Int32</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		if (window == null)
		{
			return 0;
		}
		return window.GetHashCode();
	}

	/// <summary>Tests the object for equality against the current object.</summary>
	/// <returns>true if the objects are equal; otherwise, false.</returns>
	/// <param name="obj">The object to test.</param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object obj)
	{
		return this == (HtmlWindow)obj;
	}

	/// <summary>Tests the two <see cref="T:System.Windows.Forms.HtmlWindow" /> objects for equality.</summary>
	/// <returns>true if both parameters are null, or if both elements have the same underlying COM interface; otherwise, false.</returns>
	/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
	/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
	/// <filterpriority>3</filterpriority>
	public static bool operator ==(HtmlWindow left, HtmlWindow right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		return left.window.Equals(right.window);
	}

	/// <summary>Tests two HtmlWindow objects for inequality.</summary>
	/// <returns>true if one but not both of the objects is null, or the underlying COM pointers do not match; otherwise, false.</returns>
	/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
	/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlWindow" /> object.</param>
	/// <filterpriority>3</filterpriority>
	public static bool operator !=(HtmlWindow left, HtmlWindow right)
	{
		return !(left == right);
	}
}
