using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.UI.WebControls;

/// <summary>Displays the appropriate content template for a given user, based on the user's authentication status and role membership.</summary>
[DefaultEvent("ViewChanged")]
[DefaultProperty("CurrentView")]
[Designer("System.Web.UI.Design.WebControls.LoginViewDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ParseChildren(true)]
[PersistChildren(false)]
[Themeable(true)]
[Bindable(true)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class LoginView : Control, INamingContainer
{
	private static readonly object viewChangedEvent = new object();

	private static readonly object viewChangingEvent = new object();

	private ITemplate anonymousTemplate;

	private ITemplate loggedInTemplate;

	private bool isAuthenticated;

	private bool theming;

	private RoleGroupCollection coll;

	/// <summary>Gets or sets the template to display to users who are not logged in to the Web site.</summary>
	/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> to display.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(LoginView))]
	public virtual ITemplate AnonymousTemplate
	{
		get
		{
			return anonymousTemplate;
		}
		set
		{
			anonymousTemplate = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.ControlCollection" /> object that contains the child controls for the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
	/// <returns>The collection of child controls for the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</returns>
	public override ControlCollection Controls
	{
		get
		{
			EnsureChildControls();
			return base.Controls;
		}
	}

	/// <summary>Gets or sets a value indicating whether themes can be applied to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. </summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[Browsable(true)]
	public override bool EnableTheming
	{
		get
		{
			return theming;
		}
		set
		{
			theming = value;
		}
	}

	/// <summary>Gets or sets the template to display to Web site users who are logged in to the Web site but are not members of one of the role groups specified in the <see cref="P:System.Web.UI.WebControls.LoginView.RoleGroups" /> property.</summary>
	/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> to display.</returns>
	[Browsable(false)]
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[TemplateContainer(typeof(LoginView))]
	public virtual ITemplate LoggedInTemplate
	{
		get
		{
			return loggedInTemplate;
		}
		set
		{
			loggedInTemplate = value;
		}
	}

	/// <summary>Gets a collection of role groups that associate content templates with particular roles.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> object that contains the defined role-group templates.</returns>
	[Filterable(false)]
	[MergableProperty(false)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Themeable(false)]
	public RoleGroupCollection RoleGroups
	{
		get
		{
			if (coll == null)
			{
				coll = new RoleGroupCollection();
			}
			return coll;
		}
	}

	/// <summary>Gets or sets the skin to apply to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
	/// <returns>The name of the skin to apply to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. The default value is an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentException">The skin specified in the <see cref="P:System.Web.UI.WebControls.LoginView.SkinID" /> property does not exist in the theme. </exception>
	[Browsable(true)]
	public override string SkinID
	{
		get
		{
			return base.SkinID;
		}
		set
		{
			base.SkinID = value;
		}
	}

	private bool IsAuthenticated
	{
		get
		{
			return isAuthenticated;
		}
		set
		{
			if (value != isAuthenticated)
			{
				isAuthenticated = value;
				OnViewChanging(EventArgs.Empty);
				base.ChildControlsCreated = false;
				OnViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Occurs after the view is changed.</summary>
	public event EventHandler ViewChanged
	{
		add
		{
			base.Events.AddHandler(viewChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(viewChangedEvent, value);
		}
	}

	/// <summary>Occurs before the view is changed.</summary>
	public event EventHandler ViewChanging
	{
		add
		{
			base.Events.AddHandler(viewChangingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(viewChangingEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
	public LoginView()
	{
		theming = true;
	}

	private ITemplate GetTemplateFromRoleGroup(RoleGroup rg, IPrincipal user)
	{
		if (user == null)
		{
			return null;
		}
		string[] roles = rg.Roles;
		foreach (string role in roles)
		{
			if (user.IsInRole(role))
			{
				return rg.ContentTemplate;
			}
		}
		return null;
	}

	/// <summary>Creates the child controls that make up the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
	protected internal override void CreateChildControls()
	{
		Controls.Clear();
		Control control = new Control();
		ITemplate template = null;
		if (Page != null && Page.Request.IsAuthenticated)
		{
			isAuthenticated = true;
			IPrincipal user = HttpContext.Current?.User;
			RoleGroupCollection roleGroups;
			if (Roles.Enabled && (roleGroups = RoleGroups) != null && roleGroups.Count > 0)
			{
				foreach (RoleGroup item in roleGroups)
				{
					template = GetTemplateFromRoleGroup(item, user);
					if (template != null)
					{
						break;
					}
				}
			}
			if (template == null)
			{
				template = LoggedInTemplate;
			}
		}
		else
		{
			isAuthenticated = false;
			template = AnonymousTemplate;
		}
		template?.InstantiateIn(control);
		Controls.Add(control);
	}

	/// <summary>Binds a data source to <see cref="T:System.Web.UI.WebControls.LoginView" /> and all its child controls.</summary>
	public override void DataBind()
	{
		EventArgs empty = EventArgs.Empty;
		OnDataBinding(empty);
		EnsureChildControls();
		DataBindChildren();
	}

	/// <summary>Sets input focus to a control.</summary>
	/// <exception cref="T:System.NotSupportedException">You call the <see cref="M:System.Web.UI.WebControls.LoginView.Focus" /> method.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void Focus()
	{
		throw new NotSupportedException();
	}

	/// <summary>This method implements <see cref="M:System.Web.UI.Control.LoadControlState(System.Object)" />.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
	protected internal override void LoadControlState(object savedState)
	{
		if (savedState != null)
		{
			isAuthenticated = (bool)savedState;
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event. </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
		if (Page != null)
		{
			Page.RegisterRequiresControlState(this);
		}
	}

	/// <summary>Determines which role-group template to display, based on the roles of the logged-in user.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		if (Page != null)
		{
			IsAuthenticated = Page.Request.IsAuthenticated;
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginView.ViewChanged" /> event after the <see cref="T:System.Web.UI.WebControls.LoginView" /> control switches views.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnViewChanged(EventArgs e)
	{
		((EventHandler)base.Events[viewChangedEvent])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginView.ViewChanging" /> event before the <see cref="T:System.Web.UI.WebControls.LoginView" /> control switches views.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected virtual void OnViewChanging(EventArgs e)
	{
		((EventHandler)base.Events[viewChangingEvent])?.Invoke(this, e);
	}

	/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the server control content on the client's browser.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		EnsureChildControls();
		base.Render(writer);
	}

	/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
	/// <returns>Returns the server control's current state. If there is no state associated with the control, this method returns <see langword="null" />.
	/// </returns>
	protected internal override object SaveControlState()
	{
		if (isAuthenticated)
		{
			return isAuthenticated;
		}
		return null;
	}

	/// <summary>Sets design-time data for a control.</summary>
	/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control.</param>
	[MonoTODO("for design-time usage - no more details available")]
	protected override void SetDesignModeState(IDictionary data)
	{
		base.SetDesignModeState(data);
	}
}
