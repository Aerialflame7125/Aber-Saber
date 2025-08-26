using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms;

/// <summary>Represents a line used to group items of a <see cref="T:System.Windows.Forms.ToolStrip" /> or the drop-down items of a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" /> or other <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
/// <filterpriority>2</filterpriority>
[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
public class ToolStripSeparator : ToolStripItem
{
	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false. </returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new bool AutoToolTip
	{
		get
		{
			return base.AutoToolTip;
		}
		set
		{
			base.AutoToolTip = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Image BackgroundImage
	{
		get
		{
			return base.BackgroundImage;
		}
		set
		{
			base.BackgroundImage = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override ImageLayout BackgroundImageLayout
	{
		get
		{
			return base.BackgroundImageLayout;
		}
		set
		{
			base.BackgroundImageLayout = value;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be selected. </summary>
	/// <returns>true if the component using the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is in design mode; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public override bool CanSelect => false;

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> value.</returns>
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
	public new ToolStripItemDisplayStyle DisplayStyle
	{
		get
		{
			return base.DisplayStyle;
		}
		set
		{
			base.DisplayStyle = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false. </returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new bool DoubleClickEnabled
	{
		get
		{
			return base.DoubleClickEnabled;
		}
		set
		{
			base.DoubleClickEnabled = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
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
	public override bool Enabled
	{
		get
		{
			return base.Enabled;
		}
		set
		{
			base.Enabled = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Font" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override Font Font
	{
		get
		{
			return base.Font;
		}
		set
		{
			base.Font = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
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
	public override Image Image
	{
		get
		{
			return base.Image;
		}
		set
		{
			base.Image = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.ContentAlignment" /> value.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new ContentAlignment ImageAlign
	{
		get
		{
			return base.ImageAlign;
		}
		set
		{
			base.ImageAlign = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>An <see cref="T:System.Int32" />.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new int ImageIndex
	{
		get
		{
			return base.ImageIndex;
		}
		set
		{
			base.ImageIndex = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.String" />.</returns>
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
	public new string ImageKey
	{
		get
		{
			return base.ImageKey;
		}
		set
		{
			base.ImageKey = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" /> value.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ToolStripItemImageScaling ImageScaling
	{
		get
		{
			return base.ImageScaling;
		}
		set
		{
			base.ImageScaling = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Color ImageTransparentColor
	{
		get
		{
			return base.ImageTransparentColor;
		}
		set
		{
			base.ImageTransparentColor = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>true if enabled; otherwise, false.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool RightToLeftAutoMirrorImage
	{
		get
		{
			return base.RightToLeftAutoMirrorImage;
		}
		set
		{
			base.RightToLeftAutoMirrorImage = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.String" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.ContentAlignment" /> value.</returns>
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
	public new ContentAlignment TextAlign
	{
		get
		{
			return base.TextAlign;
		}
		set
		{
			base.TextAlign = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> value.</returns>
	[DefaultValue(ToolStripTextDirection.Horizontal)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override ToolStripTextDirection TextDirection
	{
		get
		{
			return base.TextDirection;
		}
		set
		{
			base.TextDirection = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TextImageRelation" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new TextImageRelation TextImageRelation
	{
		get
		{
			return base.TextImageRelation;
		}
		set
		{
			base.TextImageRelation = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A string.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new string ToolTipText
	{
		get
		{
			return base.ToolTipText;
		}
		set
		{
			base.ToolTipText = value;
		}
	}

	protected internal override Padding DefaultMargin => default(Padding);

	protected override Size DefaultSize => new Size(6, 6);

	internal override ToolStripTextDirection DefaultTextDirection => ToolStripTextDirection.Horizontal;

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler DisplayStyleChanged
	{
		add
		{
			base.DisplayStyleChanged += value;
		}
		remove
		{
			base.DisplayStyleChanged -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler EnabledChanged
	{
		add
		{
			base.EnabledChanged += value;
		}
		remove
		{
			base.EnabledChanged -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event EventHandler TextChanged
	{
		add
		{
			base.TextChanged += value;
		}
		remove
		{
			base.TextChanged -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> class. </summary>
	public ToolStripSeparator()
	{
	}

	/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be fitted.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</returns>
	/// <param name="constrainingSize">A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</param>
	/// <filterpriority>1</filterpriority>
	public override Size GetPreferredSize(Size constrainingSize)
	{
		return new Size(6, 6);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		ToolStripItemAccessibleObject toolStripItemAccessibleObject = new ToolStripItemAccessibleObject(this);
		toolStripItemAccessibleObject.default_action = "Press";
		toolStripItemAccessibleObject.role = AccessibleRole.Separator;
		toolStripItemAccessibleObject.state = AccessibleStates.None;
		return toolStripItemAccessibleObject;
	}

	/// <summary>This method is not relevant to this class.</summary>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		if (base.Owner != null)
		{
			if (base.IsOnDropDown)
			{
				base.Owner.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, (base.Owner.Orientation != 0) ? true : false));
			}
			else
			{
				base.Owner.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, base.Owner.Orientation == Orientation.Horizontal));
			}
		}
	}

	/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
	/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> specifying the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
	protected internal override void SetBounds(Rectangle rect)
	{
		base.SetBounds(rect);
	}
}
