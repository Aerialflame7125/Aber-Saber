using System.Collections.Specialized;

namespace System.Web.UI;

/// <summary>Provides a way for ASP.NET data-bound controls, such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> and <see cref="T:System.Web.UI.WebControls.FormView" />, to automatically bind to an ASP.NET data source control within templated content sections. </summary>
public interface IBindableTemplate : ITemplate
{
	/// <summary>When implemented by a class, retrieves a set of name/value pairs for values bound using two-way ASP.NET data-binding syntax within the templated content.</summary>
	/// <param name="container">The <see cref="T:System.Web.UI.Control" /> from which to extract name/value pairs, which are passed by the data-bound control to an associated data source control in two-way data-binding scenarios.</param>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs. The name represents the name of a control within templated content, and the value is the current value of a property value bound using two-way ASP.NET data-binding syntax.</returns>
	IOrderedDictionary ExtractValues(Control container);
}
