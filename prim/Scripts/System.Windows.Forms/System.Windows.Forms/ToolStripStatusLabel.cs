using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a panel in a <see cref="T:System.Windows.Forms.StatusStrip" /> control. </summary>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
public class ToolStripStatusLabel : ToolStripLabel
{
	private ToolStripStatusLabelBorderSides border_sides;

	private Border3DStyle border_style;

	private bool spring;

	/// <summary>Gets or sets a value that determines where the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> is aligned on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public new ToolStripItemAlignment Alignment
	{
		get
		{
			return base.Alignment;
		}
		set
		{
			base.Alignment = value;
		}
	}

	/// <summary>Gets or sets a value that indicates which sides of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> show borders.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripStatusLabelBorderSides" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripStatusLabelBorderSides.None" />.</returns>
	[DefaultValue(ToolStripStatusLabelBorderSides.None)]
	public ToolStripStatusLabelBorderSides BorderSides
	{
		get
		{
			return border_sides;
		}
		set
		{
			border_sides = value;
		}
	}

	/// <summary>Gets or sets the border style of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values. The default is <see cref="F:System.Windows.Forms.Border3DStyle.Flat" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="P:System.Windows.Forms.ToolStripStatusLabel.BorderStyle" /> is not one of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values.</exception>
	[DefaultValue(Border3DStyle.Flat)]
	public Border3DStyle BorderStyle
	{
		get
		{
			return border_style;
		}
		set
		{
			border_style = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> automatically fills the available space on the <see cref="T:System.Windows.Forms.StatusStrip" /> as the form is resized. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> automatically fills the available space on the <see cref="T:System.Windows.Forms.StatusStrip" /> as the form is resized; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool Spring
	{
		get
		{
			return spring;
		}
		set
		{
			if (spring != value)
			{
				spring = value;
				CalculateAutoSize();
			}
		}
	}

	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
	protected internal override Padding DefaultMargin => new Padding(0, 3, 0, 2);

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class. </summary>
	public ToolStripStatusLabel()
		: this(string.Empty, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image. </summary>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	public ToolStripStatusLabel(Image image)
		: this(string.Empty, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified text.</summary>
	/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	public ToolStripStatusLabel(string text)
		: this(text, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image and text.</summary>
	/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	public ToolStripStatusLabel(string text, Image image)
		: this(text, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image and text, and that carries out the specified action when the user clicks the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
	/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	/// <param name="onClick">Specifies the action to carry out when the control is clicked.</param>
	public ToolStripStatusLabel(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class with the specified name that displays the specified image and text, and that carries out the specified action when the user clicks the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
	/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	/// <param name="onClick">Specifies the action to carry out when the control is clicked.</param>
	/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
	public ToolStripStatusLabel(string text, Image image, EventHandler onClick, string name)
		: base(text, image, isLink: false, onClick, name)
	{
		border_style = Border3DStyle.Flat;
	}

	/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> can be fitted.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
	/// <param name="constrainingSize">The custom-sized area for a control.</param>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return base.GetPreferredSize(constrainingSize);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
	}
}
