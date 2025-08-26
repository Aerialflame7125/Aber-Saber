namespace System.Web.UI.HtmlControls;

internal class HtmlControlBuilder : ControlBuilder
{
	public override bool HasBody()
	{
		return false;
	}
}
