using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a row within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DetailsViewRow : TableRow
{
	private int rowIndex;

	private DataControlRowState rowState;

	private DataControlRowType rowType;

	private DataControlField containingField;

	/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
	public virtual int RowIndex => rowIndex;

	/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</returns>
	public virtual DataControlRowState RowState => rowState;

	/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
	public virtual DataControlRowType RowType => rowType;

	internal DataControlField ContainingField
	{
		get
		{
			return containingField;
		}
		set
		{
			containingField = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> class.</summary>
	/// <param name="rowIndex">The index of the row in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
	/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
	public DetailsViewRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
	{
		this.rowIndex = rowIndex;
		this.rowType = rowType;
		this.rowState = rowState;
	}

	/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
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
			DetailsViewCommandEventArgs args = new DetailsViewCommandEventArgs(source, (CommandEventArgs)e);
			RaiseBubbleEvent(source, args);
			return true;
		}
		return false;
	}
}
