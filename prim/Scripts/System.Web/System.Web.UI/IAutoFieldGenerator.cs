using System.Collections;

namespace System.Web.UI;

/// <summary>Defines a method that automatically generates fields for data-bound controls that use ASP.NET Dynamic Data features.</summary>
public interface IAutoFieldGenerator
{
	/// <summary>Automatically generates <see cref="T:System.Web.DynamicData.DynamicField" /> objects based on metadata information for the table.</summary>
	/// <param name="control">The data-bound control that will contain the <see cref="T:System.Web.DynamicData.DynamicField" /> objects.</param>
	/// <returns>A collection of <see cref="T:System.Web.DynamicData.DynamicField" /> objects.</returns>
	ICollection GenerateFields(Control control);
}
