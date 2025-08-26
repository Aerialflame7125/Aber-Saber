using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides an empty control that can be used to create other controls.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[DefaultEvent("Load")]
[ComVisible(true)]
[Designer("System.Windows.Forms.Design.UserControlDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DesignerCategory("UserControl")]
public class UserControl : ContainerControl
{
	private static object LoadEvent;

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

	/// <summary>Gets or sets how the control will resize itself. </summary>
	/// <returns>A value from the <see cref="T:System.Windows.Forms.AutoSizeMode" /> enumeration. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
	[Browsable(true)]
	[Localizable(true)]
	[DefaultValue(AutoSizeMode.GrowOnly)]
	public AutoSizeMode AutoSizeMode
	{
		get
		{
			return GetAutoSizeMode();
		}
		set
		{
			if (GetAutoSizeMode() != value)
			{
				SetAutoSizeMode(value);
			}
		}
	}

	/// <summary>Gets or sets how the control performs validation when the user changes focus to another control. </summary>
	/// <returns>A member of the <see cref="T:System.Windows.Forms.AutoValidate" /> enumeration. The default value for <see cref="T:System.Windows.Forms.UserControl" /> is <see cref="F:System.Windows.Forms.AutoValidate.EnablePreventFocusChange" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
	public override AutoValidate AutoValidate
	{
		get
		{
			return base.AutoValidate;
		}
		set
		{
			base.AutoValidate = value;
		}
	}

	/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
	protected override Size DefaultSize => new Size(150, 150);

	/// <returns>The text associated with this control.</returns>
	/// <filterpriority>1</filterpriority>
	[Bindable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
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

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.Style |= 65536;
			createParams.ExStyle |= 65536;
			return createParams;
		}
	}

	/// <summary>Gets or sets the border style of the user control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(BorderStyle.None)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	[Browsable(true)]
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.UserControl.AutoSize" /> property changes. </summary>
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

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.UserControl.AutoValidate" /> property changes.</summary>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public new event EventHandler AutoValidateChanged
	{
		add
		{
			base.AutoValidateChanged += value;
		}
		remove
		{
			base.AutoValidateChanged -= value;
		}
	}

	/// <summary>Occurs before the control becomes visible for the first time.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Load
	{
		add
		{
			base.Events.AddHandler(LoadEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LoadEvent, value);
		}
	}

	/// <filterpriority>1</filterpriority>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UserControl" /> class.</summary>
	public UserControl()
	{
		SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
	}

	static UserControl()
	{
		Load = new object();
	}

	/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool ValidateChildren()
	{
		return base.ValidateChildren();
	}

	/// <returns>true if all of the children validated successfully; otherwise, false. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return false.</returns>
	/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
	[Browsable(true)]
	[EditorBrowsable(EditorBrowsableState.Always)]
	public override bool ValidateChildren(ValidationConstraints validationConstraints)
	{
		return base.ValidateChildren(validationConstraints);
	}

	/// <summary>Raises the CreateControl event.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		OnLoad(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnLoad(EventArgs e)
	{
		((EventHandler)base.Events[Load])?.Invoke(this, e);
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void WndProc(ref Message m)
	{
		Msg msg = (Msg)m.Msg;
		if (msg == Msg.WM_SETFOCUS)
		{
			if (ActiveControl == null)
			{
				SelectNextControl(null, forward: true, tabStopOnly: true, nested: true, wrap: false);
			}
			base.WndProc(ref m);
		}
		else
		{
			base.WndProc(ref m);
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnResize(EventArgs e)
	{
		base.OnResize(e);
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		Size empty = Size.Empty;
		foreach (Control control3 in base.Controls)
		{
			if (control3.is_visible)
			{
				if (control3.Dock == DockStyle.Left || control3.Dock == DockStyle.Right)
				{
					empty.Width += control3.PreferredSize.Width;
				}
				else if (control3.Dock == DockStyle.Top || control3.Dock == DockStyle.Bottom)
				{
					empty.Height += control3.PreferredSize.Height;
				}
			}
		}
		foreach (Control control4 in base.Controls)
		{
			if (control4.is_visible && control4.Dock == DockStyle.None && (control4.Anchor & AnchorStyles.Bottom) != AnchorStyles.Bottom && (control4.Anchor & AnchorStyles.Right) != AnchorStyles.Right)
			{
				empty.Width = Math.Max(empty.Width, control4.Bounds.Right + control4.Margin.Right);
				empty.Height = Math.Max(empty.Height, control4.Bounds.Bottom + control4.Margin.Bottom);
			}
		}
		return empty;
	}
}
