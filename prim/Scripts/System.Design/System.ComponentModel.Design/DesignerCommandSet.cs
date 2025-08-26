using System.Collections;

namespace System.ComponentModel.Design;

/// <summary>Represents a base class for design-time tools, not derived from <see cref="T:System.ComponentModel.Design.ComponentDesigner" />, that provide smart tag or designer verb capabilities.</summary>
public class DesignerCommandSet
{
	private DesignerActionListCollection action_lists = new DesignerActionListCollection();

	private DesignerVerbCollection verbs = new DesignerVerbCollection();

	/// <summary>Gets the collection of all the smart tags associated with the designed component.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> that contains the smart tags for the associated designed component.</returns>
	public DesignerActionListCollection ActionLists => action_lists;

	/// <summary>Gets the collection of all the designer verbs associated with the designed component.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> that contains the designer verbs for the associated designed component.</returns>
	public DesignerVerbCollection Verbs => verbs;

	/// <summary>Initializes an instance of the <see cref="T:System.ComponentModel.Design.DesignerCommandSet" /> class.</summary>
	public DesignerCommandSet()
	{
	}

	/// <summary>Returns a collection of command objects.</summary>
	/// <param name="name">The type of collection to return, indicating either a <see cref="T:System.ComponentModel.Design.DesignerActionListCollection" /> or a <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" />.</param>
	/// <returns>A collection that contains the specified type - either <see cref="T:System.ComponentModel.Design.DesignerActionList" /> or <see cref="T:System.ComponentModel.Design.DesignerVerb" /> - of command objects. The base implementation always returns <see langword="null" />.</returns>
	[System.MonoTODO]
	public virtual ICollection GetCommands(string name)
	{
		throw new NotImplementedException();
	}
}
