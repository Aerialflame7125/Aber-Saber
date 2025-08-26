using System.Collections;

namespace System.ComponentModel.Design.Data;

/// <summary>Represents to the designer a relationship between two tables in the data source accessed through a data connection. This class cannot be inherited.</summary>
public sealed class DesignerDataRelationship
{
	private string name;

	private ICollection parent_columns;

	private ICollection child_columns;

	private DesignerDataTable child_table;

	/// <summary>Gets the name of the relationship.</summary>
	/// <returns>The name of the relationship.</returns>
	public string Name => name;

	/// <summary>Gets a collection of columns from the parent table that are part of the relationship between two tables.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> objects that define the relationship in the parent table.</returns>
	public ICollection ParentColumns => parent_columns;

	/// <summary>Gets the child table referenced in the relationship.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DesignerDataTable" /> object that represents the child table in the relationship.</returns>
	public DesignerDataTable ChildTable => child_table;

	/// <summary>Gets a collection of columns from the child table that are part of the relationship.</summary>
	/// <returns>A collection of <see cref="T:System.ComponentModel.Design.Data.DesignerDataColumn" /> objects that define the relationship in the child table.</returns>
	public ICollection ChildColumns => child_columns;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataRelationship" /> class with the specified name, parent column, and child relationship.</summary>
	/// <param name="name">The name of the relationship.</param>
	/// <param name="parentColumns">The columns in the parent table that define the relationship.</param>
	/// <param name="childTable">The child table in the relationship.</param>
	/// <param name="childColumns">The columns in the child table that define the relationship.</param>
	public DesignerDataRelationship(string name, ICollection parentColumns, DesignerDataTable childTable, ICollection childColumns)
	{
		this.name = name;
		parent_columns = parentColumns;
		child_table = childTable;
		child_columns = childColumns;
	}
}
