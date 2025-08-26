namespace System.ComponentModel.Design;

/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignerActionService.DesignerActionListsChanged" /> event.</summary>
public class DesignerActionListsChangedEventArgs : EventArgs
{
	private object related_object;

	private DesignerActionListsChangedType change_type;

	private DesignerActionListCollection action_lists;

	/// <summary>Gets the collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects associated with this event.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that represents the current state of the collection.</returns>
	public DesignerActionListCollection ActionLists => action_lists;

	/// <summary>Gets a flag indicating whether an element has been added or removed from the collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListsChangedType" /> that indicates the type of change.</returns>
	public DesignerActionListsChangedType ChangeType => change_type;

	/// <summary>Gets the object that is associated with the collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
	/// <returns>The <see cref="T:System.Object" /> associated with the managed <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" />.</returns>
	public object RelatedObject => related_object;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionListsChangedEventArgs" /> class.</summary>
	/// <param name="relatedObject">The object that is associated with the collection.</param>
	/// <param name="changeType">A value that specifies whether a <see cref="T:System.ComponentModel.Design.DesignerActionList" /> has been added or removed from the collection.</param>
	/// <param name="actionLists">The collection of list elements after the action has been applied.</param>
	public DesignerActionListsChangedEventArgs(object relatedObject, DesignerActionListsChangedType changeType, DesignerActionListCollection actionLists)
	{
		related_object = relatedObject;
		change_type = changeType;
		action_lists = actionLists;
	}
}
