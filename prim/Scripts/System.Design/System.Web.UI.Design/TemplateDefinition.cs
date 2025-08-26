using System.Web.UI.WebControls;

namespace System.Web.UI.Design;

/// <summary>Provides properties and methods that define a template element in a Web server control at design time.</summary>
public class TemplateDefinition : DesignerObject
{
	/// <summary>Gets a value that indicates whether the template should enable editing of its contents.</summary>
	/// <returns>
	///   <see langword="true" /> if editing is allowed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[System.MonoNotSupported("")]
	public virtual bool AllowEditing
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the HTML markup representing the content of the template.</summary>
	/// <returns>HTML markup for the content of the template.</returns>
	[System.MonoNotSupported("")]
	public virtual string Content
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Retrieves a value indicating whether the template should limit content to Web server controls, as set in the <see cref="Overload:System.Web.UI.Design.TemplateDefinition.#ctor" /> constructor. This property is read-only.</summary>
	/// <returns>
	///   <see langword="true" /> if content is limited to Web server controls; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool ServerControlsOnly
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Retrieves the style that should be applied to the template as set in the <see cref="Overload:System.Web.UI.Design.TemplateDefinition.#ctor" /> constructor. This property is read-only.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object.</returns>
	[System.MonoNotSupported("")]
	public Style Style
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Retrieves or sets a value indicating whether the template supports data binding.</summary>
	/// <returns>
	///   <see langword="true" /> if the template supports data binding; otherwise, <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool SupportsDataBinding
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoNotSupported("")]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Retrieves the component in which the template resides. This property is read-only.</summary>
	/// <returns>The component as set when this <see cref="T:System.Web.UI.Design.TemplateDefinition" /> was created.</returns>
	[System.MonoNotSupported("")]
	public object TemplatedObject
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Retrieves the property name for the template that the design host should display in the property grid.</summary>
	/// <returns>The name of the template as it should appear in the Properties list of the design host.</returns>
	[System.MonoNotSupported("")]
	public string TemplatePropertyName
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, and property name.</summary>
	/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
	/// <param name="name">The name of the template.</param>
	/// <param name="templatedObject">The object that contains the template.</param>
	/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />,  
	/// -or-  
	/// <paramref name="templatedObject" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName)
		: base(designer, name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, property name, and whether to limit the template contents to Web server controls.</summary>
	/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
	/// <param name="name">The name of the template.</param>
	/// <param name="templatedObject">The object that contains the template.</param>
	/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
	/// <param name="serverControlsOnly">A Boolean value indicating whether the template content should allow only Web server controls.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />,  
	/// -or-  
	/// <paramref name="templatedObject" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, bool serverControlsOnly)
		: base(designer, name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, property name, and <see cref="T:System.Web.UI.WebControls.Style" /> object.</summary>
	/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
	/// <param name="name">The name of the template.</param>
	/// <param name="templatedObject">The object that contains the template.</param>
	/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
	/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> object to apply to each template.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />,  
	/// -or-  
	/// <paramref name="templatedObject" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, Style style)
		: base(designer, name)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateDefinition" /> class, using the provided designer, template name, template, property name, <see cref="T:System.Web.UI.WebControls.Style" /> object, and whether to limit content to Web server controls.</summary>
	/// <param name="designer">The parent <see cref="T:System.Web.UI.Design.ControlDesigner" /> object.</param>
	/// <param name="name">The name of the template.</param>
	/// <param name="templatedObject">The object that contains the template.</param>
	/// <param name="templatePropertyName">The property name that represents this template in the Properties list in the design host.</param>
	/// <param name="style">A <see cref="T:System.Web.UI.WebControls.Style" /> object to apply to each template.</param>
	/// <param name="serverControlsOnly">A Boolean value indicating whether the template should limit content to Web server controls.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designer" /> is <see langword="null" />,  
	/// -or-  
	/// <paramref name="templatedObject" /> is <see langword="null" />.</exception>
	[System.MonoNotSupported("")]
	public TemplateDefinition(ControlDesigner designer, string name, object templatedObject, string templatePropertyName, Style style, bool serverControlsOnly)
		: base(designer, name)
	{
		throw new NotImplementedException();
	}
}
