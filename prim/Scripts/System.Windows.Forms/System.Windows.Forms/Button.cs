using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a Windows button control.</summary>
/// <filterpriority>1</filterpriority>
[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class Button : ButtonBase, IButtonControl
{
	private DialogResult dialog_result;

	/// <summary>Gets or sets the mode by which the <see cref="T:System.Windows.Forms.Button" /> automatically resizes itself.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default value is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
	[DefaultValue(AutoSizeMode.GrowOnly)]
	[MWFCategory("Layout")]
	[Localizable(true)]
	[Browsable(true)]
	public AutoSizeMode AutoSizeMode
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

	/// <summary>Gets or sets a value that is returned to the parent form when the button is clicked.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values. The default value is None.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DialogResult" /> values. </exception>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(DialogResult.None)]
	[MWFCategory("Behavior")]
	public virtual DialogResult DialogResult
	{
		get
		{
			return dialog_result;
		}
		set
		{
			dialog_result = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> on the base class when creating a window. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" />.</returns>
	protected override CreateParams CreateParams => base.CreateParams;

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.Button" /> control.</summary>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public new event EventHandler DoubleClick
	{
		add
		{
			base.DoubleClick += value;
		}
		remove
		{
			base.DoubleClick -= value;
		}
	}

	/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.Button" /> control with the mouse.</summary>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public new event MouseEventHandler MouseDoubleClick
	{
		add
		{
			base.MouseDoubleClick += value;
		}
		remove
		{
			base.MouseDoubleClick -= value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Button" /> class.</summary>
	public Button()
	{
		dialog_result = DialogResult.None;
		SetStyle(ControlStyles.StandardDoubleClick, value: false);
	}

	/// <summary>Notifies the <see cref="T:System.Windows.Forms.Button" /> whether it is the default button so that it can adjust its appearance accordingly.</summary>
	/// <param name="value">true if the button is to have the appearance of the default button; otherwise, false. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void NotifyDefault(bool value)
	{
		base.IsDefault = value;
	}

	/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for a button.</summary>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void PerformClick()
	{
		if (base.CanSelect)
		{
			OnClick(EventArgs.Empty);
		}
	}

	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Text: " + Text;
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnClick(EventArgs e)
	{
		if (dialog_result != 0)
		{
			Form form = FindForm();
			if (form != null)
			{
				form.DialogResult = dialog_result;
			}
		}
		base.OnClick(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
	}

	/// <param name="e"></param>
	protected override void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
	}

	/// <param name="e"></param>
	protected override void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
	/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseUp(MouseEventArgs mevent)
	{
		base.OnMouseUp(mevent);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
	}

	/// <summary>Processes a mnemonic character. </summary>
	/// <returns>true if the mnemonic was processed; otherwise, false.</returns>
	/// <param name="charCode">The mnemonic character entered. </param>
	protected override bool ProcessMnemonic(char charCode)
	{
		if (base.UseMnemonic && Control.IsMnemonic(charCode, Text))
		{
			PerformClick();
			return true;
		}
		return base.ProcessMnemonic(charCode);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override void Draw(PaintEventArgs pevent)
	{
		if (base.FlatStyle == FlatStyle.System)
		{
			base.Draw(pevent);
			return;
		}
		ThemeEngine.Current.CalculateButtonTextAndImageLayout(this, out var textRectangle, out var imageRectangle);
		if (base.FlatStyle == FlatStyle.Standard)
		{
			ThemeEngine.Current.DrawButton(pevent.Graphics, this, textRectangle, imageRectangle, pevent.ClipRectangle);
		}
		else if (base.FlatStyle == FlatStyle.Flat)
		{
			ThemeEngine.Current.DrawFlatButton(pevent.Graphics, this, textRectangle, imageRectangle, pevent.ClipRectangle);
		}
		else if (base.FlatStyle == FlatStyle.Popup)
		{
			ThemeEngine.Current.DrawPopupButton(pevent.Graphics, this, textRectangle, imageRectangle, pevent.ClipRectangle);
		}
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		if (AutoSize)
		{
			return ThemeEngine.Current.CalculateButtonAutoSize(this);
		}
		return base.GetPreferredSizeCore(proposedSize);
	}
}
