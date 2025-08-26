using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.Web.UI.Design;

/// <summary>Provides a base class for the design-time functionality of a Web Forms page and allows access to and manipulation of components and controls that are contained within the Web Forms page at design time.</summary>
public abstract class WebFormsRootDesigner : IRootDesigner, IDesigner, IDisposable, IDesignerFilter
{
	/// <summary>When overridden in a derived class, gets the URL at which the Web Forms page is located.</summary>
	/// <returns>The URL at which the Web Forms page is located; otherwise, <see langword="null" />, if the Web Forms page has no associated URL.</returns>
	public abstract string DocumentUrl { get; }

	/// <summary>When overridden in a derived class, gets a value indicating whether the designer view is locked.</summary>
	/// <returns>
	///   <see langword="true" />, if the designer view is locked; otherwise, <see langword="false" />.</returns>
	public abstract bool IsDesignerViewLocked { get; }

	/// <summary>When overridden in a derived class, gets a value indicating whether the Web Forms page is still loading.</summary>
	/// <returns>
	///   <see langword="true" />, if the Web Forms page is loading; otherwise, <see langword="false" />.</returns>
	public abstract bool IsLoading { get; }

	/// <summary>When overridden in a derived class, gets a <see cref="T:System.Web.UI.Design.WebFormsReferenceManager" /> object that has information about the current Web Forms page.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.WebFormsReferenceManager" /> containing information about the current Web Forms page.</returns>
	public abstract WebFormsReferenceManager ReferenceManager { get; }

	/// <summary>Gets or sets the component that this designer is designing.</summary>
	/// <returns>The component managed by the designer.</returns>
	[System.MonoTODO]
	public virtual IComponent Component
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the culture information for the current thread.</summary>
	/// <returns>The culture information for the current thread.</returns>
	[System.MonoTODO]
	public CultureInfo CurrentCulture
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets an array of technologies that the designer component can support for its display.</summary>
	/// <returns>An array of supported <see cref="T:System.ComponentModel.Design.ViewTechnology" /> values.</returns>
	[System.MonoTODO]
	protected ViewTechnology[] SupportedTechnologies
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the design-time verbs that are supported by the designer.</summary>
	/// <returns>An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects supported by the designer; otherwise, <see langword="null" />, if the component has no verbs.</returns>
	[System.MonoTODO]
	protected DesignerVerbCollection Verbs
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the design-time verbs that are supported by the designer. For a description of this member, see <see cref="P:System.ComponentModel.Design.IDesigner.Verbs" />.</summary>
	/// <returns>The design-time verbs that are supported by the designer.</returns>
	[System.MonoTODO]
	DesignerVerbCollection IDesigner.Verbs => Verbs;

	/// <summary>Gets an array of technologies that the designer component can support for its display. For a description of this member, see <see cref="P:System.ComponentModel.Design.IRootDesigner.SupportedTechnologies" />.</summary>
	/// <returns>An array of technologies that the designer component can support for its display.</returns>
	[System.MonoTODO]
	ViewTechnology[] IRootDesigner.SupportedTechnologies => SupportedTechnologies;

	/// <summary>Occurs when the designer completes loading the Web Forms page.</summary>
	public event EventHandler LoadComplete;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> class.</summary>
	protected WebFormsRootDesigner()
	{
	}

	/// <summary>Frees the resources of the current <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> object before it is reclaimed by the garbage collector.</summary>
	~WebFormsRootDesigner()
	{
	}

	/// <summary>When overridden in a derived class, adds a client script element to the current Web Forms page.</summary>
	/// <param name="scriptItem">A <see cref="T:System.Web.UI.Design.ClientScriptItem" /> to add to the Web Forms page.</param>
	public abstract void AddClientScriptToDocument(ClientScriptItem scriptItem);

	/// <summary>When overridden in a derived class, adds a Web server control to the Web Forms page.</summary>
	/// <param name="newControl">The control to add to the Web Forms page.</param>
	/// <param name="referenceControl">The control relative to which <paramref name="newControl" /> is added.</param>
	/// <param name="location">A <see cref="T:System.Web.UI.Design.ControlLocation" /> value that indicates the location for <paramref name="newControl" /> relative to <paramref name="referenceControl" />.</param>
	/// <returns>The ID of the control that was added.</returns>
	public abstract string AddControlToDocument(Control newControl, Control referenceControl, ControlLocation location);

