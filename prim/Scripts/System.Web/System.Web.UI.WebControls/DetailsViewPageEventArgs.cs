using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.PageIndexChanging" /> event.</summary>
public class DetailsViewPageEventArgs : CancelEventArgs
{
	private int _newPageIndex;

	/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
	public int NewPageIndex
	{
		get
		{
			return _newPageIndex;
		}
		set
		{
			_newPageIndex = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewPageEventArgs" /> class.</summary>
	/// <param name="newPageIndex">The index of the new page to display.</param>
	public DetailsViewPageEventArgs(int newPageIndex)
	{
		_newPageIndex = newPageIndex;
	}
}
