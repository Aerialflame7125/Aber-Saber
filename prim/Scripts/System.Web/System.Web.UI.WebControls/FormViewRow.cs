using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a row within a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class FormViewRow : TableRow
{
	private int rowIndex;

	private DataControlRowState rowState;

	private DataControlRowType rowType;

	internal bool RenderJustCellContents { get; set; }

	/// <summary>Gets the index of the data item displayed from the data source.</summary>
	/// <returns>The index of the data item displayed from the data source.</returns>
	public virtual int ItemIndex => rowIndex;

	/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</returns>
	public virtual DataControlRowState RowState => rowState;

	/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
	public virtual DataControlRowType RowType => rowType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> class.</summary>
	/// <param name="itemIndex">The index of the data item in the data source.</param>
	/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
	public FormViewRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
	{
		rowIndex = itemIndex;
		this.rowType = rowType;
		this.rowState = rowState;
	}

	/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">The event data.</param>
	/// <returns>
	///     <see langword="true" /> if the event has been canceled; otherwise, <see langword="false" />.</returns>
	protected override bool OnBubbleEvent(object source, EventArgs e)
	{
		if (base.OnBubbleEvent(source, e))
		{
			return true;
		}
		if (e is CommandEventArgs)
		{
			FormViewCommandEventArgs args = new FormViewCommandEventArgs(source, (CommandEventArgs)e);
			RaiseBubbleEvent(source, args);
			return true;
		}
		return false;
	}

	/// <summary>Renders the control to the specified HTML writer.</summary>
	/// <param name="writer">The HTML text writer object that receives the control content.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (!RenderJustCellContents)
		{
			base.Render(writer);
			return;
		}
		foreach (TableCell cell in Cells)
		{
			cell.RenderContents(writer);
		}
	}
}
