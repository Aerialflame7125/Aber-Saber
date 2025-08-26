namespace System.Web.UI;

/// <summary>Allows a type converter to access schema information stored on an object.</summary>
public interface IDataSourceViewSchemaAccessor
{
	/// <summary>When implemented, gets or sets the schema associated with the object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the schema.</returns>
	object DataSourceViewSchema { get; set; }
}
