using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanging" /> event.</summary>
public class FormViewPageEventArgs : CancelEventArgs
{
	private int _newPageIndex;

	/// <summary>Gets or sets the index of the new page to display in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The index of the new page to display in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewPageEventArgs" /> class.</summary>
	/// <param name="newPageIndex">The index of the new page to display.</param>
	public FormViewPageEventArgs(int newPageIndex)
	{
		_newPageIndex = newPageIndex;
	}
}
