namespace System.Web.UI.Design;

/// <summary>Provides basic functionality for describing the structure of a data source at design time.</summary>
public interface IDataSourceSchema
{
	/// <summary>Gets an array of schema descriptors for views contained in the data source.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> objects.</returns>
	IDataSourceViewSchema[] GetViews();
}
