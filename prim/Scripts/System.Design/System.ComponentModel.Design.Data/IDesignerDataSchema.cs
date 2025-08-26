using System.Collections;

namespace System.ComponentModel.Design.Data;

/// <summary>Defines methods for retrieving data-store schema information.</summary>
public interface IDesignerDataSchema
{
	/// <summary>Gets a collection of specified schema items.</summary>
	/// <param name="schemaClass">The schema objects to return.</param>
	/// <returns>A collection of schema objects of the specified type.</returns>
	ICollection GetSchemaItems(DesignerDataSchemaClass schemaClass);

	/// <summary>Returns a value indicating whether the data store contains the specified data-schema object.</summary>
	/// <param name="schemaClass">The schema objects to return.</param>
	/// <returns>
	///   <see langword="true" /> if the data store supports the specified data-schema object; otherwise, <see langword="false" />.</returns>
	bool SupportsSchemaClass(DesignerDataSchemaClass schemaClass);
}
