namespace System.Web.UI.WebControls;

/// <summary>Defines the properties and methods that must be implemented by any list control that repeats a list of items.</summary>
public interface IRepeatInfoUser
{
	/// <summary>Gets a value indicating whether the list control contains a heading section.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a heading section; otherwise, <see langword="false" />.</returns>
	bool HasHeader { get; }

	/// <summary>Gets a value indicating whether the list control contains a footer section.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a footer section; otherwise, <see langword="false" />.</returns>
	bool HasFooter { get; }

	/// <summary>Gets a value indicating whether the list control contains a separator between items in the list.</summary>
	/// <returns>
	///     <see langword="true" /> if the list control contains a separator; otherwise, <see langword="false" />.</returns>
	bool HasSeparators { get; }

	/// <summary>Gets the number of items in the list control.</summary>
	/// <returns>The number of items in the list control.</returns>
	int RepeatedItemCount { get; }

	/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
	Style GetItemStyle(ListItemType itemType, int repeatIndex);

	/// <summary>Renders an item in the list with the specified information.</summary>
	/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
	/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
	/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> that represents the information used to render the item in the list. </param>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer);
}
