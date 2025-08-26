using System.Runtime.InteropServices;

namespace System.ComponentModel.Design;

/// <summary>Specifies the type of change occurring in a collection of <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects.</summary>
[ComVisible(true)]
public enum DesignerActionListsChangedType
{
	/// <summary>One or more <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects have been added to the collection.</summary>
	ActionListsAdded,
	/// <summary>One or more <see cref="T:System.ComponentModel.Design.DesignerActionList" /> objects have been removed from the collection.</summary>
	ActionListsRemoved
}
