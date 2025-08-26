using System.Web.UI.Adapters;

namespace System.Web.UI.WebControls.Adapters;

/// <summary>Customizes rendering for the Web control to which the control adapter is attached, to modify the default markup or behavior for specific browsers.</summary>
public class WebControlAdapter : ControlAdapter
{
	/// <summary>Gets a reference to the Web control to which this control adapter is attached.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.WebControl" /> to which this <see cref="T:System.Web.UI.WebControls.Adapters.WebControlAdapter" /> is attached.</returns>
	protected new WebControl Control => (WebControl)control;

	/// <summary>Gets a value indicating whether the Web control and all its parent controls are enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the associated <see cref="T:System.Web.UI.WebControls.WebControl" /> and all its parent controls are enabled; otherwise, <see langword="false" />.</returns>
	protected bool IsEnabled => Control.IsEnabled;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.WebControlAdapter" /> class.</summary>
	public WebControlAdapter()
	{
	}

	internal WebControlAdapter(WebControl wc)
		: base(wc)
	{
	}

	/// <summary>Generates the target-specific markup for the control to which the control adapter is attached.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		RenderBeginTag(writer);
		RenderContents(writer);
		RenderEndTag(writer);
	}

	/// <summary>Creates the beginning tag for the Web control in the markup that is transmitted to the target browser.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	protected virtual void RenderBeginTag(HtmlTextWriter writer)
	{
		Control.RenderBeginTag(writer);
	}

	/// <summary>Generates the target-specific inner markup for the Web control to which the control adapter is attached.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	protected virtual void RenderContents(HtmlTextWriter writer)
	{
		Control.RenderContents(writer);
	}

	/// <summary>Creates the ending tag for the Web control in the markup that is transmitted to the target browser.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
	protected virtual void RenderEndTag(HtmlTextWriter writer)
	{
		Control.RenderEndTag(writer);
	}
}
