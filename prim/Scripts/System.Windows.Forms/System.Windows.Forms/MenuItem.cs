using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents an individual item that is displayed within a <see cref="T:System.Windows.Forms.MainMenu" /> or <see cref="T:System.Windows.Forms.ContextMenu" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
/// <filterpriority>1</filterpriority>
[DefaultProperty("Text")]
[ToolboxItem(false)]
[DesignTimeVisible(false)]
[DefaultEvent("Click")]
public class MenuItem : Menu
{
	internal bool separator;

	internal bool break_;

	internal bool bar_break;

	private Shortcut shortcut;

	private string text;

	private bool checked_;

	private bool radiocheck;

	private bool enabled;

	private char mnemonic;

	private bool showshortcut;

	private int index;

	private bool mdilist;

	private Hashtable mdilist_items;

	private Hashtable mdilist_forms;

	private MdiClient mdicontainer;

	private bool is_window_menu_item;

	private bool defaut_item;

	private bool visible;

	private bool ownerdraw;

	private int menuid;

	private int mergeorder;

	private int xtab;

	private int menuheight;

	private bool menubar;

	private MenuMerge mergetype;

	internal Rectangle bounds;

	private static object ClickEvent;

	private static object DrawItemEvent;

	private static object MeasureItemEvent;

	private static object PopupEvent;

	private static object SelectEvent;

	private static object UIACheckedChangedEvent;

	private static object UIARadioCheckChangedEvent;

	private static object UIAEnabledChangedEvent;

	private static object UIATextChangedEvent;

