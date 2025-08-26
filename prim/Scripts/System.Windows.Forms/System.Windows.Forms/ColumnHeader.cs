using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Displays a single column header in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxItem(false)]
[DefaultProperty("Text")]
[DesignTimeVisible(false)]
[TypeConverter(typeof(ColumnHeaderConverter))]
public class ColumnHeader : Component, ICloneable
{
	private StringFormat format = new StringFormat();

	private string text = "ColumnHeader";

	private HorizontalAlignment text_alignment;

	private int width = ThemeEngine.Current.ListViewDefaultColumnWidth;

	private int image_index = -1;

	private string image_key = string.Empty;

	private string name = string.Empty;

	private object tag;

	private int display_index = -1;

	private Rectangle column_rect = Rectangle.Empty;

	private bool pressed;

	private ListView owner;

	private static object UIATextChangedEvent;

	internal bool Pressed
	{
		get
		{
			return pressed;
		}
		set
		{
			pressed = value;
		}
	}

	internal int X
	{
		get
		{
			return column_rect.X;
		}
		set
		{
			column_rect.X = value;
		}
	}

	internal int Y
	{
		get
		{
			return column_rect.Y;
		}
		set
		{
			column_rect.Y = value;
		}
	}

	internal int Wd
	{
		get
		{
			return column_rect.Width;
		}
		set
		{
			column_rect.Width = value;
		}
	}

	internal int Ht
	{
		get
		{
			return column_rect.Height;
		}
		set
		{
			column_rect.Height = value;
		}
	}

	internal Rectangle Rect
	{
		get
		{
			return column_rect;
		}
		set
		{
			column_rect = value;
		}
	}

	internal StringFormat Format => format;

	internal int InternalDisplayIndex
	{
		get
		{
			return display_index;
		}
		set
		{
			display_index = value;
		}
	}

	/// <summary>Gets the display order of the column relative to the currently displayed columns.</summary>
	/// <returns>The display order of the column, relative to the currently displayed columns.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int DisplayIndex
	{
		get
		{
			if (owner == null)
			{
				return display_index;
			}
			return owner.GetReorderedColumnIndex(this);
		}
		set
		{
			if (owner == null)
			{
				display_index = value;
				return;
			}
			if (value < 0 || value >= owner.Columns.Count)
			{
				throw new ArgumentOutOfRangeException("DisplayIndex");
			}
			owner.ReorderColumn(this, value, fireEvent: false);
		}
	}

