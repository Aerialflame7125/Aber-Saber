using System.ComponentModel;
using System.Drawing;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace System.Windows.Forms;

/// <summary>Represents an HTML element inside of a Web page. </summary>
/// <filterpriority>1</filterpriority>
public sealed class HtmlElement
{
	private EventHandlerList events;

	private IWebBrowser webHost;

	internal IElement element;

	private WebBrowser owner;

	private static object ClickEvent;

	private static object DoubleClickEvent;

	private static object MouseDownEvent;

	private static object MouseUpEvent;

	private static object MouseMoveEvent;

	private static object MouseOverEvent;

	private static object MouseEnterEvent;

	private static object MouseLeaveEvent;

	private static object KeyDownEvent;

	private static object KeyPressEvent;

	private static object KeyUpEvent;

	private static object DragEvent;

	private static object DragEndEvent;

	private static object DragLeaveEvent;

	private static object DragOverEvent;

	private static object FocusingEvent;

	private static object GotFocusEvent;

	private static object LosingFocusEvent;

	private static object LostFocusEvent;

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

	/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all elements underneath the current element. </summary>
	/// <returns>A collection of all elements that are direct or indirect children of the current element. If the current element is a TABLE, for example, <see cref="P:System.Windows.Forms.HtmlElement.All" /> will return every TH, TR, and TD element within the table, as well as any other elements, such as DIV and SPAN elements, contained within the cells. </returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection All => new HtmlElementCollection(owner, webHost, element.All);

	/// <summary>Gets a value indicating whether this element can have child elements.</summary>
	/// <returns>true if element can have child elements; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool CanHaveChildren
	{
		get
		{
			string tagName = TagName;
			switch (tagName.ToLowerInvariant())
			{
			case "area":
			case "base":
			case "basefont":
			case "br":
			case "col":
			case "frame":
			case "hr":
			case "img":
			case "input":
			case "isindex":
			case "link":
			case "meta":
			case "param":
				return false;
			default:
				return true;
			}
		}
	}

	/// <summary>Gets an <see cref="T:System.Windows.Forms.HtmlElementCollection" /> of all children of the current element.</summary>
	/// <returns>A collection of all <see cref="T:System.Windows.Forms.HtmlElement" /> objects that have the current element as a parent.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection Children => new HtmlElementCollection(owner, webHost, element.Children);

	/// <summary>Gets the bounds of the client area of the element in the HTML document.</summary>
	/// <returns>The client area occupied by the element, minus any area taken by borders and scroll bars. To obtain the position and dimensions of the element inclusive of its adornments, use <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> instead.</returns>
	/// <filterpriority>2</filterpriority>
	public Rectangle ClientRectangle => new Rectangle(0, 0, element.ClientWidth, element.ClientHeight);

	/// <summary>Gets the location of an element relative to its parent.</summary>
	/// <returns>The x- and y-coordinate positions of the element, and its width and its height, in relation to its parent. If an element's parent is relatively or absolutely positioned, <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> will return the offset of the parent element. If the element itself is relatively positioned with respect to its parent, <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> will return the offset from its parent.</returns>
	/// <filterpriority>2</filterpriority>
	public Rectangle OffsetRectangle => new Rectangle(element.OffsetLeft, element.OffsetTop, element.OffsetWidth, element.OffsetHeight);

	/// <summary>Gets the dimensions of an element's scrollable region.</summary>
	/// <returns>The size and coordinate location of the scrollable area of an element.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle ScrollRectangle => new Rectangle(element.ScrollLeft, element.ScrollTop, element.ScrollWidth, element.ScrollHeight);

	/// <summary>Gets or sets the distance between the edge of the element and the left edge of its content.</summary>
	/// <returns>The distance, in pixels, between the left edge of the element and the left edge of its content.</returns>
	/// <filterpriority>1</filterpriority>
	public int ScrollLeft
	{
		get
		{
			return element.ScrollLeft;
		}
		set
		{
			element.ScrollLeft = value;
		}
	}

	/// <summary>Gets or sets the distance between the edge of the element and the top edge of its content.</summary>
	/// <returns>The distance, in pixels, between the top edge of the element and the top edge of its content.</returns>
	/// <filterpriority>1</filterpriority>
	public int ScrollTop
	{
		get
		{
			return element.ScrollTop;
		}
		set
		{
			element.ScrollTop = value;
		}
	}

