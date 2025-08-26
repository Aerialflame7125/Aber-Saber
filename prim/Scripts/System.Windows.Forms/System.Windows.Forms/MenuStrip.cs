using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides a menu system for a form.</summary>
/// <filterpriority>1</filterpriority>
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public class MenuStrip : ToolStrip
{
	private class MenuStripAccessibleObject : AccessibleObject
	{
	}

	private ToolStripMenuItem mdi_window_list_item;

	private static object MenuActivateEvent;

	private static object MenuDeactivateEvent;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuStrip" /> supports overflow functionality. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.MenuStrip" /> supports overflow functionality; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	[Browsable(false)]
	public new bool CanOverflow
	{
		get
		{
			return base.CanOverflow;
		}
		set
		{
			base.CanOverflow = value;
		}
	}

	/// <summary>Gets or sets the visibility of the grip used to reposition the control.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Hidden" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(ToolStripGripStyle.Hidden)]
	public new ToolStripGripStyle GripStyle
	{
		get
		{
			return base.GripStyle;
		}
		set
		{
			base.GripStyle = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> that is used to display a list of Multiple-document interface (MDI) child forms.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
	[TypeConverter(typeof(MdiWindowListItemConverter))]
	[MergableProperty(false)]
	[DefaultValue(null)]
	public ToolStripMenuItem MdiWindowListItem
	{
		get
		{
			return mdi_window_list_item;
		}
		set
		{
			if (mdi_window_list_item != value)
			{
				mdi_window_list_item = value;
				RefreshMdiItems();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" />. </summary>
	/// <returns>true if ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" />; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public new bool ShowItemToolTips
	{
		get
		{
			return base.ShowItemToolTips;
		}
		set
		{
			base.ShowItemToolTips = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuStrip" /> stretches from end to end in its container. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.MenuStrip" /> stretches from end to end in its container; otherwise, false. The default is true.</returns>
	[DefaultValue(true)]
	public new bool Stretch
	{
		get
		{
			return base.Stretch;
		}
		set
		{
			base.Stretch = value;
		}
	}

	/// <summary>Gets the default spacing, in pixels, between the sizing grip and the edges of the <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
	/// <returns>
	///   <see cref="T:System.Windows.Forms.Padding" /> values representing the spacing, in pixels.</returns>
	protected override Padding DefaultGripMargin => new Padding(2, 2, 0, 2);

	/// <summary>Gets the spacing, in pixels, between the left, right, top, and bottom edges of the <see cref="T:System.Windows.Forms.MenuStrip" /> from the edges of the form.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the spacing. The default is {Left=6, Top=2, Right=0, Bottom=2}.</returns>
	protected override Padding DefaultPadding => new Padding(6, 2, 0, 2);

	/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.MenuStrip" /> by default.</summary>
	/// <returns>false in all cases.</returns>
	protected override bool DefaultShowItemToolTips => false;

	/// <summary>Gets the horizontal and vertical dimensions, in pixels, of the <see cref="T:System.Windows.Forms.MenuStrip" /> when it is first created.</summary>
	/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> value representing the <see cref="T:System.Windows.Forms.MenuStrip" /> horizontal and vertical dimensions, in pixels. The default is 200 x 21 pixels.</returns>
	protected override Size DefaultSize => new Size(200, 24);

	internal override bool KeyboardActive
	{
		get
		{
			return base.KeyboardActive;
		}
		set
		{
			if (base.KeyboardActive != value)
			{
				base.KeyboardActive = value;
				if (value)
				{
					OnMenuActivate(EventArgs.Empty);
				}
				else
				{
					OnMenuDeactivate(EventArgs.Empty);
				}
			}
		}
	}

	internal bool MenuDroppedDown
	{
		get
		{
			return menu_selected;
		}
		set
		{
			menu_selected = value;
		}
	}

	/// <summary>Occurs when the user accesses the menu with the keyboard or mouse. </summary>
	public event EventHandler MenuActivate
	{
		add
		{
			base.Events.AddHandler(MenuActivateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MenuActivateEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.MenuStrip" /> is deactivated.</summary>
	public event EventHandler MenuDeactivate
	{
		add
		{
			base.Events.AddHandler(MenuDeactivateEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MenuDeactivateEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuStrip" /> class. </summary>
	public MenuStrip()
	{
		base.CanOverflow = false;
		GripStyle = ToolStripGripStyle.Hidden;
		Stretch = true;
		Dock = DockStyle.Top;
	}

	static MenuStrip()
	{
		MenuActivate = new object();
		MenuDeactivate = new object();
	}

	/// <summary>Creates a new accessibility object for the control.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new MenuStripAccessibleObject();
	}

	/// <summary>Creates a <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.MenuStrip" />.</summary>
	/// <returns>A <see cref="M:System.Windows.Forms.ToolStripMenuItem.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
	/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
	/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
	/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</param>
	protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
	{
		return new ToolStripMenuItem(text, image, onClick);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuActivate" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnMenuActivate(EventArgs e)
	{
		((EventHandler)base.Events[MenuActivate])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuStrip.MenuDeactivate" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
	protected virtual void OnMenuDeactivate(EventArgs e)
	{
		((EventHandler)base.Events[MenuDeactivate])?.Invoke(this, e);
	}

	/// <summary>Processes a command key.</summary>
	/// <returns>true if the character was processed by the control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
	/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
	protected override bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		return base.ProcessCmdKey(ref m, keyData);
	}

	/// <summary>Processes Windows messages.</summary>
	/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);
	}

	internal override void Dismiss(ToolStripDropDownCloseReason reason)
	{
		MenuDroppedDown = false;
		base.Dismiss(reason);
	}

	internal void FireMenuActivate()
	{
		ToolStripManager.AppClicked += ToolStripMenuTracker_AppClicked;
		ToolStripManager.AppFocusChange += ToolStripMenuTracker_AppFocusChange;
		OnMenuActivate(EventArgs.Empty);
	}

	internal void FireMenuDeactivate()
	{
		ToolStripManager.AppClicked -= ToolStripMenuTracker_AppClicked;
		ToolStripManager.AppFocusChange -= ToolStripMenuTracker_AppFocusChange;
		OnMenuDeactivate(EventArgs.Empty);
	}

	internal override bool OnMenuKey()
	{
		ToolStripManager.SetActiveToolStrip(this, keyboard: true);
		ToolStripItem toolStripItem = SelectNextToolStripItem(null, forward: true);
		if (toolStripItem == null)
		{
			return false;
		}
		if (toolStripItem is MdiControlStrip.SystemMenuItem)
		{
			SelectNextToolStripItem(toolStripItem, forward: true);
		}
		return true;
	}

	private void ToolStripMenuTracker_AppFocusChange(object sender, EventArgs e)
	{
		GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
	}

	private void ToolStripMenuTracker_AppClicked(object sender, EventArgs e)
	{
		GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppClicked);
	}

	internal void RefreshMdiItems()
	{
		if (mdi_window_list_item == null)
		{
			return;
		}
		Form form = FindForm();
		if (form == null || form.MainMenuStrip != this)
		{
			return;
		}
		MdiClient mdiContainer = form.MdiContainer;
		if (mdiContainer == null)
		{
			return;
		}
		ToolStripItem[] array = new ToolStripItem[mdi_window_list_item.DropDownItems.Count];
		mdi_window_list_item.DropDownItems.CopyTo(array, 0);
		ToolStripItem[] array2 = array;
		foreach (ToolStripItem toolStripItem in array2)
		{
			if (toolStripItem is ToolStripMenuItem && (toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry && (!mdiContainer.mdi_child_list.Contains((toolStripItem as ToolStripMenuItem).MdiClientForm) || !(toolStripItem as ToolStripMenuItem).MdiClientForm.Visible))
			{
				mdi_window_list_item.DropDownItems.Remove(toolStripItem);
			}
		}
		for (int j = 0; j < mdiContainer.mdi_child_list.Count; j++)
		{
			Form form2 = (Form)mdiContainer.mdi_child_list[j];
			if (!form2.Visible)
			{
				continue;
			}
			ToolStripMenuItem toolStripMenuItem;
			if ((toolStripMenuItem = FindMdiMenuItemOfForm(form2)) == null)
			{
				if (CountMdiMenuItems() == 0 && mdi_window_list_item.DropDownItems.Count > 0 && !(mdi_window_list_item.DropDownItems[mdi_window_list_item.DropDownItems.Count - 1] is ToolStripSeparator))
				{
					mdi_window_list_item.DropDownItems.Add(new ToolStripSeparator());
				}
				toolStripMenuItem = new ToolStripMenuItem();
				toolStripMenuItem.MdiClientForm = form2;
				mdi_window_list_item.DropDownItems.Add(toolStripMenuItem);
			}
			toolStripMenuItem.Text = $"&{j + 1} {form2.Text}";
			toolStripMenuItem.Checked = form.ActiveMdiChild == form2;
		}
		if (NeedToReorderMdi())
		{
			ReorderMdiMenu();
		}
	}

	private ToolStripMenuItem FindMdiMenuItemOfForm(Form f)
	{
		foreach (ToolStripItem dropDownItem in mdi_window_list_item.DropDownItems)
		{
			if (dropDownItem is ToolStripMenuItem && (dropDownItem as ToolStripMenuItem).MdiClientForm == f)
			{
				return (ToolStripMenuItem)dropDownItem;
			}
		}
		return null;
	}

	private int CountMdiMenuItems()
	{
		int num = 0;
		foreach (ToolStripItem dropDownItem in mdi_window_list_item.DropDownItems)
		{
			if (dropDownItem is ToolStripMenuItem && (dropDownItem as ToolStripMenuItem).IsMdiWindowListEntry)
			{
				num++;
			}
		}
		return num;
	}

	private bool NeedToReorderMdi()
	{
		bool flag = false;
		foreach (ToolStripItem dropDownItem in mdi_window_list_item.DropDownItems)
		{
			if (!(dropDownItem is ToolStripMenuItem))
			{
				continue;
			}
			if (!(dropDownItem as ToolStripMenuItem).IsMdiWindowListEntry)
			{
				if (flag)
				{
					return true;
				}
			}
			else
			{
				flag = true;
			}
		}
		return false;
	}

	private void ReorderMdiMenu()
	{
		ToolStripItem[] array = new ToolStripItem[mdi_window_list_item.DropDownItems.Count];
		mdi_window_list_item.DropDownItems.CopyTo(array, 0);
		mdi_window_list_item.DropDownItems.Clear();
		ToolStripItem[] array2 = array;
		foreach (ToolStripItem toolStripItem in array2)
		{
			if (toolStripItem is ToolStripSeparator || !(toolStripItem as ToolStripMenuItem).IsMdiWindowListEntry)
			{
				mdi_window_list_item.DropDownItems.Add(toolStripItem);
			}
		}
		int count = mdi_window_list_item.DropDownItems.Count;
		if (count > 0 && !(mdi_window_list_item.DropDownItems[count - 1] is ToolStripSeparator))
		{
			mdi_window_list_item.DropDownItems.Add(new ToolStripSeparator());
		}
		ToolStripItem[] array3 = array;
		foreach (ToolStripItem toolStripItem2 in array3)
		{
			if (toolStripItem2 is ToolStripMenuItem && (toolStripItem2 as ToolStripMenuItem).IsMdiWindowListEntry)
			{
				mdi_window_list_item.DropDownItems.Add(toolStripItem2);
			}
		}
	}
}
