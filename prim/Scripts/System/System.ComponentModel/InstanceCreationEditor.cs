using System.Security.Permissions;

namespace System.ComponentModel;

/// <summary>Creates an instance of a particular type of property from a drop-down box within the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
public abstract class InstanceCreationEditor
{
	/// <summary>Gets the specified text.</summary>
	/// <returns>The specified text.</returns>
	public virtual string Text => global::SR.GetString("(New...)");

	/// <summary>When overridden in a derived class, returns an instance of the specified type.</summary>
	/// <param name="context">The context information.</param>
	/// <param name="instanceType">The specified type.</param>
	/// <returns>An instance of the specified type or <see langword="null" />.</returns>
	public abstract object CreateInstance(ITypeDescriptorContext context, Type instanceType);

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstanceCreationEditor" /> class.</summary>
	protected InstanceCreationEditor()
	{
	}
}
