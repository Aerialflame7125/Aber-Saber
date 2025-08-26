using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Used to group collections of controls.</summary>
/// <filterpriority>1</filterpriority>
[Designer("System.Windows.Forms.Design.PanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("BorderStyle")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Docking(DockingBehavior.Ask)]
[DefaultEvent("Paint")]
public class Panel : ScrollableControl
{
	/// <returns>true if enabled; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
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

	/// <summary>Indicates the automatic sizing behavior of the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</exception>
	[DefaultValue(AutoSizeMode.GrowOnly)]
	[Browsable(true)]
	[Localizable(true)]
	public virtual AutoSizeMode AutoSizeMode
	{
		get
		{
			return GetAutoSizeMode();
		}
		set
		{
			SetAutoSizeMode(value);
		}
	}

	/// <summary>Indicates the border style for the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle" /> value.</exception>
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
			return base.InternalBorderStyle;
		}
		set
		{
			base.InternalBorderStyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
	/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public new bool TabStop
	{
		get
		{
			return base.TabStop;
		}
		set
		{
			if (value != TabStop)
			{
				base.TabStop = value;
			}
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <returns>A <see cref="T:System.String" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[Bindable(false)]
	public override string Text
	{
		get
		{
			return base.Text;
		}
		set
		{
			if (!(value == Text))
			{
				base.Text = value;
				Refresh();
			}
		}
	}

	protected override CreateParams CreateParams => base.CreateParams;

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => ThemeEngine.Current.PanelDefaultSize;

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Panel.AutoSize" /> property has changed.</summary>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
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

	/// <summary>This member is not meaningful for this control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event KeyEventHandler KeyDown
	{
		add
		{
			base.KeyDown += value;
		}
		remove
		{
			base.KeyDown -= value;
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public new event KeyPressEventHandler KeyPress
	{
		add
		{
			base.KeyPress += value;
		}
		remove
		{
			base.KeyPress -= value;
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public new event KeyEventHandler KeyUp
	{
		add
		{
			base.KeyUp += value;
		}
		remove
		{
			base.KeyUp -= value;
		}
	}

	/// <summary>This member is not meaningful for this control.</summary>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Panel" /> class.</summary>
	public Panel()
	{
		base.TabStop = false;
		SetStyle(ControlStyles.Selectable, value: false);
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
	}

	/// <summary>Returns a string representation for this control.</summary>
	/// <returns>A <see cref="T:System.String" /> representation of the control.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", BorderStyle: " + BorderStyle;
	}

	/// <summary>Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call base.onResize to ensure that the event is fired for external listeners.</summary>
	/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnResize(EventArgs eventargs)
	{
		base.OnResize(eventargs);
		Invalidate(invalidateChildren: true);
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		Size empty = Size.Empty;
		foreach (Control control in base.Controls)
		{
			if (control.Dock == DockStyle.Fill)
			{
				if (control.Bounds.Right > empty.Width)
				{
					empty.Width = control.Bounds.Right;
				}
			}
			else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && (control.Anchor & AnchorStyles.Right) == 0 && control.Bounds.Right + control.Margin.Right > empty.Width)
			{
				empty.Width = control.Bounds.Right + control.Margin.Right;
			}
			if (control.Dock == DockStyle.Fill)
			{
				if (control.Bounds.Bottom > empty.Height)
				{
					empty.Height = control.Bounds.Bottom;
				}
			}
			else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && (control.Anchor & AnchorStyles.Bottom) == 0 && control.Bounds.Bottom + control.Margin.Bottom > empty.Height)
			{
				empty.Height = control.Bounds.Bottom + control.Margin.Bottom;
			}
		}
		return empty;
	}
}
