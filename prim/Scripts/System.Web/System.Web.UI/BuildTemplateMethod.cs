namespace System.Web.UI;

/// <summary>Supports ASP.NET during the creation of a template for a templated control from generated class code. The <see cref="T:System.Web.UI.BuildTemplateMethod" /> delegate handles the <see cref="M:System.Web.UI.CompiledTemplateBuilder.InstantiateIn(System.Web.UI.Control)" /> method. </summary>
/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that represents the container used to store the child controls in the template. </param>
public delegate void BuildTemplateMethod(Control control);
