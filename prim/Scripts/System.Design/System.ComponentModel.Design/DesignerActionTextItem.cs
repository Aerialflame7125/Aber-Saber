namespace System.ComponentModel.Design;

/// <summary>Represents a static text item on a smart tag panel.</summary>
public class DesignerActionTextItem : DesignerActionItem
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionTextItem" /> class.</summary>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The category used to group similar items on the panel.</param>
	public DesignerActionTextItem(string displayName, string category)
		: base(displayName, category, string.Empty)
	{
	}
}
