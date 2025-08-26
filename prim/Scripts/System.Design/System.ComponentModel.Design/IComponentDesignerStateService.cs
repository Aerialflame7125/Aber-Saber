namespace System.ComponentModel.Design;

/// <summary>Allows a designer to store and retrieve its state.</summary>
public interface IComponentDesignerStateService
{
	/// <summary>Gets a state item specified by the key for the given component.</summary>
	/// <param name="component">The component for which to retrieve the designer state item.</param>
	/// <param name="key">The name of the designer state item.</param>
	/// <returns>The designer state for <paramref name="component" /> specified by <paramref name="key" />.</returns>
	object GetState(IComponent component, string key);

	/// <summary>Sets a state item specified by the key for the given component.</summary>
	/// <param name="component">The component for which to set the designer state item.</param>
	/// <param name="key">The name of the designer state item.</param>
	/// <param name="value">The designer state item for <paramref name="component" />.</param>
	void SetState(IComponent component, string key, object value);
}
