using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Enables a non-control component to emulate the data-binding behavior of a Windows Forms control.</summary>
/// <filterpriority>2</filterpriority>
public interface IBindableComponent : IDisposable, IComponent
{
	/// <summary>Gets or sets the collection of currency managers for the <see cref="T:System.Windows.Forms.IBindableComponent" />. </summary>
	/// <returns>The collection of <see cref="T:System.Windows.Forms.BindingManagerBase" /> objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</returns>
	/// <filterpriority>1</filterpriority>
	BindingContext BindingContext { get; set; }

	/// <summary>Gets the collection of data-binding objects for this <see cref="T:System.Windows.Forms.IBindableComponent" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ControlBindingsCollection" /> for this <see cref="T:System.Windows.Forms.IBindableComponent" />. </returns>
	/// <filterpriority>1</filterpriority>
	ControlBindingsCollection DataBindings { get; }
}
