using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Provides an interactive user interface (UI) element that enables users to perform actions on a Web Parts page.</summary>
[TypeConverter("System.Web.UI.WebControls.WebParts.WebPartVerbConverter, System.Web")]
public class WebPartVerb : IStateManager
{
	private string clientClickHandler;

	private WebPartEventHandler serverClickHandler;

	private StateBag stateBag;

	private bool isChecked;

	private string description = string.Empty;

	private bool enabled = true;

	private string imageUrl = string.Empty;

	private string text = string.Empty;

	private bool visible = true;

	private string id;

	/// <summary>Gets a string containing a unique ID for a verb.</summary>
	/// <returns>A string containing the ID for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" />.</returns>
	public string ID => id;

	/// <summary>Implements the <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" /> property by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="P:System.Web.UI.WebControls.WebParts.WebPartVerb.IsTrackingViewState" /> property.</summary>
	/// <returns>
	///     <see langword="true" /> if view state is being tracked for a verb; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	bool IStateManager.IsTrackingViewState
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating that some state associated with a custom verb is currently active or selected.</summary>
	/// <returns>
	///     <see langword="true" /> if a state associated with a custom verb is currently active; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[WebSysDescription("Denotes verb is checked or not.")]
	[DefaultValue(false)]
	[NotifyParentProperty(true)]
	public virtual bool Checked
	{
		get
		{
			return isChecked;
		}
		set
		{
			isChecked = value;
		}
	}

	/// <summary>Gets the string containing the method name of the client-side event handler defined in the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> constructor.</summary>
	/// <returns>A string that contains the name of the method that handles client-side click events. The default is an empty string ("").</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string ClientClickHandler => clientClickHandler;

	/// <summary>Gets or sets a short description of the verb.</summary>
	/// <returns>A string containing a description of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" />. </returns>
	[Localizable(true)]
	[WebSysDescription("Gives descriptive information about the verb")]
	[NotifyParentProperty(true)]
	public virtual string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether a verb is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the verb is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[NotifyParentProperty(true)]
	[DefaultValue(true)]
	[WebSysDescription("Determines whether verb is enabled.")]
	public virtual bool Enabled
	{
		get
		{
			return enabled;
		}
		set
		{
			enabled = value;
		}
	}

	/// <summary>Gets or sets a string containing a URL to an image that represents a verb in the user interface (UI).</summary>
	/// <returns>A string that contains the URL to an image. The default is an empty string ("").</returns>
	[WebSysDescription("Denotes URL of the image to be displayed for the verb")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design", "UITypeEditor, System.Drawing")]
	[Localizable(true)]
	[NotifyParentProperty(true)]
	public string ImageUrl
	{
		get
		{
			return imageUrl;
		}
		set
		{
			imageUrl = value;
		}
	}

	/// <summary>Gets a value that indicates whether view state is currently being tracked for a verb.</summary>
	/// <returns>
	///     <see langword="true" /> if view state is being tracked; otherwise, <see langword="false" />.</returns>
	protected virtual bool IsTrackingViewState
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a reference to the method that handles server-side click events for the verb.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles server-side click events.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public WebPartEventHandler ServerClickHandler => serverClickHandler;

	/// <summary>Gets or sets the text label for a verb that is displayed in the user interface (UI).</summary>
	/// <returns>A string containing the text label for a verb. The default is an empty string ("").</returns>
	[WebSysDescription("Denotes text to be displayed for the verb")]
	[Localizable(true)]
	[NotifyParentProperty(true)]
	public virtual string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a server control across multiple requests for the same page.</summary>
	/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the server control's view-state information.</returns>
	protected StateBag ViewState => stateBag;

	/// <summary>Gets or sets a value that indicates whether a verb is visible to users.</summary>
	/// <returns>
	///     <see langword="true" /> if the verb is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("Denotes whether the verb is visible")]
	[Localizable(true)]
	[NotifyParentProperty(true)]
	public bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			visible = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates a client-side click event handler with the instance.</summary>
	/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
	/// <param name="clientClickHandler">A <see cref="T:System.String" /> that refers to the client-side handler for click events.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="clientClickHandler" /> parameter is <see langword="null" />.</exception>
	public WebPartVerb(string id, string clientClickHandler)
	{
		this.id = id;
		this.clientClickHandler = clientClickHandler;
		stateBag = new StateBag();
		stateBag.Add("clientClickHandler", clientClickHandler);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates a server-side click event handler with the instance.</summary>
	/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
	/// <param name="serverClickHandler">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles click events on the server.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="serverClickHandler" /> parameter is <see langword="null" />.</exception>
	public WebPartVerb(string id, WebPartEventHandler serverClickHandler)
	{
		this.id = id;
		this.serverClickHandler = serverClickHandler;
		stateBag = new StateBag();
		stateBag.Add("serverClickHandler", serverClickHandler);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates both client and server-side click event handlers with the instance.</summary>
	/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
	/// <param name="serverClickHandler">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles click events on the server.</param>
	/// <param name="clientClickHandler">A <see cref="T:System.String" /> that refers to the client-side handler for click events.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="clientClickHandler" /> parameter is <see langword="null" />.- or -The <paramref name="serverClickHandler" /> parameter is <see langword="null" />.</exception>
	public WebPartVerb(string id, WebPartEventHandler serverClickHandler, string clientClickHandler)
	{
		this.id = id;
		this.serverClickHandler = serverClickHandler;
		this.clientClickHandler = clientClickHandler;
		stateBag = new StateBag();
		stateBag.Add("serverClickHandler", serverClickHandler);
		stateBag.Add("clientClickHandler", clientClickHandler);
	}

	/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that contains the view state to be restored.</param>
	[MonoTODO("Not implemented")]
	protected virtual void LoadViewState(object savedState)
	{
		throw new NotImplementedException();
	}

	/// <summary>Saves a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object's view-state changes that occurred since the page was last posted back to the server.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the state data to be saved.</returns>
	[MonoTODO("Not implemented")]
	protected virtual object SaveViewState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Tracks view-state changes to a verb so the changes can be stored in the verb's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
	[MonoTODO("Not implemented")]
	protected virtual void TrackViewState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" /> method of the <see cref="T:System.Web.UI.IStateManager" /> interface by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.LoadViewState(System.Object)" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that contains the view state to be restored. </param>
	[MonoTODO("Not implemented")]
	void IStateManager.LoadViewState(object savedState)
	{
		throw new NotImplementedException();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.SaveViewState" /> method by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.SaveViewState" /> method.</summary>
	/// <returns>Returns an <see cref="T:System.Object" /> containing the control's current view state. If no view state is associated with the control, this method returns <see langword="null" />.</returns>
	[MonoTODO("Not implemented")]
	object IStateManager.SaveViewState()
	{
		throw new NotImplementedException();
	}

	/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.TrackViewState" /> method by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.TrackViewState" /> method.</summary>
	[MonoTODO("Not implemented")]
	void IStateManager.TrackViewState()
	{
		throw new NotImplementedException();
	}
}
