namespace System.Windows.Forms;

/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.ListView" /> control or a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
/// <filterpriority>2</filterpriority>
public class ListViewHitTestInfo
{
	private ListViewItem item;

	private ListViewItem.ListViewSubItem subItem;

	private ListViewHitTestLocations location = ListViewHitTestLocations.None;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem Item => item;

	/// <summary>Gets the location of a hit test on a <see cref="T:System.Windows.Forms.ListView" /> control, in relation to the <see cref="T:System.Windows.Forms.ListView" /> and the items it contains.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ListViewHitTestLocations" /> values. </returns>
	/// <filterpriority>1</filterpriority>
	public ListViewHitTestLocations Location => location;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</returns>
	/// <filterpriority>1</filterpriority>
	public ListViewItem.ListViewSubItem SubItem => subItem;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewHitTestInfo" /> class. </summary>
	/// <param name="hitItem">The <see cref="T:System.Windows.Forms.ListViewItem" /> located at the position indicated by the hit test.</param>
	/// <param name="hitSubItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> located at the position indicated by the hit test.</param>
	/// <param name="hitLocation">One of the <see cref="T:System.Windows.Forms.ListViewHitTestLocations" /> values.</param>
	public ListViewHitTestInfo(ListViewItem hitItem, ListViewItem.ListViewSubItem hitSubItem, ListViewHitTestLocations hitLocation)
	{
		item = hitItem;
		subItem = hitSubItem;
		location = hitLocation;
	}
}
