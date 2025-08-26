using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowEditing" /> event.</summary>
public class GridViewEditEventArgs : CancelEventArgs
{
	private int _newEditIndex;

	/// <summary>Gets or sets the index of the row being edited.</summary>
	/// <returns>The index of the row being edited.</returns>
	public int NewEditIndex
	{
		get
		{
			return _newEditIndex;
		}
		set
		{
			_newEditIndex = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewEditEventArgs" /> class.</summary>
	/// <param name="newEditIndex">The index of the row to edit. </param>
	public GridViewEditEventArgs(int newEditIndex)
	{
		_newEditIndex = newEditIndex;
	}
}
