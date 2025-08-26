namespace System.Windows.Forms;

/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.TreeView" /> control or a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
/// <filterpriority>2</filterpriority>
public class TreeViewHitTestInfo
{
	private TreeNode node;

	private TreeViewHitTestLocations location;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.TreeNode" /> at the position indicated by a hit test of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the position indicated by a hit test of a <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeNode Node => node;

	/// <summary>Gets the location of a hit test on a <see cref="T:System.Windows.Forms.TreeView" /> control, in relation to the <see cref="T:System.Windows.Forms.TreeView" /> and the nodes it contains.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewHitTestLocations" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeViewHitTestLocations Location => location;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewHitTestInfo" /> class. </summary>
	/// <param name="hitNode">The tree node located at the position indicated by the hit test.</param>
	/// <param name="hitLocation">One of the <see cref="T:System.Windows.Forms.TreeViewHitTestLocations" /> values.</param>
	public TreeViewHitTestInfo(TreeNode hitNode, TreeViewHitTestLocations hitLocation)
	{
		node = hitNode;
		location = hitLocation;
	}
}
