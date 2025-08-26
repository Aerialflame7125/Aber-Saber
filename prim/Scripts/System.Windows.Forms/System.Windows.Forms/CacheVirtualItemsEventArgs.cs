namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class CacheVirtualItemsEventArgs : EventArgs
{
	private int start_index;

	private int end_index;

	/// <summary>Gets the starting index for a range of values needed by a <see cref="T:System.Windows.Forms.ListView" /> control in virtual mode.</summary>
	/// <returns>The index at the start of the range of values needed by the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	public int StartIndex => start_index;

	/// <summary>Gets the ending index for the range of values needed by a <see cref="T:System.Windows.Forms.ListView" /> control in virtual mode.</summary>
	/// <returns>The index at the end of the range of values needed by the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	public int EndIndex => end_index;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> class with the specified starting and ending indices.</summary>
	/// <param name="startIndex">The starting index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
	/// <param name="endIndex">The ending index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
	public CacheVirtualItemsEventArgs(int startIndex, int endIndex)
	{
		start_index = startIndex;
		end_index = endIndex;
	}
}
