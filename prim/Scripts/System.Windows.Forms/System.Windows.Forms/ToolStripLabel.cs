using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a nonselectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that renders text and images and can display hyperlinks.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
public class ToolStripLabel : ToolStripItem
{
	private Color active_link_color;

	private bool is_link;

	private LinkBehavior link_behavior;

	private Color link_color;

	private bool link_visited;

	private Color visited_link_color;

	private static object UIAIsLinkChangedEvent;

	/// <summary>Gets or sets the color used to display an active link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system. Typically, this color is Color.Red.</returns>
	public Color ActiveLinkColor
	{
		get
		{
			return active_link_color;
		}
		set
		{
			active_link_color = value;
			Invalidate();
		}
	}

	/// <summary>Gets a value indicating the selectable state of a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
	/// <returns>false in all cases.</returns>
	/// <filterpriority>1</filterpriority>
	public override bool CanSelect => false;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> is a hyperlink. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> is a hyperlink; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool IsLink
	{
		get
		{
			return is_link;
		}
		set
		{
			is_link = value;
			Invalidate();
			OnUIAIsLinkChanged(EventArgs.Empty);
		}
	}

	/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is LinkBehavior.SystemDefault.</returns>
	[DefaultValue(LinkBehavior.SystemDefault)]
	public LinkBehavior LinkBehavior
	{
		get
		{
			return link_behavior;
		}
		set
		{
			link_behavior = value;
			Invalidate();
		}
	}

	/// <summary>Gets or sets the color used when displaying a normal link.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system. Typically, this color is Color.Blue.</returns>
	public Color LinkColor
	{
		get
		{
			return link_color;
		}
		set
		{
			link_color = value;
			Invalidate();
		}
	}

	/// <summary>Gets or sets a value indicating whether a link should be displayed as though it were visited.</summary>
	/// <returns>true if links should display as though they were visited; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool LinkVisited
	{
		get
		{
			return link_visited;
		}
		set
		{
			link_visited = value;
			Invalidate();
		}
	}

	/// <summary>Gets or sets the color used when displaying a link that that has been previously visited.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system. Typically, this color is Color.Purple.</returns>
	public Color VisitedLinkColor
	{
		get
		{
			return visited_link_color;
		}
		set
		{
			visited_link_color = value;
			Invalidate();
		}
	}

	internal event EventHandler UIAIsLinkChanged
	{
		add
		{
			base.Events.AddHandler(UIAIsLinkChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAIsLinkChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class.</summary>
	public ToolStripLabel()
		: this(null, null, isLink: false, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the image to display.</summary>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	public ToolStripLabel(Image image)
		: this(null, image, isLink: false, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text to display.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	public ToolStripLabel(string text)
		: this(text, null, isLink: false, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	public ToolStripLabel(string text, Image image)
		: this(text, image, isLink: false, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display and whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="isLink">true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, false. </param>
	public ToolStripLabel(string text, Image image, bool isLink)
		: this(text, image, isLink, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display, whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link, and providing a <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="isLink">true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, false. </param>
	/// <param name="onClick">A <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</param>
	public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick)
		: this(text, image, isLink, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display, whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link, and providing a <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler and name for the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
	/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	/// <param name="isLink">true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, false. </param>
	/// <param name="onClick">A <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
	public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick, string name)
		: base(text, image, onClick, name)
	{
		active_link_color = Color.Red;
		is_link = isLink;
		link_behavior = LinkBehavior.SystemDefault;
		link_color = Color.FromArgb(0, 0, 255);
		link_visited = false;
		visited_link_color = Color.FromArgb(128, 0, 128);
	}

	static ToolStripLabel()
	{
		UIAIsLinkChanged = new object();
	}

	internal void OnUIAIsLinkChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIAIsLinkChanged])?.Invoke(this, e);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		ToolStripItemAccessibleObject toolStripItemAccessibleObject = new ToolStripItemAccessibleObject(this);
		toolStripItemAccessibleObject.role = AccessibleRole.StaticText;
		toolStripItemAccessibleObject.state = AccessibleStates.ReadOnly;
		return toolStripItemAccessibleObject;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (base.Owner == null)
		{
			return;
		}
		Color textColor = ((!Enabled) ? SystemColors.GrayText : ForeColor);
		Image image = ((!Enabled) ? ToolStripRenderer.CreateDisabledImage(Image) : Image);
		base.Owner.Renderer.DrawLabelBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
		CalculateTextAndImageRectangles(out var text_rect, out var image_rect);
		if (base.IsOnDropDown)
		{
			text_rect = ((!base.ShowMargin) ? new Rectangle(7, text_rect.Top, text_rect.Width, text_rect.Height) : new Rectangle(35, text_rect.Top, text_rect.Width, text_rect.Height));
			if (image_rect != Rectangle.Empty)
			{
				image_rect = new Rectangle(new Point(4, 3), GetImageSize());
			}
		}
		if (image_rect != Rectangle.Empty)
		{
			base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, image_rect));
		}
		if (!(text_rect != Rectangle.Empty))
		{
			return;
		}
		if (is_link)
		{
			if (Pressed)
			{
				switch (link_behavior)
				{
				case LinkBehavior.SystemDefault:
				case LinkBehavior.AlwaysUnderline:
				case LinkBehavior.HoverUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, active_link_color, new Font(Font, FontStyle.Underline), TextAlign));
					break;
				case LinkBehavior.NeverUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, active_link_color, Font, TextAlign));
					break;
				}
			}
			else if (Selected)
			{
				switch (link_behavior)
				{
				case LinkBehavior.SystemDefault:
				case LinkBehavior.AlwaysUnderline:
				case LinkBehavior.HoverUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, link_color, new Font(Font, FontStyle.Underline), TextAlign));
					break;
				case LinkBehavior.NeverUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, link_color, Font, TextAlign));
					break;
				}
			}
			else if (link_visited)
			{
				switch (link_behavior)
				{
				case LinkBehavior.SystemDefault:
				case LinkBehavior.AlwaysUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, visited_link_color, new Font(Font, FontStyle.Underline), TextAlign));
					break;
				case LinkBehavior.HoverUnderline:
				case LinkBehavior.NeverUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, visited_link_color, Font, TextAlign));
					break;
				}
			}
			else
			{
				switch (link_behavior)
				{
				case LinkBehavior.SystemDefault:
				case LinkBehavior.AlwaysUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, link_color, new Font(Font, FontStyle.Underline), TextAlign));
					break;
				case LinkBehavior.HoverUnderline:
				case LinkBehavior.NeverUnderline:
					base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, link_color, Font, TextAlign));
					break;
				}
			}
		}
		else
		{
			base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, Text, text_rect, textColor, Font, TextAlign));
		}
	}

	/// <returns>true in all cases.</returns>
	/// <param name="charCode">The character to process. </param>
	protected internal override bool ProcessMnemonic(char charCode)
	{
		base.Parent.SelectNextToolStripItem(this, forward: true);
		return true;
	}
}
