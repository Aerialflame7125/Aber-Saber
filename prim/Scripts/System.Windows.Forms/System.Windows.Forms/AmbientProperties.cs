using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides ambient property values to top-level controls.</summary>
/// <filterpriority>2</filterpriority>
public sealed class AmbientProperties
{
	private Color fore_color;

	private Color back_color;

	private Font font;

	private Cursor cursor;

	/// <summary>Gets or sets the ambient background color of an object.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the background color of an object.</returns>
	/// <filterpriority>1</filterpriority>
	public Color BackColor
	{
		get
		{
			return back_color;
		}
		set
		{
			back_color = value;
		}
	}

	/// <summary>Gets or sets the ambient cursor of an object.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor of an object.</returns>
	/// <filterpriority>1</filterpriority>
	public Cursor Cursor
	{
		get
		{
			return cursor;
		}
		set
		{
			cursor = value;
		}
	}

	/// <summary>Gets or sets the ambient font of an object.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font used when displaying text within an object.</returns>
	/// <filterpriority>1</filterpriority>
	public Font Font
	{
		get
		{
			return font;
		}
		set
		{
			font = value;
		}
	}

	/// <summary>Gets or sets the ambient foreground color of an object.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> value that represents the foreground color of an object.</returns>
	/// <filterpriority>1</filterpriority>
	public Color ForeColor
	{
		get
		{
			return fore_color;
		}
		set
		{
			fore_color = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.AmbientProperties" /> class. </summary>
	public AmbientProperties()
	{
	}
}
