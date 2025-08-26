namespace System.Windows.Forms;

/// <summary>Encapsulates the information needed when creating a control.</summary>
/// <filterpriority>2</filterpriority>
public class CreateParams
{
	private string caption;

	private string class_name;

	private int class_style;

	private int ex_style;

	private int x;

	private int y;

	private int height;

	private int width;

	private int style;

	private object param;

	private IntPtr parent;

	internal Menu menu;

	internal Control control;

	/// <summary>Gets or sets the control's initial text.</summary>
	/// <returns>The control's initial text.</returns>
	/// <filterpriority>1</filterpriority>
	public string Caption
	{
		get
		{
			return caption;
		}
		set
		{
			caption = value;
		}
	}

	/// <summary>Gets or sets the name of the Windows class to derive the control from.</summary>
	/// <returns>The name of the Windows class to derive the control from.</returns>
	/// <filterpriority>1</filterpriority>
	public string ClassName
	{
		get
		{
			return class_name;
		}
		set
		{
			class_name = value;
		}
	}

	/// <summary>Gets or sets a bitwise combination of class style values.</summary>
	/// <returns>A bitwise combination of the class style values.</returns>
	/// <filterpriority>1</filterpriority>
	public int ClassStyle
	{
		get
		{
			return class_style;
		}
		set
		{
			class_style = value;
		}
	}

	/// <summary>Gets or sets a bitwise combination of extended window style values.</summary>
	/// <returns>A bitwise combination of the extended window style values.</returns>
	/// <filterpriority>1</filterpriority>
	public int ExStyle
	{
		get
		{
			return ex_style;
		}
		set
		{
			ex_style = value;
		}
	}

	/// <summary>Gets or sets the initial left position of the control.</summary>
	/// <returns>The numeric value that represents the initial left position of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public int X
	{
		get
		{
			return x;
		}
		set
		{
			x = value;
		}
	}

	/// <summary>Gets or sets the top position of the initial location of the control.</summary>
	/// <returns>The numeric value that represents the top position of the initial location of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public int Y
	{
		get
		{
			return y;
		}
		set
		{
			y = value;
		}
	}

	/// <summary>Gets or sets the initial width of the control.</summary>
	/// <returns>The numeric value that represents the initial width of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			width = value;
		}
	}

	/// <summary>Gets or sets the initial height of the control.</summary>
	/// <returns>The numeric value that represents the initial height of the control.</returns>
	/// <filterpriority>1</filterpriority>
	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}

	/// <summary>Gets or sets a bitwise combination of window style values.</summary>
	/// <returns>A bitwise combination of the window style values.</returns>
	/// <filterpriority>1</filterpriority>
	public int Style
	{
		get
		{
			return style;
		}
		set
		{
			style = value;
		}
	}

	/// <summary>Gets or sets additional parameter information needed to create the control.</summary>
	/// <returns>The <see cref="T:System.Object" /> that holds additional parameter information needed to create the control.</returns>
	/// <filterpriority>1</filterpriority>
	public object Param
	{
		get
		{
			return param;
		}
		set
		{
			param = value;
		}
	}

	/// <summary>Gets or sets the control's parent.</summary>
	/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle of the control's parent.</returns>
	/// <filterpriority>1</filterpriority>
	public IntPtr Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	internal bool HasWindowManager
	{
		get
		{
			if (control == null)
			{
				return false;
			}
			if (!(control is Form form))
			{
				return false;
			}
			return form.window_manager != null;
		}
	}

	internal WindowExStyles WindowExStyle
	{
		get
		{
			return (WindowExStyles)ex_style;
		}
		set
		{
			ex_style = (int)value;
		}
	}

	internal WindowStyles WindowStyle
	{
		get
		{
			return (WindowStyles)style;
		}
		set
		{
			style = (int)value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CreateParams" /> class. </summary>
	public CreateParams()
	{
	}

	internal bool IsSet(WindowStyles Style)
	{
		return ((uint)style & (uint)Style) == (uint)Style;
	}

	internal bool IsSet(WindowExStyles ExStyle)
	{
		return ((uint)ex_style & (uint)ExStyle) == (uint)ExStyle;
	}

	internal static bool IsSet(WindowExStyles ExStyle, WindowExStyles Option)
	{
		return (Option & ExStyle) == Option;
	}

	internal static bool IsSet(WindowStyles Style, WindowStyles Option)
	{
		return (Option & Style) == Option;
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"CreateParams {{'{class_name}', '{caption}', 0x{class_style:X}, 0x{ex_style:X}, {{{x}, {y}, {width}, {height}}}}}";
	}
}
