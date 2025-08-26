using System.ComponentModel;

namespace System.Web.UI.Adapters;

/// <summary>Customizes rendering for the derived control to which the adapter is attached, to modify the default markup or behavior for specific browsers, and is the base class from which all control adapters inherit.</summary>
public abstract class ControlAdapter
{
	internal Control control;

	/// <summary>Gets a reference to the browser capabilities of the client making the current HTTP request.</summary>
	/// <returns>An <see cref="T:System.Web.HttpBrowserCapabilities" /> specifying client browser and markup capabilities.</returns>
	protected HttpBrowserCapabilities Browser => Page?.Request.Browser;

	/// <summary>Gets a reference to the control to which this control adapter is attached.</summary>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> to which this <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> is attached.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	protected Control Control => control;

	/// <summary>Gets a reference to the page where the control associated with this adapter resides.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Page" /> that provides access to the page instance where the associated control is situated.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	protected Page Page => Control?.Page;

	/// <summary>Gets a reference to the page adapter for the page where the associated control resides.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Adapters.PageAdapter" /> for the page where the control associated with the current <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> is situated.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	protected PageAdapter PageAdapter => Page?.PageAdapter;

	internal ControlAdapter(Control c)
	{
		control = c;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> class.</summary>
	protected ControlAdapter()
	{
	}

	/// <summary>Called prior to the rendering of a control. In a derived adapter class, generates opening tags that are required by a specific target but not needed by HTML browsers.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	protected internal virtual void BeginRender(HtmlTextWriter writer)
	{
		writer.BeginRender();
	}

	/// <summary>Creates the target-specific child controls for a composite control.</summary>
	protected internal virtual void CreateChildControls()
	{
		Control?.CreateChildControls();
	}

	/// <summary>Called after the rendering of a control. In a derived adapter class, generates closing tags that are required by a specific target but not needed by HTML browsers.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	protected internal virtual void EndRender(HtmlTextWriter writer)
	{
		writer.EndRender();
	}

	/// <summary>Loads adapter control state information that was saved by <see cref="M:System.Web.UI.Adapters.ControlAdapter.SaveAdapterControlState" /> during a previous request to the page where the control associated with this control adapter resides.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />. </param>
	protected internal virtual void LoadAdapterControlState(object state)
	{
	}

	/// <summary>Loads adapter view state information that was saved by <see cref="M:System.Web.UI.Adapters.ControlAdapter.SaveAdapterViewState" /> during a previous request to the page where the control associated with this control adapter resides.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the adapter view state information as a <see cref="T:System.Web.UI.StateBag" />. </param>
	protected internal virtual void LoadAdapterViewState(object state)
	{
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnInit(System.EventArgs)" /> method for the associated control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnInit(EventArgs e)
	{
		Control?.OnInit(e);
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> method for the associated control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnLoad(EventArgs e)
	{
		Control?.OnLoad(e);
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method for the associated control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnPreRender(EventArgs e)
	{
		Control?.OnPreRender(e);
	}

	/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnUnload(System.EventArgs)" /> method for the associated control.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal virtual void OnUnload(EventArgs e)
	{
		Control?.OnUnload(e);
	}

	/// <summary>Generates the target-specific markup for the control to which the control adapter is attached.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to use to render the target-specific output. </param>
	protected internal virtual void Render(HtmlTextWriter writer)
	{
		Control?.Render(writer);
	}

	/// <summary>Generates the target-specific markup for the child controls in a composite control to which the control adapter is attached.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to use to render the target-specific output. </param>
	protected internal virtual void RenderChildren(HtmlTextWriter writer)
	{
		Control?.RenderChildren(writer);
	}

	/// <summary>Saves control state information for the control adapter.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />. </returns>
	protected internal virtual object SaveAdapterControlState()
	{
		return null;
	}

	/// <summary>Saves view state information for the control adapter.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the adapter view state information as a <see cref="T:System.Web.UI.StateBag" />. </returns>
	protected internal virtual object SaveAdapterViewState()
	{
		return null;
	}
}