	/// <summary>When overridden in a derived class, returns a <see cref="T:System.Web.UI.Design.ClientScriptItemCollection" /> object that contains all client script items that are on the page.</summary>
	/// <returns>An object that contains all client script items that are on the page.</returns>
	public abstract ClientScriptItemCollection GetClientScriptsInDocument();

	/// <summary>When overridden in a derived class, returns both the current design-time view and the HTML markup for the specified control.</summary>
	/// <param name="control">The control to provide the view and tag for.</param>
	/// <param name="view">When the <see cref="M:System.Web.UI.Design.WebFormsRootDesigner.GetControlViewAndTag(System.Web.UI.Control,System.Web.UI.Design.IControlDesignerView@,System.Web.UI.Design.IControlDesignerTag@)" /> method returns, <paramref name="view" /> contains an <see langword="IControlDesignerView" /> object that provides access to the visual representation and content of a control at design time. <paramref name="view" /> is passed uninitialized.</param>
	/// <param name="tag">When the <see cref="M:System.Web.UI.Design.WebFormsRootDesigner.GetControlViewAndTag(System.Web.UI.Control,System.Web.UI.Design.IControlDesignerView@,System.Web.UI.Design.IControlDesignerTag@)" /> method returns, <paramref name="tag" /> contains an <see langword="IControlDesignerTag" /> object that provides access to the HTML element for the control's control designer. <paramref name="tag" /> is passed uninitialized.</param>
	protected internal abstract void GetControlViewAndTag(Control control, out IControlDesignerView view, out IControlDesignerTag tag);

	/// <summary>Removes the specified client script from the document at design time.</summary>
	/// <param name="clientScriptId">The identifier for the previously registered client script.</param>
	public abstract void RemoveClientScriptFromDocument(string clientScriptId);

	/// <summary>When overridden in a derived class, removes the specified control from the Web Forms page.</summary>
	/// <param name="control">The control to remove from the Web Forms page.</param>
	public abstract void RemoveControlFromDocument(Control control);

