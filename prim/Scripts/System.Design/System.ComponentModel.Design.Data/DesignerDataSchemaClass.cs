namespace System.ComponentModel.Design.Data;

/// <summary>Specifies the types of objects that can be retrieved from a data-store schema. This class cannot be inherited.</summary>
public sealed class DesignerDataSchemaClass
{
	/// <summary>Indicates that stored procedures should be returned from the data-store schema.</summary>
	public static readonly DesignerDataSchemaClass StoredProcedures = new DesignerDataSchemaClass();

	/// <summary>Indicates that tables should be returned from the data-store schema.</summary>
	public static readonly DesignerDataSchemaClass Tables = new DesignerDataSchemaClass();

	/// <summary>Indicates that data views should be returned from the data-store schema.</summary>
	public static readonly DesignerDataSchemaClass Views = new DesignerDataSchemaClass();

	private DesignerDataSchemaClass()
	{
	}
}
