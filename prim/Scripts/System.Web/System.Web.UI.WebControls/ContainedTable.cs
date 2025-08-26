namespace System.Web.UI.WebControls;

internal class ContainedTable : Table
{
	private WebControl _container;

	public ContainedTable(WebControl container)
	{
		_container = container;
	}

	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		base.ControlStyle.CopyFrom(_container.ControlStyle);
		base.AddAttributesToRender(writer);
		writer.AddAttribute(HtmlTextWriterAttribute.Id, _container.ClientID);
	}
}
