using System.Collections;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Design.Behavior.BehaviorService.BeginDrag" /> and <see cref="E:System.Windows.Forms.Design.Behavior.BehaviorService.EndDrag" /> events.</summary>
public class BehaviorDragDropEventArgs : EventArgs
{
	private ICollection components;

	/// <summary>Gets the list of <see cref="T:System.ComponentModel.IComponent" /> objects currently being dragged.</summary>
	/// <returns>The <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.IComponent" /> objects currently being dragged.</returns>
	public ICollection DragComponents => components;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorDragDropEventArgs" /> class.</summary>
	/// <param name="dragComponents">The <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.IComponent" /> objects currently being dragged.</param>
	public BehaviorDragDropEventArgs(ICollection dragComponents)
	{
		components = dragComponents;
	}
}
