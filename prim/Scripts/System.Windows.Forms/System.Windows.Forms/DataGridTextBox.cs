using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a <see cref="T:System.Windows.Forms.TextBox" /> control that is hosted in a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</summary>
/// <filterpriority>2</filterpriority>
[DefaultProperty("GridEditName")]
[DesignTimeVisible(false)]
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ToolboxItem(false)]
public class DataGridTextBox : TextBox
{
	private bool isnavigating;

	private DataGrid grid;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.DataGridTextBox" /> is in a mode that allows either editing or navigating.</summary>
	/// <returns>true if the controls is in navigation mode, and editing has not begun; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public bool IsInEditOrNavigateMode
	{
		get
		{
			return isnavigating;
		}
		set
		{
			isnavigating = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBox" /> class. </summary>
	public DataGridTextBox()
	{
		isnavigating = true;
		grid = null;
		accepts_tab = true;
		accepts_return = false;
		SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, value: false);
		SetStyle(ControlStyles.FixedHeight, value: true);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		if (!base.ReadOnly)
		{
			isnavigating = false;
			grid.ColumnStartedEditing(Bounds);
		}
		base.OnKeyPress(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseWheel(MouseEventArgs e)
	{
	}

	/// <summary>Indicates whether the key pressed is processed further (for example, to navigate, or escape). This property is read-only.</summary>
	/// <returns>true if the key is consumed, false to if the key is further processed.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that contains the key data. </param>
	protected internal override bool ProcessKeyMessage(ref Message m)
	{
		Keys keys = (Keys)m.WParam.ToInt32();
		if (isnavigating && ProcessKeyPreview(ref m))
		{
			return true;
		}
		switch ((Msg)m.Msg)
		{
		case Msg.WM_CHAR:
		{
			Keys keys2 = keys;
			if (keys2 == Keys.Return)
			{
				isnavigating = true;
				return false;
			}
			return ProcessKeyEventArgs(ref m);
		}
		case Msg.WM_KEYDOWN:
			switch (keys)
			{
			case Keys.F2:
				base.SelectionStart = Text.Length;
				SelectionLength = 0;
				return false;
			case Keys.Left:
				if (base.SelectionStart == 0)
				{
					return ProcessKeyPreview(ref m);
				}
				return false;
			case Keys.Right:
				if (base.SelectionStart + SelectionLength >= Text.Length)
				{
					return ProcessKeyPreview(ref m);
				}
				return false;
			case Keys.Return:
				return true;
			case Keys.Tab:
			case Keys.Up:
			case Keys.Down:
				return ProcessKeyPreview(ref m);
			default:
				return ProcessKeyEventArgs(ref m);
			}
		default:
			return false;
		}
	}

	/// <summary>Sets the <see cref="T:System.Windows.Forms.DataGrid" /> to which this <see cref="T:System.Windows.Forms.TextBox" /> control belongs.</summary>
	/// <param name="parentGrid">The <see cref="T:System.Windows.Forms.DataGrid" /> control that hosts the control. </param>
	/// <filterpriority>1</filterpriority>
	public void SetDataGrid(DataGrid parentGrid)
	{
		grid = parentGrid;
	}

	/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> event.</summary>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that contains the event data. </param>
	protected override void WndProc(ref Message m)
	{
		switch ((Msg)m.Msg)
		{
		case Msg.WM_LBUTTONDOWN:
		case Msg.WM_LBUTTONDBLCLK:
			isnavigating = false;
			break;
		}
		base.WndProc(ref m);
	}
}
