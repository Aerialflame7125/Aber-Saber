using System.Collections.Specialized;

namespace System.Web.UI;

/// <summary>Provides the default implementation of an <see cref="T:System.Web.UI.IBindableTemplate" /> object, which ASP.NET uses whenever it parses two-way data-binding within the templated content of an ASP.NET control such as <see cref="T:System.Web.UI.WebControls.FormView" />. This class cannot be inherited.</summary>
public sealed class CompiledBindableTemplateBuilder : IBindableTemplate, ITemplate
{
	private BuildTemplateMethod templateMethod;

	private ExtractTemplateValuesMethod extractMethod;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.CompiledBindableTemplateBuilder" /> class.</summary>
	/// <param name="buildTemplateMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledBindableTemplateBuilder.InstantiateIn(System.Web.UI.Control)" /> method call.</param>
	/// <param name="extractTemplateValuesMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledBindableTemplateBuilder.ExtractValues(System.Web.UI.Control)" /> method call.</param>
	public CompiledBindableTemplateBuilder(BuildTemplateMethod buildTemplateMethod, ExtractTemplateValuesMethod extractTemplateValuesMethod)
	{
		templateMethod = buildTemplateMethod;
		extractMethod = extractTemplateValuesMethod;
	}

	/// <summary>Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to. These child controls are in turn defined within an inline template.</summary>
	/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to contain the controls that are created from the inline template. </param>
	public void InstantiateIn(Control container)
	{
		templateMethod(container);
	}

	/// <summary>Retrieves a set of name/value pairs for values bound using two-way ASP.NET data-binding syntax within the templated content.</summary>
	/// <param name="container">The <see cref="T:System.Web.UI.Control" /> from which to extract name/value pairs, which are passed by the data-bound control to an associated data source control in two-way data-binding scenarios.</param>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs. The name represents the name of the data item field specified as the first parameter to bind within templated content. The value is the current value of a property value bound using two-way ASP.NET data-binding syntax.</returns>
	public IOrderedDictionary ExtractValues(Control container)
	{
		if (extractMethod == null)
		{
			return null;
		}
		return extractMethod(container);
	}
}
