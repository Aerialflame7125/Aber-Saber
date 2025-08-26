using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Represents a Windows toolbar button. Although <see cref="T:System.Windows.Forms.ToolStripButton" /> replaces and extends the <see cref="T:System.Windows.Forms.ToolBarButton" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBarButton" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[DefaultProperty("Text")]
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[Designer("System.Windows.Forms.Design.ToolBarButtonDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class ToolBarButton : Component
{
	private bool enabled = true;

	private int image_index = -1;

	private ContextMenu menu;

	private ToolBar parent;

	private bool partial_push;

	private bool pushed;

	private ToolBarButtonStyle style = ToolBarButtonStyle.PushButton;

	private object tag;

	private string text = string.Empty;

	private string tooltip = string.Empty;

	private bool visible = true;

	private string image_key = string.Empty;

	private string name;

	private bool uiaHasFocus;

	private static object UIAGotFocusEvent;

	private static object UIALostFocusEvent;

	private static object UIATextChangedEvent;

	private static object UIAEnabledChangedEvent;

	private static object UIADropDownMenuChangedEvent;

	private static object UIAStyleChangedEvent;

	internal Image Image
	{
		get
		{
			if (Parent == null || Parent.ImageList == null)
			{
				return null;
			}
			ImageList imageList = Parent.ImageList;
			if (ImageIndex > -1 && ImageIndex < imageList.Images.Count)
			{
				return imageList.Images[ImageIndex];
			}
			if (!string.IsNullOrEmpty(image_key))
			{
				return imageList.Images[image_key];
			}
			return null;
		}
	}

	/// <summary>Gets or sets the menu to be displayed in the drop-down toolbar button.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> to be displayed in the drop-down toolbar button. The default is null.</returns>
	/// <exception cref="T:System.ArgumentException">The assigned object is not a <see cref="T:System.Windows.Forms.ContextMenu" />. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[TypeConverter(typeof(ReferenceConverter))]
	public Menu DropDownMenu
	{
		get
		{
			return menu;
		}
		set
		{
			if (value is ContextMenu)
			{
				menu = (ContextMenu)value;
				OnUIADropDownMenuChanged(EventArgs.Empty);
				return;
			}
			throw new ArgumentException("DropDownMenu must be of type ContextMenu.");
		}
	}

	/// <summary>Gets or sets a value indicating whether the button is enabled.</summary>
	/// <returns>true if the button is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	[Localizable(true)]
	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			if (value != enabled)
			{
				enabled = value;
				Invalidate();
				OnUIAEnabledChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the index value of the image assigned to the button.</summary>
	/// <returns>The index value of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button. The default is -1.</returns>
	/// <exception cref="T:System.ArgumentException">The assigned value is less than -1. </exception>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(-1)]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Localizable(true)]
	[TypeConverter(typeof(ImageIndexConverter))]
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
				throw new ArgumentException("ImageIndex value must be above or equal to -1.");
			}
			if (value != image_index)
			{
				bool flag = Parent != null && (value == -1 || image_index == -1);
				image_index = value;
				image_key = string.Empty;
				if (flag)
				{
					Parent.Redraw(recalculate: true);
				}
				else
				{
					Invalidate();
				}
			}
		}
	}

	/// <summary>Gets or sets the name of the image assigned to the button.</summary>
	/// <returns>The name of the <see cref="T:System.Drawing.Image" /> assigned to the toolbar button. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[TypeConverter(typeof(ImageKeyConverter))]
	[Localizable(true)]
	[DefaultValue("")]
	[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[RefreshProperties(RefreshProperties.Repaint)]
	public string ImageKey
	{
		get
		{
			return image_key;
		}
		set
		{
			if (!(image_key == value))
			{
				bool flag = Parent != null && (value == string.Empty || image_key == string.Empty);
				image_index = -1;
				image_key = value;
				if (flag)
				{
					Parent.Redraw(recalculate: true);
				}
				else
				{
					Invalidate();
				}
			}
		}
	}

	/// <summary>The name of the button.</summary>
	/// <returns>The name of the button.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public string Name
	{
		get
		{
			if (name == null)
			{
				return string.Empty;
			}
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>Gets the toolbar control that the toolbar button is assigned to.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolBar" /> control that the <see cref="T:System.Windows.Forms.ToolBarButton" /> is assigned to.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public ToolBar Parent => parent;

	/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is partially pushed.</summary>
	/// <returns>true if a toggle-style toolbar button is partially pushed; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool PartialPush
	{
		get
		{
			return partial_push;
		}
		set
		{
			if (value != partial_push)
			{
				partial_push = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether a toggle-style toolbar button is currently in the pushed state.</summary>
	/// <returns>true if a toggle-style toolbar button is currently in the pushed state; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Pushed
	{
		get
		{
			return pushed;
		}
		set
		{
			if (value != pushed)
			{
				pushed = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets the bounding rectangle for a toolbar button.</summary>
	/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> for a toolbar button.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Rectangle Rectangle
	{
		get
		{
			if (Visible && Parent != null && Parent.items != null)
			{
				ToolBarItem[] items = Parent.items;
				foreach (ToolBarItem toolBarItem in items)
				{
					if (toolBarItem.Button == this)
					{
						return toolBarItem.Rectangle;
					}
				}
			}
			return Rectangle.Empty;
		}
	}

	/// <summary>Gets or sets the style of the toolbar button.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. The default is ToolBarButtonStyle.PushButton.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarButtonStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[DefaultValue(ToolBarButtonStyle.PushButton)]
	public ToolBarButtonStyle Style
	{
		get
		{
			return style;
		}
		set
		{
			if (value != style)
			{
				style = value;
				if (parent != null)
				{
					parent.Redraw(recalculate: true);
				}
				OnUIAStyleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the object that contains data about the toolbar button.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains data about the toolbar button. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Bindable(true)]
	[DefaultValue(null)]
	[Localizable(false)]
	[TypeConverter(typeof(StringConverter))]
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

	/// <summary>Gets or sets the text displayed on the toolbar button.</summary>
	/// <returns>The text displayed on the toolbar button. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	[Localizable(true)]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (!(value == text))
			{
				text = value;
				OnUIATextChanged(EventArgs.Empty);
				if (Parent != null)
				{
					Parent.Redraw(recalculate: true);
				}
			}
		}
	}

	/// <summary>Gets or sets the text that appears as a ToolTip for the button.</summary>
	/// <returns>The text that is displayed when the mouse pointer moves over the toolbar button. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue("")]
	public string ToolTipText
	{
		get
		{
			return tooltip;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			tooltip = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the toolbar button is visible.</summary>
	/// <returns>true if the toolbar button is visible; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	[Localizable(true)]
	public bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			if (value != visible)
			{
				visible = value;
				if (Parent != null)
				{
					Parent.Redraw(recalculate: true);
				}
			}
		}
	}

	internal bool UIAHasFocus
	{
		get
		{
			return uiaHasFocus;
		}
		set
		{
			uiaHasFocus = value;
			((EventHandler)((!value) ? base.Events[UIALostFocus] : base.Events[UIAGotFocus]))?.Invoke(this, EventArgs.Empty);
		}
	}

	internal event EventHandler UIAGotFocus
	{
		add
		{
			base.Events.AddHandler(UIAGotFocusEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAGotFocusEvent, value);
		}
	}

	internal event EventHandler UIALostFocus
	{
		add
		{
			base.Events.AddHandler(UIALostFocusEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIALostFocusEvent, value);
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

	internal event EventHandler UIAEnabledChanged
	{
		add
		{
			base.Events.AddHandler(UIAEnabledChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAEnabledChangedEvent, value);
		}
	}

	internal event EventHandler UIADropDownMenuChanged
	{
		add
		{
			base.Events.AddHandler(UIADropDownMenuChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIADropDownMenuChangedEvent, value);
		}
	}

	internal event EventHandler UIAStyleChanged
	{
		add
		{
			base.Events.AddHandler(UIAStyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIAStyleChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButton" /> class.</summary>
	public ToolBarButton()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBarButton" /> class and displays the assigned text on the button.</summary>
	/// <param name="text">The text to display on the new <see cref="T:System.Windows.Forms.ToolBarButton" />. </param>
	public ToolBarButton(string text)
	{
		this.text = text;
	}

	static ToolBarButton()
	{
		UIAGotFocus = new object();
		UIALostFocus = new object();
		UIATextChanged = new object();
		UIAEnabledChanged = new object();
		UIADropDownMenuChanged = new object();
		UIAStyleChanged = new object();
	}

	internal void SetParent(ToolBar parent)
	{
		if (Parent != parent)
		{
			if (Parent != null)
			{
				Parent.Buttons.Remove(this);
			}
			this.parent = parent;
		}
	}

	internal void Invalidate()
	{
		if (Parent != null)
		{
			Parent.Invalidate(Rectangle);
		}
	}

	private void OnUIATextChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIATextChanged])?.Invoke(this, e);
	}

	private void OnUIAEnabledChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIAEnabledChanged])?.Invoke(this, e);
	}

	private void OnUIADropDownMenuChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIADropDownMenuChanged])?.Invoke(this, e);
	}

	private void OnUIAStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIAStyleChanged])?.Invoke(this, e);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBarButton" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBarButton" /> control.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ToolBarButton" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"ToolBarButton: {text}, Style: {style}";
	}
}
