using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Specifies the base class used for displaying dialog boxes on the screen.</summary>
/// <filterpriority>1</filterpriority>
[ToolboxItemFilter("System.Windows.Forms")]
public abstract class CommonDialog : Component
{
	internal class DialogForm : Form
	{
		protected CommonDialog owner;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= -2134376448;
				return createParams;
			}
		}

		internal DialogForm(CommonDialog owner)
		{
			this.owner = owner;
			base.ControlBox = true;
			base.MinimizeBox = false;
			base.MaximizeBox = false;
			base.ShowInTaskbar = false;
			base.FormBorderStyle = FormBorderStyle.Sizable;
			base.StartPosition = FormStartPosition.CenterScreen;
		}

		internal DialogResult RunDialog()
		{
			owner.InitFormsSize(this);
			ShowDialog();
			return base.DialogResult;
		}
	}

	internal DialogForm form;

	private object tag;

	private static object HelpRequestEvent;

	/// <summary>Gets or sets an object that contains data about the control. </summary>
	/// <filterpriority>1</filterpriority>
	[Bindable(true)]
	[Localizable(false)]
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
	[MWFCategory("Data")]
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

	/// <summary>Occurs when the user clicks the Help button on a common dialog box.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HelpRequest
	{
		add
		{
			base.Events.AddHandler(HelpRequestEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HelpRequestEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CommonDialog" /> class.</summary>
	public CommonDialog()
	{
	}

	static CommonDialog()
	{
		HelpRequest = new object();
	}

	internal virtual void InitFormsSize(Form form)
	{
		form.Width = 200;
		form.Height = 200;
	}

	/// <summary>When overridden in a derived class, resets the properties of a common dialog box to their default values.</summary>
	/// <filterpriority>1</filterpriority>
	public abstract void Reset();

	/// <summary>Runs a common dialog box with a default owner.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DialogResult ShowDialog()
	{
		return ShowDialog(null);
	}

	/// <summary>Runs a common dialog box with the specified owner.</summary>
	/// <returns>
	///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
	/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public DialogResult ShowDialog(IWin32Window owner)
	{
		if (form == null)
		{
			if (RunDialog(owner?.Handle ?? IntPtr.Zero))
			{
				return DialogResult.OK;
			}
			return DialogResult.Cancel;
		}
		if (RunDialog(form.Handle))
		{
			form.ShowDialog(owner);
		}
		return form.DialogResult;
	}

	/// <summary>Defines the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.</summary>
	/// <returns>A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.</returns>
	/// <param name="hWnd">The handle to the dialog box window. </param>
	/// <param name="msg">The message being received. </param>
	/// <param name="wparam">Additional information about the message. </param>
	/// <param name="lparam">Additional information about the message. </param>
	protected virtual IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
	{
		return IntPtr.Zero;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.CommonDialog.HelpRequest" /> event.</summary>
	/// <param name="e">An <see cref="T:System.Windows.Forms.HelpEventArgs" /> that provides the event data. </param>
	protected virtual void OnHelpRequest(EventArgs e)
	{
		((EventHandler)base.Events[HelpRequest])?.Invoke(this, e);
	}

	/// <summary>Defines the owner window procedure that is overridden to add specific functionality to a common dialog box.</summary>
	/// <returns>The result of the message processing, which is dependent on the message sent.</returns>
	/// <param name="hWnd">The window handle of the message to send. </param>
	/// <param name="msg">The Win32 message to send. </param>
	/// <param name="wparam">The <paramref name="wparam" /> to send with the message. </param>
	/// <param name="lparam">The <paramref name="lparam" /> to send with the message. </param>
	protected virtual IntPtr OwnerWndProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
	{
		return IntPtr.Zero;
	}

	/// <summary>When overridden in a derived class, specifies a common dialog box.</summary>
	/// <returns>true if the dialog box was successfully run; otherwise, false.</returns>
	/// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
	protected abstract bool RunDialog(IntPtr hwndOwner);
}
