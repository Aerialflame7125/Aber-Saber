using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.Sorting" /> event.</summary>
public class GridViewSortEventArgs : CancelEventArgs
{
	private string _sortExpression;

	private SortDirection _sortDirection;

	/// <summary>Gets or sets the direction in which to sort the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SortDirection" /> values.</returns>
	public SortDirection SortDirection
	{
		get
		{
			return _sortDirection;
		}
		set
		{
			_sortDirection = value;
		}
	}

	/// <summary>Gets or sets the expression used to sort the items in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	/// <returns>The expression used to sort the items in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
	public string SortExpression
	{
		get
		{
			return _sortExpression;
		}
		set
		{
			_sortExpression = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewSortEventArgs" /> class.</summary>
	/// <param name="sortExpression">The sort expression used to sort the items in the control.</param>
	/// <param name="sortDirection">A <see cref="T:System.Web.UI.WebControls.SortDirection" /> that indicates the direction in which to sort the items in the control.</param>
	public GridViewSortEventArgs(string sortExpression, SortDirection sortDirection)
	{
		_sortExpression = sortExpression;
		_sortDirection = sortDirection;
	}
}
