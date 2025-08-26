using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Specifies the appearance, text formatting, and behavior of a <see cref="T:System.Windows.Forms.DataGrid" /> control column. This class is abstract.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxItem(false)]
[DefaultProperty("Header")]
[DesignTimeVisible(false)]
public abstract class DataGridColumnStyle : Component, IDataGridColumnStyleEditingNotificationService
{
	/// <summary>Provides an implementation for an object that can be inspected by an accessibility application.</summary>
	[ComVisible(true)]
	protected class DataGridColumnHeaderAccessibleObject : AccessibleObject
	{
		private new DataGridColumnStyle owner;

		/// <summary>Gets the bounding rectangle of a column.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that contains the bounding values of the column.</returns>
		[System.MonoTODO("Not implemented, will throw NotImplementedException")]
		public override Rectangle Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the column that owns the accessibility object.</summary>
		/// <returns>The name of the column that owns the accessibility object.</returns>
		public override string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the column style object that owns the accessibility object.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that owns the accessibility object.</returns>
		protected DataGridColumnStyle Owner => owner;

		/// <summary>Gets the parent accessibility object.</summary>
		/// <returns>The parent <see cref="T:System.Windows.Forms.AccessibleObject" /> of the column style object.</returns>
		public override AccessibleObject Parent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the role of the accessibility object.</summary>
		/// <returns>The AccessibleRole object of the accessibility object.</returns>
		public override AccessibleRole Role
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.DataGridColumnHeaderAccessibleObject" /> class without specifying a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> host for the object. </summary>
		public DataGridColumnHeaderAccessibleObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.DataGridColumnHeaderAccessibleObject" /> class and specifies the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that hosts the object.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> that hosts the object. </param>
		public DataGridColumnHeaderAccessibleObject(DataGridColumnStyle owner)
		{
			this.owner = owner;
		}

		/// <summary>Enables navigation to another object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> specified by the <paramref name="navdir" /> parameter.</returns>
		/// <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values. </param>
		[System.MonoTODO("Not implemented, will throw NotImplementedException")]
		public override AccessibleObject Navigate(AccessibleNavigation navdir)
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Contains a <see cref="T:System.Diagnostics.TraceSwitch" /> that is used by the .NET Framework infrastructure.</summary>
	protected class CompModSwitches
	{
		/// <summary>Gets a <see cref="T:System.Diagnostics.TraceSwitch" />.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.TraceSwitch" /> used by the .NET Framework infrastructure.</returns>
		[System.MonoTODO("Not implemented, will throw NotImplementedException")]
		public static TraceSwitch DGEditColumnEditing
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle.CompModSwitches" /> class. </summary>
		public CompModSwitches()
		{
		}
	}

	internal enum ArrowDrawing
	{
		No,
		Ascending,
		Descending
	}

	internal HorizontalAlignment alignment;

	private int fontheight;

	internal DataGridTableStyle table_style;

	private string header_text;

	private string mapping_name;

	private string null_text;

	private PropertyDescriptor property_descriptor;

	private bool _readonly;

	private int width;

	internal bool is_default;

	internal DataGrid grid;

	private DataGridColumnHeaderAccessibleObject accesible_object;

	private static string def_null_text = "(null)";

	private ArrowDrawing arrow_drawing;

	internal bool bound;

	private static object AlignmentChangedEvent;

	private static object FontChangedEvent;

	private static object HeaderTextChangedEvent;

	private static object MappingNameChangedEvent;

	private static object NullTextChangedEvent;

	private static object PropertyDescriptorChangedEvent;

	private static object ReadOnlyChangedEvent;

	private static object WidthChangedEvent;

