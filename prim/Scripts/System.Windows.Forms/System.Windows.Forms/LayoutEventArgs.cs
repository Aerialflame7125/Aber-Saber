using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.Layout" /> event. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class LayoutEventArgs : EventArgs
{
	private Control affected_control;

	private string affected_property;

	private IComponent affected_component;

	/// <summary>Gets the <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.IComponent" /> representing the <see cref="T:System.ComponentModel.Component" /> affected by the layout change.</returns>
	/// <filterpriority>1</filterpriority>
	public IComponent AffectedComponent => affected_component;

	/// <summary>Gets the child control affected by the change.</summary>
	/// <returns>The child <see cref="T:System.Windows.Forms.Control" /> affected by the change.</returns>
	/// <filterpriority>1</filterpriority>
	public Control AffectedControl => affected_control;

	/// <summary>Gets the property affected by the change.</summary>
	/// <returns>The property affected by the change.</returns>
	/// <filterpriority>1</filterpriority>
	public string AffectedProperty => affected_property;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified control and property affected.</summary>
	/// <param name="affectedControl">The <see cref="T:System.Windows.Forms.Control" /> affected by the layout change.</param>
	/// <param name="affectedProperty">The property affected by the layout change.</param>
	public LayoutEventArgs(Control affectedControl, string affectedProperty)
	{
		affected_control = affectedControl;
		affected_property = affectedProperty;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutEventArgs" /> class with the specified component and property affected.</summary>
	/// <param name="affectedComponent">The <see cref="T:System.ComponentModel.Component" /> affected by the layout change. </param>
	/// <param name="affectedProperty">The property affected by the layout change. </param>
	public LayoutEventArgs(IComponent affectedComponent, string affectedProperty)
	{
		affected_component = affectedComponent;
		affected_property = affectedProperty;
	}
}
