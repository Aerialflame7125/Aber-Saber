using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides basic functionality for controls that display a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> when a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />, <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, or <see cref="T:System.Windows.Forms.ToolStripSplitButton" /> control is clicked.</summary>
/// <filterpriority>2</filterpriority>
[DefaultProperty("DropDownItems")]
[Designer("System.Windows.Forms.Design.ToolStripMenuItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public abstract class ToolStripDropDownItem : ToolStripItem
{
	internal ToolStripDropDown drop_down;

	private ToolStripDropDownDirection drop_down_direction;

	private static object DropDownClosedEvent;

	private static object DropDownItemClickedEvent;

	private static object DropDownOpenedEvent;

	private static object DropDownOpeningEvent;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that will be displayed when this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is clicked.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that is associated with the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(ReferenceConverter))]
	public ToolStripDropDown DropDown
	{
		get
		{
			if (drop_down == null)
			{
				drop_down = CreateDefaultDropDown();
				drop_down.ItemAdded += DropDown_ItemAdded;
			}
			return drop_down;
		}
		set
		{
			drop_down = value;
			drop_down.OwnerItem = this;
		}
	}

	/// <summary>Gets or sets a value indicating the direction in which the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> emerges from its parent container.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property is set to a value that is not one of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</exception>
	[Browsable(false)]
	public ToolStripDropDownDirection DropDownDirection
	{
		get
		{
			return drop_down_direction;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripDropDownDirection), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripDropDownDirection");
			}
			drop_down_direction = value;
		}
	}

	/// <summary>Gets the collection of items in the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> that is associated with this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemCollection" /> of controls.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public ToolStripItemCollection DropDownItems => DropDown.Items;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> has <see cref="T:System.Windows.Forms.ToolStripDropDown" /> controls associated with it. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> has <see cref="T:System.Windows.Forms.ToolStripDropDown" /> controls; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual bool HasDropDownItems => drop_down != null && DropDown.Items.Count != 0;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is in the pressed state.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is in the pressed state; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool Pressed => base.Pressed || (drop_down != null && DropDown.Visible);

	/// <summary>Gets the screen coordinates, in pixels, of the upper-left corner of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
	/// <returns>A Point representing the x and y screen coordinates, in pixels.</returns>
	protected internal virtual Point DropDownLocation
	{
		get
		{
			Point result;
			if (base.IsOnDropDown)
			{
				result = base.Parent.PointToScreen(new Point(Bounds.Left, Bounds.Top - 1));
				result.X += Bounds.Width;
				result.Y += Bounds.Left;
				return result;
			}
			result = new Point(Bounds.Left, Bounds.Bottom - 1);
			return base.Parent.PointToScreen(result);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> closes. </summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DropDownClosed
	{
		add
		{
			base.Events.AddHandler(DropDownClosedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownClosedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is clicked.</summary>
	/// <filterpriority>1</filterpriority>
	public event ToolStripItemClickedEventHandler DropDownItemClicked
	{
		add
		{
			base.Events.AddHandler(DropDownItemClickedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownItemClickedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> has opened.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DropDownOpened
	{
		add
		{
			base.Events.AddHandler(DropDownOpenedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownOpenedEvent, value);
		}
	}

	/// <summary>Occurs as the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is opening.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler DropDownOpening
	{
		add
		{
			base.Events.AddHandler(DropDownOpeningEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DropDownOpeningEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class. </summary>
	protected ToolStripDropDownItem()
		: this(string.Empty, null, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, and action to take when the drop-down control is clicked.</summary>
	/// <param name="text">The display text of the drop-down control.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
	/// <param name="onClick">The action to take when the drop-down control is clicked.</param>
	protected ToolStripDropDownItem(string text, Image image, EventHandler onClick)
		: this(text, image, onClick, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, and <see cref="T:System.Windows.Forms.ToolStripItem" /> collection that the drop-down control contains.</summary>
	/// <param name="text">The display text of the drop-down control.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
	/// <param name="dropDownItems">A <see cref="T:System.Windows.Forms.ToolStripItem" /> collection that the drop-down control contains.</param>
	protected ToolStripDropDownItem(string text, Image image, params ToolStripItem[] dropDownItems)
		: this(text, image, null, string.Empty)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> class with the specified display text, image, action to take when the drop-down control is clicked, and control name.</summary>
	/// <param name="text">The display text of the drop-down control.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to be displayed on the control.</param>
	/// <param name="onClick">The action to take when the drop-down control is clicked.</param>
	/// <param name="name">The name of the control.</param>
	protected ToolStripDropDownItem(string text, Image image, EventHandler onClick, string name)
		: base(text, image, onClick, name)
	{
	}

	static ToolStripDropDownItem()
	{
		DropDownClosed = new object();
		DropDownItemClicked = new object();
		DropDownOpened = new object();
		DropDownOpening = new object();
	}

	/// <summary>Makes a visible <see cref="T:System.Windows.Forms.ToolStripDropDown" /> hidden.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void HideDropDown()
	{
		if (drop_down != null && DropDown.Visible)
		{
			OnDropDownHide(EventArgs.Empty);
			DropDown.Close(ToolStripDropDownCloseReason.CloseCalled);
			is_pressed = false;
			Invalidate();
		}
	}

	/// <summary>Displays the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> control associated with this <see cref="T:System.Windows.Forms.ToolStripDropDownItem" />.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> is the same as the parent <see cref="T:System.Windows.Forms.ToolStrip" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ShowDropDown()
	{
		if (!DropDown.Visible)
		{
			OnDropDownShow(EventArgs.Empty);
			if (HasDropDownItems)
			{
				Invalidate();
				DropDown.Show(DropDownLocation);
			}
		}
	}

	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new ToolStripDropDownItemAccessibleObject(this);
	}

	/// <summary>Creates a generic <see cref="T:System.Windows.Forms.ToolStripDropDown" /> for which events can be defined.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
	protected virtual ToolStripDropDown CreateDefaultDropDown()
	{
		ToolStripDropDown toolStripDropDown = new ToolStripDropDown();
		toolStripDropDown.OwnerItem = this;
		return toolStripDropDown;
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDownItem" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		if (base.IsDisposed)
		{
			return;
		}
		if (HasDropDownItems)
		{
			foreach (ToolStripItem dropDownItem in DropDownItems)
			{
				if (dropDownItem is ToolStripMenuItem)
				{
					ToolStripManager.RemoveToolStripMenuItem((ToolStripMenuItem)dropDownItem);
				}
			}
		}
		if (drop_down != null)
		{
			ToolStripManager.RemoveToolStrip(drop_down);
		}
		base.Dispose(disposing);
	}

	protected override void OnBoundsChanged()
	{
		base.OnBoundsChanged();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownClosed" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnDropDownClosed(EventArgs e)
	{
		((EventHandler)base.Events[DropDownClosed])?.Invoke(this, e);
	}

	/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.HideDropDown" /> method.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDropDownHide(EventArgs e)
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownItemClicked" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
	protected internal virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e)
	{
		((ToolStripItemClickedEventHandler)base.Events[DropDownItemClicked])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDownItem.DropDownOpened" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnDropDownOpened(EventArgs e)
	{
		((EventHandler)base.Events[DropDownOpened])?.Invoke(this, e);
	}

	/// <summary>Raised in response to the <see cref="M:System.Windows.Forms.ToolStripDropDownItem.ShowDropDown" /> method.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnDropDownShow(EventArgs e)
	{
		((EventHandler)base.Events[DropDownOpening])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected override void OnFontChanged(EventArgs e)
	{
		base.OnFontChanged(e);
		if (drop_down != null)
		{
			drop_down.Font = Font;
		}
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnRightToLeftChanged(EventArgs e)
	{
		base.OnRightToLeftChanged(e);
	}

	/// <returns>false in all cases.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		if (HasDropDownItems)
		{
			foreach (ToolStripItem dropDownItem in DropDownItems)
			{
				if (dropDownItem.ProcessCmdKey(ref m, keyData))
				{
					return true;
				}
			}
		}
		return base.ProcessCmdKey(ref m, keyData);
	}

	/// <returns>true if the key was processed by the item; otherwise, false.</returns>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
	protected internal override bool ProcessDialogKey(Keys keyData)
	{
		if (!Selected || !HasDropDownItems)
		{
			return base.ProcessDialogKey(keyData);
		}
		if (!base.IsOnDropDown)
		{
			if (base.Parent.Orientation == Orientation.Horizontal)
			{
				if (keyData == Keys.Down || keyData == Keys.Return)
				{
					if (base.Parent is MenuStrip)
					{
						(base.Parent as MenuStrip).MenuDroppedDown = true;
					}
					ShowDropDown();
					DropDown.SelectNextToolStripItem(null, forward: true);
					return true;
				}
			}
			else if (keyData == Keys.Right || keyData == Keys.Return)
			{
				if (base.Parent is MenuStrip)
				{
					(base.Parent as MenuStrip).MenuDroppedDown = true;
				}
				ShowDropDown();
				DropDown.SelectNextToolStripItem(null, forward: true);
				return true;
			}
		}
		else if ((keyData == Keys.Right || keyData == Keys.Return) && HasDropDownItems)
		{
			ShowDropDown();
			DropDown.SelectNextToolStripItem(null, forward: true);
			return true;
		}
		return base.ProcessDialogKey(keyData);
	}

	internal override void Dismiss(ToolStripDropDownCloseReason reason)
	{
		if (HasDropDownItems && DropDown.Visible)
		{
			DropDown.Dismiss(reason);
		}
		base.Dismiss(reason);
	}

	internal override void HandleClick(EventArgs e)
	{
		OnClick(e);
	}

	internal void HideDropDown(ToolStripDropDownCloseReason reason)
	{
		if (drop_down != null && DropDown.Visible)
		{
			OnDropDownHide(EventArgs.Empty);
			DropDown.Close(reason);
			is_pressed = false;
			Invalidate();
		}
	}

	private void DropDown_ItemAdded(object sender, ToolStripItemEventArgs e)
	{
		e.Item.owner_item = this;
	}
}
