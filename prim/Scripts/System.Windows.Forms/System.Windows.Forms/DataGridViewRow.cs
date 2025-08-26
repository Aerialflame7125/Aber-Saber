using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a row in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
[TypeConverter(typeof(DataGridViewRowConverter))]
public class DataGridViewRow : DataGridViewBand
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewRow" /> to accessibility client applications.</summary>
	[ComVisible(true)]
	protected class DataGridViewRowAccessibleObject : AccessibleObject
	{
		private DataGridViewRow dataGridViewRow;

		/// <summary>Gets the location and size of the accessible object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override Rectangle Bounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override string Name => "Index: " + dataGridViewRow.Index;

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> to which this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> applies.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property has already been set.</exception>
		public DataGridViewRow Owner
		{
			get
			{
				return dataGridViewRow;
			}
			set
			{
				dataGridViewRow = value;
			}
		}

		/// <summary>Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject Parent => dataGridViewRow.AccessibilityObject;

		/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.Row" /> value.</returns>
		public override AccessibleRole Role => AccessibleRole.Row;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is the bitwise combination of the <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" /> and <see cref="F:System.Windows.Forms.AccessibleStates.Focusable" /> values.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleStates State
		{
			get
			{
				if (dataGridViewRow.Selected)
				{
					return AccessibleStates.Selected;
				}
				return AccessibleStates.Focused;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</summary>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override string Value
		{
			get
			{
				if (dataGridViewRow.Cells.Count == 0)
				{
					return "(Create New)";
				}
				string text = string.Empty;
				foreach (DataGridViewCell cell in dataGridViewRow.Cells)
				{
					text += cell.AccessibilityObject.Value;
				}
				return text;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> class without setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property.</summary>
		public DataGridViewRowAccessibleObject()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> class, setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property to the specified <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /></param>
		public DataGridViewRowAccessibleObject(DataGridViewRow owner)
		{
			dataGridViewRow = owner;
		}

		/// <summary>Returns the accessible child corresponding to the specified index.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell" /> corresponding to the specified index.</returns>
		/// <param name="index">The zero-based index of the accessible child.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than 0.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject GetChild(int index)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the number of children belonging to the accessible object.</summary>
		/// <returns>The number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> corresponds to the number of visible columns in the <see cref="T:System.Windows.Forms.DataGridView" />. If the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible" /> property is true, the <see cref="M:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.GetChildCount" /> method includes the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> in the count of child accessible objects.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override int GetChildCount()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the accessible object that has keyboard focus.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" /> if the cell indicated by the <see cref="P:System.Windows.Forms.DataGridView.CurrentCell" /> property has keyboard focus and is in the current <see cref="T:System.Windows.Forms.DataGridViewRow" />; otherwise, null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject GetFocused()
		{
			return null;
		}

		/// <summary>Gets an accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects.</summary>
		/// <returns>An accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell" /> objects in the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleObject GetSelected()
		{
			return null;
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
		/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
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
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner" /> property is null.</exception>
		public override void Select(AccessibleSelection flags)
		{
			switch (flags)
			{
			case AccessibleSelection.TakeFocus:
				dataGridViewRow.DataGridView.Focus();
				break;
			case AccessibleSelection.AddSelection:
				dataGridViewRow.DataGridView.SelectedRows.InternalAdd(dataGridViewRow);
				break;
			case AccessibleSelection.RemoveSelection:
				dataGridViewRow.DataGridView.SelectedRows.InternalRemove(dataGridViewRow);
				break;
			}
		}
	}

	private AccessibleObject accessibilityObject;

	private DataGridViewCellCollection cells;

	private ContextMenuStrip contextMenuStrip;

	private int dividerHeight;

	private string errorText;

	private DataGridViewRowHeaderCell headerCell;

	private int height;

	private int minimumHeight;

	private int explicit_height;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public AccessibleObject AccessibilityObject => accessibilityObject;

	/// <summary>Gets the collection of cells that populate the row.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellCollection" /> that contains all of the cells in the row.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public DataGridViewCellCollection Cells => cells;

	/// <summary>Gets or sets the shortcut menu for the row.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewRow" />. The default is null.</returns>
	/// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	[DefaultValue(null)]
	public override ContextMenuStrip ContextMenuStrip
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Operation cannot be performed on a shared row.");
			}
			return contextMenuStrip;
		}
		set
		{
			if (contextMenuStrip != value)
			{
				contextMenuStrip = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnRowContextMenuStripChanged(new DataGridViewRowEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets the data-bound object that populated the row.</summary>
	/// <returns>The data-bound <see cref="T:System.Object" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public object DataBoundItem
	{
		get
		{
			if (base.DataGridView != null && base.DataGridView.DataManager != null && base.DataGridView.DataManager.Count > base.Index)
			{
				return base.DataGridView.DataManager[base.Index];
			}
			return null;
		}
	}

	/// <summary>Gets or sets the default styles for the row, which are used to render cells in the row unless the styles are overridden. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[NotifyParentProperty(true)]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public override DataGridViewCellStyle DefaultCellStyle
	{
		get
		{
			return base.DefaultCellStyle;
		}
		set
		{
			if (DefaultCellStyle != value)
			{
				base.DefaultCellStyle = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnRowDefaultCellStyleChanged(new DataGridViewRowEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets a value indicating whether this row is displayed on the screen.</summary>
	/// <returns>true if the row is currently displayed on the screen; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	[Browsable(false)]
	public override bool Displayed
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the Displayed property of a shared row is not a valid operation.");
			}
			return base.Displayed;
		}
	}

	/// <summary>Gets or sets the height, in pixels, of the row divider.</summary>
	/// <returns>The height, in pixels, of the divider (the row's bottom margin). </returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	[NotifyParentProperty(true)]
	[DefaultValue(0)]
	public int DividerHeight
	{
		get
		{
			return dividerHeight;
		}
		set
		{
			dividerHeight = value;
		}
	}

	/// <summary>Gets or sets the error message text for row-level errors.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the error message.</returns>
	/// <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is a shared row in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <filterpriority>1</filterpriority>
	[NotifyParentProperty(true)]
	[DefaultValue("")]
	public string ErrorText
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Operation cannot be performed on a shared row.");
			}
			return (errorText != null) ? errorText : string.Empty;
		}
		set
		{
			if (errorText != value)
			{
				errorText = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnRowErrorTextChanged(new DataGridViewRowEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the row is frozen. </summary>
	/// <returns>true if the row is frozen; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public override bool Frozen
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the Frozen property of a shared row is not a valid operation.");
			}
			return base.Frozen;
		}
		set
		{
			base.Frozen = value;
		}
	}

	/// <summary>Gets or sets the row's header cell.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> that represents the header cell of row.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DataGridViewRowHeaderCell HeaderCell
	{
		get
		{
			return headerCell;
		}
		set
		{
			if (headerCell != value)
			{
				headerCell = value;
				headerCell.SetOwningRow(this);
				if (base.DataGridView != null)
				{
					headerCell.SetDataGridView(base.DataGridView);
					base.DataGridView.OnRowHeaderCellChanged(new DataGridViewRowEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the current height of the row.</summary>
	/// <returns>The height, in pixels, of the row. The default is the height of the default font plus 9 pixels.</returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(22)]
	[NotifyParentProperty(true)]
	public int Height
	{
		get
		{
			if (height < 0)
			{
				if (DefaultCellStyle != null && DefaultCellStyle.Font != null)
				{
					return DefaultCellStyle.Font.Height + 9;
				}
				if (base.Index >= 0 && InheritedStyle != null && InheritedStyle.Font != null)
				{
					return InheritedStyle.Font.Height + 9;
				}
				return Control.DefaultFont.Height + 9;
			}
			return height;
		}
		set
		{
			explicit_height = value;
			if (height != value)
			{
				if (value < minimumHeight)
				{
					throw new ArgumentOutOfRangeException("Height can't be less than MinimumHeight.");
				}
				height = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.Invalidate();
					base.DataGridView.OnRowHeightChanged(new DataGridViewRowEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets the cell style in effect for the row.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that specifies the formatting and style information for the cells in the row.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	public override DataGridViewCellStyle InheritedStyle
	{
		get
		{
			if (base.Index == -1)
			{
				throw new InvalidOperationException("Getting the InheritedStyle property of a shared row is not a valid operation.");
			}
			if (base.DataGridView == null)
			{
				return DefaultCellStyle;
			}
			if (DefaultCellStyle == null)
			{
				return base.DataGridView.DefaultCellStyle;
			}
			return DefaultCellStyle.Clone();
		}
	}

	/// <summary>Gets a value indicating whether the row is the row for new records.</summary>
	/// <returns>true if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" />, which is used for the entry of a new row of data; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsNewRow
	{
		get
		{
			if (base.DataGridView != null && base.DataGridView.Rows[base.DataGridView.Rows.Count - 1] == this && base.DataGridView.NewRowIndex == base.Index)
			{
				return true;
			}
			return false;
		}
	}

	internal bool IsShared => base.Index == -1 && base.DataGridView != null;

	/// <summary>Gets or sets the minimum height of the row.</summary>
	/// <returns>The minimum row height in pixels, ranging from 2 to <see cref="F:System.Int32.MaxValue" />. The default is 3.</returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int MinimumHeight
	{
		get
		{
			return minimumHeight;
		}
		set
		{
			if (minimumHeight != value)
			{
				if (value < 2 || value > int.MaxValue)
				{
					throw new ArgumentOutOfRangeException("MinimumHeight should be between 2 and Int32.MaxValue.");
				}
				minimumHeight = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnRowMinimumHeightChanged(new DataGridViewRowEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the row is read-only.</summary>
	/// <returns>true if the row is read-only; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(true)]
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	public override bool ReadOnly
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the ReadOnly property of a shared row is not a valid operation.");
			}
			if (base.DataGridView != null && base.DataGridView.ReadOnly)
			{
				return true;
			}
			return base.ReadOnly;
		}
		set
		{
			base.ReadOnly = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether users can resize the row or indicating that the behavior is inherited from the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewTriState" /> value that indicates whether the row can be resized or whether it can be resized only when the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows" /> property is set to true.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	[NotifyParentProperty(true)]
	public override DataGridViewTriState Resizable
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the Resizable property of a shared row is not a valid operation.");
			}
			return base.Resizable;
		}
		set
		{
			base.Resizable = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the row is selected. </summary>
	/// <returns>true if the row is selected; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	public override bool Selected
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the Selected property of a shared row is not a valid operation.");
			}
			return base.Selected;
		}
		set
		{
			if (base.Index == -1)
			{
				throw new InvalidOperationException("The row is a shared row.");
			}
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException("The row has not been added to a DataGridView control.");
			}
			base.Selected = value;
		}
	}

	/// <summary>Gets the current state of the row.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the row state.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	public override DataGridViewElementStates State
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the State property of a shared row is not a valid operation.");
			}
			return base.State;
		}
	}

	/// <summary>Gets or sets a value indicating whether the row is visible. </summary>
	/// <returns>true if the row is visible; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public override bool Visible
	{
		get
		{
			if (IsShared)
			{
				throw new InvalidOperationException("Getting the Visible property of a shared row is not a valid operation.");
			}
			return base.Visible;
		}
		set
		{
			if (IsNewRow && !value)
			{
				throw new InvalidOperationException("Cant make invisible a new row.");
			}
			if (!value && base.DataGridView != null && base.DataGridView.DataManager != null && base.DataGridView.DataManager.Position == base.Index)
			{
				throw new InvalidOperationException("Row associated with the currency manager's position cannot be made invisible.");
			}
			base.Visible = value;
			if (base.DataGridView != null)
			{
				base.DataGridView.Invalidate();
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> class without using a template.</summary>
	public DataGridViewRow()
	{
		cells = new DataGridViewCellCollection(this);
		minimumHeight = 3;
		height = -1;
		explicit_height = -1;
		headerCell = new DataGridViewRowHeaderCell();
		headerCell.SetOwningRow(this);
		accessibilityObject = new AccessibleObject();
		SetState(DataGridViewElementStates.Visible);
	}

	/// <summary>Modifies an input row header border style according to the specified criteria.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the new border style used.</returns>
	/// <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the row header border style to modify. </param>
	/// <param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that is used to store intermediate changes to the row header border style.</param>
	/// <param name="singleVerticalBorderAdded">true to add a single vertical border to the result; otherwise, false. </param>
	/// <param name="singleHorizontalBorderAdded">true to add a single horizontal border to the result; otherwise, false. </param>
	/// <param name="isFirstDisplayedRow">true if the row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false. </param>
	/// <param name="isLastVisibleRow">true if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has its <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual DataGridViewAdvancedBorderStyle AdjustRowHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedRow, bool isLastVisibleRow)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an exact copy of this row.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewRow dataGridViewRow = (DataGridViewRow)MemberwiseClone();
		dataGridViewRow.HeaderCell = (DataGridViewRowHeaderCell)HeaderCell.Clone();
		dataGridViewRow.SetIndex(-1);
		dataGridViewRow.cells = new DataGridViewCellCollection(dataGridViewRow);
		foreach (DataGridViewCell cell in cells)
		{
			dataGridViewRow.cells.Add(cell.Clone() as DataGridViewCell);
		}
		dataGridViewRow.SetDataGridView(null);
		return dataGridViewRow;
	}

	/// <summary>Clears the existing cells and sets their template according to the supplied <see cref="T:System.Windows.Forms.DataGridView" /> template.</summary>
	/// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView" /> that acts as a template for cell styles. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridView" /> is null. </exception>
	/// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView" /> was added. -or-A column that has no cell template was added.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void CreateCells(DataGridView dataGridView)
	{
		if (dataGridView == null)
		{
			throw new ArgumentNullException("DataGridView is null.");
		}
		if (dataGridView.Rows.Contains(this))
		{
			throw new InvalidOperationException("The row already exists in the DataGridView.");
		}
		DataGridViewCellCollection dataGridViewCellCollection = new DataGridViewCellCollection(this);
		foreach (DataGridViewColumn column in dataGridView.Columns)
		{
			if (column.CellTemplate == null)
			{
				throw new InvalidOperationException("Cell template not set in column: " + column.Index + ".");
			}
			dataGridViewCellCollection.Add((DataGridViewCell)column.CellTemplate.Clone());
		}
		cells = dataGridViewCellCollection;
	}

	/// <summary>Clears the existing cells and sets their template and values.</summary>
	/// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView" /> that acts as a template for cell styles. </param>
	/// <param name="values">An array of objects that initialize the reset cells. </param>
	/// <exception cref="T:System.ArgumentNullException">Either of the parameters is null. </exception>
	/// <exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView" /> was added. -or-A column that has no cell template was added.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void CreateCells(DataGridView dataGridView, params object[] values)
	{
		if (values == null)
		{
			throw new ArgumentNullException("values is null");
		}
		CreateCells(dataGridView);
		for (int i = 0; i < values.Length; i++)
		{
			cells[i].Value = values[i];
		}
	}

	/// <summary>Gets the shortcut menu for the row.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> that belongs to the <see cref="T:System.Windows.Forms.DataGridViewRow" /> at the specified index.</returns>
	/// <param name="rowIndex">The index of the current row.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="rowIndex" /> is -1.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than zero or greater than or equal to the number of rows in the control minus one.</exception>
	public ContextMenuStrip GetContextMenuStrip(int rowIndex)
	{
		if (rowIndex == -1)
		{
			throw new InvalidOperationException("rowIndex is -1");
		}
		if (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count)
		{
			throw new ArgumentOutOfRangeException("rowIndex is out of range");
		}
		return null;
	}

	/// <summary>Gets the error text for the row at the specified index.</summary>
	/// <returns>A string that describes the error of the row at the specified index.</returns>
	/// <param name="rowIndex">The index of the row that contains the error.</param>
	/// <exception cref="T:System.InvalidOperationException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one. </exception>
	public string GetErrorText(int rowIndex)
	{
		return string.Empty;
	}

	/// <summary>Calculates the ideal height of the specified row based on the specified criteria.</summary>
	/// <returns>The ideal height of the row, in pixels.</returns>
	/// <param name="rowIndex">The index of the row whose preferred height is calculated.</param>
	/// <param name="autoSizeRowMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> that specifies an automatic sizing mode.</param>
	/// <param name="fixedWidth">true to calculate the preferred height for a fixed cell width; otherwise, false.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="autoSizeRowMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode" /> value. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="rowIndex" /> is not in the valid range of 0 to the number of rows in the control minus 1. </exception>
	public virtual int GetPreferredHeight(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
	{
		DataGridViewRow dataGridViewRow = ((base.DataGridView == null) ? this : base.DataGridView.Rows.SharedRow(rowIndex));
		int num = 0;
		if (autoSizeRowMode == DataGridViewAutoSizeRowMode.AllCells || autoSizeRowMode == DataGridViewAutoSizeRowMode.RowHeader)
		{
			num = Math.Max(num, dataGridViewRow.HeaderCell.PreferredSize.Height);
		}
		if (autoSizeRowMode == DataGridViewAutoSizeRowMode.AllCells || autoSizeRowMode == DataGridViewAutoSizeRowMode.AllCellsExceptHeader)
		{
			foreach (DataGridViewCell cell in dataGridViewRow.Cells)
			{
				num = Math.Max(num, cell.PreferredSize.Height);
			}
		}
		return num;
	}

	/// <summary>Returns a value indicating the current state of the row.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the row state.</returns>
	/// <param name="rowIndex">The index of the row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row has been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control, but the <paramref name="rowIndex" /> value is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
	/// <exception cref="T:System.ArgumentException">The row is not a shared row, but the <paramref name="rowIndex" /> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index" /> property value.-or-The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control, but the <paramref name="rowIndex" /> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index" /> property value.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual DataGridViewElementStates GetState(int rowIndex)
	{
		DataGridViewElementStates dataGridViewElementStates = DataGridViewElementStates.None;
		if (rowIndex == -1)
		{
			dataGridViewElementStates |= DataGridViewElementStates.Displayed;
			if (base.DataGridView.ReadOnly)
			{
				dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
			}
			if (base.DataGridView.AllowUserToResizeRows)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Resizable;
			}
			if (base.DataGridView.Visible)
			{
				dataGridViewElementStates |= DataGridViewElementStates.Visible;
			}
			return dataGridViewElementStates;
		}
		DataGridViewRow dataGridViewRow = base.DataGridView.Rows[rowIndex];
		if (dataGridViewRow.Displayed)
		{
			dataGridViewElementStates |= DataGridViewElementStates.Displayed;
		}
		if (dataGridViewRow.Frozen)
		{
			dataGridViewElementStates |= DataGridViewElementStates.Frozen;
		}
		if (dataGridViewRow.ReadOnly)
		{
			dataGridViewElementStates |= DataGridViewElementStates.ReadOnly;
		}
		if (dataGridViewRow.Resizable == DataGridViewTriState.True || (dataGridViewRow.Resizable == DataGridViewTriState.NotSet && base.DataGridView.AllowUserToResizeRows))
		{
			dataGridViewElementStates |= DataGridViewElementStates.Resizable;
		}
		if (dataGridViewRow.Resizable == DataGridViewTriState.True)
		{
			dataGridViewElementStates |= DataGridViewElementStates.ResizableSet;
		}
		if (dataGridViewRow.Selected)
		{
			dataGridViewElementStates |= DataGridViewElementStates.Selected;
		}
		if (dataGridViewRow.Visible)
		{
			dataGridViewElementStates |= DataGridViewElementStates.Visible;
		}
		return dataGridViewElementStates;
	}

	/// <summary>Sets the values of the row's cells.</summary>
	/// <returns>true if all values have been set; otherwise, false.</returns>
	/// <param name="values">One or more objects that represent the cell values in the row.-or-An <see cref="T:System.Array" /> of <see cref="T:System.Object" /> values. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="values" /> is null. </exception>
	/// <exception cref="T:System.InvalidOperationException">This method is called when the associated <see cref="T:System.Windows.Forms.DataGridView" /> is operating in virtual mode. -or-This row is a shared row.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool SetValues(params object[] values)
	{
		if (values == null)
		{
			throw new ArgumentNullException("vues is null");
		}
		if (base.DataGridView != null && base.DataGridView.VirtualMode)
		{
			throw new InvalidOperationException("DataGridView is operating in virtual mode");
		}
		for (int i = 0; i < values.Length; i++)
		{
			DataGridViewCell dataGridViewCell;
			if (cells.Count > i)
			{
				dataGridViewCell = cells[i];
			}
			else
			{
				dataGridViewCell = new DataGridViewTextBoxCell();
				cells.Add(dataGridViewCell);
			}
			dataGridViewCell.Value = values[i];
		}
		return true;
	}

	/// <summary>Gets a human-readable string that describes the row.</summary>
	/// <returns>A <see cref="T:System.String" /> that describes this row.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name + ", Band Index: " + base.Index;
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewRow" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewRow" />. </returns>
	protected virtual AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewRowAccessibleObject(this);
	}

	/// <summary>Constructs a new collection of cells based on this row.</summary>
	/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridViewCellCollection" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual DataGridViewCellCollection CreateCellsInstance()
	{
		cells = new DataGridViewCellCollection(this);
		return cells;
	}

	/// <summary>Draws a focus rectangle around the specified bounds.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> used to paint the focus rectangle.</param>
	/// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of <paramref name="cellStyle" /> as the color of the focus rectangle; false to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of <paramref name="cellStyle" /> as the color of the focus rectangle.</param>
	/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="graphics" /> is null.-or-<paramref name="cellStyle" /> is null.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual void DrawFocus(Graphics graphics, Rectangle clipBounds, Rectangle bounds, int rowIndex, DataGridViewElementStates rowState, DataGridViewCellStyle cellStyle, bool cellsPaintSelectionBackground)
	{
	}

	/// <summary>Paints the current row.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
	/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
	/// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
	/// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
	/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and is a shared row.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The row is in a <see cref="T:System.Windows.Forms.DataGridView" /> control and <paramref name="rowIndex" /> is less than zero or greater than the number of rows in the control minus one.</exception>
	protected internal virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
	{
		DataGridViewCellStyle inheritedRowStyle = ((base.Index != -1) ? InheritedStyle : base.DataGridView.RowsDefaultCellStyle);
		DataGridViewRowPrePaintEventArgs dataGridViewRowPrePaintEventArgs = new DataGridViewRowPrePaintEventArgs(base.DataGridView, graphics, clipBounds, rowBounds, rowIndex, rowState, string.Empty, inheritedRowStyle, isFirstDisplayedRow, isLastVisibleRow);
		dataGridViewRowPrePaintEventArgs.PaintParts = DataGridViewPaintParts.All;
		base.DataGridView.OnRowPrePaint(dataGridViewRowPrePaintEventArgs);
		if (!dataGridViewRowPrePaintEventArgs.Handled)
		{
			if (base.DataGridView.RowHeadersVisible)
			{
				PaintHeader(graphics, dataGridViewRowPrePaintEventArgs.ClipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, dataGridViewRowPrePaintEventArgs.PaintParts);
			}
			PaintCells(graphics, dataGridViewRowPrePaintEventArgs.ClipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, dataGridViewRowPrePaintEventArgs.PaintParts);
			DataGridViewRowPostPaintEventArgs e = new DataGridViewRowPostPaintEventArgs(base.DataGridView, graphics, dataGridViewRowPrePaintEventArgs.ClipBounds, rowBounds, rowIndex, rowState, dataGridViewRowPrePaintEventArgs.ErrorText, inheritedRowStyle, isFirstDisplayedRow, isLastVisibleRow);
			base.DataGridView.OnRowPostPaint(e);
		}
	}

	/// <summary>Paints the cells in the current row.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
	/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
	/// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
	/// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
	/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values indicating the parts of the cells to paint.</param>
	/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="paintParts" /> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
	{
		List<DataGridViewColumn> columnDisplayIndexSortedArrayList = base.DataGridView.Columns.ColumnDisplayIndexSortedArrayList;
		Rectangle rectangle = rowBounds;
		if (base.DataGridView.RowHeadersVisible)
		{
			rectangle.X += base.DataGridView.RowHeadersWidth;
			rectangle.Width -= base.DataGridView.RowHeadersWidth;
		}
		for (int i = base.DataGridView.first_col_index; i < columnDisplayIndexSortedArrayList.Count; i++)
		{
			DataGridViewColumn dataGridViewColumn = columnDisplayIndexSortedArrayList[i];
			if (dataGridViewColumn.Visible)
			{
				if (!dataGridViewColumn.Displayed)
				{
					break;
				}
				rectangle.Width = dataGridViewColumn.Width;
				DataGridViewCell dataGridViewCell = Cells[dataGridViewColumn.Index];
				if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
				{
					graphics.FillRectangle(Brushes.White, rectangle);
				}
				DataGridViewCellStyle cellStyle = ((dataGridViewCell.RowIndex != -1) ? dataGridViewCell.InheritedStyle : DefaultCellStyle);
				object value;
				DataGridViewElementStates inheritedState;
				if (dataGridViewCell.RowIndex == -1)
				{
					value = null;
					object obj = null;
					string text = null;
					inheritedState = dataGridViewCell.State;
				}
				else
				{
					value = dataGridViewCell.Value;
					object obj = dataGridViewCell.FormattedValue;
					string text = dataGridViewCell.ErrorText;
					inheritedState = dataGridViewCell.InheritedState;
				}
				DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder = (DataGridViewAdvancedBorderStyle)((ICloneable)base.DataGridView.AdvancedCellBorderStyle).Clone();
				DataGridViewAdvancedBorderStyle advancedBorderStyle = dataGridViewCell.AdjustCellBorderStyle(base.DataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceholder, singleVerticalBorderAdded: true, singleHorizontalBorderAdded: true, dataGridViewCell.ColumnIndex == 0, dataGridViewCell.RowIndex == 0);
				base.DataGridView.OnCellFormattingInternal(new DataGridViewCellFormattingEventArgs(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex, value, dataGridViewCell.FormattedValueType, cellStyle));
				dataGridViewCell.PaintWork(graphics, clipBounds, rectangle, rowIndex, inheritedState, cellStyle, advancedBorderStyle, paintParts);
				rectangle.X += rectangle.Width;
			}
		}
	}

	/// <summary>Paints the header cell of the current row.</summary>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
	/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
	/// <param name="isFirstDisplayedRow">true to indicate that the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
	/// <param name="isLastVisibleRow">true to indicate that the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
	/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values indicating the parts of the cells to paint.</param>
	/// <exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="paintParts" /> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected internal virtual void PaintHeader(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
	{
		rowBounds.Width = base.DataGridView.RowHeadersWidth;
		graphics.FillRectangle(Brushes.White, rowBounds);
		HeaderCell.PaintWork(graphics, clipBounds, rowBounds, rowIndex, rowState, HeaderCell.InheritedStyle, base.DataGridView.AdvancedRowHeadersBorderStyle, paintParts);
	}

	internal override void SetDataGridView(DataGridView dataGridView)
	{
		base.SetDataGridView(dataGridView);
		headerCell.SetDataGridView(dataGridView);
		foreach (DataGridViewCell cell in cells)
		{
			cell.SetDataGridView(dataGridView);
		}
	}

	internal override void SetState(DataGridViewElementStates state)
	{
		if (State != state)
		{
			base.SetState(state);
			if (base.DataGridView != null)
			{
				base.DataGridView.OnRowStateChanged(base.Index, new DataGridViewRowStateChangedEventArgs(this, state));
			}
		}
	}

	internal void SetAutoSizeHeight(int height)
	{
		this.height = height;
		if (base.DataGridView != null)
		{
			base.DataGridView.Invalidate();
			base.DataGridView.OnRowHeightChanged(new DataGridViewRowEventArgs(this));
		}
	}

	internal void ResetToExplicitHeight()
	{
		height = explicit_height;
		if (base.DataGridView != null)
		{
			base.DataGridView.OnRowHeightChanged(new DataGridViewRowEventArgs(this));
		}
	}
}
