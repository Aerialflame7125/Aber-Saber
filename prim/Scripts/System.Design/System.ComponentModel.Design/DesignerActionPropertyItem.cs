namespace System.ComponentModel.Design;

/// <summary>Represents a panel item that is associated with a property in a class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" />. This class cannot be inherited.</summary>
public sealed class DesignerActionPropertyItem : DesignerActionItem
{
	private string member_name;

	private IComponent related_component;

	/// <summary>Gets the name of the property that this item is associated with.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the name of the associated property.</returns>
	public string MemberName => member_name;

	/// <summary>Gets or sets a component that contributes its items to the current panel.</summary>
	/// <returns>The contributing component, which should have an associated designer that supplies <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects.</returns>
	public IComponent RelatedComponent
	{
		get
		{
			return related_component;
		}
		set
		{
			related_component = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionPropertyItem" /> class, with the specified property and display names.</summary>
	/// <param name="memberName">The case-sensitive name of the property associated with this panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	public DesignerActionPropertyItem(string memberName, string displayName)
		: this(memberName, displayName, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionPropertyItem" /> class, with the specified property and category names, and display text.</summary>
	/// <param name="memberName">The case-sensitive name of the property associated with this panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
	public DesignerActionPropertyItem(string memberName, string displayName, string category)
		: this(memberName, displayName, category, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionPropertyItem" /> class, with the specified property and category names, and display and description text.</summary>
	/// <param name="memberName">The case-sensitive name of the property associated with this panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
	/// <param name="description">Supplemental text for this item, used in ToolTips or the status bar.</param>
	public DesignerActionPropertyItem(string memberName, string displayName, string category, string description)
		: base(displayName, category, description)
	{
		member_name = memberName;
	}
}
