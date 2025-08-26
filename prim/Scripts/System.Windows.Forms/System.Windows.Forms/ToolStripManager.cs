using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Controls <see cref="T:System.Windows.Forms.ToolStrip" /> rendering and rafting, and the merging of <see cref="T:System.Windows.Forms.MenuStrip" />, <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />, and <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> objects. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class ToolStripManager
{
	private static ToolStripRenderer renderer = new ToolStripProfessionalRenderer();

	private static ToolStripManagerRenderMode render_mode = ToolStripManagerRenderMode.Professional;

	private static bool visual_styles_enabled = Application.RenderWithVisualStyles;

	private static List<WeakReference> toolstrips = new List<WeakReference>();

	private static List<ToolStripMenuItem> menu_items = new List<ToolStripMenuItem>();

	private static bool activated_by_keyboard;

	/// <summary>Gets or sets the default painting styles for the form.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public static ToolStripRenderer Renderer
	{
		get
		{
			return renderer;
		}
		set
		{
			if (Renderer != value)
			{
				renderer = value;
				OnRendererChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the default theme for the form.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The set value was not one of the <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> values.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <see cref="T:System.Windows.Forms.ToolStripManagerRenderMode" /> is set to <see cref="F:System.Windows.Forms.ToolStripManagerRenderMode.Custom" />; use the <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> property instead.</exception>
	/// <filterpriority>1</filterpriority>
	public static ToolStripManagerRenderMode RenderMode
	{
		get
		{
			return render_mode;
		}
		set
		{
			if (!Enum.IsDefined(typeof(ToolStripManagerRenderMode), value))
			{
				throw new InvalidEnumArgumentException($"Enum argument value '{value}' is not valid for ToolStripManagerRenderMode");
			}
			if (render_mode != value)
			{
				render_mode = value;
				switch (value)
				{
				case ToolStripManagerRenderMode.Custom:
					throw new NotSupportedException();
				case ToolStripManagerRenderMode.System:
					Renderer = new ToolStripSystemRenderer();
					break;
				case ToolStripManagerRenderMode.Professional:
					Renderer = new ToolStripProfessionalRenderer();
					break;
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Windows.Forms.ToolStrip" /> is rendered using visual style information called themes. </summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is rendered using themes; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool VisualStylesEnabled
	{
		get
		{
			return visual_styles_enabled;
		}
		set
		{
			if (visual_styles_enabled != value)
			{
				visual_styles_enabled = value;
				if (render_mode == ToolStripManagerRenderMode.Professional)
				{
					(renderer as ToolStripProfessionalRenderer).ColorTable.UseSystemColors = !value;
					OnRendererChanged(EventArgs.Empty);
				}
			}
		}
	}

	internal static bool ActivatedByKeyboard
	{
		get
		{
			return activated_by_keyboard;
		}
		set
		{
			activated_by_keyboard = value;
		}
	}

	/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripManager.Renderer" /> property changes.</summary>
	public static event EventHandler RendererChanged;

	internal static event EventHandler AppClicked;

	internal static event EventHandler AppFocusChange;

	private ToolStripManager()
	{
	}

	/// <summary>Finds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> or a type derived from <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> or one of its derived types as specified by the <paramref name="toolStripName" /> parameter, or null if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not found.</returns>
	/// <param name="toolStripName">A string specifying the name of the <see cref="T:System.Windows.Forms.ToolStrip" /> or derived <see cref="T:System.Windows.Forms.ToolStrip" /> type to find.</param>
	/// <filterpriority>1</filterpriority>
	public static ToolStrip FindToolStrip(string toolStripName)
	{
		lock (toolstrips)
		{
			foreach (WeakReference toolstrip in toolstrips)
			{
				ToolStrip toolStrip = (ToolStrip)toolstrip.Target;
				if (toolStrip == null || !(toolStrip.Name == toolStripName))
				{
					continue;
				}
				return toolStrip;
			}
		}
		return null;
	}

	/// <summary>Retrieves a value indicating whether the specified shortcut key is used by any of the <see cref="T:System.Windows.Forms.ToolStrip" /> controls of a form.</summary>
	/// <returns>true if the shortcut key is used by any <see cref="T:System.Windows.Forms.ToolStrip" /> on the form; otherwise, false. </returns>
	/// <param name="shortcut">The shortcut key for which to search.</param>
	public static bool IsShortcutDefined(Keys shortcut)
	{
		lock (menu_items)
		{
			foreach (ToolStripMenuItem menu_item in menu_items)
			{
				if (menu_item.ShortcutKeys == shortcut)
				{
					return true;
				}
			}
		}
		return false;
	}

	/// <summary>Retrieves a value indicating whether a defined shortcut key is valid.</summary>
	/// <returns>true if the shortcut key is valid; otherwise, false. </returns>
	/// <param name="shortcut">The shortcut key to test for validity.</param>
	public static bool IsValidShortcut(Keys shortcut)
	{
		if ((shortcut & Keys.F1) == Keys.F1)
		{
			return true;
		}
		if ((shortcut & Keys.F2) == Keys.F2)
		{
			return true;
		}
		if ((shortcut & Keys.F3) == Keys.F3)
		{
			return true;
		}
		if ((shortcut & Keys.F4) == Keys.F4)
		{
			return true;
		}
		if ((shortcut & Keys.F5) == Keys.F5)
		{
			return true;
		}
		if ((shortcut & Keys.F6) == Keys.F6)
		{
			return true;
		}
		if ((shortcut & Keys.F7) == Keys.F7)
		{
			return true;
		}
		if ((shortcut & Keys.F8) == Keys.F8)
		{
			return true;
		}
		if ((shortcut & Keys.F9) == Keys.F9)
		{
			return true;
		}
		if ((shortcut & Keys.F10) == Keys.F10)
		{
			return true;
		}
		if ((shortcut & Keys.F11) == Keys.F11)
		{
			return true;
		}
		if ((shortcut & Keys.F12) == Keys.F12)
		{
			return true;
		}
		if (shortcut == Keys.Shift || shortcut == Keys.Control || shortcut == (Keys.Shift | Keys.Control) || shortcut == Keys.Alt || shortcut == (Keys.Shift | Keys.Alt) || shortcut == (Keys.Control | Keys.Alt) || shortcut == (Keys.Shift | Keys.Control | Keys.Alt))
		{
			return false;
		}
		if ((shortcut & Keys.Alt) == Keys.Alt)
		{
			return true;
		}
		if ((shortcut & Keys.Control) == Keys.Control)
		{
			return true;
		}
		if ((shortcut & Keys.Shift) == Keys.Shift)
		{
			return true;
		}
		return false;
	}

	/// <summary>Loads settings for the given <see cref="T:System.Windows.Forms.Form" /> using the full name of the <see cref="T:System.Windows.Forms.Form" /> as the settings key.</summary>
	/// <param name="targetForm">The <see cref="T:System.Windows.Forms.Form" /> whose name is also the settings key.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
	[System.MonoTODO("Stub, does nothing")]
	public static void LoadSettings(Form targetForm)
	{
		if (targetForm == null)
		{
			throw new ArgumentNullException("targetForm");
		}
	}

	/// <summary>Loads settings for the specified <see cref="T:System.Windows.Forms.Form" /> using the specified settings key.</summary>
	/// <param name="targetForm">The <see cref="T:System.Windows.Forms.Form" /> for which to load settings.</param>
	/// <param name="key">A <see cref="T:System.String" /> representing the settings key for this <see cref="T:System.Windows.Forms.Form" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="targetForm" /> parameter is null.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
	[System.MonoTODO("Stub, does nothing")]
	public static void LoadSettings(Form targetForm, string key)
	{
		if (targetForm == null)
		{
			throw new ArgumentNullException("targetForm");
		}
		if (string.IsNullOrEmpty(key))
		{
			throw new ArgumentNullException("key");
		}
	}

	/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of the same type.</summary>
	/// <returns>true if the merge is successful; otherwise, false. </returns>
	/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetName" /> parameter.</param>
	/// <param name="targetName">The name of the <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameter is null.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="sourceToolStrip" /> or <paramref name="targetName" /> parameters refer to the same <see cref="T:System.Windows.Forms.ToolStrip" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoLimitation("Only supports one level of merging, cannot merge the same ToolStrip multiple times")]
	public static bool Merge(ToolStrip sourceToolStrip, string targetName)
	{
		if (string.IsNullOrEmpty(targetName))
		{
			throw new ArgumentNullException("targetName");
		}
		return Merge(sourceToolStrip, FindToolStrip(targetName));
	}

	/// <summary>Combines two <see cref="T:System.Windows.Forms.ToolStrip" /> objects of different types.</summary>
	/// <returns>true if the merge is successful; otherwise, false.</returns>
	/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> to be combined with the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="targetToolStrip" /> parameter.</param>
	/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that receives the <see cref="T:System.Windows.Forms.ToolStrip" /> referred to by the <paramref name="sourceToolStrip" /> parameter.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoLimitation("Only supports one level of merging, cannot merge the same ToolStrip multiple times")]
	public static bool Merge(ToolStrip sourceToolStrip, ToolStrip targetToolStrip)
	{
		if (sourceToolStrip == null)
		{
			throw new ArgumentNullException("sourceToolStrip");
		}
		if (targetToolStrip == null)
		{
			throw new ArgumentNullException("targetName");
		}
		if (targetToolStrip == sourceToolStrip)
		{
			throw new ArgumentException("Source and target ToolStrip must be different.");
		}
		if (!sourceToolStrip.AllowMerge || !targetToolStrip.AllowMerge)
		{
			return false;
		}
		if (sourceToolStrip.IsCurrentlyMerged || targetToolStrip.IsCurrentlyMerged)
		{
			return false;
		}
		List<ToolStripItem> list = new List<ToolStripItem>();
		foreach (ToolStripItem item in sourceToolStrip.Items)
		{
			switch (item.MergeAction)
			{
			default:
				list.Add(item);
				break;
			case MergeAction.Insert:
				if (item.MergeIndex >= 0)
				{
					list.Add(item);
				}
				break;
			case MergeAction.Replace:
			case MergeAction.Remove:
			case MergeAction.MatchOnly:
				foreach (ToolStripItem item2 in targetToolStrip.Items)
				{
					if (item.Text == item2.Text)
					{
						list.Add(item);
						break;
					}
				}
				break;
			}
		}
		if (list.Count == 0)
		{
			return false;
		}
		sourceToolStrip.BeginMerge();
		targetToolStrip.BeginMerge();
		sourceToolStrip.SuspendLayout();
		targetToolStrip.SuspendLayout();
		while (list.Count > 0)
		{
			ToolStripItem toolStripItem3 = list[0];
			list.Remove(toolStripItem3);
			switch (toolStripItem3.MergeAction)
			{
			default:
				ToolStrip.SetItemParent(toolStripItem3, targetToolStrip);
				break;
			case MergeAction.Insert:
				RemoveItemFromParentToolStrip(toolStripItem3);
				if (toolStripItem3.MergeIndex != -1)
				{
					if (toolStripItem3.MergeIndex >= CountRealToolStripItems(targetToolStrip))
					{
						targetToolStrip.Items.AddNoOwnerOrLayout(toolStripItem3);
					}
					else
					{
						targetToolStrip.Items.InsertNoOwnerOrLayout(AdjustItemMergeIndex(targetToolStrip, toolStripItem3), toolStripItem3);
					}
					toolStripItem3.Parent = targetToolStrip;
				}
				break;
			case MergeAction.Replace:
				foreach (ToolStripItem item3 in targetToolStrip.Items)
				{
					if (toolStripItem3.Text == item3.Text)
					{
						RemoveItemFromParentToolStrip(toolStripItem3);
						targetToolStrip.Items.InsertNoOwnerOrLayout(targetToolStrip.Items.IndexOf(item3), toolStripItem3);
						targetToolStrip.Items.RemoveNoOwnerOrLayout(item3);
						targetToolStrip.HiddenMergedItems.Add(item3);
						break;
					}
				}
				break;
			case MergeAction.Remove:
				foreach (ToolStripItem item4 in targetToolStrip.Items)
				{
					if (toolStripItem3.Text == item4.Text)
					{
						targetToolStrip.Items.RemoveNoOwnerOrLayout(item4);
						targetToolStrip.HiddenMergedItems.Add(item4);
						break;
					}
				}
				break;
			case MergeAction.MatchOnly:
				foreach (ToolStripItem item5 in targetToolStrip.Items)
				{
					if (toolStripItem3.Text == item5.Text)
					{
						if (item5 is ToolStripMenuItem && toolStripItem3 is ToolStripMenuItem)
						{
							ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)toolStripItem3;
							ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)item5;
							Merge(toolStripMenuItem.DropDown, toolStripMenuItem2.DropDown);
						}
						break;
					}
				}
				break;
			}
		}
		sourceToolStrip.ResumeLayout();
		targetToolStrip.ResumeLayout();
		sourceToolStrip.CurrentlyMergedWith = targetToolStrip;
		targetToolStrip.CurrentlyMergedWith = sourceToolStrip;
		return true;
	}

	/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the <see cref="T:System.Windows.Forms.ToolStrip" /> with the specified name to its state before the merge and nullifying all previous merge operations.</summary>
	/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
	/// <param name="targetName">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool RevertMerge(string targetName)
	{
		return RevertMerge(FindToolStrip(targetName));
	}

	/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to its state before the merge and nullifying all previous merge operations.</summary>
	/// <returns>true if the undoing of the merge is successful; otherwise, false. </returns>
	/// <param name="targetToolStrip">The <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public static bool RevertMerge(ToolStrip targetToolStrip)
	{
		return RevertMerge(targetToolStrip, targetToolStrip.CurrentlyMergedWith);
	}

	/// <summary>Undoes a merging of two <see cref="T:System.Windows.Forms.ToolStrip" /> objects, returning both <see cref="T:System.Windows.Forms.ToolStrip" /> controls to their state before the merge and nullifying all previous merge operations.</summary>
	/// <returns>true if the undoing of the merge is successful; otherwise, false.</returns>
	/// <param name="targetToolStrip">The name of the <see cref="T:System.Windows.Forms.ToolStripItem" /> for which to undo a merge operation.</param>
	/// <param name="sourceToolStrip">The <see cref="T:System.Windows.Forms.ToolStrip" /> that was merged with the <paramref name="targetToolStrip" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceToolStrip" /> is null.</exception>
	public static bool RevertMerge(ToolStrip targetToolStrip, ToolStrip sourceToolStrip)
	{
		if (sourceToolStrip == null)
		{
			throw new ArgumentNullException("sourceToolStrip");
		}
		List<ToolStripItem> list = new List<ToolStripItem>();
		foreach (ToolStripItem item in targetToolStrip.Items)
		{
			if (item.Owner == sourceToolStrip)
			{
				list.Add(item);
			}
			else
			{
				if (!(item is ToolStripMenuItem))
				{
					continue;
				}
				foreach (ToolStripItem dropDownItem in (item as ToolStripMenuItem).DropDownItems)
				{
					foreach (ToolStripMenuItem item2 in sourceToolStrip.Items)
					{
						if (dropDownItem.Owner == item2.DropDown)
						{
							list.Add(dropDownItem);
						}
					}
				}
			}
		}
		if (list.Count == 0 && targetToolStrip.HiddenMergedItems.Count == 0)
		{
			return false;
		}
		while (targetToolStrip.HiddenMergedItems.Count > 0)
		{
			targetToolStrip.RevertMergeItem(targetToolStrip.HiddenMergedItems[0]);
			targetToolStrip.HiddenMergedItems.RemoveAt(0);
		}
		sourceToolStrip.SuspendLayout();
		targetToolStrip.SuspendLayout();
		while (list.Count > 0)
		{
			sourceToolStrip.RevertMergeItem(list[0]);
			list.Remove(list[0]);
		}
		sourceToolStrip.ResumeLayout();
		targetToolStrip.ResumeLayout();
		sourceToolStrip.IsCurrentlyMerged = false;
		targetToolStrip.IsCurrentlyMerged = false;
		sourceToolStrip.CurrentlyMergedWith = null;
		targetToolStrip.CurrentlyMergedWith = null;
		return true;
	}

	/// <summary>Saves settings for the given <see cref="T:System.Windows.Forms.Form" /> using the full name of the <see cref="T:System.Windows.Forms.Form" /> as the settings key.</summary>
	/// <param name="sourceForm">The <see cref="T:System.Windows.Forms.Form" /> whose name is also the settings key.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
	public static void SaveSettings(Form sourceForm)
	{
		if (sourceForm == null)
		{
			throw new ArgumentNullException("sourceForm");
		}
	}

	/// <summary>Saves settings for the specified <see cref="T:System.Windows.Forms.Form" /> using the specified settings key.</summary>
	/// <param name="sourceForm">The <see cref="T:System.Windows.Forms.Form" /> for which to save settings.</param>
	/// <param name="key">A <see cref="T:System.String" /> representing the settings key for this <see cref="T:System.Windows.Forms.Form" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceForm" /> parameter is null.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is null or empty.</exception>
	public static void SaveSettings(Form sourceForm, string key)
	{
		if (sourceForm == null)
		{
			throw new ArgumentNullException("sourceForm");
		}
		if (string.IsNullOrEmpty(key))
		{
			throw new ArgumentNullException("key");
		}
	}

	internal static void AddToolStrip(ToolStrip ts)
	{
		lock (toolstrips)
		{
			toolstrips.Add(new WeakReference(ts));
		}
	}

	private static int AdjustItemMergeIndex(ToolStrip ts, ToolStripItem tsi)
	{
		if (ts.Items[0] is MdiControlStrip.SystemMenuItem)
		{
			return tsi.MergeIndex + 1;
		}
		return tsi.MergeIndex;
	}

	private static int CountRealToolStripItems(ToolStrip ts)
	{
		int num = 0;
		foreach (ToolStripItem item in ts.Items)
		{
			if (!(item is MdiControlStrip.ControlBoxMenuItem) && !(item is MdiControlStrip.SystemMenuItem))
			{
				num++;
			}
		}
		return num;
	}

	internal static ToolStrip GetNextToolStrip(ToolStrip ts, bool forward)
	{
		lock (toolstrips)
		{
			List<ToolStrip> list = new List<ToolStrip>();
			foreach (WeakReference toolstrip in toolstrips)
			{
				ToolStrip toolStrip = (ToolStrip)toolstrip.Target;
				if (toolStrip != null)
				{
					list.Add(toolStrip);
				}
			}
			int num = list.IndexOf(ts);
			if (forward)
			{
				for (int i = num + 1; i < list.Count; i++)
				{
					if (list[i].TopLevelControl == ts.TopLevelControl && !(list[i] is StatusStrip))
					{
						return list[i];
					}
				}
				for (int j = 0; j < num; j++)
				{
					if (list[j].TopLevelControl == ts.TopLevelControl && !(list[j] is StatusStrip))
					{
						return list[j];
					}
				}
			}
			else
			{
				for (int num2 = num - 1; num2 >= 0; num2--)
				{
					if (list[num2].TopLevelControl == ts.TopLevelControl && !(list[num2] is StatusStrip))
					{
						return list[num2];
					}
				}
				for (int num3 = list.Count - 1; num3 > num; num3--)
				{
					if (list[num3].TopLevelControl == ts.TopLevelControl && !(list[num3] is StatusStrip))
					{
						return list[num3];
					}
				}
			}
		}
		return null;
	}

	internal static bool ProcessCmdKey(ref Message m, Keys keyData)
	{
		lock (menu_items)
		{
			foreach (ToolStripMenuItem menu_item in menu_items)
			{
				if (menu_item.ProcessCmdKey(ref m, keyData))
				{
					return true;
				}
			}
		}
		return false;
	}

	internal static bool ProcessMenuKey(ref Message m)
	{
		if (Application.KeyboardCapture != null && Application.KeyboardCapture.OnMenuKey())
		{
			return true;
		}
		Form form = (Form)Control.FromHandle(m.HWnd).TopLevelControl;
		if (form == null)
		{
			return false;
		}
		if (form.MainMenuStrip != null && form.MainMenuStrip.OnMenuKey())
		{
			return true;
		}
		lock (toolstrips)
		{
			foreach (WeakReference toolstrip in toolstrips)
			{
				ToolStrip toolStrip = (ToolStrip)toolstrip.Target;
				if (toolStrip == null || toolStrip.TopLevelControl != form || !toolStrip.OnMenuKey())
				{
					continue;
				}
				return true;
			}
		}
		return false;
	}

	internal static void SetActiveToolStrip(ToolStrip toolStrip, bool keyboard)
	{
		if (Application.KeyboardCapture != null)
		{
			Application.KeyboardCapture.KeyboardActive = false;
		}
		if (toolStrip == null)
		{
			activated_by_keyboard = false;
			return;
		}
		activated_by_keyboard = keyboard;
		toolStrip.KeyboardActive = true;
	}

	internal static void AddToolStripMenuItem(ToolStripMenuItem tsmi)
	{
		lock (menu_items)
		{
			menu_items.Add(tsmi);
		}
	}

	internal static void RemoveToolStrip(ToolStrip ts)
	{
		lock (toolstrips)
		{
			foreach (WeakReference toolstrip in toolstrips)
			{
				if (toolstrip.Target == ts)
				{
					toolstrips.Remove(toolstrip);
					break;
				}
			}
		}
	}

	internal static void RemoveToolStripMenuItem(ToolStripMenuItem tsmi)
	{
		lock (menu_items)
		{
			menu_items.Remove(tsmi);
		}
	}

	internal static void FireAppClicked()
	{
		if (ToolStripManager.AppClicked != null)
		{
			ToolStripManager.AppClicked(null, EventArgs.Empty);
		}
		if (Application.KeyboardCapture != null)
		{
			Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppClicked);
		}
	}

	internal static void FireAppFocusChanged(Form form)
	{
		if (ToolStripManager.AppFocusChange != null)
		{
			ToolStripManager.AppFocusChange(form, EventArgs.Empty);
		}
		if (Application.KeyboardCapture != null)
		{
			Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
		}
	}

	internal static void FireAppFocusChanged(object sender)
	{
		if (ToolStripManager.AppFocusChange != null)
		{
			ToolStripManager.AppFocusChange(sender, EventArgs.Empty);
		}
		if (Application.KeyboardCapture != null)
		{
			Application.KeyboardCapture.Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
		}
	}

	private static void OnRendererChanged(EventArgs e)
	{
		if (ToolStripManager.RendererChanged != null)
		{
			ToolStripManager.RendererChanged(null, e);
		}
	}

	private static void RemoveItemFromParentToolStrip(ToolStripItem tsi)
	{
		if (tsi.Owner != null)
		{
			tsi.Owner.Items.RemoveNoOwnerOrLayout(tsi);
			if (tsi.Owner is ToolStripOverflow)
			{
				(tsi.Owner as ToolStripOverflow).ParentToolStrip.Items.RemoveNoOwnerOrLayout(tsi);
			}
		}
	}
}
