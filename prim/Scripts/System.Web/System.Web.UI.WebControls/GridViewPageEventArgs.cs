using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging" /> event.</summary>
public class GridViewPageEventArgs : CancelEventArgs
{
	private int _newPageIndex;

	/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.GridViewPageEventArgs.NewPageIndex" /> property is less than zero.</exception>
	public int NewPageIndex
	{
		get
		{
			return _newPageIndex;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			_newPageIndex = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs" /> class.</summary>
	/// <param name="newPageIndex">The index of the new page to display. </param>
	public GridViewPageEventArgs(int newPageIndex)
	{
		_newPageIndex = newPageIndex;
	}
}
