namespace System.Web.UI.WebControls;

internal sealed class WizardLayoutNavigationContainer : WebControl
{
	protected internal override void Render(HtmlTextWriter writer)
	{
		RenderChildren(writer);
	}
}
