namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class RetrieveVirtualItemEventArgs : EventArgs
{
	private ListViewItem item;

	private int item_index;

	/// <summary>Gets or sets the item retrieved from the cache.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> retrieved from the cache.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem Item
	{
		get
		{
			return item;
		}
		set
		{
			item = value;
		}
	}

	/// <summary>Gets the index of the item to retrieve from the cache.</summary>
	/// <returns>The index of the item to retrieve from the cache.</returns>
	/// <filterpriority>1</filterpriority>
	public int ItemIndex => item_index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> class. </summary>
	/// <param name="itemIndex">The index of the item to retrieve.</param>
	public RetrieveVirtualItemEventArgs(int itemIndex)
	{
		item_index = itemIndex;
	}
}
