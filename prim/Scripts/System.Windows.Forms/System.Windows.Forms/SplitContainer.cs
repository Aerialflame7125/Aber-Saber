using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a control consisting of a movable bar that divides a container's display area into two resizable panels. </summary>
/// <filterpriority>1</filterpriority>
[DefaultEvent("SplitterMoved")]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[Docking(DockingBehavior.AutoDock)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class SplitContainer : ContainerControl
{
	internal class SplitContainerTypedControlCollection : ControlCollection
	{
		public SplitContainerTypedControlCollection(Control owner)
			: base(owner)
		{
		}
	}

	private FixedPanel fixed_panel;

	private Orientation orientation;

	private int splitter_increment;

	private Rectangle splitter_rectangle;

	private Rectangle splitter_rectangle_moving;

	private Rectangle splitter_rectangle_before_move;

	private bool splitter_fixed;

	private bool splitter_dragging;

	private int splitter_prev_move;

	private Cursor restore_cursor;

	private double fixed_none_ratio;

	private SplitterPanel panel1;

	private bool panel1_collapsed;

	private int panel1_min_size;

	private SplitterPanel panel2;

	private bool panel2_collapsed;

	private int panel2_min_size;

	private static object SplitterMovedEvent;

	private static object SplitterMovingEvent;

	private static object UIACanResizeChangedEvent;

	/// <summary>When overridden in a derived class, gets or sets a value indicating whether scroll bars automatically appear if controls are placed outside the <see cref="T:System.Windows.Forms.SplitContainer" /> client area. This property is not relevant to this class.</summary>
	/// <returns>true if scroll bars to automatically appear when controls are placed outside the <see cref="T:System.Windows.Forms.SplitContainer" /> client area; otherwise, false. The default is false.</returns>
	[Browsable(false)]
	[Localizable(true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DefaultValue(false)]
	public override bool AutoScroll
	{
		get
		{
			return base.AutoScroll;
		}
		set
		{
			base.AutoScroll = value;
		}
	}

	/// <summary>Gets or sets the size of the auto-scroll margin. This property is not relevant to this class. This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> value that represents the height and width, in pixels, of the auto-scroll margin.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Size AutoScrollMargin
	{
		get
		{
			return base.AutoScrollMargin;
		}
		set
		{
			base.AutoScrollMargin = value;
		}
	}

	/// <summary>Gets or sets the minimum size of the scroll bar. This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width of the scroll bar, in pixels.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new Size AutoScrollMinSize
	{
		get
		{
			return base.AutoScrollMinSize;
		}
		set
		{
			base.AutoScrollMinSize = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
	[Browsable(false)]
	[DefaultValue("{X=0,Y=0}")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override Point AutoScrollOffset
	{
		get
		{
			return base.AutoScrollOffset;
		}
		set
		{
			base.AutoScrollOffset = value;
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Point AutoScrollPosition
	{
		get
		{
			return base.AutoScrollPosition;
		}
		set
		{
			base.AutoScrollPosition = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitContainer" /> is automatically resized to display its entire contents. This property is not relevant to this class.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.SplitContainer" /> is automatically resized; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool AutoSize
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

	/// <summary>Gets or sets the background image displayed in the control.</summary>
	/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
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
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
	[Browsable(false)]
	public override BindingContext BindingContext
	{
		get
		{
			return base.BindingContext;
		}
		set
		{
			base.BindingContext = value;
		}
	}

	/// <summary>Gets or sets the style of border for the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the property is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DispId(-504)]
	[DefaultValue(BorderStyle.None)]
	public BorderStyle BorderStyle
	{
		get
		{
			return panel1.BorderStyle;
		}
		set
		{
			if (!Enum.IsDefined(typeof(BorderStyle), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for BorderStyle");
			}
			panel1.BorderStyle = value;
			panel2.BorderStyle = value;
		}
	}

	/// <summary>Gets a collection of child controls. This property is not relevant to this class.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.Control.ControlCollection" /> that contains the child controls.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new ControlCollection Controls => base.Controls;

	/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.SplitContainer" /> borders are attached to the edges of the container.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default value is None.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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

	/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.SplitContainer" /> panel remains the same size when the container is resized.</summary>
	/// <returns>One of the values of <see cref="T:System.Windows.Forms.FixedPanel" />. The default value is None.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.FixedPanel" /> values.</exception>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(FixedPanel.None)]
	public FixedPanel FixedPanel
	{
		get
		{
			return fixed_panel;
		}
		set
		{
			if (!Enum.IsDefined(typeof(FixedPanel), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for FixedPanel");
			}
			fixed_panel = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the splitter is fixed or movable.</summary>
	/// <returns>true if the splitter is fixed; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(false)]
	public bool IsSplitterFixed
	{
		get
		{
			return splitter_fixed;
		}
		set
		{
			splitter_fixed = value;
		}
	}

	/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="T:System.Windows.Forms.SplitContainer" /> panels.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values. The default is Vertical.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Orientation" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(Orientation.Vertical)]
	public Orientation Orientation
	{
		get
		{
			return orientation;
		}
		set
		{
			if (!Enum.IsDefined(typeof(Orientation), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for Orientation");
			}
			if (orientation != value)
			{
				if (value == Orientation.Vertical)
				{
					splitter_rectangle.Width = splitter_rectangle.Height;
					splitter_rectangle.X = splitter_rectangle.Y;
				}
				else
				{
					splitter_rectangle.Height = splitter_rectangle.Width;
					splitter_rectangle.Y = splitter_rectangle.X;
				}
				orientation = value;
				UpdateSplitter();
			}
		}
	}

	/// <summary>Gets or sets the interior spacing, in pixels, between the edges of a <see cref="T:System.Windows.Forms.SplitterPanel" /> and its contents. This property is not relevant to this class.</summary>
	/// <returns>An object of type <see cref="T:System.Windows.Forms.Padding" /> representing the interior spacing.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new Padding Padding
	{
		get
		{
			return base.Padding;
		}
		set
		{
			base.Padding = value;
		}
	}

	/// <summary>Gets the left or top panel of the <see cref="T:System.Windows.Forms.SplitContainer" />, depending on <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</summary>
	/// <returns>If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Vertical, the left panel of the <see cref="T:System.Windows.Forms.SplitContainer" />. If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Horizontal, the top panel of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public SplitterPanel Panel1 => panel1;

	/// <summary>Gets or sets a value determining whether <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is collapsed or expanded.</summary>
	/// <returns>true if <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is collapsed; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Panel1Collapsed
	{
		get
		{
			return panel1_collapsed;
		}
		set
		{
			if (panel1_collapsed != value)
			{
				panel1_collapsed = value;
				panel1.Visible = !value;
				OnUIACanResizeChanged(EventArgs.Empty);
				PerformLayout();
			}
		}
	}

	/// <summary>Gets or sets the minimum distance in pixels of the splitter from the left or top edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the minimum distance in pixels of the splitter from the left or top edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />. The default value is 25 pixels, regardless of <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is incompatible with the orientation. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[Localizable(true)]
	[DefaultValue(25)]
	public int Panel1MinSize
	{
		get
		{
			return panel1_min_size;
		}
		set
		{
			panel1_min_size = value;
		}
	}

	/// <summary>Gets the right or bottom panel of the <see cref="T:System.Windows.Forms.SplitContainer" />, depending on <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</summary>
	/// <returns>If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Vertical, the right panel of the <see cref="T:System.Windows.Forms.SplitContainer" />. If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Horizontal, the bottom panel of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public SplitterPanel Panel2 => panel2;

	/// <summary>Gets or sets a value determining whether <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is collapsed or expanded.</summary>
	/// <returns>true if <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is collapsed; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Panel2Collapsed
	{
		get
		{
			return panel2_collapsed;
		}
		set
		{
			if (panel2_collapsed != value)
			{
				panel2_collapsed = value;
				panel2.Visible = !value;
				OnUIACanResizeChanged(EventArgs.Empty);
				PerformLayout();
			}
		}
	}

	/// <summary>Gets or sets the minimum distance in pixels of the splitter from the right or bottom edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the minimum distance in pixels of the splitter from the right or bottom edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />. The default value is 25 pixels, regardless of <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is incompatible with the orientation.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(25)]
	[RefreshProperties(RefreshProperties.All)]
	public int Panel2MinSize
	{
		get
		{
			return panel2_min_size;
		}
		set
		{
			panel2_min_size = value;
		}
	}

	/// <summary>Gets or sets the location of the splitter, in pixels, from the left or top edge of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the location of the splitter, in pixels, from the left or top edge of the <see cref="T:System.Windows.Forms.SplitContainer" />. The default value is 50 pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
	/// <exception cref="T:System.InvalidOperationException">The value is incompatible with the orientation.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[SettingsBindable(true)]
	[DefaultValue(50)]
	public int SplitterDistance
	{
		get
		{
			if (orientation == Orientation.Vertical)
			{
				return splitter_rectangle.X;
			}
			return splitter_rectangle.Y;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (value < panel1_min_size)
			{
				value = panel1_min_size;
			}
			bool flag = true;
			if (orientation == Orientation.Vertical)
			{
				if (base.Width - (SplitterWidth + value) < panel2_min_size)
				{
					value = base.Width - (SplitterWidth + panel2_min_size);
				}
				if (splitter_rectangle.X != value)
				{
					splitter_rectangle.X = value;
					flag = true;
				}
			}
			else
			{
				if (base.Height - (SplitterWidth + value) < panel2_min_size)
				{
					value = base.Height - (SplitterWidth + panel2_min_size);
				}
				if (splitter_rectangle.Y != value)
				{
					splitter_rectangle.Y = value;
					flag = true;
				}
			}
			if (flag)
			{
				UpdateSplitter();
				OnSplitterMoved(new SplitterEventArgs(base.Left, base.Top, splitter_rectangle.X, splitter_rectangle.Y));
			}
		}
	}

	/// <summary>Gets or sets a value representing the increment of splitter movement in pixels.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the increment of splitter movement in pixels. The default value is one pixel.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than one. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(1)]
	[System.MonoTODO("Stub, never called")]
	[Localizable(true)]
	public int SplitterIncrement
	{
		get
		{
			return splitter_increment;
		}
		set
		{
			splitter_increment = value;
		}
	}

	/// <summary>Gets the size and location of the splitter relative to the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the size and location of the splitter relative to the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Rectangle SplitterRectangle => splitter_rectangle;

	/// <summary>Gets or sets the width of the splitter in pixels.</summary>
	/// <returns>An <see cref="T:System.Int32" /> representing the width of the splitter, in pixels. The default is four pixels.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than one or is incompatible with the orientation. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(4)]
	public int SplitterWidth
	{
		get
		{
			if (orientation == Orientation.Vertical)
			{
				return splitter_rectangle.Width;
			}
			return splitter_rectangle.Height;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (orientation == Orientation.Vertical)
			{
				splitter_rectangle.Width = value;
			}
			else
			{
				splitter_rectangle.Height = value;
			}
			UpdateSplitter();
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to the splitter using the TAB key.</summary>
	/// <returns>true if the user can give the focus to the splitter using the TAB key; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DispId(-516)]
	[DefaultValue(true)]
	[System.MonoTODO("Stub, never called")]
	public new bool TabStop
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	/// <summary>This property is not relevant to this class.</summary>
	/// <returns>A string.</returns>
	[Browsable(false)]
	[Bindable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
	protected override Size DefaultSize => new Size(150, 100);

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitContainer.AutoSize" /> property changes. This property is not relevant to this class.</summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.SplitContainer.BackgroundImage" /> property changes. </summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public new event EventHandler BackgroundImageChanged
	{
		add
		{
			base.BackgroundImageChanged += value;
		}
		remove
		{
			base.BackgroundImageChanged -= value;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.SplitContainer.BackgroundImageLayout" /> property changes. This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler BackgroundImageLayoutChanged
	{
		add
		{
			base.BackgroundImageLayoutChanged += value;
		}
		remove
		{
			base.BackgroundImageLayoutChanged -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event ControlEventHandler ControlAdded
	{
		add
		{
			base.ControlAdded += value;
		}
		remove
		{
			base.ControlAdded -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event ControlEventHandler ControlRemoved
	{
		add
		{
			base.ControlRemoved += value;
		}
		remove
		{
			base.ControlRemoved -= value;
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event EventHandler PaddingChanged
	{
		add
		{
			base.PaddingChanged += value;
		}
		remove
		{
			base.PaddingChanged -= value;
		}
	}

	/// <summary>Occurs when the splitter control is moved.</summary>
	/// <filterpriority>1</filterpriority>
	public event SplitterEventHandler SplitterMoved
	{
		add
		{
			base.Events.AddHandler(SplitterMovedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SplitterMovedEvent, value);
		}
	}

	/// <summary>Occurs when the splitter control is in the process of moving.</summary>
	/// <filterpriority>1</filterpriority>
	public event SplitterCancelEventHandler SplitterMoving
	{
		add
		{
			base.Events.AddHandler(SplitterMovingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SplitterMovingEvent, value);
		}
	}

	/// <summary>This event is not relevant to this class.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	internal event EventHandler UIACanResizeChanged
	{
		add
		{
			base.Events.AddHandler(UIACanResizeChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIACanResizeChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitContainer" /> class.</summary>
	public SplitContainer()
	{
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
		SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		fixed_panel = FixedPanel.None;
		orientation = Orientation.Vertical;
		splitter_rectangle = new Rectangle(50, 0, 4, base.Height);
		splitter_increment = 1;
		splitter_prev_move = -1;
		restore_cursor = null;
		splitter_fixed = false;
		panel1_collapsed = false;
		panel2_collapsed = false;
		panel1_min_size = 25;
		panel2_min_size = 25;
		panel1 = new SplitterPanel(this);
		panel2 = new SplitterPanel(this);
		panel1.Size = new Size(50, 50);
		UpdateSplitter();
		Controls.Add(panel2);
		Controls.Add(panel1);
	}

	static SplitContainer()
	{
		SplitterMoved = new object();
		SplitterMoving = new object();
		UIACanResizeChanged = new object();
	}

	internal void OnUIACanResizeChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIACanResizeChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoved" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	public void OnSplitterMoved(SplitterEventArgs e)
	{
		((SplitterEventHandler)base.Events[SplitterMoved])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoving" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	public void OnSplitterMoving(SplitterCancelEventArgs e)
	{
		((SplitterCancelEventHandler)base.Events[SplitterMoving])?.Invoke(this, e);
	}

	/// <summary>Creates a new instance of the control collection for the control.</summary>
	/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override ControlCollection CreateControlsInstance()
	{
		return new SplitContainerTypedControlCollection(this);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
	protected override void OnLayout(LayoutEventArgs e)
	{
		UpdateLayout();
		base.OnLayout(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnLostFocus(EventArgs e)
	{
		base.OnLostFocus(e);
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseCaptureChanged(EventArgs e)
	{
		base.OnMouseCaptureChanged(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (!splitter_fixed && SplitterHitTest(e.Location))
		{
			splitter_dragging = true;
			SplitterBeginMove(e.Location);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		SplitterRestoreCursor();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (splitter_dragging)
		{
			SplitterMove(e.Location);
		}
		if (!splitter_fixed && SplitterHitTest(e.Location))
		{
			SplitterSetCursor(orientation);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (splitter_dragging)
		{
			SplitterEndMove(e.Location, cancel: false);
			SplitterRestoreCursor();
			splitter_dragging = false;
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	/// <summary>Processes a dialog box key.</summary>
	/// <returns>true if the key was processed by the control; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected override bool ProcessDialogKey(Keys keyData)
	{
		return base.ProcessDialogKey(keyData);
	}

	/// <summary>Selects the next available control and makes it the active control.</summary>
	/// <returns>true if a control is selected; otherwise, false.</returns>
	/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
	protected override bool ProcessTabKey(bool forward)
	{
		return base.ProcessTabKey(forward);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		base.ScaleControl(factor, specified);
	}

	protected override void Select(bool directed, bool forward)
	{
		base.Select(directed, forward);
	}

	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
	/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		base.SetBoundsCore(x, y, width, height, specified);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="msg">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
	protected override void WndProc(ref Message msg)
	{
		base.WndProc(ref msg);
	}

	private bool SplitterHitTest(Point location)
	{
		if (location.X >= splitter_rectangle.X && location.X <= splitter_rectangle.X + splitter_rectangle.Width && location.Y >= splitter_rectangle.Y && location.Y <= splitter_rectangle.Y + splitter_rectangle.Height)
		{
			return true;
		}
		return false;
	}

	private void SplitterBeginMove(Point location)
	{
		splitter_prev_move = ((orientation != Orientation.Vertical) ? location.Y : location.X);
		splitter_rectangle_moving = splitter_rectangle;
		splitter_rectangle_before_move = splitter_rectangle;
	}

	private void SplitterMove(Point location)
	{
		int num = ((orientation != Orientation.Vertical) ? location.Y : location.X);
		int num2 = num - splitter_prev_move;
		Rectangle rect = splitter_rectangle_moving;
		bool flag = false;
		if (orientation == Orientation.Vertical)
		{
			int num3 = panel1_min_size;
			int num4 = panel2.Location.X + (panel2.Width - panel2_min_size) - splitter_rectangle_moving.Width;
			if (splitter_rectangle_moving.X + num2 > num3 && splitter_rectangle_moving.X + num2 < num4)
			{
				splitter_rectangle_moving.X += num2;
				flag = true;
			}
			else if (splitter_rectangle_moving.X + num2 <= num3 && splitter_rectangle_moving.X != num3)
			{
				splitter_rectangle_moving.X = num3;
				flag = true;
			}
			else if (splitter_rectangle_moving.X + num2 >= num4 && splitter_rectangle_moving.X != num4)
			{
				splitter_rectangle_moving.X = num4;
				flag = true;
			}
		}
		else if (orientation == Orientation.Horizontal)
		{
			int num5 = panel1_min_size;
			int num6 = panel2.Location.Y + (panel2.Height - panel2_min_size) - splitter_rectangle_moving.Height;
			if (splitter_rectangle_moving.Y + num2 > num5 && splitter_rectangle_moving.Y + num2 < num6)
			{
				splitter_rectangle_moving.Y += num2;
				flag = true;
			}
			else if (splitter_rectangle_moving.Y + num2 <= num5 && splitter_rectangle_moving.Y != num5)
			{
				splitter_rectangle_moving.Y = num5;
				flag = true;
			}
			else if (splitter_rectangle_moving.Y + num2 >= num6 && splitter_rectangle_moving.Y != num6)
			{
				splitter_rectangle_moving.Y = num6;
				flag = true;
			}
		}
		if (flag)
		{
			splitter_prev_move = num;
			OnSplitterMoving(new SplitterCancelEventArgs(location.X, location.Y, splitter_rectangle.X, splitter_rectangle.Y));
			XplatUI.DrawReversibleRectangle(Handle, rect, 1);
			XplatUI.DrawReversibleRectangle(Handle, splitter_rectangle_moving, 1);
		}
	}

	private void SplitterEndMove(Point location, bool cancel)
	{
		if (!cancel && splitter_rectangle_before_move != splitter_rectangle_moving)
		{
			splitter_rectangle = splitter_rectangle_moving;
			UpdateSplitter();
		}
		SplitterEventArgs e = new SplitterEventArgs(location.X, location.Y, splitter_rectangle.X, splitter_rectangle.Y);
		OnSplitterMoved(e);
	}

	private void SplitterSetCursor(Orientation orientation)
	{
		if (restore_cursor == null)
		{
			restore_cursor = Cursor;
		}
		Cursor = ((orientation != Orientation.Vertical) ? Cursors.HSplit : Cursors.VSplit);
	}

	private void SplitterRestoreCursor()
	{
		if (restore_cursor != null)
		{
			Cursor = restore_cursor;
			restore_cursor = null;
		}
	}

	private void UpdateSplitter()
	{
		SuspendLayout();
		panel1.SuspendLayout();
		panel2.SuspendLayout();
		if (panel1_collapsed)
		{
			panel2.Size = base.Size;
			panel2.Location = new Point(0, 0);
		}
		else if (panel2_collapsed)
		{
			panel1.Size = base.Size;
			panel1.Location = new Point(0, 0);
		}
		else
		{
			panel1.Location = new Point(0, 0);
			if (orientation == Orientation.Vertical)
			{
				splitter_rectangle.Y = 0;
				SplitterPanel splitterPanel = panel1;
				int height = base.Height;
				panel2.InternalHeight = height;
				splitterPanel.InternalHeight = height;
				panel1.InternalWidth = Math.Max(SplitterDistance, panel1_min_size);
				panel2.Location = new Point(SplitterWidth + SplitterDistance, 0);
				panel2.InternalWidth = Math.Max(base.Width - (SplitterWidth + SplitterDistance), panel2_min_size);
				fixed_none_ratio = (double)base.Width / (double)SplitterDistance;
			}
			else if (orientation == Orientation.Horizontal)
			{
				splitter_rectangle.X = 0;
				SplitterPanel splitterPanel2 = panel1;
				int height = base.Width;
				panel2.InternalWidth = height;
				splitterPanel2.InternalWidth = height;
				panel1.InternalHeight = Math.Max(SplitterDistance, panel1_min_size);
				panel2.Location = new Point(0, SplitterWidth + SplitterDistance);
				panel2.InternalHeight = Math.Max(base.Height - (SplitterWidth + SplitterDistance), panel2_min_size);
				fixed_none_ratio = (double)base.Height / (double)SplitterDistance;
			}
		}
		panel1.ResumeLayout();
		panel2.ResumeLayout();
		ResumeLayout();
	}

	private void UpdateLayout()
	{
		panel1.SuspendLayout();
		panel2.SuspendLayout();
		if (panel1_collapsed)
		{
			panel2.Size = base.Size;
			panel2.Location = new Point(0, 0);
		}
		else if (panel2_collapsed)
		{
			panel1.Size = base.Size;
			panel1.Location = new Point(0, 0);
		}
		else
		{
			panel1.Location = new Point(0, 0);
			if (orientation == Orientation.Vertical)
			{
				panel1.Location = new Point(0, 0);
				SplitterPanel splitterPanel = panel1;
				int height = base.Height;
				panel2.InternalHeight = height;
				splitterPanel.InternalHeight = height;
				splitter_rectangle.Height = base.Height;
				if (fixed_panel == FixedPanel.None)
				{
					splitter_rectangle.X = Math.Max((int)Math.Floor((double)base.Width / fixed_none_ratio), panel1_min_size);
					panel1.InternalWidth = SplitterDistance;
					panel2.InternalWidth = base.Width - (SplitterWidth + SplitterDistance);
					panel2.Location = new Point(SplitterWidth + SplitterDistance, 0);
				}
				else if (fixed_panel == FixedPanel.Panel1)
				{
					panel1.InternalWidth = SplitterDistance;
					panel2.InternalWidth = Math.Max(base.Width - (SplitterWidth + SplitterDistance), panel2_min_size);
					panel2.Location = new Point(SplitterWidth + SplitterDistance, 0);
				}
				else if (fixed_panel == FixedPanel.Panel2)
				{
					splitter_rectangle.X = Math.Max(base.Width - (SplitterWidth + panel2.Width), panel1_min_size);
					panel1.InternalWidth = SplitterDistance;
					panel2.Location = new Point(SplitterWidth + SplitterDistance, 0);
				}
			}
			else if (orientation == Orientation.Horizontal)
			{
				panel1.Location = new Point(0, 0);
				SplitterPanel splitterPanel2 = panel1;
				int height = base.Width;
				panel2.InternalWidth = height;
				splitterPanel2.InternalWidth = height;
				splitter_rectangle.Width = base.Width;
				if (fixed_panel == FixedPanel.None)
				{
					splitter_rectangle.Y = Math.Max((int)Math.Floor((double)base.Height / fixed_none_ratio), panel1_min_size);
					panel1.InternalHeight = SplitterDistance;
					panel2.InternalHeight = base.Height - (SplitterWidth + SplitterDistance);
					panel2.Location = new Point(0, SplitterWidth + SplitterDistance);
				}
				else if (fixed_panel == FixedPanel.Panel1)
				{
					panel1.InternalHeight = SplitterDistance;
					panel2.InternalHeight = Math.Max(base.Height - (SplitterWidth + SplitterDistance), panel2_min_size);
					panel2.Location = new Point(0, SplitterWidth + SplitterDistance);
				}
				else if (fixed_panel == FixedPanel.Panel2)
				{
					splitter_rectangle.Y = Math.Max(base.Height - (SplitterWidth + panel2.Height), panel1_min_size);
					panel1.InternalHeight = SplitterDistance;
					panel2.Location = new Point(0, SplitterWidth + SplitterDistance);
				}
			}
		}
		panel1.ResumeLayout();
		panel2.ResumeLayout();
	}
}
