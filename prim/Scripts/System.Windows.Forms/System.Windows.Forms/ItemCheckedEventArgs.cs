namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event of the <see cref="T:System.Windows.Forms.ListView" /> control. </summary>
/// <filterpriority>2</filterpriority>
public class ItemCheckedEventArgs : EventArgs
{
	private ListViewItem item;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> whose checked state is changing.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose checked state is changing.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem Item => item;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> class. </summary>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that is being checked or unchecked.</param>
	public ItemCheckedEventArgs(ListViewItem item)
	{
		this.item = item;
	}
}
