namespace System.Web.UI;

/// <summary>Serves as the property entry for read/write and read-only properties such as templates.</summary>
public class ComplexPropertyEntry : BuilderPropertyEntry
{
	/// <summary>Gets a value indicating whether the property is a collection object.</summary>
	/// <returns>
	///     <see langword="true" /> if the property entry represents an item that contains a collection of values; otherwise, <see langword="false" />.</returns>
	public bool IsCollectionItem { get; private set; }

	/// <summary>Gets or sets a value indicating whether the item represented in the property entry contains a method for setting its value.</summary>
	/// <returns>
	///     <see langword="true" /> if the item represented by the property entry does not contain a set method; otherwise, <see langword="false" />.</returns>
	public bool ReadOnly { get; set; }

	internal ComplexPropertyEntry(bool isCollectionItem, bool readOnly)
	{
		IsCollectionItem = isCollectionItem;
		ReadOnly = readOnly;
	}
}