	private bool selected;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" /> is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a submenu item or menu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
	/// <returns>true if the menu item is placed on a new line or in a new column; false if the menu item is left in its default placement. The default is false.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(false)]
	public bool BarBreak
	{
		get
		{
			return break_;
		}
		set
		{
			break_ = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the item is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a menu item or submenu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
	/// <returns>true if the menu item is placed on a new line or in a new column; false if the menu item is left in its default placement. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DefaultValue(false)]
	public bool Break
	{
		get
		{
			return bar_break;
		}
		set
		{
			bar_break = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a check mark appears next to the text of the menu item.</summary>
	/// <returns>true if there is a check mark next to the menu item; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.MenuItem" /> is a top-level menu or has children.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Checked
	{
		get
		{
			return checked_;
		}
		set
		{
			if (checked_ != value)
			{
				checked_ = value;
				OnUIACheckedChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the menu item is the default menu item.</summary>
	/// <returns>true if the menu item is the default item in a menu; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool DefaultItem
	{
		get
		{
			return defaut_item;
		}
		set
		{
			defaut_item = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the menu item is enabled.</summary>
	/// <returns>true if the menu item is enabled; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(true)]
	public bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			if (enabled != value)
			{
				enabled = value;
				OnUIAEnabledChanged(EventArgs.Empty);
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets a value indicating the position of the menu item in its parent menu.</summary>
	/// <returns>The zero-based index representing the position of the menu item in its parent menu.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero or greater than the item count.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public int Index
	{
		get
		{
			return index;
		}
		set
		{
			if (Parent != null && Parent.MenuItems != null && (value < 0 || value >= Parent.MenuItems.Count))
			{
				throw new ArgumentException("'" + value + "' is not a valid value for 'value'");
			}
			index = value;
		}
	}

	/// <summary>Gets a value indicating whether the menu item contains child menu items.</summary>
	/// <returns>true if the menu item contains child menu items; false if the menu is a standalone menu item.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public override bool IsParent => IsPopup;

	/// <summary>Gets or sets a value indicating whether the menu item will be populated with a list of the Multiple Document Interface (MDI) child windows that are displayed within the associated form.</summary>
	/// <returns>true if a list of the MDI child windows is displayed in this menu item; otherwise, false. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool MdiList
	{
		get
		{
			return mdilist;
		}
		set
		{
			if (mdilist == value)
			{
				return;
			}
			mdilist = value;
			if (mdilist || mdilist_items == null)
			{
				return;
			}
			foreach (MenuItem key in mdilist_items.Keys)
			{
				base.MenuItems.Remove(key);
			}
			mdilist_items.Clear();
			mdilist_items = null;
		}
	}

	/// <summary>Gets a value indicating the Windows identifier for this menu item.</summary>
	/// <returns>The Windows identifier for this menu item.</returns>
	protected int MenuID => menuid;

	/// <summary>Gets or sets a value indicating the relative position of the menu item when it is merged with another.</summary>
	/// <returns>A zero-based index representing the merge order position for this menu item. The default is 0.</returns>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(0)]
	public int MergeOrder
	{
		get
		{
			return mergeorder;
		}
		set
		{
			mergeorder = value;
		}
	}

	/// <summary>Gets or sets a value indicating the behavior of this menu item when its menu is merged with another.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MenuMerge" /> value that represents the menu item's merge type.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</exception>
	/// <filterpriority>2</filterpriority>
	[DefaultValue(MenuMerge.Add)]
	public MenuMerge MergeType
	{
		get
		{
			return mergetype;
		}
		set
		{
			if (!Enum.IsDefined(typeof(MenuMerge), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for MenuMerge");
			}
			mergetype = value;
		}
	}

	/// <summary>Gets a value indicating the mnemonic character that is associated with this menu item.</summary>
	/// <returns>A character that represents the mnemonic character associated with this menu item. Returns the NUL character (ASCII value 0) if no mnemonic character is specified in the text of the <see cref="T:System.Windows.Forms.MenuItem" />.</returns>
	/// <filterpriority>2</filterpriority>
	[Browsable(false)]
	public char Mnemonic => mnemonic;

	/// <summary>Gets or sets a value indicating whether the code that you provide draws the menu item or Windows draws the menu item.</summary>
	/// <returns>true if the menu item is to be drawn using code; false if the menu item is to be drawn by Windows. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool OwnerDraw
	{
		get
		{
			return ownerdraw;
		}
		set
		{
			ownerdraw = value;
		}
	}

	/// <summary>Gets a value indicating the menu that contains this menu item.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Menu" /> that represents the menu that contains this menu item.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public Menu Parent => parent_menu;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" />, if checked, displays a radio-button instead of a check mark.</summary>
	/// <returns>true if a radio-button is to be used instead of a check mark; false if the standard check mark is to be displayed when the menu item is checked. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool RadioCheck
	{
		get
		{
			return radiocheck;
		}
		set
		{
			if (radiocheck != value)
			{
				radiocheck = value;
				OnUIARadioCheckChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating the shortcut key associated with the menu item.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. The default is Shortcut.None.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(Shortcut.None)]
	[Localizable(true)]
	public Shortcut Shortcut
	{
		get
		{
			return shortcut;
		}
		set
		{
			if (!Enum.IsDefined(typeof(Shortcut), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for Shortcut");
			}
			shortcut = value;
			UpdateMenuItem();
		}
	}

	/// <summary>Gets or sets a value indicating whether the shortcut key that is associated with the menu item is displayed next to the menu item caption.</summary>
	/// <returns>true if the shortcut key combination is displayed next to the menu item caption; false if the shortcut key combination is not to be displayed. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	[DefaultValue(true)]
	public bool ShowShortcut
	{
		get
		{
			return showshortcut;
		}
		set
		{
			showshortcut = value;
		}
	}

	/// <summary>Gets or sets a value indicating the caption of the menu item.</summary>
	/// <returns>The text caption of the menu item.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
			if (text == "-")
			{
				separator = true;
			}
			else
			{
				separator = false;
			}
			OnUIATextChanged(EventArgs.Empty);
			ProcessMnemonic();
			Invalidate();
		}
	}

	/// <summary>Gets or sets a value indicating whether the menu item is visible.</summary>
	/// <returns>true if the menu item will be made visible on the menu; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(true)]
	public bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			if (value == visible)
			{
				return;
			}
			visible = value;
			if (menu_items != null)
			{
				foreach (MenuItem menu_item in menu_items)
				{
					menu_item.Visible = value;
				}
			}
			if (parent_menu != null)
			{
				parent_menu.OnMenuChanged(EventArgs.Empty);
			}
		}
	}

	internal new int Height
	{
		get
		{
			return bounds.Height;
		}
		set
		{
			bounds.Height = value;
		}
	}

	internal bool IsPopup
	{
		get
		{
			if (menu_items.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	internal bool MeasureEventDefined
	{
		get
		{
			if (ownerdraw && (object)base.Events[MeasureItem] != null)
			{
				return true;
			}
			return false;
		}
	}

	internal bool MenuBar
	{
		get
		{
			return menubar;
		}
		set
		{
			menubar = value;
		}
	}

	internal int MenuHeight
	{
		get
		{
			return menuheight;
		}
		set
		{
			menuheight = value;
		}
	}

	internal bool Selected
	{
		get
		{
			return selected;
		}
		set
		{
			selected = value;
		}
	}

	internal bool Separator
	{
		get
		{
			return separator;
		}
		set
		{
			separator = value;
		}
	}

	internal DrawItemState Status
	{
		get
		{
			DrawItemState drawItemState = DrawItemState.None;
			MenuTracker menuTracker = Parent.Tracker;
			if (Selected)
			{
				drawItemState = (DrawItemState)((int)drawItemState | ((menuTracker.active || menuTracker.Navigating) ? 1 : 64));
			}
			if (!Enabled)
			{
				drawItemState |= DrawItemState.Grayed | DrawItemState.Disabled;
			}
			if (Checked)
			{
				drawItemState |= DrawItemState.Checked;
			}
			if (!menuTracker.Navigating)
			{
				drawItemState |= DrawItemState.NoAccelerator;
			}
			return drawItemState;
		}
	}

	internal bool VisibleItems
	{
		get
		{
			if (menu_items != null)
			{
				foreach (MenuItem menu_item in menu_items)
				{
					if (menu_item.Visible)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	internal new int Width
	{
		get
		{
			return bounds.Width;
		}
		set
		{
			bounds.Width = value;
		}
	}

	internal new int X
	{
		get
		{
			return bounds.X;
		}
		set
		{
			bounds.X = value;
		}
	}

	internal int XTab
	{
		get
		{
			return xtab;
		}
		set
		{
			xtab = value;
		}
	}

	internal new int Y
	{
		get
		{
			return bounds.Y;
		}
		set
		{
			bounds.Y = value;
		}
	}

	/// <summary>Occurs when the menu item is clicked or selected using a shortcut key or access key defined for the menu item.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MenuItem.OwnerDraw" /> property of a menu item is set to true and a request is made to draw the menu item.</summary>
	/// <filterpriority>1</filterpriority>
	public event DrawItemEventHandler DrawItem
	{
		add
		{
			base.Events.AddHandler(DrawItemEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DrawItemEvent, value);
		}
	}

	/// <summary>Occurs when the menu needs to know the size of a menu item before drawing it.</summary>
	/// <filterpriority>1</filterpriority>
	public event MeasureItemEventHandler MeasureItem
	{
		add
		{
			base.Events.AddHandler(MeasureItemEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MeasureItemEvent, value);
		}
	}

	/// <summary>Occurs before a menu item's list of menu items is displayed.</summary>
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

	/// <summary>Occurs when the user places the pointer over a menu item.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler Select
	{
		add
		{
			base.Events.AddHandler(SelectEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectEvent, value);
		}
	}

	internal event EventHandler UIACheckedChanged
	{
		add
		{
			base.Events.AddHandler(UIACheckedChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIACheckedChangedEvent, value);
		}
	}

	internal event EventHandler UIARadioCheckChanged
	{
		add
		{
			base.Events.AddHandler(UIARadioCheckChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UIARadioCheckChangedEvent, value);
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

	/// <summary>Initializes a <see cref="T:System.Windows.Forms.MenuItem" /> with a blank caption.</summary>
	public MenuItem()
		: base(null)
	{
		CommonConstructor(string.Empty);
		shortcut = Shortcut.None;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption for the menu item.</summary>
	/// <param name="text">The caption for the menu item. </param>
	public MenuItem(string text)
		: base(null)
	{
		CommonConstructor(text);
		shortcut = Shortcut.None;
	}

	/// <summary>Initializes a new instance of the class with a specified caption and event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event of the menu item.</summary>
	/// <param name="text">The caption for the menu item. </param>
	/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
	public MenuItem(string text, EventHandler onClick)
		: base(null)
	{
		CommonConstructor(text);
		shortcut = Shortcut.None;
		Click += onClick;
	}

	/// <summary>Initializes a new instance of the class with a specified caption and an array of submenu items defined for the menu item.</summary>
	/// <param name="text">The caption for the menu item. </param>
	/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item. </param>
	public MenuItem(string text, MenuItem[] items)
		: base(items)
	{
		CommonConstructor(text);
		shortcut = Shortcut.None;
	}

	/// <summary>Initializes a new instance of the class with a specified caption, event handler, and associated shortcut key for the menu item.</summary>
	/// <param name="text">The caption for the menu item. </param>
	/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
	/// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. </param>
	public MenuItem(string text, EventHandler onClick, Shortcut shortcut)
		: base(null)
	{
		CommonConstructor(text);
		Click += onClick;
		this.shortcut = shortcut;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption; defined event-handlers for the <see cref="E:System.Windows.Forms.MenuItem.Click" />, <see cref="E:System.Windows.Forms.MenuItem.Select" /> and <see cref="E:System.Windows.Forms.MenuItem.Popup" /> events; a shortcut key; a merge type; and order specified for the menu item.</summary>
	/// <param name="mergeType">One of the <see cref="T:System.Windows.Forms.MenuMerge" /> values. </param>
	/// <param name="mergeOrder">The relative position that this menu item will take in a merged menu. </param>
	/// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. </param>
	/// <param name="text">The caption for the menu item. </param>
	/// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item. </param>
	/// <param name="onPopup">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event for this menu item. </param>
	/// <param name="onSelect">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item. </param>
	/// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item. </param>
	public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem[] items)
		: base(items)
	{
		CommonConstructor(text);
		this.shortcut = shortcut;
		mergeorder = mergeOrder;
		mergetype = mergeType;
		Click += onClick;
		Popup += onPopup;
		Select += onSelect;
	}

	static MenuItem()
	{
		Click = new object();
		DrawItem = new object();
		MeasureItem = new object();
		Popup = new object();
		Select = new object();
		UIACheckedChanged = new object();
		UIARadioCheckChanged = new object();
		UIAEnabledChanged = new object();
		UIATextChanged = new object();
	}

	private void CommonConstructor(string text)
	{
		defaut_item = false;
		separator = false;
		break_ = false;
		bar_break = false;
		checked_ = false;
		radiocheck = false;
		enabled = true;
		showshortcut = true;
		visible = true;
		ownerdraw = false;
		menubar = false;
		menuheight = 0;
		xtab = 0;
		index = -1;
		mnemonic = '\0';
		menuid = -1;
		mergeorder = 0;
		mergetype = MenuMerge.Add;
		Text = text;
	}

	internal void OnUIACheckedChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIACheckedChanged])?.Invoke(this, e);
	}

	internal void OnUIARadioCheckChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIARadioCheckChanged])?.Invoke(this, e);
	}

	internal void OnUIAEnabledChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIAEnabledChanged])?.Invoke(this, e);
	}

	internal void OnUIATextChanged(EventArgs e)
	{
		((EventHandler)base.Events[UIATextChanged])?.Invoke(this, e);
	}

	/// <summary>Creates a copy of the current <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the duplicated menu item.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual MenuItem CloneMenu()
	{
		MenuItem menuItem = new MenuItem();
		menuItem.CloneMenu(this);
		return menuItem;
	}

	/// <summary>Creates a copy of the specified <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
	/// <param name="itemSrc">The <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item to copy. </param>
	protected void CloneMenu(MenuItem itemSrc)
	{
		CloneMenu((Menu)itemSrc);
		MdiList = itemSrc.MdiList;
		is_window_menu_item = itemSrc.is_window_menu_item;
		bool flag = false;
		for (int num = base.MenuItems.Count - 1; num >= 0; num--)
		{
			if (base.MenuItems[num].is_window_menu_item)
			{
				base.MenuItems.RemoveAt(num);
				flag = true;
			}
		}
		if (flag)
		{
			PopulateWindowMenu();
		}
		BarBreak = itemSrc.BarBreak;
		Break = itemSrc.Break;
		Checked = itemSrc.Checked;
		DefaultItem = itemSrc.DefaultItem;
		Enabled = itemSrc.Enabled;
		MergeOrder = itemSrc.MergeOrder;
		MergeType = itemSrc.MergeType;
		OwnerDraw = itemSrc.OwnerDraw;
		RadioCheck = itemSrc.RadioCheck;
		Shortcut = itemSrc.Shortcut;
		ShowShortcut = itemSrc.ShowShortcut;
		Text = itemSrc.Text;
		Visible = itemSrc.Visible;
		base.Name = itemSrc.Name;
		base.Tag = itemSrc.Tag;
		base.Events[Click] = itemSrc.Events[Click];
		base.Events[DrawItem] = itemSrc.Events[DrawItem];
		base.Events[MeasureItem] = itemSrc.Events[MeasureItem];
		base.Events[Popup] = itemSrc.Events[Popup];
		base.Events[Select] = itemSrc.Events[Select];
	}

	/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
	protected override void Dispose(bool disposing)
	{
		if (disposing && parent_menu != null)
		{
			parent_menu.MenuItems.Remove(this);
		}
		base.Dispose(disposing);
	}

	/// <summary>Merges this <see cref="T:System.Windows.Forms.MenuItem" /> with another <see cref="T:System.Windows.Forms.MenuItem" /> and returns the resulting merged <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the merged menu item.</returns>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual MenuItem MergeMenu()
	{
		MenuItem menuItem = new MenuItem();
		menuItem.CloneMenu(this);
		return menuItem;
	}

	/// <summary>Merges another menu item with this menu item.</summary>
	/// <param name="itemSrc">A <see cref="T:System.Windows.Forms.MenuItem" /> that specifies the menu item to merge with this one. </param>
	/// <filterpriority>2</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void MergeMenu(MenuItem itemSrc)
	{
		base.MergeMenu(itemSrc);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(EventArgs e)
	{
		((EventHandler)base.Events[Click])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.DrawItem" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
	protected virtual void OnDrawItem(DrawItemEventArgs e)
	{
		((DrawItemEventHandler)base.Events[DrawItem])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnInitMenuPopup(EventArgs e)
	{
		OnPopup(e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.MeasureItem" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data. </param>
	protected virtual void OnMeasureItem(MeasureItemEventArgs e)
	{
		if (OwnerDraw)
		{
			((MeasureItemEventHandler)base.Events[MeasureItem])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnPopup(EventArgs e)
	{
		((EventHandler)base.Events[Popup])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelect(EventArgs e)
	{
		((EventHandler)base.Events[Select])?.Invoke(this, e);
	}

	/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.MenuItem" />, simulating a click by a user.</summary>
	/// <filterpriority>1</filterpriority>
	public void PerformClick()
	{
		OnClick(EventArgs.Empty);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</summary>
	/// <filterpriority>1</filterpriority>
	public virtual void PerformSelect()
	{
		OnSelect(EventArgs.Empty);
	}

	/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
	/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MenuItem" />. The string includes the type and the <see cref="P:System.Windows.Forms.MenuItem.Text" /> property of the control.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString()
	{
		return base.ToString() + ", Items.Count: " + base.MenuItems.Count + ", Text: " + text;
	}

	internal virtual void Invalidate()
	{
		if (Parent != null && Parent is MainMenu && Parent.Wnd != null)
		{
			Form form = Parent.Wnd.FindForm();
			if (form != null && form.IsHandleCreated)
			{
				XplatUI.RequestNCRecalc(form.Handle);
			}
		}
	}

	internal void PerformPopup()
	{
		OnPopup(EventArgs.Empty);
	}

	internal void PerformDrawItem(DrawItemEventArgs e)
	{
		PopulateWindowMenu();
		if (OwnerDraw)
		{
			OnDrawItem(e);
		}
		else
		{
			ThemeEngine.Current.DrawMenuItem(this, e);
		}
	}

	private void PopulateWindowMenu()
	{
		if (mdilist)
		{
			if (mdilist_items == null)
			{
				mdilist_items = new Hashtable();
				mdilist_forms = new Hashtable();
			}
			MainMenu mainMenu = GetMainMenu();
			if (mainMenu == null || mainMenu.GetForm() == null)
			{
				return;
			}
			Form form = mainMenu.GetForm();
			mdicontainer = form.MdiContainer;
			if (mdicontainer == null)
			{
				return;
			}
			MenuItem[] array = new MenuItem[mdilist_items.Count];
			mdilist_items.Keys.CopyTo(array, 0);
			MenuItem[] array2 = array;
			foreach (MenuItem menuItem in array2)
			{
				Form form2 = (Form)mdilist_items[menuItem];
				if (!mdicontainer.mdi_child_list.Contains(form2))
				{
					mdilist_items.Remove(menuItem);
					mdilist_forms.Remove(form2);
					base.MenuItems.Remove(menuItem);
				}
			}
			for (int j = 0; j < mdicontainer.mdi_child_list.Count; j++)
			{
				Form form3 = (Form)mdicontainer.mdi_child_list[j];
				MenuItem menuItem2;
				if (mdilist_forms.Contains(form3))
				{
					menuItem2 = (MenuItem)mdilist_forms[form3];
				}
				else
				{
					menuItem2 = new MenuItem();
					menuItem2.is_window_menu_item = true;
					menuItem2.Click += MdiWindowClickHandler;
					mdilist_items[menuItem2] = form3;
					mdilist_forms[form3] = menuItem2;
					base.MenuItems.AddNoEvents(menuItem2);
				}
				menuItem2.Visible = form3.Visible;
				menuItem2.Text = "&" + (j + 1) + " " + form3.Text;
				menuItem2.Checked = form.ActiveMdiChild == form3;
			}
		}
		else
		{
			if (mdilist_items == null)
			{
				return;
			}
			foreach (MenuItem value in mdilist_items.Values)
			{
				base.MenuItems.Remove(value);
			}
			mdilist_forms.Clear();
			mdilist_items.Clear();
		}
	}

	internal void PerformMeasureItem(MeasureItemEventArgs e)
	{
		OnMeasureItem(e);
	}

	private void ProcessMnemonic()
	{
		if (text == null || text.Length < 2)
		{
			mnemonic = '\0';
			return;
		}
		bool flag = false;
		for (int i = 0; i < text.Length - 1; i++)
		{
			if (text[i] == '&')
			{
				if (!flag && text[i + 1] != '&')
				{
					mnemonic = char.ToUpper(text[i + 1]);
					return;
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
		}
		mnemonic = '\0';
	}

	private string GetShortCutTextCtrl()
	{
		return "Ctrl";
	}

	private string GetShortCutTextAlt()
	{
		return "Alt";
	}

	private string GetShortCutTextShift()
	{
		return "Shift";
	}

	internal string GetShortCutText()
	{
		if (Shortcut >= Shortcut.CtrlA && Shortcut <= Shortcut.CtrlZ)
		{
			return GetShortCutTextCtrl() + "+" + (char)(65 + (Shortcut - 131137));
		}
		if (Shortcut >= Shortcut.Alt0 && Shortcut <= Shortcut.Alt9)
		{
			return GetShortCutTextAlt() + "+" + (char)(48 + (Shortcut - 262192));
		}
		if (Shortcut >= Shortcut.AltF1 && Shortcut <= Shortcut.AltF9)
		{
			return GetShortCutTextAlt() + "+F" + (char)(49 + (Shortcut - 262256));
		}
		if (Shortcut >= Shortcut.Ctrl0 && Shortcut <= Shortcut.Ctrl9)
		{
			return GetShortCutTextCtrl() + "+" + (char)(48 + (Shortcut - 131120));
		}
		if (Shortcut >= Shortcut.CtrlF1 && Shortcut <= Shortcut.CtrlF9)
		{
			return GetShortCutTextCtrl() + "+F" + (char)(49 + (Shortcut - 131184));
		}
		if (Shortcut >= Shortcut.CtrlShift0 && Shortcut <= Shortcut.CtrlShift9)
		{
			return GetShortCutTextCtrl() + "+" + GetShortCutTextShift() + "+" + (char)(48 + (Shortcut - 196656));
		}
		if (Shortcut >= Shortcut.CtrlShiftA && Shortcut <= Shortcut.CtrlShiftZ)
		{
			return GetShortCutTextCtrl() + "+" + GetShortCutTextShift() + "+" + (char)(65 + (Shortcut - 196673));
		}
		if (Shortcut >= Shortcut.CtrlShiftF1 && Shortcut <= Shortcut.CtrlShiftF9)
		{
			return GetShortCutTextCtrl() + "+" + GetShortCutTextShift() + "+F" + (char)(49 + (Shortcut - 196720));
		}
		if (Shortcut >= Shortcut.F1 && Shortcut <= Shortcut.F9)
		{
			return "F" + (char)(49 + (Shortcut - 112));
		}
		if (Shortcut >= Shortcut.ShiftF1 && Shortcut <= Shortcut.ShiftF9)
		{
			return GetShortCutTextShift() + "+F" + (char)(49 + (Shortcut - 65648));
		}
		return Shortcut switch
		{
			Shortcut.AltBksp => "AltBksp", 
			Shortcut.AltF10 => GetShortCutTextAlt() + "+F10", 
			Shortcut.AltF11 => GetShortCutTextAlt() + "+F11", 
			Shortcut.AltF12 => GetShortCutTextAlt() + "+F12", 
			Shortcut.CtrlDel => GetShortCutTextCtrl() + "+Del", 
			Shortcut.CtrlF10 => GetShortCutTextCtrl() + "+F10", 
			Shortcut.CtrlF11 => GetShortCutTextCtrl() + "+F11", 
			Shortcut.CtrlF12 => GetShortCutTextCtrl() + "+F12", 
			Shortcut.CtrlIns => GetShortCutTextCtrl() + "+Ins", 
			Shortcut.CtrlShiftF10 => GetShortCutTextCtrl() + "+" + GetShortCutTextShift() + "+F10", 
			Shortcut.CtrlShiftF11 => GetShortCutTextCtrl() + "+" + GetShortCutTextShift() + "+F11", 
			Shortcut.CtrlShiftF12 => GetShortCutTextCtrl() + "+" + GetShortCutTextShift() + "+F12", 
			Shortcut.Del => "Del", 
			Shortcut.F10 => "F10", 
			Shortcut.F11 => "F11", 
			Shortcut.F12 => "F12", 
			Shortcut.Ins => "Ins", 
			Shortcut.None => "None", 
			Shortcut.ShiftDel => GetShortCutTextShift() + "+Del", 
			Shortcut.ShiftF10 => GetShortCutTextShift() + "+F10", 
			Shortcut.ShiftF11 => GetShortCutTextShift() + "+F11", 
			Shortcut.ShiftF12 => GetShortCutTextShift() + "+F12", 
			Shortcut.ShiftIns => GetShortCutTextShift() + "+Ins", 
			_ => string.Empty, 
		};
	}

	private void MdiWindowClickHandler(object sender, EventArgs e)
	{
		Form form = (Form)mdilist_items[sender];
		if (form != null)
		{
			mdicontainer.ActivateChild(form);
		}
	}

	private void UpdateMenuItem()
	{
		if (parent_menu != null && parent_menu.Tracker != null)
		{
			parent_menu.Tracker.RemoveShortcuts(this);
			parent_menu.Tracker.AddShortcuts(this);
		}
	}
}
