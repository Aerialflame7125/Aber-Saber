using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Declares the base type of the container control of a property that returns an <see cref="T:System.Web.UI.ITemplate" /> interface and is marked with the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> attribute. The control with the <see cref="T:System.Web.UI.ITemplate" /> property must implement the <see cref="T:System.Web.UI.INamingContainer" /> interface. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Property)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class TemplateContainerAttribute : Attribute
{
	private Type containerType;

	private BindingDirection direction;

	/// <summary>Gets the binding direction of the container control.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.BindingDirection" /> indicating the container control's binding direction. The default is <see cref="F:System.ComponentModel.BindingDirection.OneWay" />.</returns>
	public BindingDirection BindingDirection => direction;

	/// <summary>Gets the container control type.</summary>
	/// <returns>The container control <see cref="T:System.Type" />.</returns>
	public Type ContainerType => containerType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> class using the specified container type and the <see cref="P:System.Web.UI.TemplateContainerAttribute.BindingDirection" /> property.</summary>
	/// <param name="containerType">The <see cref="T:System.Type" /> for the container control.</param>
	/// <param name="bindingDirection">The <see cref="P:System.Web.UI.TemplateContainerAttribute.BindingDirection" /> for the container control.</param>
	public TemplateContainerAttribute(Type containerType, BindingDirection bindingDirection)
	{
		this.containerType = containerType;
		direction = bindingDirection;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> class using the specified container type.</summary>
	/// <param name="containerType">The <see cref="T:System.Type" /> for the container control. </param>
	public TemplateContainerAttribute(Type containerType)
	{
		this.containerType = containerType;
	}
}