	/// <summary>Gets the element from which <see cref="P:System.Windows.Forms.HtmlElement.OffsetRectangle" /> is calculated.</summary>
	/// <returns>The element from which the offsets are calculated.If an element's parent or another element in the element's hierarchy uses relative or absolute positioning, OffsetParent will be the first relatively or absolutely positioned element in which the current element is nested. If none of the elements above the current element are absolutely or relatively positioned, OffsetParent will be the BODY tag of the document. </returns>
	/// <filterpriority>2</filterpriority>
	public HtmlElement OffsetParent => new HtmlElement(owner, webHost, element.OffsetParent);

	/// <summary>Gets the <see cref="T:System.Windows.Forms.HtmlDocument" /> to which this element belongs.</summary>
	/// <returns>The parent document of this element.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlDocument Document => new HtmlDocument(owner, webHost, element.Owner);

	/// <summary>Gets or sets whether the user can input data into this element.</summary>
	/// <returns>true if the element allows user input; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool Enabled
	{
		get
		{
			return !element.Disabled;
		}
		set
		{
			element.Disabled = !value;
		}
	}

	/// <summary>Gets or sets the HTML markup underneath this element.</summary>
	/// <returns>The HTML markup that defines the child elements of the current element.</returns>
	/// <exception cref="T:System.NotSupportedException">Creating child elements on this element is not allowed. </exception>
	/// <filterpriority>1</filterpriority>
	public string InnerHtml
	{
		get
		{
			return element.InnerHTML;
		}
		set
		{
			element.InnerHTML = value;
		}
	}

	/// <summary>Gets or sets the text assigned to the element.</summary>
	/// <returns>The element's text, absent any HTML markup. If the element contains child elements, only the text in those child elements will be preserved. </returns>
	/// <exception cref="T:System.NotSupportedException">The specified element cannot contain text (for example, an IMG element). </exception>
	/// <filterpriority>1</filterpriority>
	public string InnerText
	{
		get
		{
			return element.InnerText;
		}
		set
		{
			element.InnerText = value;
		}
	}

	/// <summary>Gets or sets a label by which to identify the element.</summary>
	/// <returns>The unique identifier for the element. </returns>
	/// <filterpriority>1</filterpriority>
	public string Id
	{
		get
		{
			return GetAttribute("id");
		}
		set
		{
			SetAttribute("id", value);
		}
	}

	/// <summary>Gets or sets the name of the element. </summary>
	/// <returns>A <see cref="T:System.String" /> representing the element's name.</returns>
	public string Name
	{
		get
		{
			return GetAttribute("name");
		}
		set
		{
			SetAttribute("name", value);
		}
	}

	/// <summary>Gets the next element below this element in the document tree. </summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> representing the first element contained underneath the current element, in source order.</returns>
	public HtmlElement FirstChild => new HtmlElement(owner, webHost, (IElement)element.FirstChild);

	/// <summary>Gets the next element at the same level as this element in the document tree. </summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElement" /> representing the element to the right of the current element. </returns>
	public HtmlElement NextSibling => new HtmlElement(owner, webHost, (IElement)element.Next);

	/// <summary>Gets the current element's parent element.</summary>
	/// <returns>The element above the current element in the HTML document's hierarchy.</returns>
	/// <filterpriority>1</filterpriority>
	public HtmlElement Parent => new HtmlElement(owner, webHost, (IElement)element.Parent);

	/// <summary>Gets the name of the HTML tag.</summary>
	/// <returns>The name used to create this element using HTML markup.</returns>
	/// <filterpriority>1</filterpriority>
	public string TagName => element.TagName;

	/// <summary>Gets or sets the location of this element in the tab order.</summary>
	/// <returns>The numeric index of the element in the tab order.</returns>
	/// <filterpriority>1</filterpriority>
	public short TabIndex
	{
		get
		{
			return (short)element.TabIndex;
		}
		set
		{
			element.TabIndex = value;
		}
	}

	/// <summary>Gets an unmanaged interface pointer for this element.</summary>
	/// <returns>The COM IUnknown pointer for the element, which you can cast to one of the HTML element interfaces, such as IHTMLElement.</returns>
	/// <filterpriority>1</filterpriority>
	public object DomElement
	{
		get
		{
			throw new NotSupportedException("Retrieving a reference to an mshtml interface is not supported. Sorry.");
		}
	}

