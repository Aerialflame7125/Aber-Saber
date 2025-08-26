using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides properties that specify the appearance of <see cref="T:System.Windows.Forms.Button" /> controls whose <see cref="T:System.Windows.Forms.FlatStyle" /> is <see cref="F:System.Windows.Forms.FlatStyle.Flat" />.</summary>
[TypeConverter(typeof(FlatButtonAppearanceConverter))]
public class FlatButtonAppearance
{
	private Color borderColor = Color.Empty;

	private int borderSize = 1;

	private Color checkedBackColor = Color.Empty;

	private Color mouseDownBackColor = Color.Empty;

	private Color mouseOverBackColor = Color.Empty;

	private ButtonBase owner;

	/// <summary>Gets or sets the color of the border around the button.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the border around the button.</returns>
	[Browsable(true)]
	[NotifyParentProperty(true)]
	[DefaultValue(typeof(Color), "")]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public Color BorderColor
	{
		get
		{
			return borderColor;
		}
		set
		{
			if (!(borderColor == value))
			{
				if (value == Color.Transparent)
				{
					throw new NotSupportedException("Cannot have a Transparent border.");
				}
				borderColor = value;
				if (owner != null)
				{
					owner.Invalidate();
				}
			}
		}
	}

	/// <summary>Gets or sets a value that specifies the size, in pixels, of the border around the button.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the size, in pixels, of the border around the button.</returns>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[DefaultValue(1)]
	[NotifyParentProperty(true)]
	[Browsable(true)]
	public int BorderSize
	{
		get
		{
			return borderSize;
		}
		set
		{
			if (borderSize != value)
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", $"'{value}' is not a valid value for 'BorderSize'. 'BorderSize' must be greater or equal than {0}.");
				}
				borderSize = value;
				if (owner != null)
				{
					owner.Invalidate();
				}
			}
		}
	}

	/// <summary>Gets or sets the color of the client area of the button when the button is checked and the mouse pointer is outside the bounds of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
	[DefaultValue(typeof(Color), "")]
	[Browsable(true)]
	[NotifyParentProperty(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public Color CheckedBackColor
	{
		get
		{
			return checkedBackColor;
		}
		set
		{
			if (!(checkedBackColor == value))
			{
				checkedBackColor = value;
				if (owner != null)
				{
					owner.Invalidate();
				}
			}
		}
	}

	/// <summary>Gets or sets the color of the client area of the button when the mouse is pressed within the bounds of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
	[DefaultValue(typeof(Color), "")]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[NotifyParentProperty(true)]
	[Browsable(true)]
	public Color MouseDownBackColor
	{
		get
		{
			return mouseDownBackColor;
		}
		set
		{
			if (!(mouseDownBackColor == value))
			{
				mouseDownBackColor = value;
				if (owner != null)
				{
					owner.Invalidate();
				}
			}
		}
	}

	/// <summary>Gets or sets the color of the client area of the button when the mouse pointer is within the bounds of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[DefaultValue(typeof(Color), "")]
	[NotifyParentProperty(true)]
	public Color MouseOverBackColor
	{
		get
		{
			return mouseOverBackColor;
		}
		set
		{
			if (!(mouseOverBackColor == value))
			{
				mouseOverBackColor = value;
				if (owner != null)
				{
					owner.Invalidate();
				}
			}
		}
	}

	internal FlatButtonAppearance(ButtonBase owner)
	{
		this.owner = owner;
	}
}
