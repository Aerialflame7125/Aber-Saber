using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.SessionState;

namespace System.Web.UI;

/// <summary>
///     Represents an .ascx file, also known as a user control, requested from a server that hosts an ASP.NET Web application. The file must be called from a Web Forms page or a parser error will occur.</summary>
[ControlBuilder(typeof(UserControlControlBuilder))]
[DefaultEvent("Load")]
[DesignerCategory("ASPXCodeBehind")]
[ToolboxItem(false)]
[Designer("System.Web.UI.Design.UserControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
[ParseChildren(true)]
[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class UserControl : TemplateControl, IAttributeAccessor, IUserControlDesignerAccessor, INamingContainer, IFilterResolutionService, INonBindingContainer
{
	private ControlCachePolicy cachePolicy;

	private bool initialized;

	private AttributeCollection attributes;

	private StateBag attrBag;

	/// <summary>Gets the <see cref="P:System.Web.HttpContext.Application" /> object for the current Web request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> object for the current Web request.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpApplicationState Application => Page?.Application;

	/// <summary>Gets a collection of all attribute name and value pairs declared in the user control tag within the .aspx file.</summary>
	/// <returns>An <see cref="T:System.Web.UI.AttributeCollection" /> object that contains all the name and value pairs declared in the user control tag.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public AttributeCollection Attributes
	{
		get
		{
			EnsureAttributes();
			return attributes;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object that is associated with the application that contains the user control.</summary>
	/// <returns>The <see cref="T:System.Web.Caching.Cache" /> object in which to store the user control's data.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public Cache Cache => Page?.Cache;

	/// <summary>Gets a reference to a collection of caching parameters for this user control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCachePolicy" /> containing properties that define the caching parameters for this <see cref="T:System.Web.UI.UserControl" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ControlCachePolicy CachePolicy
	{
		get
		{
			if (Parent is BasePartialCachingControl basePartialCachingControl)
			{
				return basePartialCachingControl.CachePolicy;
			}
			if (cachePolicy == null)
			{
				cachePolicy = new ControlCachePolicy();
			}
			return cachePolicy;
		}
	}

	/// <summary>Gets a value indicating whether the user control is being loaded in response to a client postback, or if it is being loaded and accessed for the first time.</summary>
	/// <returns>
	///     <see langword="true" /> if the user control is being loaded in response to a client postback; otherwise, <see langword="false" />.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsPostBack => Page?.IsPostBack ?? false;

	/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the current Web request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpRequest" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpRequest Request => Page?.Request;

	/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the current Web request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpResponse" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpResponse Response => Page?.Response;

	/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> object for the current Web request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpServerUtility Server => Page?.Server;

	/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current Web request.</summary>
	/// <returns>An <see cref="T:System.Web.SessionState.HttpSessionState" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public HttpSessionState Session => Page?.Session;

	/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</summary>
	/// <returns>The data from the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public TraceContext Trace => Page?.Trace;

	/// <summary>Gets or sets the text that appears between the opening and closing tags of a user control.</summary>
	/// <returns>The text that appears between the opening and closing tabs of a user control.</returns>
	string IUserControlDesignerAccessor.InnerText
	{
		get
		{
			string text = (string)ViewState["!DesignTimeInnerText"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			ViewState["!DesignTimeInnerText"] = value;
		}
	}

	/// <summary>Gets or sets the full tag name of the user control.</summary>
	/// <returns>The full tag name of the user control.</returns>
	string IUserControlDesignerAccessor.TagName
	{
		get
		{
			string text = (string)ViewState["!DesignTimeTagName"];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}
		set
		{
			ViewState["!DesignTimeTagName"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.UserControl" /> class.</summary>
	public UserControl()
	{
	}

	private void EnsureAttributes()
	{
		if (attributes == null)
		{
			attrBag = new StateBag(ignoreCase: true);
			if (base.IsTrackingViewState)
			{
				attrBag.TrackViewState();
			}
			attributes = new AttributeCollection(attrBag);
		}
	}

	/// <summary>Performs any initialization steps on the user control that are required by RAD designers.</summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void DesignerInitialize()
	{
		InitRecursive(null);
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.UserControl" /> object that has been created declaratively. Since there are some differences between pages and user controls, this method makes sure that the user control is initialized properly.</summary>
	/// <param name="page">The <see cref="T:System.Web.UI.Page" /> object that contains the user control. </param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void InitializeAsUserControl(Page page)
	{
		if (!initialized)
		{
			Page = page;
			InitializeAsUserControlInternal();
		}
	}

	internal void InitializeAsUserControlInternal()
	{
		if (!initialized)
		{
			initialized = true;
			WireupAutomaticEvents();
			FrameworkInitialize();
		}
	}

	/// <summary>Assigns a virtual file path, either absolute or relative, to a physical file path.</summary>
	/// <param name="virtualPath">The virtual file path to map. </param>
	/// <returns>The physical path to the file.</returns>
	public string MapPath(string virtualPath)
	{
		return Request.MapPath(virtualPath, TemplateSourceDirectory, allowCrossAppMapping: true);
	}

	/// <summary>Restores the view-state information from a previous user control request that was saved by the <see cref="M:System.Web.UI.UserControl.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the user control state to be restored. </param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			Pair pair = (Pair)savedState;
			base.LoadViewState(pair.First);
			if (pair.Second != null)
			{
				EnsureAttributes();
				attrBag.LoadViewState(pair.Second);
			}
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
	protected internal override void OnInit(EventArgs e)
	{
		InitializeAsUserControl(Page);
		base.OnInit(e);
	}

	/// <summary>Saves any user control view-state changes that have occurred since the last page postback.</summary>
	/// <returns>Returns the user control's current view state. If there is no view state associated with the control, it returns <see langword="null" />.</returns>
	protected override object SaveViewState()
	{
		object obj = base.SaveViewState();
		object obj2 = null;
		if (attributes != null)
		{
			obj2 = attrBag.SaveViewState();
		}
		if (obj == null && obj2 == null)
		{
			return null;
		}
		return new Pair(obj, obj2);
	}

	/// <summary>Returns the value of the specified user control attribute.</summary>
	/// <param name="name">The name of the attribute to get the value of.</param>
	/// <returns>The value of the specified user control attribute.</returns>
	string IAttributeAccessor.GetAttribute(string name)
	{
		if (attributes == null)
		{
			return null;
		}
		return attributes[name];
	}

	/// <summary>Sets the value of the specified user control attribute.</summary>
	/// <param name="name">The name of the attribute to set.</param>
	/// <param name="value">The value of the attribute to set.</param>
	void IAttributeAccessor.SetAttribute(string name, string value)
	{
		EnsureAttributes();
		Attributes[name] = value;
	}

	[MonoTODO("Not implemented")]
	int IFilterResolutionService.CompareFilters(string filter1, string filter2)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	bool IFilterResolutionService.EvaluateFilter(string filterName)
	{
		throw new NotImplementedException();
	}
}