	/// <summary>Gets or sets the alignment of text in a column.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is Left. Valid options include Left, Center, and Right.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(HorizontalAlignment.Left)]
	public virtual HorizontalAlignment Alignment
	{
		get
		{
			return alignment;
		}
		set
		{
			if (value != alignment)
			{
				alignment = value;
				if (table_style != null && table_style.DataGrid != null)
				{
					table_style.DataGrid.Invalidate();
				}
				((EventHandler)base.Events[AlignmentChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> for the column.</summary>
	/// <returns>The <see cref="P:System.Windows.Forms.DataGridColumnStyle.DataGridTableStyle" /> that contains the current <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual DataGridTableStyle DataGridTableStyle => table_style;

	/// <summary>Gets the height of the column's font.</summary>
	/// <returns>The height of the font, in pixels. If no font height has been set, the property returns the <see cref="T:System.Windows.Forms.DataGrid" /> control's font height; if that property hasn't been set, the default font height value for the <see cref="T:System.Windows.Forms.DataGrid" /> control is returned.</returns>
	protected int FontHeight
	{
		get
		{
			if (fontheight != -1)
			{
				return fontheight;
			}
			if (table_style != null)
			{
				return -1;
			}
			return -1;
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public AccessibleObject HeaderAccessibleObject => accesible_object;

	/// <summary>Gets or sets the text of the column header.</summary>
	/// <returns>A string that is displayed as the column header. If it is created by the <see cref="T:System.Windows.Forms.DataGrid" />, the default value is the name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column. If it is created by the user, the default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	public virtual string HeaderText
	{
		get
		{
			return header_text;
		}
		set
		{
			if (value != header_text)
			{
				header_text = value;
				Invalidate();
				((EventHandler)base.Events[HeaderTextChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the data member to map the column style to.</summary>
	/// <returns>The name of the data member to map the column style to.</returns>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.DataGridColumnStyleMappingNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue("")]
	[Localizable(true)]
	public string MappingName
	{
		get
		{
			return mapping_name;
		}
		set
		{
			if (value != mapping_name)
			{
				mapping_name = value;
				((EventHandler)base.Events[MappingNameChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the text that is displayed when the column contains null.</summary>
	/// <returns>A string displayed in a column containing a <see cref="F:System.DBNull.Value" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	public virtual string NullText
	{
		get
		{
			return null_text;
		}
		set
		{
			if (value != null_text)
			{
				null_text = value;
				if (table_style != null && table_style.DataGrid != null)
				{
					table_style.DataGrid.Invalidate();
				}
				((EventHandler)base.Events[NullTextChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that determines the attributes of data displayed by the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that contains data about the attributes of the column.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DefaultValue(null)]
	public virtual PropertyDescriptor PropertyDescriptor
	{
		get
		{
			return property_descriptor;
		}
		set
		{
			if (value != property_descriptor)
			{
				property_descriptor = value;
				((EventHandler)base.Events[PropertyDescriptorChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the data in the column can be edited.</summary>
	/// <returns>true, if the data cannot be edited; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public virtual bool ReadOnly
	{
		get
		{
			return _readonly;
		}
		set
		{
			if (value != _readonly)
			{
				_readonly = value;
				if (table_style != null && table_style.DataGrid != null)
				{
					table_style.DataGrid.CalcAreasAndInvalidate();
				}
				((EventHandler)base.Events[ReadOnlyChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the width of the column.</summary>
	/// <returns>The width of the column, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(100)]
	public virtual int Width
	{
		get
		{
			return width;
		}
		set
		{
			if (value != width)
			{
				width = value;
				if (table_style != null && table_style.DataGrid != null)
				{
					table_style.DataGrid.CalcAreasAndInvalidate();
				}
				((EventHandler)base.Events[WidthChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	internal ArrowDrawing ArrowDrawingMode
	{
		get
		{
			return arrow_drawing;
		}
		set
		{
			arrow_drawing = value;
		}
	}

	internal bool TableStyleReadOnly => table_style != null && table_style.ReadOnly;

	internal DataGridTableStyle TableStyle
	{
		set
		{
			table_style = value;
		}
	}

	internal bool IsDefault => is_default;

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.Alignment" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler AlignmentChanged
	{
		add
		{
			base.Events.AddHandler(AlignmentChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AlignmentChangedEvent, value);
		}
	}

	/// <summary>Occurs when the column's font changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler FontChanged
	{
		add
		{
			base.Events.AddHandler(FontChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FontChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.HeaderText" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HeaderTextChanged
	{
		add
		{
			base.Events.AddHandler(HeaderTextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HeaderTextChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.MappingName" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MappingNameChanged
	{
		add
		{
			base.Events.AddHandler(MappingNameChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MappingNameChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.NullText" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler NullTextChanged
	{
		add
		{
			base.Events.AddHandler(NullTextChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(NullTextChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.PropertyDescriptor" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public event EventHandler PropertyDescriptorChanged
	{
		add
		{
			base.Events.AddHandler(PropertyDescriptorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PropertyDescriptorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.ReadOnly" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ReadOnlyChanged
	{
		add
		{
			base.Events.AddHandler(ReadOnlyChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ReadOnlyChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridColumnStyle.Width" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler WidthChanged
	{
		add
		{
			base.Events.AddHandler(WidthChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(WidthChangedEvent, value);
		}
	}

	/// <summary>In a derived class, initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> class.</summary>
	public DataGridColumnStyle()
		: this(null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> class with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
	/// <param name="prop">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the attributes for the column. </param>
	public DataGridColumnStyle(PropertyDescriptor prop)
	{
		property_descriptor = prop;
		fontheight = -1;
		table_style = null;
		header_text = string.Empty;
		mapping_name = string.Empty;
		null_text = def_null_text;
		accesible_object = new DataGridColumnHeaderAccessibleObject(this);
		_readonly = prop?.IsReadOnly ?? false;
		width = -1;
		grid = null;
		is_default = false;
		alignment = HorizontalAlignment.Left;
	}

	static DataGridColumnStyle()
	{
		AlignmentChanged = new object();
		FontChanged = new object();
		HeaderTextChanged = new object();
		MappingNameChanged = new object();
		NullTextChanged = new object();
		PropertyDescriptorChanged = new object();
		ReadOnlyChanged = new object();
		WidthChanged = new object();
	}

	/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> control that the user has begun editing the column.</summary>
	/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that is editing the column.</param>
	void IDataGridColumnStyleEditingNotificationService.ColumnStartedEditing(Control editingControl)
	{
		ColumnStartedEditing(editingControl);
	}

	/// <summary>When overridden in a derived class, initiates a request to interrupt an edit procedure.</summary>
	/// <param name="rowNum">The row number upon which an operation is being interrupted. </param>
	protected internal abstract void Abort(int rowNum);

	/// <summary>Suspends the painting of the column until the <see cref="M:System.Windows.Forms.DataGridColumnStyle.EndUpdate" /> method is called.</summary>
	[System.MonoTODO("Will not suspend updates")]
	protected void BeginUpdate()
	{
	}

	/// <summary>Throws an exception if the <see cref="T:System.Windows.Forms.DataGrid" /> does not have a valid data source, or if this column is not mapped to a valid property in the data source.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.CurrencyManager" /> to check. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> is null. </exception>
	/// <exception cref="T:System.ApplicationException">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for this column is null. </exception>
	protected void CheckValidDataSource(CurrencyManager value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("CurrencyManager cannot be null");
		}
		if (property_descriptor == null)
		{
			property_descriptor = value.GetItemProperties()[mapping_name];
			if (property_descriptor == null)
			{
				throw new InvalidOperationException("The PropertyDescriptor for this column is a null reference");
			}
		}
	}

	/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> that the user has begun editing the column.</summary>
	/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that hosted by the column. </param>
	protected internal virtual void ColumnStartedEditing(Control editingControl)
	{
	}

	/// <summary>When overridden in a derived class, initiates a request to complete an editing procedure.</summary>
	/// <returns>true if the editing procedure committed successfully; otherwise, false.</returns>
	/// <param name="dataSource">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <param name="rowNum">The number of the row being edited. </param>
	protected internal abstract bool Commit(CurrencyManager dataSource, int rowNum);

	/// <summary>Notifies a column that it must relinquish the focus to the control it is hosting.</summary>
	protected internal virtual void ConcedeFocus()
	{
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> for the column.</returns>
	protected virtual AccessibleObject CreateHeaderAccessibleObject()
	{
		return new DataGridColumnHeaderAccessibleObject(this);
	}

	/// <summary>Prepares a cell for editing.</summary>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <param name="rowNum">The row number to edit. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
	/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
	protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly)
	{
		Edit(source, rowNum, bounds, readOnly, string.Empty);
	}

	/// <summary>Prepares the cell for editing using the specified <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and <see cref="T:System.Drawing.Rectangle" /> parameters.</summary>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <param name="rowNum">The row number in this column which is being edited. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
	/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
	/// <param name="displayText">The text to display in the control. </param>
	protected internal virtual void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText)
	{
		Edit(source, rowNum, bounds, readOnly, displayText, cellIsVisible: true);
	}

	/// <summary>When overridden in a deriving class, prepares a cell for editing.</summary>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <param name="rowNum">The row number in this column which is being edited. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
	/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
	/// <param name="displayText">The text to display in the control. </param>
	/// <param name="cellIsVisible">A value indicating whether the cell is visible. true if the cell is visible; otherwise, false. </param>
	protected internal abstract void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible);

	/// <summary>Resumes the painting of columns suspended by calling the <see cref="M:System.Windows.Forms.DataGridColumnStyle.BeginUpdate" /> method.</summary>
	protected void EndUpdate()
	{
	}

	/// <summary>Enters a <see cref="F:System.DBNull.Value" /> into the column.</summary>
	protected internal virtual void EnterNullValue()
	{
	}

	/// <summary>Gets the value in the specified row from the specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> containing the value.</returns>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> containing the data. </param>
	/// <param name="rowNum">The row number containing the data. </param>
	/// <exception cref="T:System.ApplicationException">The <see cref="T:System.Data.DataColumn" /> for this <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> hasn't been set yet. </exception>
	protected internal virtual object GetColumnValueAtRow(CurrencyManager source, int rowNum)
	{
		CheckValidDataSource(source);
		if (rowNum >= source.Count)
		{
			return DBNull.Value;
		}
		return property_descriptor.GetValue(source[rowNum]);
	}

	/// <summary>When overridden in a derived class, gets the minimum height of a row.</summary>
	/// <returns>The minimum height of a row.</returns>
	protected internal abstract int GetMinimumHeight();

	/// <summary>When overridden in a derived class, gets the height used for automatically resizing columns.</summary>
	/// <returns>The height used for auto resizing a cell.</returns>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object. </param>
	/// <param name="value">An object value for which you want to know the screen height and width. </param>
	protected internal abstract int GetPreferredHeight(Graphics g, object value);

	/// <summary>When overridden in a derived class, gets the width and height of the specified value. The width and height are used when the user navigates to <see cref="T:System.Windows.Forms.DataGridTableStyle" /> using the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the dimensions of the cell.</returns>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object. </param>
	/// <param name="value">An object value for which you want to know the screen height and width. </param>
	protected internal abstract Size GetPreferredSize(Graphics g, object value);

	/// <summary>Redraws the column and causes a paint message to be sent to the control.</summary>
	protected virtual void Invalidate()
	{
		if (grid != null)
		{
			grid.InvalidateColumn(this);
		}
	}

	/// <summary>Paints the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, and row number.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
	/// <param name="rowNum">The number of the row in the underlying data being referred to. </param>
	protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum);

	/// <summary>When overridden in a derived class, paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and alignment.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
	/// <param name="rowNum">The number of the row in the underlying data being referred to. </param>
	/// <param name="alignToRight">A value indicating whether to align the column's content to the right. true if the content should be aligned to the right; otherwise false. </param>
	protected internal abstract void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight);

	/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, background color, foreground color, and alignment.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
	/// <param name="rowNum">The number of the row in the underlying data table being referred to. </param>
	/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> used to paint the background color. </param>
	/// <param name="foreBrush">A <see cref="T:System.Drawing.Color" /> used to paint the foreground color. </param>
	/// <param name="alignToRight">A value indicating whether to align the content to the right. true if the content is aligned to the right, otherwise, false. </param>
	protected internal virtual void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
	{
	}

	/// <summary>Allows the column to free resources when the control it hosts is not needed.</summary>
	protected internal virtual void ReleaseHostedControl()
	{
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridColumnStyle.HeaderText" /> to its default value, null.</summary>
	/// <filterpriority>1</filterpriority>
	public void ResetHeaderText()
	{
		HeaderText = string.Empty;
	}

	/// <summary>Sets the value in a specified row with the value from a specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
	/// <param name="source">A <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <param name="rowNum">The number of the row. </param>
	/// <param name="value">The value to set. </param>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.CurrencyManager" /> object's <see cref="P:System.Windows.Forms.BindingManagerBase.Position" /> does not match <paramref name="rowNum" />. </exception>
	protected internal virtual void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
	{
		CheckValidDataSource(source);
		if (source[rowNum] is IEditableObject editableObject)
		{
			editableObject.BeginEdit();
		}
		property_descriptor.SetValue(source[rowNum], value);
	}

	/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> control that this column belongs to.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGrid" /> control that this column belongs to. </param>
	protected virtual void SetDataGrid(DataGrid value)
	{
		grid = value;
		property_descriptor = null;
	}

	/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> for the column.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.DataGrid" />. </param>
	protected virtual void SetDataGridInColumn(DataGrid value)
	{
		SetDataGrid(value);
	}

	internal void SetDataGridInternal(DataGrid value)
	{
		SetDataGridInColumn(value);
	}

	/// <summary>Updates the value of a specified row with the given text.</summary>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> associated with the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />. </param>
	/// <param name="rowNum">The row to update. </param>
	/// <param name="displayText">The new value. </param>
	protected internal virtual void UpdateUI(CurrencyManager source, int rowNum, string displayText)
	{
	}

	internal virtual void OnMouseDown(MouseEventArgs e, int row, int column)
	{
	}

	internal virtual void OnKeyDown(KeyEventArgs ke, int row, int column)
	{
	}

	internal void PaintHeader(Graphics g, Rectangle bounds, int colNum)
	{
		ThemeEngine.Current.DataGridPaintColumnHeader(g, bounds, grid, colNum);
	}

	internal void PaintNewRow(Graphics g, Rectangle bounds, Brush backBrush, Brush foreBrush)
	{
		g.FillRectangle(backBrush, bounds);
		PaintGridLine(g, bounds);
	}

	internal void PaintGridLine(Graphics g, Rectangle bounds)
	{
		if (table_style.CurrentGridLineStyle == DataGridLineStyle.Solid)
		{
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(table_style.CurrentGridLineColor), bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(table_style.CurrentGridLineColor), bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height);
		}
	}
}
