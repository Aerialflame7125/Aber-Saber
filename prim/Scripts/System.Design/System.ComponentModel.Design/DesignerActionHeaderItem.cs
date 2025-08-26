namespace System.ComponentModel.Design;

/// <summary>Represents a static header item on a smart tag panel. This class cannot be inherited.</summary>
public sealed class DesignerActionHeaderItem : DesignerActionTextItem
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionHeaderItem" /> class using the provided name string.</summary>
	/// <param name="displayName">The text to be displayed in the header.</param>
	public DesignerActionHeaderItem(string displayName)
		: base(displayName, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionHeaderItem" /> class using the provided name and category strings.</summary>
	/// <param name="displayName">The text to be displayed in the header.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> that defines the groupings of panel entries.</param>
	public DesignerActionHeaderItem(string displayName, string category)
		: base(displayName, category)
	{
	}
}
