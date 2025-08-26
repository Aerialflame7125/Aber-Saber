using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class SearchForVirtualItemEventArgs : EventArgs
{
	private SearchDirectionHint direction;

	private bool include_sub_items_in_search;

	private int index;

	private bool is_prefix_search;

	private bool is_text_search;

	private int start_index;

	private Point starting_point;

	private string text;

	/// <summary>Gets the direction from the current item that the search should take place.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public SearchDirectionHint Direction => direction;

	/// <summary>Gets a value indicating whether the search should include subitems of list items.</summary>
	/// <returns>true if subitems should be included in the search; otherwise, false. </returns>
	/// <filterpriority>1</filterpriority>
	public bool IncludeSubItemsInSearch => include_sub_items_in_search;

	/// <summary>Gets or sets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" /> .</summary>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
	public int Index
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
		}
	}

	/// <summary>Gets a value indicating whether the search should return an item if its text starts with the search text.</summary>
	/// <returns>true if the search should match item text that starts with the search text; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsPrefixSearch => is_prefix_search;

	/// <summary>Gets a value indicating whether the search is a text search.</summary>
	/// <returns>true if the search is a text search; false if the search is a location search.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsTextSearch => is_text_search;

	/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> where the search starts.</summary>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> indicating where the search starts</returns>
	/// <filterpriority>1</filterpriority>
	public int StartIndex => start_index;

	/// <summary>Gets the starting location of the search.</summary>
	/// <returns>The <see cref="T:System.Drawing.Point" /> that indicates the starting location of the search.</returns>
	/// <filterpriority>1</filterpriority>
	public Point StartingPoint => starting_point;

	/// <summary>Gets the text used to find an item in the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <returns>The text used to find an item in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	public string Text => text;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> class. </summary>
	/// <param name="isTextSearch">A value indicating whether the search is a text search.</param>
	/// <param name="isPrefixSearch">A value indicating whether the search is a prefix search.</param>
	/// <param name="includeSubItemsInSearch">A value indicating whether to include subitems of list items in the search.</param>
	/// <param name="text">The text of the item to search for.</param>
	/// <param name="startingPoint">The <see cref="T:System.Drawing.Point" /> at which to start the search.</param>
	/// <param name="direction">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
	/// <param name="startIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> at which to start the search.</param>
	public SearchForVirtualItemEventArgs(bool isTextSearch, bool isPrefixSearch, bool includeSubItemsInSearch, string text, Point startingPoint, SearchDirectionHint direction, int startIndex)
	{
		is_text_search = isTextSearch;
		is_prefix_search = isPrefixSearch;
		include_sub_items_in_search = includeSubItemsInSearch;
		this.text = text;
		starting_point = startingPoint;
		this.direction = direction;
		start_index = startIndex;
		index = -1;
	}
}
