namespace System.Web.UI.WebControls;

internal class ChildTable : Table
{
	private Control parent;

	public ChildTable(Control parent)
	{
		this.parent = parent;
	}

	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.AddAttributesToRender(writer);
		if (ID == null)
		{
			writer.AddAttribute("id", parent.ClientID);
		}
	}
}
