namespace System.Windows.Forms;

/// <summary>Specifies the valid grid item types for a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
/// <filterpriority>2</filterpriority>
public enum GridItemType
{
	/// <summary>A grid entry that corresponds to a property.</summary>
	Property,
	/// <summary>A grid entry that is a category name. A category is a descriptive grouping for groups of <see cref="T:System.Windows.Forms.GridItem" /> rows. Typical categories include the following Behavior, Layout, Data, and Appearance.</summary>
	Category,
	/// <summary>The <see cref="T:System.Windows.Forms.GridItem" /> is an element of an array.</summary>
	ArrayValue,
	/// <summary>A root item in the grid hierarchy.</summary>
	Root
}