	/// <summary>Gets or sets the current element's HTML code. </summary>
	/// <returns>The HTML code for the current element and its children.</returns>
	/// <filterpriority>1</filterpriority>
	public string OuterHtml
	{
		get
		{
			return element.OuterHTML;
		}
		set
		{
			element.OuterHTML = value;
		}
	}

	/// <summary>Gets or sets the current element's text. </summary>
	/// <returns>The text inside the current element, and in the element's children. </returns>
	/// <exception cref="T:System.NotSupportedException">You cannot set text outside of this element.</exception>
	/// <filterpriority>1</filterpriority>
	public string OuterText
	{
		get
		{
			return element.OuterText;
		}
		set
		{
			element.OuterText = value;
		}
	}

	/// <summary>Gets or sets a comma-delimited list of styles for the current element. </summary>
	/// <returns>A string consisting of all of the element's styles</returns>
	/// <filterpriority>1</filterpriority>
	public string Style
	{
		get
		{
			return element.Style;
		}
		set
		{
			element.Style = value;
		}
	}

	/// <summary>Occurs when the user clicks on the element with the left mouse button. </summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Click
	{
		add
		{
			Events.AddHandler(ClickEvent, value);
			element.Click += OnClick;
		}
		remove
		{
			Events.RemoveHandler(ClickEvent, value);
			element.Click -= OnClick;
		}
	}

	/// <summary>Occurs when the user clicks the left mouse button over an element twice, in rapid succession.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler DoubleClick
	{
		add
		{
			Events.AddHandler(DoubleClickEvent, value);
			element.DoubleClick += OnDoubleClick;
		}
		remove
		{
			Events.RemoveHandler(DoubleClickEvent, value);
			element.DoubleClick -= OnDoubleClick;
		}
	}

	/// <summary>Occurs when the user presses a mouse button.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseDown
	{
		add
		{
			Events.AddHandler(MouseDownEvent, value);
			element.MouseDown += OnMouseDown;
		}
		remove
		{
			Events.RemoveHandler(MouseDownEvent, value);
			element.MouseDown -= OnMouseDown;
		}
	}

	/// <summary>Occurs when the user releases a mouse button.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseUp
	{
		add
		{
			Events.AddHandler(MouseUpEvent, value);
			element.MouseUp += OnMouseUp;
		}
		remove
		{
			Events.RemoveHandler(MouseUpEvent, value);
			element.MouseUp -= OnMouseUp;
		}
	}

	/// <summary>Occurs when the user moves the mouse cursor across the element.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseMove
	{
		add
		{
			Events.AddHandler(MouseMoveEvent, value);
			element.MouseMove += OnMouseMove;
		}
		remove
		{
			Events.RemoveHandler(MouseMoveEvent, value);
			element.MouseMove -= OnMouseMove;
		}
	}

	/// <summary>Occurs when the mouse cursor enters the bounds of the element.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler MouseOver
	{
		add
		{
			Events.AddHandler(MouseOverEvent, value);
			element.MouseOver += OnMouseOver;
		}
		remove
		{
			Events.RemoveHandler(MouseOverEvent, value);
			element.MouseOver -= OnMouseOver;
		}
	}

	/// <summary>Occurs when the user first moves the mouse cursor over the current element. </summary>
	public event HtmlElementEventHandler MouseEnter
	{
		add
		{
			Events.AddHandler(MouseEnterEvent, value);
			element.MouseEnter += OnMouseEnter;
		}
		remove
		{
			Events.RemoveHandler(MouseEnterEvent, value);
			element.MouseEnter -= OnMouseEnter;
		}
	}

	/// <summary>Occurs when the user moves the mouse cursor off of the current element. </summary>
	public event HtmlElementEventHandler MouseLeave
	{
		add
		{
			Events.AddHandler(MouseLeaveEvent, value);
			element.MouseLeave += OnMouseLeave;
		}
		remove
		{
			Events.RemoveHandler(MouseLeaveEvent, value);
			element.MouseLeave -= OnMouseLeave;
		}
	}

