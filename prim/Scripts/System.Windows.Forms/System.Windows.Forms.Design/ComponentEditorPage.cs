using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design;

/// <summary>Provides a base implementation for a <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" />.</summary>
[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
public abstract class ComponentEditorPage : Panel
{
	private bool commitOnDeactivate;

	private IComponent component;

	private bool firstActivate = true;

	private Icon icon;

	private int loading;

	private bool loadRequired;

	private IComponentEditorPageSite pageSite;

	[EditorBrowsable(EditorBrowsableState.Never)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public new virtual bool AutoSize
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

	/// <summary>Specifies whether the editor should apply its changes before it is deactivated.</summary>
	/// <returns>true if the editor should apply its changes; otherwise, false.</returns>
	public bool CommitOnDeactivate
	{
		get
		{
			return commitOnDeactivate;
		}
		set
		{
			commitOnDeactivate = value;
		}
	}

	/// <summary>Gets or sets the component to edit.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> this page allows you to edit.</returns>
	protected IComponent Component
	{
		get
		{
			return component;
		}
		set
		{
			component = value;
		}
	}

	/// <summary>Gets the creation parameters for the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that indicates the creation parameters for the control.</returns>
	[System.MonoTODO("Find out what this does.")]
	protected override CreateParams CreateParams
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the page is being activated for the first time.</summary>
	/// <returns>true if the page has not previously been activated; otherwise, false.</returns>
	protected bool FirstActivate
	{
		get
		{
			return firstActivate;
		}
		set
		{
			firstActivate = value;
		}
	}

	/// <summary>Gets or sets the icon for the page.</summary>
	/// <returns>An <see cref="T:System.Drawing.Icon" /> used to represent the page.</returns>
	public Icon Icon
	{
		get
		{
			return icon;
		}
		set
		{
			icon = value;
		}
	}

	/// <summary>Indicates how many load dependencies remain until loading has been completed.</summary>
	/// <returns>The number of remaining load dependencies.</returns>
	protected int Loading
	{
		get
		{
			return loading;
		}
		set
		{
			loading = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a component must be loaded before editing can occur.</summary>
	/// <returns>true if a component must be loaded before editing can occur; otherwise, false.</returns>
	protected bool LoadRequired
	{
		get
		{
			return loadRequired;
		}
		set
		{
			loadRequired = value;
		}
	}

	/// <summary>Gets or sets the page site.</summary>
	/// <returns>The page site.</returns>
	protected IComponentEditorPageSite PageSite
	{
		get
		{
			return pageSite;
		}
		set
		{
			pageSite = value;
		}
	}

	/// <summary>Gets the title of the page.</summary>
	/// <returns>The title of the page.</returns>
	public virtual string Title => base.Text;

	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" /> class.</summary>
	public ComponentEditorPage()
	{
	}

	/// <summary>Activates and displays the page.</summary>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Activate()
	{
		base.Visible = true;
		firstActivate = false;
		if (loadRequired)
		{
			EnterLoadingMode();
			LoadComponent();
			ExitLoadingMode();
		}
	}

	/// <summary>Applies changes to all the components being edited.</summary>
	public virtual void ApplyChanges()
	{
		SaveComponent();
	}

	/// <summary>Deactivates and hides the page.</summary>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void Deactivate()
	{
		base.Visible = false;
	}

	/// <summary>Increments the loading counter.</summary>
	protected void EnterLoadingMode()
	{
		loading++;
	}

	/// <summary>Decrements the loading counter.</summary>
	protected void ExitLoadingMode()
	{
		loading--;
	}

	/// <summary>Gets the control that represents the window for this page.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the window for this page.</returns>
	public virtual Control GetControl()
	{
		return this;
	}

	/// <summary>Gets the component that is to be edited.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that is to be edited.</returns>
	protected IComponent GetSelectedComponent()
	{
		return component;
	}

	/// <summary>Gets a value indicating whether the page is being activated for the first time.</summary>
	/// <returns>true if this is the first time the page is being activated; otherwise, false.</returns>
	protected bool IsFirstActivate()
	{
		return firstActivate;
	}

	/// <summary>Gets a value indicating whether the page is being loaded.</summary>
	/// <returns>true if the page is being loaded; otherwise, false.</returns>
	protected bool IsLoading()
	{
		return loading != 0;
	}

	/// <summary>Processes messages that could be handled by the page.</summary>
	/// <returns>true if the page processed the message; otherwise, false.</returns>
	/// <param name="msg">The message to process. </param>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual bool IsPageMessage(ref Message msg)
	{
		return PreProcessMessage(ref msg);
	}

	/// <summary>Loads the component into the page user interface (UI).</summary>
	protected abstract void LoadComponent();

	/// <summary>Called when the page and any sibling pages have applied their changes.</summary>
	[System.MonoTODO("Find out what this does.")]
	public virtual void OnApplyComplete()
	{
	}

	/// <summary>Reloads the component for the page.</summary>
	protected virtual void ReloadComponent()
	{
		loadRequired = true;
	}

	/// <summary>Saves the component from the page user interface (UI).</summary>
	protected abstract void SaveComponent();

	/// <summary>Sets the component to be edited.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to be edited. </param>
	public virtual void SetComponent(IComponent component)
	{
		this.component = component;
		ReloadComponent();
	}

	/// <summary>Sets the page as changed since the last load or save.</summary>
	[System.MonoTODO("Find out what this does.")]
	protected virtual void SetDirty()
	{
	}

	/// <summary>Sets the site for this page.</summary>
	/// <param name="site">The site for this page. </param>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void SetSite(IComponentEditorPageSite site)
	{
		pageSite = site;
		pageSite.GetControl().Controls.Add(this);
	}

	/// <summary>Shows Help information if the page supports Help information.</summary>
	public virtual void ShowHelp()
	{
	}

	/// <summary>Gets a value indicating whether the editor supports Help.</summary>
	/// <returns>true if the editor supports Help; otherwise, false. The default implementation returns false.</returns>
	public virtual bool SupportsHelp()
	{
		return false;
	}
}
