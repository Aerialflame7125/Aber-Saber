namespace System.Web.UI.WebControls;

/// <summary>Specifies the layout of items in a list control.</summary>
public enum RepeatLayout
{
	/// <summary>Items are displayed in a table.</summary>
	Table,
	/// <summary>Items are displayed without a table structure. Rendered markup consists of a <see langword="span" /> element and items are separated by <see langword="br" /> elements.</summary>
	Flow,
	/// <summary>Items are displayed without a table structure. Rendered markup consists of a <see langword="ul" /> element that contains <see langword="li" /> elements.</summary>
	UnorderedList,
	/// <summary>Items are displayed without a table structure. Rendered markup consists of an <see langword="ol" /> element that contains <see langword="li" /> elements.</summary>
	OrderedList
}