	/// <summary>Occurs when the user presses a key on the keyboard.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler KeyDown
	{
		add
		{
			Events.AddHandler(KeyDownEvent, value);
			element.KeyDown += OnKeyDown;
		}
		remove
		{
			Events.RemoveHandler(KeyDownEvent, value);
			element.KeyDown -= OnKeyDown;
		}
	}

	/// <summary>Occurs when the user presses and releases a key on the keyboard.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler KeyPress
	{
		add
		{
			Events.AddHandler(KeyPressEvent, value);
			element.KeyPress += OnKeyPress;
		}
		remove
		{
			Events.RemoveHandler(KeyPressEvent, value);
			element.KeyPress -= OnKeyPress;
		}
	}

	/// <summary>Occurs when the user releases a key on the keyboard.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler KeyUp
	{
		add
		{
			Events.AddHandler(KeyUpEvent, value);
			element.KeyUp += OnKeyUp;
		}
		remove
		{
			Events.RemoveHandler(KeyUpEvent, value);
			element.KeyUp -= OnKeyUp;
		}
	}

	/// <summary>Occurs when the user drags text to various locations. </summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler Drag
	{
		add
		{
			Events.AddHandler(DragEvent, value);
		}
		remove
		{
			Events.RemoveHandler(DragEvent, value);
		}
	}

	/// <summary>Occurs when a user finishes a drag operation.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler DragEnd
	{
		add
		{
			Events.AddHandler(DragEndEvent, value);
		}
		remove
		{
			Events.RemoveHandler(DragEndEvent, value);
		}
	}

	/// <summary>Occurs when the user is no longer dragging an item over this element. </summary>
	public event HtmlElementEventHandler DragLeave
	{
		add
		{
			Events.AddHandler(DragLeaveEvent, value);
		}
		remove
		{
			Events.RemoveHandler(DragLeaveEvent, value);
		}
	}

	/// <summary>Occurs when the user drags text over the element.</summary>
	/// <filterpriority>1</filterpriority>
	public event HtmlElementEventHandler DragOver
	{
		add
		{
			Events.AddHandler(DragOverEvent, value);
		}
		remove
		{
			Events.RemoveHandler(DragOverEvent, value);
		}
	}

	/// <summary>Occurs when the element first receives user input focus. </summary>
	public event HtmlElementEventHandler Focusing
	{
		add
		{
			Events.AddHandler(FocusingEvent, value);
			element.OnFocus += OnFocusing;
		}
		remove
		{
			Events.RemoveHandler(FocusingEvent, value);
			element.OnFocus -= OnFocusing;
		}
	}

	/// <summary>Occurs when the element has received user input focus.</summary>
	public event HtmlElementEventHandler GotFocus
	{
		add
		{
			Events.AddHandler(GotFocusEvent, value);
			element.OnFocus += OnGotFocus;
		}
		remove
		{
			Events.RemoveHandler(GotFocusEvent, value);
			element.OnFocus -= OnGotFocus;
		}
	}

	/// <summary>Occurs when the element is losing user input focus. </summary>
	public event HtmlElementEventHandler LosingFocus
	{
		add
		{
			Events.AddHandler(LosingFocusEvent, value);
			element.OnBlur += OnLosingFocus;
		}
		remove
		{
			Events.RemoveHandler(LosingFocusEvent, value);
			element.OnBlur -= OnLosingFocus;
		}
	}

	/// <summary>Occurs when the element has lost user input focus. </summary>
	public event HtmlElementEventHandler LostFocus
	{
		add
		{
			Events.AddHandler(LostFocusEvent, value);
			element.OnBlur += OnLostFocus;
		}
		remove
		{
			Events.RemoveHandler(LostFocusEvent, value);
			element.OnBlur -= OnLostFocus;
		}
	}

	internal HtmlElement(WebBrowser owner, IWebBrowser webHost, IElement element)
	{
		this.webHost = webHost;
		this.element = element;
		this.owner = owner;
	}

	static HtmlElement()
	{
		Click = new object();
		DoubleClick = new object();
		MouseDown = new object();
		MouseUp = new object();
		MouseMove = new object();
		MouseOver = new object();
		MouseEnter = new object();
		MouseLeave = new object();
		KeyDown = new object();
		KeyPress = new object();
		KeyUp = new object();
		Drag = new object();
		DragEnd = new object();
		DragLeave = new object();
		DragOver = new object();
		Focusing = new object();
		GotFocus = new object();
		LosingFocus = new object();
		LostFocus = new object();
	}

