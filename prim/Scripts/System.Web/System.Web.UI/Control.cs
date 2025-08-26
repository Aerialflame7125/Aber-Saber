using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Defines the properties, methods, and events that are shared by all ASP.NET server controls.</summary>
[DefaultProperty("ID")]
[DesignerCategory("Code")]
[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Require)]
[ToolboxItem("System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[Designer("System.Web.UI.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.ControlCodeDomSerializer, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[Bindable(true)]
[Themeable(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Control : IComponent, IDisposable, IParserAccessor, IDataBindingsAccessor, IUrlResolutionService, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
{
	internal static readonly object DataBindingEvent;

	internal static readonly object DisposedEvent;

	internal static readonly object InitEvent;

	internal static readonly object LoadEvent;

	internal static readonly object PreRenderEvent;

	internal static readonly object UnloadEvent;

	internal static string[] defaultNameArray;

	private int event_mask;

	private const int databinding_mask = 1;

	private const int disposed_mask = 2;

	private const int init_mask = 4;

	private const int load_mask = 8;

	private const int prerender_mask = 16;

	private const int unload_mask = 32;

	[ThreadStatic]
	private static Dictionary<Type, bool> loadViewStateByIDCache;

	private bool? loadViewStateByID;

	private string uniqueID;

	private string clientID;

	private string _userId;

	private ControlCollection _controls;

	private Control _namingContainer;

	private Page _page;

	private Control _parent;

	private ISite _site;

	private StateBag _viewState;

	private EventHandlerList _events;

	private RenderMethod _renderMethodDelegate;

	private Hashtable _controlsCache;

	private int defaultNumberID;

	private DataBindingCollection dataBindings;

	private Hashtable pendingVS;

	private TemplateControl _templateControl;

	private bool _isChildControlStateCleared;

	private string _templateSourceDirectory;

	private ViewStateMode viewStateMode;

	private ClientIDMode? clientIDMode;

	private ClientIDMode? effectiveClientIDMode;

	private Version renderingCompatibility;

	private bool? renderingCompatibilityOld;

	private int stateMask;

	private const int ENABLE_VIEWSTATE = 1;

	private const int VISIBLE = 2;

	private const int AUTOID = 4;

	private const int CREATING_CONTROLS = 8;

	private const int BINDING_CONTAINER = 16;

	private const int AUTO_EVENT_WIREUP = 32;

	private const int IS_NAMING_CONTAINER = 64;

	private const int VISIBLE_CHANGED = 128;

	private const int TRACK_VIEWSTATE = 256;

	private const int CHILD_CONTROLS_CREATED = 512;

	private const int ID_SET = 1024;

	private const int INITED = 2048;

	private const int INITING = 4096;

	private const int VIEWSTATE_LOADED = 8192;

	private const int LOADED = 16384;

	private const int PRERENDERED = 32768;

	private const int ENABLE_THEMING = 65536;

	private const int AUTOID_SET = 131072;

	private const int REMOVED = 262144;

	private ControlAdapter adapter;

	private bool did_adapter_lookup;

	private string _appRelativeTemplateSourceDirectory;

	internal ControlSkin controlSkin;

	private string skinId = string.Empty;

	private bool _enableTheming = true;

	private ExpressionBindingCollection expressionBindings;

	/// <summary>Gets the browser-specific adapter for the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> for this control. If the target browser does not require an adapter, returns <see langword="null" />.</returns>
	protected internal ControlAdapter Adapter
	{
		get
		{
			if (!did_adapter_lookup)
			{
				adapter = ResolveAdapter();
				if (adapter != null)
				{
					adapter.control = this;
				}
				did_adapter_lookup = true;
			}
			return adapter;
		}
	}

	/// <summary>Gets or sets the application-relative virtual directory of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> object that contains this control.</summary>
	/// <returns>The application-relative virtual directory of the page or user control that contains this control.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string AppRelativeTemplateSourceDirectory
	{
		get
		{
			if (_appRelativeTemplateSourceDirectory != null)
			{
				return _appRelativeTemplateSourceDirectory;
			}
			string text = null;
			TemplateControl templateControl = TemplateControl;
			if (templateControl != null)
			{
				string appRelativeVirtualPath = templateControl.AppRelativeVirtualPath;
				if (!string.IsNullOrEmpty(appRelativeVirtualPath))
				{
					text = VirtualPathUtility.GetDirectory(appRelativeVirtualPath, normalize: false);
				}
			}
			_appRelativeTemplateSourceDirectory = ((text != null) ? text : VirtualPathUtility.ToAppRelative(TemplateSourceDirectory));
			return _appRelativeTemplateSourceDirectory;
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		set
		{
			_appRelativeTemplateSourceDirectory = value;
			_templateSourceDirectory = null;
		}
	}

	/// <summary>Gets the control that contains this control's data binding.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> that contains this control's data binding.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[Bindable(false)]
	public Control BindingContainer
	{
		get
		{
			Control control = NamingContainer;
			if ((control != null && control is INonBindingContainer) || (stateMask & 0x10) == 0)
			{
				control = control.BindingContainer;
			}
			return control;
		}
	}

	/// <summary>Gets the control ID for HTML markup that is generated by ASP.NET.</summary>
	/// <returns>The control ID for HTML markup that is generated by ASP.NET.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("An Identification of the control that is rendered.")]
	public virtual string ClientID
	{
		get
		{
			if (clientID != null)
			{
				return clientID;
			}
			clientID = GetClientID();
			stateMask |= 1024;
			return clientID;
		}
	}

	/// <summary>Gets a value that specifies the ASP.NET version that rendered HTML will be compatible with.</summary>
	/// <returns>The ASP.NET version that rendered HTML will be compatible with.</returns>
	[Bindable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual Version RenderingCompatibility
	{
		get
		{
			if (renderingCompatibility == null)
			{
				PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
				renderingCompatibility = ((pagesSection != null) ? pagesSection.ControlRenderingCompatibilityVersion : new Version(4, 0));
			}
			return renderingCompatibility;
		}
		set
		{
			renderingCompatibility = value;
			renderingCompatibilityOld = null;
		}
	}

	internal bool RenderingCompatibilityLessThan40
	{
		get
		{
			if (!renderingCompatibilityOld.HasValue)
			{
				renderingCompatibilityOld = RenderingCompatibility < new Version(4, 0);
			}
			return renderingCompatibilityOld.Value;
		}
	}

	/// <summary>Gets a reference to the naming container if the naming container implements <see cref="T:System.Web.UI.IDataItemContainer" />.</summary>
	/// <returns>The naming container. In a hierarchy of naming containers that implement <see cref="T:System.Web.UI.IDataItemContainer" />, this property returns the naming container at the top of the hierarchy, or <see langword="null" /> if the current <see cref="T:System.Web.UI.Control" /> object is not in a naming container that implements <see cref="T:System.Web.UI.IDataItemContainer" />.</returns>
	[Bindable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Control DataItemContainer
	{
		get
		{
			Control namingContainer = NamingContainer;
			if (namingContainer == null)
			{
				return null;
			}
			if (namingContainer is IDataItemContainer)
			{
				return namingContainer;
			}
			return namingContainer.DataItemContainer;
		}
	}

	/// <summary>Gets a reference to the naming container if the naming container implements <see cref="T:System.Web.UI.IDataKeysControl" />.</summary>
	/// <returns>The naming container. In a hierarchy of naming containers that implement <see cref="T:System.Web.UI.IDataKeysControl" />, the property returns the naming container at the top of the hierarchy, or <see langword="null" /> if the current <see cref="T:System.Web.UI.Control" /> object is not in a naming container that implements <see cref="T:System.Web.UI.IDataKeysControl" />.</returns>
	[Bindable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Control DataKeysContainer
	{
		get
		{
			Control namingContainer = NamingContainer;
			if (namingContainer == null)
			{
				return null;
			}
			if (namingContainer is IDataKeysControl)
			{
				return namingContainer;
			}
			return namingContainer.DataKeysContainer;
		}
	}

	/// <summary>Gets or sets the algorithm that is used to generate the value of the <see cref="P:System.Web.UI.Control.ClientID" /> property.</summary>
	/// <returns>A value that indicates how the <see cref="P:System.Web.UI.Control.ClientID" /> property is generated. The default is <see cref="F:System.Web.UI.ClientIDMode.Inherit" />.</returns>
	[Themeable(false)]
	[DefaultValue(ClientIDMode.Inherit)]
	public virtual ClientIDMode ClientIDMode
	{
		get
		{
			if (!clientIDMode.HasValue)
			{
				return ClientIDMode.Inherit;
			}
			return clientIDMode.Value;
		}
		set
		{
			if (!clientIDMode.HasValue || clientIDMode.Value != value)
			{
				ClearCachedClientID();
				ClearEffectiveClientIDMode();
				clientIDMode = value;
			}
		}
	}

	internal ClientIDMode EffectiveClientIDMode
	{
		get
		{
			if (effectiveClientIDMode.HasValue)
			{
				return effectiveClientIDMode.Value;
			}
			ClientIDMode clientIDMode = ClientIDMode;
			if (clientIDMode != 0)
			{
				effectiveClientIDMode = clientIDMode;
				return clientIDMode;
			}
			Control namingContainer = NamingContainer;
			if (namingContainer != null)
			{
				effectiveClientIDMode = namingContainer.EffectiveClientIDMode;
				return effectiveClientIDMode.Value;
			}
			PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
			effectiveClientIDMode = pagesSection.ClientIDMode;
			return effectiveClientIDMode.Value;
		}
	}

	/// <summary>Gets a character value representing the separator character used in the <see cref="P:System.Web.UI.Control.ClientID" /> property.</summary>
	/// <returns>Always returns the underscore character (_).</returns>
	protected char ClientIDSeparator => '_';

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls for a specified server control in the UI hierarchy.</summary>
	/// <returns>The collection of child controls for the specified server control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("The child controls of this control.")]
	public virtual ControlCollection Controls
	{
		get
		{
			if (_controls == null)
			{
				_controls = CreateControlCollection();
			}
			return _controls;
		}
	}

	/// <summary>Gets a value indicating whether a control is being used on a design surface.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is being used in a designer; otherwise, <see langword="false" />.</returns>
	[MonoTODO("revisit once we have a real design strategy")]
	protected internal bool DesignMode => false;

	/// <summary>Gets or sets a value indicating whether the server control persists its view state, and the view state of any child controls it contains, to the requesting client.</summary>
	/// <returns>
	///     <see langword="true" /> if the server control maintains its view state; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebCategory("Behavior")]
	[WebSysDescription("An Identification of the control that is rendered.")]
	[Themeable(false)]
	public virtual bool EnableViewState
	{
		get
		{
			return (stateMask & 1) != 0;
		}
		set
		{
			SetMask(1, value);
		}
	}

	/// <summary>Gets or sets the programmatic identifier assigned to the server control.</summary>
	/// <returns>The programmatic identifier assigned to the control.</returns>
	[MergableProperty(false)]
	[ParenthesizePropertyName(true)]
	[WebSysDescription("The name of the control that is rendered.")]
	[Filterable(false)]
	[Themeable(false)]
	public virtual string ID
	{
		get
		{
			if ((stateMask & 0x400) == 0)
			{
				return null;
			}
			return _userId;
		}
		set
		{
			if (value != null && value.Length == 0)
			{
				value = null;
			}
			stateMask |= 1024;
			_userId = value;
			NullifyUniqueID();
		}
	}

	/// <summary>Gets a value indicating whether controls contained within this control have control state.</summary>
	/// <returns>
	///     <see langword="true" /> if children of this control do not use control state; otherwise, <see langword="false" />.</returns>
	protected internal bool IsChildControlStateCleared => _isChildControlStateCleared;

	/// <summary>Gets a value indicating whether the control participates in loading its view state by <see cref="P:System.Web.UI.Control.ID" /> instead of index. </summary>
	/// <returns>
	///     <see langword="true" /> if the control loads its view state by <see cref="P:System.Web.UI.Control.ID" />; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	protected bool LoadViewStateByID
	{
		get
		{
			if (!loadViewStateByID.HasValue)
			{
				loadViewStateByID = IsLoadViewStateByID();
			}
			return loadViewStateByID.Value;
		}
	}

	/// <summary>Gets a value indicating whether view state is enabled for this control.</summary>
	/// <returns>
	///     <see langword="true" /> if view state is enabled for the control; otherwise, <see langword="false" />.</returns>
	protected internal bool IsViewStateEnabled
	{
		get
		{
			for (Control control = this; control != null; control = control.Parent)
			{
				if (!control.EnableViewState)
				{
					return false;
				}
				ViewStateMode viewStateMode = control.ViewStateMode;
				if (viewStateMode != 0)
				{
					return viewStateMode == ViewStateMode.Enabled;
				}
			}
			return true;
		}
	}

	/// <summary>Gets the character used to separate control identifiers.</summary>
	/// <returns>The separator character. The default is "$".</returns>
	protected char IdSeparator => '$';

	/// <summary>Gets a reference to the server control's naming container, which creates a unique namespace for differentiating between server controls with the same <see cref="P:System.Web.UI.Control.ID" /> property value.</summary>
	/// <returns>The server control's naming container.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("The container that this control is part of. The control's name has to be unique within the container.")]
	[Bindable(false)]
	public virtual Control NamingContainer
	{
		get
		{
			if (_namingContainer == null && _parent != null)
			{
				if ((_parent.stateMask & 0x40) == 0)
				{
					_namingContainer = _parent.NamingContainer;
				}
				else
				{
					_namingContainer = _parent;
				}
			}
			return _namingContainer;
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.Page" /> instance that contains the server control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Page" /> instance that contains the server control.</returns>
	/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("The webpage that this control resides on.")]
	[Bindable(false)]
	public virtual Page Page
	{
		get
		{
			if (_page == null)
			{
				if (NamingContainer != null)
				{
					_page = NamingContainer.Page;
				}
				else if (Parent != null)
				{
					_page = Parent.Page;
				}
			}
			return _page;
		}
		set
		{
			_page = value;
		}
	}

	/// <summary>Gets a reference to the server control's parent control in the page control hierarchy.</summary>
	/// <returns>A reference to the server control's parent control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("The parent control of this control.")]
	[Bindable(false)]
	public virtual Control Parent => _parent;

	/// <summary>Gets information about the container that hosts the current control when rendered on a design surface.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.ISite" /> that contains information about the container that the control is hosted in.</returns>
	/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[WebSysDescription("The site this control is part of.")]
	public ISite Site
	{
		get
		{
			return _site;
		}
		set
		{
			_site = value;
		}
	}

	/// <summary>Gets or sets a reference to the template that contains this control. </summary>
	/// <returns>The <see cref="T:System.Web.UI.TemplateControl" /> instance that contains this control. </returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Bindable(false)]
	public TemplateControl TemplateControl
	{
		get
		{
			return TemplateControlInternal;
		}
		[EditorBrowsable(EditorBrowsableState.Never)]
		set
		{
			_templateControl = value;
		}
	}

	internal virtual TemplateControl TemplateControlInternal
	{
		get
		{
			if (_templateControl != null)
			{
				return _templateControl;
			}
			if (_parent != null)
			{
				return _parent.TemplateControl;
			}
			return null;
		}
	}

	/// <summary>Gets the virtual directory of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> that contains the current server control.</summary>
	/// <returns>The virtual directory of the page or user control that contains the server control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("A virtual directory containing the parent of the control.")]
	public virtual string TemplateSourceDirectory
	{
		get
		{
			if (_templateSourceDirectory == null)
			{
				TemplateControl templateControl = TemplateControl;
				if (templateControl == null)
				{
					HttpContext context = Context;
					if (context != null)
					{
						_templateSourceDirectory = VirtualPathUtility.GetDirectory(context.Request.CurrentExecutionFilePath);
					}
				}
				else if (templateControl != this)
				{
					_templateSourceDirectory = templateControl.TemplateSourceDirectory;
				}
				if (_templateSourceDirectory == null && this is TemplateControl)
				{
					string appRelativeVirtualPath = ((TemplateControl)this).AppRelativeVirtualPath;
					if (appRelativeVirtualPath != null)
					{
						string directory = VirtualPathUtility.GetDirectory(VirtualPathUtility.ToAbsolute(appRelativeVirtualPath));
						int length = directory.Length;
						if (length <= 1)
						{
							return directory;
						}
						if (directory[--length] == '/')
						{
							_templateSourceDirectory = directory.Substring(0, length);
						}
					}
					else
					{
						_templateSourceDirectory = string.Empty;
					}
				}
				if (_templateSourceDirectory == null)
				{
					_templateSourceDirectory = string.Empty;
				}
			}
			return _templateSourceDirectory;
		}
	}

	/// <summary>Gets the unique, hierarchically qualified identifier for the server control.</summary>
	/// <returns>The fully qualified identifier for the server control.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[WebSysDescription("The unique ID of the control.")]
	public virtual string UniqueID
	{
		get
		{
			if (uniqueID != null)
			{
				return uniqueID;
			}
			Control namingContainer = NamingContainer;
			if (namingContainer == null)
			{
				return _userId;
			}
			EnsureIDInternal();
			string text = namingContainer.UniqueID;
			if (namingContainer == Page || text == null)
			{
				uniqueID = _userId;
				return uniqueID;
			}
			uniqueID = text + IdSeparator + _userId;
			return uniqueID;
		}
	}

	/// <summary>Gets or sets a value that indicates whether a server control is rendered as UI on the page.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is visible on the page; otherwise <see langword="false" />.</returns>
	[DefaultValue(true)]
	[Bindable(true)]
	[WebCategory("Behavior")]
	[WebSysDescription("Visiblity state of the control.")]
	public virtual bool Visible
	{
		get
		{
			if ((stateMask & 2) == 0)
			{
				return false;
			}
			if (_parent != null)
			{
				return _parent.Visible;
			}
			return true;
		}
		set
		{
			if (((value && (stateMask & 2) == 0) || (!value && (stateMask & 2) != 0)) && IsTrackingViewState)
			{
				stateMask |= 128;
			}
			SetMask(2, value);
		}
	}

	/// <summary>Gets a value that indicates whether the server control's child controls have been created.</summary>
	/// <returns>
	///     <see langword="true" /> if child controls have been created; otherwise, <see langword="false" />.</returns>
	protected bool ChildControlsCreated
	{
		get
		{
			return (stateMask & 0x200) != 0;
		}
		set
		{
			if (!value && (stateMask & 0x200) != 0)
			{
				Controls?.Clear();
			}
			SetMask(512, value);
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object associated with the server control for the current Web request.</summary>
	/// <returns>The specified <see cref="T:System.Web.HttpContext" /> object associated with the current request.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected internal virtual HttpContext Context
	{
		get
		{
			Page page = Page;
			if (page != null)
			{
				return page.Context;
			}
			return HttpContext.Current;
		}
	}

	/// <summary>Gets a list of event handler delegates for the control. This property is read-only.</summary>
	/// <returns>The list of event handler delegates.</returns>
	protected EventHandlerList Events
	{
		get
		{
			if (_events == null)
			{
				_events = new EventHandlerList();
			}
			return _events;
		}
	}

	/// <summary>Gets a value indicating whether the current server control's child controls have any saved view-state settings.</summary>
	/// <returns>
	///     <see langword="true" /> if any child controls have saved view state information; otherwise, <see langword="false" />.</returns>
	protected bool HasChildViewState
	{
		get
		{
			if (pendingVS != null)
			{
				return pendingVS.Count > 0;
			}
			return false;
		}
	}

	/// <summary>Gets a value that indicates whether the server control is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" /> if the control is marked to save its state; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => (stateMask & 0x100) != 0;

	/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a server control across multiple requests for the same page.</summary>
	/// <returns>An instance of the <see cref="T:System.Web.UI.StateBag" /> class that contains the server control's view-state information.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("ViewState")]
	protected virtual StateBag ViewState
	{
		get
		{
			if (_viewState == null)
			{
				_viewState = new StateBag(ViewStateIgnoresCase);
			}
			if (IsTrackingViewState)
			{
				_viewState.TrackViewState();
			}
			return _viewState;
		}
	}

	/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.StateBag" /> object is case-insensitive.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.StateBag" /> instance is case-insensitive; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected virtual bool ViewStateIgnoresCase => false;

	internal bool AutoEventWireup
	{
		get
		{
			return (stateMask & 0x20) != 0;
		}
		set
		{
			SetMask(32, value);
		}
	}

	internal bool AutoID
	{
		get
		{
			return (stateMask & 4) != 0;
		}
		set
		{
			if (value || (stateMask & 0x40) == 0)
			{
				SetMask(4, value);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="true" />. </returns>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="Page_PreInit" /> event has already occurred.- or -The control has already been added to the <see langword="Controls" /> collection.</exception>
	[Browsable(false)]
	[Themeable(false)]
	[DefaultValue(true)]
	public virtual bool EnableTheming
	{
		get
		{
			if ((stateMask & 0x10000) != 0)
			{
				return _enableTheming;
			}
			if (_parent != null)
			{
				return _parent.EnableTheming;
			}
			return true;
		}
		set
		{
			SetMask(65536, val: true);
			_enableTheming = value;
		}
	}

	/// <summary>Gets or sets the skin to apply to the control.</summary>
	/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The style sheet has already been applied.- or -The <see langword="Page_PreInit" /> event has already occurred.- or -The control was already added to the <see langword="Controls" /> collection.</exception>
	[Browsable(false)]
	[DefaultValue("")]
	[Filterable(false)]
	public virtual string SkinID
	{
		get
		{
			return skinId;
		}
		set
		{
			skinId = value;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IControlBuilderAccessor.ControlBuilder" />. </summary>
	/// <returns>The <see cref="T:System.Web.UI.ControlBuilder" /> that built the control; otherwise, <see langword="null" /> if no builder was used.</returns>
	ControlBuilder IControlBuilderAccessor.ControlBuilder
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IControlDesignerAccessor.UserData" />. </summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing information about the control.</returns>
	IDictionary IControlDesignerAccessor.UserData
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IExpressionsAccessor.Expressions" />. </summary>
	/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> containing <see cref="T:System.Web.UI.ExpressionBinding" /> objects that represent the properties and expressions for a control.</returns>
	ExpressionBindingCollection IExpressionsAccessor.Expressions
	{
		get
		{
			if (expressionBindings == null)
			{
				expressionBindings = new ExpressionBindingCollection();
			}
			return expressionBindings;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IExpressionsAccessor.HasExpressions" />. </summary>
	/// <returns>
	///     <see langword="true" /> if the control has properties set through expressions; otherwise, <see langword="false" />.</returns>
	bool IExpressionsAccessor.HasExpressions
	{
		get
		{
			if (expressionBindings != null)
			{
				return expressionBindings.Count > 0;
			}
			return false;
		}
	}

	internal bool IsInited => (stateMask & 0x800) != 0;

	internal bool IsLoaded => (stateMask & 0x4000) != 0;

	internal bool IsPrerendered => (stateMask & 0x8000) != 0;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataBindingsAccessor.DataBindings" />. </summary>
	/// <returns>The collection of data bindings.</returns>
	DataBindingCollection IDataBindingsAccessor.DataBindings
	{
		get
		{
			if (dataBindings == null)
			{
				dataBindings = new DataBindingCollection();
			}
			return dataBindings;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataBindingsAccessor.HasDataBindings" />. </summary>
	/// <returns>
	///     <see langword="true" /> if the control contains data-binding logic; otherwise, <see langword="false" />.</returns>
	bool IDataBindingsAccessor.HasDataBindings
	{
		get
		{
			if (dataBindings != null && dataBindings.Count > 0)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets or sets the view-state mode of this control.</summary>
	/// <returns>The view-state mode of this control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is not in the <see cref="T:System.Web.UI.ViewStateMode" /> enumeration.</exception>
	[Themeable(false)]
	[DefaultValue(ViewStateMode.Inherit)]
	public virtual ViewStateMode ViewStateMode
	{
		get
		{
			return viewStateMode;
		}
		set
		{
			if (value < ViewStateMode.Inherit || value > ViewStateMode.Disabled)
			{
				throw new ArgumentOutOfRangeException("An attempt was made to set this property to a value that is not in the ViewStateMode enumeration.");
			}
			viewStateMode = value;
		}
	}

	/// <summary>Occurs when the server control binds to a data source.</summary>
	[WebCategory("FIXME")]
	[WebSysDescription("Raised when the contols databound properties are evaluated.")]
	public event EventHandler DataBinding
	{
		add
		{
			event_mask |= 1;
			Events.AddHandler(DataBindingEvent, value);
		}
		remove
		{
			Events.RemoveHandler(DataBindingEvent, value);
		}
	}

	/// <summary>Occurs when a server control is released from memory, which is the last stage of the server control lifecycle when an ASP.NET page is requested.</summary>
	[WebSysDescription("Raised when the contol is disposed.")]
	public event EventHandler Disposed
	{
		add
		{
			event_mask |= 2;
			Events.AddHandler(DisposedEvent, value);
		}
		remove
		{
			Events.RemoveHandler(DisposedEvent, value);
		}
	}

	/// <summary>Occurs when the server control is initialized, which is the first step in its lifecycle.</summary>
	[WebSysDescription("Raised when the page containing the control is initialized.")]
	public event EventHandler Init
	{
		add
		{
			event_mask |= 4;
			Events.AddHandler(InitEvent, value);
		}
		remove
		{
			Events.RemoveHandler(InitEvent, value);
		}
	}

	/// <summary>Occurs when the server control is loaded into the <see cref="T:System.Web.UI.Page" /> object.</summary>
	[WebSysDescription("Raised after the page containing the control has been loaded.")]
	public event EventHandler Load
	{
		add
		{
			event_mask |= 8;
			Events.AddHandler(LoadEvent, value);
		}
		remove
		{
			Events.RemoveHandler(LoadEvent, value);
		}
	}

	/// <summary>Occurs after the <see cref="T:System.Web.UI.Control" /> object is loaded but prior to rendering.</summary>
	[WebSysDescription("Raised before the page containing the control is rendered.")]
	public event EventHandler PreRender
	{
		add
		{
			event_mask |= 16;
			Events.AddHandler(PreRenderEvent, value);
		}
		remove
		{
			Events.RemoveHandler(PreRenderEvent, value);
		}
	}

	/// <summary>Occurs when the server control is unloaded from memory.</summary>
	[WebSysDescription("Raised when the page containing the control is unloaded.")]
	public event EventHandler Unload
	{
		add
		{
			event_mask |= 32;
			Events.AddHandler(UnloadEvent, value);
		}
		remove
		{
			Events.RemoveHandler(UnloadEvent, value);
		}
	}

	static Control()
	{
		DataBindingEvent = new object();
		DisposedEvent = new object();
		InitEvent = new object();
		LoadEvent = new object();
		PreRenderEvent = new object();
		UnloadEvent = new object();
		defaultNameArray = new string[100];
		for (int i = 0; i < 100; i++)
		{
			defaultNameArray[i] = "ctl" + i.ToString("D2");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Control" /> class.</summary>
	public Control()
	{
		stateMask = 55;
		if (this is INamingContainer)
		{
			stateMask |= 64;
		}
		viewStateMode = ViewStateMode.Inherit;
	}

	/// <summary>Sets the cached <see cref="P:System.Web.UI.Control.ClientID" /> value to <see langword="null" />.</summary>
	protected void ClearCachedClientID()
	{
		clientID = null;
		if (HasControls())
		{
			for (int i = 0; i < _controls.Count; i++)
			{
				_controls[i].ClearCachedClientID();
			}
		}
	}

	/// <summary>Sets the <see cref="P:System.Web.UI.Control.ClientIDMode" /> property of the current control instance and of any child controls to <see cref="F:System.Web.UI.ClientIDMode.Inherit" />.</summary>
	protected void ClearEffectiveClientIDMode()
	{
		effectiveClientIDMode = null;
		if (HasControls())
		{
			for (int i = 0; i < _controls.Count; i++)
			{
				_controls[i].ClearEffectiveClientIDMode();
			}
		}
	}

	private string GetClientID()
	{
		switch (EffectiveClientIDMode)
		{
		case ClientIDMode.AutoID:
			return UniqueID2ClientID(UniqueID);
		case ClientIDMode.Predictable:
			EnsureID();
			return GeneratePredictableClientID();
		case ClientIDMode.Static:
			EnsureID();
			return ID;
		default:
			throw new InvalidOperationException("Unsupported ClientIDMode value.");
		}
	}

	private string GeneratePredictableClientID()
	{
		string value = ID;
		bool flag = !string.IsNullOrEmpty(value);
		char clientIDSeparator = ClientIDSeparator;
		StringBuilder stringBuilder = new StringBuilder();
		Control namingContainer = NamingContainer;
		if (this is INamingContainer && !flag)
		{
			if (namingContainer != null)
			{
				EnsureIDInternal();
			}
			value = _userId;
		}
		if (namingContainer != null && namingContainer != Page)
		{
			if (!string.IsNullOrEmpty(namingContainer.ID))
			{
				stringBuilder.Append(namingContainer.GetClientID());
				stringBuilder.Append(clientIDSeparator);
			}
			else
			{
				stringBuilder.Append(namingContainer.GeneratePredictableClientID());
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(clientIDSeparator);
				}
			}
		}
		if (!flag)
		{
			if (this is INamingContainer || !AutoID)
			{
				stringBuilder.Append(value);
			}
			else
			{
				int length = stringBuilder.Length;
				if (length > 0 && stringBuilder[length - 1] == clientIDSeparator)
				{
					stringBuilder.Length = length - 1;
				}
			}
			return stringBuilder.ToString();
		}
		stringBuilder.Append(value);
		if (!(DataItemContainer is IDataItemContainer dataItemContainer))
		{
			return stringBuilder.ToString();
		}
		IDataKeysControl dataKeysContainer = DataKeysContainer as IDataKeysControl;
		GetDataBoundControlFieldValue(stringBuilder, clientIDSeparator, dataItemContainer, dataKeysContainer);
		return stringBuilder.ToString();
	}

	private void GetDataBoundControlFieldValue(StringBuilder sb, char separator, IDataItemContainer dataItemContainer, IDataKeysControl dataKeysContainer)
	{
		if (dataItemContainer is IDataBoundItemControl)
		{
			return;
		}
		int displayIndex = dataItemContainer.DisplayIndex;
		if (dataKeysContainer == null)
		{
			if (displayIndex >= 0)
			{
				sb.Append(separator);
				sb.Append(displayIndex);
			}
			return;
		}
		string[] clientIDRowSuffix = dataKeysContainer.ClientIDRowSuffix;
		DataKeyArray clientIDRowSuffixDataKeys = dataKeysContainer.ClientIDRowSuffixDataKeys;
		if (clientIDRowSuffixDataKeys == null || clientIDRowSuffix == null || clientIDRowSuffix.Length == 0)
		{
			sb.Append(separator);
			sb.Append(displayIndex);
			return;
		}
		DataKey dataKey = clientIDRowSuffixDataKeys[displayIndex];
		string[] array = clientIDRowSuffix;
		foreach (string name in array)
		{
			sb.Append(separator);
			object obj = dataKey?[name];
			if (obj != null)
			{
				sb.Append(obj.ToString());
			}
		}
	}

	internal string UniqueID2ClientID(string uniqueId)
	{
		if (string.IsNullOrEmpty(uniqueId))
		{
			return null;
		}
		return uniqueId.Replace(IdSeparator, ClientIDSeparator);
	}

	private void SetMask(int m, bool val)
	{
		if (val)
		{
			stateMask |= m;
		}
		else
		{
			stateMask &= ~m;
		}
	}

	internal void SetBindingContainer(bool isBC)
	{
		SetMask(16, isBC);
	}

	internal void ResetChildNames()
	{
		ResetChildNames(-1);
	}

	internal void ResetChildNames(int value)
	{
		if (value < 0)
		{
			defaultNumberID = 0;
		}
		else
		{
			defaultNumberID = value;
		}
	}

	internal int GetDefaultNumberID()
	{
		return defaultNumberID;
	}

	private string GetDefaultName()
	{
		if (defaultNumberID > 99)
		{
			return "ctl" + defaultNumberID++;
		}
		return defaultNameArray[defaultNumberID++];
	}

	private void NullifyUniqueID()
	{
		uniqueID = null;
		ClearCachedClientID();
		if (HasControls())
		{
			for (int i = 0; i < _controls.Count; i++)
			{
				_controls[i].NullifyUniqueID();
			}
		}
	}

	private bool IsLoadViewStateByID()
	{
		if (loadViewStateByIDCache == null)
		{
			loadViewStateByIDCache = new Dictionary<Type, bool>();
		}
		Type type = GetType();
		if (loadViewStateByIDCache.TryGetValue(type, out var value))
		{
			return value;
		}
		System.ComponentModel.AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
		value = false;
		if (attributes != null)
		{
			foreach (Attribute item in attributes)
			{
				if (item is ViewStateModeByIdAttribute)
				{
					value = true;
					break;
				}
			}
		}
		loadViewStateByIDCache.Add(type, value);
		return value;
	}

	/// <summary>Called after a child control is added to the <see cref="P:System.Web.UI.Control.Controls" /> collection of the <see cref="T:System.Web.UI.Control" /> object.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that has been added. </param>
	/// <param name="index">The index of the control in the <see cref="P:System.Web.UI.Control.Controls" /> collection. </param>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="control" /> is a <see cref="T:System.Web.UI.WebControls.Substitution" />  control.</exception>
	protected internal virtual void AddedControl(Control control, int index)
	{
		ResetControlsCache();
		if (control._parent != null)
		{
			control._parent.Controls.Remove(control);
		}
		control._parent = this;
		Control namingContainer = (((stateMask & 0x40) != 0) ? this : NamingContainer);
		if ((stateMask & 0x1800) != 0)
		{
			control.InitRecursive(namingContainer);
			control.SetMask(262144, val: false);
			if ((stateMask & 0x6000) != 0 && pendingVS != null)
			{
				bool flag = LoadViewStateByID;
				string key;
				object obj;
				if (flag)
				{
					control.EnsureID();
					key = control.ID;
					obj = pendingVS[key];
				}
				else
				{
					key = null;
					obj = pendingVS[index];
				}
				if (obj != null)
				{
					if (flag)
					{
						pendingVS.Remove(key);
					}
					else
					{
						pendingVS.Remove(index);
					}
					if (pendingVS.Count == 0)
					{
						pendingVS = null;
					}
					control.LoadViewStateRecursive(obj);
				}
			}
			if ((stateMask & 0x4000) != 0)
			{
				control.LoadRecursive();
			}
			if ((stateMask & 0x8000) != 0)
			{
				control.PreRenderRecursiveInternal();
			}
		}
		else
		{
			control.SetNamingContainer(namingContainer);
			control.SetMask(262144, val: false);
		}
	}

	private void SetNamingContainer(Control nc)
	{
		if (nc != null)
		{
			_namingContainer = nc;
			if (AutoID)
			{
				EnsureIDInternal();
			}
		}
	}

	/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
	protected virtual void AddParsedSubObject(object obj)
	{
		if (obj is Control child)
		{
			Controls.Add(child);
		}
	}

	/// <summary>Applies the style properties defined in the page style sheet to the control.</summary>
	/// <param name="page">The <see cref="T:System.Web.UI.Page" /> containing the control.</param>
	/// <exception cref="T:System.InvalidOperationException">The style sheet is already applied.</exception>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual void ApplyStyleSheetSkin(Page page)
	{
		if (page != null && EnableTheming && page.StyleSheetPageTheme != null)
		{
			page.StyleSheetPageTheme.GetControlSkin(GetType(), SkinID)?.ApplySkin(this);
		}
	}

	/// <summary>Gathers information about the server control and delivers it to the <see cref="P:System.Web.UI.Page.Trace" /> property to be displayed when tracing is enabled for the page.</summary>
	/// <param name="parentId">The identifier of the control's parent. </param>
	/// <param name="calcViewState">A Boolean that indicates whether the view-state size is calculated. </param>
	[MonoTODO]
	protected void BuildProfileTree(string parentId, bool calcViewState)
	{
	}

	/// <summary>Deletes the control-state information for the server control's child controls. </summary>
	protected void ClearChildControlState()
	{
		_isChildControlStateCleared = true;
	}

	/// <summary>Deletes the view-state and control-state information for all the server control's child controls.</summary>
	protected void ClearChildState()
	{
		ClearChildViewState();
		ClearChildControlState();
	}

	/// <summary>Deletes the view-state information for all the server control's child controls.</summary>
	protected void ClearChildViewState()
	{
		pendingVS = null;
	}

	/// <summary>Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.</summary>
	protected internal virtual void CreateChildControls()
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
	protected virtual ControlCollection CreateControlCollection()
	{
		return new ControlCollection(this);
	}

	/// <summary>Determines whether the server control contains child controls. If it does not, it creates child controls.</summary>
	protected virtual void EnsureChildControls()
	{
		if (!ChildControlsCreated && (stateMask & 8) == 0)
		{
			stateMask |= 8;
			if (Adapter != null)
			{
				Adapter.CreateChildControls();
			}
			else
			{
				CreateChildControls();
			}
			ChildControlsCreated = true;
			stateMask &= -9;
		}
	}

	private void EnsureIDInternal()
	{
		if (_userId == null)
		{
			_userId = NamingContainer.GetDefaultName();
			SetMask(131072, val: true);
		}
	}

	/// <summary>Creates an identifier for controls that do not have an identifier assigned.</summary>
	protected void EnsureID()
	{
		if (NamingContainer != null)
		{
			EnsureIDInternal();
			SetMask(1024, val: true);
		}
	}

	/// <summary>Returns a value indicating whether events are registered for the control or any child controls.</summary>
	/// <returns>
	///     <see langword="true" /> if events are registered; otherwise, <see langword="false" />.</returns>
	protected bool HasEvents()
	{
		return _events != null;
	}

	private void ResetControlsCache()
	{
		_controlsCache = null;
		if ((stateMask & 0x40) == 0 && Parent != null)
		{
			Parent.ResetControlsCache();
		}
	}

	private Hashtable InitControlsCache()
	{
		if (_controlsCache != null)
		{
			return _controlsCache;
		}
		if ((stateMask & 0x40) != 0 || Parent == null)
		{
			_controlsCache = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}
		else
		{
			_controlsCache = Parent.InitControlsCache();
		}
		return _controlsCache;
	}

	private void EnsureControlsCache()
	{
		if (_controlsCache == null)
		{
			InitControlsCache();
			FillControlCache(_controls);
		}
	}

	private void FillControlCache(ControlCollection controls)
	{
		if (controls == null || controls.Count == 0)
		{
			return;
		}
		foreach (Control control in controls)
		{
			try
			{
				if (control._userId != null)
				{
					_controlsCache.Add(control._userId, control);
				}
			}
			catch (ArgumentException)
			{
				throw new HttpException("Multiple controls with the same ID '" + control._userId + "' were found. FindControl requires that controls have unique IDs. ");
			}
			if ((control.stateMask & 0x40) == 0 && control.HasControls())
			{
				FillControlCache(control.Controls);
			}
		}
	}

	/// <summary>Determines if the server control holds only literal content.</summary>
	/// <returns>
	///     <see langword="true" /> if the server control contains solely literal content; otherwise <see langword="false" />.</returns>
	protected bool IsLiteralContent()
	{
		if (_controls != null && _controls.Count == 1 && _controls[0] is LiteralControl)
		{
			return true;
		}
		return false;
	}

	/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter.</summary>
	/// <param name="id">The identifier for the control to be found. </param>
	/// <returns>The specified control, or <see langword="null" /> if the specified control does not exist.</returns>
	[WebSysDescription("")]
	public virtual Control FindControl(string id)
	{
		return FindControl(id, 0);
	}

	private Control LookForControlByName(string id)
	{
		EnsureControlsCache();
		return (Control)_controlsCache[id];
	}

	/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> and an integer, specified in the <paramref name="pathOffset" /> parameter, which aids in the search. You should not override this version of the <see cref="Overload:System.Web.UI.Control.FindControl" /> method.</summary>
	/// <param name="id">The identifier for the control to be found. </param>
	/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
	/// <returns>The specified control, or <see langword="null" /> if the specified control does not exist.</returns>
	protected virtual Control FindControl(string id, int pathOffset)
	{
		EnsureChildControls();
		Control control = null;
		if ((stateMask & 0x40) == 0)
		{
			return NamingContainer?.FindControl(id, pathOffset);
		}
		if (!HasControls())
		{
			return null;
		}
		int num = id.IndexOf(IdSeparator, pathOffset);
		if (num == -1)
		{
			Control control2 = LookForControlByName((pathOffset > 0) ? id.Substring(pathOffset) : id);
			if (control2 != null)
			{
				return control2;
			}
			if (pathOffset == 0)
			{
				control = NamingContainer;
				if (control != null)
				{
					control2 = control.FindControl(id);
					if (control2 != null)
					{
						return control2;
					}
				}
			}
			return null;
		}
		string id2 = id.Substring(pathOffset, num - pathOffset);
		return LookForControlByName(id2)?.FindControl(id, num + 1);
	}

	/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
	protected virtual void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			ViewState.LoadViewState(savedState);
			object obj = ViewState["Visible"];
			if (obj != null)
			{
				SetMask(2, (bool)obj);
				stateMask |= 128;
			}
		}
	}

	/// <summary>Retrieves the physical path that a virtual path, either absolute or relative, maps to.</summary>
	/// <param name="virtualPath">A relative or root relative URL. </param>
	/// <returns>The physical path to the requested file.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="virtualPath" /> is <see langword="null" /> or an empty string ("").</exception>
	protected string MapPathSecure(string virtualPath)
	{
		string virtualPath2 = UrlUtils.Combine(TemplateSourceDirectory, virtualPath);
		return Context.Request.MapPath(virtualPath2);
	}

	/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="args">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	protected virtual bool OnBubbleEvent(object source, EventArgs args)
	{
		return false;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected virtual void OnDataBinding(EventArgs e)
	{
		if ((event_mask & 1) != 0)
		{
			((EventHandler)_events[DataBindingEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal virtual void OnInit(EventArgs e)
	{
		if ((event_mask & 4) != 0)
		{
			((EventHandler)_events[InitEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal virtual void OnLoad(EventArgs e)
	{
		if ((event_mask & 8) != 0)
		{
			((EventHandler)_events[LoadEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal virtual void OnPreRender(EventArgs e)
	{
		if ((event_mask & 0x10) != 0)
		{
			((EventHandler)_events[PreRenderEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data. </param>
	protected internal virtual void OnUnload(EventArgs e)
	{
		if ((event_mask & 0x20) != 0)
		{
			((EventHandler)_events[UnloadEvent])?.Invoke(this, e);
		}
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> used to read a file.</summary>
	/// <param name="path">The path to the desired file.</param>
	/// <returns>A <see cref="T:System.IO.Stream" /> that references the desired file.</returns>
	/// <exception cref="T:System.Web.HttpException">Access to the specified file was denied.</exception>
	protected internal Stream OpenFile(string path)
	{
		try
		{
			return File.OpenRead(Context.Server.MapPath(path));
		}
		catch (UnauthorizedAccessException)
		{
			throw new HttpException("Access to the specified file was denied.");
		}
	}

	internal string GetPhysicalFilePath(string virtualPath)
	{
		Page page = Page;
		if (VirtualPathUtility.IsAbsolute(virtualPath))
		{
			if (page == null)
			{
				return Context.Server.MapPath(virtualPath);
			}
			return page.MapPath(virtualPath);
		}
		MasterPage masterPage = null;
		for (Control parent = Parent; parent != null; parent = parent.Parent)
		{
			if (parent is MasterPage)
			{
				masterPage = parent as MasterPage;
				break;
			}
		}
		string text = ((masterPage == null) ? VirtualPathUtility.Combine(TemplateSourceDirectory + "/", virtualPath) : VirtualPathUtility.Combine(masterPage.TemplateSourceDirectory + "/", virtualPath));
		if (page == null)
		{
			return Context.Server.MapPath(text);
		}
		return page.MapPath(text);
	}

	/// <summary>Assigns any sources of the event and its information to the control's parent.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="args">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected void RaiseBubbleEvent(object source, EventArgs args)
	{
		Control parent = Parent;
		while (parent != null && !parent.OnBubbleEvent(source, args))
		{
			parent = parent.Parent;
		}
	}

	/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content. </param>
	protected internal virtual void Render(HtmlTextWriter writer)
	{
		RenderChildren(writer);
	}

	/// <summary>Outputs the content of a server control's children to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content. </param>
	protected internal virtual void RenderChildren(HtmlTextWriter writer)
	{
		if (_renderMethodDelegate != null)
		{
			_renderMethodDelegate(writer, this);
		}
		else
		{
			if (_controls == null)
			{
				return;
			}
			int count = _controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control = _controls[i];
				if (control != null)
				{
					ControlAdapter controlAdapter = control.Adapter;
					if (controlAdapter != null)
					{
						control.RenderControl(writer, controlAdapter);
					}
					else
					{
						control.RenderControl(writer);
					}
				}
			}
		}
	}

	/// <summary>Gets the control adapter responsible for rendering the specified control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> that will render the control.</returns>
	protected virtual ControlAdapter ResolveAdapter()
	{
		HttpContext context = Context;
		if (context == null)
		{
			return null;
		}
		if (!context.Request.BrowserMightHaveAdapters)
		{
			return null;
		}
		IDictionary adapters = context.Request.Browser.Adapters;
		Type type = GetType();
		Type type2 = (Type)adapters[type];
		while (type2 == null && type != typeof(Control))
		{
			type = type.BaseType;
			type2 = (Type)adapters[type];
		}
		ControlAdapter result = null;
		if (type2 != null)
		{
			result = (ControlAdapter)Activator.CreateInstance(type2);
		}
		return result;
	}

	/// <summary>Saves any server control view-state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns <see langword="null" />.</returns>
	protected virtual object SaveViewState()
	{
		if ((stateMask & 0x80) != 0)
		{
			ViewState["Visible"] = (stateMask & 2) != 0;
		}
		else if (_viewState == null)
		{
			return null;
		}
		return _viewState.SaveViewState();
	}

	/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
	protected virtual void TrackViewState()
	{
		if (_viewState != null)
		{
			_viewState.TrackViewState();
		}
		stateMask |= 256;
	}

	/// <summary>Enables a server control to perform final clean up before it is released from memory.</summary>
	public virtual void Dispose()
	{
		if ((event_mask & 2) != 0)
		{
			((EventHandler)_events[DisposedEvent])?.Invoke(this, EventArgs.Empty);
		}
	}

	/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
	public virtual void DataBind()
	{
		DataBind(raiseOnDataBinding: true);
	}

	/// <summary>Binds a data source to the server control's child controls.</summary>
	protected virtual void DataBindChildren()
	{
		if (HasControls())
		{
			int num = ((_controls != null) ? _controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				_controls[i].DataBind();
			}
		}
	}

	/// <summary>Determines if the server control contains any child controls.</summary>
	/// <returns>
	///     <see langword="true" /> if the control contains other controls; otherwise, <see langword="false" />.</returns>
	public virtual bool HasControls()
	{
		if (_controls != null)
		{
			return _controls.Count > 0;
		}
		return false;
	}

	/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
	public virtual void RenderControl(HtmlTextWriter writer)
	{
		if (adapter != null)
		{
			RenderControl(writer, adapter);
		}
		else if ((stateMask & 2) != 0)
		{
			HttpContext context = Context;
			TraceContext traceContext = context?.Trace;
			int num = 0;
			if (traceContext != null && traceContext.IsEnabled)
			{
				num = context.Response.GetOutputByteCount();
			}
			Render(writer);
			if (traceContext != null && traceContext.IsEnabled)
			{
				int num2 = context.Response.GetOutputByteCount() - num;
				traceContext.SaveSize(this, (num2 >= 0) ? num2 : 0);
			}
		}
	}

	/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object using a provided <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the control content.</param>
	/// <param name="adapter">The <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> that defines the rendering.</param>
	protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
	{
		if ((stateMask & 2) != 0)
		{
			adapter.BeginRender(writer);
			adapter.Render(writer);
			adapter.EndRender(writer);
		}
	}

	/// <summary>Converts a URL into one that is usable on the requesting client.</summary>
	/// <param name="relativeUrl">The URL associated with the <see cref="P:System.Web.UI.Control.TemplateSourceDirectory" /> property. </param>
	/// <returns>The converted URL.</returns>
	/// <exception cref="T:System.ArgumentNullException">Occurs if the <paramref name="relativeUrl" /> parameter contains <see langword="null" />. </exception>
	public string ResolveUrl(string relativeUrl)
	{
		if (relativeUrl == null)
		{
			throw new ArgumentNullException("relativeUrl");
		}
		if (relativeUrl == string.Empty)
		{
			return relativeUrl;
		}
		if (VirtualPathUtility.IsAbsolute(relativeUrl))
		{
			return relativeUrl;
		}
		if (relativeUrl[0] == '#')
		{
			return relativeUrl;
		}
		string appRelativeTemplateSourceDirectory = AppRelativeTemplateSourceDirectory;
		HttpResponse httpResponse = Context?.Response;
		if (appRelativeTemplateSourceDirectory == null || appRelativeTemplateSourceDirectory.Length == 0 || httpResponse == null || relativeUrl.IndexOf(':') >= 0)
		{
			return relativeUrl;
		}
		if (!VirtualPathUtility.IsAppRelative(relativeUrl))
		{
			relativeUrl = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(appRelativeTemplateSourceDirectory), relativeUrl);
		}
		return httpResponse.ApplyAppPathModifier(relativeUrl);
	}

	/// <summary>Gets a URL that can be used by the browser.</summary>
	/// <param name="relativeUrl">A URL relative to the current page.</param>
	/// <returns>A fully qualified URL to the specified resource suitable for use on the browser.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="relativeUrl" /> is <see langword="null" />.</exception>
	public string ResolveClientUrl(string relativeUrl)
	{
		if (relativeUrl == null)
		{
			throw new ArgumentNullException("relativeUrl");
		}
		if (relativeUrl.Length == 0)
		{
			return string.Empty;
		}
		if (VirtualPathUtility.IsAbsolute(relativeUrl) || relativeUrl.IndexOf(':') >= 0)
		{
			return relativeUrl;
		}
		HttpRequest httpRequest = Context?.Request;
		if (httpRequest != null)
		{
			string templateSourceDirectory = TemplateSourceDirectory;
			if (templateSourceDirectory == null || templateSourceDirectory.Length == 0)
			{
				return relativeUrl;
			}
			string text = httpRequest.ClientFilePath;
			if (text.Length > 1 && text[text.Length - 1] != '/')
			{
				text = VirtualPathUtility.GetDirectory(text, normalize: false);
			}
			if (VirtualPathUtility.IsAppRelative(relativeUrl))
			{
				return VirtualPathUtility.MakeRelative(text, relativeUrl);
			}
			string text2 = VirtualPathUtility.AppendTrailingSlash(templateSourceDirectory);
			if (text.Length == text2.Length && string.CompareOrdinal(text, text2) == 0)
			{
				return relativeUrl;
			}
			relativeUrl = VirtualPathUtility.Combine(text2, relativeUrl);
			return VirtualPathUtility.MakeRelative(text, relativeUrl);
		}
		return relativeUrl;
	}

	internal bool HasRenderMethodDelegate()
	{
		return _renderMethodDelegate != null;
	}

	/// <summary>Assigns an event handler delegate to render the server control and its content into its parent control.</summary>
	/// <param name="renderMethod">The information necessary to pass to the delegate so that it can render the server control. </param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public void SetRenderMethodDelegate(RenderMethod renderMethod)
	{
		_renderMethodDelegate = renderMethod;
	}

	internal void LoadRecursive()
	{
		if ((stateMask & 0x4000) == 0)
		{
			if (Adapter != null)
			{
				Adapter.OnLoad(EventArgs.Empty);
			}
			else
			{
				OnLoad(EventArgs.Empty);
			}
		}
		int num = ((_controls != null) ? _controls.Count : 0);
		for (int i = 0; i < num; i++)
		{
			_controls[i].LoadRecursive();
		}
		stateMask |= 16384;
	}

	internal void UnloadRecursive(bool dispose)
	{
		int num = ((_controls != null) ? _controls.Count : 0);
		for (int i = 0; i < num; i++)
		{
			_controls[i].UnloadRecursive(dispose);
		}
		ControlAdapter controlAdapter = Adapter;
		if (controlAdapter != null)
		{
			controlAdapter.OnUnload(EventArgs.Empty);
		}
		else
		{
			OnUnload(EventArgs.Empty);
		}
		if (dispose)
		{
			Dispose();
		}
	}

	internal void PreRenderRecursiveInternal()
	{
		if (Visible)
		{
			SetMask(2, val: true);
			EnsureChildControls();
			if (Adapter != null)
			{
				Adapter.OnPreRender(EventArgs.Empty);
			}
			else
			{
				OnPreRender(EventArgs.Empty);
			}
			if (!HasControls())
			{
				return;
			}
			int num = ((_controls != null) ? _controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				_controls[i].PreRenderRecursiveInternal();
			}
		}
		else
		{
			SetMask(2, val: false);
		}
		stateMask |= 32768;
	}

	internal virtual void InitRecursive(Control namingContainer)
	{
		SetNamingContainer(namingContainer);
		if (HasControls())
		{
			if ((stateMask & 0x40) != 0)
			{
				namingContainer = this;
			}
			int num = ((_controls != null) ? _controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				_controls[i].InitRecursive(namingContainer);
			}
		}
		if ((stateMask & 0x40000) == 0 && (stateMask & 0x800) != 2048)
		{
			stateMask |= 4096;
			ApplyTheme();
			ControlAdapter controlAdapter = Adapter;
			if (controlAdapter != null)
			{
				controlAdapter.OnInit(EventArgs.Empty);
			}
			else
			{
				OnInit(EventArgs.Empty);
			}
			TrackViewState();
			stateMask |= 2048;
			stateMask &= -4097;
		}
	}

	internal object SaveViewStateRecursive()
	{
		TraceContext traceContext = ((Context != null && Context.Trace.IsEnabled) ? Context.Trace : null);
		ArrayList arrayList = null;
		bool flag = LoadViewStateByID;
		if (HasControls())
		{
			int num = ((_controls != null) ? _controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				Control control = _controls[i];
				object obj = control.SaveViewStateRecursive();
				if (obj != null)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					if (flag)
					{
						control.EnsureID();
						arrayList.Add(new Pair(control.ID, obj));
					}
					else
					{
						arrayList.Add(new Pair(i, obj));
					}
				}
			}
		}
		object obj2 = null;
		if (Adapter != null)
		{
			obj2 = Adapter.SaveAdapterViewState();
		}
		object obj3 = null;
		if (IsViewStateEnabled)
		{
			obj3 = SaveViewState();
		}
		if (obj3 == null && arrayList == null)
		{
			traceContext?.SaveViewState(this, null);
			return null;
		}
		traceContext?.SaveViewState(this, obj3);
		obj3 = new object[2] { obj3, obj2 };
		return new Pair(obj3, arrayList);
	}

	internal void LoadViewStateRecursive(object savedState)
	{
		if (savedState == null)
		{
			return;
		}
		Pair obj = (Pair)savedState;
		object[] array = (object[])obj.First;
		if (Adapter != null)
		{
			Adapter.LoadAdapterViewState(array[1]);
		}
		LoadViewState(array[0]);
		if (!(obj.Second is ArrayList { Count: var count } arrayList))
		{
			return;
		}
		bool flag = LoadViewStateByID;
		for (int i = 0; i < count; i++)
		{
			if (!(arrayList[i] is Pair pair))
			{
				continue;
			}
			if (flag)
			{
				string text = (string)pair.First;
				bool flag2 = false;
				foreach (Control control in Controls)
				{
					control.EnsureID();
					if (control.ID == text)
					{
						flag2 = true;
						control.LoadViewStateRecursive(pair.Second);
						break;
					}
				}
				if (!flag2)
				{
					if (pendingVS == null)
					{
						pendingVS = new Hashtable();
					}
					pendingVS[text] = pair.Second;
				}
				continue;
			}
			int num = (int)pair.First;
			if (num < Controls.Count)
			{
				Controls[num].LoadViewStateRecursive(pair.Second);
				continue;
			}
			if (pendingVS == null)
			{
				pendingVS = new Hashtable();
			}
			pendingVS[num] = pair.Second;
		}
		stateMask |= 8192;
	}

	internal void ApplyTheme()
	{
		Page page = Page;
		if (page != null && page.PageTheme != null && EnableTheming)
		{
			page.PageTheme.GetControlSkin(GetType(), SkinID)?.ApplySkin(this);
		}
	}

	/// <summary>Called after a child control is removed from the <see cref="P:System.Web.UI.Control.Controls" /> collection of the <see cref="T:System.Web.UI.Control" /> object.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that has been removed. </param>
	/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
	protected internal virtual void RemovedControl(Control control)
	{
		control.UnloadRecursive(dispose: false);
		control._parent = null;
		control._page = null;
		control._namingContainer = null;
		if ((control.stateMask & 0x20000) != 0)
		{
			control._userId = null;
			control.SetMask(1024, val: false);
		}
		control.NullifyUniqueID();
		control.SetMask(262144, val: true);
		ResetControlsCache();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.GetDesignModeState" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> of the control state.</returns>
	IDictionary IControlDesignerAccessor.GetDesignModeState()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.SetDesignModeState(System.Collections.IDictionary)" />. </summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
	void IControlDesignerAccessor.SetDesignModeState(IDictionary designData)
	{
		SetDesignModeState(designData);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.SetOwnerControl(System.Web.UI.Control)" />. </summary>
	/// <param name="owner">The owner of the control. </param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="owner" /> is set to the current control.</exception>
	void IControlDesignerAccessor.SetOwnerControl(Control control)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets input focus to a control.</summary>
	public virtual void Focus()
	{
		Page.SetFocus(this);
	}

	/// <summary>Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
	protected internal virtual void LoadControlState(object savedState)
	{
	}

	/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>Returns the server control's current state. If there is no state associated with the control, this method returns <see langword="null" />.</returns>
	protected internal virtual object SaveControlState()
	{
		return null;
	}

	/// <summary>Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event. </summary>
	/// <param name="raiseOnDataBinding">
	///       <see langword="true" /> if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, <see langword="false" />.</param>
	protected virtual void DataBind(bool raiseOnDataBinding)
	{
		bool foundDataItem = false;
		if ((stateMask & 0x40) != 0 && Page != null)
		{
			object dataItem = DataBinder.GetDataItem(this, out foundDataItem);
			if (foundDataItem)
			{
				Page.PushDataItemContext(dataItem);
			}
		}
		try
		{
			if (raiseOnDataBinding)
			{
				OnDataBinding(EventArgs.Empty);
			}
			DataBindChildren();
		}
		finally
		{
			if (foundDataItem)
			{
				Page.PopDataItemContext();
			}
		}
	}

	/// <summary>Gets design-time data for a control.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control.</returns>
	protected virtual IDictionary GetDesignModeState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets design-time data for a control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
	protected virtual void SetDesignModeState(IDictionary data)
	{
		throw new NotImplementedException();
	}

	private bool CheckForValidationSupport()
	{
		return GetType().GetCustomAttributes(typeof(SupportsEventValidationAttribute), inherit: false).Length != 0;
	}

	internal void ValidateEvent(string uniqueId, string argument)
	{
		Page page = Page;
		if (page != null && CheckForValidationSupport())
		{
			page.ClientScript.ValidateEvent(uniqueId, argument);
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IParserAccessor.AddParsedSubObject(System.Object)" />. </summary>
	/// <param name="obj">The object to add.</param>
	void IParserAccessor.AddParsedSubObject(object obj)
	{
		AddParsedSubObject(obj);
	}

	/// <summary>Gets the URL that corresponds to a set of route parameters.</summary>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns>The URL that corresponds to the specified route parameters.</returns>
	public string GetRouteUrl(object routeParameters)
	{
		return GetRouteUrl(null, new RouteValueDictionary(routeParameters));
	}

	/// <summary>Gets the URL that corresponds to a set of route parameters.</summary>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns>The URL that corresponds to the specified route parameters.</returns>
	public string GetRouteUrl(RouteValueDictionary routeParameters)
	{
		return GetRouteUrl(null, routeParameters);
	}

	/// <summary>Gets the URL that corresponds to a set of route parameters and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns>The URL that corresponds to the specified route parameters and route name.</returns>
	public string GetRouteUrl(string routeName, object routeParameters)
	{
		return GetRouteUrl(routeName, new RouteValueDictionary(routeParameters));
	}

	/// <summary>Gets the URL that corresponds to a set of route parameters and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeParameters">The route parameters.</param>
	/// <returns>The URL that corresponds to the specified route parameters and route name.</returns>
	public string GetRouteUrl(string routeName, RouteValueDictionary routeParameters)
	{
		HttpRequest httpRequest = (Context ?? HttpContext.Current)?.Request;
		if (httpRequest == null)
		{
			return null;
		}
		return RouteTable.Routes.GetVirtualPath(httpRequest.RequestContext, routeName, routeParameters)?.VirtualPath;
	}

	/// <summary>Returns the prefixed portion of the <see cref="P:System.Web.UI.Control.UniqueID" /> property of the specified control.</summary>
	/// <param name="control">A control that is within a naming container.</param>
	/// <returns>The prefixed portion of the <see cref="P:System.Web.UI.Control.UniqueID" /> property of the specified control.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Control.NamingContainer" /> property of <paramref name="control" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="control" /> is <see langword="null" />.</exception>
	public string GetUniqueIDRelativeTo(Control control)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		Control control2 = this;
		Control namingContainer = control.NamingContainer;
		if (namingContainer != null)
		{
			while (control2 != null && control2 != namingContainer)
			{
				control2 = control2.Parent;
			}
		}
		if (control2 != namingContainer)
		{
			throw new InvalidOperationException($"This control is not a descendant of the NamingContainer of '{control.UniqueID}'");
		}
		int num = control.UniqueID.LastIndexOf(IdSeparator);
		if (num < 0)
		{
			return UniqueID;
		}
		return UniqueID.Substring(num + 1);
	}
}
