using System.Collections.Specialized;

namespace System.Web.UI;

/// <summary>Provides a delegate with which ASP.NET extracts a set of name/value pairs from an <see cref="T:System.Web.UI.IBindableTemplate" /> object at run time. This class cannot be inherited.</summary>
/// <param name="control">The <see cref="T:System.Web.UI.Control" /> from which to extract name/value pairs, which are passed by the data-bound control to an associated data source control in two-way data-binding scenarios. This parameter corresponds to the <paramref name="control" /> parameter passed by the <see cref="M:System.Web.UI.IBindableTemplate.ExtractValues(System.Web.UI.Control)" /> method.</param>
/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs used in two-way, templated data-binding scenarios between ASP.NET data-bound controls and data source controls.</returns>
public delegate IOrderedDictionary ExtractTemplateValuesMethod(Control control);