	/// <summary>Adds an element to another element's subtree.</summary>
	/// <returns>The element after it has been added to the tree. </returns>
	/// <param name="newElement">The <see cref="T:System.Windows.Forms.HtmlElement" /> to append to this location in the tree. </param>
	/// <filterpriority>1</filterpriority>
	public HtmlElement AppendChild(HtmlElement newElement)
	{
		IElement element = this.element.AppendChild(newElement.element);
		newElement.element = element;
		return newElement;
	}

	/// <summary>Adds an event handler for a named event on the HTML Document Object Model (DOM).</summary>
	/// <param name="eventName">The name of the event you want to handle.</param>
	/// <param name="eventHandler">The managed code that handles the event.</param>
	public void AttachEventHandler(string eventName, EventHandler eventHandler)
	{
		element.AttachEventHandler(eventName, eventHandler);
	}

	/// <summary>Removes an event handler from a named event on the HTML Document Object Model (DOM).</summary>
	/// <param name="eventName">The name of the event you want to handle.</param>
	/// <param name="eventHandler">The managed code that handles the event.</param>
	public void DetachEventHandler(string eventName, EventHandler eventHandler)
	{
		element.DetachEventHandler(eventName, eventHandler);
	}

	/// <summary>Puts user input focus on the current element.</summary>
	public void Focus()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the value of the named attribute on the element.</summary>
	/// <returns>The value of this attribute on the element, as a <see cref="T:System.String" /> value. If the specified attribute does not exist on this element, returns an empty string.</returns>
	/// <param name="attributeName">The name of the attribute. This argument is case-insensitive.</param>
	/// <filterpriority>1</filterpriority>
	public string GetAttribute(string attributeName)
	{
		return element.GetAttribute(attributeName);
	}

	/// <summary>Retrieves a collection of elements represented in HTML by the specified HTML tag.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.HtmlElementCollection" /> containing all elements whose HTML tag name is equal to <paramref name="tagName" />.</returns>
	/// <param name="tagName">The name of the tag whose <see cref="T:System.Windows.Forms.HtmlElement" /> objects you wish to retrieve.</param>
	/// <filterpriority>1</filterpriority>
	public HtmlElementCollection GetElementsByTagName(string tagName)
	{
		IElementCollection elementsByTagName = element.GetElementsByTagName(tagName);
		return new HtmlElementCollection(owner, webHost, elementsByTagName);
	}

