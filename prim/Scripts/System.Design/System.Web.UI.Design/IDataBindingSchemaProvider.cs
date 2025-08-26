namespace System.Web.UI.Design;

/// <summary>Provides an interface for design-time access to a schema provider in a design host.</summary>
public interface IDataBindingSchemaProvider
{
	/// <summary>Gets a value indicating whether the provider can refresh the schema.</summary>
	/// <returns>
	///   <see langword="true" />, if the schema can be refreshed; otherwise, <see langword="false" />.</returns>
	bool CanRefreshSchema { get; }

	/// <summary>Gets the current schema object for the designer.</summary>
	/// <returns>The current schema object for the designer.</returns>
	IDataSourceViewSchema Schema { get; }

	/// <summary>Refreshes the schema for the data source.</summary>
	/// <param name="preferSilent">
	///   <see langword="true" /> to disable data-binding events until after the schema has been refreshed; <see langword="false" /> to enable the events.</param>
	void RefreshSchema(bool preferSilent);
}
