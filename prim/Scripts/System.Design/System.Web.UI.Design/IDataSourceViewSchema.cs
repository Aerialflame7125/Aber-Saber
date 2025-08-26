namespace System.Web.UI.Design;

/// <summary>Defines a set of methods and properties used to examine a data source.</summary>
public interface IDataSourceViewSchema
{
	/// <summary>Gets the name of the view.</summary>
	/// <returns>The name of the view.</returns>
	string Name { get; }

	/// <summary>Gets an array representing the child views contained in the current view.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceViewSchema" /> objects that represent the child views contained in the current view.</returns>
	IDataSourceViewSchema[] GetChildren();

	/// <summary>Gets an array containing information about each field in the data source.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.IDataSourceFieldSchema" /> objects representing each of the fields in the data source.</returns>
	IDataSourceFieldSchema[] GetFields();
}
