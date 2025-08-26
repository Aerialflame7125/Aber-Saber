namespace System.ComponentModel.Design;

/// <summary>Represents a smart tag panel item that is associated with a method in a class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</summary>
public class DesignerActionMethodItem : DesignerActionItem
{
	private string member_name;

	private bool designer_verb;

	private IComponent related_component;

	private DesignerActionList action_list;

	/// <summary>Gets a value that indicates the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> should appear in other user interface contexts.</summary>
	/// <returns>
	///   <see langword="true" /> if the item is to be used in shortcut menus; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool IncludeAsDesignerVerb => designer_verb;

	/// <summary>Gets the name of the method that this <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> is associated with.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the name of the associated method.</returns>
	public virtual string MemberName => member_name;

	/// <summary>Gets or sets a component that contributes its <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> objects to the current panel.</summary>
	/// <returns>The contributing component, which should have an associated designer that supplies items.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and display names.</summary>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
	/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName)
		: this(actionList, memberName, displayName, null, includeAsDesignerVerb: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and display names, and a flag that indicates whether the item should appear in other user interface contexts.</summary>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
	/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="includeAsDesignerVerb">A flag that specifies whether to also treat the associated method as a designer verb.</param>
	public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, bool includeAsDesignerVerb)
		: this(actionList, memberName, displayName, null, includeAsDesignerVerb)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method, display, and category names.</summary>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
	/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
	public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category)
		: this(actionList, memberName, displayName, category, includeAsDesignerVerb: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method, display, and category names, and a flag that indicates whether the item should appear in other user interface contexts.</summary>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
	/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
	/// <param name="includeAsDesignerVerb">A flag that specifies whether to also treat the associated method as a designer verb for the associated component.</param>
	public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, bool includeAsDesignerVerb)
		: this(actionList, memberName, displayName, category, null, includeAsDesignerVerb)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and category names, and display and description text.</summary>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
	/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
	/// <param name="description">Supplemental text for this item, used in ToolTips or the status bar.</param>
	public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, string description)
		: this(actionList, memberName, displayName, category, description, includeAsDesignerVerb: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" /> class, with the specified method and category names, display and description text, and a flag that indicates whether the item should appear in other user interface contexts.</summary>
	/// <param name="actionList">The <see cref="T:System.ComponentModel.Design.DesignerActionList" /> that contains the method this item is associated with.</param>
	/// <param name="memberName">The case-sensitive name of the method in the class derived from <see cref="T:System.ComponentModel.Design.DesignerActionList" /> to invoke through the panel item.</param>
	/// <param name="displayName">The panel text for this item.</param>
	/// <param name="category">The case-sensitive <see cref="T:System.String" /> used to group similar items on the panel.</param>
	/// <param name="description">Supplemental text for this item, used in ToolTips or the status bar.</param>
	/// <param name="includeAsDesignerVerb">A flag that specifies whether to also treat the associated method as a designer verb for the associated component.</param>
	public DesignerActionMethodItem(DesignerActionList actionList, string memberName, string displayName, string category, string description, bool includeAsDesignerVerb)
		: base(displayName, category, description)
	{
		action_list = actionList;
		member_name = memberName;
		designer_verb = includeAsDesignerVerb;
	}

	/// <summary>Programmatically executes the method associated with the <see cref="T:System.ComponentModel.Design.DesignerActionMethodItem" />.</summary>
	/// <exception cref="T:System.InvalidOperationException">The method, named in <see cref="P:System.ComponentModel.Design.DesignerActionMethodItem.MemberName" /> cannot be found.</exception>
	public virtual void Invoke()
	{
		throw new NotImplementedException();
	}
}
