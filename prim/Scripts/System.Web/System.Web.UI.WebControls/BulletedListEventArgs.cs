namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.BulletedList.Click" /> event of a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
public class BulletedListEventArgs : EventArgs
{
	private int _index;

	/// <summary>Gets the index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control that raised the event.</summary>
	/// <returns>The index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control that raised the event.</returns>
	public int Index => _index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BulletedListEventArgs" /> class.</summary>
	/// <param name="index">The zero-based index of the list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> that raised the event. </param>
	public BulletedListEventArgs(int index)
	{
		_index = index;
	}
}
