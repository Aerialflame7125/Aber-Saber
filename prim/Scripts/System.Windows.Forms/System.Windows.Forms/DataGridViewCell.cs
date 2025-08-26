using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents an individual cell in a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
/// <filterpriority>2</filterpriority>
[TypeConverter(typeof(DataGridViewCellConverter))]
public abstract class DataGridViewCell : DataGridViewElement, IDisposable, ICloneable
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCell" /> to accessibility client applications.</summary>
	[ComVisible(true)]
	protected class DataGridViewCellAccessibleObject : AccessibleObject
	{
		private DataGridViewCell dataGridViewCell;

		/// <summary>Gets the location and size of the accessible object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override Rectangle Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The string "Edit".</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override string DefaultAction => "Edit";

		/// <summary>Gets the names of the owning cell's type and base type.</summary>
		/// <returns>The names of the owning cell's type and base type.</returns>
		public override string Help => base.Help;

		/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override string Name => dataGridViewCell.OwningColumn.HeaderText + ": " + dataGridViewCell.RowIndex;

		/// <summary>Gets or sets the cell that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property has already been set.</exception>
		public DataGridViewCell Owner
		{
			get
			{
				return dataGridViewCell;
			}
			set
			{
				dataGridViewCell = value;
			}
		}

		/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <returns>The parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject Parent => dataGridViewCell.OwningRow.AccessibilityObject;

		/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Cell" /> value.</returns>
		public override AccessibleRole Role => AccessibleRole.Cell;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. </returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleStates State
		{
			get
			{
				if (dataGridViewCell.Selected)
				{
					return AccessibleStates.Selected;
				}
				return AccessibleStates.Focused;
			}
		}

		/// <summary>Gets or sets a string representing the formatted value of the owning cell. </summary>
		/// <returns>A <see cref="T:System.String" /> representation of the cell value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override string Value
		{
			get
			{
				if (dataGridViewCell.FormattedValue == null)
				{
					return "(null)";
				}
				return dataGridViewCell.FormattedValue.ToString();
			}
			set
			{
				if (owner == null)
				{
					throw new InvalidOperationException("owner is null");
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> class without initializing the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property.</summary>
		public DataGridViewCellAccessibleObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property to the specified <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</param>
		public DataGridViewCellAccessibleObject(DataGridViewCell owner)
		{
			dataGridViewCell = owner;
		}

		/// <summary>Performs the default action associated with the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.-or-The value of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> property is not null and the <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is equal to -1.</exception>
		public override void DoDefaultAction()
		{
			if (dataGridViewCell.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically && !dataGridViewCell.IsInEditMode)
			{
			}
		}

		/// <summary>Returns the accessible object corresponding to the specified index.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents the accessible child corresponding to the specified index.</returns>
		/// <param name="index">The zero-based index of the child accessible object.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject GetChild(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the number of children that belong to the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</summary>
		/// <returns>The value 1 if the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> is being edited; otherwise, –1.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override int GetChildCount()
		{
			if (dataGridViewCell.IsInEditMode)
			{
				return 1;
			}
			return -1;
		}

		/// <summary>Returns the child accessible object that has keyboard focus.</summary>
		/// <returns>null in all cases.</returns>
		public override AccessibleObject GetFocused()
		{
			return null;
		}

		/// <summary>Returns the child accessible object that is currently selected.</summary>
		/// <returns>null in all cases.</returns>
		public override AccessibleObject GetSelected()
		{
			return null;
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell" /> at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation" /> value.</returns>
		/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
		{
			switch (navigationDirection)
			{
			default:
				return null;
			case AccessibleNavigation.Up:
			case AccessibleNavigation.Down:
			case AccessibleNavigation.Left:
			case AccessibleNavigation.Right:
			case AccessibleNavigation.Next:
			case AccessibleNavigation.Previous:
				return null;
			}
		}

		/// <summary>Modifies the selection or moves the keyboard focus of the accessible object.</summary>
		/// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override void Select(AccessibleSelection flags)
		{
			switch (flags)
			{
			case AccessibleSelection.TakeFocus:
				dataGridViewCell.dataGridViewOwner.Focus();
				break;
			case AccessibleSelection.AddSelection:
				dataGridViewCell.dataGridViewOwner.SelectedCells.InternalAdd(dataGridViewCell);
				break;
			case AccessibleSelection.RemoveSelection:
				dataGridViewCell.dataGridViewOwner.SelectedCells.InternalRemove(dataGridViewCell);
				break;
			}
		}
	}

	private DataGridView dataGridViewOwner;

	private AccessibleObject accessibilityObject;

	private int columnIndex;

	private ContextMenuStrip contextMenuStrip;

	private bool displayed;

	private string errorText;

	private bool isInEditMode;

	private DataGridViewRow owningRow;

	private DataGridViewTriState readOnly;

	private bool selected;

	private DataGridViewCellStyle style;

	private object tag;

	private string toolTipText;

	private object valuex;

	private Type valueType;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public AccessibleObject AccessibilityObject
	{
		get
		{
			if (accessibilityObject == null)
			{
				accessibilityObject = CreateAccessibilityInstance();
			}
			return accessibilityObject;
		}
	}

	/// <summary>Gets the column index for this cell. </summary>
	/// <returns>The index of the column that contains the cell; -1 if the cell is not contained within a column.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex
	{
		get
		{
			if (base.DataGridView == null)
			{
				return -1;
			}
			return columnIndex;
		}
	}

	/// <summary>Gets the bounding rectangle that encloses the cell's content area.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property is less than 0, indicating that the cell is a row header cell.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Rectangle ContentBounds => GetContentBounds(RowIndex);

	/// <summary>Gets or sets the shortcut menu associated with the cell. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the cell.</returns>
	[DefaultValue(null)]
	public virtual ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return contextMenuStrip;
		}
		set
		{
			contextMenuStrip = value;
		}
	}

	/// <summary>Gets the default value for a cell in the row for new records.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the default value.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual object DefaultNewRowValue => null;

	/// <summary>Gets a value that indicates whether the cell is currently displayed on-screen. </summary>
	/// <returns>true if the cell is on-screen or partially on-screen; otherwise, false.</returns>
	[Browsable(false)]
	public virtual bool Displayed => displayed;

	/// <summary>Gets the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed. </summary>
	/// <returns>The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell. </exception>
	/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public object EditedFormattedValue => GetEditedFormattedValue(RowIndex, DataGridViewDataErrorContexts.Formatting);

	/// <summary>Gets the type of the cell's hosted editing control. </summary>
	/// <returns>A <see cref="T:System.Type" /> representing the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> type.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public virtual Type EditType => typeof(DataGridViewTextBoxEditingControl);

	/// <summary>Gets the bounds of the error icon for the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the error icon for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or- <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public Rectangle ErrorIconBounds
	{
		get
		{
			if (this is DataGridViewTopLeftHeaderCell)
			{
				return GetErrorIconBounds(null, null, RowIndex);
			}
			if (base.DataGridView == null || columnIndex < 0)
			{
				throw new InvalidOperationException();
			}
			if (RowIndex < 0 || RowIndex >= base.DataGridView.Rows.Count)
			{
				throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
			}
			return GetErrorIconBounds(null, null, RowIndex);
		}
	}

	/// <summary>Gets or sets the text describing an error condition associated with the cell. </summary>
	/// <returns>The text that describes an error condition associated with the cell.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public string ErrorText
	{
		get
		{
			if (this is DataGridViewTopLeftHeaderCell)
			{
				return GetErrorText(-1);
			}
			if (OwningRow == null)
			{
				return string.Empty;
			}
			return GetErrorText(OwningRow.Index);
		}
		set
		{
			if (errorText != value)
			{
				errorText = value;
				OnErrorTextChanged(new DataGridViewCellEventArgs(ColumnIndex, RowIndex));
			}
		}
	}

	/// <summary>Gets the value of the cell as formatted for display.</summary>
	/// <returns>The formatted value of the cell or null if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public object FormattedValue
	{
		get
		{
			if (base.DataGridView == null)
			{
				return null;
			}
			DataGridViewCellStyle cellStyle = InheritedStyle;
			return GetFormattedValue(Value, RowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
		}
	}

	/// <summary>Gets the type of the formatted value associated with the cell. </summary>
	/// <returns>A <see cref="T:System.Type" /> representing the type of the cell's formatted value.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual Type FormattedValueType => null;

	/// <summary>Gets a value indicating whether the cell is frozen. </summary>
	/// <returns>true if the cell is frozen; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual bool Frozen
	{
		get
		{
			if (base.DataGridView == null)
			{
				return false;
			}
			if (RowIndex >= 0)
			{
				return OwningRow.Frozen && OwningColumn.Frozen;
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property has been set.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property has been set; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool HasStyle => style != null;

	/// <summary>Gets the current state of the cell as inherited from the state of its row and column.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
	/// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property is not -1.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> property is -1.</exception>
	[Browsable(false)]
	public DataGridViewElementStates InheritedState => GetInheritedState(RowIndex);

	/// <summary>Gets the style currently applied to the cell. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> currently applied to the cell.</returns>
	/// <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or- <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	[Browsable(false)]
	public DataGridViewCellStyle InheritedStyle => GetInheritedStyle(null, RowIndex, includeColors: true);

	/// <summary>Gets a value indicating whether this cell is currently being edited.</summary>
	/// <returns>true if the cell is in edit mode; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.</exception>
	[Browsable(false)]
	public bool IsInEditMode
	{
		get
		{
			if (base.DataGridView == null)
			{
				return false;
			}
			if (RowIndex == -1)
			{
				throw new InvalidOperationException("Operation cannot be performed on a cell of a shared row.");
			}
			return isInEditMode;
		}
	}

	/// <summary>Gets the column that contains this cell.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> that contains the cell, or null if the cell is not in a column.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public DataGridViewColumn OwningColumn
	{
		get
		{
			if (base.DataGridView == null || columnIndex < 0 || columnIndex >= base.DataGridView.Columns.Count)
			{
				return null;
			}
			return base.DataGridView.Columns[columnIndex];
		}
	}

	/// <summary>Gets the row that contains this cell. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that contains the cell, or null if the cell is not in a row.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public DataGridViewRow OwningRow => owningRow;

	/// <summary>Gets the size, in pixels, of a rectangular area into which the cell can fit. </summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public Size PreferredSize
	{
		get
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			return GetPreferredSize(Hwnd.GraphicsContext, InheritedStyle, RowIndex, Size.Empty);
		}
	}

	/// <summary>Gets or sets a value indicating whether the cell's data can be edited. </summary>
	/// <returns>true if the cell's data cannot be edited; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">There is no owning row when setting this property. -or-The owning row is shared when setting this property.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual bool ReadOnly
	{
		get
		{
			if (base.DataGridView != null && base.DataGridView.ReadOnly)
			{
				return true;
			}
			if (readOnly != 0)
			{
				return readOnly == DataGridViewTriState.True;
			}
			if (OwningRow != null && !OwningRow.IsShared && OwningRow.ReadOnly)
			{
				return true;
			}
			if (OwningColumn != null && OwningColumn.ReadOnly)
			{
				return true;
			}
			return false;
		}
		set
		{
			readOnly = (value ? DataGridViewTriState.True : DataGridViewTriState.False);
			if (value)
			{
				SetState(DataGridViewElementStates.ReadOnly | State);
			}
			else
			{
				SetState(~DataGridViewElementStates.ReadOnly & State);
			}
		}
	}

	/// <summary>Gets a value indicating whether the cell can be resized. </summary>
	/// <returns>true if the cell can be resized; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual bool Resizable
	{
		get
		{
			if (base.DataGridView == null)
			{
				return false;
			}
			if (RowIndex == -1 || columnIndex == -1)
			{
				return false;
			}
			return OwningRow.Resizable == DataGridViewTriState.True || OwningColumn.Resizable == DataGridViewTriState.True;
		}
	}

	/// <summary>Gets the index of the cell's parent row. </summary>
	/// <returns>The index of the row that contains the cell; -1 if there is no owning row.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int RowIndex
	{
		get
		{
			if (owningRow == null)
			{
				return -1;
			}
			return owningRow.Index;
		}
	}

	/// <summary>Gets or sets a value indicating whether the cell has been selected. </summary>
	/// <returns>true if the cell has been selected; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView" /> when setting this property. -or-The owning row is shared when setting this property.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual bool Selected
	{
		get
		{
			if (selected)
			{
				return true;
			}
			if (base.DataGridView != null)
			{
				if (RowIndex >= 0 && RowIndex < base.DataGridView.Rows.Count && base.DataGridView.Rows[RowIndex].Selected)
				{
					return true;
				}
				if (ColumnIndex >= 0 && ColumnIndex < base.DataGridView.Columns.Count && base.DataGridView.Columns[ColumnIndex].Selected)
				{
					return true;
				}
			}
			return false;
		}
		set
		{
			bool flag = selected != value;
			selected = value;
			if (value != ((State & DataGridViewElementStates.Selected) != 0))
			{
				SetState(State ^ DataGridViewElementStates.Selected);
			}
			if (!selected && OwningRow != null && OwningRow.Selected)
			{
				OwningRow.Selected = false;
				if (columnIndex != 0 && OwningRow.Cells.Count > 0)
				{
					OwningRow.Cells[0].Selected = true;
				}
				else if (OwningRow.Cells.Count > 1)
				{
					OwningRow.Cells[1].Selected = true;
				}
			}
			if (flag && base.DataGridView != null && base.DataGridView.IsHandleCreated)
			{
				base.DataGridView.Invalidate();
			}
		}
	}

	/// <summary>Gets the size of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> set to the owning row's height and the owning column's width. </returns>
	/// <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Size Size
	{
		get
		{
			if (base.DataGridView == null)
			{
				return new Size(-1, -1);
			}
			return GetSize(RowIndex);
		}
	}

	/// <summary>Gets or sets the style for the cell. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(true)]
	public DataGridViewCellStyle Style
	{
		get
		{
			if (style == null)
			{
				style = new DataGridViewCellStyle();
				style.StyleChanged += OnStyleChanged;
			}
			return style;
		}
		set
		{
			style = value;
		}
	}

	/// <summary>Gets or sets the object that contains supplemental data about the cell. </summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the cell. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(false)]
	[TypeConverter("System.ComponentModel.StringConverter, System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Bindable(true, BindingDirection.OneWay)]
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

	/// <summary>Gets or sets the ToolTip text associated with this cell.</summary>
	/// <returns>The ToolTip text associated with the cell. The default is <see cref="F:System.String.Empty" />. </returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string ToolTipText
	{
		get
		{
			return (toolTipText != null) ? toolTipText : string.Empty;
		}
		set
		{
			toolTipText = value;
		}
	}

	/// <summary>Gets or sets the value associated with this cell. </summary>
	/// <returns>Gets or sets the data to be displayed by the cell. The default is null.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public object Value
	{
		get
		{
			return GetValue(RowIndex);
		}
		set
		{
			SetValue(RowIndex, value);
		}
	}

	/// <summary>Gets or sets the data type of the values in the cell. </summary>
	/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual Type ValueType
	{
		get
		{
			if ((object)valueType == null)
			{
				if (DataProperty != null)
				{
					valueType = DataProperty.PropertyType;
				}
				else if (OwningColumn != null)
				{
					valueType = OwningColumn.ValueType;
				}
			}
			return valueType;
		}
		set
		{
			valueType = value;
		}
	}

	/// <summary>Gets a value indicating whether the cell is in a row or column that has been hidden. </summary>
	/// <returns>true if the cell is visible; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual bool Visible
	{
		get
		{
			DataGridViewColumn owningColumn = OwningColumn;
			DataGridViewRow dataGridViewRow = OwningRow;
			bool flag = true;
			bool flag2 = true;
			if (dataGridViewRow == null && owningColumn == null)
			{
				return false;
			}
			if (dataGridViewRow != null)
			{
				flag = !dataGridViewRow.IsShared && dataGridViewRow.Visible;
			}
			if (owningColumn != null)
			{
				flag2 = owningColumn.Index >= 0 && owningColumn.Visible;
			}
			return flag && flag2;
		}
	}

	private PropertyDescriptor DataProperty
	{
		get
		{
			if (OwningColumn != null && OwningColumn.DataColumnIndex != -1 && base.DataGridView != null && base.DataGridView.DataManager != null)
			{
				return base.DataGridView.DataManager.GetItemProperties()[OwningColumn.DataColumnIndex];
			}
			return null;
		}
	}

	private TypeConverter FormattedValueTypeConverter
	{
		get
		{
			if ((object)FormattedValueType != null)
			{
				return TypeDescriptor.GetConverter(FormattedValueType);
			}
			return null;
		}
	}

	private TypeConverter ValueTypeConverter
	{
		get
		{
			if (DataProperty != null && DataProperty.Converter != null)
			{
				return DataProperty.Converter;
			}
			if (Value != null)
			{
				return TypeDescriptor.GetConverter(Value);
			}
			if ((object)ValueType != null)
			{
				return TypeDescriptor.GetConverter(ValueType);
			}
			return null;
		}
	}

	internal virtual Rectangle InternalErrorIconsBounds => GetErrorIconBounds(null, null, -1);

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> class. </summary>
	protected DataGridViewCell()
	{
		columnIndex = -1;
		dataGridViewOwner = null;
		errorText = string.Empty;
	}

	/// <summary>Releases the unmanaged resources and performs other cleanup operations before the <see cref="T:System.Windows.Forms.DataGridViewCell" /> is reclaimed by garbage collection.</summary>
	~DataGridViewCell()
	{
		Dispose(disposing: false);
	}

	internal override void SetState(DataGridViewElementStates state)
	{
		base.SetState(state);
		if (base.DataGridView != null)
		{
			base.DataGridView.OnCellStateChangedInternal(new DataGridViewCellStateChangedEventArgs(this, state));
		}
	}

	/// <summary>Modifies the input cell border style according to the specified criteria. </summary>
	/// <returns>The modified <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" />.</returns>
	/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the cell border style to modify.</param>
	/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the cell border style. </param>
	/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false. </param>
	/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false. </param>
	/// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false. </param>
	/// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
	{
		return dataGridViewAdvancedBorderStyleInput;
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual object Clone()
	{
		DataGridViewCell dataGridViewCell = (DataGridViewCell)Activator.CreateInstance(GetType());
		dataGridViewCell.accessibilityObject = accessibilityObject;
		dataGridViewCell.columnIndex = columnIndex;
		dataGridViewCell.displayed = displayed;
		dataGridViewCell.errorText = errorText;
		dataGridViewCell.isInEditMode = isInEditMode;
		dataGridViewCell.owningRow = owningRow;
		dataGridViewCell.readOnly = readOnly;
		dataGridViewCell.selected = selected;
		dataGridViewCell.style = style;
		dataGridViewCell.tag = tag;
		dataGridViewCell.toolTipText = toolTipText;
		dataGridViewCell.valuex = valuex;
		dataGridViewCell.valueType = valueType;
		return dataGridViewCell;
	}

	/// <summary>Removes the cell's editing control from the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <exception cref="T:System.InvalidOperationException">This cell is not associated with a <see cref="T:System.Windows.Forms.DataGridView" />.-or-The <see cref="P:System.Windows.Forms.DataGridView.EditingControl" /> property of the associated <see cref="T:System.Windows.Forms.DataGridView" /> has a value of null. This is the case, for example, when the control is not in edit mode.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void DetachEditingControl()
	{
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell" />. </summary>
	/// <filterpriority>1</filterpriority>
	public void Dispose()
	{
	}

	/// <summary>Returns the bounding rectangle that encloses the cell's content area using a default <see cref="T:System.Drawing.Graphics" /> and cell style currently in effect for the cell.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1. </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	public Rectangle GetContentBounds(int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return Rectangle.Empty;
		}
		return GetContentBounds(Hwnd.GraphicsContext, InheritedStyle, rowIndex);
	}

	/// <summary>Returns the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.</summary>
	/// <returns>The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <param name="rowIndex">The row index of the cell.</param>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1. </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
	public object GetEditedFormattedValue(int rowIndex, DataGridViewDataErrorContexts context)
	{
		if (base.DataGridView == null)
		{
			return null;
		}
		if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
		{
			throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
		}
		if (IsInEditMode)
		{
			if (base.DataGridView.EditingControl != null)
			{
				return (base.DataGridView.EditingControl as IDataGridViewEditingControl).GetEditingControlFormattedValue(context);
			}
			return (this as IDataGridViewEditingCell).GetEditingCellFormattedValue(context);
		}
		DataGridViewCellStyle cellStyle = InheritedStyle;
		return GetFormattedValue(GetValue(rowIndex), rowIndex, ref cellStyle, null, null, context);
	}

	/// <summary>Gets the inherited shortcut menu for the current cell.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> if the parent <see cref="T:System.Windows.Forms.DataGridView" />, <see cref="T:System.Windows.Forms.DataGridViewRow" />, or <see cref="T:System.Windows.Forms.DataGridViewColumn" /> has a <see cref="T:System.Windows.Forms.ContextMenuStrip" /> assigned; otherwise, null.</returns>
	/// <param name="rowIndex">The row index of the current cell.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and the specified <paramref name="rowIndex" /> is less than 0 or greater than the number of rows in the control minus 1. </exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	public virtual ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return null;
		}
		if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		if (columnIndex < 0)
		{
			throw new InvalidOperationException("cannot perform this on a column header cell");
		}
		if (contextMenuStrip != null)
		{
			return contextMenuStrip;
		}
		if (OwningRow.ContextMenuStrip != null)
		{
			return OwningRow.ContextMenuStrip;
		}
		if (OwningColumn.ContextMenuStrip != null)
		{
			return OwningColumn.ContextMenuStrip;
		}
		return base.DataGridView.ContextMenuStrip;
	}

	/// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row and column.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	/// <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is not -1.-or-<paramref name="rowIndex" /> is not the index of the row containing this cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
	public virtual DataGridViewElementStates GetInheritedState(int rowIndex)
	{
		if (base.DataGridView == null && rowIndex != -1)
		{
			throw new ArgumentException("msg?");
		}
		if (base.DataGridView != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
		{
			throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
		}
		DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.ResizableSet | State;
		DataGridViewColumn owningColumn = OwningColumn;
		DataGridViewRow dataGridViewRow = OwningRow;
		if (base.DataGridView == null)
		{
			if (dataGridViewRow != null)
			{
				if (dataGridViewRow.Resizable == DataGridViewTriState.True)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Resizable;
				}
				if (dataGridViewRow.Visible)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Visible;
				}
				if (dataGridViewRow.ReadOnly)
				{
					dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
				}
				if (dataGridViewRow.Frozen)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Frozen;
				}
				if (dataGridViewRow.Displayed)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Displayed;
				}
				if (dataGridViewRow.Selected)
				{
					dataGridViewElementStates |= DataGridViewElementStates.Selected;
				}
			}
			return dataGridViewElementStates;
		}
		if (owningColumn != null)
		{
			if (owningColumn.Resizable == DataGridViewTriState.True && dataGridViewRow.Resizable == DataGridViewTriState.True)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Resizable;
			}
			if (owningColumn.Visible && dataGridViewRow.Visible)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Visible;
			}
			if (owningColumn.ReadOnly || dataGridViewRow.ReadOnly)
			{
				dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
			}
			if (owningColumn.Frozen || dataGridViewRow.Frozen)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Frozen;
			}
			if (owningColumn.Displayed && dataGridViewRow.Displayed)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Displayed;
			}
			if (owningColumn.Selected || dataGridViewRow.Selected)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Selected;
			}
		}
		return dataGridViewElementStates;
	}

	/// <summary>Gets the style applied to the cell. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	/// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be populated with the inherited cell style. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param>
	/// <exception cref="T:System.InvalidOperationException">The cell has no associated <see cref="T:System.Windows.Forms.DataGridView" />.-or-<see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than 0, or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
	public virtual DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
	{
		if (base.DataGridView == null)
		{
			throw new InvalidOperationException("Cell is not in a DataGridView. The cell cannot retrieve the inherited cell style.");
		}
		if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle(base.DataGridView.DefaultCellStyle);
		if (OwningColumn != null)
		{
			dataGridViewCellStyle.ApplyStyle(OwningColumn.DefaultCellStyle);
		}
		dataGridViewCellStyle.ApplyStyle(base.DataGridView.RowsDefaultCellStyle);
		if (rowIndex % 2 == 1)
		{
			dataGridViewCellStyle.ApplyStyle(base.DataGridView.AlternatingRowsDefaultCellStyle);
		}
		dataGridViewCellStyle.ApplyStyle(base.DataGridView.Rows.SharedRow(rowIndex).DefaultCellStyle);
		if (HasStyle)
		{
			dataGridViewCellStyle.ApplyStyle(Style);
		}
		return dataGridViewCellStyle;
	}

	/// <summary>Initializes the control used to edit the cell.</summary>
	/// <param name="rowIndex">The zero-based row index of the cell's location.</param>
	/// <param name="initialFormattedValue">An <see cref="T:System.Object" /> that represents the value displayed by the cell when editing is started.</param>
	/// <param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView" /> or if one is present, it does not have an associated editing control. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
	{
		if (base.DataGridView == null || base.DataGridView.EditingControl == null)
		{
			throw new InvalidOperationException("No editing control defined");
		}
	}

	/// <summary>Determines if edit mode should be started based on the given key.</summary>
	/// <returns>true if edit mode should be started; otherwise, false. The default is false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
	/// <filterpriority>1</filterpriority>
	public virtual bool KeyEntersEditMode(KeyEventArgs e)
	{
		return false;
	}

	/// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics.</summary>
	/// <returns>The height, in pixels, of the text.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
	/// <param name="maxWidth">The maximum width of the text.</param>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxWidth" /> is less than 1.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags)
	{
		if (graphics == null)
		{
			throw new ArgumentNullException("Graphics argument null");
		}
		if (font == null)
		{
			throw new ArgumentNullException("Font argument null");
		}
		if (maxWidth < 1)
		{
			throw new ArgumentOutOfRangeException("maxWidth is less than 1.");
		}
		return TextRenderer.MeasureText(graphics, text, font, new Size(maxWidth, 0), flags).Height;
	}

	/// <summary>Gets the height, in pixels, of the specified text, given the specified characteristics. Also indicates whether the required width is greater than the specified maximum width.</summary>
	/// <returns>The height, in pixels, of the text.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
	/// <param name="maxWidth">The maximum width of the text.</param>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
	/// <param name="widthTruncated">Set to true if the required width of the text is greater than <paramref name="maxWidth" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxWidth" /> is less than 1.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[System.MonoTODO("does not use widthTruncated parameter")]
	public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags, out bool widthTruncated)
	{
		widthTruncated = false;
		return TextRenderer.MeasureText(graphics, text, font, new Size(maxWidth, 0), flags).Height;
	}

	/// <summary>Gets the ideal height and width of the specified text given the specified characteristics.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the preferred height and width of the text.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
	/// <param name="maxRatio">The maximum width-to-height ratio of the block of text.</param>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxRatio" /> is less than or equal to 0.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static Size MeasureTextPreferredSize(Graphics graphics, string text, Font font, float maxRatio, TextFormatFlags flags)
	{
		if (graphics == null)
		{
			throw new ArgumentNullException("Graphics argument null");
		}
		if (font == null)
		{
			throw new ArgumentNullException("Font argument null");
		}
		if (maxRatio <= 0f)
		{
			throw new ArgumentOutOfRangeException("maxRatio is less than or equals to 0.");
		}
		return MeasureTextSize(graphics, text, font, flags);
	}

	/// <summary>Gets the height and width of the specified text given the specified characteristics.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the text.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static Size MeasureTextSize(Graphics graphics, string text, Font font, TextFormatFlags flags)
	{
		return TextRenderer.MeasureText(graphics, text, font, Size.Empty, flags);
	}

	/// <summary>Gets the width, in pixels, of the specified text given the specified characteristics.</summary>
	/// <returns>The width, in pixels, of the text.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to render the text.</param>
	/// <param name="text">The text to measure.</param>
	/// <param name="font">The <see cref="T:System.Drawing.Font" /> applied to the text.</param>
	/// <param name="maxHeight">The maximum height of the text.</param>
	/// <param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values to apply to the text.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="graphics" /> is null.-or-<paramref name="font" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="maxHeight" /> is less than 1.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="flags" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags" />  values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static int MeasureTextWidth(Graphics graphics, string text, Font font, int maxHeight, TextFormatFlags flags)
	{
		if (graphics == null)
		{
			throw new ArgumentNullException("Graphics argument null");
		}
		if (font == null)
		{
			throw new ArgumentNullException("Font argument null");
		}
		if (maxHeight < 1)
		{
			throw new ArgumentOutOfRangeException("maxHeight is less than 1.");
		}
		return TextRenderer.MeasureText(graphics, text, font, new Size(0, maxHeight), flags).Width;
	}

	/// <summary>Converts a value formatted for display to an actual cell value.</summary>
	/// <returns>The cell value.</returns>
	/// <param name="formattedValue">The display value of the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="cellStyle" /> is null.</exception>
	/// <exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property value is null.-or-The <see cref="P:System.Windows.Forms.DataGridViewCell.ValueType" /> property value is null.-or-<paramref name="formattedValue" /> cannot be converted.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="formattedValue" /> is null.-or-The type of <paramref name="formattedValue" /> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property. </exception>
	public virtual object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
	{
		if (cellStyle == null)
		{
			throw new ArgumentNullException("cellStyle is null.");
		}
		if ((object)FormattedValueType == null)
		{
			throw new FormatException("The System.Windows.Forms.DataGridViewCell.FormattedValueType property value is null.");
		}
		if (formattedValue == null)
		{
			throw new ArgumentException("formattedValue is null.");
		}
		if ((object)ValueType == null)
		{
			throw new FormatException("valuetype is null");
		}
		if (!FormattedValueType.IsAssignableFrom(formattedValue.GetType()))
		{
			throw new ArgumentException("formattedValue is not of formattedValueType.");
		}
		if (formattedValueTypeConverter == null)
		{
			formattedValueTypeConverter = FormattedValueTypeConverter;
		}
		if (valueTypeConverter == null)
		{
			valueTypeConverter = ValueTypeConverter;
		}
		if (valueTypeConverter != null && valueTypeConverter.CanConvertFrom(FormattedValueType))
		{
			return valueTypeConverter.ConvertFrom(formattedValue);
		}
		if (formattedValueTypeConverter != null && formattedValueTypeConverter.CanConvertTo(ValueType))
		{
			return formattedValueTypeConverter.ConvertTo(formattedValue, ValueType);
		}
		return Convert.ChangeType(formattedValue, ValueType);
	}

	/// <summary>Sets the location and size of the editing control hosted by a cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
	/// <param name="setLocation">true to have the control placed as specified by the other arguments; false to allow the control to place itself.</param>
	/// <param name="setSize">true to specify the size; false to allow the control to size itself. </param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds. </param>
	/// <param name="cellClip">The area that will be used to paint the editing control.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
	/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
	/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
	/// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false.</param>
	/// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false.</param>
	/// <exception cref="T:System.InvalidOperationException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
	{
		if (base.DataGridView.EditingControl != null)
		{
			if (setLocation && setSize)
			{
				base.DataGridView.EditingControl.Bounds = cellBounds;
			}
			else if (setLocation)
			{
				base.DataGridView.EditingControl.Location = cellBounds.Location;
			}
			else if (setSize)
			{
				base.DataGridView.EditingControl.Size = cellBounds.Size;
			}
		}
	}

	/// <summary>Sets the location and size of the editing panel hosted by the cell, and returns the normal bounds of the editing control within the editing panel.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the normal bounds of the editing control within the editing panel.</returns>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds. </param>
	/// <param name="cellClip">The area that will be used to paint the editing panel.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
	/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
	/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
	/// <param name="isFirstDisplayedColumn">true if the cell is in the first column currently displayed in the control; otherwise, false.</param>
	/// <param name="isFirstDisplayedRow">true if the cell is in the first row currently displayed in the control; otherwise, false.</param>
	/// <exception cref="T:System.InvalidOperationException">The cell has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a string that describes the current object. </summary>
	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"{GetType().Name} {{ ColumnIndex={ColumnIndex}, RowIndex={RowIndex} }}";
	}

	/// <summary>Returns a <see cref="T:System.Drawing.Rectangle" /> that represents the widths of all the cell margins. </summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the widths of all the cell margins.</returns>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that the margins are to be calculated for. </param>
	protected virtual Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
	{
		Rectangle empty = Rectangle.Empty;
		empty.X = BorderToWidth(advancedBorderStyle.Left);
		empty.Y = BorderToWidth(advancedBorderStyle.Top);
		empty.Width = BorderToWidth(advancedBorderStyle.Right);
		empty.Height = BorderToWidth(advancedBorderStyle.Bottom);
		if (OwningColumn != null)
		{
			empty.Width += OwningColumn.DividerWidth;
		}
		if (OwningRow != null)
		{
			empty.Height += OwningRow.DividerHeight;
		}
		return empty;
	}

	private int BorderToWidth(DataGridViewAdvancedCellBorderStyle style)
	{
		switch (style)
		{
		case DataGridViewAdvancedCellBorderStyle.None:
			return 0;
		default:
			return 1;
		case DataGridViewAdvancedCellBorderStyle.InsetDouble:
		case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			return 2;
		}
	}

	/// <summary>Indicates whether the cell's row will be unshared when the cell is clicked.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
	protected virtual bool ClickUnsharesRow(DataGridViewCellEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether the cell's row will be unshared when the cell's content is clicked.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
	protected virtual bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether the cell's row will be unshared when the cell's content is double-clicked.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
	protected virtual bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
	{
		return false;
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewCell" />. </returns>
	protected virtual AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewCellAccessibleObject(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Indicates whether the cell's row will be unshared when the cell is double-clicked.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)" /> method.</param>
	protected virtual bool DoubleClickUnsharesRow(DataGridViewCellEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether the parent row will be unshared when the focus moves to the cell.</summary>
	/// <returns>true if the row will be unshared; otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
	protected virtual bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
	{
		return false;
	}

	/// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</returns>
	/// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
	/// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="format">The current format string of the cell.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the control.</exception>
	/// <exception cref="T:System.InvalidOperationException">The value of the cell's <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property is null.-or-<see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0, indicating that the cell is a row header cell.</exception>
	/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
	protected virtual object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
	{
		if (base.DataGridView == null)
		{
			return null;
		}
		if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
		{
			throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
		}
		string text = null;
		if (Selected)
		{
			DataGridViewCellStyle inheritedStyle = GetInheritedStyle(null, rowIndex, includeColors: false);
			text = GetEditedFormattedValue(rowIndex, DataGridViewDataErrorContexts.Formatting | DataGridViewDataErrorContexts.ClipboardContent) as string;
		}
		if (text == null)
		{
			text = string.Empty;
		}
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		string text5 = string.Empty;
		string text6 = string.Empty;
		string text7 = string.Empty;
		if (format == DataFormats.UnicodeText || format == DataFormats.Text)
		{
			if (lastCell && !inLastRow)
			{
				text6 = Environment.NewLine;
			}
			else if (!lastCell)
			{
				text6 = "\t";
			}
		}
		else if (format == DataFormats.CommaSeparatedValue)
		{
			if (lastCell && !inLastRow)
			{
				text6 = Environment.NewLine;
			}
			else if (!lastCell)
			{
				text6 = ",";
			}
		}
		else
		{
			if (!(format == DataFormats.Html))
			{
				return text;
			}
			if (inFirstRow && firstCell)
			{
				text2 = "<TABLE>";
			}
			if (inLastRow && lastCell)
			{
				text5 = "</TABLE>";
			}
			if (firstCell)
			{
				text4 = "<TR>";
			}
			if (lastCell)
			{
				text7 = "</TR>";
			}
			text3 = "<TD>";
			text6 = "</TD>";
			if (!Selected)
			{
				text = "&nbsp;";
			}
		}
		return text2 + text4 + text3 + text + text6 + text7 + text5;
	}

	internal object GetClipboardContentInternal(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
	{
		return GetClipboardContent(rowIndex, firstCell, lastCell, inFirstRow, inLastRow, format);
	}

	/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> and cell style.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected virtual Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		return Rectangle.Empty;
	}

	/// <summary>Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected virtual Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		return Rectangle.Empty;
	}

	/// <summary>Returns a string that represents the error for the cell.</summary>
	/// <returns>A string that describes the error for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <param name="rowIndex">The row index of the cell.</param>
	protected internal virtual string GetErrorText(int rowIndex)
	{
		return errorText;
	}

	/// <summary>Gets the value of the cell as formatted for display. </summary>
	/// <returns>The formatted value of the cell or null if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
	/// <param name="value">The value to be formatted. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
	/// <exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event of the <see cref="T:System.Windows.Forms.DataGridView" /> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException" />.</exception>
	protected virtual object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
	{
		if (base.DataGridView == null)
		{
			return null;
		}
		if (rowIndex < 0 || rowIndex >= base.DataGridView.RowCount)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		if (!(this is DataGridViewRowHeaderCell))
		{
			DataGridViewCellFormattingEventArgs dataGridViewCellFormattingEventArgs = new DataGridViewCellFormattingEventArgs(ColumnIndex, rowIndex, value, FormattedValueType, cellStyle);
			base.DataGridView.OnCellFormattingInternal(dataGridViewCellFormattingEventArgs);
			if (dataGridViewCellFormattingEventArgs.FormattingApplied)
			{
				return dataGridViewCellFormattingEventArgs.Value;
			}
			cellStyle = dataGridViewCellFormattingEventArgs.CellStyle;
			value = dataGridViewCellFormattingEventArgs.Value;
		}
		if ((value == null || (cellStyle != null && value == cellStyle.DataSourceNullValue)) && (object)FormattedValueType == typeof(string))
		{
			return string.Empty;
		}
		if ((object)FormattedValueType == typeof(string) && value is IFormattable && !string.IsNullOrEmpty(cellStyle.Format))
		{
			return ((IFormattable)value).ToString(cellStyle.Format, cellStyle.FormatProvider);
		}
		if (value != null && FormattedValueType.IsAssignableFrom(value.GetType()))
		{
			return value;
		}
		if (formattedValueTypeConverter == null)
		{
			formattedValueTypeConverter = FormattedValueTypeConverter;
		}
		if (valueTypeConverter == null)
		{
			valueTypeConverter = ValueTypeConverter;
		}
		if (valueTypeConverter != null && valueTypeConverter.CanConvertTo(FormattedValueType))
		{
			return valueTypeConverter.ConvertTo(value, FormattedValueType);
		}
		if (formattedValueTypeConverter != null && formattedValueTypeConverter.CanConvertFrom(ValueType))
		{
			return formattedValueTypeConverter.ConvertFrom(value);
		}
		return Convert.ChangeType(value, FormattedValueType);
	}

	/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	protected virtual Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		return new Size(-1, -1);
	}

	/// <summary>Gets the size of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the cell's dimensions.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="rowIndex" /> is -1</exception>
	protected virtual Size GetSize(int rowIndex)
	{
		if (RowIndex == -1)
		{
			throw new InvalidOperationException("Getting the Size property of a cell in a shared row is not a valid operation.");
		}
		return new Size(OwningColumn.Width, OwningRow.Height);
	}

	/// <summary>Gets the value of the cell. </summary>
	/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is not null and the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> property is less than 0, indicating that the cell is a row header cell.</exception>
	protected virtual object GetValue(int rowIndex)
	{
		if (base.DataGridView != null && (RowIndex < 0 || RowIndex >= base.DataGridView.Rows.Count))
		{
			throw new ArgumentOutOfRangeException("rowIndex", "Specified argument was out of the range of valid values.");
		}
		if (OwningRow != null && OwningRow.Index == base.DataGridView.NewRowIndex)
		{
			return DefaultNewRowValue;
		}
		if (DataProperty != null && OwningRow.DataBoundItem != null)
		{
			return DataProperty.GetValue(OwningRow.DataBoundItem);
		}
		if (valuex != null)
		{
			return valuex;
		}
		DataGridViewCellValueEventArgs dataGridViewCellValueEventArgs = new DataGridViewCellValueEventArgs(columnIndex, rowIndex);
		if (base.DataGridView != null)
		{
			base.DataGridView.OnCellValueNeeded(dataGridViewCellValueEventArgs);
		}
		return dataGridViewCellValueEventArgs.Value;
	}

	/// <summary>Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared if a key is pressed while a cell in the row has focus.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual bool KeyPressUnsharesRow(KeyPressEventArgs e, int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether the parent row is unshared when the user releases a key while the focus is on the cell.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the focus leaves a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
	protected virtual bool LeaveUnsharesRow(int rowIndex, bool throughMouseClick)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared if the user clicks a mouse button while the pointer is on a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual bool MouseClickUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared if the user double-clicks a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected virtual bool MouseDoubleClickUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the user holds down a mouse button while the pointer is on a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual bool MouseEnterUnsharesRow(int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual bool MouseLeaveUnsharesRow(int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the user releases a mouse button while the pointer is on a cell in the row.</summary>
	/// <returns>true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell" /> class always returns false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return false;
	}

	/// <summary>Called when the cell is clicked.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(DataGridViewCellEventArgs e)
	{
	}

	internal void OnClickInternal(DataGridViewCellEventArgs e)
	{
		OnClick(e);
	}

	/// <summary>Called when the cell's contents are clicked.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected virtual void OnContentClick(DataGridViewCellEventArgs e)
	{
	}

	internal void OnContentClickInternal(DataGridViewCellEventArgs e)
	{
		OnContentClick(e);
	}

	/// <summary>Called when the cell's contents are double-clicked.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected virtual void OnContentDoubleClick(DataGridViewCellEventArgs e)
	{
	}

	internal void OnContentDoubleClickInternal(DataGridViewCellEventArgs e)
	{
		OnContentDoubleClick(e);
	}

	/// <summary>Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell changes.</summary>
	protected override void OnDataGridViewChanged()
	{
	}

	internal void OnDataGridViewChangedInternal()
	{
		OnDataGridViewChanged();
	}

	/// <summary>Called when the cell is double-clicked.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected virtual void OnDoubleClick(DataGridViewCellEventArgs e)
	{
	}

	internal void OnDoubleClickInternal(DataGridViewCellEventArgs e)
	{
		OnDoubleClick(e);
	}

	/// <summary>Called when the focus moves to a cell.</summary>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
	protected virtual void OnEnter(int rowIndex, bool throughMouseClick)
	{
	}

	internal void OnEnterInternal(int rowIndex, bool throughMouseClick)
	{
		OnEnter(rowIndex, throughMouseClick);
	}

	/// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual void OnKeyDown(KeyEventArgs e, int rowIndex)
	{
	}

	internal void OnKeyDownInternal(KeyEventArgs e, int rowIndex)
	{
		OnKeyDown(e, rowIndex);
	}

	/// <summary>Called when a key is pressed while the focus is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual void OnKeyPress(KeyPressEventArgs e, int rowIndex)
	{
	}

	internal void OnKeyPressInternal(KeyPressEventArgs e, int rowIndex)
	{
		OnKeyPress(e, rowIndex);
	}

	/// <summary>Called when a character key is released while the focus is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual void OnKeyUp(KeyEventArgs e, int rowIndex)
	{
	}

	internal void OnKeyUpInternal(KeyEventArgs e, int rowIndex)
	{
		OnKeyUp(e, rowIndex);
	}

	/// <summary>Called when the focus moves from a cell.</summary>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="throughMouseClick">true if a user action moved focus from the cell; false if a programmatic operation moved focus from the cell.</param>
	protected virtual void OnLeave(int rowIndex, bool throughMouseClick)
	{
	}

	internal void OnLeaveInternal(int rowIndex, bool throughMouseClick)
	{
		OnLeave(rowIndex, throughMouseClick);
	}

	/// <summary>Called when the user clicks a mouse button while the pointer is on a cell.  </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseClick(DataGridViewCellMouseEventArgs e)
	{
	}

	internal void OnMouseClickInternal(DataGridViewCellMouseEventArgs e)
	{
		OnMouseClick(e);
	}

	/// <summary>Called when the user double-clicks a mouse button while the pointer is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)
	{
	}

	internal void OnMouseDoubleClickInternal(DataGridViewCellMouseEventArgs e)
	{
		OnMouseDoubleClick(e);
	}

	/// <summary>Called when the user holds down a mouse button while the pointer is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseDown(DataGridViewCellMouseEventArgs e)
	{
	}

	internal void OnMouseDownInternal(DataGridViewCellMouseEventArgs e)
	{
		OnMouseDown(e);
	}

	/// <summary>Called when the mouse pointer moves over a cell.</summary>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual void OnMouseEnter(int rowIndex)
	{
	}

	internal void OnMouseEnterInternal(int rowIndex)
	{
		OnMouseEnter(rowIndex);
	}

	/// <summary>Called when the mouse pointer leaves the cell.</summary>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected virtual void OnMouseLeave(int rowIndex)
	{
	}

	internal void OnMouseLeaveInternal(int e)
	{
		OnMouseLeave(e);
	}

	/// <summary>Called when the mouse pointer moves within a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseMove(DataGridViewCellMouseEventArgs e)
	{
	}

	internal void OnMouseMoveInternal(DataGridViewCellMouseEventArgs e)
	{
		OnMouseMove(e);
	}

	/// <summary>Called when the user releases a mouse button while the pointer is on a cell. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data. </param>
	protected virtual void OnMouseUp(DataGridViewCellMouseEventArgs e)
	{
	}

	internal void OnMouseUpInternal(DataGridViewCellMouseEventArgs e)
	{
		OnMouseUp(e);
	}

	internal void PaintInternal(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	/// <summary>Paints the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
		{
			PaintPartBackground(graphics, cellBounds, cellStyle);
		}
		if ((paintParts & DataGridViewPaintParts.SelectionBackground) == DataGridViewPaintParts.SelectionBackground)
		{
			PaintPartSelectionBackground(graphics, cellBounds, cellState, cellStyle);
		}
		if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
		{
			PaintPartContent(graphics, cellBounds, rowIndex, cellState, cellStyle, formattedValue);
		}
		if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
		{
			PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
		}
		if ((paintParts & DataGridViewPaintParts.Focus) == DataGridViewPaintParts.Focus)
		{
			PaintPartFocus(graphics, cellBounds);
		}
		if ((paintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon && !string.IsNullOrEmpty(ErrorText))
		{
			PaintErrorIcon(graphics, clipBounds, cellBounds, ErrorText);
		}
	}

	/// <summary>Paints the border of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the area of the border that is being painted.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the current cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles of the border that is being painted.</param>
	protected virtual void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
	{
		Pen pen = new Pen(base.DataGridView.GridColor);
		switch (advancedBorderStyle.Left)
		{
		case DataGridViewAdvancedCellBorderStyle.Single:
			if (base.DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.Single)
			{
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
			}
			break;
		case DataGridViewAdvancedCellBorderStyle.Inset:
		case DataGridViewAdvancedCellBorderStyle.Outset:
			graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
			break;
		case DataGridViewAdvancedCellBorderStyle.InsetDouble:
		case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height - 1);
			graphics.DrawLine(pen, bounds.X + 2, bounds.Y, bounds.X + 2, bounds.Y + bounds.Height - 1);
			break;
		}
		switch (advancedBorderStyle.Right)
		{
		case DataGridViewAdvancedCellBorderStyle.Single:
			graphics.DrawLine(pen, bounds.X + bounds.Width - 1, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
			break;
		case DataGridViewAdvancedCellBorderStyle.Inset:
		case DataGridViewAdvancedCellBorderStyle.InsetDouble:
		case DataGridViewAdvancedCellBorderStyle.Outset:
		case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			graphics.DrawLine(pen, bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height - 1);
			break;
		}
		switch (advancedBorderStyle.Top)
		{
		case DataGridViewAdvancedCellBorderStyle.Single:
			if (base.DataGridView.CellBorderStyle != DataGridViewCellBorderStyle.Single)
			{
				graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y);
			}
			break;
		case DataGridViewAdvancedCellBorderStyle.Inset:
		case DataGridViewAdvancedCellBorderStyle.InsetDouble:
		case DataGridViewAdvancedCellBorderStyle.Outset:
		case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			graphics.DrawLine(pen, bounds.X, bounds.Y, bounds.X + bounds.Width - 1, bounds.Y);
			break;
		}
		switch (advancedBorderStyle.Bottom)
		{
		case DataGridViewAdvancedCellBorderStyle.Single:
		case DataGridViewAdvancedCellBorderStyle.Inset:
		case DataGridViewAdvancedCellBorderStyle.InsetDouble:
		case DataGridViewAdvancedCellBorderStyle.Outset:
		case DataGridViewAdvancedCellBorderStyle.OutsetDouble:
			graphics.DrawLine(pen, bounds.X, bounds.Y + bounds.Height - 1, bounds.X + bounds.Width - 1, bounds.Y + bounds.Height - 1);
			break;
		}
	}

	/// <summary>Paints the error icon of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the border.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellValueBounds">The bounding <see cref="T:System.Drawing.Rectangle" /> that encloses the cell's content area.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	protected virtual void PaintErrorIcon(Graphics graphics, Rectangle clipBounds, Rectangle cellValueBounds, string errorText)
	{
		Rectangle errorIconBounds = GetErrorIconBounds(graphics, null, RowIndex);
		if (!errorIconBounds.IsEmpty)
		{
			Point location = errorIconBounds.Location;
			location.X += cellValueBounds.Left;
			location.Y += cellValueBounds.Top;
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X + 1, location.Y + 2, 10, 7));
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X + 2, location.Y + 1, 8, 9));
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X + 4, location.Y, 4, 11));
			graphics.FillRectangle(Brushes.Red, new Rectangle(location.X, location.Y + 4, 12, 3));
			graphics.FillRectangle(Brushes.White, new Rectangle(location.X + 5, location.Y + 2, 2, 4));
			graphics.FillRectangle(Brushes.White, new Rectangle(location.X + 5, location.Y + 7, 2, 2));
		}
	}

	internal virtual void PaintPartBackground(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle style)
	{
		Color backColor = style.BackColor;
		graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(backColor), cellBounds);
	}

	internal Pen GetBorderPen()
	{
		return ThemeEngine.Current.ResPool.GetPen(base.DataGridView.GridColor);
	}

	internal virtual void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
	{
		if (!IsInEditMode)
		{
			Color foreColor = ((!Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
			TextFormatFlags textFormatFlags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			textFormatFlags |= AlignmentToFlags(style.Alignment);
			cellBounds.Height -= 2;
			cellBounds.Width -= 2;
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, cellBounds, foreColor, textFormatFlags);
			}
		}
	}

	private void PaintPartFocus(Graphics graphics, Rectangle cellBounds)
	{
		cellBounds.Width--;
		cellBounds.Height--;
		if (base.DataGridView.ShowFocusCues && base.DataGridView.CurrentCell == this && base.DataGridView.Focused)
		{
			ControlPaint.DrawFocusRectangle(graphics, cellBounds);
		}
	}

	internal virtual void PaintPartSelectionBackground(Graphics graphics, Rectangle cellBounds, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle)
	{
		if ((cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected && (RowIndex < 0 || !IsInEditMode || (object)EditType == null))
		{
			Color selectionBackColor = cellStyle.SelectionBackColor;
			graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(selectionBackColor), cellBounds);
		}
	}

	internal void PaintWork(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		object value;
		object formattedValue;
		if (RowIndex == -1 && !(this is DataGridViewColumnHeaderCell))
		{
			value = null;
			formattedValue = null;
		}
		else if (RowIndex == -1)
		{
			value = Value;
			formattedValue = Value;
		}
		else
		{
			value = Value;
			formattedValue = GetFormattedValue(Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
		}
		DataGridViewCellPaintingEventArgs dataGridViewCellPaintingEventArgs = new DataGridViewCellPaintingEventArgs(base.DataGridView, graphics, clipBounds, cellBounds, rowIndex, columnIndex, cellState, value, formattedValue, ErrorText, cellStyle, advancedBorderStyle, paintParts);
		base.DataGridView.OnCellPaintingInternal(dataGridViewCellPaintingEventArgs);
		if (!dataGridViewCellPaintingEventArgs.Handled)
		{
			dataGridViewCellPaintingEventArgs.Paint(dataGridViewCellPaintingEventArgs.ClipBounds, dataGridViewCellPaintingEventArgs.PaintParts);
		}
	}

	/// <summary>Sets the value of the cell. </summary>
	/// <returns>true if the value has been set; otherwise, false.</returns>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="value">The cell value to set. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex" /> is less than 0.</exception>
	protected virtual bool SetValue(int rowIndex, object value)
	{
		object value2 = Value;
		if (DataProperty != null && !DataProperty.IsReadOnly)
		{
			DataProperty.SetValue(OwningRow.DataBoundItem, value);
		}
		else
		{
			valuex = value;
		}
		if (!object.ReferenceEquals(value2, value) || !object.Equals(value2, value))
		{
			RaiseCellValueChanged(new DataGridViewCellEventArgs(ColumnIndex, RowIndex));
			if (this is IDataGridViewEditingCell)
			{
				(this as IDataGridViewEditingCell).EditingCellValueChanged = false;
			}
			if (base.DataGridView != null)
			{
				base.DataGridView.InvalidateCell(this);
			}
			return true;
		}
		return false;
	}

	private void OnStyleChanged(object sender, EventArgs args)
	{
		if (base.DataGridView != null)
		{
			base.DataGridView.RaiseCellStyleChanged(new DataGridViewCellEventArgs(ColumnIndex, RowIndex));
		}
	}

	internal void InternalPaint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	internal void SetOwningRow(DataGridViewRow row)
	{
		owningRow = row;
	}

	internal void SetOwningColumn(DataGridViewColumn col)
	{
		columnIndex = col.Index;
	}

	internal void SetColumnIndex(int index)
	{
		columnIndex = index;
	}

	internal void SetIsInEditMode(bool isInEditMode)
	{
		this.isInEditMode = isInEditMode;
	}

	internal void OnErrorTextChanged(DataGridViewCellEventArgs args)
	{
		if (base.DataGridView != null)
		{
			base.DataGridView.OnCellErrorTextChanged(args);
		}
	}

	internal TextFormatFlags AlignmentToFlags(DataGridViewContentAlignment align)
	{
		TextFormatFlags textFormatFlags = TextFormatFlags.Left;
		switch (align)
		{
		case DataGridViewContentAlignment.BottomCenter:
			textFormatFlags |= TextFormatFlags.Bottom;
			textFormatFlags |= TextFormatFlags.HorizontalCenter;
			break;
		case DataGridViewContentAlignment.BottomLeft:
			textFormatFlags |= TextFormatFlags.Bottom;
			break;
		case DataGridViewContentAlignment.BottomRight:
			textFormatFlags |= TextFormatFlags.Bottom;
			textFormatFlags |= TextFormatFlags.Right;
			break;
		case DataGridViewContentAlignment.MiddleCenter:
			textFormatFlags |= TextFormatFlags.VerticalCenter;
			textFormatFlags |= TextFormatFlags.HorizontalCenter;
			break;
		case DataGridViewContentAlignment.MiddleLeft:
			textFormatFlags |= TextFormatFlags.VerticalCenter;
			break;
		case DataGridViewContentAlignment.MiddleRight:
			textFormatFlags |= TextFormatFlags.VerticalCenter;
			textFormatFlags |= TextFormatFlags.Right;
			break;
		case DataGridViewContentAlignment.TopLeft:
			textFormatFlags |= TextFormatFlags.Left;
			break;
		case DataGridViewContentAlignment.TopCenter:
			textFormatFlags |= TextFormatFlags.HorizontalCenter;
			textFormatFlags |= TextFormatFlags.Left;
			break;
		case DataGridViewContentAlignment.TopRight:
			textFormatFlags |= TextFormatFlags.Right;
			textFormatFlags |= TextFormatFlags.Left;
			break;
		}
		return textFormatFlags;
	}

	internal Rectangle AlignInRectangle(Rectangle outer, Size inner, DataGridViewContentAlignment align)
	{
		int x = 0;
		int y = 0;
		switch (align)
		{
		case DataGridViewContentAlignment.TopLeft:
		case DataGridViewContentAlignment.MiddleLeft:
		case DataGridViewContentAlignment.BottomLeft:
			x = outer.X;
			break;
		case DataGridViewContentAlignment.TopCenter:
		case DataGridViewContentAlignment.MiddleCenter:
		case DataGridViewContentAlignment.BottomCenter:
			x = Math.Max(outer.X + (outer.Width - inner.Width) / 2, outer.Left);
			break;
		case DataGridViewContentAlignment.TopRight:
		case DataGridViewContentAlignment.MiddleRight:
		case DataGridViewContentAlignment.BottomRight:
			x = Math.Max(outer.Right - inner.Width, outer.X);
			break;
		}
		switch (align)
		{
		case DataGridViewContentAlignment.TopLeft:
		case DataGridViewContentAlignment.TopCenter:
		case DataGridViewContentAlignment.TopRight:
			y = outer.Y;
			break;
		case DataGridViewContentAlignment.MiddleLeft:
		case DataGridViewContentAlignment.MiddleCenter:
		case DataGridViewContentAlignment.MiddleRight:
			y = Math.Max(outer.Y + (outer.Height - inner.Height) / 2, outer.Y);
			break;
		case DataGridViewContentAlignment.BottomLeft:
		case DataGridViewContentAlignment.BottomCenter:
		case DataGridViewContentAlignment.BottomRight:
			y = Math.Max(outer.Bottom - inner.Height, outer.Y);
			break;
		}
		return new Rectangle(x, y, Math.Min(inner.Width, outer.Width), Math.Min(inner.Height, outer.Height));
	}
}
