namespace System.Web.UI.WebControls;

/// <summary>Specifies the type of an item in a list control.</summary>
public enum ListItemType
{
	/// <summary>A header for the list control. It is not data-bound.</summary>
	Header,
	/// <summary>A footer for the list control. It is not data-bound.</summary>
	Footer,
	/// <summary>An item in the list control. It is data-bound.</summary>
	Item,
	/// <summary>An item in alternating (zero-based even-indexed) cells. It is data-bound.</summary>
	AlternatingItem,
	/// <summary>A selected item in the list control. It is data-bound.</summary>
	SelectedItem,
	/// <summary>An item in a list control currently in edit mode. It is data-bound.</summary>
	EditItem,
	/// <summary>A separator between items in a list control. It is not data-bound.</summary>
	Separator,
	/// <summary>A pager that displays the controls to navigate to different pages associated with the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. It is not data-bound.</summary>
	Pager
}
