namespace System.Web.UI.WebControls;

internal sealed class WizardLayoutContainer : WebControl
{
	protected internal override void Render(HtmlTextWriter writer)
	{
		RenderChildren(writer);
	}
}
