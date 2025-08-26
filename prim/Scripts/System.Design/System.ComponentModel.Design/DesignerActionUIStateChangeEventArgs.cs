namespace System.ComponentModel.Design;

/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignerActionUIService.DesignerActionUIStateChange" /> event.</summary>
public class DesignerActionUIStateChangeEventArgs : EventArgs
{
	private object related_object;

	private DesignerActionUIStateChangeType change_type;

	/// <summary>Gets a flag indicating whether the smart tag panel is being displayed or hidden.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionUIStateChangeType" /> that indicates the state of the panel.</returns>
	public DesignerActionUIStateChangeType ChangeType => change_type;

	/// <summary>Gets the object that is associated with the smart tag panel.</summary>
	/// <returns>The <see cref="T:System.Object" /> associated with the smart tag panel.</returns>
	public object RelatedObject => related_object;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionUIStateChangeEventArgs" /> class.</summary>
	/// <param name="relatedObject">The object that is associated with the panel.</param>
	/// <param name="changeType">A value that specifies whether the panel is being displayed or hidden.</param>
	public DesignerActionUIStateChangeEventArgs(object relatedObject, DesignerActionUIStateChangeType changeType)
	{
		related_object = relatedObject;
		change_type = changeType;
	}
}
