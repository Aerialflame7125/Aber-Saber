using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.SelectedIndexChanging" /> event.</summary>
public class GridViewSelectEventArgs : CancelEventArgs
{
	private int _newSelectedIndex;

	/// <summary>Gets or sets the index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	/// <returns>The index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
	public int NewSelectedIndex
	{
		get
		{
			return _newSelectedIndex;
		}
		set
		{
			_newSelectedIndex = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewSelectEventArgs" /> class.</summary>
	/// <param name="newSelectedIndex">The index of the new row to select in the <see cref="T:System.Web.UI.WebControls.GridView" /> control. </param>
	public GridViewSelectEventArgs(int newSelectedIndex)
	{
		_newSelectedIndex = newSelectedIndex;
	}
}
