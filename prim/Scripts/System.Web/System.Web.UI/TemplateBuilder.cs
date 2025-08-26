using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Supports the page parser in building a template and the child controls it contains.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class TemplateBuilder : ControlBuilder, ITemplate
{
	private string text;

	private TemplateContainerAttribute containerAttribute;

	private TemplateInstanceAttribute instanceAttribute;

	private List<TemplateBinding> bindings;

	/// <summary>Gets or sets the text between the opening and closing tags of the template.</summary>
	/// <returns>The text that appears between the opening and closing tags of the template.</returns>
	public virtual string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	internal Type ContainerType
	{
		get
		{
			if (containerAttribute == null)
			{
				return null;
			}
			return containerAttribute.ContainerType;
		}
	}

	internal TemplateInstance? TemplateInstance
	{
		get
		{
			if (instanceAttribute == null)
			{
				return null;
			}
			return instanceAttribute.Instances;
		}
	}

	internal BindingDirection BindingDirection
	{
		get
		{
			if (containerAttribute == null)
			{
				return BindingDirection.TwoWay;
			}
			return containerAttribute.BindingDirection;
		}
	}

	internal ICollection Bindings => bindings;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateBuilder" /> class.</summary>
	public TemplateBuilder()
	{
	}

	internal TemplateBuilder(ICustomAttributeProvider prov)
	{
		object[] customAttributes = prov.GetCustomAttributes(typeof(TemplateContainerAttribute), inherit: true);
		if (customAttributes.Length != 0)
		{
			containerAttribute = (TemplateContainerAttribute)customAttributes[0];
		}
		customAttributes = prov.GetCustomAttributes(typeof(TemplateInstanceAttribute), inherit: true);
		if (customAttributes.Length != 0)
		{
			instanceAttribute = (TemplateInstanceAttribute)customAttributes[0];
		}
	}

	internal void RegisterBoundProperty(Type controlType, string controlProperty, string controlId, string fieldName)
	{
		if (bindings == null)
		{
			bindings = new List<TemplateBinding>();
		}
		bindings.Add(new TemplateBinding(controlType, controlProperty, controlId, fieldName));
	}

	/// <summary>Used during design time to build the template and its child controls. </summary>
	/// <returns>A reference to the instance of the <see cref="T:System.Web.UI.TemplateBuilder" /> class.</returns>
	public override object BuildObject()
	{
		return base.BuildObject();
	}

	/// <summary>Initializes the template builder when a Web request is made.</summary>
	/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> responsible for parsing the control. </param>
	/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> responsible for building the control. </param>
	/// <param name="type">The <see cref="T:System.Type" /> assigned to the control that the builder will create. </param>
	/// <param name="tagName">The name of the tag to build. This allows the builder to support multiple tag types. </param>
	/// <param name="ID">The <see cref="P:System.Web.UI.ControlBuilder.ID" /> assigned to the control. </param>
	/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> that holds all the specified tag attributes. </param>
	public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
	{
		if (parser != null)
		{
			base.FileName = parser.InputFile;
		}
		base.Init(parser, parentBuilder, type, tagName, ID, attribs);
	}

	/// <summary>Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to in design time.</summary>
	/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to contain the instances of controls from the inline template.</param>
	public virtual void InstantiateIn(Control container)
	{
		CreateChildren(container);
	}

	/// <summary>Determines if the control builder needs to get its inner text.</summary>
	/// <returns>
	///     <see langword="true" /> if the control builder needs to get its inner text. The default is <see langword="false" />.</returns>
	public override bool NeedsTagInnerText()
	{
		return false;
	}

	/// <summary>Saves the inner text of the template tag.</summary>
	/// <param name="text">The inner text of the template.</param>
	public override void SetTagInnerText(string text)
	{
		this.text = text;
	}
}
