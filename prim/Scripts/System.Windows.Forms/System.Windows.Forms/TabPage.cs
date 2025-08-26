using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a single tab page in a <see cref="T:System.Windows.Forms.TabControl" />.</summary>
/// <filterpriority>2</filterpriority>
[Designer("System.Windows.Forms.Design.TabPageDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ComVisible(true)]
[ToolboxItem(false)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultEvent("Click")]
[DesignTimeVisible(false)]
[DefaultProperty("Text")]
public class TabPage : Panel
{
	/// <summary>Contains the collection of controls that the <see cref="T:System.Windows.Forms.TabPage" /> uses.</summary>
	[ComVisible(false)]
	public class TabPageControlCollection : ControlCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage.TabPageControlCollection" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.TabPage" /> that contains this collection of controls. </param>
		public TabPageControlCollection(TabPage owner)
			: base(owner)
		{
		}

		/// <summary>Adds a control to the collection.</summary>
		/// <param name="value">The control to add. </param>
		/// <exception cref="T:System.ArgumentException">The specified control is a <see cref="T:System.Windows.Forms.TabPage" />. </exception>
		public override void Add(Control value)
		{
			base.Add(value);
		}
	}

	private int imageIndex = -1;

	private string imageKey;

	private string tooltip_text = string.Empty;

	private Rectangle tab_bounds;

	private int row;

	private bool use_visual_style_back_color;

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>The default value is false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>Always <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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
			base.AutoSizeMode = value;
		}
	}

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DefaultValue("{Width=0, Height=0}")]
	public override Size MaximumSize
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

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public override Size MinimumSize
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

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new Size PreferredSize => base.PreferredSize;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TabPage" /> background renders using the current visual style when visual styles are enabled.</summary>
	/// <returns>true to render the background using the current visual style; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool UseVisualStyleBackColor
	{
		get
		{
			return use_visual_style_back_color;
		}
		set
		{
			use_visual_style_back_color = value;
		}
	}

	/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.TabPage" />. </returns>
	public override Color BackColor
	{
		get
		{
			return base.BackColor;
		}
		set
		{
			use_visual_style_back_color = false;
			base.BackColor = value;
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.AnchorStyles" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override AnchorStyles Anchor
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

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DockStyle" /> value.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override DockStyle Dock
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

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new bool Enabled
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

	/// <summary>Gets or sets the index to the image displayed on this tab.</summary>
	/// <returns>The zero-based index to the image in the <see cref="P:System.Windows.Forms.TabControl.ImageList" /> that appears on the tab. The default is -1, which signifies no image.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.TabPage.ImageIndex" /> value is less than -1. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[TypeConverter(typeof(ImageIndexConverter))]
	[Localizable(true)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue(-1)]
	public int ImageIndex
	{
		get
		{
			return imageIndex;
		}
		set
		{
			if (imageIndex != value)
			{
				imageIndex = value;
				UpdateOwner();
			}
		}
	}

	/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.TabControl.ImageList" /> of the associated <see cref="T:System.Windows.Forms.TabControl" />.</summary>
	/// <returns>A string representing the key of the image.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[TypeConverter(typeof(ImageKeyConverter))]
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue("")]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string ImageKey
	{
		get
		{
			return imageKey;
		}
		set
		{
			imageKey = value;
			if (base.Parent is TabControl tabControl)
			{
				ImageIndex = tabControl.ImageList.Images.IndexOfKey(imageKey);
			}
		}
	}

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>An <see cref="T:System.Int32" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
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

	/// <summary>Gets or sets the text to display on the tab.</summary>
	/// <returns>The text to display on the tab.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Localizable(true)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			if (!(value == base.Text))
			{
				base.Text = value;
				UpdateOwner();
			}
		}
	}

	/// <summary>Gets or sets the ToolTip text for this tab.</summary>
	/// <returns>The ToolTip text for this tab.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue("")]
	public string ToolTipText
	{
		get
		{
			return tooltip_text;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			tooltip_text = value;
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new bool Visible
	{
		get
		{
			return base.Visible;
		}
		set
		{
		}
	}

	internal Rectangle TabBounds
	{
		get
		{
			return tab_bounds;
		}
		set
		{
			tab_bounds = value;
		}
	}

	internal int Row
	{
		get
		{
			return row;
		}
		set
		{
			row = value;
		}
	}

	private TabControl Owner => base.Parent as TabControl;

	/// <summary>This property is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Point" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.AutoSize" /> property changes.</summary>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Dock" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Enabled" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Location" /> property changes.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabIndex" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabStop" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
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

	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
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

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Visible" /> property changes.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage" /> class.</summary>
	public TabPage()
	{
		Visible = true;
		SetStyle(ControlStyles.CacheText, value: true);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage" /> class and specifies the text for the tab.</summary>
	/// <param name="text">The text for the tab. </param>
	public TabPage(string text)
	{
		base.Text = text;
	}

	/// <summary>Retrieves the tab page that contains the specified object.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> that contains the specified object, or null if the object cannot be found.</returns>
	/// <param name="comp">The object to look for. </param>
	/// <filterpriority>1</filterpriority>
	public static TabPage GetTabPageOfComponent(object comp)
	{
		if (!(comp is Control { Parent: var control2 }))
		{
			return null;
		}
		while (control2 != null && !(control2 is TabPage))
		{
			control2 = control2.Parent;
		}
		return control2 as TabPage;
	}

	/// <summary>Returns a string containing the value of the <see cref="P:System.Windows.Forms.TabPage.Text" /> property.</summary>
	/// <returns>A string containing the value of the <see cref="P:System.Windows.Forms.TabPage.Text" /> property.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return "TabPage: {" + Text + "}";
	}

	private void UpdateOwner()
	{
		if (Owner != null)
		{
			Owner.Redraw();
		}
	}

	internal void SetVisible(bool value)
	{
		base.Visible = value;
	}

	/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
	protected override ControlCollection CreateControlsInstance()
	{
		return new TabPageControlCollection(this);
	}

	/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" />.</summary>
	/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
	/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
	/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
	/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
	/// <param name="specified">A bitwise combination of <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
	protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
	{
		if (Owner != null && Owner.IsHandleCreated)
		{
			Rectangle displayRectangle = Owner.DisplayRectangle;
			base.SetBoundsCore(displayRectangle.X, displayRectangle.Y, displayRectangle.Width, displayRectangle.Height, BoundsSpecified.All);
		}
		else
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event of the <see cref="T:System.Windows.Forms.TabPage" />. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnEnter(EventArgs e)
	{
		base.OnEnter(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnLeave(EventArgs e)
	{
		base.OnLeave(e);
	}

	/// <summary>Paints the background of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains data useful for painting the background. </param>
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
	}
}
