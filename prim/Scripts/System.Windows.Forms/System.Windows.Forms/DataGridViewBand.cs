using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a linear collection of elements in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewBand : DataGridViewElement, IDisposable, ICloneable
{
	private ContextMenuStrip contextMenuStrip;

	private DataGridViewCellStyle defaultCellStyle;

	private Type defaultHeaderCellType;

	private bool displayed;

	private bool frozen;

	private int index = -1;

	private bool readOnly;

	private DataGridViewTriState resizable;

	private bool selected;

	private object tag;

	private bool visible = true;

	private DataGridViewHeaderCell headerCellCore;

	private bool isRow;

	private DataGridViewCellStyle inheritedStyle;

	/// <summary>Gets or sets the shortcut menu for the band.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewBand" />. The default is null.</returns>
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

	/// <summary>Gets or sets the default cell style of the band.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public virtual DataGridViewCellStyle DefaultCellStyle
	{
		get
		{
			if (defaultCellStyle == null)
			{
				defaultCellStyle = new DataGridViewCellStyle();
			}
			return defaultCellStyle;
		}
		set
		{
			defaultCellStyle = value;
		}
	}

	/// <summary>Gets or sets the run-time type of the default header cell.</summary>
	/// <returns>A <see cref="T:System.Type" /> that describes the run-time class of the object used as the default header cell.</returns>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Type" /> representing <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> or a derived type. </exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Type DefaultHeaderCellType
	{
		get
		{
			return defaultHeaderCellType;
		}
		set
		{
			if (!value.IsSubclassOf(typeof(DataGridViewHeaderCell)))
			{
				throw new ArgumentException("Type is not DataGridViewHeaderCell or a derived type.");
			}
			defaultHeaderCellType = value;
		}
	}

	/// <summary>Gets a value indicating whether the band is currently displayed onscreen. </summary>
	/// <returns>true if the band is currently onscreen; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public virtual bool Displayed => displayed;

	/// <summary>Gets or sets a value indicating whether the band will move when a user scrolls through the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <returns>true if the band cannot be scrolled from view; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public virtual bool Frozen
	{
		get
		{
			return frozen;
		}
		set
		{
			if (frozen != value)
			{
				frozen = value;
				if (frozen)
				{
					SetState(State | DataGridViewElementStates.Frozen);
				}
				else
				{
					SetState(State & ~DataGridViewElementStates.Frozen);
				}
			}
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property has been set. </summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle" /> property has been set; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public bool HasDefaultCellStyle => defaultCellStyle != null;

	/// <summary>Gets the relative position of the band within the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	/// <returns>The zero-based position of the band in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection" /> or <see cref="T:System.Windows.Forms.DataGridViewColumnCollection" /> that it is contained within. The default is -1, indicating that there is no associated <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public int Index => index;

	/// <summary>Gets the cell style in effect for the current band, taking into account style inheritance.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand" />. The default is null.</returns>
	[Browsable(false)]
	public virtual DataGridViewCellStyle InheritedStyle => inheritedStyle;

	/// <summary>Gets or sets a value indicating whether the user can edit the band's cells.</summary>
	/// <returns>true if the user cannot edit the band's cells; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is a shared <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public virtual bool ReadOnly
	{
		get
		{
			return readOnly;
		}
		set
		{
			if (readOnly != value)
			{
				readOnly = value;
				if (readOnly)
				{
					SetState(State | DataGridViewElementStates.ReadOnly);
				}
				else
				{
					SetState(State & ~DataGridViewElementStates.ReadOnly);
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the band can be resized in the user interface (UI).</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(true)]
	public virtual DataGridViewTriState Resizable
	{
		get
		{
			if (resizable == DataGridViewTriState.NotSet && base.DataGridView != null)
			{
				return base.DataGridView.AllowUserToResizeColumns ? DataGridViewTriState.True : DataGridViewTriState.False;
			}
			return resizable;
		}
		set
		{
			if (value != resizable)
			{
				resizable = value;
				if (resizable == DataGridViewTriState.True)
				{
					SetState(State | DataGridViewElementStates.Resizable);
				}
				else
				{
					SetState(State & ~DataGridViewElementStates.Resizable);
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the band is in a selected user interface (UI) state.</summary>
	/// <returns>true if the band is selected; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is true, but the band has not been added to a <see cref="T:System.Windows.Forms.DataGridView" /> control. -or-This property is being set on a shared <see cref="T:System.Windows.Forms.DataGridViewRow" />.</exception>
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
			return selected;
		}
		set
		{
			if (base.DataGridView == null)
			{
				throw new InvalidOperationException("Cant select a row non associated with a DataGridView.");
			}
			if (isRow)
			{
				base.DataGridView.SetSelectedRowCoreInternal(Index, value);
			}
			else
			{
				base.DataGridView.SetSelectedColumnCoreInternal(Index, value);
			}
		}
	}

	internal bool SelectedInternal
	{
		get
		{
			return selected;
		}
		set
		{
			if (selected != value)
			{
				selected = value;
				if (selected)
				{
					SetState(State | DataGridViewElementStates.Selected);
				}
				else
				{
					SetState(State & ~DataGridViewElementStates.Selected);
				}
			}
		}
	}

	internal bool DisplayedInternal
	{
		get
		{
			return displayed;
		}
		set
		{
			if (value != displayed)
			{
				displayed = value;
				if (displayed)
				{
					SetState(State | DataGridViewElementStates.Displayed);
				}
				else
				{
					SetState(State & ~DataGridViewElementStates.Displayed);
				}
			}
		}
	}

	/// <summary>Gets or sets the object that contains data to associate with the band.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains information associated with the band. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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

	/// <summary>Gets or sets a value indicating whether the band is visible to the user.</summary>
	/// <returns>true if the band is visible; otherwise, false. The default is true.</returns>
	/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and the band is the row for new records.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	public virtual bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			if (visible != value)
			{
				visible = value;
				if (visible)
				{
					SetState(State | DataGridViewElementStates.Visible);
				}
				else
				{
					SetState(State & ~DataGridViewElementStates.Visible);
				}
			}
		}
	}

	/// <summary>Gets or sets the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> representing the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
	/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell" /> and this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is of type <see cref="T:System.Windows.Forms.DataGridViewRow" />.-or-The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> and this <see cref="T:System.Windows.Forms.DataGridViewBand" /> instance is of type <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected DataGridViewHeaderCell HeaderCellCore
	{
		get
		{
			return headerCellCore;
		}
		set
		{
			headerCellCore = value;
		}
	}

	/// <summary>Gets a value indicating whether the band represents a row.</summary>
	/// <returns>true if the band represents a <see cref="T:System.Windows.Forms.DataGridViewRow" />; otherwise, false.</returns>
	protected bool IsRow => isRow;

	internal DataGridViewBand()
	{
		defaultHeaderCellType = typeof(DataGridViewHeaderCell);
		isRow = this is DataGridViewRow;
	}

	/// <summary>Releases the resources associated with the band.</summary>
	~DataGridViewBand()
	{
		Dispose();
	}

	/// <summary>Creates an exact copy of this band.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual object Clone()
	{
		return new DataGridViewBand();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" />.  </summary>
	/// <filterpriority>1</filterpriority>
	public void Dispose()
	{
	}

	/// <summary>Returns a string that represents the current band.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name + ": " + index + ".";
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand" /> and optionally releases the managed resources.  </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Called when the band is associated with a different <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	protected override void OnDataGridViewChanged()
	{
	}

	internal virtual void SetIndex(int index)
	{
		this.index = index;
	}
}
