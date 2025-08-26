namespace System.ComponentModel.Design;

/// <summary>Provides the base class for types that define a list of items used to create a smart tag panel.</summary>
public class DesignerActionList
{
	private IComponent component;

	private bool auto_show;

	private DesignerActionItemCollection action_items;

	/// <summary>Gets or sets a value indicating whether the smart tag panel should automatically be displayed when it is created.</summary>
	/// <returns>
	///   <see langword="true" /> if the panel should be shown when the owning component is created; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool AutoShow
	{
		get
		{
			return auto_show;
		}
		set
		{
			auto_show = value;
		}
	}

	/// <summary>Gets the component related to <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</summary>
	/// <returns>A component related to <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</returns>
	public IComponent Component => component;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerActionList" /> class.</summary>
	/// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
	public DesignerActionList(IComponent component)
	{
		this.component = component;
		action_items = new DesignerActionItemCollection();
	}

	/// <summary>Returns an object that represents a service provided by the component associated with the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</summary>
	/// <param name="serviceType">A service provided by the <see cref="T:System.ComponentModel.Component" />.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents a service provided by the <see cref="T:System.ComponentModel.Component" />. This value is <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> does not provide the specified service.</returns>
	public object GetService(Type serviceType)
	{
		return null;
	}

	/// <summary>Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
	public virtual DesignerActionItemCollection GetSortedActionItems()
	{
		return action_items;
	}
}
