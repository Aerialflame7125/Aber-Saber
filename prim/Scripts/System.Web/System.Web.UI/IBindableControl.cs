using System.Collections.Specialized;

namespace System.Web.UI;

/// <summary>Defines a method that enables field template controls to implement two-way data-binding.</summary>
public interface IBindableControl
{
	/// <summary>Retrieves a set of name/value pairs to implement two-way data-binding in field template controls.</summary>
	/// <param name="dictionary">The dictionary that contains the name/value pairs. </param>
	void ExtractValues(IOrderedDictionary dictionary);
}