	/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		if (element == null)
		{
			return 0;
		}
		return element.GetHashCode();
	}

	internal bool HasAttribute(string name)
	{
		return element.HasAttribute(name);
	}

	/// <summary>Insert a new element into the Document Object Model (DOM).</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.HtmlElement" /> that was just inserted. If insertion failed, this will return null.</returns>
	/// <param name="orient">Where to insert this element in relation to the current element.</param>
	/// <param name="newElement">The new element to insert.</param>
	/// <filterpriority>2</filterpriority>
	public HtmlElement InsertAdjacentElement(HtmlElementInsertionOrientation orient, HtmlElement newElement)
	{
		switch (orient)
		{
		case HtmlElementInsertionOrientation.BeforeBegin:
			element.Parent.InsertBefore(newElement.element, element);
			return newElement;
		case HtmlElementInsertionOrientation.AfterBegin:
			element.InsertBefore(newElement.element, element.FirstChild);
			return newElement;
		case HtmlElementInsertionOrientation.BeforeEnd:
			return AppendChild(newElement);
		case HtmlElementInsertionOrientation.AfterEnd:
			return AppendChild(newElement);
		default:
			return null;
		}
	}

	/// <summary>Executes an unexposed method on the underlying DOM element of this element.</summary>
	/// <returns>The element returned by this method, represented as an <see cref="T:System.Object" />. If this <see cref="T:System.Object" /> is another HTML element, and you have a reference to the unmanaged MSHTML library added to your project, you can cast it to its appropriate unmanaged interface.</returns>
	/// <param name="methodName">The name of the property or method to invoke. </param>
	/// <filterpriority>2</filterpriority>
	public object InvokeMember(string methodName)
	{
		return element.Owner.InvokeScript("eval ('" + methodName + "()');");
	}

	/// <summary>Executes a function defined in the current HTML page by a scripting language.</summary>
	/// <returns>The element returned by the function, represented as an <see cref="T:System.Object" />. If this <see cref="T:System.Object" /> is another HTML element, and you have a reference to the unmanaged MSHTML library added to your project, you can cast it to its appropriate unmanaged interface.</returns>
	/// <param name="methodName">The name of the property or method to invoke.</param>
	/// <param name="parameter"></param>
	/// <filterpriority>2</filterpriority>
	public object InvokeMember(string methodName, params object[] parameter)
	{
		string[] array = new string[parameter.Length];
		for (int i = 0; i < parameter.Length; i++)
		{
			array[i] = parameter.ToString();
		}
		return element.Owner.InvokeScript("eval ('" + methodName + "(" + string.Join(",", array) + ")');");
	}

	/// <summary>Causes the named event to call all registered event handlers. </summary>
	/// <param name="eventName">The name of the event to raise. </param>
	public void RaiseEvent(string eventName)
	{
		element.FireEvent(eventName);
	}

	/// <summary>Removes focus from the current element, if that element has focus. </summary>
	/// <filterpriority>1</filterpriority>
	public void RemoveFocus()
	{
		element.Blur();
	}

	/// <summary>Scrolls through the document containing this element until the top or bottom edge of this element is aligned with the document's window. </summary>
	/// <param name="alignWithTop">If true, the top of the object will be displayed at the top of the window. If false, the bottom of the object will be displayed at the bottom of the window.</param>
	/// <filterpriority>1</filterpriority>
	public void ScrollIntoView(bool alignWithTop)
	{
		element.ScrollIntoView(alignWithTop);
	}

	/// <summary>Sets the value of the named attribute on the element.</summary>
	/// <param name="attributeName">The name of the attribute to set.</param>
	/// <param name="value">The new value of this attribute. </param>
	/// <filterpriority>1</filterpriority>
	public void SetAttribute(string attributeName, string value)
	{
		element.SetAttribute(attributeName, value);
	}

	/// <summary>Tests if the supplied object is equal to the current element.</summary>
	/// <returns>true if <paramref name="obj" /> is an <see cref="T:System.Windows.Forms.HtmlElement" />; otherwise, false.</returns>
	/// <param name="obj">The object to test for equality.</param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object obj)
	{
		return this == (HtmlElement)obj;
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

	private void OnDoubleClick(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[DoubleClick];
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

	private void OnMouseUp(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseUp];
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

	private void OnMouseEnter(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[MouseEnter];
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

	private void OnKeyDown(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[KeyDown];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnKeyPress(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[KeyPress];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnKeyUp(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[KeyUp];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnDrag(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[Drag];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnDragEnd(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[DragEnd];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnDragLeave(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[DragLeave];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	private void OnDragOver(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[DragOver];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
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

	private void OnGotFocus(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[GotFocus];
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

	private void OnLostFocus(object sender, EventArgs e)
	{
		HtmlElementEventHandler htmlElementEventHandler = (HtmlElementEventHandler)Events[LostFocus];
		if (htmlElementEventHandler != null)
		{
			HtmlElementEventArgs e2 = new HtmlElementEventArgs();
			htmlElementEventHandler(this, e2);
		}
	}

	/// <summary>Compares two elements for equality.</summary>
	/// <returns>true if both parameters are null, or if both elements have the same underlying COM interface; otherwise, false.</returns>
	/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
	/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
	/// <filterpriority>3</filterpriority>
	public static bool operator ==(HtmlElement left, HtmlElement right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		return left.element.Equals(right.element);
	}

	/// <summary>Compares two <see cref="T:System.Windows.Forms.HtmlElement" /> objects for inequality.</summary>
	/// <returns>true is only one element is null, or the two objects are not equal; otherwise, false. </returns>
	/// <param name="left">The first <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
	/// <param name="right">The second <see cref="T:System.Windows.Forms.HtmlElement" />.</param>
	/// <filterpriority>3</filterpriority>
	public static bool operator !=(HtmlElement left, HtmlElement right)
	{
		return !(left == right);
	}
}
