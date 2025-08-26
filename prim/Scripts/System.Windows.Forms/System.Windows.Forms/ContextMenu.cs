using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents a shortcut menu. Although <see cref="T:System.Windows.Forms.ContextMenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ContextMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.ContextMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>2</filterpriority>
[DefaultEvent("Popup")]
public class ContextMenu : Menu
{
	private RightToLeft right_to_left;

	private Control src_control;

	private static object CollapseEvent;

	private static object PopupEvent;

	/// <summary>Gets or sets a value indicating whether text displayed by the control is displayed from right to left.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(RightToLeft.No)]
	[Localizable(true)]
	public virtual RightToLeft RightToLeft
	{
		get
		{
			return right_to_left;
		}
		set
		{
			right_to_left = value;
		}
	}

	/// <summary>Gets the control that is displaying the shortcut menu.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is displaying the shortcut menu. If no control has displayed the shortcut menu, the property returns null.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Control SourceControl => src_control;

	/// <summary>Occurs when the shortcut menu collapses.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Collapse
	{
		add
		{
			base.Events.AddHandler(CollapseEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CollapseEvent, value);
		}
	}

	/// <summary>Occurs before the shortcut menu is displayed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Popup
	{
		add
		{
			base.Events.AddHandler(PopupEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PopupEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with no menu items specified.</summary>
	public ContextMenu()
		: base(null)
	{
		tracker = new MenuTracker(this);
		right_to_left = RightToLeft.Inherit;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
	/// <param name="menuItems">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that represent the menu items to add to the shortcut menu. </param>
	public ContextMenu(MenuItem[] menuItems)
		: base(menuItems)
	{
		tracker = new MenuTracker(this);
		right_to_left = RightToLeft.Inherit;
	}

	static ContextMenu()
	{
		Collapse = new object();
		Popup = new object();
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	/// <param name="control">The control to which the command key applies.</param>
	protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData, Control control)
	{
		src_control = control;
		return ProcessCmdKey(ref msg, keyData);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Collapse" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected internal virtual void OnCollapse(EventArgs e)
	{
		((EventHandler)base.Events[Collapse])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Popup" /> event </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnPopup(EventArgs e)
	{
		((EventHandler)base.Events[Popup])?.Invoke(this, e);
	}

	/// <summary>Displays the shortcut menu at the specified position.</summary>
	/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated. </param>
	/// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter. </param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="control" /> parameter is null.</exception>
	/// <exception cref="T:System.ArgumentException">The handle of the control does not exist or the control is not visible.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show(Control control, Point pos)
	{
		if (control == null)
		{
			throw new ArgumentException();
		}
		src_control = control;
		OnPopup(EventArgs.Empty);
		pos = control.PointToScreen(pos);
		MenuTracker.TrackPopupMenu(this, pos);
		OnCollapse(EventArgs.Empty);
	}

	/// <summary>Displays the shortcut menu at the specified position and with the specified alignment.</summary>
	/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated.</param>
	/// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter.</param>
	/// <param name="alignment">A <see cref="T:System.Windows.Forms.LeftRightAlignment" /> that specifies the alignment of the control relative to the <paramref name="pos" /> parameter.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void Show(Control control, Point pos, LeftRightAlignment alignment)
	{
		Point pos2 = ((alignment != 0) ? pos : new Point(pos.X - control.Width, pos.Y));
		Show(control, pos2);
	}

	internal void Hide()
	{
		tracker.Deactivate();
	}
}
