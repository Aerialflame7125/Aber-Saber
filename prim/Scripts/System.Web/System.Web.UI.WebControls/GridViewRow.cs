using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents an individual row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class GridViewRow : TableRow, IDataItemContainer, INamingContainer
{
	private object dataItem;

	private int rowIndex;

	private int dataItemIndex;

	private DataControlRowState rowState;

	private DataControlRowType rowType;

	/// <summary>Gets the underlying data object to which the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object is bound.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the underlying data object to which the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object is bound. </returns>
	public virtual object DataItem
	{
		get
		{
			return dataItem;
		}
		set
		{
			dataItem = value;
		}
	}

	/// <summary>Gets the index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying <see cref="T:System.Data.DataSet" />.</summary>
	/// <returns>The index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying data source.</returns>
	public virtual int DataItemIndex => dataItemIndex;

	/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
	public virtual int RowIndex => rowIndex;

	/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</returns>
	public virtual DataControlRowState RowState
	{
		get
		{
			return rowState;
		}
		set
		{
			rowState = value;
		}
	}

	/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
	public virtual DataControlRowType RowType
	{
		get
		{
			return rowType;
		}
		set
		{
			rowType = value;
		}
	}

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
	/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" />.</returns>
	object IDataItemContainer.DataItem => DataItem;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
	/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItemIndex" />.</returns>
	int IDataItemContainer.DataItemIndex => DataItemIndex;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
	/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.RowIndex" />.</returns>
	int IDataItemContainer.DisplayIndex => RowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> class.</summary>
	/// <param name="rowIndex">The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</param>
	/// <param name="dataItemIndex">The index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying <see cref="T:System.Data.DataSet" />.</param>
	/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
	/// <param name="rowState">A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
	public GridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
	{
		this.rowIndex = rowIndex;
		this.dataItemIndex = dataItemIndex;
		this.rowType = rowType;
		this.rowState = rowState;
	}

	/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
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
			GridViewCommandEventArgs args = new GridViewCommandEventArgs(this, source, (CommandEventArgs)e);
			RaiseBubbleEvent(source, args);
			return true;
		}
		return false;
	}
}
