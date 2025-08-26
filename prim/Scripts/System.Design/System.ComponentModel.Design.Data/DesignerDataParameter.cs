using System.Data;

namespace System.ComponentModel.Design.Data;

/// <summary>Represents a parameter for a stored procedure. This class cannot be inherited.</summary>
public sealed class DesignerDataParameter
{
	private string name;

	private DbType type;

	private ParameterDirection direction;

	/// <summary>Gets the database type of the parameter.</summary>
	/// <returns>One of the <see cref="T:System.Data.DbType" /> values.</returns>
	public DbType DataType => type;

	/// <summary>Gets the name of the parameter.</summary>
	/// <returns>The name of the parameter.</returns>
	public string Name => name;

	/// <summary>Gets a value indicating whether the parameter is input-only, output-only, bidirectional, or a stored procedure return-value parameter.</summary>
	/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values.</returns>
	public ParameterDirection Direction => direction;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataParameter" /> class with the specified name, data type, and input/output semantics.</summary>
	/// <param name="name">The name of the parameter.</param>
	/// <param name="dataType">One of the <see cref="T:System.Data.DbType" /> values.</param>
	/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
	public DesignerDataParameter(string name, DbType dataType, ParameterDirection direction)
	{
		this.name = name;
		type = dataType;
		this.direction = direction;
	}
}
