using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class ToolStripItemTextRenderEventArgs : ToolStripItemRenderEventArgs
{
	private string text;

	private Color text_color;

	private ToolStripTextDirection text_direction;

	private Font text_font;

	private TextFormatFlags text_format;

	private Rectangle text_rectangle;

	/// <summary>Gets or sets the text to be drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A string that represents the text to be painted on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text. </summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text.</returns>
	/// <filterpriority>1</filterpriority>
	public Color TextColor
	{
		get
		{
			return text_color;
		}
		set
		{
			text_color = value;
		}
	}

	/// <summary>Gets or sets whether the text is drawn vertically or horizontally.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. </returns>
	/// <filterpriority>1</filterpriority>
	public ToolStripTextDirection TextDirection
	{
		get
		{
			return text_direction;
		}
		set
		{
			text_direction = value;
		}
	}

	/// <summary>Gets or sets the font of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Font TextFont
	{
		get
		{
			return text_font;
		}
		set
		{
			text_font = value;
		}
	}

	/// <summary>Gets or sets the display and layout information of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values that specify the display and layout information of the drawn text. </returns>
	/// <filterpriority>1</filterpriority>
	public TextFormatFlags TextFormat
	{
		get
		{
			return text_format;
		}
		set
		{
			text_format = value;
		}
	}

	/// <summary>Gets or sets the rectangle that represents the bounds to draw the text in.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle TextRectangle
	{
		get
		{
			return text_rectangle;
		}
		set
		{
			text_rectangle = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties. </summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
	/// <param name="text">The text to be drawn.</param>
	/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
	/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
	/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
	/// <param name="textAlign">The <see cref="T:System.Drawing.ContentAlignment" /> that specifies the vertical and horizontal alignment of the text in the bounding area.</param>
	public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, ContentAlignment textAlign)
		: base(g, item)
	{
		this.text = text;
		text_rectangle = textRectangle;
		text_color = textColor;
		text_font = textFont;
		text_direction = item.TextDirection;
		switch (textAlign)
		{
		case ContentAlignment.BottomCenter:
			text_format = TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
			break;
		case ContentAlignment.BottomLeft:
			text_format = TextFormatFlags.Bottom;
			break;
		case ContentAlignment.BottomRight:
			text_format = TextFormatFlags.Right | TextFormatFlags.Bottom;
			break;
		case ContentAlignment.MiddleCenter:
			text_format = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
			break;
		default:
			text_format = TextFormatFlags.VerticalCenter;
			break;
		case ContentAlignment.MiddleRight:
			text_format = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
			break;
		case ContentAlignment.TopCenter:
			text_format = TextFormatFlags.HorizontalCenter;
			break;
		case ContentAlignment.TopLeft:
			text_format = TextFormatFlags.Left;
			break;
		case ContentAlignment.TopRight:
			text_format = TextFormatFlags.Right;
			break;
		}
		if ((Application.KeyboardCapture == null || !ToolStripManager.ActivatedByKeyboard) && !SystemInformation.MenuAccessKeysUnderlined)
		{
			text_format |= TextFormatFlags.HidePrefix;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties format.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
	/// <param name="text">The text to be drawn.</param>
	/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
	/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
	/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
	/// <param name="format">The display and layout information for text strings.</param>
	public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, TextFormatFlags format)
		: base(g, item)
	{
		this.text = text;
		text_rectangle = textRectangle;
		text_color = textColor;
		text_font = textFont;
		text_format = format;
		text_direction = ToolStripTextDirection.Horizontal;
	}
}
