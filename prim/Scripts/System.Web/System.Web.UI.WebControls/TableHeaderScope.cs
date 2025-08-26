namespace System.Web.UI.WebControls;

/// <summary>Represents the HTML <see langword="scope" /> attribute for classes that represent header cells in a table. </summary>
public enum TableHeaderScope
{
	/// <summary>The <see langword="scope" /> attribute is not rendered for the header cell.</summary>
	NotSet,
	/// <summary>The object that represents a header cell of a table is rendered with the <see langword="scope" /> attribute set to "Row".</summary>
	Row,
	/// <summary>The object that represents a header cell of a table is rendered with the <see langword="scope" /> attribute set to "Column".</summary>
	Column
}