	/// <summary>Returns a design-time <see cref="T:System.ComponentModel.Design.DesignerActionService" /> object.</summary>
	/// <param name="serviceProvider">A design host, such as Visual Studio 2005, cast as an <see cref="T:System.IServiceProvider" />.</param>
	/// <returns>A design-time designer action service object.</returns>
	[System.MonoTODO]
	protected virtual DesignerActionService CreateDesignerActionService(IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an <see cref="T:System.Web.UI.IUrlResolutionService" /> that resolves relative URLs.</summary>
	/// <returns>An object that resolves relative URLs.</returns>
	[System.MonoTODO]
	protected virtual IUrlResolutionService CreateUrlResolutionService()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources that are used by the <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected virtual void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates empty HTML markup for a control at design time.</summary>
	/// <param name="control">The control to generate HTML markup for.</param>
	/// <returns>HTML markup for an empty control.</returns>
	[System.MonoTODO]
	public virtual string GenerateEmptyDesignTimeHtml(Control control)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates HTML markup that is used to display an error message at design time by using the specified control, exception, and message.</summary>
	/// <param name="control">The control that raised the exception.  
	///  -or-  
	///  <see langword="null" />.</param>
	/// <param name="e">The exception.  
	///  -or-  
	///  <see langword="null" />.</param>
	/// <param name="errorMessage">A message to add to the exception message.  
	///  -or-  
	///  An empty string ("").</param>
	/// <returns>HTML markup for a control and exception information.</returns>
	[System.MonoTODO]
	public virtual string GenerateErrorDesignTimeHtml(Control control, Exception e, string errorMessage)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the requested service.</summary>
	/// <param name="serviceType">The type of service to retrieve.</param>
	/// <returns>The requested service; otherwise, <see langword="null" />, if the service cannot be resolved.</returns>
	[System.MonoTODO]
	protected internal virtual object GetService(Type serviceType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a view object that is determined by the provided <see cref="T:System.ComponentModel.Design.ViewTechnology" /> object.</summary>
	/// <param name="viewTechnology">A <see cref="T:System.ComponentModel.Design.ViewTechnology" /> obtained from the <see cref="P:System.Web.UI.Design.WebFormsRootDesigner.SupportedTechnologies" /> property.</param>
	/// <returns>An object containing the current view of the component.</returns>
	[System.MonoTODO]
	protected object GetView(ViewTechnology viewTechnology)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.Design.WebFormsRootDesigner" /> object using the specified component.</summary>
	/// <param name="component">The component that this designer is designing.</param>
	[System.MonoTODO]
	public virtual void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Design.WebFormsRootDesigner.LoadComplete" /> event when the Web Forms page is completely loaded.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" />.</param>
	[System.MonoTODO]
	protected virtual void OnLoadComplete(EventArgs e)
	{
		if (this.LoadComplete != null)
		{
			this.LoadComplete(this, e);
		}
	}

	/// <summary>Allows a designer to change or remove items from the set of attributes that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <param name="attributes">The attributes for the class of the component.</param>
	[System.MonoTODO]
	protected virtual void PostFilterAttributes(IDictionary attributes)
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows a designer to change or remove items from the set of events that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <param name="events">The events for the class of the component.</param>
	[System.MonoTODO]
	protected virtual void PostFilterEvents(IDictionary events)
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows a designer to change or remove items from the set of properties that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <param name="properties">The properties for the class of the component.</param>
	[System.MonoTODO]
	protected virtual void PostFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows a designer to add to the set of attributes that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <param name="attributes">The attributes for the class of the component.</param>
	[System.MonoTODO]
	protected virtual void PreFilterAttributes(IDictionary attributes)
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows a designer to add items to the set of events that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <param name="events">The events for the class of the component.</param>
	[System.MonoTODO]
	protected virtual void PreFilterEvents(IDictionary events)
	{
		throw new NotImplementedException();
	}

	/// <summary>Allows a designer to add items to the set of properties that the designer exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
	/// <param name="properties">The properties for the class of the component.</param>
	[System.MonoTODO]
	protected virtual void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts a relative URL into a fully qualified URL.</summary>
	/// <param name="relativeUrl">A relative URL for a resource on the site.</param>
	/// <returns>A fully qualified URL resolved from <paramref name="relativeUrl" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="relativeUrl" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public string ResolveUrl(string relativeUrl)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the <see langword="ID" /> property of the specified control with the specified string.</summary>
	/// <param name="control">The control on which to set the ID.</param>
	/// <param name="id">The string to set as the ID for the control.</param>
	[System.MonoTODO]
	public virtual void SetControlID(Control control, string id)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesigner.DoDefaultAction" />.</summary>
	[System.MonoTODO]
	void IDesigner.DoDefaultAction()
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterAttributes(System.Collections.IDictionary)" />.</summary>
	/// <param name="attributes">The attribute objects for the class of the component.</param>
	[System.MonoTODO]
	void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterEvents(System.Collections.IDictionary)" />.</summary>
	/// <param name="events">The event descriptor objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
	[System.MonoTODO]
	void IDesignerFilter.PostFilterEvents(IDictionary events)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterProperties(System.Collections.IDictionary)" />.</summary>
	/// <param name="properties">The property descriptor objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
	[System.MonoTODO]
	void IDesignerFilter.PostFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterAttributes(System.Collections.IDictionary)" />.</summary>
	/// <param name="attributes">The attribute objects for the class of the component.</param>
	[System.MonoTODO]
	void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterEvents(System.Collections.IDictionary)" />.</summary>
	/// <param name="events">The event descriptor objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
	[System.MonoTODO]
	void IDesignerFilter.PreFilterEvents(IDictionary events)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterProperties(System.Collections.IDictionary)" />.</summary>
	/// <param name="properties">The property descriptor objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
	[System.MonoTODO]
	void IDesignerFilter.PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a view object for the specified view technology. For a description of this member, see <see cref="M:System.ComponentModel.Design.IRootDesigner.GetView(System.ComponentModel.Design.ViewTechnology)" />.</summary>
	/// <param name="viewTechnology">The view technology.</param>
	/// <returns>The view object for the specified view technology.</returns>
	[System.MonoTODO]
	object IRootDesigner.GetView(ViewTechnology viewTechnology)
	{
		throw new NotImplementedException();
	}

	/// <summary>For a description of this member, see <see cref="M:System.IDisposable.Dispose" />.</summary>
	[System.MonoTODO]
	void IDisposable.Dispose()
	{
		Dispose(disposing: true);
	}
}
