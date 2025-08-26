namespace System.Web.UI.WebControls.Adapters;

/// <summary>Provides rendering capabilities for the associated Web control to modify the default markup or behavior for a specific browser.</summary>
public class HideDisabledControlAdapter : WebControlAdapter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.HideDisabledControlAdapter" /> class. </summary>
	public HideDisabledControlAdapter()
	{
	}

	internal HideDisabledControlAdapter(WebControl c)
		: base(c)
	{
	}

	/// <summary>Writes the associated Web control to the output stream as HTML.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output. </param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (base.Control.IsEnabled)
		{
			base.Render(writer);
		}
	}
}
