using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Creates a panel that is associated with a <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
/// <filterpriority>1</filterpriority>
[ToolboxItem(false)]
[Designer("System.Windows.Forms.Design.SplitterPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[Docking(DockingBehavior.Never)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public sealed class SplitterPanel : Panel
{
	/// <summary>Gets or sets how a <see cref="T:System.Windows.Forms.SplitterPanel" /> attaches to the edges of the <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new AnchorStyles Anchor
	{
		get
		{
			return base.Anchor;
		}
		set
		{
			base.Anchor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitterPanel" /> is automatically resized to display its entire contents. This property is not relevant to this class.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.SplitterPanel" /> is automatically resized; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool AutoSize
	{
		get
		{
			return base.AutoSize;
		}
		set
		{
			base.AutoSize = value;
		}
	}

	/// <summary>Enables the <see cref="T:System.Windows.Forms.SplitterPanel" /> to shrink when <see cref="P:System.Windows.Forms.SplitterPanel.AutoSize" /> is true. This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override AutoSizeMode AutoSizeMode
	{
		get
		{
			return base.AutoSizeMode;
		}
		set
		{
		}
	}

	/// <summary>Gets or sets the border style for the <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new BorderStyle BorderStyle
	{
		get
		{
			return base.BorderStyle;
		}
		set
		{
			base.BorderStyle = value;
		}
	}

	/// <summary>Gets or sets which edge of the <see cref="T:System.Windows.Forms.SplitContainer" /> that the <see cref="T:System.Windows.Forms.SplitterPanel" /> is docked to. This property is not relevant to this class.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new DockStyle Dock
	{
		get
		{
			return base.Dock;
		}
		set
		{
			base.Dock = value;
		}
	}

	/// <summary>Gets the internal spacing between the <see cref="T:System.Windows.Forms.SplitterPanel" /> and its edges. This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new DockPaddingEdges DockPadding => base.DockPadding;

	/// <summary>Gets or sets the height of the <see cref="T:System.Windows.Forms.SplitterPanel" />.</summary>
	/// <returns>The height of the <see cref="T:System.Windows.Forms.SplitterPanel" />, in pixels.</returns>
	/// <exception cref="T:System.NotSupportedException">The height cannot be set.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(false)]
	public new int Height
	{
		get
		{
			return Visible ? base.Height : 0;
		}
		set
		{
			throw new NotSupportedException("The height cannot be set");
		}
	}

	/// <summary>Gets or sets the coordinates of the upper-left corner of the <see cref="T:System.Windows.Forms.SplitterPanel" /> relative to the upper-left corner of its <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
	/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.SplitterPanel" /> relative to the upper-left corner of its <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Point Location
	{
		get
		{
			return base.Location;
		}
		set
		{
			base.Location = value;
		}
	}

	/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify. This property is not relevant to this class.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Size MaximumSize
	{
		get
		{
			return base.MaximumSize;
		}
		set
		{
			base.MaximumSize = value;
		}
	}

	/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify. This property is not relevant to this class.</summary>
	/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
	/// <exception cref="T:System.NotSupportedException">The width cannot be set.</exception>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Size MinimumSize
	{
		get
		{
			return base.MinimumSize;
		}
		set
		{
			base.MinimumSize = value;
		}
	}

	/// <summary>The name of this <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the name of this <see cref="T:System.Windows.Forms.SplitterPanel" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new string Name
	{
		get
		{
			return base.Name;
		}
		set
		{
			base.Name = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.SplitContainer" /> that contains this <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" /> representing the <see cref="T:System.Windows.Forms.SplitContainer" /> that contains this <see cref="T:System.Windows.Forms.SplitterPanel" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Control Parent
	{
		get
		{
			return base.Parent;
		}
		set
		{
			throw new NotSupportedException("The parent cannot be set");
		}
	}

	/// <summary>Gets or sets the height and width of the <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
	/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the <see cref="T:System.Windows.Forms.SplitterPanel" /> in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Size Size
	{
		get
		{
			return base.Size;
		}
		set
		{
			base.Size = value;
		}
	}

	/// <summary>Gets or sets the tab order of the <see cref="T:System.Windows.Forms.SplitterPanel" /> within its <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
	/// <returns>The index value of the <see cref="T:System.Windows.Forms.SplitterPanel" /> within the set of other <see cref="T:System.Windows.Forms.SplitterPanel" /> objects within its <see cref="T:System.Windows.Forms.SplitContainer" /> that are included in the tab order.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new int TabIndex
	{
		get
		{
			return base.TabIndex;
		}
		set
		{
			base.TabIndex = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to this <see cref="T:System.Windows.Forms.SplitterPanel" /> using the TAB key. This property is not relevant to this class.</summary>
	/// <returns>true if the user can give the focus to this <see cref="T:System.Windows.Forms.SplitterPanel" /> using the TAB key; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool TabStop
	{
		get
		{
			return base.TabStop;
		}
		set
		{
			base.TabStop = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitterPanel" /> is displayed. This property is not relevant to this class.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.SplitterPanel" /> is displayed; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
			base.Visible = value;
		}
	}

	/// <summary>Gets or sets the width of the <see cref="T:System.Windows.Forms.SplitterPanel" />.</summary>
	/// <returns>The width of the <see cref="T:System.Windows.Forms.SplitterPanel" /> in pixels.</returns>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new int Width
	{
		get
		{
			return Visible ? base.Width : 0;
		}
		set
		{
			throw new NotSupportedException("The width cannot be set");
		}
	}

	protected override Padding DefaultMargin => new Padding(0);

	internal int InternalHeight
	{
		set
		{
			base.Height = value;
		}
	}

	internal int InternalWidth
	{
		set
		{
			base.Width = value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler AutoSizeChanged
	{
		add
		{
			base.AutoSizeChanged += value;
		}
		remove
		{
			base.AutoSizeChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Dock" /> property changes. This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new event EventHandler DockChanged
	{
		add
		{
			base.DockChanged += value;
		}
		remove
		{
			base.DockChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Location" /> property changes. This event is not relevant to this class.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler LocationChanged
	{
		add
		{
			base.LocationChanged += value;
		}
		remove
		{
			base.LocationChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.TabIndex" /> property changes. This event is not relevant to this class.</summary>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TabIndexChanged
	{
		add
		{
			base.TabIndexChanged += value;
		}
		remove
		{
			base.TabIndexChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.TabStop" /> property changes. This event is not relevant to this class.</summary>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler TabStopChanged
	{
		add
		{
			base.TabStopChanged += value;
		}
		remove
		{
			base.TabStopChanged -= value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Visible" /> property changes. This event is not relevant to this class.</summary>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler VisibleChanged
	{
		add
		{
			base.VisibleChanged += value;
		}
		remove
		{
			base.VisibleChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitterPanel" /> class with its specified <see cref="T:System.Windows.Forms.SplitContainer" />. </summary>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.SplitContainer" /> that contains the <see cref="T:System.Windows.Forms.SplitterPanel" />.</param>
	public SplitterPanel(SplitContainer owner)
	{
	}
}
