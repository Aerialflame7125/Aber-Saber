using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.SqlDataSource.Filtering" /> event of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control.</summary>
public class SqlDataSourceFilteringEventArgs : CancelEventArgs
{
	private IOrderedDictionary _parameterValues;

	/// <summary>Gets an <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that provides access to the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects of the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> class.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</returns>
	public IOrderedDictionary ParameterValues => _parameterValues;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceFilteringEventArgs" /> class by using the specified object.</summary>
	/// <param name="parameterValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of <see cref="T:System.Web.UI.WebControls.Parameter" /> objects.</param>
	public SqlDataSourceFilteringEventArgs(IOrderedDictionary parameterValues)
	{
		_parameterValues = parameterValues;
	}
}
