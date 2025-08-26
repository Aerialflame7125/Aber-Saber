namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event. </summary>
public class ListViewItemSelectionChangedEventArgs : EventArgs
{
	private bool is_selected;

	private ListViewItem item;

	private int item_index;

	/// <summary>Gets the item whose selection state has changed.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</returns>
	public ListViewItem Item => item;

	/// <summary>Gets a value indicating whether the item's state has changed to selected. </summary>
	/// <returns>true if the item's state has changed to selected; false if the item's state has changed to deselected.</returns>
	public bool IsSelected => is_selected;

	/// <summary>Gets the index of the item whose selection state has changed.</summary>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</returns>
	public int ItemIndex => item_index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> class. </summary>
	/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
	/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
	/// <param name="isSelected">true to indicate the item's state has changed to selected; false to indicate the item's state has changed to deselected.</param>
	public ListViewItemSelectionChangedEventArgs(ListViewItem item, int itemIndex, bool isSelected)
	{
		this.item = item;
		item_index = itemIndex;
		is_selected = isSelected;
	}
}