	/// <summary>Gets or sets the index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />. </summary>
	/// <returns>The index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <see cref="P:System.Windows.Forms.ColumnHeader.ImageIndex" /> is less than -1.</exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[TypeConverter(typeof(ImageIndexConverter))]
	[RefreshProperties(RefreshProperties.Repaint)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue(-1)]
	public int ImageIndex
	{
		get
		{
			return image_index;
		}
		set
		{
			if (value < -1)
			{
				throw new ArgumentOutOfRangeException("ImageIndex");
			}
			image_index = value;
			image_key = string.Empty;
			if (owner != null)
			{
				owner.header_control.Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the key of the image displayed in the column.</summary>
	/// <returns>The key of the image displayed in the column.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(ImageKeyConverter))]
	[DefaultValue("")]
	public string ImageKey
	{
		get
		{
			return image_key;
		}
		set
		{
			image_key = ((value != null) ? value : string.Empty);
			image_index = -1;
			if (owner != null)
			{
				owner.header_control.Invalidate();
			}
		}
	}

	/// <summary>Gets the image list associated with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ImageList" /> associated with the <see cref="T:System.Windows.Forms.ColumnHeader" />. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ImageList ImageList
	{
		get
		{
			if (owner == null)
			{
				return null;
			}
			return owner.SmallImageList;
		}
	}

	/// <summary>Gets the location with the <see cref="T:System.Windows.Forms.ListView" /> control's <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of this column.</summary>
	/// <returns>The zero-based index of the column header within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control it is contained in.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int Index
	{
		get
		{
			if (owner != null && owner.Columns != null && owner.Columns.Contains(this))
			{
				return owner.Columns.IndexOf(this);
			}
			return -1;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListView" /> control the <see cref="T:System.Windows.Forms.ColumnHeader" /> is located in.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ListView" /> control that represents the control that contains the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ListView ListView => owner;

	/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.ColumnHeader" />. </summary>
	/// <returns>The name of the <see cref="T:System.Windows.Forms.ColumnHeader" />. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = ((value != null) ? value : string.Empty);
		}
	}

	/// <summary>Gets or sets an object that contains data to associate with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data to associate with the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[Localizable(false)]
	[Bindable(true)]
	[DefaultValue(null)]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			tag = value;
		}
	}

	/// <summary>Gets or sets the text displayed in the column header.</summary>
	/// <returns>The text displayed in the column header.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (text != value)
			{
				text = value;
				if (owner != null)
				{
					owner.Redraw(recalculate: true);
				}
				OnUIATextChanged();
			}
		}
	}

	/// <summary>Gets or sets the horizontal alignment of the text displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(HorizontalAlignment.Left)]
	public HorizontalAlignment TextAlign
	{
		get
		{
			return text_alignment;
		}
		set
		{
			text_alignment = value;
			if (owner != null)
			{
				owner.Redraw(recalculate: true);
			}
		}
	}

	/// <summary>Gets or sets the width of the column.</summary>
	/// <returns>The width of the column, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(60)]
	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			if (width != value)
			{
				width = value;
				if (owner != null)
				{
					owner.Redraw(recalculate: true);
					owner.RaiseColumnWidthChanged(this);
				}
			}
		}
	}

	internal event EventHandler UIATextChanged
	{
		add
		{
			base.Events.AddHandler(UIATextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIATextChangedEvent, value);
		}
	}

	internal ColumnHeader(ListView owner, string text, HorizontalAlignment alignment, int width)
	{
		this.owner = owner;
		this.text = text;
		this.width = width;
		text_alignment = alignment;
		CalcColumnHeader();
	}

	internal ColumnHeader(string key, string text, int width, HorizontalAlignment textAlign)
	{
		Name = key;
		Text = text;
		this.width = width;
		text_alignment = textAlign;
		CalcColumnHeader();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class.</summary>
	public ColumnHeader()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
	/// <param name="imageIndex">The index of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
	public ColumnHeader(int imageIndex)
	{
		ImageIndex = imageIndex;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
	/// <param name="imageKey">The key of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
	public ColumnHeader(string imageKey)
	{
		ImageKey = imageKey;
	}

	static ColumnHeader()
	{
		UIATextChanged = new object();
	}

	internal void CalcColumnHeader()
	{
		if (text_alignment == HorizontalAlignment.Center)
		{
			format.Alignment = StringAlignment.Center;
		}
		else if (text_alignment == HorizontalAlignment.Right)
		{
			format.Alignment = StringAlignment.Far;
		}
		else
		{
			format.Alignment = StringAlignment.Near;
		}
		format.LineAlignment = StringAlignment.Center;
		format.Trimming = StringTrimming.EllipsisCharacter;
		format.FormatFlags = StringFormatFlags.NoWrap;
		if (owner != null)
		{
			column_rect.Height = ThemeEngine.Current.ListViewGetHeaderHeight(owner, owner.Font);
		}
		else
		{
			column_rect.Height = ThemeEngine.Current.ListViewGetHeaderHeight(null, ThemeEngine.Current.DefaultFont);
		}
		if (width >= 0)
		{
			column_rect.Width = width;
		}
		else if (Index != -1)
		{
			column_rect.Width = owner.GetChildColumnSize(Index).Width;
			width = column_rect.Width;
		}
		else
		{
			column_rect.Width = 0;
		}
	}

	internal void SetListView(ListView list_view)
	{
		owner = list_view;
	}

	/// <summary>Resizes the width of the column as indicated by the resize style.</summary>
	/// <param name="headerAutoResize">One of the <see cref="T:System.Windows.Forms.ColumnHeaderAutoResizeStyle" />  values.</param>
	/// <exception cref="T:System.InvalidOperationException">A value other than <see cref="F:System.Windows.Forms.ColumnHeaderAutoResizeStyle.None" /> is passed to the <see cref="M:System.Windows.Forms.ColumnHeader.AutoResize(System.Windows.Forms.ColumnHeaderAutoResizeStyle)" /> method when the <see cref="P:System.Windows.Forms.ListView.View" /> property is a value other than <see cref="F:System.Windows.Forms.View.Details" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void AutoResize(ColumnHeaderAutoResizeStyle headerAutoResize)
	{
		switch (headerAutoResize)
		{
		case ColumnHeaderAutoResizeStyle.None:
			break;
		case ColumnHeaderAutoResizeStyle.ColumnContent:
			Width = -1;
			break;
		case ColumnHeaderAutoResizeStyle.HeaderSize:
			Width = -2;
			break;
		default:
			throw new InvalidEnumArgumentException("headerAutoResize", (int)headerAutoResize, typeof(ColumnHeaderAutoResizeStyle));
		}
	}

	/// <summary>Creates an identical copy of the current <see cref="T:System.Windows.Forms.ColumnHeader" /> that is not attached to any list view control.</summary>
	/// <returns>An object representing a copy of this <see cref="T:System.Windows.Forms.ColumnHeader" /> object.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public object Clone()
	{
		ColumnHeader columnHeader = new ColumnHeader();
		columnHeader.text = text;
		columnHeader.text_alignment = text_alignment;
		columnHeader.width = width;
		columnHeader.owner = owner;
		columnHeader.format = (StringFormat)Format.Clone();
		columnHeader.column_rect = Rectangle.Empty;
		return columnHeader;
	}

	/// <summary>Returns a string representation of this column header.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"ColumnHeader: Text: {text}";
	}

	/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	private void OnUIATextChanged()
	{
		((EventHandler)base.Events[UIATextChanged])?.Invoke(this, EventArgs.Empty);
	}
}
