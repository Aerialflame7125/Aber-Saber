namespace System.Web.UI.WebControls.Adapters;

/// <summary>Provides the means to modify the behavior of the <see cref="T:System.Web.UI.WebControls.Menu" /> control for specific browsers.</summary>
public class MenuAdapter : WebControlAdapter, IPostBackEventHandler
{
	/// <summary>Retrieves a strongly typed reference to the <see cref="T:System.Web.UI.WebControls.Menu" /> control associated with this <see cref="T:System.Web.UI.WebControls.Adapters.MenuAdapter" /> object.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Menu" /> control associated with the current <see cref="T:System.Web.UI.WebControls.Adapters.MenuAdapter" /> object.</returns>
	protected new Menu Control => (Menu)control;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.MenuAdapter" /> class.</summary>
	public MenuAdapter()
	{
	}

	internal MenuAdapter(Menu c)
		: base(c)
	{
	}

	/// <summary>Registers the associated <see cref="T:System.Web.UI.WebControls.Menu" /> control as one that requires control state.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> data associated with this event.</param>
	protected internal override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}

	/// <summary>Handles the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method for the associated <see cref="T:System.Web.UI.WebControls.Menu" /> control.</summary>
	/// <param name="e">The <see cref="T:System.EventArgs" /> data associated with this event.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Used to maintain the path of the menu when a postback event is raised.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the path of the current node in the menu hierarchy.</param>
	/// <exception cref="T:System.InvalidOperationException">The depth of the current node is more than allowed. This can be caused by an invalid declaration, by a change since the last request, or by a forged (spoofed) request.</exception>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		Control.RaisePostBackEvent(eventArgument);
	}

	/// <summary>Adds tag attributes and writes the markup for the opening tag of the control to the output stream emitted to the browser or device.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance containing methods to build and render the device-specific output.</param>
	protected override void RenderBeginTag(HtmlTextWriter writer)
	{
		base.RenderBeginTag(writer);
	}

	/// <summary>Writes the associated menu items in the associated <see cref="T:System.Web.UI.WebControls.Menu" /> control to the output stream as a series of hyperlinks.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output.</param>
	/// <exception cref="T:System.InvalidOperationException">The depth of the current item is more than allowed.-or-The current item is disabled.</exception>
	protected override void RenderContents(HtmlTextWriter writer)
	{
		base.RenderContents(writer);
	}

	/// <summary>Creates final markup and writes the markup for the closing tag of the control to the output stream emitted to the browser or device.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance containing methods to build and render the device-specific output.</param>
	protected override void RenderEndTag(HtmlTextWriter writer)
	{
		base.RenderEndTag(writer);
	}

	/// <summary>Renders a single menu item as a hyperlink.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance containing methods to build and render the device-specific output.</param>
	/// <param name="item">The <see cref="T:System.Web.UI.WebControls.MenuItem" /> object containing the properties to write to the response stream. </param>
	/// <param name="position">The position of the specified <see cref="T:System.Web.UI.WebControls.MenuItem" /> object in the menu hierarchy.</param>
	protected internal virtual void RenderItem(HtmlTextWriter writer, MenuItem item, int position)
	{
		Control.RenderItem(writer, item, position);
	}

	/// <summary>Loads any control state information that was saved by the <see cref="M:System.Web.UI.WebControls.Adapters.MenuAdapter.SaveAdapterControlState" /> method during a previous request to the page.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> in the form of a dictionary that contains the adapter's state information.</param>
	protected internal override void LoadAdapterControlState(object state)
	{
	}

	/// <summary>Saves any changes to the adapter private control state that have occurred since the page was posted back to the server.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />.</returns>
	protected internal override object SaveAdapterControlState()
	{
		return null;
	}

	/// <summary>Enables the <see cref="T:System.Web.UI.WebControls.Adapters.MenuAdapter" /> class to process an event raised when a page is posted back to the server.</summary>
	/// <param name="eventArgument">A <see cref="T:System.String" /> that contains an optional event argument to pass to the event handler.</param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}
}
